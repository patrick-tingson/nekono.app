using Nekono.AA.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nekono.AA.Business
{
    public interface IItemLogic
    {
        Task<IEnumerable<ItemDetails>> GetAllItemDetails();
        Task<IEnumerable<ItemHistoryDetails>> GetAllItemHistory();
        Task<IEnumerable<ItemInPOSDetails>> GetAllItemInPOSDetails();
        Task<ItemDetails> GetItemDetails(string itemNo);
        Task<IEnumerable<ItemHistoryDetails>> GetItemHistory(string itemNo);
        Task<ItemInPOSDetails> GetItemInPOSDetails(string itemNo);
        Task<bool> Insert(ItemDetails details, string createdBy);
        Task<bool> Update(ItemDetails details, string updatedBy);
        Task<bool> Delete(DeleteRequest request, string updatedBy);
    }
}