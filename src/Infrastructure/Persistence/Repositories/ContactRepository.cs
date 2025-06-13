using Application.RepositoryContracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class ContactRepository : IContactRepository
{
    private readonly AppDbContext appDbContext;

    public ContactRepository(AppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    public async Task<long> AddContactAsync(Contact contact)
    {
        await appDbContext.Contacts.AddAsync(contact);
        await appDbContext.SaveChangesAsync();
        return contact.Id;
    }

    public async Task DeleteContactAsync(Contact contact)
    {
        if (contact is null)
        {
            throw new Exception("not found user");
        }

        appDbContext.Contacts.Remove(contact);
        await appDbContext.SaveChangesAsync();
    }

    public async Task<ICollection<Contact>> GetAllContactsAsync()
    {
        return await appDbContext.Contacts.ToListAsync();
    }

    public async Task<Contact> GetContactByIdAsync(long contactId)
    {
        var result = await appDbContext.Contacts.FirstOrDefaultAsync(b => b.Id == contactId);
        if (result is null)
        {
            throw new Exception("not found contactId");
        }
        return result;
    }

    public async Task UpdateContactAsync(Contact contact)
    {
        appDbContext.Contacts.Update(contact);
        await appDbContext.SaveChangesAsync();
    }
}
