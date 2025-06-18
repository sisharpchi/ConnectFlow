using Domain.Entities;

namespace Application.RepositoryContracts;

public interface IContactRepository
{
    Task<long> InsertContactAsync(Contact contact);
    Task DeleteContactAsync(Contact contact);
    Task UpdateContactAsync(Contact contact);
    Task<ICollection<Contact>> SelectAllUserContactsAsync(long userId);
    Task<Contact> SelectContactByContactIdAsync(long contactId);
    Task<int> ContactTotalCount();
    IQueryable<Contact> SelectAllContacts();
}
