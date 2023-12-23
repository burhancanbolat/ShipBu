using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShipBu.Areas.Admin.Models;
using ShipBu.Data;
using ShipBu.Models;
using System.ComponentModel;
using System.Linq;
using System.Security.Claims;
using System.Xml.Linq;

namespace ShipBu.Areas.Admin.Controllers;

[Area("Admin")]
public class WareHousesController : Controller
{
    private readonly string entityName = "Amazon Depo Bilgileri";
    private readonly AppDbContext context;
    private readonly IConfiguration configuration;
    public WareHousesController(
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
        var query = context.WareHouses;

        var result = new DataTableResult
        {
            data = await query
                /*  .Where(p => !string.IsNullOrEmpty(p.State))  // Sadece state değeri boş olmayanları seç*/
                .Skip(parameters.Start)
                .Take(parameters.Length)
                .Select(p => new
                {
                    p.Id,
                    p.Name,
                    CountryName=p.Country.Name,
                    StateName=p.State.Name,
                    
                    

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

        var viewModel = new WareHouseModel
        {
            Country = country
            

        };

        // View'e ViewModel'i gönder
        return View(viewModel);


        
        
    }
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
    [HttpPost]
    public async Task<IActionResult> Create(WareHouseModel model)
    {
        var wareHouse = new WareHouse
        {
            Name = model.Name,
             
           UserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!)

    };
        var selectedIdsCountry = model.SelectedIdsCountry;
        var selectedIdsState = model.SelectedIdsState;
        if (selectedIdsCountry != null && selectedIdsCountry.Any())
        {
            wareHouse.CountryId = selectedIdsCountry.First();
        }

        // Son olarak StateId'yi kaydet
        if (selectedIdsState != null && selectedIdsState.Any())
        {
            wareHouse.StateId = selectedIdsState.First();
        }

       
        context.WareHouses.Add(wareHouse);
        context.SaveChanges();
        TempData["success"] = "Amazon Depo  ekleme işlemi başarıyla tamamlanmıştır";
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Edit(Guid id)
    {
        ViewBag.Country = new SelectList(await context.Countries.OrderBy(p => p.Name).ToListAsync(), "Id", "Name");
        ViewBag.State = new SelectList(await context.States.OrderBy(p => p.Name).ToListAsync(), "Id", "Name");
       


        var model = context.WareHouses.Find(id);

        return View(model);


    }

    [HttpPost]
    public async Task<IActionResult> Edit(WareHouse model)
    {
        

        model.UserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        context.WareHouses.Add(model);
        context.WareHouses.Update(model);
        context.SaveChanges();
        TempData["success"] = "Amazon Depo  güncelleme işlemi başarıyla tamamlanmıştır";
        return RedirectToAction(nameof(Index));

    }

    public IActionResult Delete(Guid id)
    {
        var model = context.WareHouses.Find(id);
        context.WareHouses.Remove(model);
        context.SaveChanges();
        TempData["success"] = "Amazon Depo Bilgileri  silme işlemi başarıyla tamamlanmıştır";
        return RedirectToAction(nameof(Index));
    }



}