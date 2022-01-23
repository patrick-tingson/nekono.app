using Nekono.AA.Domain.Model;

namespace Nekono.Web.Models
{
    public class ItemsMultipleModel
    {
        public ItemDetails ItemDetails { get; set; }
        public InventoryDetails InventoryDetails { get; set; }
        public DeleteRequest DeleteRequest { get; set; }
    }
}
