using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_Models
{
    public class ChatDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        [Required]
        public string CommunicationType { get; set; }
        public string? GroupName { get; set; }
        [Required]
        public string Users { get; set; }
        public string UsersIds { get; set; }
    }
}
