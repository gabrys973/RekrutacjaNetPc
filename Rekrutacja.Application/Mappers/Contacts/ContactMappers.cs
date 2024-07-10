using Rekrutacja.Application.Dtos.Contacts;
using Rekrutacja.Application.Mappers.Categories;
using Rekrutacja.Application.Mappers.Subcategories;
using Rekrutacja.Application.Requests.Contacts;
using Rekrutacja.Domain.Entities;

namespace Rekrutacja.Application.Mappers.Contacts;

public static class ContactMappers
{
    public static ContactDto ToContactDto(this Contact contact)
    {
        return new ContactDto(contact.Id, contact.Name, contact.Surname, contact.Email, contact.PhoneNumber, contact.DateOfBirth, contact.Category.ToCategoryDto(), contact.Subcategory?.ToSubcategoryDto(), contact.CustomSubcategory);
    }

    public static Contact ToContactFromRequests(this ContactRequest contact)
    {
        return new Contact
        {
            Name = contact.Name,
            Surname = contact.Surname,
            Email = contact.Email,
            Password = contact.Password,
            PhoneNumber = contact.PhoneNumber.Trim(),
            DateOfBirth = contact.DateOfBirth.Date,
            CategoryId = contact.CategoryId,
            SubcategoryId = contact.SubcategoryId,
            CustomSubcategory = contact.CustomSubcategory
        };
    }
}