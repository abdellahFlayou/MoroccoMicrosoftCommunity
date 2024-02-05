using AutoMapper;
using MoroccoMicrosoftCommunity.Application.Dtos;
using MoroccoMicrosoftCommunity.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoroccoMicrosoftCommunity.Application.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
           CreateMap<Evenement , EventDto>().ReverseMap();
            CreateMap<SessionDto, Session>()
     //.ForMember(dest => dest.Image, opt => opt.MapFrom(src => Convert.FromBase64String(src.Image)))
     .ReverseMap();

            CreateMap<Support , SupportDto>().ReverseMap();
            CreateMap<Partenaire , PartenaireDto>().ReverseMap();
            CreateMap<Participant ,ParticipantDto>().ReverseMap();
            CreateMap<Sponsor ,SponsorDto>().ReverseMap();
            CreateMap<Speaker ,SpeakerDto>().ReverseMap();
        }

    }
}
