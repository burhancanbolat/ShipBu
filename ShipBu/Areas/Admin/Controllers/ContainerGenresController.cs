using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShipBu.Areas.Admin.Models;
using ShipBu.Data;
using System.Security.Claims;

namespace ShipBu.Areas.Admin.Controllers;
[Area("Admin")]
public class ContainerGenresController : Controller
{
    private readonly string entityName = "Konteyner Türü Bilgileri";
    private readonly AppDbContext context;
    private readonly IConfiguration configuration;
    public ContainerGenresController(
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
        var query = context.ContainerGenres;

        var result = new DataTableResult
        {
            data = await query
                /*  .Where(p => !string.IsNullOrEmpty(p.State))  // Sadece state değeri boş olmayanları seç*/
                .Skip(parameters.Start)
                .Take(parameters.Length)
                .Select(p => new
                {
                    p.Id,
                    p.Genre,
                   


                }).ToListAsync(),
            draw = parameters.Draw,
            recordsFiltered = await query.CountAsync(),
            recordsTotal = await query.CountAsync()
        };

        return Json(result);
    }

    public async Task<IActionResult> Create()
    {


        // Assuming you have a context named "context" for accessing the database
        ViewBag.ContainerGenres = new SelectList(await context.ContainerGenres.OrderBy(p => p.Genre).ToListAsync(), "Id", "Name");

        return View();

    }
    [HttpPost]
    public async Task<IActionResult> Create(ContainerGenre model)
    {


        model.UserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        context.ContainerGenres.Add(model);
        context.SaveChanges();
        TempData["success"] = "Konteyner Türü  ekleme işlemi başarıyla tamamlanmıştır";
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Edit(Guid id)
    {
        var model = context.ContainerGenres.Find(id);

        return View(model);


    }

    [HttpPost]
    public async Task<IActionResult> Edit(ContainerGenre model)
    {
        model.UserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        context.ContainerGenres.Add(model);
        context.ContainerGenres.Update(model);
        context.SaveChanges();
        TempData["success"] = "Konteyner Türü  güncelleme işlemi başarıyla tamamlanmıştır";
        return RedirectToAction(nameof(Index));

    }

    public IActionResult Delete(Guid id)
    {
        var model = context.ContainerGenres.Find(id);
        context.ContainerGenres.Remove(model);
        context.SaveChanges();
        TempData["success"] = "Konteyner Türü Bilgileri  silme işlemi başarıyla tamamlanmıştır";
        return RedirectToAction(nameof(Index));
    }



}