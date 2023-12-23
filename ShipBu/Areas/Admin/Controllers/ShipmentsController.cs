using ShipBu.Data;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp.Formats.Jpeg;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using ShipBu.Areas.Admin.Models;
using System.Linq;
using System.Globalization;

namespace ShipBu.Areas.Admin.Controllers
{
        [Area("Admin")]
        public class ShipmentsController : Controller
        {
            private readonly string entityName = "Sevkiyat";

            private readonly AppDbContext context;
            private readonly IConfiguration configuration;


            public ShipmentsController(
                AppDbContext context,
                IConfiguration configuration

                )
            {
                this.context = context;
                this.configuration = configuration;

            }

            public IActionResult Index()
            {
                return View();
            }
        public async Task<IActionResult> TableData(Guid? id, DataTableParameters parameters)
        {
            var query = context.Shipments;

            var result = new DataTableResult
            {
                data = await query
                    .Skip(parameters.Start)
                    .Take(parameters.Length)
                    .Select(p => new
                    {
                        p.Id,
                        p.ShipmentNo,
                        p.Date,
                        p.SenderCountry,
                        p.RecipientCountry,
                        p.Buyer,
                        p.Price,
                        p.TrackingId,
                        
                    }).ToListAsync(),
                draw = parameters.Draw,
                recordsFiltered = await query.CountAsync(),
                recordsTotal = await query.CountAsync()
            };

            return Json(result);
        }
        public async Task<IActionResult> Create()
            {

                return View();
            }
            [HttpPost]
            public async Task<IActionResult> Create(Shipment model)
            {
            model.Price = decimal.Parse(model.PriceText, CultureInfo.CreateSpecificCulture("tr-TR"));
            model.Date = DateTime.UtcNow;
            model.UserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
                context.Shipments.Add(model);
                context.SaveChanges();
                TempData["success"] = "Sevkiyat ekleme işlemi başarıyla tamamlanmıştır";
                return RedirectToAction(nameof(Index));
            }
            public async Task<IActionResult> Edit(Guid id)
            {
                var model = context.Shipments.Find(id);

                return View(model);

            }

            [HttpPost]
            public async Task<IActionResult> Edit(Shipment model)
            {
            model.Price = decimal.Parse(model.PriceText, CultureInfo.CreateSpecificCulture("tr-TR"));
            model.Date = DateTime.UtcNow;
            model.UserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
                context.Shipments.Update(model);
                context.SaveChanges();
                TempData["success"] = "Sevkiyat güncelleme işlemi başarıyla tamamlanmıştır";
                return RedirectToAction(nameof(Index));
            }
            public IActionResult Delete(Guid id)
            {
                var model = context.Shipments.Find(id);
                context.Shipments.Remove(model);
                context.SaveChanges();
                TempData["success"] = "Sevkiyat silme işlemi başarıyla tamamlanmıştır";
                return RedirectToAction(nameof(Index));
            }
        }
    }

