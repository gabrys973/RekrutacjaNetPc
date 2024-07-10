using Microsoft.EntityFrameworkCore;
using Rekrutacja.Application.Helpers.Contacts;
using Rekrutacja.Domain.Entities;
using Rekrutacja.Infrastructure.DataAccess;

namespace Rekrutacja.Infrastructure.Repositories.Contacts;

public class ContactRepository : Repository<Contact>, IContactRepository
{
    public ContactRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<List<Contact>> GetAllContacts(QueryContactHelper query)
    {
        var contacts = GetAll().Include(c => c.Category).Include(s => s.Subcategory).AsQueryable();

        // filtry po liście kontaktów
        if(!string.IsNullOrWhiteSpace(query.Name))
        {
            contacts = contacts.Where(x => x.Name.Contains(query.Name));
        }

        if(!string.IsNullOrWhiteSpace(query.Surname))
        {
            contacts = contacts.Where(x => x.Surname.Contains(query.Surname));
        }

        if(!string.IsNullOrWhiteSpace(query.Email))
        {
            contacts = contacts.Where(x => x.Email.Contains(query.Email));
        }

        if(!string.IsNullOrWhiteSpace(query.PhoneNumber))
        {
            contacts = contacts.Where(x => x.PhoneNumber.Contains(query.PhoneNumber));
        }

        if(query.CategoryId is not null)
        {
            contacts = contacts.Where(x => x.CategoryId == query.CategoryId);
        }

        if(query.SubcategoryId is not null)
        {
            contacts = contacts.Where(x => x.SubcategoryId == query.SubcategoryId);
        }

        if(!string.IsNullOrWhiteSpace(query.CustomSubcategory))
        {
            contacts = contacts.Where(x => x.CustomSubcategory != null && x.CustomSubcategory.Contains(query.CustomSubcategory));
        }

        //pageowanie listy kontaktów
        var skipNumber = (query.PageNumber - 1) * query.PageSize;

        return await contacts.Skip(skipNumber).Take(query.PageSize).ToListAsync();
    }

    public async Task<Contact?> GetByIdContact(int id)
    {
        return await GetAll().Include(c => c.Category).Include(s => s.Subcategory).FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Contact> CreateAsync(Contact contact)
    {
        await Entities.AddAsync(contact);
        await _context.SaveChangesAsync();

        return contact;
    }

    public async Task<Contact?> EditAsync(int id, Contact contact)
    {
        var existingContact = await Entities.FirstOrDefaultAsync(x => x.Id == id);

        if(existingContact is null)
        {
            return null;
        }

        existingContact = contact;

        await _context.SaveChangesAsync();

        return existingContact;
    }

    public async Task<Contact?> DeleteAsync(int id)
    {
        var contact = await Entities.FirstOrDefaultAsync(x => x.Id == id);

        if(contact is null)
        {
            return null;
        }

        Entities.Remove(contact);
        await _context.SaveChangesAsync();

        return contact;
    }

    public async Task<int> GetCountAsync()
    {
        return await GetAll().CountAsync();
    }
}