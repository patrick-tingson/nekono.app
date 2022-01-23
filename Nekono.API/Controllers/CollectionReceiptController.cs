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
    public class CollectionReceiptController : ControllerBase
    {
        private readonly ICollectionReceiptLogic collectionReceiptLogic;

        public CollectionReceiptController(ICollectionReceiptLogic collectionReceiptLogic)
        {
            this.collectionReceiptLogic = collectionReceiptLogic ??
                throw new ArgumentNullException(nameof(collectionReceiptLogic));
        }

        [HttpGet]
        [Route("v1/[controller]/{collectionReceiptNo}")]
        public async Task<ActionResult> GetByCollectionReceiptNo(string collectionReceiptNo)
        {
            return this.Ok(await collectionReceiptLogic.GetByCollectionReceiptNo(collectionReceiptNo));
        }

        [HttpGet]
        [Route("v1/[controller]")]
        public async Task<ActionResult> GetByDate([FromQuery] string startDate, [FromQuery] string endDate)
        {
            return this.Ok(await collectionReceiptLogic.GetByDate(startDate, endDate));
        }

        [HttpGet]
        [Route("v1/[controller]/today")]
        public async Task<ActionResult> GetToday()
        {
            var tokenUsername = User.GetTokenUsername();

            return this.Ok(await collectionReceiptLogic.GetToday(tokenUsername));
        }

        [HttpGet]
        [Route("v1/[controller]/getall")]
        public async Task<ActionResult> GetAll()
        { 
            return this.Ok(await collectionReceiptLogic.GetAll());
        }

        [HttpPost]
        [Route("v1/[controller]/sale")]
        public async Task<ActionResult> Sale(CollectionReceiptDetails collectionReceiptDetails)
        {
            var tokenUsername = User.GetTokenUsername();

            var salesResult = await collectionReceiptLogic.Sale(collectionReceiptDetails, tokenUsername);

            if (salesResult.Length == 0)
            {
                return StatusCode(500);
            }

            return this.Ok(salesResult);
        }

        [HttpPost]
        [Route("v1/[controller]/void")]
        public async Task<ActionResult> Void(DeleteRequest request)
        {
            var tokenUsername = User.GetTokenUsername();

            var newItemResult = await collectionReceiptLogic.Void(request, tokenUsername);

            if (!newItemResult)
            {
                return StatusCode(500);
            }

            return this.Ok();
        }
    }
}
