using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Test1.Data;
using Test1.Models.EVouchers;
using Test1.Models.ViewModels.EVouchers;
using Microsoft.AspNetCore.Hosting;

namespace Test1.Areas.EVouchers.Controllers
{
    [Area("EVouchers")]
    public class EvouchersController : Controller
    {
        private readonly ApplicationDbContext _context;
        public EvoucherViewModel evoucherVM { get; set; }
        public EvouchersController(ApplicationDbContext context)
        {
            _context = context;
            evoucherVM = new EvoucherViewModel()
            {
                Evoucher = new Evoucher(),
                EvoucherList = new List<Evoucher>(),
            };
        }
        [HttpGet]
        public async Task<IActionResult> EvoucherAIO(int EvoucherId, string btnActionName)
        {
            var totalEvouchers = _context.EVouchers.OrderBy(x => x.EvoucherTitle).ToList();
            var currentEvoucher = totalEvouchers.FirstOrDefault();
            if (EvoucherId == 0)
            {
                EvoucherViewModel evoucherVM = new EvoucherViewModel()
                {
                    EvoucherList = totalEvouchers,
                    Evoucher = new Evoucher(),
                    BtnActionName = "Create",
                };

                return View(evoucherVM);
            }
            else
            {
                Evoucher ev = new Evoucher();
                List<Evoucher> lstEvouchers = new List<Evoucher>();
                lstEvouchers = await _context.EVouchers.Where(a => a.EvoucherId == EvoucherId).ToListAsync();
                EvoucherViewModel evoucherVM = new EvoucherViewModel()
                {
                    EvoucherList = totalEvouchers,
                    Evoucher = lstEvouchers.FirstOrDefault(),
                    BtnActionName = btnActionName,
                };
                return View(evoucherVM);
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EvoucherAIO(EvoucherViewModel model)
        {
            //if (ModelState.IsValid)
            //{
                Evoucher ev = new Evoucher();
                var totalEV = await _context.EVouchers.ToListAsync();
                var ModuleNameExist = await _context.EVouchers.Where(x => x.EvoucherTitle == model.Evoucher.EvoucherTitle).FirstOrDefaultAsync();
                if (ModuleNameExist == null && model.BtnActionName != "Delete")
                {
                    if (model.BtnActionName == "Edit")
                    {
                        try
                        {
                            var dati = _context.EVouchers.Where(p => p.EvoucherId == model.EvoucherId).Single();
                            dati.EvoucherTitle = model.Evoucher.EvoucherTitle;
                            dati.EvoucherDescription = model.Evoucher.EvoucherDescription;
                            dati.ExpireDate = model.Evoucher.ExpireDate;
                            dati.Image = model.Evoucher.Image;
                            dati.Amount = model.Evoucher.Amount;
                            await _context.SaveChangesAsync();
                            return RedirectToAction(nameof(EvoucherAIO));
                        }
                        catch (DbUpdateConcurrencyException)
                        {
                            if (!ModuleExists(model.Evoucher.EvoucherId))
                            {
                                return NotFound();
                            }
                            else
                            {
                                throw;
                            }
                        }
                    }
                    else
                    {
                        ev.EvoucherTitle = model.Evoucher.EvoucherTitle;
                        ev.EvoucherDescription = model.Evoucher.EvoucherDescription;
                        ev.ExpireDate = model.Evoucher.ExpireDate;
                        ev.Image = model.Evoucher.Image;
                        ev.Amount = model.Evoucher.Amount;
                        await _context.EVouchers.AddAsync(ev);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(EvoucherAIO));
                    }
                }
                else
                {
                    if (model.BtnActionName == "Delete")
                    {
                        Evoucher evoucher = await _context.EVouchers
                            .FindAsync(model.EvoucherId);
                        _context.EVouchers.Remove(evoucher);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(EvoucherAIO));
                    }
                    else
                    {
                        try
                        {
                            ModelState.AddModelError("Evoucher.EvoucherTitle", "Voucher Title already exist");
                            return View(model);
                        }
                        catch (DbUpdateConcurrencyException)
                        {
                            if (!ModuleExists(model.Evoucher.EvoucherId))
                            {
                                return NotFound();
                            }
                            else
                            {
                                throw;
                            }
                        }
                    }
                }
            //}
            //else
            //{
            //    _context.EVouchers.Remove(model.Evoucher);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(EvoucherAIO));
            //}
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
