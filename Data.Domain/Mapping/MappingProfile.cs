using AutoMapper;
using Data.Domain.Dto.Weather;
using Data.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Domain.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Weather, WeatherAddDto>();
            CreateMap<Weather, WeatherAddDto>().ReverseMap();

            CreateMap<Weather, WeatherDto>();
            CreateMap<Weather, WeatherDto>().ReverseMap();
        }
    }
}
