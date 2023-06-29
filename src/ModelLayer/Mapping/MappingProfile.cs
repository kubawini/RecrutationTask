using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ModelLayer.DTOs;
using ModelLayer.Models;

namespace ModelLayer.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserModel, UserDTO>().ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.surename));
            CreateMap<UserDTO, UserModel>().ForMember(dest => dest.surename, opt => opt.MapFrom(src => src.Surname));
        }
    }
}
