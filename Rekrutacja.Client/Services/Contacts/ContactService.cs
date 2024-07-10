using IdentityModel.Client;
using Newtonsoft.Json;
using Rekrutacja.Client.Models.Requests.Contacts;
using Rekrutacja.Client.Services.Identity;
using System.Text;

namespace Rekrutacja.Client.Services.Contacts;

public class ContactService : IContactService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly ITokenService _tokenService;

    public ContactService(HttpClient httpClient, IConfiguration configuration, ITokenService tokenService)
    {
        _httpClient = httpClient;
        _configuration = configuration;
        _tokenService = tokenService;
    }

    public async Task<HttpResponseMessage> GetAllAsync(string queryParams)
    {
        return await _httpClient.GetAsync(_configuration["apiUrl"] + "/api/contacts?" + queryParams);
    }

    public async Task<HttpResponseMessage> GetByIdAsync(int id)
    {
        return await _httpClient.GetAsync(_configuration["apiUrl"] + $"/api/contacts/{id}");
    }

    public async Task<HttpResponseMessage> AddAsync(ContactRequestModel request)
    {
        // pobranie oraz zamieszczenie tokena w zapytaniu http
        var tokenResponse = await _tokenService.GetToken("RekrutacjaAPI.read");
        _httpClient.SetBearerToken(tokenResponse.AccessToken);

        var requestJson = JsonConvert.SerializeObject(request);

        StringContent jsonContent = new(
        requestJson,
        Encoding.UTF8,
        "application/json");

        return await _httpClient.PostAsync(_configuration["apiUrl"] + "/api/contacts", jsonContent);
    }

    public async Task<HttpResponseMessage> EditAsync(int id, ContactRequestModel request)
    {
        // pobranie oraz zamieszczenie tokena w zapytaniu http
        var tokenResponse = await _tokenService.GetToken("RekrutacjaAPI.read");
        _httpClient.SetBearerToken(tokenResponse.AccessToken);

        var requestJson = JsonConvert.SerializeObject(request);

        StringContent jsonContent = new(
        requestJson,
        Encoding.UTF8,
        "application/json");

        return await _httpClient.PutAsync(_configuration["apiUrl"] + $"/api/contacts/{id}", jsonContent);
    }

    public async Task<HttpResponseMessage> DeleteAsync(int id)
    {
        return await _httpClient.DeleteAsync(_configuration["apiUrl"] + $"/api/contacts/{id}");
    }
}