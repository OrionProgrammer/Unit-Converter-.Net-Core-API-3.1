using System;
using System.Threading.Tasks;
using App.Api.Helpers;
using App.Model;
using App.Repository.Helpers;
using App.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace App.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UnitController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private IHubContext<UnitHub> _hub;

        private readonly IAuditLogRepository _auditLogRepository;

        Domain.AuditLog auditLog = new Domain.AuditLog();

        public UnitController(IUnitOfWork unitOfWork, 
            IHubContext<UnitHub> hub, 
            IAuditLogRepository auditLogRepository)
        {
            _unitOfWork = unitOfWork;
            _hub = hub;
            _auditLogRepository = auditLogRepository;
        }

        /// <summary>
        /// Converts celcius to farenheit, or farenheit to celcius
        /// </summary>
        /// <param name="unitModel">{Unit, ToMetric, UserId}</param>
        /// <returns></returns>
        [HttpPost("temp")]
        public async Task<IActionResult> Temperature(UnitModel unitModel)
        {
            auditLog.Description = unitModel.ToMetric ? "Converted temperature from farenheit to celcius. Unit Value = " + unitModel.Unit.ToString() :
                    "Converted temperature from celcius to farenheit. Unit Value = " + unitModel.Unit.ToString();
            auditLog.UserId = unitModel.UserId;

            //log user interaction. 
             _unitOfWork.AuditLog.Add(auditLog);
             _unitOfWork.Complete();

            //calculate and return result. 1st calculation is to Metric, 2nd is to Imperial
            double result = 0;
            if (unitModel.ToMetric)
                result = ((unitModel.Unit - 32) * 5) / 9;
            else
                result = ((unitModel.Unit / 5) * 9) + 32;

            unitModel.Result = Math.Round(result, 4);

            //send the result back using SignalR
            await _hub.Clients.All.SendAsync("unitModelResult", unitModel);

            return Ok(unitModel);
        }

        /// <summary>
        /// Converts meters to yards, or yards to meters
        /// </summary>
        /// <param name="unitModel">{Unit, ToMetric, UserId}</param>
        /// <returns></returns>
        [HttpPost("length")]
        public async Task<IActionResult> Length(UnitModel unitModel)
        {
            auditLog.Description = unitModel.ToMetric ? "Converted length from yards to meters. Unit Value = " + unitModel.Unit.ToString() :
                    "Converted length from meters to yards. Unit Value = " + unitModel.Unit.ToString();
            auditLog.UserId = unitModel.UserId;

            //log user interaction.
             _unitOfWork.AuditLog.Add(auditLog);
             _unitOfWork.Complete();

            //calculate and return result. 1st calculation is Metric, 2nd is Imperial
            double result = 0;
            if (unitModel.ToMetric)
                result = unitModel.Unit / 1.09361;
            else
                result = unitModel.Unit * 1.09361;

            unitModel.Result = Math.Round(result, 4);

            //send the result back using SignalR
            await _hub.Clients.All.SendAsync("unitModelResult", unitModel);
            
            return Ok(unitModel);
        }


        /// <summary>
        /// Converts kilograms to pounds, or pounds to kilograms
        /// </summary>
        /// <param name="unitModel">{Unit, ToMetric, UserId}</param>
        /// <returns></returns>
        [HttpPost("mass")]
        public async Task<IActionResult> Mass(UnitModel unitModel)
        {
            auditLog.Description = unitModel.ToMetric ? "Converted mass from pounds to kilograms. Unit Value = " + unitModel.Unit.ToString() :
                    "Converted mass from kilograms to pounds. Unit Value = " + unitModel.Unit.ToString();
            auditLog.UserId = unitModel.UserId;

            //log user interaction.
             _unitOfWork.AuditLog.Add(auditLog);
             _unitOfWork.Complete();

            //calculate and return result. 1st calculation is Metric, 2nd is Imperial
            double result = 0;
            if (unitModel.ToMetric)
                result = unitModel.Unit / 2.20462;
            else
                result = unitModel.Unit * 2.20462;

            unitModel.Result = Math.Round(result, 4);

            //send the result back using SignalR
            await _hub.Clients.All.SendAsync("unitModelResult", unitModel);

            return Ok(unitModel);
        }

        /// <summary>
        /// Converts kilowatt to horsepower, or horsepower to kilowatt
        /// </summary>
        /// <param name="unitModel">{Unit, ToMetric, UserId}</param>
        /// <returns></returns>
        [HttpPost("power")]
        public async Task<IActionResult> Power(UnitModel unitModel)
        {
            auditLog.Description = unitModel.ToMetric ? "Converted power from horsepower to kilowatt. Unit Value = " + unitModel.Unit.ToString() :
                    "Converted power from kilowatt to horsepower. Unit Value = " + unitModel.Unit.ToString();
            auditLog.UserId = unitModel.UserId;

            //log user interaction.
             _unitOfWork.AuditLog.Add(auditLog);
             _unitOfWork.Complete();

            //calculate and return result. 1st calculation is Metric, 2nd is Imperial
            double result = 0;
            if (unitModel.ToMetric)
                result = unitModel.Unit / 1.34102;
            else
                result = unitModel.Unit * 1.34102;

            unitModel.Result = Math.Round(result, 4);

            //send the result back using SignalR
            await _hub.Clients.All.SendAsync("unitModelResult", unitModel);

            return Ok(unitModel);
        }

        /// <summary>
        /// Converts cubic meter to cubic foot, or cubic foot to cubic meter
        /// </summary>
        /// <param name="unitModel">{Unit, ToMetric, UserId}</param>
        /// <returns></returns>
        [HttpPost("volume")]
        public async Task<IActionResult> Volume(UnitModel unitModel)
        {
            auditLog.Description = unitModel.ToMetric ? "Converted volume from cubic foot to cubic meter. Unit Value = " + unitModel.Unit.ToString() :
                    "Converted volume from cubic meter to cubic foot. Unit Value = " + unitModel.Unit.ToString();
            auditLog.UserId = unitModel.UserId;

            //log user interaction.
             _unitOfWork.AuditLog.Add(auditLog);
             _unitOfWork.Complete();

            //calculate and return result. 1st calculation is Metric, 2nd is Imperial
            double result = 0;
            if (unitModel.ToMetric)
                result = unitModel.Unit / 35.3147;
            else
                result = unitModel.Unit * 35.3147;

            unitModel.Result = Math.Round(result, 4);

            //send the result back using SignalR
            await _hub.Clients.All.SendAsync("unitModelResult", unitModel);

            return Ok(unitModel);
        }
    }
}
