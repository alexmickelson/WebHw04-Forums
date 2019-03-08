using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebHw04_Forums.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public DateTime Time { get; set; }
        public string Content { get; set; }
        public string Title { get; set; }

        public List<Comment> ChildComments { get; set; }

        [ForeignKey(nameof(Topic))]
        public string TopicId { get; set; }
    }
}
