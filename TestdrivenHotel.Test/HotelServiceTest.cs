using FluentAssertions;
using System.Security.Cryptography.X509Certificates;
using TestDrivenHotel.DataAccess;
using TestDrivenHotel.Logic;

namespace TestdrivenHotel.Test
{
    public class HotelServiceTest
    {



        [Fact]
        public void GetAvailableRooms_WhenNoBookings_ShouldReturnExpectedRooms()
        {
            // Given - Arrange
            HotelService service = new HotelService();
            var checkInDate = DateTime.Now;
            var checkOutDate = checkInDate.AddDays(3);
            var numberOfGuests = 2;

            // When - Act
            var availableRooms = service.GetAvailableRooms(checkInDate, checkOutDate, numberOfGuests);
            //Then - Assert -Using fluent assertions to use more natural language
            availableRooms.Should().NotBeEmpty();
            availableRooms.Should().OnlyContain(r => r.GuestCapacity >= numberOfGuests);

        }
    }
}