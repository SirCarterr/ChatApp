using Chat_Business.Repository.IRepository;
using Chat_Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chat_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class MessageController : Controller
    {
        private readonly IChatMessageRepository _chatMessageRepository;

        public MessageController(IChatMessageRepository chatMessageRepository)
        {
            _chatMessageRepository = chatMessageRepository;
        }

        [HttpGet("{id}")]
        [ActionName("GetMessages")]
        public async Task<IActionResult> GetMessages(int? id) 
        {
            if (id == 0 || id == null)
            {
                return BadRequest(new ErrorModelDTO()
                {
                    ErrorMessage = "Invalid Id",
                    StatusCode = StatusCodes.Status406NotAcceptable
                });
            }

            return Ok(await _chatMessageRepository.GetChatMessages(id.Value));
        }

        [HttpGet("{id}/{startId}")]
        [ActionName("GetMoreMessages")]
        public async Task<IActionResult> GetMoreMessages(int? id, int? startId) 
        {
            if (id == 0 || id == null || startId == null)
            {
                return BadRequest(new ErrorModelDTO()
                {
                    ErrorMessage = "Invalid Id",
                    StatusCode = StatusCodes.Status406NotAcceptable
                });
            }

            return Ok(await _chatMessageRepository.GetMoreChatMessages(id.Value, startId.Value));
        }

        [HttpPost]
        [ActionName("CreateMessage")]
        public async Task<IActionResult> CreateMessage([FromBody] ChatMessageDTO chatMessageDTO)
        {
            chatMessageDTO.CreatedDate = DateTime.Now;
            var message = await _chatMessageRepository.CreateMessage(chatMessageDTO);
            return Ok(message);
        }

        [HttpPost]
        [ActionName("UpdateMessage")]
        public async Task<IActionResult> UpdateMessage([FromBody] ChatMessageDTO chatMessageDTO)
        {
            var message = await _chatMessageRepository.UpdateMessage(chatMessageDTO);
            return Ok(message);
        }

        [HttpDelete("{id}")]
        [ActionName("DeleteMessage")]
        public async Task<IActionResult> DeleteMessage(int? id)
        {
            if (id == 0 || id == null)
            {
                return BadRequest(new ErrorModelDTO()
                {
                    ErrorMessage = "Invalid Id",
                    StatusCode = StatusCodes.Status406NotAcceptable
                });
            }

            await _chatMessageRepository.DeleteMessage(id.Value);

            return Ok(201);
        }

        [HttpGet("{chatId}/{userId}")]
        [ActionName("LoadNewMessages")]
        public async Task<IActionResult> LoadNewMessages(int? chatId, string? userId)
        {
            if (chatId == 0 || chatId == null)
            {
                return BadRequest(new ErrorModelDTO()
                {
                    ErrorMessage = "Invalid chatId",
                    StatusCode = StatusCodes.Status406NotAcceptable
                });
            }

            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest(new ErrorModelDTO()
                {
                    ErrorMessage = "Invalid userId",
                    StatusCode = StatusCodes.Status406NotAcceptable
                });
            }

            int count = await _chatMessageRepository.GetNewMessagesNumber(chatId.Value, userId);
            return Ok(count);
        }
    }
}
