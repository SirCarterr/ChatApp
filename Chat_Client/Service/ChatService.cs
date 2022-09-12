using Chat_Client.Service.IService;
using Chat_Models;
using Newtonsoft.Json;
using System.Text;

namespace Chat_Client.Service
{
    public class ChatService : IChatService
    {
        private readonly HttpClient _httpClient;

        public ChatService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ChatDTO> CreateChat(ChatDTO messageDTO)
        {
            var content = JsonConvert.SerializeObject(messageDTO);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/api/chat/CreateChat", bodyContent);
            string responseResult = response.Content.ReadAsStringAsync().Result;
            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<ChatDTO>(responseResult);
                return result;
            }
            return new ChatDTO();
        }

        public async Task<bool> DeleteChat(int id)
        {
            var response = await _httpClient.DeleteAsync($"/api/chat/DeleteChat/{id}");
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<ChatDTO>> GetChats()
        {
            var response = await _httpClient.GetAsync("/api/chat/GetChats");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var chats = JsonConvert.DeserializeObject<IEnumerable<ChatDTO>>(content);
                return chats;
            }
            return new List<ChatDTO>();
        }

        public async Task<IEnumerable<UserDTO>> GetUsers()
        {
            var response = await _httpClient.GetAsync("/api/chat/GetUsers");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var users = JsonConvert.DeserializeObject<IEnumerable<UserDTO>>(content);
                return users;
            }
            return new List<UserDTO>();
        }

        public async Task<ChatDTO> UpdateChat(ChatDTO messageDTO)
        {
            var content = JsonConvert.SerializeObject(messageDTO);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/api/chat/UpdateChat", bodyContent);
            string responseResult = response.Content.ReadAsStringAsync().Result;
            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<ChatDTO>(responseResult);
                return result;
            }
            return new ChatDTO();
        }
    }
}
