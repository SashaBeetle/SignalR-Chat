using AutoMapper;
using SignalRChat_backend.API.Mapping.DTOs;
using SignalRChat_backend.Data.Entities;

namespace SignalRChat_backend.API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Chat, ChatDTO>();
            CreateMap<Message, MessageDTO>();
            CreateMap<User, UserDTO>();

            CreateMap<ChatDTO, Chat>();
            CreateMap<MessageDTO, Message>();
            CreateMap<UserDTO, User>();
        }
    }
}
