using AutoMapper;
using SignalRChat_backend.API.Mapping.DTOs;
using SignalRChat_backend.Data.Entities;

namespace SignalRChat_backend.API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDTO>()
                .ForMember(dest => dest.ChatIds, opt => opt.MapFrom(src => src.UserChats.Select(uc => uc.ChatId)))
                .ReverseMap()
                .ForMember(dest => dest.UserChats, opt => opt.MapFrom(src => src.ChatIds.Select(chatId => new UserChat { ChatId = chatId, UserId = src.Id })));

            CreateMap<Chat, ChatDTO>()
                .ForMember(dest => dest.UserIds, opt => opt.MapFrom(src => src.UserChats.Select(uc => uc.UserId)))
                .ReverseMap()
                .ForMember(dest => dest.UserChats, opt => opt.MapFrom(src => src.UserIds.Select(userId => new UserChat { UserId = userId, ChatId = src.Id })));
            
            CreateMap<Message, MessageDTO>().ReverseMap();
            CreateMap<UserChat, UserChatDTO>().ReverseMap();
        }
    }
}
