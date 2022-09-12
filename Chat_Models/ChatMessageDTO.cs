using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_Models
{
    public class ChatMessageDTO
    {
        public long Id { get; set; }
        public int ChatId { get; set; }
        public string FromUserId { get; set; }
        [Required]
        public string Message { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool IsReplyMessage { get; set; } = false;
        public bool IsEdited { get; set; } = false;
        public bool DeletedForCaller { get; set; } = false;
        public ChatDTO? Chat { get; set; }
    }
}
