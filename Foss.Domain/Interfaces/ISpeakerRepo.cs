using NetAPI.Domain.Entities;

namespace NetAPI.Domain.Interfaces;

public interface ISpeakerRepo
{
    Speaker[] GetAll();
    Speaker? Get(int id);
    void Delete(int id);
    void Update(Speaker speakerDto);
    int Create(Speaker speakerDto);
}