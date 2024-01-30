using AutoMapper;
using LfragmentApi.DTOs;
using LfragmentApi.Entities;

namespace LfragmentApi.RequestHelper
{
    public class MappingProfiler : Profile
    {
            public MappingProfiler()
            {
            CreateMap<CreateFragmentDto, Fragment>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.Updated, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags));

            CreateMap<UpdateFragmentDto, Fragment>()
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.Updated, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags));

            CreateMap<Fragment, UpdateFragmentDto>()
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags));

            CreateMap<Fragment, CreateFragmentDto>()
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags));
        }
    }
}