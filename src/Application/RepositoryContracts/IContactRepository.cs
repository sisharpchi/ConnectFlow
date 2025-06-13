using Domain.Entities;

namespace Application.RepositoryContracts;

public interface IContactRepository
{
    Task<ICollection<Contact>> GetAllContactsAsync();
    Task<Contact> GetContactByIdAsync(long contactId);
    Task<long> AddContactAsync(Contact contact);
    Task UpdateContactAsync(Contact contact);
    Task DeleteContactAsync(Contact contact);
}
