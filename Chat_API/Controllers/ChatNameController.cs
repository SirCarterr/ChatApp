using Chat_Business.Repository;
using Chat_Business.Repository.IRepository;
using Chat_Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chat_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class ChatNameController : Controller
    {
        private readonly IUserChatNameRepository _userChatNameRepository;

        public ChatNameController(IUserChatNameRepository userChatNameRepository)
        {
            _userChatNameRepository = userChatNameRepository;
        }

        [HttpGet("{userId}")]
        [ActionName("GetNames")]
        public async Task<IActionResult> GetChatNames(string? userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest(new ErrorModelDTO()
                {
                    ErrorMessage = "Invalid Id",
                    StatusCode = StatusCodes.Status406NotAcceptable
                });
            }

            return Ok(await _userChatNameRepository.GetChatNames(userId));
        }

        [HttpPost]
        [ActionName("Create")]
        public async Task<IActionResult> CreateChatName([FromBody] UserChatNameDTO nameDTO)
        {
            var name = await _userChatNameRepository.CreateChatName(nameDTO);
            return Ok(name);
        }

        [HttpPost]
        [ActionName("Update")]
        public async Task<IActionResult> Update([FromBody] UserChatNameDTO nameDTO)
        {
            var name = await _userChatNameRepository.UpdateChatName(nameDTO);
            return Ok(name);
        }

        [HttpDelete("{id}")]
        [ActionName("Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == 0 || id == null)
            {
                return BadRequest(new ErrorModelDTO()
                {
                    ErrorMessage = "Invalid Id",
                    StatusCode = StatusCodes.Status406NotAcceptable
                });
            }

            await _userChatNameRepository.DeleteChatName(id.Value);

            return Ok(201);
        }
    }
}
