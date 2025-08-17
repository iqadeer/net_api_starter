using NetAPI.Domain.Enums;

namespace NetAPI.Application.Dtos;

public class SpeakerDto
{
    public int? Id { get; set; }
    public required string Name { get; set; }
    public int Gender { get; set; }
    public required string Email { get; set; }
    public required Country Country { get; set; }
    public required DateTime Dob { get; set; }
    public required string Phone { get; set; }
    public required bool AcceptTerms { get; set; }

}