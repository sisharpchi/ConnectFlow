using Application.RepositoryContracts;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories;

public class ContactRepository(AppDbContext appDbContext) : IContactRepository
{
    public Task<int> ContactTotalCount()
    {
        throw new NotImplementedException();
    }

    public Task DeleteContactAsync(Contact contact)
    {
        throw new NotImplementedException();
    }

    public Task<long> InsertContactAsync(Contact contact)
    {
        throw new NotImplementedException();
    }

    public IQueryable<Contact> SelectAllContacts()
    {
        throw new NotImplementedException();
    }

    public Task<ICollection<Contact>> SelectAllUserContactsAsync(long userId)
    {
        throw new NotImplementedException();
    }

    public Task<Contact> SelectContactByContactIdAsync(long contactId)
    {
        throw new NotImplementedException();
    }

    public Task UpdateContactAsync(Contact contact)
    {
        throw new NotImplementedException();
    }
}
