using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NETCore.MailKit.Core;
using ShipBu.Data;

using ShipBu.Models;
using ShipBu.ViewModels;
using System.Diagnostics;
using System.Security.Claims;
using System.Text.Json.Serialization;
using System.Text.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Linq;
using Microsoft.Extensions.Logging;
using SixLabors.ImageSharp.Formats.Jpeg;
using ShipBu.Areas.Admin.Models;
using Org.BouncyCastle.Bcpg.Sig;

namespace ShipBu.Controllers
{
    [Authorize(Roles = "Members,Administrators")]
    public class HomeController : Controller
    {



        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext context;

        public HomeController(
            ILogger<HomeController> logger,
            AppDbContext context
            )
        {
            _logger = logger;
            this.context = context;
        }

        [HttpGet]
        public IActionResult Features()
        {
            var packageProcesses = context.Packageprocesses.Select(p => new { p.Feature, p.Id, p.IsChecked }).ToList();
            return Json(packageProcesses);
        }
        public IActionResult Offer()
        {



            var packageProcesses = context.Packageprocesses.ToList();

            // ViewModel'i oluþtur ve PackageProcesses verilerini ViewModel'e ekle
            var viewModel = new PackageAndBoxViewModel
            {
                PackageProcesses = packageProcesses
                // Burada PackageProcesses, PackageAndBoxViewModel içindeki bir property olmalýdýr.
            };

            // View'e ViewModel'i gönder
            return View(viewModel);
        }

        [HttpPost("/home/offer")]
        public IActionResult Offer([FromBody] OfferSubmitModel submitModel)
        {
            if (submitModel == null || submitModel.Items == null)
            {
                return BadRequest("Gönderilen veri null veya eksik.");
            }

            foreach (var offerViewModel in submitModel.Items)
            {
                var boxProduct = new BoxProduct
                {
                    // OfferViewModel'den BoxProduct'a uygun þekilde alanlarý kopyala
                    Quantity = offerViewModel.Qty,
                    ProductCount = offerViewModel.ProductCount,
                    Weight = offerViewModel.Weight,
                    Width = offerViewModel.Width,
                    Height = offerViewModel.Height,
                    Length = offerViewModel.Length,
                    UserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!)
                };

                // BoxProduct'ý veritabanýna ekle
                context.BoxProducts.Add(boxProduct);
                var boxProductId = boxProduct.Id;

                // Seçilen iþlemleri iþle
                foreach (var process in submitModel.SelectedProcesses)
                {
                    var productFeature = new ProductFeature
                    {
                        BoxProductId = boxProductId,
                        PackageprocessId = process.Id,
                        Photo = process.Image,
                        UserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!)
                    };

                    context.ProductFeatures.Add(productFeature);
                }
            }

            context.SaveChanges(); // Veritabanýndaki deðiþiklikleri kaydet

            return Json(true);
        }



        //[HttpPost]
        //public async Task<IActionResult> offer(PackageAndBoxViewModel viewModel)
        //{
        //    if (viewModel.ProductFeature.PhotoFile is not null)
        //    {
        //        using var image = await Image.LoadAsync(viewModel.ProductFeature.PhotoFile.OpenReadStream());


        //        image.Mutate(p => p.Resize(new ResizeOptions
        //        {
        //            Size = new Size(200, 200),
        //            Mode = ResizeMode.Crop
        //        }));

        //        viewModel.ProductFeature.Photo = image.ToBase64String(JpegFormat.Instance);
        //    }


        //    // BoxProductModel'den yeni bir BoxProduct oluþturun ve kaydedin
        //    var boxProduct = new BoxProduct
        //    {
        //        Height = viewModel.Height,
        //        Weight = viewModel.Weight,
        //        Width = viewModel.Width,
        //        Quantity = viewModel.Quantity,
        //        Length = viewModel.Length,
        //        ProductCount = viewModel.ProductCount,
        //        UserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!)

        //    };

        //    await context.BoxProducts.AddAsync(boxProduct);
        //    await context.SaveChangesAsync();

        //    // Seçilen iþlemlerin ID'leri
        //    var selectedProcessIds = viewModel.SelectedProcessIds;

        //    // Her bir iþlem ID'si için bir ProductFeature oluþtur ve kaydet
        //    if (selectedProcessIds != null && selectedProcessIds.Any())
        //    {
        //        foreach (var processId in selectedProcessIds)
        //        {
        //            var productFeature = new ProductFeature
        //            {
        //                BoxProductId = boxProduct.Id,
        //                PackageprocessId = processId,



        //            };

        //            await context.ProductFeatures.AddAsync(productFeature);

        //        }

        //        await context.SaveChangesAsync();
        //    }

        //    else if (viewModel.ProductFeature.Photo != null)
        //    {
        //        if (selectedProcessIds != null && selectedProcessIds.Any())
        //        {
        //            foreach (var processId in selectedProcessIds)

        //            {
        //                using var image = await Image.LoadAsync(viewModel.ProductFeature.PhotoFile.OpenReadStream());


        //                image.Mutate(p => p.Resize(new ResizeOptions
        //                {
        //                    Size = new Size(200, 200),
        //                    Mode = ResizeMode.Crop
        //                }));
        //                viewModel.ProductFeature.Photo = image.ToBase64String(JpegFormat.Instance);

        //                // Dosya yükleme iþlemi
        //                var productFeature = new ProductFeature
        //                { 
        //                    BoxProductId = boxProduct.Id,
        //                    Photo = viewModel.ProductFeature.Photo,

        //                };

        //                await context.ProductFeatures.AddAsync(productFeature);
        //            }

        //            await context.SaveChangesAsync();

        //        }
        //    }

        //    var productFeatureId = context.ProductFeatures
        //     .Where(pf => pf.BoxProductId == boxProduct.Id)
        //     .Select(pf => pf.Id)
        //     .FirstOrDefault();

        //    return RedirectToAction("LocationInfo", "Home", new { boxid = boxProduct.Id, productfeatureid = productFeatureId });
        //}

        public IActionResult offerPalette()
        {



            var packageProcesses = context.Packageprocesses.ToList();

            // ViewModel'i oluþtur ve PackageProcesses verilerini ViewModel'e ekle
            var viewModel = new PackageAndBoxViewModel
            {
                PackageProcesses = packageProcesses
                // Burada PackageProcesses, PackageAndBoxViewModel içindeki bir property olmalýdýr.
            };

            // View'e ViewModel'i gönder
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> offerPalette(PackageAndBoxViewModel viewModel)
        {
            // BoxProductModel'den yeni bir BoxProduct oluþturun ve kaydedin
            var paletteproduct = new PaletteProduct
            {
                Height = viewModel.PaletteProduct.Height,
                Weight = viewModel.PaletteProduct.Weight,
                Width = viewModel.PaletteProduct.Width,
                Quantity = viewModel.PaletteProduct.Quantity,
                Length = viewModel.PaletteProduct.Length,

                UserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!)

            };

            await context.PaletteProducts.AddAsync(paletteproduct);
            await context.SaveChangesAsync();


            // Seçilen iþlemlerin ID'leri
            var selectedProcessIds = viewModel.SelectedProcessIds;

            // Her bir iþlem ID'si için bir ProductFeature oluþtur ve kaydet
            if (selectedProcessIds != null && selectedProcessIds.Any())
            {
                foreach (var processId in selectedProcessIds)
                {
                    var productFeature = new ProductFeature
                    {
                        PaletteProductId = paletteproduct.Id,
                        PackageprocessId = processId
                    };

                    await context.ProductFeatures.AddAsync(productFeature);
                }

                await context.SaveChangesAsync();

            }
            var productFeaturePaletteId = context.ProductFeatures
          .Where(pf => pf.PaletteProductId == paletteproduct.Id)
          .Select(pf => pf.Id)
          .FirstOrDefault();

            return RedirectToAction("LocationInfo", "Home", new { paletteid = paletteproduct.Id, productfeaturepaletteid = productFeaturePaletteId });
        }
        public IActionResult offerContainer()
        {

            var containerGenre = context.ContainerGenres.ToList();

            var packageProcesses = context.Packageprocesses.ToList();

            // ViewModel'i oluþtur ve PackageProcesses verilerini ViewModel'e ekle
            var viewModel = new PackageAndBoxViewModel
            {
                ContainerGenre = containerGenre,
                PackageProcesses = packageProcesses
                // Burada PackageProcesses, PackageAndBoxViewModel içindeki bir property olmalýdýr.
            };

            // View'e ViewModel'i gönder
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> offerContainer(PackageAndBoxViewModel viewModel)
        {
            // BoxProductModel'den yeni bir BoxProduct oluþturun ve kaydedin
            var containerproduct = new ContainerProduct
            {

                Quantity = viewModel.ContainerProduct.Quantity,
                Ton = viewModel.ContainerProduct.Ton,
                ContainerGenreId = viewModel.SelectedGenreIds.First(),
                UserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!)

            };

            await context.ContainerProducts.AddAsync(containerproduct);
            await context.SaveChangesAsync();


            // Seçilen iþlemlerin ID'leri
            var selectedProcessIds = viewModel.SelectedProcessIds;
            var selectedGenreIds = viewModel.SelectedGenreIds;
            if (selectedGenreIds != null && selectedGenreIds.Any())
            {
                containerproduct.ContainerGenreId = selectedGenreIds.First(); // Sadece ilk seçileni al
            }
            // Her bir iþlem ID'si için bir ProductFeature oluþtur ve kaydet
            if (selectedProcessIds != null && selectedProcessIds.Any())
            {
                foreach (var processId in selectedProcessIds)
                {
                    var productFeature = new ProductFeature
                    {
                        ContainerProductId = containerproduct.Id,
                        PackageprocessId = processId
                    };

                    await context.ProductFeatures.AddAsync(productFeature);
                }

                await context.SaveChangesAsync();
            }

            return RedirectToAction("LocationInfo", "Home", new { containerid = containerproduct.Id });
        }



        [HttpGet]
        public async Task<IActionResult> LocationInfo(Guid? boxid, Guid? containerid, Guid? paletteid, Guid productfeatureid, Guid productFeaturePaletteId)
        {

            var sendingCountry = context.SendingCountries.ToList();
            var country = context.Countries.ToList();
            var packages = context.Packageprocesses.Where(p => p.IsCheckedWare).ToList();


            var box = await context.BoxProducts.SingleOrDefaultAsync(p => p.Id == boxid);
            var palette = await context.PaletteProducts.SingleOrDefaultAsync(p => p.Id == paletteid);
            var container = await context.ContainerProducts.SingleOrDefaultAsync(p => p.Id == containerid);
            var PfId = await context.ProductFeatures.SingleOrDefaultAsync(p => p.Id == productfeatureid);
            var PfPaletteId = await context.ProductFeatures.SingleOrDefaultAsync(p => p.Id == productFeaturePaletteId);


            var viewModel = new LocationInfoModel
            {
                Country = country,
                SendingCountry = sendingCountry,
                BoxId = box?.Id,
                PaletteId = palette?.Id,
                ContainerId = container?.Id,
                Packageprocesses = packages,
                PFId = PfId?.Id,
                PFPaletteId = PfPaletteId?.Id



            };

            // View'e ViewModel'i gönder
            return View(viewModel);
        }
        // HomeController veya uygun bir controller içinde


        [HttpPost]
        public async Task<IActionResult> LocationInfo(LocationInfoModel viewModel)
        {
            Guid? boxId = viewModel.BoxId;
            Guid? paletteId = viewModel.PaletteId;
            Guid? containerId = viewModel.ContainerId;
            Guid? PFId = viewModel.PFId;
            Guid? PFPaletteId = viewModel.PFPaletteId;


            var locationinfo = new Locationinfo
            {
                TelephoneNumber = viewModel.locationInfo.TelephoneNumber,
                Adress = viewModel.locationInfo.Adress,
                CommunicationName = viewModel.locationInfo.CommunicationName,
                UserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!),
                BoxProductId = boxId ?? null,
                ContainerProductId = containerId ?? null,
                PaletteProductId = paletteId ?? null,
                ProductFeatureId = PFId ?? PFPaletteId ?? null,






            };

            var selectedIds = viewModel.SelectedIds;
            var selectedIdsCountry = viewModel.SelectedIdsCountry;
            var selectedIdsState = viewModel.SelectedIdsState;
            var selectedIdsWareHouse = viewModel.SelectedIdsWareHouse;

            // Ýlk önce SendingCountryId'yi kaydet
            if (selectedIds != null && selectedIds.Any())
            {
                locationinfo.SendingCountryId = selectedIds.First(); // Sadece ilk seçileni al
            }

            // Sonra CountryId'yi kaydet
            if (selectedIdsCountry != null && selectedIdsCountry.Any())
            {
                locationinfo.CountryId = selectedIdsCountry.First();
            }

            // Son olarak StateId'yi kaydet
            if (selectedIdsState != null && selectedIdsState.Any())
            {
                locationinfo.StateId = selectedIdsState.First();
            }
            if (selectedIdsWareHouse != null && selectedIdsWareHouse.Any())
            {
                locationinfo.WareHouseId = selectedIdsWareHouse.First();
            }


            // Locationinfos tablosuna yeni bilgileri ekle
            await context.Locationinfos.AddAsync(locationinfo);

            Guid locationInfoId = locationinfo.Id;


            Payment tmodel = new Payment
            {
                // Diðer payment özelliklerini gerekirse burada ayarlayabilirsiniz
                LocationInfoId = locationInfoId,
                ProductFeatureId = PFId ?? PFPaletteId ?? null,

                // Diðer özellikler
                UserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!)

            };
            context.Payments.Add(tmodel);
            await context.SaveChangesAsync();

            // Eðer ModelState geçerli deðilse, ayný view'e doðrulama hatalarý ile dön
            return RedirectToAction("Payment", "Home", new { locationInfoId = locationinfo.Id });
        }


        public async Task<IActionResult> Payment(Guid locationInfoId)
        {
            // Gönderi türlerini sorgula
            var sendingGenres = context.SendingGenres.ToList();

            // Ödeme bilgisini sorgula ve gerekli alanlarý seç
            var paymentInfo = context.Payments
                .Where(p => p.LocationInfoId == locationInfoId)
                .Select(p => new { p.locationInfo.Country, p.locationInfo.state, p.locationInfo.WareHouse }).FirstOrDefault();
            var features = context.Payments
            .Where(pf => pf.LocationInfoId == locationInfoId)
            .Select(p => new
            {
                p.locationInfo.ProductFeature.Packageprocess.Feature,
                p.locationInfo.ProductFeature.Packageprocess.Price,
            })
            .ToList();
            var featurePrices = features.Select(f => f.Price).ToList();
            var featureName = features.Select(f => f.Feature).ToList();
            // ViewModel oluþtur
            var viewModel = new FeatureDetailViewModel
            {
                SendingGenre = sendingGenres,
                CountryName = paymentInfo.Country?.Name,
                StateName = paymentInfo.state?.Name,
                wareHouseName = paymentInfo.WareHouse?.Name,
                FeaturePrices = featurePrices,
                Features = featureName


            };

            // View'e ViewModel'i gönder
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Payment(Payment model)
        {



            return View();
        }





        [HttpGet]
        public IActionResult GetStates(Guid countryId)
        {
            // countryId parametresini kullanarak, seçilen ülkeye ait eyaletleri veritabanýndan çekin
            var states = context.States
                .Where(s => s.CountryId == countryId)
                .Select(p => new { p.Id, p.Name })
                .ToList();

            // Eyaletleri JSON formatýnda geri döndürün
            return Json(states);
        }
        [HttpGet]
        public IActionResult GetWare(Guid countryId)
        {
            // countryId parametresini kullanarak, seçilen ülkeye ait eyaletleri veritabanýndan çekin
            var WareHouses = context.WareHouses
                .Where(s => s.CountryId == countryId)
                .Select(p => new { p.Id, p.Name })
                .ToList();

            // Eyaletleri JSON formatýnda geri döndürün
            return Json(WareHouses);
        }
        public IActionResult GetWarehouses(Guid stateId)
        {
            // stateId parametresini kullanarak, seçilen eyaletin Amazon depolarýný veritabanýndan çekin
            var warehouses = context.WareHouses
                .Where(w => w.StateId == stateId)
                .Select(warehouse => new { warehouse.Id, warehouse.Name })
                .ToList();

            // Depolarý JSON formatýnda geri döndürün
            return Json(warehouses);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

