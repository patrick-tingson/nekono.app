using Nekono.AA.Data;
using Nekono.AA.Domain.CustomException;
using Nekono.AA.Domain.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nekono.AA.Business
{
    public class CollectionReceiptLogic : ICollectionReceiptLogic
    {
        private readonly ICollectionReceiptsServices collectionReceiptsServices;
        private readonly IInventoryServices inventoryServices;

        public CollectionReceiptLogic(ICollectionReceiptsServices collectionReceiptsServices,
            IInventoryServices inventoryServices)
        {
            this.collectionReceiptsServices = collectionReceiptsServices ??
                    throw new ArgumentNullException(nameof(collectionReceiptsServices));

            this.inventoryServices = inventoryServices ??
                    throw new ArgumentNullException(nameof(inventoryServices));
        }

        public async Task<CollectionReceiptDetails> GetByCollectionReceiptNo(string collectionReceiptNo)
        {
            return await collectionReceiptsServices.GetByCollectionReceiptNo(collectionReceiptNo);
        }

        public async Task<IEnumerable<CollectionReceiptDetails>> GetByDate(string startDate, string endDate)
        {
            return await collectionReceiptsServices.GetByDate(startDate, endDate);
        }

        public async Task<IEnumerable<CollectionReceiptDetails>> GetToday(string createdBy)
        {
            return await collectionReceiptsServices.GetToday(createdBy);
        }

        public async Task<IEnumerable<CollectionReceiptDetails>> GetAll()
        {
            return await collectionReceiptsServices.GetAll();
        }

        public async Task<string> Sale(CollectionReceiptDetails details, string createdBy)
        {
            var result = "";

            var createCollectionReceiptResult = await collectionReceiptsServices.Sale(details, createdBy);

            if(createCollectionReceiptResult.Length > 0)
            {
                foreach (var inventoryDetail in details.InventoryDetails)
                {
                    var inventoryInsertResult = await inventoryServices.Insert(inventoryDetail, createdBy, createCollectionReceiptResult);

                    if(!inventoryInsertResult)
                    {
                        await collectionReceiptsServices.Void(new DeleteRequest
                        {
                            Id = createCollectionReceiptResult,
                            Remarks = $"Failed to log all items. {inventoryDetail.ItemNo} {inventoryDetail.ItemName}"
                        }, createdBy);

                        throw new HttpStatusCodeException(System.Net.HttpStatusCode.InternalServerError,
                            "Failed to log all items. Please contact your system admin.");
                    }
                }

                result = createCollectionReceiptResult;
            }
            else
            {
                throw new HttpStatusCodeException(System.Net.HttpStatusCode.InternalServerError,
                    "Failed to create Collection Receipt. Please contact your system admin.");
            }

            return result;
        }

        public async Task<bool> Void(DeleteRequest request, string updatedBy)
        {
            return await collectionReceiptsServices.Void(request, updatedBy);
        }
    }
}
