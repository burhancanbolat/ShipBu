using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShipBu.Areas.Admin.Models;
using ShipBu.Data;
using System.Security.Claims;

namespace ShipBu.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class PriceEditsController : Controller
    {
        private readonly string entityName = "Fiyatlandırma";
        private readonly AppDbContext context;
        private readonly IConfiguration configuration;
        public PriceEditsController(
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
            var query = context.PriceEdits;

            var result = new DataTableResult
            {
                data = await query
                    /*  .Where(p => !string.IsNullOrEmpty(p.State))  // Sadece state değeri boş olmayanları seç*/
                    .Skip(parameters.Start)
                    .Take(parameters.Length)
                    .Select(p => new
                    {
                        p.Id,
                        
                        CountryName = p.Country.Name,
                        StateName = p.State.Name,
                        WareHouse=p.WareHouse.Name,
                        p.KG,
                        p.Price,
                      genreName=p.SendingGenre.Name



                    }).ToListAsync(),
                draw = parameters.Draw,
                recordsFiltered = await query.CountAsync(),
                recordsTotal = await query.CountAsync()
            };

            return Json(result);
        }

        public async Task<IActionResult> Create()
        {
            var country = context.Countries.ToList();
            var Genre = context.SendingGenres.ToList();


            var viewModel = new PriceEditModel
            {
                Country = country,
              sendingGenres=Genre


            };

            // View'e ViewModel'i gönder
            return View(viewModel);




        }

        [HttpGet]
        public IActionResult GetStates(Guid countryId)
        {
            // countryId parametresini kullanarak, seçilen ülkeye ait eyaletleri veritabanından çekin
            var states = context.States
                .Where(s => s.CountryId == countryId)
                .Select(p => new { p.Id, p.Name })
                .ToList();

            // Eyaletleri JSON formatında geri döndürün
            return Json(states);
        }
        [HttpGet]
        public IActionResult GetWare(Guid countryId)
        {
            // countryId parametresini kullanarak, seçilen ülkeye ait eyaletleri veritabanından çekin
            var WareHouses = context.WareHouses
                .Where(s => s.CountryId == countryId)
                .Select(p => new { p.Id, p.Name })
                .ToList();

            // Eyaletleri JSON formatında geri döndürün
            return Json(WareHouses);
        }
        public IActionResult GetWarehouses(Guid stateId)
        {
            // stateId parametresini kullanarak, seçilen eyaletin Amazon depolarını veritabanından çekin
            var warehouses = context.WareHouses
                .Where(w => w.StateId == stateId)
                .Select(warehouse => new { warehouse.Id, warehouse.Name })
                .ToList();

            // Depoları JSON formatında geri döndürün
            return Json(warehouses);
        }
        [HttpPost]
        public async Task<IActionResult> Create(PriceEditModel model)
        {
            var priceEdit = new PriceEdit
            {
                Price=model.Price,
                PrimaryKG=model.PrimaryKg,
                SecondaryKg=model.SecondaryKg,

                


                UserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!)

            };
            var selectedIdsCountry = model.SelectedIdsCountry;
            var selectedIdsState = model.SelectedIdsState;
            var SelectedIdsWareHouse = model.SelectedIdsWareHouse;
            var SelectedIdsGenre = model.SelectedIdsGenre;
            if (selectedIdsCountry != null && selectedIdsCountry.Any())
            {
                priceEdit.CountryId = selectedIdsCountry.First();
            }

            // Son olarak StateId'yi kaydet
            if (selectedIdsState != null && selectedIdsState.Any())
            {
                priceEdit.StateId = selectedIdsState.First();
            }
            if (SelectedIdsWareHouse != null && SelectedIdsWareHouse.Any())
            {
                priceEdit.WareHouseId = SelectedIdsWareHouse.First();
            }
            if (SelectedIdsGenre != null && SelectedIdsGenre.Any())
            {
                priceEdit.SendingGenreId = SelectedIdsGenre.First();
            }


            context.PriceEdits.Add(priceEdit);
            context.SaveChanges();
            TempData["success"] = "Amazon Depo  ekleme işlemi başarıyla tamamlanmıştır";
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(Guid id)
        {
            ViewBag.Country = new SelectList(await context.Countries.OrderBy(p => p.Name).ToListAsync(), "Id", "Name");
            ViewBag.State = new SelectList(await context.States.OrderBy(p => p.Name).ToListAsync(), "Id", "Name");
            ViewBag.WareHouse = new SelectList(await context.WareHouses.OrderBy(p => p.Name).ToListAsync(), "Id", "Name");
            ViewBag.SendingGenre = new SelectList(await context.SendingGenres.OrderBy(p => p.Name).ToListAsync(), "Id", "Name");

            var model = context.PriceEdits.Find(id);

            return View(model);


        }

        [HttpPost]
        public async Task<IActionResult> Edit(PriceEdit model)
        {
            model.UserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            context.PriceEdits.Add(model);
            context.PriceEdits.Update(model);
            context.SaveChanges();
            TempData["success"] = "Lokasyon Bilgileri  güncelleme işlemi başarıyla tamamlanmıştır";
            return RedirectToAction(nameof(Index));

        }

        public IActionResult Delete(Guid id)
        {
            var model = context.PriceEdits.Find(id);
            context.PriceEdits.Remove(model);
            context.SaveChanges();
            TempData["success"] = "Lokasyon Bilgileri  silme işlemi başarıyla tamamlanmıştır";
            return RedirectToAction(nameof(Index));
        }
    }
}
