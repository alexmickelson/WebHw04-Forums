using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebHw04_Forums.Models
{
    public class UserViewModel
    {
        public string UserName { get; set; }
        public string UserId { get; set; }
        public List<string> Roles { get; set; }
    }
}
