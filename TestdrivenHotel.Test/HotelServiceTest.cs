using FluentAssertions;
using FluentAssertions.Common;
using System.Security.Cryptography.X509Certificates;
using TestDrivenHotel.Data;
using TestDrivenHotel.DataAccess;
using TestDrivenHotel.Logic;

namespace TestdrivenHotel.Test
{
    public class HotelServiceTest
    {

        [Fact]
        public void InitializeRoomsList_AddsRoomsToRoomsList()
        {
            // Arrange 
            HotelService service = new HotelService();
            // Act
            service.InitializeRoomsList();
            // Assert
            HotelData.Rooms.Should().HaveCount(9);
            HotelData.Rooms.Should().NotBeEmpty();
        }
    
        // ------------AvailableRooms method test-------------
        [Fact]
        public void GetAvailableRooms_WhenNoBookings_ShouldReturnExpectedRooms()
        {
            // Given - Arrange
            HotelService service = new HotelService();
            service.InitializeRoomsList();
            var checkInDate = DateTime.Now;
            var checkOutDate = checkInDate.AddDays(5);
            var numberOfGuests = 2;

            // When - Act
            var availableRooms = service.GetAvailableRooms(checkInDate, checkOutDate, numberOfGuests);
            //Then - Assert -Using fluent assertions to use more natural language
            availableRooms.Should().NotBeEmpty();
            availableRooms.Should().OnlyContain(r => r.GuestCapacity >= numberOfGuests);

        }
      

        [Fact]
        public void GetAvailableRooms_ReturnsOnlyMatchingRooms_WhenCalledWithRoomType()
        {
            // Arrange
            HotelService service = new HotelService();
            service.InitializeRoomsList();
            var checkInDate = DateTime.Today;
            var checkOutDate = checkInDate.AddDays(2);
            var numberOfGuests = 1;
            var roomType = "Double Room";

            // Act
            var availableRooms = service.GetAvailableRooms(checkInDate, checkOutDate, numberOfGuests, roomType);

            // Assert
            availableRooms.Should().NotBeEmpty();
            availableRooms.Should().OnlyContain(r => r.RoomType == roomType && r.GuestCapacity >= numberOfGuests);
        }

        // test checking if list is empty when dates not avalible
        [Fact]
        public void GetAvailableRooms_ReturnsEmptyList_WhenCalledWithCheckOutDateBeforeCheckInDate()
        {
            // Arrange
            HotelService service = new HotelService();
            service.InitializeRoomsList();
            var checkInDate = DateTime.Now;
            var checkOutDate = checkInDate.AddDays(-2);
            var numberOfGuests = 1;

            // Act
            var availableRooms = service.GetAvailableRooms(checkInDate, checkOutDate, numberOfGuests);

            // Assert
            availableRooms.Should().BeEmpty();
        }


        //Bookingtest

        [Fact]
        public void BookRoom_AddsBookingToList_WhenCalledWithValidParameters()
        {
            // Arrange
            HotelService service = new HotelService();
            var checkInDate = DateTime.Today;
            var checkOutDate = checkInDate.AddDays(2);
            var numberOfGuests = 4;
            var roomId = 102;

            // Act
            var booking = service.BookRoom(roomId, checkInDate, checkOutDate, numberOfGuests);

            // Assert
            HotelData.Bookings.Should().Contain(b => b.RoomId == roomId && b.CheckInDate == checkInDate && b.CheckOutDate == checkOutDate && b.NumberOfGuests == numberOfGuests);
        }

        [Fact]
        public void BookRoom_ThrowsArgumentException_WhenCalledWithRoomIdNotInRoomsList()
        {
            // Arrange
            HotelService service = new HotelService();
            var checkInDate = DateTime.Today;
            var checkOutDate = checkInDate.AddDays(2);
            var numberOfGuests = 1;
            var roomId = 2;


            // Act and Assert
            Assert.Throws<ArgumentException>(() => service.BookRoom(roomId, checkInDate, checkOutDate, numberOfGuests));
        }

    }
}