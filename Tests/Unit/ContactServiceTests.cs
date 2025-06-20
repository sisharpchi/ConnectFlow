using Application.Dtos;
using Application.RepositoryContracts;
using Application.ServiceContracts.ServiceImplementations;
using Core.Errors;
using Domain.Entities;
using Moq;
using Xunit;

namespace ContactMangerTest.ContactServiceTests;

public class ContactServiceTests
{
    private readonly Mock<IContactRepository> _mockRepo;
    private readonly ContactService _service;

    public ContactServiceTests()
    {
        _mockRepo = new Mock<IContactRepository>();
        _service = new ContactService(_mockRepo.Object);
    }


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
            CreatedAt = contact.CreatedAt,
        };
    }

    [Fact]
    public async Task DeleteContactAsync_ValidContact_CallsRepositoryDelete()
    {
        // Arrange
        var contactId = 1L;
        var userId = 10L;

        var contact = new Contact { ContactId = contactId, UserId = userId };

        _mockRepo.Setup(r => r.SelectAllContacts())
                 .Returns(new List<Contact> { contact }.AsQueryable());

        _mockRepo.Setup(r => r.DeleteContactAsync(contact))
                 .Returns(Task.CompletedTask);

        // Act
        await _service.DeleteContactAsync(contactId, userId); // ✅ Call the service method

        // Assert
        _mockRepo.Verify(r => r.DeleteContactAsync(contact), Times.Once); // ✅ Verify repo was used

    }

    [Fact]
    public async Task DeleteContactAsync_ContactNotFound_ThrowsInvalidArgumentException()
    {
        // Arrange
        var contactId = 1L;
        var userId = 10L;
        var contact = new Contact { ContactId = contactId, UserId = userId };


        _mockRepo.Setup(r => r.SelectAllContacts())
                 .Returns(new List<Contact>().AsQueryable()); // Empty list, no contact found

        // Act & Assert
        await Assert.ThrowsAsync<InvalidArgumentException>(() =>
            _service.DeleteContactAsync(contactId, userId)); // ✅ Test exception is thrown
    }

    [Fact]
    public async Task AddContactAsync_InvalidContact_ThrowsValidationFailedException()
    {
        // Arrange
        var contactCreateDto = new ContactCreateDto()
        {
            FirstName = "Hello world of tanks mandks flksjflksjflksjf fjsljfsflkjsfsjf"
        };
        var userId = 1L;

        //var task = _service.AddContactAsync(contactCreateDto, userId);

        // Act & Assert
        await Assert.ThrowsAsync<ValidateFailedException>(async () => await _service.AddContactAsync(contactCreateDto, userId));
        //await Assert.ThrowsAsync<ValidationFailedException>(async () => await task);
    }

    [Fact]
    public async Task AddContactAsync_ValidContact_CallsRepositoryInsert()
    {
        // Arrange
        var contactCreateDto = new ContactCreateDto()
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com",
            PhoneNumber = "+998941234567",
            Address = "123 Street",
        };

        var userId = 1L;
        var expectedContactId = 42L;

        var contact = new Contact()
        {
            ContactId = expectedContactId,
            UserId = userId,
        };

        _mockRepo.Setup(r => r.InsertContactAsync(It.IsAny<Contact>())).ReturnsAsync(expectedContactId);

        // Act
        var result = await _service.AddContactAsync(contactCreateDto, userId);

        // Assert
        Assert.Equal(expectedContactId, result);

        _mockRepo.Verify(r => r.InsertContactAsync(It.Is<Contact>(c =>
                        c.FirstName == contactCreateDto.FirstName &&
                        c.LastName == contactCreateDto.LastName &&
                        c.Email == contactCreateDto.Email &&
                        c.UserId == userId
                        )), Times.Once);
    }

    [Fact]
    public async Task GetAllContactstAsync_ReturnsCollectionOfContacts()
    {
        // Arrange
        long userId = 1L;

        var contacts = new List<Contact>
        {
            new() { FirstName = "John", LastName = "Doe", Email = "john@example.com" },
            new() { FirstName = "Jane", LastName = "Smith", Email = "jane@example.com" }
        };

        var expectedDtos = contacts.Select(c => ConvertToContactDto(c)).ToList();

        _mockRepo.Setup(r => r.SelectAllUserContactsAsync(userId))
             .ReturnsAsync(contacts);

        // Act
        var result = await _service.GetAllContactstAsync(userId);

        // Assert
        Assert.Equal(expectedDtos.Count, result.Count);
        Assert.Equal(expectedDtos[0].Email, result.First().Email); // just a sample field
    }

    [Fact]
    public async Task GetContactByContactIdAsync_UserOwnsContact_ReturnsContactDto()
    {
        // Arrange
        long contactId = 2;
        long userId = 4;

        User user = new User()
        {
            UserId = userId
        };

        Contact contact = new Contact()
        {
            ContactId = contactId,
            FirstName = "John",
            LastName = "Doe",
            Email = "john@example.com",
            Address = "Chilla 20",
            PhoneNumber = "+998959439543",
            User = user,
            UserId = userId,
        };

        var expectedDto = ConvertToContactDto(contact);

        _mockRepo.Setup(r => r.SelectContactByContactIdAsync(contactId)).
            ReturnsAsync(contact);

        // Act
        var result = await _service.GetContactByContacIdAsync(contactId, userId);

        // Assert
        Assert.Equal(expectedDto.FirstName, result.FirstName);
        Assert.Equal(expectedDto.LastName, result.LastName);
        Assert.Equal(expectedDto.Email, result.Email);
        //Assert.Equal(expectedDto, result);
    }
}
