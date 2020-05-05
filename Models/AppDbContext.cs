using Microsoft.EntityFrameworkCore;

namespace TaskManager.Models
{
  public class AppDbContext : DbContext
  {
      public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

      public DbSet<Task> Tasks {get; set;}
  }
}