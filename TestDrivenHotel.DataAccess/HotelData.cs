using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDrivenHotel.Data;
using TestDrivenHotel.DataAccess;

namespace TestDrivenHotel.Data
{   //Denna Klass ska agera som en mockup databas då detta projekt inte ska använda sig utav en databas
    public class HotelData
    {

        public static List<RoomModel> Rooms = new List<RoomModel>()
         {
            new RoomModel { Id = 101, RoomType = "Single Room", Price = 100, SquareMeters = 20, GuestCapacity = 2, View = "Sea" },
            new RoomModel { Id = 102, RoomType = "Single Room", Price = 100, SquareMeters = 20, GuestCapacity = 4, View = "Garden" },
            new RoomModel { Id = 103, RoomType = "Single Room", Price = 100, SquareMeters = 20, GuestCapacity = 6, View = "Sea" },
            new RoomModel { Id = 201, RoomType = "Double Room", Price = 150, SquareMeters = 30, GuestCapacity = 2, View = "Garden" },
            new RoomModel { Id = 202, RoomType = "Double Room", Price = 150, SquareMeters = 30, GuestCapacity = 4, View = "Sea" },
            new RoomModel { Id = 203, RoomType = "Double Room", Price = 150, SquareMeters = 30, GuestCapacity = 6, View = "Garden" },
            new RoomModel { Id = 301, RoomType = "Luxury Room", Price = 200, SquareMeters = 40, GuestCapacity = 2, View = "Sea" },
            new RoomModel { Id = 302, RoomType = "Luxury Room", Price = 200, SquareMeters = 40, GuestCapacity = 4, View = "Sea" },
            new RoomModel { Id = 303, RoomType = "Luxury Room", Price = 200, SquareMeters = 40, GuestCapacity = 6, View = "Sea" },
        };
       
        public static List<BookingModel> Bookings = new List<BookingModel>();

        public static List<GuestModel> Guests= new List<GuestModel>();
    }
}
