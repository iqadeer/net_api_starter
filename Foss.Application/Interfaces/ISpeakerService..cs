using NetAPI.Application.Dtos;

namespace NetAPI.Application.Interfaces;

public interface ISpeakerService
{
    SpeakerDto[] GetAll();
    SpeakerDto? Get(int id);
    void Delete(int id);
    void Update(SpeakerDto speakerDto);
    int? Create(SpeakerDto speakerDto);
}