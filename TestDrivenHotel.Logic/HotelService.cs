using System;
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

        //Read - Läsa
        public List<RoomModel> GetAvailableRooms(DateTime checkInDate, DateTime checkOutDate, int numberOfGuests, string roomType = null)
        {
            var availableRooms = HotelData.Rooms.Where(r => r.GuestCapacity >= numberOfGuests).ToList();

            return availableRooms;
        }
        //Update - Uppdatera
        //Delete - ta Bort
    }
}
