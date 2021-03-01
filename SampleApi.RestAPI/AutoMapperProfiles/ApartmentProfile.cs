using AutoMapper;
using SampleApi.Data.Entities;
using SampleApi.RestAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApi.RestAPI.AutoMapperProfiles
{
    public class ApartmentProfile : Profile
    {
        public ApartmentProfile()
        {
            CreateMap<Apartment, ApartmentDto>()
                .ForMember(dest => dest.Location, o => o.MapFrom(source => source.Location));
            CreateMap<CreateApartmentDto, Apartment>();
        }
    }
}
