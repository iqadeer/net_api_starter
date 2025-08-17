using NetAPI.Domain.Enums;

namespace NetAPI.Domain.Entities;

public class Speaker
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Gender { get; set; }
    public string Email { get; set; } = string.Empty;
    public Country Country { get; set; }
    public string Phone { get; set; } = string.Empty;
    public DateTime Dob { get; set; }
    public bool AcceptTerms { get; set; }
}
