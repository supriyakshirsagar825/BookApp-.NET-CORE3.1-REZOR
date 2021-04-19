using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookAppWith.NetCoreMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookAppWith.NetCoreMVC.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDBContext _db;
        public BooksController(ApplicationDBContext db)
        {
            _db = db;
        }
        [BindProperty]
        public Book Book { get; set; }


        public async Task<IActionResult> Upsert(int? Id)
        {
            Book = new Book();
            if(Id==null)
            {
                //create
                return View(Book);
            }
            Book = await _db.Books.FirstOrDefaultAsync(x => x.Id == Id);
            if(Book==null)
            {
                return NotFound();
            }
            return View(Book);

        }
        [HttpPost]
        public async Task<IActionResult> Upsert()
        {
            
            if (Book.Id == 0)
            {
                //create
                _db.Books.Add(Book);
            }
            else
            {
                _db.Books.Update(Book);
            }
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");

        }
        [HttpGet]
        public async Task<IActionResult> GetALL()
        {
            return Json(new { data = await _db.Books.ToListAsync() });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var fromDB = await _db.Books.FirstOrDefaultAsync(u => u.Id == id);
            if (fromDB == null)
            {
                return Json(new { success = false, message = "Error while deleting record" });
            }
            _db.Books.Remove(fromDB);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Delete Successful" });
        }
        public IActionResult Index()
        {
            return View();
        }


    }
}
