using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Blog.Data;
using Blog.Models;

namespace Blog.Pages.Post
{
    public class EditModel : PageModel
    {
        private readonly Blog.Data.BlogDbContext _context;

        public EditModel(Blog.Data.BlogDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public BlogPost BlogPost { get; set; } = default!;
        public List<SelectListItem> TagList { get; set; }
        public List<Tag> tags { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            
            if (id == null || _context.BlogPosts == null)
            {
                return NotFound();
            }

            var blogpost = await _context.BlogPosts
        .Include(b => b.Tags)
        .FirstOrDefaultAsync(m => m.Id == id);

            if (blogpost == null)
            {
                return NotFound();
            }
            BlogPost = blogpost;

            if (BlogPost.Tags == null)
            {
                BlogPost.Tags = new List<Tag>();
            }

            TagList = _context.Tags
                .Select(t => new SelectListItem { Value = t.Id.ToString(), Text = t.Name })
                .ToList();

            tags = BlogPost.Tags.ToList();


            foreach (var item in tags)
            {
                if(TagList.Where(x => x.Text == item.Name).FirstOrDefault()!=null)
                {
                    var selected = TagList.Where(y => y.Text == item.Name).FirstOrDefault();
                    selected.Selected = true;
                }
            }

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(BlogPost).State = EntityState.Modified;


            TagList = _context.Tags
                .Select(t => new SelectListItem { Value = t.Id.ToString(), Text = t.Name })
                .ToList();
            BlogPost.Tags = new List<Tag>();

            var selectedTags = Request.Form["BlogPost.Tags"];
            if (selectedTags.Count > 0)
            {
                foreach (var tagId in selectedTags)
                {
                    if(true) //(tags.FirstOrDefault(x => x.Id.ToString() == tagId) == null)
                    {
                        var tag = await _context.Tags.FindAsync(int.Parse(tagId));
                        if (tag != null)
                        {
                            BlogPost.Tags.Add(tag);
                        }
                    }
                }
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BlogPostExists(BlogPost.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool BlogPostExists(int id)
        {
          return (_context.BlogPosts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
