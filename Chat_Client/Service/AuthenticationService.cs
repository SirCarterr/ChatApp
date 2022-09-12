using Blazored.LocalStorage;
using Chat_Client.Service.IService;
using Microsoft.AspNetCore.Components.Authorization;
using Chat_Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using Chat_Common;

namespace Chat_Client.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationStateProvider _authStateProvider;

        public AuthenticationService(HttpClient httpClient, ILocalStorageService localStorage, AuthenticationStateProvider authStateProvider)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
            _authStateProvider = authStateProvider;
        }

        public async Task<SignInResponseDTO> LoginUser(SignInRequestDTO requestDTO)
        {
            var content = JsonConvert.SerializeObject(requestDTO);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/account/signin", bodyContent);
            var contentTemp = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<SignInResponseDTO>(contentTemp);

            if (response.IsSuccessStatusCode)
            {
                await _localStorage.SetItemAsync(SD.Local_Token, result.Token);
                await _localStorage.SetItemAsync(SD.Local_UserDetails, result.UserDTO);
                ((AuthStateProvider)_authStateProvider).NotifyUserLoggedIn(result.Token);
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.Token);
                return new SignInResponseDTO() { IsAuthSuccessful = true };
            }
            else
            {
                return result;
            }
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync(SD.Local_Token);
            await _localStorage.RemoveItemAsync(SD.Local_UserDetails);

            ((AuthStateProvider)_authStateProvider).NotifyUserLogout();

            _httpClient.DefaultRequestHeaders.Authorization = null;
        }

        public async Task<SignUpResponseDTO> RegisterUser(SignUpRequestDTO requestDTO)
        {
            var content = JsonConvert.SerializeObject(requestDTO);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/account/signup", bodyContent);
            var contentTemp = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<SignUpResponseDTO>(contentTemp);

            if (response.IsSuccessStatusCode)
            {
                return new SignUpResponseDTO() { IsRegistrationSuccessful = true };
            }
            else
            {
                return new SignUpResponseDTO() { IsRegistrationSuccessful = false, Errors = result.Errors };
            }
        }
    }
}
