using CompanyExpenseTracking.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CompanyExpenseTracking.Infrastructure;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<Category> Categories { get; set; }
}

