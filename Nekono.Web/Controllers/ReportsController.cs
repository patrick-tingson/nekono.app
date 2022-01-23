using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Nekono.AA.Domain.Config;
using Nekono.AA.Domain.Model;
using Nekono.Web.Extensions;
using Newtonsoft.Json;

namespace Nekono.Web.Controllers
{
    public class ReportsController : Controller
    {
        private readonly HttpClient httpClient = new HttpClient();
        private string API_ITEM_MOVEMENTS = "v1/inventory";
        private string API_ITEM_SALES = "v1/inventory/salevoid";
        private readonly IOptions<NekonoAppConfig> nekonoAppConfig;

        public ReportsController(IOptions<NekonoAppConfig> nekonoAppConfig)
        {
            this.nekonoAppConfig = nekonoAppConfig ??
              throw new ArgumentNullException(nameof(nekonoAppConfig));
        }

        public IActionResult ItemMovements()
        {
            return View();
        }

        public IActionResult ItemSales()
        {
            return View();
        }

        public async Task<IActionResult> ItemMovementsByDate()
        {
            IEnumerable<InventoryDetails> itemDetails = null;

            try
            {
                if (ModelState.IsValid)
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", User.GetAccessToken());

                    var query = $"?startDate={DateTime.Now.Subtract(new TimeSpan(7,0,0,0)):yyyy/MM/dd}&endDate={DateTime.Now:yyyy/MM/dd}";

                    var result = await httpClient.GetAsync($"{nekonoAppConfig.Value.NekonoAPI}{API_ITEM_MOVEMENTS}{query}");

                    if (result.IsSuccessStatusCode)
                    {
                        using (var content = result.Content)
                        {
                            itemDetails = JsonConvert.DeserializeObject<IEnumerable<InventoryDetails>>(await content.ReadAsStringAsync());
                        }
                    }
                    else if (result.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        return this.Unauthorized();
                    }
                    else
                    {
                        ViewBag.Message = "Unable to get the report. Please contact system admin.";
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                Console.WriteLine(ex.Message);
            }

            return Json(itemDetails);
        }

        public async Task<IActionResult> ItemSalesByDate()
        {
            IEnumerable<InventoryDetails> itemDetails = null;

            try
            {
                if (ModelState.IsValid)
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", User.GetAccessToken());

                    var query = $"?startDate={DateTime.Now.Subtract(new TimeSpan(7, 0, 0, 0)):yyyy/MM/dd}&endDate={DateTime.Now:yyyy/MM/dd}";

                    var result = await httpClient.GetAsync($"{nekonoAppConfig.Value.NekonoAPI}{API_ITEM_SALES}{query}");

                    if (result.IsSuccessStatusCode)
                    {
                        using (var content = result.Content)
                        {
                            itemDetails = JsonConvert.DeserializeObject<IEnumerable<InventoryDetails>>(await content.ReadAsStringAsync());
                        }
                    }
                    else if (result.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        return this.Unauthorized();
                    }
                    else
                    {
                        ViewBag.Message = "Unable to get the report. Please contact system admin.";
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                Console.WriteLine(ex.Message);
            }

            return Json(itemDetails);
        }
    }
}
