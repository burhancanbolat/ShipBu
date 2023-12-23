using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShipBu.Areas.Admin.Models;
using ShipBu.Data;
using System.Linq;
using System.Security.Claims;

namespace ShipBu.Areas.Admin.Controllers;
[Area("Admin")]
public class CountriesController : Controller
{   
    private readonly string entityName = "Ülke Bilgileri";
    private readonly AppDbContext context;
    private readonly IConfiguration configuration;
    public CountriesController(
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
        var query = context.Countries;

        var result = new DataTableResult
        {
            data = await query
               /* .Where(p => string.IsNullOrEmpty(p.State))  // Sadece state değeri boş olanları seç*/
                .Skip(parameters.Start)
                .Take(parameters.Length)
                .Select(p => new
                {
                    p.Id,
                    p.Name
                   
                  
                  
                    
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
    public async Task<IActionResult> Create(Country model)
    {


        model.UserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        context.Countries.Add(model);
        context.SaveChanges();
        TempData["success"] = "Lokasyon Bilgileri  ekleme işlemi başarıyla tamamlanmıştır";
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Edit(Guid id)
    {
        var model = context.Countries.Find(id);

        return View(model);


    }

    [HttpPost]
    public async Task<IActionResult> Edit(Country model)
    {
        model.UserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        context.Countries.Add(model);
        context.Countries.Update(model);
        context.SaveChanges();
        TempData["success"] = "Lokasyon Bilgileri  güncelleme işlemi başarıyla tamamlanmıştır";
        return RedirectToAction(nameof(Index));

    }

    public IActionResult Delete(Guid id)
    {
        var model = context.Countries.Find(id);
        context.Countries.Remove(model);
        context.SaveChanges();
        TempData["success"] = "Lokasyon Bilgileri  silme işlemi başarıyla tamamlanmıştır";
        return RedirectToAction(nameof(Index));
    }

}
