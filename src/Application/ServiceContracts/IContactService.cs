using Application.Dtos;
using Application.Dtos.Pagenation;

namespace Application.ServiceContracts;

public interface IContactService
{
    Task<long> AddContactAsync(ContactCreateDto contactCreateDto, long userId);
    Task<ICollection<ContactDto>> GetAllContactstAsync(long userId);
    Task DeleteContactAsync(long contactId, long userId);
    Task UpdateContactAsync(ContactDto contactDto, long userId);
    Task<ContactDto> GetContactByContacIdAsync(long contactId, long userId);

    Task<PagedResult<ContactDto>> GetAllAsync(ContactQueryParams queryParams, CancellationToken cancellationToken = default);
    Task<List<ContactDto>> FilterContactsAsync(long userId, string? name);
}
