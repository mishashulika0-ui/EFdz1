using EFdz1.Entities;  
using Microsoft.EntityFrameworkCore;

namespace EFdz1.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=EFdz1DB;Trusted_Connection=True;");
        }
    }
}