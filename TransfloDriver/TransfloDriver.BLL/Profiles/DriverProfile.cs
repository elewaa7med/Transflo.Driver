using AutoMapper;
using Transflo.Entities;
using TransflowDriver.DTO.ViewModels.Driver;

namespace TransfloDriver.BLL.Profiles
{
    public class DriverProfile : Profile
    {
        public DriverProfile()
        {
            CreateMap<Driver, AddDriverModel>().ReverseMap()
                .ForMember(x=>x.Id, z=> z.MapFrom(x=>Guid.NewGuid()));
            CreateMap<Driver, DriverViewModel>().ReverseMap();
            CreateMap<Driver, UpdateDriverModel>().ReverseMap();
        }
    }
}
