using AutoMapper;
using Chat_Business.Mapper;
using Chat_Business.Repository.IRepository;
using Chat_DataAccess;
using Chat_DataAccess.Data;
using Chat_Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_Business.Repository
{
    public class ChatMessageRepository : IChatMessageRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public ChatMessageRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ChatMessageDTO> CreateMessage(ChatMessageDTO message)
        {
            var m = _mapper.Map<ChatMessageDTO, ChatMessage>(message);
            _db.ChatMessages.Add(m);
            await _db.SaveChangesAsync();
            return _mapper.Map<ChatMessage, ChatMessageDTO>(m);
        }

        public async Task<int> DeleteMessage(int id)
        {
            var message = await _db.ChatMessages.FirstOrDefaultAsync(m => m.Id == id);
            if (message != null)
            {
                var previousMessage = _db.ChatMessages.OrderByDescending(m => m.Id)
                .Where(m => m.Id < message.Id).Take(1);
                if (previousMessage.FirstOrDefault() != null)
                {
                    if (previousMessage.First().IsReplyMessage)
                    {
                        _db.ChatMessages.Remove(previousMessage.First());
                    }
                }
                _db.ChatMessages.Remove(message);
                return await _db.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<IEnumerable<ChatMessageDTO>> GetChatMessages(int chatId)
        {
            return _mapper.Map<IEnumerable<ChatMessage>, IEnumerable<ChatMessageDTO>>(_db.ChatMessages.Include(c => c.Chat).OrderByDescending(m => m.Id)
                .Where(m => m.ChatId == chatId).Take(20));
        }

        public async Task<IEnumerable<ChatMessageDTO>> GetMoreChatMessages(int chatId, int startId)
        {
            return _mapper.Map<IEnumerable<ChatMessage>, IEnumerable<ChatMessageDTO>>(_db.ChatMessages.Include(c => c.Chat).OrderByDescending(m => m.Id)
                .Where(m => m.Id < startId && m.ChatId == chatId).Take(20));
        }

        public async Task<int> GetNewMessagesNumber(int chatId, string userId)
        {
            var messages = _db.ChatMessages.Include(c => c.Chat).OrderByDescending(m => m.Id).Where(m => m.ChatId == chatId);
            int count = 0;

            foreach (var message in messages)
            {
                if (message.FromUserId.Equals(userId))
                {
                    break;
                }
                else
                {
                    count++;
                }
            }

            return count;
        }

        public async Task<ChatMessageDTO> UpdateMessage(ChatMessageDTO message)
        {
            var m_db = await _db.ChatMessages.FirstOrDefaultAsync(m => m.Id == message.Id);
            if (m_db != null)
            {
                if (!m_db.Message.Equals(message.Message))
                {
                    var replied = _db.ChatMessages.Where(m => m.CreatedDate.Equals(message.CreatedDate) && m.IsReplyMessage && m.Message.Equals(m_db) 
                    && m.Chat.Id == message.Chat!.Id);
                    foreach (var reply in replied)
                    {
                        string replySplit = reply.Message.Split(':')[0];
                        reply.Message = replySplit + " " + message.Message;
                        _db.ChatMessages.Update(reply);
                    }
                    m_db.IsEdited = true;
                    m_db.Message = message.Message;
                }
                else
                {
                    m_db.DeletedForCaller = message.DeletedForCaller;
                }
                _db.ChatMessages.Update(m_db);
                await _db.SaveChangesAsync();
                return _mapper.Map<ChatMessage, ChatMessageDTO>(m_db);
            }
            return message;
        }
    }
}
