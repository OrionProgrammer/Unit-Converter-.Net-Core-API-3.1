using AutoMapper;
using App.Domain;
using App.Model;

namespace App.UI.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //Employee
            CreateMap<User, RegisterModel>().ReverseMap();
            CreateMap<User, RegisterModel>();

            CreateMap<User, UserModel>().ReverseMap();
            CreateMap<User, UserModel>();

        }
    }
}
