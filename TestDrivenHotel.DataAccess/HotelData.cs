using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDrivenHotel.DataAccess;

namespace TestDrivenHotel.Data
{   //Denna Klass ska agera som en mockup databas då detta projekt inte ska använda sig utav en databas
    public class HotelData
    {

        public static List<RoomModel> Rooms = new List<RoomModel>();
       
        public static List<BookingModel> Bookings = new List<BookingModel>();

        public static List<GuestModel> BookingIds = new List<GuestModel>();
    }
}
