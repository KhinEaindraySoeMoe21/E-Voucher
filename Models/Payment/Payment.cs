using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Test1.Models.Payments
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }

        [Required]
        [Display(Name = "Payment Type")]
        public string PaymentType { get; set; }

        public float? Discount { get; set; }

    }
}
