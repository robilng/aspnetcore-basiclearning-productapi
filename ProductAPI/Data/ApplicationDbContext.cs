using Microsoft.EntityFrameworkCore;
using ProductAPI.Models;


namespace ProductAPI.Data
{
    public class ApplicationDbContext : DbContext //Schema der Datenbank
    {
        public DbSet<Product> Products { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
    }
}
