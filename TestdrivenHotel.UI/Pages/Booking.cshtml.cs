using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TestDrivenHotel.Logic;

namespace TestdrivenHotel.UI.Pages
{
    public class BookingModel : PageModel
    {
        HotelService hotelService = new HotelService();

        [BindProperty]
        public DateTime CheckInDate { get; set; } = DateTime.Now;

        [BindProperty]
        public DateTime CheckOutDate { get; set; } = DateTime.Now;

        [BindProperty]
        public int NumberOfGuests { get; set; }

        [BindProperty]
        public string? RoomType { get; set; }

        public void OnGet()
        {
        }
    }
}
