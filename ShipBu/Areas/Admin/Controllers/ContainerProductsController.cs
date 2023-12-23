
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShipBu.Areas.Admin.Models;
using ShipBu.Data;
using System.Linq;
using System.Security.Claims;


namespace ShipBu.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContainerProductsController : Controller
    {

        private readonly string entityName = "Konteyner Ürünleri";
        private readonly AppDbContext context;
        private readonly IConfiguration configuration;
        public ContainerProductsController(
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
            var query = context.ContainerProducts;

            var result = new DataTableResult
            {
                data = await query
                    .Skip(parameters.Start)
                    .Take(parameters.Length)
                    .Select(p => new
                    {
                        p.Id,
                        p.Quantity,
                        p.Ton,
                      genreName=p.ContainerGenre.Genre,
                        p.TotalTon,


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
        public async Task<IActionResult> Create(ContainerProduct model)
        {



            context.ContainerProducts.Add(model);
            context.SaveChanges();
            TempData["success"] = "Konteyner ekleme işlemi başarıyla tamamlanmıştır";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var model = context.ContainerProducts.Find(id);

            return View(model);


        }

        [HttpPost]
        public async Task<IActionResult> Edit(ContainerProduct model)
        {

            context.ContainerProducts.Add(model);
            context.ContainerProducts.Update(model);
            context.SaveChanges();
            TempData["success"] = "Konteyner güncelleme işlemi başarıyla tamamlanmıştır";
            return RedirectToAction(nameof(Index));

        }

        public IActionResult Delete(Guid id)
        {
            var model = context.ContainerProducts.Find(id);
            context.ContainerProducts.Remove(model);
            context.SaveChanges();
            TempData["success"] = "Konteyner silme işlemi başarıyla tamamlanmıştır";
            return RedirectToAction(nameof(Index));
        }
    }
}

