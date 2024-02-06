using FluentAssertions;

namespace TestdrivenHotel.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            string expected = "Hello world";

            string result = "Hello world";

            result.Should().BeEquivalentTo(expected);

        }
    }
}