using Nekono.AA.Data;
using Nekono.AA.Domain.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nekono.AA.Business
{
    public class InventoryLogic : IInventoryLogic
    {
        private readonly IInventoryServices inventoryServices;

        public InventoryLogic(IInventoryServices inventoryServices)
        {
            this.inventoryServices = inventoryServices ??
                    throw new ArgumentNullException(nameof(inventoryServices));
        }

        public async Task<bool> Delete(DeleteRequest request, string updatedBy)
        {
            return await inventoryServices.Delete(request, updatedBy);
        }

        public async Task<IEnumerable<InventoryDetails>> GetByCollectionReceiptNo(string collectionReceiptNo)
        {
            return await inventoryServices.GetByCollectionReceiptNo(collectionReceiptNo);
        }

        public async Task<InventoryDetails> GetByInventoryNo(string inventoryNo)
        {
            return await inventoryServices.GetByInventoryNo(inventoryNo);
        }

        public async Task<IEnumerable<InventoryDetails>> GetByItemNo(string itemNo, bool includeDeleted = false)
        {
            return await inventoryServices.GetByItemNo(itemNo, includeDeleted);
        }

        public async Task<IEnumerable<InventoryDetails>> GetItemMovementByDate(string startDate, string endDate)
        {
            return await inventoryServices.GetItemMovementByDate(startDate, endDate);
        }

        public async Task<IEnumerable<ItemTotalMovementDetails>> GetItemTotalMovementByDate(string startDate, string endDate)
        {
            return await inventoryServices.GetItemTotalMovementByDate(startDate, endDate);
        }

        public async Task<IEnumerable<InventoryDetails>> GetSalesByDate(string startDate, string endDate)
        {
            return await inventoryServices.GetSalesByDate(startDate, endDate);
        }

        public async Task<IEnumerable<InventoryDetails>> GetSalesVoidsByDate(string startDate, string endDate)
        {
            return await inventoryServices.GetSalesVoidsByDate(startDate, endDate);
        }

        public async Task<IEnumerable<TopSalesItemDetails>> GetTopSalesAmountItemByDate(int top, string startDate, string endDate)
        {
            return await inventoryServices.GetTopSalesAmountItemByDate(top, startDate, endDate);
        }

        public async Task<IEnumerable<TopSalesItemDetails>> GetTopSalesVolumeItemByDate(int top, string startDate, string endDate)
        {
            return await inventoryServices.GetTopSalesVolumeItemByDate(top, startDate, endDate);
        }

        public async Task<IEnumerable<InventoryDetails>> GetVoidByDate(string startDate, string endDate)
        {
            return await inventoryServices.GetVoidByDate(startDate, endDate);
        }

        public async Task<bool> Insert(InventoryDetails details, string createdBy, string collectionReceiptNo = "")
        {
            return await inventoryServices.Insert(details, createdBy, collectionReceiptNo);
        }

        public async Task<bool> Update(InventoryDetails details, string updatedBy)
        {
            return await inventoryServices.Update(details, updatedBy);
        }
    }
}
