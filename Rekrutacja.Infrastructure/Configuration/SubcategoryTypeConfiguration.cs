using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rekrutacja.Domain.Entities;

namespace Rekrutacja.Infrastructure.Configuration;

public class SubcategoryTypeConfiguration : IEntityTypeConfiguration<Subcategory>
{
    public void Configure(EntityTypeBuilder<Subcategory> builder)
    {
        builder.HasData(new Subcategory[] {
                new Subcategory{Id=1, Name="Szef", CategoryId = 1},
                new Subcategory{Id=2, Name="Klient", CategoryId = 1},
                new Subcategory{Id=3, Name="Pracownik", CategoryId = 1},
            });
    }
}