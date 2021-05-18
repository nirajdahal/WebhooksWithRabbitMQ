using AirlineWeb.Dtos;
using AirlineWeb.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineWeb.Profiles
{
    public class FlightProfile : Profile
    {
        public FlightProfile()
        {
            CreateMap<FlightDetailCreationDto, FlightDetail>();
            CreateMap<FlightDetail, FlightDetailDto>();
            CreateMap<FlightDetailForUpdateDto, FlightDetail>();
            CreateMap<FlightDetail, FlightDetailForUpdateDto>();
        }
    }
}
