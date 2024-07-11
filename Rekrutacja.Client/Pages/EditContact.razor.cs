using BlazorBootstrap;
using Microsoft.AspNetCore.Components;
using Rekrutacja.Client.Models.Dtos.Categories;
using Rekrutacja.Client.Models.Dtos.Contacts;
using Rekrutacja.Client.Models.Dtos.Subcategories;
using Rekrutacja.Client.Models.Requests.Contacts;
using Rekrutacja.Client.Services.Categories;
using Rekrutacja.Client.Services.Contacts;
using Rekrutacja.Client.Services.Subcategories;

namespace Rekrutacja.Client.Pages;

public partial class EditContact : ComponentBase
{
    [Parameter]
    public int ContactId { get; set; }

    [Inject]
    private IContactService ContactService { get; set; }

    [Inject]
    private ICategoryService CategoryService { get; set; }

    [Inject]
    private ISubcategoryService SubcategoryService { get; set; }

    [Inject]
    private NavigationManager NavManager { get; set; }

    private ContactDto Contact;
    private List<ToastMessage> _messages = new();
    private ContactRequestModel ContactRequest { get; set; }
    private List<CategoryDto> Categories { get; set; }
    private List<SubcategoryDto> Subcategories { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var result = await ContactService.GetByIdAsync(ContactId);

        if(result.IsSuccessStatusCode)
        {
            Contact = await result.Content.ReadFromJsonAsync<ContactDto>();
        }

        ContactRequest = new ContactRequestModel
        {
            Name = Contact.Name,
            Surname = Contact.Surname,
            Email = Contact.Email,
            PhoneNumber = Contact.PhoneNumber,
            DateOfBirth = Contact.DateOfBirth,
            CategoryId = Contact.Category.Id,
            SubcategoryId = Contact.Subcategory?.Id,
            CustomSubcategory = Contact.CustomSubcategory
        };

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

    private async Task UpdateContactAsync()
    {
        var result = await ContactService.EditAsync(ContactId, ContactRequest);

        if(result.IsSuccessStatusCode)
        {
            _messages.Add(new ToastMessage
            {
                Type = ToastType.Success,
                Message = $"Rekord został zaktualizowany pomyślnie"
            });

            await Task.Delay(5000);
            NavManager.NavigateTo("");
        }
        else
        {
            _messages.Add(new ToastMessage
            {
                Type = ToastType.Danger,
                Message = $"Nie udało się dodać zaktualizować rekordu"
            });
        }
    }
}