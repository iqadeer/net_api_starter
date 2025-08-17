namespace NetAPI.Application.Dtos;

public class PersonDto
{
    public int? Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public DateTime? Dob { get; set; }
    public string? Gender { get; set; }
    public required string City { get; set; }
    public bool TermsAccepted { get; set; }
}