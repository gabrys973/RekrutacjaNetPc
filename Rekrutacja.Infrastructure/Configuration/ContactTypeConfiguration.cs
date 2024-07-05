using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rekrutacja.Domain.Entities;

namespace Rekrutacja.Infrastructure.Configuration;

public class ContactTypeConfiguration : IEntityTypeConfiguration<Contact>
{
    public void Configure(EntityTypeBuilder<Contact> builder)
    {
        builder.HasIndex(p => p.Email).IsUnique(true);

        builder.HasData(new Contact[] {
                new Contact{Id = 1, Name = "Adam", Surname = "Kowalski", Email = "adamKowal@gmail.com", Password = "ypVYDhynIwMsT8lVkV1tyg==;BHHVobjaCMOQ99zejjnt8NyefwVq7F4wNd4MU/uaTBE=",
                    PhoneNumber = "+48123321123", DateOfBirth = new DateTime(1997, 10, 21), CategoryId = 1, SubcategoryId = 1},
                new Contact{Id = 2, Name = "Gabriel", Surname = "Nowak", Email = "gabnowa@gmail.com", Password = "L+0a39rxi2Jo/3DpmVa3GQ==;X7hX4aBVhc+7sioY9wXk2ypssIjvhltlQuTaupJSbPs=",
                    PhoneNumber = "485216948", DateOfBirth = new DateTime(1980, 3, 13), CategoryId = 3, CustomSubcategory = "domowy"},
                new Contact{Id = 3, Name = "Żaneta", Surname = "Pewna", Email = "zancia123@gmail.com", Password = "2FizU1vwmTqce9CzMPBM9w==;HiorraGCAfyQfF15LvIFVsIerFqw0kjVk8L4IdD5gWg=",
                    PhoneNumber = "871594862", DateOfBirth = new DateTime(2000, 12, 12), CategoryId = 2}
            });
    }
}