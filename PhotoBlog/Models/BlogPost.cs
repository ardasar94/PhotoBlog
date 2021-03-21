using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PhotoBlog.Models
{
    public class BlogPost
    {
        public BlogPost()
        {
            this.Tags = new HashSet<HashTag>();
        }
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Content { get; set; }
        public string PhotoPath { get; set; }
        public DateTime? CreatingTime { get; set; }
        public virtual ICollection<HashTag> Tags { get; set; }
    }
}