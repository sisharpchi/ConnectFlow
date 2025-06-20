using Application.RepositoryContracts;
using Moq;

namespace Tests.Unit;

public class ContactServiceTests
{
    private readonly Mock<IContactRepository> _mockRepo;
    private readonly ContactServiceTests _service;

    public ContactServiceTests()
    {
        _mockRepo = new Mock<IContactRepository>();
        _service = new ContactService(_mockRepo.Object);
    }
}
