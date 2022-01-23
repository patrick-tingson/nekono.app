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
    public class CollectionReceiptsController : Controller
    {
        private readonly HttpClient httpClient = new HttpClient();
        private string API_COLLECTION_RECEIPT_TODAY = "v1/collectionreceipt/today";
        private string API_COLLECTION_RECEIPT_ALL = "v1/collectionreceipt/getall";
        private string API_COLLECTION_RECEIPT = "v1/collectionreceipt";
        private string API_COLLECTION_RECEIPT_ITEMS = "v1/inventory/collectionreceipt/";
        private readonly IOptions<NekonoAppConfig> nekonoAppConfig;

        public CollectionReceiptsController(IOptions<NekonoAppConfig> nekonoAppConfig)
        {
            this.nekonoAppConfig = nekonoAppConfig ??
              throw new ArgumentNullException(nameof(nekonoAppConfig));
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetByDate(string startDate, string endDate)
        {
            IEnumerable<CollectionReceiptDetails> collectionReceiptDetails = null;

            try
            {
                if (ModelState.IsValid)
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", User.GetAccessToken());

                    var authenticateResult = await httpClient.GetAsync($"{nekonoAppConfig.Value.NekonoAPI}{API_COLLECTION_RECEIPT}?startDate={startDate}&endDate={endDate}");

                    if (authenticateResult.IsSuccessStatusCode)
                    {
                        using (var content = authenticateResult.Content)
                        {
                            collectionReceiptDetails = JsonConvert.DeserializeObject<IEnumerable<CollectionReceiptDetails>>(await content.ReadAsStringAsync());
                        }
                    }
                    else if (authenticateResult.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        return RedirectToAction("Index", "Login");
                        //return Json(new { isRedirect = true, redirectToUrl = Url.Action("Index", "Login") });
                    }
                    else
                    {
                        ViewBag.Message = "Unable to get collection receipt master list. Please contact system admin.";
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                Console.WriteLine(ex.Message);
            }

            return Json(collectionReceiptDetails);
        }

        public async Task<IActionResult> GetToday()
        {
            IEnumerable<CollectionReceiptDetails> collectionReceiptDetails = null;

            try
            {
                if (ModelState.IsValid)
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", User.GetAccessToken());

                    var authenticateResult = await httpClient.GetAsync($"{nekonoAppConfig.Value.NekonoAPI}{API_COLLECTION_RECEIPT_TODAY}");

                    if (authenticateResult.IsSuccessStatusCode)
                    {
                        using (var content = authenticateResult.Content)
                        {
                            collectionReceiptDetails = JsonConvert.DeserializeObject<IEnumerable<CollectionReceiptDetails>>(await content.ReadAsStringAsync());
                        }
                    }
                    else if (authenticateResult.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        return this.Unauthorized();
                    }
                    else
                    {
                        ViewBag.Message = "Unable to get collection receipt master list. Please contact system admin.";
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                Console.WriteLine(ex.Message);
            }

            return Json(collectionReceiptDetails);
        }

        public async Task<IActionResult> GetAll()
        {
            IEnumerable<CollectionReceiptDetails> collectionReceiptDetails = null;

            try
            {
                if (ModelState.IsValid)
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", User.GetAccessToken());

                    var authenticateResult = await httpClient.GetAsync($"{nekonoAppConfig.Value.NekonoAPI}{API_COLLECTION_RECEIPT_ALL}");

                    if (authenticateResult.IsSuccessStatusCode)
                    {
                        using (var content = authenticateResult.Content)
                        {
                            collectionReceiptDetails = JsonConvert.DeserializeObject<IEnumerable<CollectionReceiptDetails>>(await content.ReadAsStringAsync());
                        }
                    }
                    else if (authenticateResult.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        return this.Unauthorized();
                    }
                    else
                    {
                        ViewBag.Message = "Unable to get collection receipt master list. Please contact system admin.";
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                Console.WriteLine(ex.Message);
            }

            return Json(collectionReceiptDetails);
        }

        public async Task<IActionResult> GetItems([FromQuery] string collectionReceipt)
        {
            IEnumerable<InventoryDetails> inventoryDetails = null;

            try
            {
                if (ModelState.IsValid)
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", User.GetAccessToken());

                    var authenticateResult = await httpClient.GetAsync($"{nekonoAppConfig.Value.NekonoAPI}{API_COLLECTION_RECEIPT_ITEMS}{collectionReceipt}");

                    if (authenticateResult.IsSuccessStatusCode)
                    {
                        using (var content = authenticateResult.Content)
                        {
                            inventoryDetails = JsonConvert.DeserializeObject<IEnumerable<InventoryDetails>>(await content.ReadAsStringAsync());
                        }
                    }
                    else if (authenticateResult.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        return this.Unauthorized();
                    }
                    else
                    {
                        ViewBag.Message = "Unable to get items for this collection receipt. Please contact system admin.";
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                Console.WriteLine(ex.Message);
            }

            return Json(inventoryDetails);
        }
    }
}
