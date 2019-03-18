using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebHw04_Forums.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime Time { get; set; }
        public string Content { get; set; }

        
        public int? ParentId { get; set; }

        [ForeignKey(nameof(Post))]
        public int PostId { get; set; }

    }
}
