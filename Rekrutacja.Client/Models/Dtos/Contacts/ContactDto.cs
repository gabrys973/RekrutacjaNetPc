using Rekrutacja.Client.Models.Dtos.Categories;
using Rekrutacja.Client.Models.Dtos.Subcategories;

namespace Rekrutacja.Client.Models.Dtos.Contacts;

public record ContactDto(int Id, string Name, string Surname, string Email, string PhoneNumber, DateTime DateOfBirth, CategoryDto Category, SubcategoryDto? Subcategory, string? CustomSubcategory);