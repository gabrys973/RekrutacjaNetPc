namespace Rekrutacja.Client.Models.Helpers.Contacts;

public class QueryContactHelper
{
    public string? Name { get; set; } = null;
    public string? Surname { get; set; } = null;
    public string? Email { get; set; } = null;
    public string? PhoneNumber { get; set; } = null;
    public int? CategoryId { get; set; } = null;
    public int? SubcategoryId { get; set; } = null;
    public string? CustomSubcategory { get; set; } = null;

    // pageowanie domyślnie ustawione na 1 stronę i 20 rekordów
    public int PageNumber { get; set; } = 1;

    public int PageSize { get; set; } = 10;
}