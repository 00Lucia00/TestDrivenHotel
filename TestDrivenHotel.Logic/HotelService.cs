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

        //Read - Läsa

        // Denna funktion hämtar tillgänliga rum utifrån sökkriteria
        public List<RoomModel> GetAvailableRooms(DateTime checkInDate, DateTime checkOutDate, int numberOfGuests, string roomType = null)
        {
            // lägger till det rum som har kapasietet för det anatlet gäster man söker i separat variabel
            var availableRooms = HotelData.Rooms.Where(r => r.GuestCapacity >= numberOfGuests).ToList();

            // kollar om man valt till rumtyp - annars visas alla rumtyper utifrån det andra kriterierna
            if (!string.IsNullOrEmpty(roomType))
            {
                availableRooms = availableRooms.Where(r => r.RoomType == roomType).ToList();
            }
            if(checkInDate < DateTime.Today || checkOutDate < DateTime.Today.AddDays(1))
            {
                availableRooms.Clear();
                return availableRooms;
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
        //Delete - ta Bort
    }
}
