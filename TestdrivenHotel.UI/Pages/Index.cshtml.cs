﻿using Microsoft.AspNetCore.Mvc;
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
        public string? RoomType { get; set; }


        public void OnGet()
        {
            
            
            
        }


        public IActionResult OnPost(DateTime CheckinDate, DateTime CheckoutDate, int NumberOfGuests, string? RoomType)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            return RedirectToPage("/Explore", new { CheckinDate, CheckoutDate, NumberOfGuests, RoomType });// Redirect to the  page with the search parameters


        }
    }
}