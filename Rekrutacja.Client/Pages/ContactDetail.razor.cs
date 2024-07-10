using Microsoft.AspNetCore.Components;
using Rekrutacja.Client.Models.Dtos.Contacts;
using Rekrutacja.Client.Services.Contacts;

namespace Rekrutacja.Client.Pages;

public partial class ContactDetail : ComponentBase
{
    [Parameter]
    public int ContactId { get; set; }

    [Inject]
    private IContactService ContactService { get; set; }

    private ContactDto Contact;

    protected override async Task OnInitializedAsync()
    {
        var result = await ContactService.GetByIdAsync(ContactId);

        if(result.IsSuccessStatusCode)
        {
            Contact = await result.Content.ReadFromJsonAsync<ContactDto>();
        }

        base.OnInitialized();
    }
}