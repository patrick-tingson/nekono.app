using Nekono.AA.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nekono.AA.Business
{
    public interface ICollectionReceiptLogic
    {
        Task<CollectionReceiptDetails> GetByCollectionReceiptNo(string collectionReceiptNo);
        Task<IEnumerable<CollectionReceiptDetails>> GetByDate(string startDate, string endDate);
        Task<IEnumerable<CollectionReceiptDetails>> GetAll();
        Task<IEnumerable<CollectionReceiptDetails>> GetToday(string createdBy);
        Task<string> Sale(CollectionReceiptDetails details, string createdBy);
        Task<bool> Void(DeleteRequest request, string updatedBy);
    }
}