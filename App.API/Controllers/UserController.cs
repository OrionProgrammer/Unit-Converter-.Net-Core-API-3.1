using Microsoft.AspNetCore.Mvc;
using App.Repository.Helpers;
using System;
using App.Model;
using AutoMapper;
using App.Api.Helpers;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using App.Domain;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;

        public UserController(IUnitOfWork unitOfWork,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        //authenticate
        [HttpGet]
        public IActionResult Index()
        {
            return Ok();
        }

        //authenticate
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] AuthenticateModel model)
        {
            if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
                return BadRequest(new { message = "Username and password is required" });

            var user = _unitOfWork.User.GetByEmail(model.Email);

            // check if user exists
            if (user == null)
                return BadRequest(new { message = "Username is incorrect" });

            // check if password is correct
            if (!UserSecurity.VerifyPasswordHash(model.Password, user.PasswordHash, user.PasswordSalt))
                return BadRequest(new { message = "Password is incorrect" });

            //create JWT
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // return basic user info and authentication token
            return Ok(new
            {
                user.Id,
                user.Email,
                user.Name,
                user.Surname,
                Token = tokenString
            });
        }

        //register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            // map model to entity
            var user = _mapper.Map<User>(model);

            //check if email already exists
            var existingUser = _unitOfWork.User.GetByEmail(model.Email);

            // check if user exists
            if (existingUser != null)
                return BadRequest(new { message = "Email already exists!" });

            try
            {
                byte[] passwordHash, passwordSalt;
                UserSecurity.CreatePasswordHash(model.Password, out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;

                // create user
                _unitOfWork.User.Add(user);
                return Ok(await _unitOfWork.Complete());
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }


    }
}
