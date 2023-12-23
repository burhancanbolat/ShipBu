﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShipBu.Areas.Admin.Models;
using ShipBu.Data;
using System.Linq;
using System.Security.Claims;

namespace ShipBu.Areas.Admin.Controllers;
[Area("Admin")]
public class SendingGenresController : Controller
{
    private readonly string entityName = "Ülke Bilgileri";
    private readonly AppDbContext context;
    private readonly IConfiguration configuration;
    public SendingGenresController(
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
        var query = context.SendingGenres;

        var result = new DataTableResult
        {
            data = await query
                /* .Where(p => string.IsNullOrEmpty(p.State))  // Sadece state değeri boş olanları seç*/
                .Skip(parameters.Start)
                .Take(parameters.Length)
                .Select(p => new
                {
                    p.Id,
                    p.Name,
                    p.DeliveryTıme,
                    dateTime=DateTime.UtcNow   





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
    public async Task<IActionResult> Create(SendingGenre model)
    {


        model.UserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        context.SendingGenres.Add(model);
        context.SaveChanges();
        TempData["success"] = "Lokasyon Bilgileri  ekleme işlemi başarıyla tamamlanmıştır";
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Edit(Guid id)
    {
        var model = context.SendingGenres.Find(id);

        return View(model);


    }

    [HttpPost]
    public async Task<IActionResult> Edit(SendingGenre model)
    {
        model.UserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        context.SendingGenres.Add(model);
        context.SendingGenres.Update(model);
        context.SaveChanges();
        TempData["success"] = "Lokasyon Bilgileri  güncelleme işlemi başarıyla tamamlanmıştır";
        return RedirectToAction(nameof(Index));

    }

    public IActionResult Delete(Guid id)
    {
        var model = context.SendingGenres.Find(id);
        context.SendingGenres.Remove(model);
        context.SaveChanges();
        TempData["success"] = "Lokasyon Bilgileri  silme işlemi başarıyla tamamlanmıştır";
        return RedirectToAction(nameof(Index));
    }

}
