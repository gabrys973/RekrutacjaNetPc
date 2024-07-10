using Rekrutacja.Client.Models.Requests.Contacts;

namespace Rekrutacja.Client.Services.Contacts;

public interface IContactService
{
    Task<HttpResponseMessage> GetAllAsync(string queryParams);

    Task<HttpResponseMessage> GetByIdAsync(int id);

    Task<HttpResponseMessage> AddAsync(ContactRequestModel request);

    Task<HttpResponseMessage> EditAsync(int id, ContactRequestModel request);

    Task<HttpResponseMessage> DeleteAsync(int id);
}