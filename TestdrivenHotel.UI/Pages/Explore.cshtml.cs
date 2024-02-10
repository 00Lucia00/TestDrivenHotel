using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TestDrivenHotel.Data;
using TestDrivenHotel.DataAccess;
using TestDrivenHotel.Logic;

namespace TestdrivenHotel.UI.Pages
{
    public class ExploreModel : PageModel
    {
        HotelService hotelService = new();

        [BindProperty(SupportsGet = true)]
        public DateTime CheckinDate { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime CheckoutDate { get; set; }

        [BindProperty(SupportsGet = true)]
        public int NumberOfGuests { get; set; }

        [BindProperty(SupportsGet = true)]
        public string RoomType { get; set; }

        public List<RoomModel> SearchResults { get; set; }

        

        public IActionResult OnGet(DateTime CheckInDate, DateTime CheckOutDate, int NumberOfGuests, string RoomType)
        {
            SearchResults = hotelService.GetAvailableRooms(CheckInDate, CheckOutDate, NumberOfGuests, RoomType);
           
            return Page();
        }

        public IActionResult actionResult()
        {
            if (!ModelState.IsValid)
            {
                
            }
            return Page();
        }

        
    }
}