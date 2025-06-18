using Application.Dtos;
using Application.RepositoryContracts;

namespace Application.ServiceContracts.ServiceImplementations;

public class ContactService(IContactRepository contactRepository) : IContactService
{
    public Task<long> AddContactAsync(ContactCreateDto contactCreateDto, long userId)
    {
        throw new NotImplementedException();
    }

    public Task DeleteContactAsync(long contactId, long userId)
    {
        throw new NotImplementedException();
    }

    public Task<ICollection<ContactDto>> GetAllContactstAsync(long userId)
    {
        throw new NotImplementedException();
    }

    public Task<ContactDto> GetContactByContacIdAsync(long contactId, long userId)
    {
        throw new NotImplementedException();
    }

    public Task UpdateContactAsync(ContactDto contactDto, long userId)
    {
        throw new NotImplementedException();
    }
}
