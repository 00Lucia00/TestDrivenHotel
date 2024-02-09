using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDrivenHotel.Data;
using TestDrivenHotel.Logic;

namespace TestdrivenHotel.Test
{
    public class HotelService_BookingTest
    {

        public class DeleteBookingTests
        {

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
                HotelService service = new ();
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
                service.InitializeRoomsList();
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
                service.InitializeRoomsList();
                service.BookRoom(101, DateTime.Today, DateTime.Today.AddDays(2), 2);
                var bookingId = 2;

                // Act and Assert
                Assert.Throws<ArgumentException>(() => service.DeleteBooking(bookingId));
            }
        }
    }
}
