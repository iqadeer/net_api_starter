using AutoFixture.Xunit2;
using NetAPI.Domain.Entities;
using NetAPI.Domain.Enums;
using NetAPI.Infrastructure.Repos;
using Shouldly;

namespace NetApi.Infrastructure.Tests
{
    public class PersonRepoTest
    {
        [Theory, AutoData]
        public void ShouldReturnAllPersons(List<Person> persons)
        {
            var sut = new PersonRepo();

            foreach (var person in persons)
            {
                sut.AddPerson(person);
            }

            var personsReceived = sut.GetPersons();
            personsReceived.ShouldNotBeEmpty();
            personsReceived.Length.ShouldBe(persons.Count);

            foreach (var personReceived in personsReceived)
            {
                var person = persons.Single(p => p.Id == personReceived.Id);
                person.ShouldNotBeNull();
                person.FirstName.ShouldBe(personReceived.FirstName);
                person.LastName.ShouldBe(personReceived.LastName);
            }
        }

        [Theory, AutoData]
        public void ShouldReturnPersonById(List<Person> persons)
        {
            var sut = new PersonRepo();

            foreach (var person in persons)
            {
                sut.AddPerson(person);
            }

            var personReceived = sut.GetPerson(persons[0].Id!.Value);

            var expectedPerson = persons[0];
            expectedPerson.ShouldNotBeNull();
            expectedPerson.FirstName.ShouldBe(personReceived.FirstName);
            expectedPerson.LastName.ShouldBe(personReceived.LastName);
        }


        public class SpeakerInMemoryRepoTests()
        {
            [Theory, AutoData]
            public void Should_Get_All_Speakers()
            {
                var sut = new SpeakerInMemoryRepo();

                var speakers = sut.GetAll();
                speakers.ShouldNotBeNull();
                speakers.Length.ShouldBe(2);
                speakers[0].Name.ShouldBe("Alice Johnson");
                speakers[0].Country.ShouldBe(Country.Pakistan);
            }

            [Theory]
            [InlineData(1)]
            [InlineData(2)]
            public void Should_Get_Speaker_By_Id(int id)
            {
                var sut = new SpeakerInMemoryRepo();

                var speakers = sut.GetAll();
                var speaker = sut.Get(id);
                speaker.ShouldNotBeNull();
                speaker.ShouldBeEquivalentTo(speakers.Single(s => s.Id == id));
            }

            [Theory]
            [InlineData(1)]
            [InlineData(2)]
            public void Should_Delete_Speaker_By_Id(int id)
            {
                var sut = new SpeakerInMemoryRepo();

                var speaker = sut.Get(id);
                speaker.ShouldNotBeNull();

                sut.Delete(id);
                speaker = sut.Get(id);

                speaker.ShouldBeNull();
            }

            [Theory]
            [InlineAutoData(1)]
            [InlineAutoData(2)]
            public void Should_Update_Speaker(int id, Speaker speakerToUpdate)
            {
                var sut = new SpeakerInMemoryRepo();
                speakerToUpdate.Id = id;

                sut.Update(speakerToUpdate);

                var updatedSpeaker = sut.Get(id);
                updatedSpeaker.ShouldNotBeNull();
                updatedSpeaker.ShouldBeEquivalentTo(speakerToUpdate);
            }

            [Theory]
            [InlineAutoData(1)]
            [InlineAutoData(2)]
            public void Should_Create_Speaker(int id, Speaker speakerToCreate)
            {
                var sut = new SpeakerInMemoryRepo();

                var speaker = sut.Get(speakerToCreate.Id);
                speaker.ShouldBeNull();

                sut.Create(speakerToCreate);
                speaker = sut.Get(speakerToCreate.Id);

                speaker.ShouldNotBeNull();
                speaker.ShouldBeEquivalentTo(speakerToCreate);

            }
        }

    }
}