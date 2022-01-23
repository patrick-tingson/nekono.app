using Nekono.AA.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nekono.AA.Data
{
    public interface IItemServices
    {
        Task<ItemDetails> GetItemDetails(string itemNo);
        Task<IEnumerable<ItemHistoryDetails>> GetItemHistory(string itemNo);
        Task<ItemInPOSDetails> GetItemInPOSDetails(string itemNo);
        Task<IEnumerable<ItemInPOSDetails>> GetAllItemInPOSDetails();
        Task<IEnumerable<ItemHistoryDetails>> GetAllItemHistory();
        Task<IEnumerable<ItemDetails>> GetAllItemDetails();
        Task<bool> Insert(ItemDetails details, string createdBy);
        Task<bool> Update(ItemDetails details, string updatedBy);
        Task<bool> Delete(DeleteRequest request, string updatedBy);
    }
}