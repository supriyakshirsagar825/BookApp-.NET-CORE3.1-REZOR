using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookApp.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookApp.Pages.BookList
{
    public class UpsertModel : PageModel
    {
        private readonly ApplicationDBContext _db;

        public UpsertModel(ApplicationDBContext db)
        {
            _db = db;
        }
        [BindProperty]
        public Book Book { get; set; }
        public async Task<IActionResult> OnGet(int? Id)
        {
            Book = new Book();
            if (Id == null)
            {
                return Page();
            }

            Book = await _db.Book.FirstOrDefaultAsync(u => u.Id == Id);
            if(Book==null)
            {
                return NotFound();
            }
            return Page();
        }
        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                //var BookFrmDb = await _db.Book.FindAsync(Book.Id);
                //BookFrmDb.Name = Book.Name;
                //BookFrmDb.Author = Book.Author;
                //BookFrmDb.ISBN = Book.ISBN;
                if(Book.Id==0)
                {
                    _db.Book.Add(Book);
                }
                else
                {
                    _db.Book.Update(Book);
                }

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
