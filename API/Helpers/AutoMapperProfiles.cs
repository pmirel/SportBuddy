using System.Linq;
using API.DTOs;
using API.Entities;
using AutoMapper;

namespace API.Extensions
{
  public class AutoMapperProfiles : Profile
  {
    public AutoMapperProfiles()
    {
      CreateMap<AppUser, MemberDto>()
      .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src =>
      src.Photos.FirstOrDefault(x => x.IsMain).Url))
      .ForMember(dest => dest.Age, opt => opt.MapFrom(src =>
      src.DateOfBirth.CalculateAge()));
      CreateMap<Photo, PhotoDto>();
      CreateMap<MemberUpdateDto, AppUser>();
      CreateMap<RegisterDto, AppUser>();
      CreateMap<Message, MessageDto>()
      .ForMember(dest => dest.SenderPhotoUrl,
      opt => opt.MapFrom(src => src.Sender.Photos.FirstOrDefault(x => x.IsMain).Url))
      .ForMember(dest => dest.RecipientPhotoUrl,
      opt => opt.MapFrom(src => src.Recipient.Photos.FirstOrDefault(x => x.IsMain).Url));
      CreateMap<Challenge, ChallengeDto>()
      .ForMember(dest => dest.SenderPhotoUrl,
      opt => opt.MapFrom(src => src.Sender.Photos.FirstOrDefault(x => x.IsMain).Url))
      .ForMember(dest => dest.RecipientPhotoUrl,
      opt => opt.MapFrom(src => src.Recipient.Photos.FirstOrDefault(x => x.IsMain).Url));
      CreateMap<ChallengeUpdateDto, ChallengeDto>();
    }
  }
}