using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;

namespace Blog.Models
{
    public class PostTag
    {
        [Key]
        public int Id { get; set; }
        public int PostsId { get; set; }
        public int TagsId { get; set; }
        public BlogPost Post { get; set; } = null!;
        public Tag Tag { get; set; } = null!;
    }
}
