using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookApp.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookApp.Pages.BookList
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDBContext _db;

        public EditModel(ApplicationDBContext db)
        {
            _db = db;  
        }
        [BindProperty]
        public Book Book { get; set; }
        public async Task OnGet(int Id)
        {
          Book= await _db.Book.FindAsync(Id);
        }
        public async Task<IActionResult> OnPost()
        {
            if(ModelState.IsValid)
            {
              var BookFrmDb = await _db.Book.FindAsync(Book.Id);
                BookFrmDb.Name = Book.Name;
                BookFrmDb.Author = Book.Author;
                BookFrmDb.ISBN = Book.ISBN;
                await _db.SaveChangesAsync();
                return RedirectToPage("Index");

            }
            else
            {
                return Page();
            }
        }
    }
}
