using Chat_Models;

namespace Chat_Client.Service.IService
{
    public interface IChatNameService
    {
        public Task<IEnumerable<UserChatNameDTO>> GetNames(string userId);
        public Task<UserChatNameDTO> Create(UserChatNameDTO nameDTO);
        public Task<UserChatNameDTO> Update(UserChatNameDTO nameDTO);
        public Task<bool> Delete(int id);
    }
}
