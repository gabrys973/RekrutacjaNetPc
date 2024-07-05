using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rekrutacja.Domain.Entities;

namespace Rekrutacja.Infrastructure.Configuration;

public class CategoryTypeConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasData(new Category[] {
                new Category{Id=1, Name="Służbowy"},
                new Category{Id=2, Name="Prywatny"},
                new Category{Id=3, Name="Inny"},
            });
    }
}