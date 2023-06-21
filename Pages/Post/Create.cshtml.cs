using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Blog.Data;
using Blog.Models;
using Microsoft.Extensions.Hosting;

namespace Blog.Pages.Post
{
    public class CreateModel : PageModel
    {
        private readonly Blog.Data.BlogDbContext _context;
        private readonly IHostEnvironment _environment;
        public CreateModel(IHostEnvironment environment, Blog.Data.BlogDbContext context)
        {
            _environment = environment;
            _context = context;
        }

        [BindProperty]
        public BlogPost BlogPost { get; set; } = new BlogPost(); // Initialize Tags collection

        public List<SelectListItem> TagList { get; set; }

        public IActionResult OnGet()
        {
            TagList = _context.Tags
                .Select(t => new SelectListItem { Value = t.Id.ToString(), Text = t.Name })
                .ToList();
            return Page();
        }

        [BindProperty]
        public List<IFormFile> FormFiles { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            TagList = _context.Tags
                .Select(t => new SelectListItem { Value = t.Id.ToString(), Text = t.Name })
                .ToList();

            if (!ModelState.IsValid || _context.BlogPosts == null || BlogPost == null)
            {
                return Page();
            }

            BlogPost.Tags = new List<Tag>();

            var selectedTags = Request.Form["BlogPost.Tags"];
            if (selectedTags.Count > 0)
            {
                foreach (var tagId in selectedTags)
                {
                    var tag = await _context.Tags.FindAsync(int.Parse(tagId));
                    if (tag != null)
                    {
                        BlogPost.Tags.Add(tag);
                    }
                }
            }

            BlogPost.UserName = User.Identity.Name;
            BlogPost.PublishedDate = DateTime.Now;


            var files = new List<FileEntity>();

            if (FormFiles.Count == 0)
            {
                //AlertMessage = "Nie wybrano/odnaleziono pliku.";
                return Page();
            }
            foreach (var aformFile in FormFiles)
            {
                var fileEntity = new FileEntity();

                var formFile = aformFile;

                var fileSize = formFile.Length;
                var checkType = formFile.ContentType;
                var fileCounter = 1;

                /*
                if (!(checkType.Contains("image")))
                {
                    AlertMessage = "Wybrano niepoprawny format pliku. Obsługiwane formaty to gif/jpeg/png/webp.";
                    return Page();
                }
                else if (fileSize > 1048576)
                {
                    AlertMessage = "Plik nie może przekraczać 10mb";
                    return Page();
                }*/

                string targetFileName = $"{_environment.ContentRootPath}/wwwroot/{fileCounter}{formFile.FileName}";

                while (System.IO.File.Exists(targetFileName))
                {
                    fileCounter++;
                    targetFileName = $"{_environment.ContentRootPath}/wwwroot/{fileCounter}{formFile.FileName}";
                }

                using (var stream = new FileStream(targetFileName, FileMode.Create))
                {
                    await formFile.CopyToAsync(stream);
                }

                fileEntity.FilePath = $"{fileCounter}{formFile.FileName}";
                files.Add(fileEntity);
               // _context.FileEntities.Add(fileEntity);
            }
            BlogPost.FilePaths = files;

            _context.BlogPosts.Add(BlogPost);
            _context.SaveChanges();

            return RedirectToPage("./Index");
            }

        }
    }
