using BlazorBootstrap;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Rekrutacja.Client.Models.Dtos.Categories;
using Rekrutacja.Client.Models.Dtos.Contacts;
using Rekrutacja.Client.Models.Helpers.Contacts;
using Rekrutacja.Client.Services.Categories;
using Rekrutacja.Client.Services.Contacts;
using System.Web;

namespace Rekrutacja.Client.Pages;

public partial class ContactList : ComponentBase
{
    [Inject]
    private IContactService ContactService { get; set; }

    [Inject]
    private NavigationManager NavManager { get; set; }

    [Inject]
    private ICategoryService CategoryService { get; set; }

    private ContactListDto ContactsDto { get; set; }
    private List<ToastMessage> _messages = new();
    private int TotalPages { get; set; }
    private QueryContactHelper ContactQuery { get; set; } = new();
    private List<CategoryDto> Categories { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await GetAllContactsAsync();
        await GetAllCategoriesAsync();

        base.OnInitialized();
    }

    private async Task GetAllContactsAsync()
    {
        var queryParams = ToQueryString();

        var result = await ContactService.GetAllAsync(queryParams);

        if(result.IsSuccessStatusCode)
        {
            ContactsDto = await result.Content.ReadFromJsonAsync<ContactListDto>();
        }

        // wyliczamy liczbę stron na podstawie ilości wsyzstkich rekordów oraz ilości rekordów na stronę
        TotalPages = (ContactsDto.Count + ContactsDto.PageSize - 1) / ContactsDto.PageSize;
    }

    // konwersja obiektu typu QueryContactHelper z filtrami do stringa
    private string ToQueryString()
    {
        var jsonString = JsonConvert.SerializeObject(ContactQuery);
        var jsonObject = JObject.Parse(jsonString);
        var properties = jsonObject
            .Properties()
            .Where(p => p.Value.Type != JTokenType.Null)
            .Select(p =>
                $"{HttpUtility.UrlEncode(p.Name)}={HttpUtility.UrlEncode(p.Value.ToString())}");
        return string.Join("&", properties);
    }

    private async Task GetAllCategoriesAsync()
    {
        var result = await CategoryService.GetAllAsync();

        if(result.IsSuccessStatusCode)
        {
            Categories = await result.Content.ReadFromJsonAsync<List<CategoryDto>>();
        }
    }

    private async Task ContactDeletedEvent()
    {
        _messages.Add(new ToastMessage
        {
            Type = ToastType.Success,
            Message = $"Rekord został usunięty"
        });

        await GetAllContactsAsync();
    }

    private void ContactDeletedErrorEvent()
    {
        _messages.Add(new ToastMessage
        {
            Type = ToastType.Danger,
            Message = $"Nie udało się usunąć rekordu"
        });
    }

    private async Task OnPageChangedAsync(int newPageNumber)
    {
        await Task.Run(() => { ContactQuery.PageNumber = newPageNumber; });
        await GetAllContactsAsync();
    }

    private async Task OnPageSizeChangedAsync(ChangeEventArgs e)
    {
        ContactQuery.PageSize = Int32.Parse((string)e.Value);
        await GetAllContactsAsync();
    }

    private async Task GoToAddingPage()
    {
        await Task.Run(() => { NavManager.NavigateTo("/dodaj-kontakt"); });
    }

    private async Task OnFilterAsync()
    {
        await GetAllContactsAsync();
    }

    private async Task OnClearFiltersAsync()
    {
        ContactQuery = new();
        await GetAllContactsAsync();
    }
}