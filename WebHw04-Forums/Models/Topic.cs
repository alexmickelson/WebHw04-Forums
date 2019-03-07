using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebHw04_Forums.Models
{
    public class Topic
    {
        [Key]
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Post> Posts { get; set; }
    }
}
