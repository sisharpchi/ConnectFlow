using Application.Dtos;
using Application.FluentValidation;
using Application.FluintValidation;
using Application.RepositoryContracts;
using Core.Errors;
using Domain.Entities;

namespace Application.ServiceContracts.ServiceImplementations;

public class ContactService(IContactRepository contactRepository) : IContactService
{
    private ContactDto ConvertToContactDto(Contact contact)
    {
        return new ContactDto()
        {
            ContactId = contact.ContactId,
            FirstName = contact.FirstName,
            LastName = contact.LastName,
            Email = contact.Email,
            PhoneNumber = contact.PhoneNumber,
            Address = contact.Address,
        };
    }

    public async Task<long> AddContactAsync(ContactCreateDto contactCreateDto, long userId)
    {
        var contactValidator = new ContactCreateDtoValidator();
        var result = contactValidator.Validate(contactCreateDto);

        if (!result.IsValid)
        {
            var errors = "";
            foreach (var error in result.Errors)
            {
                errors = errors + "\n" + error.ErrorMessage;
            }
            throw new Exception(errors);
        }

        var contact = new Contact()
        {
            FirstName = contactCreateDto.FirstName,
            LastName = contactCreateDto.LastName,
            FullName = contactCreateDto.FirstName + " " + contactCreateDto.LastName,
            Email = contactCreateDto.Email,
            PhoneNumber = contactCreateDto.PhoneNumber,
            Address = contactCreateDto.Address,
            CreatedAt = DateTime.UtcNow,
            UserId = userId,
        };

        var contactId = await contactRepository.InsertContactAsync(contact);
        return contactId;
    }

    public async Task DeleteContactAsync(long contactId, long userId)
    {
        var contactOfUser = contactRepository.SelectAllContacts()
            .FirstOrDefault(c => c.ContactId == contactId && c.UserId == userId);
        if (contactOfUser is null)
            throw new InvalidArgumentException($"Contact with contactId: {contactId} does not belong to user with userId: {userId}");
        await contactRepository.DeleteContactAsync(contactOfUser);
    }

    public async Task<ICollection<ContactDto>> GetAllContactstAsync(long userId)
    {
        var contacts = await contactRepository.SelectAllUserContactsAsync(userId);

        var contactsDto = contacts.Select(contact => ConvertToContactDto(contact));
        return contactsDto.ToList();
    }

    public async Task<ContactDto> GetContactByContacIdAsync(long contactId, long userId)
    {
        var contact = await contactRepository.SelectContactByContactIdAsync(contactId);
        var contactDto = ConvertToContactDto(contact);
        if (contact.User.UserId == userId)
            return contactDto;
        else
            throw new NotAllowedException($"Contact does not belong to user with userId: {userId}");
    }

    public async Task UpdateContactAsync(ContactDto contactDto, long userId)
    {
        var contactDtoValidator = new ContactDtoValidator();
        var result = contactDtoValidator.Validate(contactDto);

        if (!result.IsValid)
        {
            var errors = "";
            foreach (var error in result.Errors)
            {
                errors = errors + "\n" + error.ErrorMessage;
            }
            throw new Exception(errors);
        }

        var contactOfUser = contactRepository.SelectAllContacts()
            .FirstOrDefault(c => c.ContactId == contactDto.ContactId && c.UserId == userId);
        if (contactOfUser is null)
            throw new NotAllowedException($"Contact with contactId: {contactDto.ContactId} does not belong to user with userId: {userId}");
        
        else
        {
            contactOfUser.FirstName = contactDto.FirstName;
            contactOfUser.LastName = contactDto.LastName;
            contactOfUser.FullName = contactDto.FirstName + " " + contactDto.LastName;
            contactOfUser.Email = contactDto.Email;
            contactOfUser.PhoneNumber = contactDto.PhoneNumber;
            contactOfUser.Address = contactDto.Address;
        }

        await contactRepository.UpdateContactAsync(contactOfUser);
    }

}
