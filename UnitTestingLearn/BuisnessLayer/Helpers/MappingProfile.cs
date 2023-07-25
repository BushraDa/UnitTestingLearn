using AutoMapper;
using UnitTestingLearn.DataLayer.Dtos;
using UnitTestingLearn.DataLayer.Models;

namespace UnitTestingLearn.BuisnessLayer
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CategoryAddDto, Category>()
                .ForMember(dest => dest.Image, opt => opt.Ignore());
        }
    }
}
