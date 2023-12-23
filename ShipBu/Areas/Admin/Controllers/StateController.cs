using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShipBu.Areas.Admin.Models;
using ShipBu.Data;
using System.Linq;
using System.Security.Claims;

namespace ShipBu.Areas.Admin.Controllers;
[Area("Admin")]
public class StateController : Controller
{
	private readonly string entityName = "Ülke Bilgileri";
	private readonly AppDbContext context;
	private readonly IConfiguration configuration;
	public StateController(
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
        var query = context.States;

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
					countryName=p.Country.Name,
				
              
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
        ViewBag.States = new SelectList(await context.Countries.OrderBy(p => p.Name).ToListAsync(), "Id", "Name");

        return View();
    }
    [HttpPost]
	public async Task<IActionResult> Create(State model)
	{


		
		context.States.Add(model);
		context.SaveChanges();
		TempData["success"] = "Ülke Bilgileri  ekleme işlemi başarıyla tamamlanmıştır";
		return RedirectToAction(nameof(Index));
	}
	public async Task<IActionResult> Edit(Guid id)
	{
        ViewBag.States = new SelectList(await context.Countries.OrderBy(p => p.Name).ToListAsync(), "Id", "Name");
        var model = context.States.Find(id);

		return View(model);


	}

	[HttpPost]
	public async Task<IActionResult> Edit(State model)
	{
		
		context.States.Add(model);
		context.States.Update(model);
		context.SaveChanges();
		TempData["success"] = "Ülke Bilgileri  güncelleme işlemi başarıyla tamamlanmıştır";
		return RedirectToAction(nameof(Index));

	}

	public IActionResult Delete(Guid id)
	{
		var model = context.States.Find(id);
		context.States.Remove(model);
		context.SaveChanges();
		TempData["success"] = "Ülke Bilgileri  silme işlemi başarıyla tamamlanmıştır";
		return RedirectToAction(nameof(Index));
	}

}
