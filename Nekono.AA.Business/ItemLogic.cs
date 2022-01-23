using Nekono.AA.Data;
using Nekono.AA.Domain.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nekono.AA.Business
{
    public class ItemLogic : IItemLogic
    {
        private readonly IItemServices itemServices;

        public ItemLogic(IItemServices itemServices)
        {
            this.itemServices = itemServices ??
              throw new ArgumentNullException(nameof(itemServices));
        }

        public async Task<bool> Delete(DeleteRequest request, string updatedBy)
        {
            return await itemServices.Delete(request, updatedBy);
        }

        public async Task<IEnumerable<ItemDetails>> GetAllItemDetails()
        {
            return await itemServices.GetAllItemDetails();
        }

        public async Task<IEnumerable<ItemHistoryDetails>> GetAllItemHistory()
        {
            return await itemServices.GetAllItemHistory();
        }

        public async Task<IEnumerable<ItemInPOSDetails>> GetAllItemInPOSDetails()
        {
            return await itemServices.GetAllItemInPOSDetails();
        }

        public async Task<ItemDetails> GetItemDetails(string itemNo)
        {
            return await itemServices.GetItemDetails(itemNo);
        }

        public async Task<IEnumerable<ItemHistoryDetails>> GetItemHistory(string itemNo)
        {
            return await itemServices.GetItemHistory(itemNo);
        }

        public async Task<ItemInPOSDetails> GetItemInPOSDetails(string itemNo)
        {
            return await itemServices.GetItemInPOSDetails(itemNo);
        }

        public async Task<bool> Insert(ItemDetails details, string createdBy)
        {
            return await itemServices.Insert(details, createdBy);
        }

        public async Task<bool> Update(ItemDetails details, string updatedBy)
        {
            return await itemServices.Update(details, updatedBy);
        }
    }
}
