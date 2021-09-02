using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Test1.Models.EVouchers
{
    public class Evoucher
    {
        [Key]
        public int EvoucherId { get; set; }

        [Required]
        [Display(Name = "Title")]
        public string EvoucherTitle { get; set; }

        [Display(Name = "Description")]
        public string EvoucherDescription { get; set; }

        [Required]
        public DateTime ExpireDate { get; set; }

        public string Image { get; set; }

        public float Amount { get; set; }

        public bool IsActive { get; set; } = true;

    }
}
