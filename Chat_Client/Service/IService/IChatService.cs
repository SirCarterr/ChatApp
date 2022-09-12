using Chat_Models;
using Microsoft.AspNetCore.Identity;

namespace Chat_Client.Service.IService
{
    public interface IChatService
    {
        public Task<IEnumerable<UserDTO>> GetUsers();
        public Task<IEnumerable<ChatDTO>> GetChats();
        public Task<ChatDTO> CreateChat(ChatDTO messageDTO);
        public Task<ChatDTO> UpdateChat(ChatDTO messageDTO);
        public Task<bool> DeleteChat(int id);
    }
}
