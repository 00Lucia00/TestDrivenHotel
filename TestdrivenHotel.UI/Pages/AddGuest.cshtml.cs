using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TestDrivenHotel.Data;
using TestDrivenHotel.Logic;

namespace TestdrivenHotel.UI.Pages
{
    public class AddGuestModel : PageModel
    {
        HotelService service { get; set; }

        public GuestModel? Guest { get; set; }

        

        public void OnGet()
        {
        }
       

        public IActionResult OnPost(string firstName, string lastName, string email)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Guest = service.RegisterGuest(firstName, lastName, email);
            int id = Guest.Id;
            return RedirectToPage("/booking", new { id });
        }
    }
}
