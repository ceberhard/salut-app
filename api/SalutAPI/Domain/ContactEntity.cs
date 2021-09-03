namespace SalutAPI.Domain;

public class ContactEntity {
    public long Id { get; init; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}