using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nekono.AA.Business;
using Nekono.AA.Domain.Model;
using Nekono.API.Extensions;

namespace Nekono.API.Controllers
{
    [Route("api")]
    [ApiController]
    [Authorize]
    public class ItemController : ControllerBase
    {
        private readonly IItemLogic itemLogic;

        public ItemController(IItemLogic itemLogic)
        {
            this.itemLogic = itemLogic ??
                throw new ArgumentNullException(nameof(itemLogic));
        }

        [HttpGet]
        [Route("v1/[controller]")]
        public async Task<ActionResult> GetAllItemDetails()
        {
            return this.Ok(await itemLogic.GetAllItemDetails());
        }

        [HttpGet]
        [Route("v1/[controller]/history")]
        public async Task<ActionResult> GetAllItemHistory()
        {
            return this.Ok(await itemLogic.GetAllItemHistory());
        }

        [HttpGet]
        [Route("v1/[controller]/pos")]
        public async Task<ActionResult> GetAllItemInPOSDetails()
        {
            return this.Ok(await itemLogic.GetAllItemInPOSDetails());
        }

        [HttpGet]
        [Route("v1/[controller]/{itemNo}")]
        public async Task<ActionResult> GetItemDetails(string itemNo)
        {
            return this.Ok(await itemLogic.GetItemDetails(itemNo));
        }

        [HttpGet]
        [Route("v1/[controller]/history/{itemNo}")]
        public async Task<ActionResult> GetItemHistory(string itemNo)
        {
            return this.Ok(await itemLogic.GetItemHistory(itemNo));
        }

        [HttpGet]
        [Route("v1/[controller]/pos/{itemNo}")]
        public async Task<ActionResult> GetItemInPOSDetails(string itemNo)
        {
            return this.Ok(await itemLogic.GetItemInPOSDetails(itemNo));
        }

        [HttpPost]
        [Route("v1/[controller]/new")]
        public async Task<ActionResult> New(ItemDetails itemDetails)
        {
            var tokenUsername = User.GetTokenUsername();

            var result = await itemLogic.Insert(itemDetails, tokenUsername);

            if(!result)
            {
                return StatusCode(500);
            }

            return this.Ok();
        }

        [HttpPost]
        [Route("v1/[controller]/update")]
        public async Task<ActionResult> Update(ItemDetails itemDetails)
        {
            var tokenUsername = User.GetTokenUsername();

            var result = await itemLogic.Update(itemDetails, tokenUsername);

            if (!result)
            {
                return StatusCode(500);
            }

            return this.Ok();
        }

        [HttpPost]
        [Route("v1/[controller]/delete")]
        public async Task<ActionResult> Delete(DeleteRequest request)
        {
            var tokenUsername = User.GetTokenUsername();

            var result = await itemLogic.Delete(request, tokenUsername);

            if (!result)
            {
                return StatusCode(500);
            }

            return this.Ok();
        }
    }
}
