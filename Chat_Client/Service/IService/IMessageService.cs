using Chat_Models;

namespace Chat_Client.Service.IService
{
    public interface IMessageService
    {
        public Task<IEnumerable<ChatMessageDTO>> GetChatMessages(int chatId);
        public Task<IEnumerable<ChatMessageDTO>> GetMoreChatMessages(int chatId, int startId);
        public Task<ChatMessageDTO> CreateMessage(ChatMessageDTO messageDTO);
        public Task<ChatMessageDTO> UpdateMessage(ChatMessageDTO messageDTO);
        public Task<bool> DeleteMessage(int id);
    }
}
