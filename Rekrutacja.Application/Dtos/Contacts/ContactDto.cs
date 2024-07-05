using Rekrutacja.Application.Dtos.Categories;
using Rekrutacja.Application.Dtos.Subcategories;

namespace Rekrutacja.Application.Dtos.Contacts;

public record ContactDto(int Id, string Name, string Surname, string Email, string PhoneNumber, DateTime DateOfBirth, CategoryDto? Category, SubcategoryDto? Subcategory, string? CustomSubcategory);