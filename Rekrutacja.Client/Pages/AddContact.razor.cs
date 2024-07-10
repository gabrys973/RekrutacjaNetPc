using BlazorBootstrap;
using Microsoft.AspNetCore.Components;
using Rekrutacja.Client.Models.Dtos.Categories;
using Rekrutacja.Client.Models.Dtos.Subcategories;
using Rekrutacja.Client.Models.Requests.Contacts;
using Rekrutacja.Client.Services.Categories;
using Rekrutacja.Client.Services.Contacts;
using Rekrutacja.Client.Services.Subcategories;

namespace Rekrutacja.Client.Pages;

public partial class AddContact : ComponentBase
{
    [Inject]
    private IContactService ContactService { get; set; }

    [Inject]
    private ICategoryService CategoryService { get; set; }

    [Inject]
    private ISubcategoryService SubcategoryService { get; set; }

    [Inject]
    private NavigationManager NavManager { get; set; }

    private readonly List<ToastMessage> _messages = new();
    private ContactRequestModel ContactRequest { get; set; } = new();
    private List<CategoryDto> Categories { get; set; }
    private List<SubcategoryDto> Subcategories { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await GetAllCategoriesAsync();
        await GetAllSubcategoriesAsync();

        base.OnInitialized();
    }

    private async Task GetAllCategoriesAsync()
    {
        var result = await CategoryService.GetAllAsync();

        if(result.IsSuccessStatusCode)
        {
            Categories = await result.Content.ReadFromJsonAsync<List<CategoryDto>>();
        }
    }

    private async Task GetAllSubcategoriesAsync()
    {
        var result = await SubcategoryService.GetAllAsync();

        if(result.IsSuccessStatusCode)
        {
            Subcategories = await result.Content.ReadFromJsonAsync<List<SubcategoryDto>>();
        }
    }

    private async Task AddContactAsync()
    {
        var result = await ContactService.AddAsync(ContactRequest);

        if(result.IsSuccessStatusCode)
        {
            _messages.Add(new ToastMessage
            {
                Type = ToastType.Success,
                Message = $"Nowy rekord został dodany pomyślnie"
            });

            await Task.Delay(5000);
            await Task.Run(() => { NavManager.NavigateTo(""); });
        }
        else
        {
            _messages.Add(new ToastMessage
            {
                Type = ToastType.Danger,
                Message = $"Nie udało się dodać nowego rekordu"
            });
        }
    }
}