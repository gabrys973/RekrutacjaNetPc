using BlazorBootstrap;
using Microsoft.AspNetCore.Components;
using Rekrutacja.Client.Models.Dtos.Contacts;
using Rekrutacja.Client.Services.Contacts;

namespace Rekrutacja.Client.Pages;

public partial class ContactListDetail : ComponentBase
{
    [Parameter]
    public ContactDto Contact { get; set; }

    [Parameter]
    public EventCallback<int> OnContactDeleted { get; set; }

    [Parameter]
    public EventCallback<int> OnContactDeletedError { get; set; }

    [Inject]
    private IContactService ContactService { get; set; }
    [Inject]
    private NavigationManager NavManager { get; set; }
    private Modal detailModal = default!;
    private Modal deleteModal = default!;

    private async Task ShowContactDetailsModal(int id)
    {
        var parameters = new Dictionary<string, object>
        {
            { "ContactId", id }
        };

        await detailModal.ShowAsync<ContactDetail>(title: "Szczegóły kontaktu", parameters: parameters);
    }

    private async Task CloseContactDetailsModal()
    {
        await detailModal.HideAsync();
    }

    private async Task ShowDeletingModal()
    {
        await deleteModal.ShowAsync();
    }

    private async Task ConfirmDeletingModal()
    {
        var result = await DeleteContactAsync();
        await deleteModal.HideAsync();

        if(result is true)
            await OnContactDeleted.InvokeAsync(Contact.Id);
        else
            await OnContactDeletedError.InvokeAsync(Contact.Id);
    }

    private async Task CloseDeletingModal()
    {
        await deleteModal.HideAsync();
    }

    private async Task<bool> DeleteContactAsync()
    {
        var result = await ContactService.DeleteAsync(Contact.Id);

        if(result.IsSuccessStatusCode)
        {
            return true;
        }

        return false;
    }

    private async Task GoToEditingPage()
    {
        await Task.Run(() => { NavManager.NavigateTo($"/edytuj-kontakt/{Contact.Id}"); });
    }
}