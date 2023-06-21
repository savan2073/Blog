﻿using System;
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
    public class DeleteModel : PageModel
    {
        private readonly Blog.Data.BlogDbContext _context;

        public DeleteModel(Blog.Data.BlogDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public BlogPost BlogPost { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.BlogPosts == null)
            {
                return NotFound();
            }

            var blogpost = await _context.BlogPosts.FirstOrDefaultAsync(m => m.Id == id);

            if (blogpost == null)
            {
                return NotFound();
            }
            else 
            {
                BlogPost = blogpost;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.BlogPosts == null)
            {
                return NotFound();
            }
            var blogpost = await _context.BlogPosts.FindAsync(id);

            if (blogpost != null)
            {
                BlogPost = blogpost;
                _context.BlogPosts.Remove(BlogPost);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
