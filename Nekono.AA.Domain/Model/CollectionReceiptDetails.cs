using System.Collections.Generic;

namespace Nekono.AA.Domain.Model
{
    public class CollectionReceiptDetails
    {
        public string CollectionReceiptNo { get; set; }
        public string TranDate { get; set; }
        public string TranTime { get; set; }
        public decimal TotalAmount { get; set; }
        public string Remarks { get; set; }
        public string BranchCode { get; set; }
        public string CreatedBy { get; set; }
        public string LastUpdatedDate { get; set; }
        public string LastUpdatedTime { get; set; }
        public string LastUpdatedBy { get; set; }
        public string Type { get; set; }
        public string RefNo { get; set; }
        public string Bank { get; set; }
        public int Active { get; set; }
        public IEnumerable<InventoryDetails> InventoryDetails { get; set; }
    }
}
