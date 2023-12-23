using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShipBu.Areas.Admin.Models;
using ShipBu.Data;
using System.Security.Claims;

namespace ShipBu.Areas.Admin.Controllers;
[Area("Admin")]
public class PackageprocessController : Controller
{
    private readonly string entityName = "ÖZELLİK EKLE";

    private readonly AppDbContext context;
    private readonly IConfiguration configuration;


    public PackageprocessController(
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
        var query = context.Packageprocesses;

        var result = new DataTableResult
        {
            data = await query
                .Skip(parameters.Start)
                .Take(parameters.Length)
                .Select(p => new
                {
                    p.Id,
                    p.Feature,
                    p.Description,
                    p.Price
                  

                   
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
    public async Task<IActionResult> Create(Packageprocess model)
    {

		model.UserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

		context.Packageprocesses.Add(model);
        context.SaveChanges();
        TempData["success"] = "Paket özelliği ekleme işlemi başarıyla tamamlanmıştır";
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Edit(Guid id)
    {
        var model = context.Packageprocesses.Find(id);

        return View(model);


    }

    [HttpPost]
    public async Task<IActionResult> Edit(Packageprocess model)
    {
        model.UserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        context.Packageprocesses.Add(model);
        context.Packageprocesses.Update(model);
        context.SaveChanges();
        TempData["success"] = "Paket özelliği güncelleme işlemi başarıyla tamamlanmıştır";
        return RedirectToAction(nameof(Index));

    }

    public IActionResult Delete(Guid id)
    {
        var model = context.Packageprocesses.Find(id);
        context.Packageprocesses.Remove(model);
        context.SaveChanges();
        TempData["success"] = "Paket özelliği silme işlemi başarıyla tamamlanmıştır";
        return RedirectToAction(nameof(Index));
    }
}
