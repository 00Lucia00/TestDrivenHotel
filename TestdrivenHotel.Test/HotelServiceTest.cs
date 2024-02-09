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

        
       /* public void InitializeRoomsList_AddsRoomsToRoomsList()
        {
            // Arrange 
            HotelService service = new HotelService();
            // Act
            service.InitializeRoomsList();
            // Assert
            HotelData.Rooms.Should().HaveCount(9);
            HotelData.Rooms.Should().NotBeEmpty();
        }*/

        // ---------------Get ALL Rooms-------------------
        [Fact]
        public void GetAllRooms_ReturnsAllRooms()
        {
            // Arrange
            var hotelService = new HotelService();
            //hotelService.InitializeRoomsList();

            // Act
            var rooms = hotelService.GetAllRooms();

            // Assert
            rooms.Should().NotBeNullOrEmpty();
            // Add more assertions as needed
        }


        // ------------AvailableRooms method test-------------
        [Fact]
        public void GetAvailableRooms_WhenNoBookings_ShouldReturnExpectedRooms()
        {
            // Given - Arrange
            HotelService service = new HotelService();
           // service.InitializeRoomsList();
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
           // service.InitializeRoomsList();
            var checkInDate = DateTime.Today;
            var checkOutDate = checkInDate.AddDays(2);
            var numberOfGuests = 6;
            var roomType = "Double Room";

            // Act
            var availableRooms = service.GetAvailableRooms(checkInDate, checkOutDate, numberOfGuests, roomType);

            // Assert
            availableRooms.Should().NotBeEmpty();
            availableRooms.Should().OnlyContain(r => r.RoomType == roomType && r.GuestCapacity >= numberOfGuests);
        }


        // -----------------GetRoomByID--------------------
        [Fact]
        public void GetRoomById_WithValidId_ReturnsRoom()
        {
            // Arrange
            var hotelService = new HotelService();
            //hotelService.InitializeRoomsList();
            int validRoomId = 101;

            // Act
            var room = hotelService.GetRoomById(validRoomId);

            // Assert
            room.Should().NotBeNull();
            room.Id.Should().Be(validRoomId);
        }

        [Fact]
        public void GetRoomById_WithInvalidId_ThrowsException()
        {
            // Arrange
            var hotelService = new HotelService();
           // hotelService.InitializeRoomsList();
            int invalidRoomId = 999;

            // Act & Assert
            hotelService.Invoking(s => s.GetRoomById(invalidRoomId))
                .Should().Throw<ArgumentException>()
                .WithMessage($"Room with ID {invalidRoomId} not found.");
        }

        //------------------GETBYRoomType-------------------------------
        [Fact]
        public void GetRoomsByType_WithValidType_ReturnsRoomsOfType()
        {
            // Arrange
            var hotelService = new HotelService();
            //hotelService.InitializeRoomsList();
            string roomType = "Single Room"; // Assuming "Single Room" is a valid room type

            // Act
            var rooms = hotelService.GetRoomsByType(roomType);

            // Assert
            rooms.Should().NotBeNull(); // Check if rooms list is not null
            rooms.Should().NotBeEmpty(); // Check if rooms list is not empty
            rooms.Should().OnlyContain(r => r.RoomType == roomType); // Check if all rooms have the specified room type
        }

        [Fact]
        public void GetRoomsByType_WithInvalidType_ReturnsEmptyList()
        {
            // Arrange
            var hotelService = new HotelService();
            //hotelService.InitializeRoomsList();
            string roomType = "Nonexistent Room Type"; // Assuming "Nonexistent Room Type" is not a valid room type

            // Act
            var rooms = hotelService.GetRoomsByType(roomType);

            // Assert
            rooms.Should().NotBeNull(); // Check if rooms list is not null
            rooms.Should().BeEmpty(); // Check if rooms list is empty
        }
        // ---------------edgecases---GetRoom by type------------------

        [Fact]
        public void GetRoomsByType_WithNullRoomType_ReturnsEmptyList()
        {
            // Arrange
            var hotelService = new HotelService();
           // hotelService.InitializeRoomsList();
            string roomType = null; // Null room type

            // Act
            var rooms = hotelService.GetRoomsByType(roomType);

            // Assert
            
            rooms.Should().BeNull(); // Check if rooms list is empty
        }

      

        //Bookingtest
        [Fact]
        public void BookRoom_AddsBookingToList_WhenCalledWithValidParameters()
        {
            // Arrange
            HotelService service = new HotelService();
            //service.InitializeRoomsList();
            var checkInDate = DateTime.Today;
            var checkOutDate = checkInDate.AddDays(2);
            var numberOfGuests = 4;
            var roomId = 101;

            // Act
            var booking = service.BookRoom(roomId, checkInDate, checkOutDate, numberOfGuests);

            // Assert
            HotelData.Bookings.Should().Contain(b => b.RoomId == roomId && b.CheckInDate == checkInDate && b.CheckOutDate == checkOutDate && b.NumberOfGuests == numberOfGuests);
        }

        [Fact]
        public void BookRoom_ThrowsArgumentException_WhenCalledWithRoomIdNotInRoomsList()
        {
            // Arrange
            HotelService service = new();
            var checkInDate = DateTime.Today;
            var checkOutDate = checkInDate.AddDays(2);
            var numberOfGuests = 1;
            var roomId = 2;


            // Act and Assert
            Assert.Throws<ArgumentException>(() => service.BookRoom(roomId, checkInDate, checkOutDate, numberOfGuests));
        }

        //________DeleteBooking__________

        [Fact]
        public void DeleteBooking_RemovesBookingFromBookingsList_WhenCalledWithValidParameters()
        {
            // Arrange
            var service = new HotelService();
            //service.InitializeRoomsList();
            service.BookRoom(101, DateTime.Today, DateTime.Today.AddDays(2), 2);

            var bookingId = 0;

            // Act
            service.DeleteBooking(bookingId);

            // Assert
            HotelData.Bookings.Should().NotContain(b => b.Id == bookingId);
        }

        [Fact]
        public void DeleteBooking_ThrowsArgumentException_WhenCalledWithBookingIdNotInBookingsList()
        {
            // Arrange
            var service = new HotelService();
           // service.InitializeRoomsList();
            service.BookRoom(101, DateTime.Today, DateTime.Today.AddDays(2), 2);
            var bookingId = 2;

            // Act and Assert
            Assert.Throws<ArgumentException>(() => service.DeleteBooking(bookingId));
        }
    }
}



