using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_Models
{
    public class UserChatNameDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int ChatId { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
