using Application.RepositoryContracts;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories;

public class ContactRepository : IContactRepository
{
    public Task<long> AddContact(Contact contact)
    {
        throw new NotImplementedException();
    }

    public Task DeleteContact(Contact contact)
    {
        throw new NotImplementedException();
    }

    public Task<ICollection<Contact>> GetAllContacts()
    {
        throw new NotImplementedException();
    }

    public Task<Role> GetContactById(long contactId)
    {
        throw new NotImplementedException();
    }

    public Task UpdateContact(Contact contact)
    {
        throw new NotImplementedException();
    }
}
