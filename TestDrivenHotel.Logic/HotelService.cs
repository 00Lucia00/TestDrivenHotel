using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDrivenHotel.Data;
using TestDrivenHotel.DataAccess;

namespace TestDrivenHotel.Logic
{   //Denna klass ska innehålla "CRUD"(typ) funktionalitet.
    public class HotelService 
    {
         
        //Create - Skapa
        public void InitializeRoomsList()
        {
            HotelData.Rooms.Add(new RoomModel { Id = 101, RoomType = "Single Room", Price = 100, SquareMeters = 20, GuestCapacity = 2, View = "Sea" });
            HotelData.Rooms.Add(new RoomModel { Id = 102, RoomType = "Single Room", Price = 100, SquareMeters = 20, GuestCapacity = 4, View = "Garden" });
            HotelData.Rooms.Add(new RoomModel { Id = 103, RoomType = "Single Room", Price = 100, SquareMeters = 20, GuestCapacity = 6, View = "Sea" });
            HotelData.Rooms.Add(new RoomModel { Id = 201, RoomType = "Double Room", Price = 150, SquareMeters = 30, GuestCapacity = 2, View = "Garden" });
            HotelData.Rooms.Add(new RoomModel { Id = 202, RoomType = "Double Room", Price = 150, SquareMeters = 30, GuestCapacity = 4, View = "Sea" });
            HotelData.Rooms.Add(new RoomModel { Id = 203, RoomType = "Double Room", Price = 150, SquareMeters = 30, GuestCapacity = 6, View = "Garden" });
            HotelData.Rooms.Add(new RoomModel { Id = 301, RoomType = "Luxury Room", Price = 200, SquareMeters = 40, GuestCapacity = 2, View = "Sea" });
            HotelData.Rooms.Add(new RoomModel { Id = 302, RoomType = "Luxury Room", Price = 200, SquareMeters = 40, GuestCapacity = 4, View = "Sea" });
            HotelData.Rooms.Add(new RoomModel { Id = 303, RoomType = "Luxury Room", Price = 200, SquareMeters = 40, GuestCapacity = 6, View = "Sea" });
        }

        //Read - Läsa

        public RoomModel GetRoomById(int roomId)
        {
            var room = HotelData.Rooms.FirstOrDefault(r => r.Id == roomId);
            if (room == null)
            {
                throw new ArgumentException($"Room with ID {roomId} not found.");
            }
            return room;
        }

        public List<RoomModel> GetRoomsByType(string roomType)
        {
            return HotelData.Rooms.Where(r => r.RoomType == roomType.ToString()).ToList();
        }

        public List<RoomModel> GetAllRooms()
        {
            return HotelData.Rooms;
        }

        // Denna funktion hämtar tillgänliga rum utifrån sökkriteria
        public List<RoomModel> GetAvailableRooms(DateTime checkInDate, DateTime checkOutDate, int numberOfGuests, string roomType = null)
        {
            // lägger till det rum som har kapasietet för det anatlet gäster man söker i separat variabel
            var availableRooms = HotelData.Rooms.Where(r => r.GuestCapacity >= numberOfGuests).ToList();

            // kollar om man valt till rumtyp - annars visas alla rumtyper utifrån det andra kriterierna
            if (!string.IsNullOrEmpty(roomType))
            {
                availableRooms = availableRooms.Where(r => r.RoomType == roomType && r.GuestCapacity >= numberOfGuests).ToList();
            }

            if (checkInDate >= checkOutDate)
            {
                throw new ArgumentException("Check-out date must be after check-in date.");
            }
            if (numberOfGuests <= 0)
            {
                throw new ArgumentException("Number of guests must be a positive integer.");
            }
            // kollar ifall datumen man sökt efter är tillgängliga och inte ligger i bookings listan
            foreach (var booking in HotelData.Bookings)
             {
                if (checkInDate >= booking.CheckInDate && checkInDate < booking.CheckOutDate ||
                    checkOutDate > booking.CheckInDate && checkOutDate <= booking.CheckOutDate ||
                    checkInDate < booking.CheckInDate && checkOutDate > booking.CheckOutDate)
                {
                     //tarbort det rum som är bokade under det sökta datumen
                     availableRooms.Remove(availableRooms.First(r => r.Id == booking.RoomId));
                }
             }
          
            return availableRooms;

        }


        //Update - Uppdatera

        //Booka ett rum funktion(lägger till valda parametrar i bokade rum listan) ger tillbaka en instans av objektet
        public BookingModel BookRoom(int roomId, DateTime checkInDate, DateTime checkOutDate, int numberOfGuests)
        {
           
            var roomBooking = new BookingModel
            {
               
                RoomId = roomId,
                CheckInDate = checkInDate,
                CheckOutDate = checkOutDate,
                NumberOfGuests = numberOfGuests
            };

            HotelData.Bookings.Add(roomBooking);

            if (!HotelData.Rooms.Any(room => room.Id == roomId))
            {
                throw new ArgumentException("Room not found", nameof(roomId));
            }

            return roomBooking;
        }

        //Delete - ta Bort

        public void DeleteBooking(int bookingId)
        {
            var booking = HotelData.Bookings.FirstOrDefault(b => b.Id == bookingId);
           
            if (booking == null)
            {
                throw new ArgumentException("Booking not found", nameof(bookingId));
            }

            HotelData.Bookings.Remove(booking);
        }
    }
}
