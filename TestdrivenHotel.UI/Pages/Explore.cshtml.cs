using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TestDrivenHotel.Data;
using TestDrivenHotel.DataAccess;
using TestDrivenHotel.Logic;

namespace TestdrivenHotel.UI.Pages
{
    public class ExploreModel : PageModel
    {
        HotelService _roomService = new HotelService();

       

        [BindProperty]
        public RoomModel Room { get; set; }
        
        [BindProperty]
        public BookingModel Booking { get; set; }

        [BindProperty]
        public int roomId { get; set; }

        [BindProperty (SupportsGet = true)]
        public DateTime CheckInDate { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime CheckOutDate { get; set; }

        [BindProperty]
        public int NumberOfGuests { get; set; }
        [BindProperty]
        public string? RoomType { get; set; }


        public List<BookingModel> BookedRooms { get; set; }
        public List<RoomModel> Rooms { get; set; }

        public void OnGet()
        {

            Rooms = _roomService.GetAvailableRooms(CheckInDate, CheckOutDate, NumberOfGuests, RoomType);
        }

        public IActionResult OnPost()
        {
            
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Rooms = _roomService.GetAvailableRooms(CheckInDate, CheckOutDate, NumberOfGuests, RoomType);
        
            return Page();
        }
    }
}