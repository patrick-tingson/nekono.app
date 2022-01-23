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
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryLogic inventoryLogic;

        public InventoryController(IInventoryLogic inventoryLogic)
        {
            this.inventoryLogic = inventoryLogic ??
                throw new ArgumentNullException(nameof(inventoryLogic));
        }

        [HttpGet]
        [Route("v1/[controller]/collectionReceipt/{collectionReceiptNo}")]
        public async Task<ActionResult> GetByCollectionReceiptNo(string collectionReceiptNo)
        {
            return this.Ok(await inventoryLogic.GetByCollectionReceiptNo(collectionReceiptNo));
        }

        [HttpGet]
        [Route("v1/[controller]/{inventoryNo}")]
        public async Task<ActionResult> GetByInventoryNo(string inventoryNo)
        {
            return this.Ok(await inventoryLogic.GetByInventoryNo(inventoryNo));
        }

        [HttpGet]
        [Route("v1/[controller]/item/{itemNo}")]
        public async Task<ActionResult> GetByItemNo(string itemNo, [FromQuery] bool includeDeleted = false)
        {
            return this.Ok(await inventoryLogic.GetByItemNo(itemNo, includeDeleted));
        }

        [HttpGet]
        [Route("v1/[controller]")]
        public async Task<ActionResult> GetItemMovementByDate([FromQuery] string startDate, [FromQuery] string endDate)
        {
            return this.Ok(await inventoryLogic.GetItemMovementByDate(startDate, endDate));
        }

        [HttpGet]
        [Route("v1/[controller]/total")]
        public async Task<ActionResult> GetItemTotalMovementByDate([FromQuery] string startDate, [FromQuery] string endDate)
        {
            return this.Ok(await inventoryLogic.GetItemTotalMovementByDate(startDate, endDate));
        }

        [HttpGet]
        [Route("v1/[controller]/sale")]
        public async Task<ActionResult> GetSalesByDate([FromQuery] string startDate, [FromQuery] string endDate)
        {
            return this.Ok(await inventoryLogic.GetSalesByDate(startDate, endDate));
        }

        [HttpGet]
        [Route("v1/[controller]/salevoid")]
        public async Task<ActionResult> GetSalesVoidsByDate([FromQuery] string startDate, [FromQuery] string endDate)
        {
            return this.Ok(await inventoryLogic.GetSalesVoidsByDate(startDate, endDate));
        }

        [HttpGet]
        [Route("v1/[controller]/void")]
        public async Task<ActionResult> GetVoidByDate([FromQuery] string startDate, [FromQuery] string endDate)
        {
            return this.Ok(await inventoryLogic.GetVoidByDate(startDate, endDate));
        }

        [HttpGet]
        [Route("v1/[controller]/sale/topamount")]
        public async Task<ActionResult> GetTopSalesAmountItemByDate([FromQuery] string startDate, [FromQuery] string endDate, [FromQuery] int top)
        {
            return this.Ok(await inventoryLogic.GetTopSalesAmountItemByDate(top, startDate, endDate));
        }

        [HttpGet]
        [Route("v1/[controller]/sale/topvolume")]
        public async Task<ActionResult> GetTopSalesVolumeItemByDate([FromQuery] string startDate, [FromQuery] string endDate, [FromQuery] int top)
        {
            return this.Ok(await inventoryLogic.GetTopSalesVolumeItemByDate(top, startDate, endDate));
        }

        [HttpPost]
        [Route("v1/[controller]/new")]
        public async Task<ActionResult> Insert(InventoryDetails inventoryDetails)
        {
            var tokenUsername = User.GetTokenUsername();

            var result = await inventoryLogic.Insert(inventoryDetails, tokenUsername);

            if (!result)
            {
                return StatusCode(500);
            }

            return this.Ok();
        }

        [HttpPost]
        [Route("v1/[controller]/update")]
        public async Task<ActionResult> Update(InventoryDetails inventoryDetails)
        {
            var tokenUsername = User.GetTokenUsername();

            var result = await inventoryLogic.Update(inventoryDetails, tokenUsername);

            if (!result)
            {
                return StatusCode(500);
            }

            return this.Ok();
        }

        [HttpPost]
        [Route("v1/[controller]/delete")]
        public async Task<ActionResult> Delete(DeleteRequest deleteRequest)
        {
            var tokenUsername = User.GetTokenUsername();

            var result = await inventoryLogic.Delete(deleteRequest, tokenUsername);

            if (!result)
            {
                return StatusCode(500);
            }

            return this.Ok();
        }
    }
}
