using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDrivenHotel.Data
{
    public class GuestModel
    {
        public int Id { get; set; } = 0;
        [Required]
        public string firstName { get; set; }
        [Required]
        public string lastName { get; set; }
        [Required]
        public string CustomerEmail { get; set; }
        
        public string? CustomerPhoneNumber { get; set; }

    }
}
