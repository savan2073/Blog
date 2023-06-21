using Microsoft.AspNetCore.Identity;

namespace Blog.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string CommentText { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
        public int PostId { get; set; }
        public BlogPost Post { get; set; }
        public string UserIP { get; set; }
    }
}
