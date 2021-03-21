using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PhotoBlog.Models
{
    public class HashTag
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public virtual ICollection<BlogPost> Posts { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}