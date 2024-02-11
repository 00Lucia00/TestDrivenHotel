using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TestDrivenHotel.Data;
using TestDrivenHotel.DataAccess;
using TestDrivenHotel.Logic;

namespace TestdrivenHotel.UI.Pages
{
    public class BookingModel : PageModel
    {
        HotelService hotelService = new HotelService();

        
        [BindProperty(SupportsGet = true)]
        public DateTime CheckInDate { get; set; } = DateTime.Now;

        [BindProperty(SupportsGet = true)]
        public DateTime CheckOutDate { get; set; } = DateTime.Now;

        [BindProperty(SupportsGet = true)]
        public int NumberOfGuests { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? RoomId { get; set; }
        
        [BindProperty(SupportsGet = true)]
        public string? RoomType { get; set; }

        [BindProperty(SupportsGet = true)]
        public int guestId { get; set; }
       
        public List<BookingModel>? Bookings { get; set; } = new List<BookingModel>();

        public List<GuestModel>? Guests { get; set; }

      

        public IActionResult OnGet(int roomId, DateTime checkInDate, DateTime checkOutDate, int numberOfGuests,string RoomType, int? guestId)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var booking = hotelService.BookRoom(roomId, checkInDate, checkOutDate, numberOfGuests, guestId);


            TempData["Message"] = "Booking successful!";

            return RedirectToPage("/AddGuest", new { bookingId = booking.Id });
        }

       /* public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var booking = hotelService.BookRoom(Booking.RoomId, Booking.CheckInDate, Booking.CheckOutDate, Booking.NumberOfGuests, Booking.GuestId);
            
            return RedirectToPage("/Booking");
        }*/
    
       
    }
}
