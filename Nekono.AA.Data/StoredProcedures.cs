using System;
using System.Collections.Generic;
using System.Text;

namespace Nekono.AA.Data
{
    public class StoredProcedures
    {
        public const string LoginSP = "LoginSP";
        public const string CollectionRecieptInsertSP = "CollectionRecieptInsertSP";
        public const string CollectionRecieptVoidSP = "CollectionRecieptVoidSP";
        public const string InventoryDeleteSP = "InventoryDeleteSP";
        public const string InventoryInsertSP = "InventoryInsertSP";
        public const string ItemDeleteSP = "ItemDeleteSP";
        public const string ItemInsertSP = "ItemInsertSP";
        public const string ItemMovementByDate = "ItemMovementByDate";
        public const string ItemRestoreSP = "ItemRestoreSP";
        public const string ItemTotalMovementByDateSP = "ItemTotalMovementByDateSP";
        public const string ItemUpdateSP = "ItemUpdateSP";
        public const string SalesByDateSP = "SalesByDateSP";
        public const string SalesVoidsByDateSP = "SalesVoidsByDateSP";
        public const string TopSalesAmountItemByDate = "TopSalesAmountItemByDate";
        public const string TopSalesVolumeItemByDate = "TopSalesVolumeItemByDate";
        public const string VoidsByDateSP = "VoidsByDateSP";
    }
}
