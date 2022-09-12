using Chat_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_Business.Repository.IRepository
{
    public interface IChatRepository
    {
        public Task<IEnumerable<ChatDTO>> GetUserChats(string userId);
        public Task<ChatDTO> CreateChat(ChatDTO chat);
        public Task<int> DeleteChat(int chatId);
        public Task<ChatDTO> UpdateChat(ChatDTO chat);
    }
}
