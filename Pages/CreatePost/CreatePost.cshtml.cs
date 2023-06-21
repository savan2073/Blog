using Blog.Data;
using Blog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.Pages.CreatePost
{
    public class CreatePostModel : PageModel
    {
        private readonly BlogDbContext _context;
        private readonly ILogger<CreatePostModel> _logger;

        public CreatePostModel(BlogDbContext context, ILogger<CreatePostModel> logger)
        {
            _context = context;
            _logger = logger;
        }
        [BindProperty]
        public BlogPost BlogPost { get; set; }

        public string SuccessMessage { get; set; }
        public string AlertMessage { get; set; }
        [BindProperty]
        public List<IFormFile> FormFiles { get; set; }
    
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            return Page();
        }
    }
}
