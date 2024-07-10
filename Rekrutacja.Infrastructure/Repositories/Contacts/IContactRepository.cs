using Rekrutacja.Application.Helpers.Contacts;
using Rekrutacja.Domain.Entities;

namespace Rekrutacja.Infrastructure.Repositories.Contacts;

public interface IContactRepository : IRepository<Contact>
{
    Task<List<Contact>> GetAllContacts(QueryContactHelper query);

    Task<Contact?> GetByIdContact(int id);

    Task<Contact> CreateAsync(Contact contact);

    Task<Contact?> EditAsync(int id, Contact contact);

    Task<Contact?> DeleteAsync(int id);

    Task<int> GetCountAsync();
}