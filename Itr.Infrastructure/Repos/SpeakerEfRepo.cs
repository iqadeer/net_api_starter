using NetAPI.Application.Interfaces;
using NetAPI.Domain.Entities;

namespace NetAPI.Infrastructure.Repos;

public class SpeakerEfRepo(SpeakerDbContext context) : ISpeakerRepo
{
    public Speaker[] GetAll()
    {
        return context.Speakers.ToArray();
    }

    public Speaker? Get(int id)
    {
        return context.Speakers.Find(id);
    }

    public void Delete(int id)
    {
        var speaker = context.Speakers.Find(id);
        if (speaker is null) return;

        context.Speakers.Remove(speaker);
        context.SaveChanges();
    }

    public void Update(Speaker speaker)
    {
        context.Speakers.Update(speaker);
        context.SaveChanges();
    }

    public int Create(Speaker speaker)
    {
        context.Speakers.Add(speaker);
        context.SaveChanges();
        return speaker.Id;
    }
}