using Microsoft.EntityFrameworkCore;
using EFdz1.Entities;  

namespace EFdz1.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=EFdz1DB;Trusted_Connection=True;");
        }
    }
}