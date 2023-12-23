using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShipBu.Areas.Admin.Models;
using ShipBu.Data;
using System.Security.Claims;

namespace ShipBu.Areas.Admin.Controllers;

[Area("Admin")]
public class PaymentsController : Controller
{
    private readonly string entityName = "Amazon Depo Bilgileri";
    private readonly AppDbContext context;
    private readonly IConfiguration configuration;
    public PaymentsController(
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
        var query = context.Payments
     .Where(p => p.locationInfo != null && p.locationInfo.BoxProductId != null);

        var result = new DataTableResult
        {
            data = await query
            /*  .Where(p => !string.IsNullOrEmpty(p.State))  // Sadece state değeri boş olmayanları seç*/
            .Skip(parameters.Start)
            .Take(parameters.Length)
            .Select(p => new
            {
                p.Id,
                UserName = p.locationInfo.CreaterUser.Name,

                sendingCountry = p.locationInfo.SendingCountry.Name,
                Country = p.locationInfo.Country.Name,
                State = p.locationInfo.state.Name ?? "Eyalet Seçilmedi",
                wareHouse = p.locationInfo.WareHouse.Name ?? "Depo Seçilmedi",
                totalDesi = p.locationInfo.BoxProduct.TotalDesi,
                totalWeight = p.locationInfo.BoxProduct.TotalWeight






            }).ToListAsync(),
            draw = parameters.Draw,
            recordsFiltered = await query.CountAsync(),
            recordsTotal = await query.CountAsync()
        };

        return Json(result);
    }




    public IActionResult FeatureDetail(Guid id)
    {
        var payment = context.Payments
            .Include(p => p.locationInfo)
            .FirstOrDefault(p => p.Id == id);

        if (payment == null || payment.locationInfo == null)
        {
            return NotFound();
        }

        var boxProductId = payment.locationInfo.BoxProductId;

        var features = context.ProductFeatures
            .Where(pf => pf.BoxProductId == boxProductId)
            .Select(p => new
            {
                p.Packageprocess.Feature,
                p.Packageprocess.Price
            })
            .ToList();

        var Country = context.Locationinfos
            .Where(p => p.BoxProductId == boxProductId)
            .Select(p => new { p.Country.Name })
            .FirstOrDefault();

        var State = context.Locationinfos
            .Where(p => p.BoxProductId == boxProductId && p.StateId != null)
            .Select(p => new { p.state.Name})
            .FirstOrDefault();

        var wareHouse = context.Locationinfos
            .Where(p => p.BoxProductId == boxProductId && p.WareHouseId != null)
            .Select(p => new { p.WareHouse.Name })
            .FirstOrDefault();

        var privateAdressPrice = context.CalculationVariables
            .Select(p => p.PrivateAdressPrice)
            .FirstOrDefault();

        var totalValue = context.BoxProducts
            .Where(p => p.Id == boxProductId)
            .Select(p => new { p.TotalDesi, p.TotalWeight, p.Quantity })
            .FirstOrDefault();

        var Variable = context.CalculationVariables
            .Select(p => p.Variable)
            .Select(p => p.Value)
            .Sum();
        var PrivateAdress = context.CalculationVariables
           .Select(p => p.PrivateAdressPrice)
           .Select(p => p.Value)
           .Sum();

        var Result = (totalValue.TotalDesi / Variable) * totalValue.Quantity;

        var featurePrices = features.Select(f => f.Price).ToList();
        var featureName = features.Select(f => f.Feature).ToList();
     //   var totalResult = (Country?.Price ?? 0) + (State?.Price ?? 0) + (wareHouse?.Price ?? PrivateAdress) + (featurePrices?.Sum() ?? 0);
		var users = context.Payments.Where(P=>P.Id== id)
			.Select(p => new AppUser
			{
				Name = p.CreaterUser.Name,
            
				Adress = p.CreaterUser.Adress,
				PhoneNumber = p.CreaterUser.PhoneNumber,
				Email = p.CreaterUser.Email
			})
			.ToList();



		if (Result > totalValue.TotalWeight)
        {
            var viewModel = new FeatureDetailViewModel
            {   Users=users,

			//	TotalPrice=totalResult,
                Features = featureName,
                TotalDesi = totalValue.TotalDesi,
                TotalWeight = totalValue.TotalWeight,
                Quantity = totalValue.Quantity,
                Variable = Variable,
                Result = Result,
                CountryName = Country.Name,
               
                StateName = State?.Name ?? "Eyalet Seçilmedi",
               
                wareHouseName = wareHouse?.Name ?? "Depo Seçilmedi (Adres Gönderim Ücreti)",
              
                FeaturePrices = featurePrices // Feature Prices ekleniyor
            };

            return View(viewModel);
        }
        else
        {
            var viewModel = new FeatureDetailViewModel
            {
				Users = users,
				//TotalPrice = totalResult,
                Features = featureName,
                TotalDesi = totalValue.TotalDesi,
                TotalWeight = totalValue.TotalWeight,
                Quantity = totalValue.Quantity,
                Variable = Variable,
                Result = totalValue.TotalWeight,
                CountryName = Country.Name,
               
                StateName = State?.Name ?? "Eyalet Seçilmedi",
               
                wareHouseName = wareHouse?.Name ?? "Depo Seçilmedi (Adres Gönderim Ücreti)",
               
                FeaturePrices = featurePrices // Feature Prices ekleniyor
            };

         

            // totalResult ile ilgili bir şeyler yapabilirsiniz.

            return View(viewModel);
        }
    }


}

