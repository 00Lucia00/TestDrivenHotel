using FluentAssertions;
using System.Security.Cryptography.X509Certificates;
using TestDrivenHotel.DataAccess;
using TestDrivenHotel.Logic;

namespace TestdrivenHotel.Test
{
    public class HotelServiceTest
    {
        private readonly HotelService service;

        public HotelServiceTest()
        {
            service = new HotelService();
        }

        [Fact]
        public void GetAvailableRooms_WhenNoBookings_ShouldReturnExpectedRooms()
        {
            // Given - Arrange
            
            var checkInDate = DateTime.Now;
            var checkOutDate = checkInDate.AddDays(3);
            var numberOfGuests = 2;

            // When - Act
            var availableRooms = service.GetAvailableRooms(checkInDate, checkOutDate, numberOfGuests);
            //Then - Assert -Using fluent assertions to use more natural language
            availableRooms.Should().NotBeEmpty();
            availableRooms.Should().OnlyContain(r => r.GuestCapacity >= numberOfGuests);

        }

        // test checking if list is empty when dates not avalible
        [Fact]
        public void GetAvailableRooms_ReturnsEmptyList_WhenCalledWithCheckOutDateBeforeCheckInDate()
        {
            // Arrange
           
            var checkInDate = DateTime.Now;
            var checkOutDate = checkInDate.AddDays(-2);
            var numberOfGuests = 1;

            // Act
            var availableRooms = service.GetAvailableRooms(checkInDate, checkOutDate, numberOfGuests);

            // Assert
            availableRooms.Should().BeEmpty();
        }
    }
}