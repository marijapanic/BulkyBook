using BulkyBook.Models;
using Microsoft.EntityFrameworkCore;

namespace BulkyBook.DataAccess
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {   
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<CoverType> CoverType { get; internal set; }
    }
}
