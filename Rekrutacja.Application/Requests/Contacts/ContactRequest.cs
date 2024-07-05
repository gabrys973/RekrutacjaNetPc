namespace Rekrutacja.Application.Requests.Contacts;

public record ContactRequest(string Name, string Surname, string Email, string Password, string PhoneNumber, DateTime DateOfBirth, int? CategoryId, int? SubcategoryId, string? CustomSubcategory);