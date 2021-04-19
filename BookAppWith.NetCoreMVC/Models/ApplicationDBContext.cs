using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookAppWith.NetCoreMVC.Models
{
    public class ApplicationDBContext: DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> dbContext):base(dbContext)
        {
                
        }

        public DbSet<Book> Books { get; set; }
    }
}
