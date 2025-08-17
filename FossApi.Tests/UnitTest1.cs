using AutoFixture;
using AutoFixture.AutoNSubstitute;
using AutoFixture.Xunit2;
using Shouldly;

namespace NetAPI.API.Tests
{
    public class UnitTest1
    {
        [Theory, AutoData]
        public void Test1(int number)
        {
            var fixture = new Fixture().Customize(new AutoNSubstituteCustomization());
            var number2 = fixture.Create<int>();
            var number1 = number;
            number1.ShouldBe(number);
        }
    }
}