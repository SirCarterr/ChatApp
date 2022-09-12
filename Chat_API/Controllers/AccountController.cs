using Chat_API.Helper;
using Chat_Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Chat_Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Chat_API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly APISettings _aPISettings;

        public AccountController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IOptions<APISettings> options)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _aPISettings = options.Value;
        }

        [HttpPost]
        public async Task<IActionResult> SignUp([FromBody] SignUpRequestDTO signUpRequestDTO)
        {
            if (signUpRequestDTO == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = new IdentityUser
            {
                UserName = signUpRequestDTO.Email,
                Email = signUpRequestDTO.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, signUpRequestDTO.Password);

            if (!result.Succeeded)
            {
                return BadRequest(new SignUpResponseDTO()
                {
                    IsRegistrationSuccessful = false,
                    Errors = result.Errors.Select(u => u.Description)
                });
            }

            return StatusCode(201);
        }

        [HttpPost]
        public async Task<IActionResult> SignIn([FromBody] SignInRequestDTO signInRequestDTO)
        {
            if (signInRequestDTO == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _signInManager.PasswordSignInAsync(signInRequestDTO.Username, signInRequestDTO.Password, false, false);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(signInRequestDTO.Username);
                if (user == null)
                {
                    return Unauthorized(new SignInResponseDTO
                    {
                        IsAuthSuccessful = false,
                        ErrorMessage = "Invalid Authentication"
                    });
                }

                //we need to login
                var signingCredentials = GetSigningCredentials();
                var claims = await GetClaims(user);

                var tokenOptions = new JwtSecurityToken(
                    issuer: _aPISettings.ValidIssuer,
                    audience: _aPISettings.ValidAudience,
                    claims: claims,
                    expires: DateTime.Now.AddDays(30),
                    signingCredentials: signingCredentials);

                var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

                return Ok(new SignInResponseDTO()
                {
                    IsAuthSuccessful = true,
                    Token = token,
                    UserDTO = new UserDTO()
                    {
                        Id = user.Id,
                        Email = user.Email
                    }
                });
            }
            else
            {
                return Unauthorized(new SignInResponseDTO
                {
                    IsAuthSuccessful = false,
                    ErrorMessage = "Invalid Authentication"
                });
            }

            return StatusCode(201);
        }

        private SigningCredentials GetSigningCredentials()
        {
            var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_aPISettings.SecretKey));

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaims(IdentityUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("Id", user.Id),
            };

            return claims;
        }
    }
}
