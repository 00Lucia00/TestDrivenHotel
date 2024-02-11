using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TestDrivenHotel.Data;
using TestDrivenHotel.Logic;

namespace TestdrivenHotel.Test
{
    public class HotelService_BookingTest
    {
        //Bookingtest
        //--------------------GetAllBookings---------------------
        [Fact]
        public void GetAllBookings_WithEmptyList_ReturnsEmptyList()
        {
            // Arrange
            var hotelService = new HotelService();

            // Act
            var bookings = hotelService.GetAllBookings();

            // Assert
            bookings.Should().NotBeNull(); // Check if bookings list is not null
            bookings.Should().BeEmpty(); // Check if bookings list is empty
        }

        [Fact]
        public void GetAllBookings_WithNonEmptyList_ReturnsAllBookings()
        {
            // Arrange
            var hotelService = new HotelService();

            // Create some bookings
            hotelService.BookRoom(101, DateTime.Today.AddDays(1), DateTime.Today.AddDays(3), 2, 1);
            hotelService.BookRoom(102, DateTime.Today.AddDays(2), DateTime.Today.AddDays(4), 4, 2);

            // Act
            var bookings = hotelService.GetAllBookings();

            // Assert
            bookings.Should().NotBeNull(); // Check if bookings list is not null
            bookings.Should().NotBeEmpty(); // Check if bookings list is not empty
                                            // You can add more assertions to validate specific properties or conditions of the bookings
        }

        //------------------------BookRoom---------------------------
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
            var booking = service.BookRoom(roomId, checkInDate, checkOutDate, numberOfGuests, 12);

            // Assert
            booking.Should().NotBeNull(); // Check if booking is not null
            booking.RoomId.Should().Be(roomId); // Check if booking room ID is correct
            HotelData.Bookings.Should().Contain(b => b.RoomId == roomId && b.CheckInDate == checkInDate && b.CheckOutDate == checkOutDate && b.NumberOfGuests == numberOfGuests);//Check if parameters exists in the static List

        }

        // -------------EdgeCase----BookRoom------------------
        public void BookRoom_WithOverlappingBooking_ThrowsException()
        {
            // Arrange
            var hotelService = new HotelService();
            int roomId = 101;
            DateTime checkInDate = DateTime.Today.AddDays(1);
            DateTime checkOutDate = DateTime.Today.AddDays(3);
            int numberOfGuests = 2;
            // Add a booking that overlaps with the test booking
            hotelService.BookRoom(roomId, checkInDate.AddDays(-1), checkOutDate.AddDays(1), numberOfGuests, null);

            // Act & Assert
            hotelService.Invoking(s => s.BookRoom(roomId, checkInDate, checkOutDate, numberOfGuests, null))
                .Should().Throw<ArgumentException>()
                .WithMessage("Room is already booked for the selected dates.");
        }
        [Fact]
        public void BookRoom_WithNonexistentRoomId_ThrowsException()
        {
            // Arrange
            var hotelService = new HotelService();
            int nonexistentRoomId = 999;
            DateTime checkInDate = DateTime.Today.AddDays(1);
            DateTime checkOutDate = DateTime.Today.AddDays(3);
            int numberOfGuests = 2;

            // Act & Assert
            hotelService.Invoking(s => s.BookRoom(nonexistentRoomId, checkInDate, checkOutDate, numberOfGuests, null))
                .Should().Throw<ArgumentException>()
                .WithMessage("Room not found");
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
            Assert.Throws<ArgumentException>(() => service.BookRoom(roomId, checkInDate, checkOutDate, numberOfGuests, 3));
        }

        // ------------------UpdateBooking----------------------------
    
        [Fact]
        public void UpdateBooking_WithNonExistingGuestId_DoesNotUpdateBooking()
        {
            // Arrange
            var hotelService = new HotelService();
            int nonExistingGuestId = 999; // Non-existing guest ID
            int roomId = 101; // Existing room ID
            int newNumberOfGuests = 2;
            var newCheckInDate = DateTime.Today.AddDays(1);
            var newCheckOutDate = DateTime.Today.AddDays(3);

            // Act
            hotelService.UpdateBooking(nonExistingGuestId, roomId, newNumberOfGuests, newCheckInDate, newCheckOutDate);

            // Assert
            // Ensure that no booking is updated
            var updatedBooking = HotelData.Bookings.FirstOrDefault(b => b.guestId == nonExistingGuestId);
            updatedBooking.Should().BeNull();
        }

        [Fact]
        public void UpdateBooking_WithNonExistingRoomId_DoesNotUpdateBooking()
        {
            // Arrange
            var hotelService = new HotelService();
            int guestId = 1; // Existing guest ID
            int nonExistingRoomId = 999; // Non-existing room ID
            int newNumberOfGuests = 2;
            var newCheckInDate = DateTime.Today.AddDays(1);
            var newCheckOutDate = DateTime.Today.AddDays(3);

            // Act
            hotelService.UpdateBooking(guestId, nonExistingRoomId, newNumberOfGuests, newCheckInDate, newCheckOutDate);

            // Assert
            // Ensure that no booking is updated
            var updatedBooking = HotelData.Bookings.FirstOrDefault(b => b.guestId == guestId);
            updatedBooking.Should().BeNull();
        }

        [Fact]
        public void UpdateBooking_WithExistingGuestAndRoom_UpdatesBooking()
        {
            // Arrange
            var hotelService = new HotelService();
            int guestId = 1; // Existing guest ID
            int roomId = 101; // Existing room ID
            int newNumberOfGuests = 3; // Change in the number of guests
            var newCheckInDate = DateTime.Today.AddDays(2); // Change in check-in date
            var newCheckOutDate = DateTime.Today.AddDays(4); // Change in check-out date

            // Act
            hotelService.BookRoom(101, DateTime.Today, DateTime.Today.AddDays(2), 2, 1);
            hotelService.UpdateBooking(guestId, roomId, newNumberOfGuests, newCheckInDate, newCheckOutDate);

            // Assert
            // Ensure that the booking is updated with new details
            var updatedBooking = HotelData.Bookings.FirstOrDefault(b => b.guestId == guestId && b.RoomId == roomId);
            updatedBooking.Should().NotBeNull();
            updatedBooking.NumberOfGuests.Should().Be(newNumberOfGuests);
            updatedBooking.CheckInDate.Should().Be(newCheckInDate);
            updatedBooking.CheckOutDate.Should().Be(newCheckOutDate);
        }

        //________DeleteBooking__________

        [Fact]
        public void DeleteBooking_RemovesBookingFromBookingsList_WhenCalledWithValidParameters()
        {
            // Arrange
            var service = new HotelService();
            //service.InitializeRoomsList();
            service.BookRoom(101, DateTime.Today, DateTime.Today.AddDays(2), 2, 5);

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
            service.BookRoom(101, DateTime.Today, DateTime.Today.AddDays(2), 2, 4);
            var bookingId = 2;

            // Act and Assert
            Assert.Throws<ArgumentException>(() => service.DeleteBooking(bookingId));
        }

    }
}
