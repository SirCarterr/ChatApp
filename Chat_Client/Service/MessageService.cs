using Chat_Client.Service.IService;
using Chat_Models;
using Newtonsoft.Json;
using System.Text;

namespace Chat_Client.Service
{
    public class MessageService : IMessageService
    {
        private readonly HttpClient _httpClient;

        public MessageService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ChatMessageDTO> CreateMessage(ChatMessageDTO messageDTO)
        {
            var content = JsonConvert.SerializeObject(messageDTO);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/api/message/CreateMessage", bodyContent);
            string responseResult = response.Content.ReadAsStringAsync().Result;
            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<ChatMessageDTO>(responseResult);
                return result;
            }
            return new ChatMessageDTO();
        }

        public async Task<bool> DeleteMessage(int id)
        {
            var response = await _httpClient.DeleteAsync($"/api/message/DeleteMessage/{id}");
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<ChatMessageDTO>> GetChatMessages(int chatId)
        {
            var response = await _httpClient.GetAsync($"/api/message/GetMessages/{chatId}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var messages = JsonConvert.DeserializeObject<IEnumerable<ChatMessageDTO>>(content);
                return messages.Reverse();
            }
            return new List<ChatMessageDTO>();
        }

        public async Task<IEnumerable<ChatMessageDTO>> GetMoreChatMessages(int chatId, int startId)
        {
            var response = await _httpClient.GetAsync($"/api/message/GetMoreMessages/{chatId}/{startId}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var messages = JsonConvert.DeserializeObject<IEnumerable<ChatMessageDTO>>(content);
                return messages.Reverse();
            }
            return new List<ChatMessageDTO>();
        }

        public async Task<int> LoadNewMessages(int chatId, string userId)
        {
            var response = await _httpClient.GetAsync($"/api/message/LoadNewMessages/{chatId}/{userId})");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var count = JsonConvert.DeserializeObject<int>(content);
                return count;
            }
            return 0;
        }

        public async Task<ChatMessageDTO> UpdateMessage(ChatMessageDTO messageDTO)
        {
            var content = JsonConvert.SerializeObject(messageDTO);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/api/message/UpdateMessage", bodyContent);
            string responseResult = response.Content.ReadAsStringAsync().Result;
            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<ChatMessageDTO>(responseResult);
                return result;
            }
            return new ChatMessageDTO();
        }
    }
}
