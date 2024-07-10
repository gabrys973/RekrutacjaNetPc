namespace Rekrutacja.Client.Services.Categories;

public interface ICategoryService
{
    Task<HttpResponseMessage> GetAllAsync();
}