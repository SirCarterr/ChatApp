using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_Models
{
    public class NotificationsDTO
    {
        public int ChatId { get; set; }
        public string ChatName { get; set; }
        public int NewMessages { get; set; }
    }
}
