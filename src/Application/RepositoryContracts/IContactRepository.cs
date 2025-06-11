using Domain.Entities;

namespace Application.RepositoryContracts;

public interface IContactRepository
{
    Task<ICollection<Contact>> GetAllContacts();
    Task<Role> GetContactById(long contactId);
    Task<long> AddContact(Contact contact);
    Task UpdateContact(Contact contact);
    Task DeleteContact(Contact contact);
}
