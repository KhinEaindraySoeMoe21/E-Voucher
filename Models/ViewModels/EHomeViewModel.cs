using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Test1.Models.EVouchers;
using Test1.Models.Payments;

namespace Test1.Models.ViewModels
{
    public class EHomeViewModel
    {
        public EHomeViewModel()
        {
            EvoucherList = new List<Evoucher>();
            BuyerList = new List<Buyer>();
            PaymentList = new List<Payment>();
            PurchaseByUserList = new List<PurchaseByUser>();
        }
        public Evoucher Evoucher { get; set; }
        public List<Evoucher> EvoucherList { get; set; }
        public Payment Payment { get; set; }
        public List<Payment> PaymentList { get; set; }
        public Buyer Buyer { get; set; }
        public List<Buyer> BuyerList { get; set; }
        public PurchaseByUser PurchaseByUser { get; set; }
        public List<PurchaseByUser> PurchaseByUserList { get; set; }
        public int EvoucherId { get; set; }
        public int? PaymentId { get; set; }
        public int BuyerId { get; set; }
        public int PhoneNo { get; set; }
        public int Qty { get; set; }
        public float TotalCost { get; set; }
        public string BtnActionName { get; set; }

        
    }
}
