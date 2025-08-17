using NetAPI.Application.Interfaces;
using NetAPI.Domain.Entities;
using NetAPI.Domain.Enums;

namespace NetAPI.Infrastructure.Repos;

public class SpeakerInMemoryRepo : ISpeakerRepo
{
    private readonly List<Speaker> _speakers = new List<Speaker>
    {
        new Speaker
        {
            Id = 1,
            Name = "Alice Johnson",
            Gender = 0, // Example: 0 = Female, 1 = Male
            Email = "alice.johnson@example.com",
            Country = Country.Pakistan,
            Phone = "+1-202-555-0123",
            Dob = new DateTime(1985, 3, 15),
            AcceptTerms = true,

        },
        new Speaker
        {
            Id = 2,
            Name = "Brian Smith",
            Gender = 1,
            Email = "brian.smith@example.com",
            Country = Country.Denmark,
            Phone = "+1-416-555-0198",
            Dob = new DateTime(1990, 7, 22),
            AcceptTerms = true,

        },
    };

    public Speaker[] GetAll()
    {
        return _speakers.ToArray();
    }

    public Speaker? Get(int id)
    {
        return _speakers.FirstOrDefault(s => s.Id == id);
    }

    public void Delete(int id)
    {
        var speakerToDelete = _speakers.Find(s => s.Id == id);
        if (speakerToDelete != null)
            _speakers.Remove(speakerToDelete);
    }

    public void Update(Speaker speaker)
    {
        var speakerToUpdate = _speakers.Find(s => s.Id == speaker.Id);
        if (speakerToUpdate is null) return;

        _speakers[_speakers.FindIndex(s => s.Id == speaker.Id)] = speaker;
        //speaker.Name = speakerDto.Name;
        //speaker.Gender = speakerDto.Gender;
        //speaker.Email = speakerDto.Email;
        //speaker.Country = speakerDto.Country;
        //speaker.Phone = speakerDto.Phone;
        //speaker.Dob = speakerDto.Dob;
    }

    public int Create(Speaker speakerDto)
    {
        speakerDto.Id = _speakers.Select(s => s.Id).Max() + 1;
        _speakers.Add(speakerDto);
        return speakerDto.Id;
    }
}