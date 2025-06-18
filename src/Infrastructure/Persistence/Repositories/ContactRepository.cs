using Application.RepositoryContracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
//using Core.Errors;

namespace Infrastructure.Persistence.Repositories;

public class ContactRepository(AppDbContext appDbContext) : IContactRepository
{
    public async Task<int> ContactTotalCount()
    {
        var totalCount = await appDbContext.Contacts.CountAsync();
        return totalCount;
    }

    public async Task DeleteContactAsync(Contact contact)
    {
        appDbContext.Contacts.Remove(contact);
        await appDbContext.SaveChangesAsync();
    }

    public async Task<long> InsertContactAsync(Contact contact)
    {
        await appDbContext.Contacts.AddAsync(contact);
        await appDbContext.SaveChangesAsync();
        return contact.ContactId;
    }

    public IQueryable<Contact> SelectAllContacts()
    {
        return appDbContext.Contacts.Include(c => c.User);
    }

    public async Task<ICollection<Contact>> SelectAllUserContactsAsync(long userId)
    {
        var contacts = await appDbContext.Contacts.Where(c => c.UserId == userId).ToListAsync();
        return contacts;
    }

    public async Task<Contact> SelectContactByContactIdAsync(long contactId)
    {
        var contact = await appDbContext.Contacts.Include(c => c.User).FirstOrDefaultAsync(c => c.ContactId == contactId);
        return contact == null ? throw new Exception($"Contact wiht contactId {contactId} not found") : contact;
    }

    public async Task UpdateContactAsync(Contact contact)
    {
        var contactFronDb = await SelectContactByContactIdAsync(contact.ContactId);
        appDbContext.Contacts.Update(contactFronDb);
        await appDbContext.SaveChangesAsync();
    }
}
