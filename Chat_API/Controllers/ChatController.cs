using Chat_Business.Repository.IRepository;
using Chat_DataAccess.Data;
using Chat_Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Chat_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class ChatController : Controller
    {
        private readonly IChatRepository _chatRepository;
        private readonly ApplicationDbContext _db;

        public ChatController(IChatRepository chatRepository, ApplicationDbContext db)
        {
            _chatRepository = chatRepository;
            _db = db;
        }

        [HttpGet]
        [ActionName("GetUsers")]
        public async Task<IActionResult> GetUsers()
        {
            var currentUserId = User.Claims.FirstOrDefault(c => c.Type.ToString() == "Id")!.Value;
            var users = _db.Users.Where(u => u.Id != currentUserId).ToList();
            List<UserDTO> usersDTO = new List<UserDTO>();

            foreach (var user in users)
            {
                usersDTO.Add(new UserDTO()
                {
                    Id = user.Id,
                    Email = user.Email
                });
            }

            return Ok(usersDTO);
        }

        [HttpGet]
        [ActionName("GetChats")]
        public async Task<IActionResult> GetChats()
        {
            string userId = User.Claims.FirstOrDefault(c => c.Type.ToString() == "Id")!.Value;
            return Ok(await _chatRepository.GetUserChats(userId));
        }

        [HttpPost]
        [ActionName("CreateChat")]
        public async Task<IActionResult> CreateChat([FromBody] ChatDTO chatDTO)
        {
            var chat = await _chatRepository.CreateChat(chatDTO);
            return Ok(chat);
        }

        [HttpPost]
        [ActionName("UpdateChat")]
        public async Task<IActionResult> UpdateChat([FromBody] ChatDTO chatDTO)
        {
            var chat = await _chatRepository.UpdateChat(chatDTO);
            return Ok(chat);
        }

        [HttpDelete("{id}")]
        [ActionName("DeleteChat")]
        public async Task<IActionResult> DeleteChat(int? id)
        {
            if (id == 0 || id == null)
            {
                return BadRequest(new ErrorModelDTO()
                {
                    ErrorMessage = "Invalid request",
                    StatusCode = StatusCodes.Status404NotFound
                });
            }

            await _chatRepository.DeleteChat(id.Value);

            return Ok(201);
        }
    }
}
