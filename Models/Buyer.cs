using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Test1.Models
{
    public class Buyer
    {
        [Key]
        public int BuyerId { get; set; }

        [Required]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string PhoneNo { get; set; }
        public string Name { get; set; }
        public int MaxNo { get; set; }
    }
}
