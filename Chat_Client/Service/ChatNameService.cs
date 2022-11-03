using Chat_Client.Service.IService;
using Chat_Models;
using Newtonsoft.Json;
using System.Text;

namespace Chat_Client.Service
{
    public class ChatNameService : IChatNameService
    {
        private readonly HttpClient _httpClient;

        public ChatNameService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UserChatNameDTO> Create(UserChatNameDTO nameDTO)
        {
            var content = JsonConvert.SerializeObject(nameDTO);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/api/chatname/Create", bodyContent);
            string responseResult = response.Content.ReadAsStringAsync().Result;
            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<UserChatNameDTO>(responseResult);
                return result;
            }
            return new UserChatNameDTO();
        }

        public async Task<bool> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"/api/chatname/Delete/{id}");
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<UserChatNameDTO>> GetNames(string userId)
        {
            var response = await _httpClient.GetAsync($"/api/chatname/getnames/{userId}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var chatNames = JsonConvert.DeserializeObject<IEnumerable<UserChatNameDTO>>(content);
                return chatNames;
            }
            return new List<UserChatNameDTO>();
        }

        public async Task<UserChatNameDTO> Update(UserChatNameDTO nameDTO)
        {
            var content = JsonConvert.SerializeObject(nameDTO);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/api/chatname/Update", bodyContent);
            string responseResult = response.Content.ReadAsStringAsync().Result;
            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<UserChatNameDTO>(responseResult);
                return result;
            }
            return new UserChatNameDTO();
        }
    }
}
