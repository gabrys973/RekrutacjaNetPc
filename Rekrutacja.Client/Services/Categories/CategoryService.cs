namespace Rekrutacja.Client.Services.Categories;

public class CategoryService : ICategoryService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public CategoryService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public async Task<HttpResponseMessage> GetAllAsync()
    {
        return await _httpClient.GetAsync(_configuration["apiUrl"] + "/api/categories");
    }
}