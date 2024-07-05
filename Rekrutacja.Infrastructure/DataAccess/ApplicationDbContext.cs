using Microsoft.EntityFrameworkCore;
using Rekrutacja.Domain.Entities;
using Rekrutacja.Infrastructure.Configuration;

namespace Rekrutacja.Infrastructure.DataAccess;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    { }

    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Contact> Contact { get; set; }
    public DbSet<Category> Category { get; set; }
    public DbSet<Subcategory> Subcategory { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CategoryTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ContactTypeConfiguration());
        modelBuilder.ApplyConfiguration(new SubcategoryTypeConfiguration());
    }
}