using AutoMapper;
using Chat_DataAccess;
using Chat_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_Business.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Chat, ChatDTO>().ReverseMap();
            CreateMap<ChatMessage, ChatMessageDTO>().ReverseMap();
            CreateMap<UserChatName, UserChatNameDTO>().ReverseMap();
        }
    }
}
