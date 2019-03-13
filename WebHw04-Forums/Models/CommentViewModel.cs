using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebHw04_Forums.Models
{
    public class CommentViewModel
    {
        public int PostId { get; set; }
        public int? ParentComment { get; set; }

    }
}
