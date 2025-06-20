using Domain.Entities;
using System.Linq.Expressions;

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

    Task<int> CountAsync(Expression<Func<Contact, bool>>? predicate = null, CancellationToken cancellationToken = default);
}
