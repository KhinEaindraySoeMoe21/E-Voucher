using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Test1.Models.EVouchers;

namespace Test1.Models.ViewModels.EVouchers
{
    public class EvoucherViewModel
    {
        public EvoucherViewModel()
        {
            EvoucherList = new List<Evoucher>();
        }
        public Evoucher Evoucher { get; set; }
        public int? EvoucherId { get; set; }

        [Required]
        [Display(Name = "Title")]
        public string EvoucherTitle { get; set; }

        [Display(Name = "Description")]
        public string EvoucherDescription { get; set; }

        [Required]
        public DateTime ExpireDate { get; set; }

        public string Image { get; set; }

        public float Amount { get; set; }
        public string BtnActionName { get; set; }

        public List<Evoucher> EvoucherList { get; set; }
    }
}
