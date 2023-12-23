
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ShipBu.Areas.Admin.Models;
using ShipBu.Data;
using SixLabors.ImageSharp.Formats.Jpeg;
using System.Linq;
using System.Security.Claims;

namespace ShipBu.Areas.Admin.Controllers;

[Area("Admin")]
public class BoxProductsController : Controller
{
    private readonly string entityName = "Koli Ürünleri";
    private readonly AppDbContext context;
    private readonly IConfiguration configuration;
    public BoxProductsController(
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
        var query = context.BoxProducts;

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
                    p.ProductCount,

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
    public async Task<IActionResult> Create(BoxProduct model)
    {


        model.UserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        context.BoxProducts.Add(model);
        context.SaveChanges();
        TempData["success"] = "Koli ekleme işlemi başarıyla tamamlanmıştır";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(Guid id)
    {
        var model = context.BoxProducts.Find(id);

        return View(model);


    }
   
    [HttpPost]
    public async Task<IActionResult> Edit(BoxProduct model)
    {
       
        context.BoxProducts.Add(model);
        context.BoxProducts.Update(model);
        context.SaveChanges();
        TempData["success"] = "Koli güncelleme işlemi başarıyla tamamlanmıştır";
        return RedirectToAction(nameof(Index));

    }

    public IActionResult Delete(Guid id)
    {
        var model = context.BoxProducts.Find(id);
        context.BoxProducts.Remove(model);
        context.SaveChanges();
        TempData["success"] = "Koli silme işlemi başarıyla tamamlanmıştır";
        return RedirectToAction(nameof(Index));
    }
}
