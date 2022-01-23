using Nekono.AA.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nekono.AA.Data
{
    public interface IInventoryServices
    {
        Task<InventoryDetails> GetByInventoryNo(string inventoryNo);
        Task<IEnumerable<InventoryDetails>> GetByItemNo(string itemNo, bool includeDeleted = false);
        Task<IEnumerable<InventoryDetails>> GetByCollectionReceiptNo(string collectionReceiptNo);
        Task<IEnumerable<InventoryDetails>> GetItemMovementByDate(string startDate, string endDate);
        Task<IEnumerable<ItemTotalMovementDetails>> GetItemTotalMovementByDate(string startDate, string endDate);
        Task<IEnumerable<InventoryDetails>> GetSalesByDate(string startDate, string endDate);
        Task<IEnumerable<InventoryDetails>> GetVoidByDate(string startDate, string endDate);
        Task<IEnumerable<InventoryDetails>> GetSalesVoidsByDate(string startDate, string endDate);
        Task<IEnumerable<TopSalesItemDetails>> GetTopSalesAmountItemByDate(int top, string startDate, string endDate);
        Task<IEnumerable<TopSalesItemDetails>> GetTopSalesVolumeItemByDate(int top, string startDate, string endDate);
        Task<bool> Insert(InventoryDetails details, string createdBy, string collectionReceiptNo = "");
        Task<bool> Update(InventoryDetails details, string updatedBy);
        Task<bool> Delete(DeleteRequest request, string updatedBy);
    }
}