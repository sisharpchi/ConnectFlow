namespace Domain.Entities;

public class Contact
{
    public long ContactId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public DateTime CreatedAt { get; set; }

    public long UserId { get; set; }
    public User User { get; set; }
}
