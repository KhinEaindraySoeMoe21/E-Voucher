using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Test1.Models.EVouchers;
using Test1.Models.Payments;

namespace Test1.Models
{
    public class PurchaseByUser
    {
        [Key]
        public int PurchaseByUserId { get; set; }
        public string State_name { get; set; }
        public int EvoucherId { get; set; }
        [ForeignKey("EvoucherId")]
        public virtual Evoucher Evoucher { get; set; }
        public int BuyerId { get; set; }
        [ForeignKey("BuyerId")]
        public virtual Buyer Buyer { get; set; }
        public int PaymentId { get; set; }
        [ForeignKey("PaymentId")]
        public virtual Payment Payment { get; set; }
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string? ReceiverPhone { get; set; }

        public float Amount { get; set; }

    }
}
