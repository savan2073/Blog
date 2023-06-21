using System.ComponentModel.DataAnnotations;

namespace Blog.Models
{
    public class Tag
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Pole jest obowiązkowe")]
        [Display(Name= "Nazwa")]
        [MaxLength(10)]
        public string Name { get; set; }

        public ICollection<BlogPost>? BlogPosts { get; set; }
    }
}
