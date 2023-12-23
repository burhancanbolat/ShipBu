using ShipBu.Data;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using ShipBu.Areas.Admin.Models;
using System.Linq;

namespace ShipBu.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PaletteProductsController : Controller
    {

        private readonly string entityName = "Palet Ürünleri";
        private readonly AppDbContext context;
        private readonly IConfiguration configuration;
        public PaletteProductsController(
     AppDbContext context,
     IConfiguration configuration

     )
        {
            this.context = context;
            this.configuration = configuration;

        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> TableData(Guid? id, DataTableParameters parameters)
        {
            var query = context.PaletteProducts;

            var result = new DataTableResult
            {
                data = await query
                    .Skip(parameters.Start)
                    .Take(parameters.Length)
                    .Select(p => new
                    {
                        p.Id,
                        p.Quantity,
                        p.Weight,
                        p.TotalWeight,
                        p.Width,
                        p.Height,
                        p.Length,
                   

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
        public async Task<IActionResult> Create(PaletteProduct model)
        {



            context.PaletteProducts.Add(model);
            context.SaveChanges();
            TempData["success"] = "Palet ekleme işlemi başarıyla tamamlanmıştır";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var model = context.PaletteProducts.Find(id);

            return View(model);


        }

        [HttpPost]
        public async Task<IActionResult> Edit(PaletteProduct model)
        {

            context.PaletteProducts.Add(model);
            context.PaletteProducts.Update(model);
            context.SaveChanges();
            TempData["success"] = "Palet güncelleme işlemi başarıyla tamamlanmıştır";
            return RedirectToAction(nameof(Index));

        }

        public IActionResult Delete(Guid id)
        {
            var model = context.PaletteProducts.Find(id);
            context.PaletteProducts.Remove(model);
            context.SaveChanges();
            TempData["success"] = "Palet silme işlemi başarıyla tamamlanmıştır";
            return RedirectToAction(nameof(Index));
        }
    }
}
