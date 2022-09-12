using Chat_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_Business.Repository.IRepository
{
    public interface IChatMessageRepository
    {
        public Task<IEnumerable<ChatMessageDTO>> GetChatMessages(int chatId);
        public Task<IEnumerable<ChatMessageDTO>> GetMoreChatMessages(int chatId, int startId);
        public Task<ChatMessageDTO> CreateMessage(ChatMessageDTO message);
        public Task<ChatMessageDTO> UpdateMessage(ChatMessageDTO message);
        public Task<int> DeleteMessage(int id);
    }
}
