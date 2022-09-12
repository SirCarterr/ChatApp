using Chat_Models;

namespace Chat_Client.Service.IService
{
    public interface IAuthenticationService
    {
        Task<SignUpResponseDTO> RegisterUser(SignUpRequestDTO requestDTO);
        Task<SignInResponseDTO> LoginUser(SignInRequestDTO requestDTO);
        Task Logout();
    }
}
