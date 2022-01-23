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
using Nekono.Web.Config;
using Nekono.Web.Extensions;
using Nekono.Web.Models;
using Newtonsoft.Json;

namespace Nekono.Web.Controllers
{
    public class ItemsController : Controller
    {
        private readonly HttpClient httpClient = new HttpClient();
        private string API_NEW_ITEM = "v1/item/new";
        private string API_UPDATE_ITEM = "v1/item/update";
        private string API_DELETE_ITEM = "v1/item/delete";
        private string API_INVENTORY_NEW = "v1/inventory/new";
        private string API_ITEM_INVENTORY = "v1/inventory/item";
        private string API_ITEM = "v1/item";
        private readonly IOptions<NekonoAppConfig> nekonoAppConfig;

        public ItemsController(IOptions<NekonoAppConfig> nekonoAppConfig)
        {
            this.nekonoAppConfig = nekonoAppConfig ??
              throw new ArgumentNullException(nameof(nekonoAppConfig));
        }

        public async Task<IActionResult> Index(string message)
        {
            //try
            //{
            //    if (ModelState.IsValid)
            //    {
            //        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", User.GetAccessToken());

            //        var result = await httpClient.GetAsync($"{nekonoAppConfig.Value.NekonoAPI}{API_ITEM}");

            //        if (result.IsSuccessStatusCode)
            //        {
            //            using (var content = result.Content)
            //            {
            //                ViewData["ItemsList"] = JsonConvert.DeserializeObject<IEnumerable<ItemDetails>>(await content.ReadAsStringAsync());
            //            }
            //        }
            //        else if (result.StatusCode == HttpStatusCode.Unauthorized)
            //        {
            //            return RedirectToAction("Index", "Login");
            //        }
            //        else
            //        {
            //            ViewData["Message"] = "Unable to get items master list. Please contact system admin.";
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    ViewData["Message"] = ex.Message;
            //}

            if (message != null)
            {
                ViewData["Message"] = message;
            }

            return View(new ItemsMultipleModel());
        }

        public async Task<IActionResult> PrintLabel()
        {
            return View();
        }

        public async Task<IActionResult> GetAll()
        {
            IEnumerable<ItemDetails> itemDetails = null;

            try
            {
                if (ModelState.IsValid)
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", User.GetAccessToken());

                    var result = await httpClient.GetAsync($"{nekonoAppConfig.Value.NekonoAPI}{API_ITEM}");

                    if (result.IsSuccessStatusCode)
                    {
                        using (var content = result.Content)
                        {
                            itemDetails = JsonConvert.DeserializeObject<IEnumerable<ItemDetails>>(await content.ReadAsStringAsync());
                        }
                    }
                    else if (result.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        return this.Unauthorized();
                        //return RedirectToAction("Index", "Login");
                        //return Json(new { isRedirect = true, redirectToUrl = Url.Action("Index", "Login") });
                    }
                    else
                    {
                        ViewBag.Message = "Unable to get items master list. Please contact system admin.";
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

        public async Task<IActionResult> New(string message)
        {
            if (message != null)
            {
                ViewData["Message"] = message;
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> New(ItemDetails itemDetails)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", User.GetAccessToken());

                    var result = await httpClient.PostAsync($"{nekonoAppConfig.Value.NekonoAPI}{API_NEW_ITEM}", itemDetails, JsonConfig.PostFormatter());

                    if (result.IsSuccessStatusCode)
                    {
                        return Redirect($"{Url.Action("New", "Items")}?message=SUCCESS");
                    }
                    else if (result.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        //return this.Unauthorized();
                        return RedirectToAction("Index", "Login");
                    }
                    else
                    {
                        ViewBag.Message = "Unable to save item. Please contact system admin.";
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                Console.WriteLine(ex.Message);
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Update(ItemDetails itemDetails)
        {
            var message = "";

            try
            {
                if (ModelState.IsValid)
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", User.GetAccessToken());

                    var result = await httpClient.PostAsync($"{nekonoAppConfig.Value.NekonoAPI}{API_UPDATE_ITEM}", itemDetails, JsonConfig.PostFormatter());

                    if (result.IsSuccessStatusCode)
                    {
                        message = "SUCCESS1";
                    }
                    else if (result.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        //return this.Unauthorized();
                        return RedirectToAction("Index", "Login");
                    }
                    else
                    {
                        message = "Unable to save item. Please contact system admin.";
                    }
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            return Redirect($"{Url.Action("Index", "Items")}?message={message}");
        }

        [HttpPost]
        public async Task<IActionResult> Stocks(InventoryDetails inventoryDetails)
        {
            var message = "";

            try
            {
                if (ModelState.IsValid)
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", User.GetAccessToken());

                    var result = await httpClient.PostAsync($"{nekonoAppConfig.Value.NekonoAPI}{API_INVENTORY_NEW}", inventoryDetails, JsonConfig.PostFormatter());

                    if (result.IsSuccessStatusCode)
                    {
                        message = "SUCCESS1";
                    }
                    else if (result.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        //return this.Unauthorized();
                        return RedirectToAction("Index", "Login");
                    }
                    else
                    {
                        message = "Unable to save item. Please contact system admin.";
                    }
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            return Redirect($"{Url.Action("Index", "Items")}?message={message}");
        }

        public async Task<IActionResult> Stocks(string itemNo)
        {
            IEnumerable<InventoryDetails> inventoryDetails = null;

            try
            {
                if (ModelState.IsValid)
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", User.GetAccessToken());

                    var result = await httpClient.GetAsync($"{nekonoAppConfig.Value.NekonoAPI}{API_ITEM_INVENTORY}/{itemNo}");

                    if (result.IsSuccessStatusCode)
                    {
                        using (var content = result.Content)
                        {
                            inventoryDetails = JsonConvert.DeserializeObject<IEnumerable<InventoryDetails>>(await content.ReadAsStringAsync());
                        }
                    }
                    else if (result.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        return this.Unauthorized();
                        //return RedirectToAction("Index", "Login");
                        //return Json(new { isRedirect = true, redirectToUrl = Url.Action("Index", "Login") });
                    }
                    else
                    {
                        return this.StatusCode((int)result.StatusCode, result.Content);
                    }
                }
            }
            catch (Exception ex)
            {
                return this.StatusCode(500, ex.Message);
            }

            return Json(inventoryDetails);
        }
    }
}
