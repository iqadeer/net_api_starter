using AutoMapper;
using NetAPI.Application.Dtos;
using NetAPI.Application.Interfaces;
using NetAPI.Domain.Entities;
using NetAPI.Domain.Interfaces;

namespace NetAPI.Application.Services;

public class SpeakerService(ISpeakerRepo speakerRepo, IMapper mapper) : ISpeakerService
{
    public SpeakerDto[] GetAll()
    {
        return mapper.Map<SpeakerDto[]>(speakerRepo.GetAll());
    }

    public SpeakerDto? Get(int id)
    {
        return mapper.Map<SpeakerDto?>(speakerRepo.Get(id));
    }

    public void Delete(int id)
    {
        speakerRepo.Delete(id);
    }

    public void Update(SpeakerDto speakerDto)
    {
        speakerRepo.Update(mapper.Map<Speaker>(speakerDto));
    }

    public int? Create(SpeakerDto speakerDto)
    {
        return speakerRepo.Create(mapper.Map<Speaker>(speakerDto));
    }
}