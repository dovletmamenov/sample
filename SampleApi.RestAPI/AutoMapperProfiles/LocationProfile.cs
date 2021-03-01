using AutoMapper;
using SampleApi.Data.Entities;
using SampleApi.RestAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApi.RestAPI.AutoMapperProfiles
{
    public class LocationProfile : Profile
    {
        public LocationProfile()
        {
            CreateMap<Location, LocationDto>();
            CreateMap<CreateLocationDto, Location>();
        }
    }
}
