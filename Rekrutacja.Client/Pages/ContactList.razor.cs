using IdentityModel.Client;
using Microsoft.AspNetCore.Components;
using Rekrutacja.Application.Dtos.Contacts;
using Rekrutacja.Client.Services;

namespace Rekrutacja.Client.Pages;

public partial class ContactList : ComponentBase
{
    private List<ContactDto> Contacts = new();
    [Inject] private HttpClient HttpClient { get; set; }
    [Inject] private IConfiguration Configuration { get; set; }
    [Inject] private ITokenService TokenService { get; set; }
    private int? DeleteContactId { get; set; }

    protected override async Task OnInitializedAsync()
    {
        // pobranie oraz zamieszczenie tokenu w zapytaniu http
        var tokenResponse = await TokenService.GetToken("RekrutacjaAPI.read");
        HttpClient.SetBearerToken(tokenResponse.AccessToken);

        var result = await HttpClient.GetAsync(Configuration["apiUrl"] + "/api/contacts");

        if(result.IsSuccessStatusCode)
        {
            Contacts = await result.Content.ReadFromJsonAsync<List<ContactDto>>();
        }
    }

    private async Task<bool> DeleteAsync()
    {
        if(DeleteContactId is null)
            return false;

        var tokenResponse = await TokenService.GetToken("RekrutacjaAPI.read");
        HttpClient.SetBearerToken(tokenResponse.AccessToken);

        var result = await HttpClient.DeleteAsync(Configuration["apiUrl"] + $"/api/contacts/{DeleteContactId}");

        if(result.IsSuccessStatusCode)
        {
            return true;
        }

        return false;
    }
}