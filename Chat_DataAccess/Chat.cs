using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_DataAccess
{
    public class Chat
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; } //creator
        [Required]
        public string Name { get; set; }
        [Required]
        public string Users { get; set; }
        [Required]
        public string UsersIds { get; set; }
    }
}
