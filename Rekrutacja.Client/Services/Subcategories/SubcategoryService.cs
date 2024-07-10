namespace Rekrutacja.Client.Services.Subcategories;

public class SubcategoryService : ISubcategoryService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public SubcategoryService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public async Task<HttpResponseMessage> GetAllAsync()
    {
        return await _httpClient.GetAsync(_configuration["apiUrl"] + "/api/categories/1/subcategories");
    }
}