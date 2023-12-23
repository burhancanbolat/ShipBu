using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShipBu.Areas.Admin.Models;
using ShipBu.Data;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Claims;

namespace ShipBu.Areas.Admin.Controllers;
[Area("Admin")]
public class LocationinfosController : Controller
{
    private readonly string entityName = "Lokasyon Bilgileri";
    private readonly AppDbContext context;
    private readonly IConfiguration configuration;
    public LocationinfosController(
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
        var query = context.Locationinfos;

        var result = new DataTableResult
        {
            data = await query
                .Skip(parameters.Start)
                .Take(parameters.Length)
                .Select(p => new
                {
                    p.Id,
                    p.CommunicationName,
                    p.Adress,
                    p.TelephoneNumber,
                    p.BoxProductId,
                    CountryName=p.Country.Name,
                   

                }).ToListAsync(),
            draw = parameters.Draw,
            recordsFiltered = await query.CountAsync(),
            recordsTotal = await query.CountAsync()
        };

        return Json(result);
    }

    public async Task<IActionResult> Create()
    {
        ViewBag.Country = new SelectList(await context.Countries.OrderBy(p => p.Name).ToListAsync(), "Id", "Name");


        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(Locationinfo model)
    {


        model.UserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        context.Locationinfos.Add(model);
        context.SaveChanges();
        TempData["success"] = "Lokasyon Bilgileri  ekleme işlemi başarıyla tamamlanmıştır";
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Edit(Guid id)
    {
        var model = context.Locationinfos.Find(id);
        ViewBag.Country = new SelectList(await context.Countries.OrderBy(p => p.Name).ToListAsync(), "Id", "Name");

        return View(model);


    }
      


    [HttpPost]
    public async Task<IActionResult> Edit(Locationinfo model)
    {

        model.UserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        context.Locationinfos.Add(model);
        context.Locationinfos.Update(model);
        context.SaveChanges();
        TempData["success"] = "Lokasyon Bilgileri  güncelleme işlemi başarıyla tamamlanmıştır";
        return RedirectToAction(nameof(Index));

    }


    [HttpPost]
    public async Task<IActionResult> Success(Payment model)
    {

        model.UserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        context.Payments.Add(model);
        context.Payments.Update(model);
        context.SaveChanges();
        TempData["success"] = "sipariş   gönderme işlemi başarıyla tamamlanmıştır";
        return RedirectToAction(nameof(Index));

    }
    public IActionResult Delete(Guid id)
    {
        var model = context.Locationinfos.Find(id);
        context.Locationinfos.Remove(model);
        context.SaveChanges();
        TempData["success"] = "Lokasyon Bilgileri  silme işlemi başarıyla tamamlanmıştır";
        return RedirectToAction(nameof(Index));
    }
}
