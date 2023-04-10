using LinqSql.Models;
using Microsoft.EntityFrameworkCore;
namespace LinqSql.Data;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions options) : base(options)
    {
        Database.EnsureCreated();
        Database.EnsureCreated();
    }
    public DbSet<User> User { get; set; }
    public DbSet<Company> Companies { get; set; }
}