using Microsoft.EntityFrameworkCore;
using CompanyExpenseTracking.Domain.Entities;

namespace CompanyExpenseTracking.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Username = "admin", Email = "admin@company.com", PasswordHash = "hashedpassword", Role = "Admin", IBAN = null },
                new User { Id = 2, Username = "personel", Email = "personel@company.com", PasswordHash = "hashedpassword", Role = "Personel", IBAN = "TR000000000000000000000000" }
            );

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Yemek" },
                new Category { Id = 2, Name = "Ulaşım" },
                new Category { Id = 3, Name = "Konaklama" }
            );
        }
    }
}