using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Blog.Data;
using Blog.Models;

namespace Blog.Pages.Post
{
    public class IndexModel : PageModel
    {
        private readonly Blog.Data.BlogDbContext _context;

        public IndexModel(Blog.Data.BlogDbContext context)
        {
            _context = context;
        }

        public IList<BlogPost> BlogPost { get;set; } = default!;
        public IList <PostTag> postTags { get;set; } = new List<PostTag>();
        public async Task OnGetAsync()
        {
            BlogPost = await _context.BlogPosts.Include(bp => bp.Tags).Include(bp => bp.FilePaths).ToListAsync();

            if (_context.BlogPosts != null)
            {
                BlogPost = await _context.BlogPosts.ToListAsync();
            }
        }
    }
}
