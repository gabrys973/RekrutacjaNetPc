namespace Rekrutacja.Client.Services.Subcategories;

public interface ISubcategoryService
{
    Task<HttpResponseMessage> GetAllAsync();
}