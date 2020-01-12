using AutoMapper;
using Dating.DTO;
using Dating.Models;
using System;
using System.Linq;

namespace Dating.Common.Extensions
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<UserModel, UserForListDto>()
                .ForMember(dest => dest.PhotoUrl,
                    opt => opt.MapFrom(src => src.Photos.FirstOrDefault(x => x.IsMain).Url))
                .ForMember(dest => dest.Age,
                    opt => opt.MapFrom(src => src.DateOfBirth.CalcuateAge()));

            CreateMap<UserModel, UserForDetailedDto>()
                 .ForMember(dest => dest.PhotoUrl,
                    opt => opt.MapFrom(src => src.Photos.FirstOrDefault(x => x.IsMain).Url))
                 .ForMember(dest => dest.Age,
                    opt => opt.MapFrom(src => src.DateOfBirth.CalcuateAge()));

            CreateMap<PhotoModel, PhotosForDetailedDto>();
            CreateMap<UserModel, UserForUdpateDto>();
            CreateMap<UserForUdpateDto, UserModel>();
            CreateMap<PhotoModel, PhotoForReturnDto>();
            CreateMap<PhotoForCreatingDto, PhotoModel>();
        }
    }
}
