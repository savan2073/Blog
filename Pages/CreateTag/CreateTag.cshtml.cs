using Blog.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Blog.Models;

namespace Blog.Pages.CreateTag
{
    public class CreateTagModel : PageModel
    {
        private readonly BlogDbContext _context;
        private readonly ILogger<CreateTagModel> _logger;

        public CreateTagModel(ILogger<CreateTagModel> logger, BlogDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        [BindProperty]
        public Tag Tag { get; set; }
        public string SuccessMessage { get; set; }
        public string AlertMessage { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Tag.Name == null)
            {
                AlertMessage = "Pola nie zostały uzupełnione";
                return Page();
            }
            
            _context.Tags.Add(Tag);
            _context.SaveChanges();

            SuccessMessage = "Udało się utworzyć tag";

            return Page();
        }
    }
}
