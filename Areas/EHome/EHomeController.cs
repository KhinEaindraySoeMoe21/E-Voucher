using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Test1.Data;
using Test1.Models;
using Test1.Models.EVouchers;
using Test1.Models.Payments;
using Test1.Models.ViewModels;

namespace Test1.Areas.EHome
{
    [Area("EHome")]
    public class EHomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public EHomeViewModel ehomeVM { get; set; }
        public EHomeController(ApplicationDbContext context)
        {
            _context = context;
            ehomeVM = new EHomeViewModel()
            {
                Evoucher = new Evoucher(),
                EvoucherList = new List<Evoucher>(),
                Payment = new Payment(),
                PaymentList = new List<Payment>(),
                Buyer = new Buyer(),
                BuyerList = new List<Buyer>(),
            };
        }
        [HttpGet]
        public async Task<IActionResult> EHomeIndex()
        {
            var totalEvouchers = _context.EVouchers.OrderBy(x => x.EvoucherTitle).ToList();
            var totalBuyers = _context.Buyers.ToList();
            var totalPayments = _context.Payments.ToList();
            var totalPBUs = _context.PurchaseByUsers.ToList();
            var currentEvoucher = totalEvouchers.FirstOrDefault();
            
                EHomeViewModel ehVM = new EHomeViewModel()
                {
                    EvoucherList = totalEvouchers,
                    Evoucher = new Evoucher(),
                    PaymentList = totalPayments,
                    Payment = new Payment(),
                    BuyerList = totalBuyers,
                    Buyer = new Buyer(),
                    PurchaseByUser = new PurchaseByUser(),
                    PurchaseByUserList = totalPBUs,
                    BtnActionName = "Create",
                };

            return View(ehVM);

        }
        [HttpGet]
        public async Task<IActionResult> EHomePurchase(int EvoucherId)
        {
            var totalEvouchers = _context.EVouchers.OrderBy(x => x.EvoucherTitle).Where(x => x.IsActive == true).ToList();
            var totalBuyers = _context.Buyers.ToList();
            var totalPayments = _context.Payments.ToList();
            var currentEvoucher = totalEvouchers.FirstOrDefault();
            if (EvoucherId == 0)
            {
                EHomeViewModel ehVM = new EHomeViewModel()
                {
                    EvoucherList = totalEvouchers,
                    Evoucher = new Evoucher(),
                    PaymentList = totalPayments,
                    Payment = new Payment(),
                    BuyerList = totalBuyers,
                    Buyer = new Buyer(),
                    BtnActionName = "Create",
                };

                return View(ehVM);
            }
            else
            {
                Evoucher ev = new Evoucher();
                List<Evoucher> lstEvouchers = new List<Evoucher>();
                Payment pm = new Payment();
                List<Payment> lstPayments = new List<Payment>();
                Buyer by = new Buyer();
                List<Buyer> lstBuyers = new List<Buyer>();
                lstEvouchers = _context.EVouchers.Where(a => a.EvoucherId == EvoucherId).ToList();
                EHomeViewModel ehVM = new EHomeViewModel()
                {
                    EvoucherList = totalEvouchers,
                    Evoucher = lstEvouchers.FirstOrDefault(),
                    PaymentList = totalPayments,
                    Payment = new Payment(),
                    BuyerList = totalBuyers,
                    Buyer = new Buyer(),
                    BtnActionName="",
                };
                return View(ehVM);
            }

        }
        [HttpGet]
        public async Task<IActionResult> PurchaseHistory(int BuyerId)
        {
            var totalEvouchers = _context.EVouchers.OrderBy(x => x.EvoucherTitle).ToList();
            var totalBuyers = _context.Buyers.ToList();
            var totalPayments = _context.Payments.ToList();
            var totalPBUs = _context.PurchaseByUsers.Where(x=>x.BuyerId==BuyerId).ToList();
            var currentBuyer = totalPBUs.FirstOrDefault();
            var currentEvoucher = totalEvouchers.FirstOrDefault();

            EHomeViewModel ehVM = new EHomeViewModel()
            {
                EvoucherList = totalEvouchers,
                Evoucher = new Evoucher(),
                PaymentList = totalPayments,
                Payment = new Payment(),
                BuyerList = totalBuyers,
                Buyer = new Buyer(),
                PurchaseByUserList = totalPBUs,
                PurchaseByUser = currentBuyer,

                BtnActionName = "Create",
            };

            return View(ehVM);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EHomePurchase(EHomeViewModel model)
        {
            PurchaseByUser pbu = new PurchaseByUser();
            pbu.EvoucherId = model.EvoucherId;
            pbu.PaymentId = 1;
            pbu.BuyerId = model.Buyer.BuyerId;
            pbu.Amount = 0000;
            await _context.PurchaseByUsers.AddAsync(pbu);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(PurchaseHistory), new { BuyerId = model.BuyerId });
        }
        [HttpPost]
        private bool ModuleNameExists(string EvoucherTitle)
        {
            return _context.EVouchers.Any(e => e.EvoucherTitle == EvoucherTitle);

        }
        private bool ModuleExists(int id)
        {
            return _context.EVouchers.Any(e => e.EvoucherId == id);
        }
    }
}
