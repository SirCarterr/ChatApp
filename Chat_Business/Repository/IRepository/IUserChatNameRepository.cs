using Chat_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_Business.Repository.IRepository
{
    public interface IUserChatNameRepository
    {
        public Task<IEnumerable<UserChatNameDTO>> GetChatNames(string userId);
        public Task<UserChatNameDTO> CreateChatName(UserChatNameDTO nameDTO);
        public Task<int> DeleteChatName(int id);
        public Task<UserChatNameDTO> UpdateChatName(UserChatNameDTO nameDTO);
    }
}
