using Microsoft.EntityFrameworkCore;
using SharpChat.Models;

namespace SharpChat.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Password> Passwords { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<User>()
            .HasOne(u => u.Password)
            .WithOne(p => p.User)
            .HasForeignKey<Password>(p => p.OfUserId);

        modelBuilder
            .Entity<Password>()
            .HasOne(p => p.User)
            .WithOne(u => u.Password);
    }
}
