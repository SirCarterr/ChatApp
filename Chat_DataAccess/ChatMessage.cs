
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_DataAccess
{
    public class ChatMessage
    {
        [Key]
        public long Id { get; set; }
        public int ChatId { get; set; }
        public string FromUserId { get; set; }
        [Required]
        public string Message { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        public bool IsReplyMessage { get; set; }
        public bool IsEdited { get; set; }
        public bool DeletedForCaller { get; set; } = false;
        [ForeignKey("ChatId")]
        public Chat Chat { get; set; }
    }
}
