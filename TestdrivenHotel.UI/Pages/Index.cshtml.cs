using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TestDrivenHotel.Data;
using TestDrivenHotel.DataAccess;
using TestDrivenHotel.Logic;

namespace TestdrivenHotel.UI.Pages
{
    public class IndexModel : PageModel
    {

        HotelService hotelService = new HotelService();

        [BindProperty]
        public DateTime CheckInDate { get; set; } = DateTime.Now;

        [BindProperty]
        public DateTime CheckOutDate { get; set; } = DateTime.Now;

        [BindProperty]
        public int NumberOfGuests { get; set; }

        [BindProperty]
        public string RoomType { get; set; }

        

        public void OnGet()
        {
            //hotelService.InitializeRoomsList();
           // hotelService.BookRoom(101, DateTime.Today, DateTime.Today.AddDays(2), 2);
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            
            return Page();
        }
    }
}