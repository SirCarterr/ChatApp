using AutoMapper;
using Chat_Business.Repository.IRepository;
using Chat_DataAccess;
using Chat_DataAccess.Data;
using Chat_Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Chat_Business.Repository
{
    public class ChatRepository : IChatRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public ChatRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ChatDTO> CreateChat(ChatDTO chat)
        {
            var c = _mapper.Map<ChatDTO, Chat>(chat);
            _db.Chats.Add(c);
            await _db.SaveChangesAsync();
            return _mapper.Map<Chat, ChatDTO>(c);
        }

        public async Task<int> DeleteChat(int chatId)
        {
            var c = await _db.Chats.FirstOrDefaultAsync(i => i.Id == chatId);
            if (c != null)
            {
                _db.Chats.Remove(c);
                return await _db.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<IEnumerable<ChatDTO>> GetUserChats(string userId)
        {
            var chats = _db.Chats;
            string regex = $"({userId})";
            List<ChatDTO> userChats = new List<ChatDTO>();
            foreach (var chat in chats)
            {
                if (Regex.IsMatch(chat.UsersIds, regex) || Regex.IsMatch(chat.UserId, regex))
                {
                    userChats.Add(_mapper.Map<Chat, ChatDTO>(chat));
                }
            }
            return userChats;
        }

        public async Task<ChatDTO> UpdateChat(ChatDTO chat)
        {
            var c = await _db.Chats.FirstOrDefaultAsync(i => i.Id == chat.Id);
            if (c != null)
            {
                c.GroupName = chat.GroupName;
                c.Users = chat.Users;
                c.UsersIds = chat.UsersIds;
                _db.Chats.Update(c);
                await _db.SaveChangesAsync();
                return _mapper.Map<Chat, ChatDTO>(c);
            }
            return chat;
        }
    }
}
