using System.ComponentModel.DataAnnotations;

namespace Blog.Models
{
    public class BlogPost
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Pole jest obowiązkowe")]
        [Display(Name = "Tytuł")]
        [MaxLength(40)]
        public string Title { get; set; }
        [Required(ErrorMessage = "Pole jest obowiązkowe")]
        [Display(Name = "Treść postu")]
        [StringLength(256)]
        public string Description { get; set; }
        public string? UserName { get; set; }
        public DateTime PublishedDate { get; set; }

        public ICollection<Tag> Tags { get; set; }

        [Display(Name = "Zdjęcia")]
        public List<FileEntity>? FilePaths { get; set; }
    }


}

