using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Nekono.AA.Domain.Config;
using System.Data.SqlClient;
using Nekono.AA.Domain.Model;
using Nekono.AA.Domain.Utility;

namespace Nekono.AA.Data
{
    public class InventoryServices : IInventoryServices
    {
        private readonly IOptions<NekonoAppConfig> nekonoAppConfig;

        public InventoryServices(IOptions<NekonoAppConfig> nekonoAppConfig)
        {
            this.nekonoAppConfig = nekonoAppConfig ??
              throw new ArgumentNullException(nameof(nekonoAppConfig));
        }

        public Task<IEnumerable<InventoryDetails>> GetByCollectionReceiptNo(string collectionReceiptNo)
        {
            var result = new List<InventoryDetails>();

            using (SqlConnection SQLConnect = new SqlConnection(nekonoAppConfig.Value.DbConn))
            {
                SQLConnect.Open();

                StringBuilder xSQL = new StringBuilder();
                xSQL.AppendLine("SELECT * ");
                xSQL.AppendLine("FROM ItemInventoryMovementsView ");
                xSQL.AppendLine("WHERE collectionReceiptNo = @collectionReceiptNo ");

                SqlCommand SQLCommand = new SqlCommand(xSQL.ToString(), SQLConnect);

                SQLCommand.Parameters.AddWithValue("@collectionReceiptNo", collectionReceiptNo);

                SqlDataReader reader = SQLCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result.Add(new InventoryDetails
                        {
                            CollectionReceiptNo = (reader["collectionReceiptNo"] != System.DBNull.Value) ? reader["collectionReceiptNo"].ToString() : null,
                            InventoryNo = (reader["inventoryNo"] != System.DBNull.Value) ? reader["inventoryNo"].ToString() : null,
                            ItemNo = (reader["itemNo"] != System.DBNull.Value) ? reader["itemNo"].ToString() : null,
                            ItemName = (reader["itemName"] != System.DBNull.Value) ? reader["itemName"].ToString() : null,
                            ItemDesc = (reader["itemDesc"] != System.DBNull.Value) ? reader["itemDesc"].ToString() : null,
                            VendorCode = (reader["vendorCode"] != System.DBNull.Value) ? reader["vendorCode"].ToString() : null,
                            VendorName = (reader["vendorName"] != System.DBNull.Value) ? reader["vendorName"].ToString() : null,
                            LastUpdatedBy = (reader["lastUpdatedBy"] != System.DBNull.Value) ? reader["lastUpdatedBy"].ToString() : null,
                            LastUpdatedDate = (reader["lastUpdatedDate"] != System.DBNull.Value) ? reader["lastUpdatedDate"].ToString() : null,
                            LastUpdatedTime = (reader["lastUpdatedTime"] != System.DBNull.Value) ? reader["lastUpdatedTime"].ToString() : null,
                            BranchCode = (reader["branchCode"] != System.DBNull.Value) ? reader["branchCode"].ToString() : null,
                            Type = (reader["type"] != System.DBNull.Value) ? reader["type"].ToString() : null,
                            CreatedBy = (reader["createdBy"] != System.DBNull.Value) ? reader["createdBy"].ToString() : null,
                            TranDate = (reader["tranDate"] != System.DBNull.Value) ? reader["tranDate"].ToString() : null,
                            TranTime = (reader["tranTime"] != System.DBNull.Value) ? reader["tranTime"].ToString() : null,
                            Remarks = (reader["remarks"] != System.DBNull.Value) ? reader["remarks"].ToString() : null,
                            TotalAmount = (reader["totalAmount"] != System.DBNull.Value) ? Convert.ToDecimal(reader["totalAmount"].ToString()) : 0,
                            Discount = (reader["discount"] != System.DBNull.Value) ? Convert.ToDecimal(reader["discount"].ToString()) : 0,
                            AmountPerQty = (reader["amountPerQty"] != System.DBNull.Value) ? Convert.ToDecimal(reader["amountPerQty"].ToString()) : 0,
                            Qty = (reader["qty"] != System.DBNull.Value) ? Convert.ToInt32(reader["qty"].ToString()) : 0,
                            Active = (reader["active"] != System.DBNull.Value) ? Convert.ToInt32(reader["active"].ToString()) : 0
                        });
                    }
                }
            }

            return Task.FromResult<IEnumerable<InventoryDetails>>(result);
        }

        public Task<InventoryDetails> GetByInventoryNo(string inventoryNo)
        {
            InventoryDetails result = null;

            using (SqlConnection SQLConnect = new SqlConnection(nekonoAppConfig.Value.DbConn))
            {
                SQLConnect.Open();

                StringBuilder xSQL = new StringBuilder();
                xSQL.AppendLine("SELECT * ");
                xSQL.AppendLine("FROM ItemInventoryMovementsView ");
                xSQL.AppendLine("WHERE inventoryNo = @inventoryNo ");


                SqlCommand SQLCommand = new SqlCommand(xSQL.ToString(), SQLConnect);

                SQLCommand.Parameters.AddWithValue("@inventoryNo", inventoryNo);

                SqlDataReader reader = SQLCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result = new InventoryDetails
                        {
                            CollectionReceiptNo = (reader["collectionReceiptNo"] != System.DBNull.Value) ? reader["collectionReceiptNo"].ToString() : null,
                            InventoryNo = (reader["inventoryNo"] != System.DBNull.Value) ? reader["inventoryNo"].ToString() : null,
                            ItemNo = (reader["itemNo"] != System.DBNull.Value) ? reader["itemNo"].ToString() : null,
                            ItemName = (reader["itemName"] != System.DBNull.Value) ? reader["itemName"].ToString() : null,
                            ItemDesc = (reader["itemDesc"] != System.DBNull.Value) ? reader["itemDesc"].ToString() : null,
                            VendorCode = (reader["vendorCode"] != System.DBNull.Value) ? reader["vendorCode"].ToString() : null,
                            VendorName = (reader["vendorName"] != System.DBNull.Value) ? reader["vendorName"].ToString() : null,
                            LastUpdatedBy = (reader["lastUpdatedBy"] != System.DBNull.Value) ? reader["lastUpdatedBy"].ToString() : null,
                            LastUpdatedDate = (reader["lastUpdatedDate"] != System.DBNull.Value) ? reader["lastUpdatedDate"].ToString() : null,
                            LastUpdatedTime = (reader["lastUpdatedTime"] != System.DBNull.Value) ? reader["lastUpdatedTime"].ToString() : null,
                            BranchCode = (reader["branchCode"] != System.DBNull.Value) ? reader["branchCode"].ToString() : null,
                            Type = (reader["type"] != System.DBNull.Value) ? reader["type"].ToString() : null,
                            CreatedBy = (reader["createdBy"] != System.DBNull.Value) ? reader["createdBy"].ToString() : null,
                            TranDate = (reader["tranDate"] != System.DBNull.Value) ? reader["tranDate"].ToString() : null,
                            TranTime = (reader["tranTime"] != System.DBNull.Value) ? reader["tranTime"].ToString() : null,
                            Remarks = (reader["remarks"] != System.DBNull.Value) ? reader["remarks"].ToString() : null,
                            TotalAmount = (reader["totalAmount"] != System.DBNull.Value) ? Convert.ToDecimal(reader["totalAmount"].ToString()) : 0,
                            Discount = (reader["discount"] != System.DBNull.Value) ? Convert.ToDecimal(reader["discount"].ToString()) : 0,
                            AmountPerQty = (reader["amountPerQty"] != System.DBNull.Value) ? Convert.ToDecimal(reader["amountPerQty"].ToString()) : 0,
                            Qty = (reader["qty"] != System.DBNull.Value) ? Convert.ToInt32(reader["qty"].ToString()) : 0,
                            Active = (reader["active"] != System.DBNull.Value) ? Convert.ToInt32(reader["active"].ToString()) : 0
                        };
                    }
                }
            }

            return Task.FromResult(result);
        }

        public Task<IEnumerable<InventoryDetails>> GetByItemNo(string itemNo, bool includeDeleted = false)
        {
            var result = new List<InventoryDetails>();

            using (SqlConnection SQLConnect = new SqlConnection(nekonoAppConfig.Value.DbConn))
            {
                SQLConnect.Open();

                StringBuilder xSQL = new StringBuilder();
                xSQL.AppendLine("SELECT * ");
                xSQL.AppendLine("FROM ItemInventoryMovementsView ");
                xSQL.AppendLine("WHERE itemNo = @itemNo ");

                if (includeDeleted)
                {
                    xSQL.AppendLine("   AND active = @active ");
                }

                SqlCommand SQLCommand = new SqlCommand(xSQL.ToString(), SQLConnect);

                SQLCommand.Parameters.AddWithValue("@itemNo", itemNo);

                if(includeDeleted)
                {
                    SQLCommand.Parameters.AddWithValue("@active", 1);
                }

                SqlDataReader reader = SQLCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result.Add(new InventoryDetails
                        {
                            CollectionReceiptNo = (reader["collectionReceiptNo"] != System.DBNull.Value) ? reader["collectionReceiptNo"].ToString() : null,
                            InventoryNo = (reader["inventoryNo"] != System.DBNull.Value) ? reader["inventoryNo"].ToString() : null,
                            ItemNo = (reader["itemNo"] != System.DBNull.Value) ? reader["itemNo"].ToString() : null,
                            ItemName = (reader["itemName"] != System.DBNull.Value) ? reader["itemName"].ToString() : null,
                            ItemDesc = (reader["itemDesc"] != System.DBNull.Value) ? reader["itemDesc"].ToString() : null,
                            VendorCode = (reader["vendorCode"] != System.DBNull.Value) ? reader["vendorCode"].ToString() : null,
                            VendorName = (reader["vendorName"] != System.DBNull.Value) ? reader["vendorName"].ToString() : null,
                            LastUpdatedBy = (reader["lastUpdatedBy"] != System.DBNull.Value) ? reader["lastUpdatedBy"].ToString() : null,
                            LastUpdatedDate = (reader["lastUpdatedDate"] != System.DBNull.Value) ? reader["lastUpdatedDate"].ToString() : null,
                            LastUpdatedTime = (reader["lastUpdatedTime"] != System.DBNull.Value) ? reader["lastUpdatedTime"].ToString() : null,
                            BranchCode = (reader["branchCode"] != System.DBNull.Value) ? reader["branchCode"].ToString() : null,
                            Type = (reader["type"] != System.DBNull.Value) ? reader["type"].ToString() : null,
                            CreatedBy = (reader["createdBy"] != System.DBNull.Value) ? reader["createdBy"].ToString() : null,
                            TranDate = (reader["tranDate"] != System.DBNull.Value) ? reader["tranDate"].ToString() : null,
                            TranTime = (reader["tranTime"] != System.DBNull.Value) ? reader["tranTime"].ToString() : null,
                            Remarks = (reader["remarks"] != System.DBNull.Value) ? reader["remarks"].ToString() : null,
                            TotalAmount = (reader["totalAmount"] != System.DBNull.Value) ? Convert.ToDecimal(reader["totalAmount"].ToString()) : 0,
                            Discount = (reader["discount"] != System.DBNull.Value) ? Convert.ToDecimal(reader["discount"].ToString()) : 0,
                            AmountPerQty = (reader["amountPerQty"] != System.DBNull.Value) ? Convert.ToDecimal(reader["amountPerQty"].ToString()) : 0,
                            Qty = (reader["qty"] != System.DBNull.Value) ? Convert.ToInt32(reader["qty"].ToString()) : 0,
                            Active = (reader["active"] != System.DBNull.Value) ? Convert.ToInt32(reader["active"].ToString()) : 0
                        });
                    }
                }
            }

            return Task.FromResult<IEnumerable<InventoryDetails>>(result);
        }

        public Task<IEnumerable<InventoryDetails>> GetItemMovementByDate(string startDate, string endDate)
        {
            var result = new List<InventoryDetails>();

            using (SqlConnection SQLConnect = new SqlConnection(nekonoAppConfig.Value.DbConn))
            {
                SQLConnect.Open();

                SqlCommand SQLCommand = new SqlCommand(StoredProcedures.ItemMovementByDate, SQLConnect);

                SQLCommand.CommandType = System.Data.CommandType.StoredProcedure;

                SQLCommand.Parameters.AddWithValue("@startDate", startDate);
                SQLCommand.Parameters.AddWithValue("@endDate", endDate);

                SqlDataReader reader = SQLCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result.Add(new InventoryDetails
                        {
                            CollectionReceiptNo = (reader["collectionReceiptNo"] != System.DBNull.Value) ? reader["collectionReceiptNo"].ToString() : null,
                            InventoryNo = (reader["inventoryNo"] != System.DBNull.Value) ? reader["inventoryNo"].ToString() : null,
                            ItemNo = (reader["itemNo"] != System.DBNull.Value) ? reader["itemNo"].ToString() : null,
                            ItemName = (reader["itemName"] != System.DBNull.Value) ? reader["itemName"].ToString() : null,
                            ItemDesc = (reader["itemDesc"] != System.DBNull.Value) ? reader["itemDesc"].ToString() : null,
                            VendorCode = (reader["vendorCode"] != System.DBNull.Value) ? reader["vendorCode"].ToString() : null,
                            VendorName = (reader["vendorName"] != System.DBNull.Value) ? reader["vendorName"].ToString() : null,
                            LastUpdatedBy = (reader["lastUpdatedBy"] != System.DBNull.Value) ? reader["lastUpdatedBy"].ToString() : null,
                            LastUpdatedDate = (reader["lastUpdatedDate"] != System.DBNull.Value) ? reader["lastUpdatedDate"].ToString() : null,
                            LastUpdatedTime = (reader["lastUpdatedTime"] != System.DBNull.Value) ? reader["lastUpdatedTime"].ToString() : null,
                            BranchCode = (reader["branchCode"] != System.DBNull.Value) ? reader["branchCode"].ToString() : null,
                            Type = (reader["type"] != System.DBNull.Value) ? reader["type"].ToString() : null,
                            CreatedBy = (reader["createdBy"] != System.DBNull.Value) ? reader["createdBy"].ToString() : null,
                            TranDate = (reader["tranDate"] != System.DBNull.Value) ? reader["tranDate"].ToString() : null,
                            TranTime = (reader["tranTime"] != System.DBNull.Value) ? reader["tranTime"].ToString() : null,
                            Remarks = (reader["remarks"] != System.DBNull.Value) ? reader["remarks"].ToString() : null,
                            TotalAmount = (reader["totalAmount"] != System.DBNull.Value) ? Convert.ToDecimal(reader["totalAmount"].ToString()) : 0,
                            Discount = (reader["discount"] != System.DBNull.Value) ? Convert.ToDecimal(reader["discount"].ToString()) : 0,
                            AmountPerQty = (reader["amountPerQty"] != System.DBNull.Value) ? Convert.ToDecimal(reader["amountPerQty"].ToString()) : 0,
                            Qty = (reader["qty"] != System.DBNull.Value) ? Convert.ToInt32(reader["qty"].ToString()) : 0,
                            Active = (reader["active"] != System.DBNull.Value) ? Convert.ToInt32(reader["active"].ToString()) : 0
                        });
                    }
                }
            }

            return Task.FromResult<IEnumerable<InventoryDetails>>(result);
        }

        public Task<IEnumerable<ItemTotalMovementDetails>> GetItemTotalMovementByDate(string startDate, string endDate)
        {
            var result = new List<ItemTotalMovementDetails>();

            using (SqlConnection SQLConnect = new SqlConnection(nekonoAppConfig.Value.DbConn))
            {
                SQLConnect.Open();

                SqlCommand SQLCommand = new SqlCommand(StoredProcedures.ItemTotalMovementByDateSP, SQLConnect);

                SQLCommand.CommandType = System.Data.CommandType.StoredProcedure;

                SQLCommand.Parameters.AddWithValue("@startDate", startDate);
                SQLCommand.Parameters.AddWithValue("@endDate", endDate);

                SqlDataReader reader = SQLCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result.Add(new ItemTotalMovementDetails
                        {
                            ItemNo = (reader["itemNo"] != System.DBNull.Value) ? reader["itemNo"].ToString() : null,
                            ItemName = (reader["itemName"] != System.DBNull.Value) ? reader["itemName"].ToString() : null,
                            ItemDesc = (reader["itemDesc"] != System.DBNull.Value) ? reader["itemDesc"].ToString() : null,
                            VendorCode = (reader["vendorCode"] != System.DBNull.Value) ? reader["vendorCode"].ToString() : null,
                            VendorName = (reader["vendorName"] != System.DBNull.Value) ? reader["vendorName"].ToString() : null,
                            AvailableStock = (reader["availableStock"] != System.DBNull.Value) ? Convert.ToInt32(reader["availableStock"].ToString()) : 0,
                            RestockQty = (reader["restockQty"] != System.DBNull.Value) ? Convert.ToInt32(reader["restockQty"].ToString()) : 0,
                            PullQty = (reader["pullQty"] != System.DBNull.Value) ? Convert.ToInt32(reader["pullQty"].ToString()) : 0,
                            DamageQty = (reader["damageQty"] != System.DBNull.Value) ? Convert.ToInt32(reader["damageQty"].ToString()) : 0,
                            SaleQty = (reader["saleQty"] != System.DBNull.Value) ? Convert.ToInt32(reader["saleQty"].ToString()) : 0,
                            SaleAmount = (reader["saleAmount"] != System.DBNull.Value) ? Convert.ToDecimal(reader["saleAmount"].ToString()) : 0,
                            WholesaleQty = (reader["wholesaleQty"] != System.DBNull.Value) ? Convert.ToInt32(reader["wholesaleQty"].ToString()) : 0,
                            WholesaleAmount = (reader["wholesaleAmount"] != System.DBNull.Value) ? Convert.ToDecimal(reader["wholesaleAmount"].ToString()) : 0,
                            TotalSaleQty = (reader["totalSaleQty"] != System.DBNull.Value) ? Convert.ToInt32(reader["totalSaleQty"].ToString()) : 0,
                            TotalSaleAmount = (reader["totalSaleAmount"] != System.DBNull.Value) ? Convert.ToDecimal(reader["totalSaleAmount"].ToString()) : 0
                        });
                    }
                }
            }

            return Task.FromResult<IEnumerable<ItemTotalMovementDetails>>(result);
        }

        public Task<IEnumerable<InventoryDetails>> GetSalesByDate(string startDate, string endDate)
        {
            var result = new List<InventoryDetails>();

            using (SqlConnection SQLConnect = new SqlConnection(nekonoAppConfig.Value.DbConn))
            {
                SQLConnect.Open();

                SqlCommand SQLCommand = new SqlCommand(StoredProcedures.SalesByDateSP, SQLConnect);

                SQLCommand.CommandType = System.Data.CommandType.StoredProcedure;

                SQLCommand.Parameters.AddWithValue("@startDate", startDate);
                SQLCommand.Parameters.AddWithValue("@endDate", endDate);

                SqlDataReader reader = SQLCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result.Add(new InventoryDetails
                        {
                            CollectionReceiptNo = (reader["collectionReceiptNo"] != System.DBNull.Value) ? reader["collectionReceiptNo"].ToString() : null,
                            InventoryNo = (reader["inventoryNo"] != System.DBNull.Value) ? reader["inventoryNo"].ToString() : null,
                            ItemNo = (reader["itemNo"] != System.DBNull.Value) ? reader["itemNo"].ToString() : null,
                            ItemName = (reader["itemName"] != System.DBNull.Value) ? reader["itemName"].ToString() : null,
                            ItemDesc = (reader["itemDesc"] != System.DBNull.Value) ? reader["itemDesc"].ToString() : null,
                            VendorCode = (reader["vendorCode"] != System.DBNull.Value) ? reader["vendorCode"].ToString() : null,
                            VendorName = (reader["vendorName"] != System.DBNull.Value) ? reader["vendorName"].ToString() : null,
                            BranchCode = (reader["branchCode"] != System.DBNull.Value) ? reader["branchCode"].ToString() : null,
                            Type = (reader["type"] != System.DBNull.Value) ? reader["type"].ToString() : null,
                            CreatedBy = (reader["createdBy"] != System.DBNull.Value) ? reader["createdBy"].ToString() : null,
                            TranDate = (reader["tranDate"] != System.DBNull.Value) ? reader["tranDate"].ToString() : null,
                            TranTime = (reader["tranTime"] != System.DBNull.Value) ? reader["tranTime"].ToString() : null,
                            TotalAmount = (reader["totalAmount"] != System.DBNull.Value) ? Convert.ToDecimal(reader["totalAmount"].ToString()) : 0,
                            Discount = (reader["discount"] != System.DBNull.Value) ? Convert.ToDecimal(reader["discount"].ToString()) : 0,
                            AmountPerQty = (reader["amountPerQty"] != System.DBNull.Value) ? Convert.ToDecimal(reader["amountPerQty"].ToString()) : 0,
                            Qty = (reader["qty"] != System.DBNull.Value) ? Convert.ToInt32(reader["qty"].ToString()) : 0
                        });
                    }
                }
            }

            return Task.FromResult<IEnumerable<InventoryDetails>>(result);
        }

        public Task<IEnumerable<InventoryDetails>> GetSalesVoidsByDate(string startDate, string endDate)
        {
            var result = new List<InventoryDetails>();

            using (SqlConnection SQLConnect = new SqlConnection(nekonoAppConfig.Value.DbConn))
            {
                SQLConnect.Open();

                SqlCommand SQLCommand = new SqlCommand(StoredProcedures.SalesVoidsByDateSP, SQLConnect);

                SQLCommand.CommandType = System.Data.CommandType.StoredProcedure;

                SQLCommand.Parameters.AddWithValue("@startDate", startDate);
                SQLCommand.Parameters.AddWithValue("@endDate", endDate);

                SqlDataReader reader = SQLCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result.Add(new InventoryDetails
                        {
                            CollectionReceiptNo = (reader["collectionReceiptNo"] != System.DBNull.Value) ? reader["collectionReceiptNo"].ToString() : null,
                            InventoryNo = (reader["inventoryNo"] != System.DBNull.Value) ? reader["inventoryNo"].ToString() : null,
                            ItemNo = (reader["itemNo"] != System.DBNull.Value) ? reader["itemNo"].ToString() : null,
                            ItemName = (reader["itemName"] != System.DBNull.Value) ? reader["itemName"].ToString() : null,
                            ItemDesc = (reader["itemDesc"] != System.DBNull.Value) ? reader["itemDesc"].ToString() : null,
                            VendorCode = (reader["vendorCode"] != System.DBNull.Value) ? reader["vendorCode"].ToString() : null,
                            VendorName = (reader["vendorName"] != System.DBNull.Value) ? reader["vendorName"].ToString() : null,
                            LastUpdatedBy = (reader["lastUpdatedBy"] != System.DBNull.Value) ? reader["lastUpdatedBy"].ToString() : null,
                            LastUpdatedDate = (reader["lastUpdatedDate"] != System.DBNull.Value) ? reader["lastUpdatedDate"].ToString() : null,
                            LastUpdatedTime = (reader["lastUpdatedTime"] != System.DBNull.Value) ? reader["lastUpdatedTime"].ToString() : null,
                            BranchCode = (reader["branchCode"] != System.DBNull.Value) ? reader["branchCode"].ToString() : null,
                            Type = (reader["type"] != System.DBNull.Value) ? reader["type"].ToString() : null,
                            CreatedBy = (reader["createdBy"] != System.DBNull.Value) ? reader["createdBy"].ToString() : null,
                            TranDate = (reader["tranDate"] != System.DBNull.Value) ? reader["tranDate"].ToString() : null,
                            TranTime = (reader["tranTime"] != System.DBNull.Value) ? reader["tranTime"].ToString() : null,
                            Remarks = (reader["remarks"] != System.DBNull.Value) ? reader["remarks"].ToString() : null,
                            TotalAmount = (reader["totalAmount"] != System.DBNull.Value) ? Convert.ToDecimal(reader["totalAmount"].ToString()) : 0,
                            Discount = (reader["discount"] != System.DBNull.Value) ? Convert.ToDecimal(reader["discount"].ToString()) : 0,
                            AmountPerQty = (reader["amountPerQty"] != System.DBNull.Value) ? Convert.ToDecimal(reader["amountPerQty"].ToString()) : 0,
                            Qty = (reader["qty"] != System.DBNull.Value) ? Convert.ToInt32(reader["qty"].ToString()) : 0,
                            Active = (reader["active"] != System.DBNull.Value) ? Convert.ToInt32(reader["active"].ToString()) : 0
                        });
                    }
                }
            }

            return Task.FromResult<IEnumerable<InventoryDetails>>(result);
        }

        public Task<IEnumerable<InventoryDetails>> GetVoidByDate(string startDate, string endDate)
        {
            var result = new List<InventoryDetails>();

            using (SqlConnection SQLConnect = new SqlConnection(nekonoAppConfig.Value.DbConn))
            {
                SQLConnect.Open();

                SqlCommand SQLCommand = new SqlCommand(StoredProcedures.VoidsByDateSP, SQLConnect);

                SQLCommand.CommandType = System.Data.CommandType.StoredProcedure;

                SQLCommand.Parameters.AddWithValue("@startDate", startDate);
                SQLCommand.Parameters.AddWithValue("@endDate", endDate);

                SqlDataReader reader = SQLCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result.Add(new InventoryDetails
                        {
                            CollectionReceiptNo = (reader["collectionReceiptNo"] != System.DBNull.Value) ? reader["collectionReceiptNo"].ToString() : null,
                            InventoryNo = (reader["inventoryNo"] != System.DBNull.Value) ? reader["inventoryNo"].ToString() : null,
                            ItemNo = (reader["itemNo"] != System.DBNull.Value) ? reader["itemNo"].ToString() : null,
                            ItemName = (reader["itemName"] != System.DBNull.Value) ? reader["itemName"].ToString() : null,
                            ItemDesc = (reader["itemDesc"] != System.DBNull.Value) ? reader["itemDesc"].ToString() : null,
                            VendorCode = (reader["vendorCode"] != System.DBNull.Value) ? reader["vendorCode"].ToString() : null,
                            VendorName = (reader["vendorName"] != System.DBNull.Value) ? reader["vendorName"].ToString() : null,
                            LastUpdatedBy = (reader["lastUpdatedBy"] != System.DBNull.Value) ? reader["lastUpdatedBy"].ToString() : null,
                            LastUpdatedDate = (reader["lastUpdatedDate"] != System.DBNull.Value) ? reader["lastUpdatedDate"].ToString() : null,
                            LastUpdatedTime = (reader["lastUpdatedTime"] != System.DBNull.Value) ? reader["lastUpdatedTime"].ToString() : null,
                            BranchCode = (reader["branchCode"] != System.DBNull.Value) ? reader["branchCode"].ToString() : null,
                            Type = (reader["type"] != System.DBNull.Value) ? reader["type"].ToString() : null,
                            CreatedBy = (reader["createdBy"] != System.DBNull.Value) ? reader["createdBy"].ToString() : null,
                            TranDate = (reader["tranDate"] != System.DBNull.Value) ? reader["tranDate"].ToString() : null,
                            TranTime = (reader["tranTime"] != System.DBNull.Value) ? reader["tranTime"].ToString() : null,
                            Remarks = (reader["remarks"] != System.DBNull.Value) ? reader["remarks"].ToString() : null,
                            TotalAmount = (reader["totalAmount"] != System.DBNull.Value) ? Convert.ToDecimal(reader["totalAmount"].ToString()) : 0,
                            Discount = (reader["discount"] != System.DBNull.Value) ? Convert.ToDecimal(reader["discount"].ToString()) : 0,
                            AmountPerQty = (reader["amountPerQty"] != System.DBNull.Value) ? Convert.ToDecimal(reader["amountPerQty"].ToString()) : 0,
                            Qty = (reader["qty"] != System.DBNull.Value) ? Convert.ToInt32(reader["qty"].ToString()) : 0
                        });
                    }
                }
            }

            return Task.FromResult<IEnumerable<InventoryDetails>>(result);
        }

        public Task<IEnumerable<TopSalesItemDetails>> GetTopSalesAmountItemByDate(int top, string startDate, string endDate)
        {
            var result = new List<TopSalesItemDetails>();

            using (SqlConnection SQLConnect = new SqlConnection(nekonoAppConfig.Value.DbConn))
            {
                SQLConnect.Open();

                SqlCommand SQLCommand = new SqlCommand(StoredProcedures.TopSalesAmountItemByDate, SQLConnect);

                SQLCommand.CommandType = System.Data.CommandType.StoredProcedure;

                SQLCommand.Parameters.AddWithValue("@startDate", startDate);
                SQLCommand.Parameters.AddWithValue("@endDate", endDate);
                SQLCommand.Parameters.AddWithValue("@top", top);

                SqlDataReader reader = SQLCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result.Add(new TopSalesItemDetails
                        {
                            ItemNo = (reader["itemNo"] != System.DBNull.Value) ? reader["itemNo"].ToString() : null,
                            ItemName = (reader["itemName"] != System.DBNull.Value) ? reader["itemName"].ToString() : null,
                            ItemDesc = (reader["itemDesc"] != System.DBNull.Value) ? reader["itemDesc"].ToString() : null,
                            VendorCode = (reader["vendorCode"] != System.DBNull.Value) ? reader["vendorCode"].ToString() : null,
                            VendorName = (reader["vendorName"] != System.DBNull.Value) ? reader["vendorName"].ToString() : null,
                            TotalAmount = (reader["totalAmount"] != System.DBNull.Value) ? Convert.ToDecimal(reader["totalAmount"].ToString()) : 0,
                            TotalQty = (reader["totalQty"] != System.DBNull.Value) ? Convert.ToInt32(reader["totalQty"].ToString()) : 0
                        });
                    }
                }
            }

            return Task.FromResult<IEnumerable<TopSalesItemDetails>>(result);
        }

        public Task<IEnumerable<TopSalesItemDetails>> GetTopSalesVolumeItemByDate(int top, string startDate, string endDate)
        {
            var result = new List<TopSalesItemDetails>();

            using (SqlConnection SQLConnect = new SqlConnection(nekonoAppConfig.Value.DbConn))
            {
                SQLConnect.Open();

                SqlCommand SQLCommand = new SqlCommand(StoredProcedures.TopSalesVolumeItemByDate, SQLConnect);

                SQLCommand.CommandType = System.Data.CommandType.StoredProcedure;

                SQLCommand.Parameters.AddWithValue("@startDate", startDate);
                SQLCommand.Parameters.AddWithValue("@endDate", endDate);
                SQLCommand.Parameters.AddWithValue("@top", top);

                SqlDataReader reader = SQLCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result.Add(new TopSalesItemDetails
                        {
                            ItemNo = (reader["itemNo"] != System.DBNull.Value) ? reader["itemNo"].ToString() : null,
                            ItemName = (reader["itemName"] != System.DBNull.Value) ? reader["itemName"].ToString() : null,
                            ItemDesc = (reader["itemDesc"] != System.DBNull.Value) ? reader["itemDesc"].ToString() : null,
                            VendorCode = (reader["vendorCode"] != System.DBNull.Value) ? reader["vendorCode"].ToString() : null,
                            VendorName = (reader["vendorName"] != System.DBNull.Value) ? reader["vendorName"].ToString() : null,
                            TotalAmount = (reader["totalAmount"] != System.DBNull.Value) ? Convert.ToDecimal(reader["totalAmount"].ToString()) : 0,
                            TotalQty = (reader["totalQty"] != System.DBNull.Value) ? Convert.ToInt32(reader["totalQty"].ToString()) : 0
                        });
                    }
                }
            }

            return Task.FromResult<IEnumerable<TopSalesItemDetails>>(result);
        }

        public Task<bool> Insert(InventoryDetails details, string createdBy, string collectionReceiptNo = "")
        {
            bool result = false;

            using (SqlConnection SQLConnect = new SqlConnection(nekonoAppConfig.Value.DbConn))
            {
                SQLConnect.Open();

                SqlCommand SQLCommand = new SqlCommand(StoredProcedures.InventoryInsertSP, SQLConnect);

                SQLCommand.CommandType = System.Data.CommandType.StoredProcedure;

                SQLCommand.Parameters.AddWithValue("@itemNo", details.ItemNo);
                SQLCommand.Parameters.AddWithValue("@type", details.Type);
                SQLCommand.Parameters.AddWithValue("@qty", details.Qty);
                SQLCommand.Parameters.AddWithValue("@tranDate", Variable.TranDate());
                SQLCommand.Parameters.AddWithValue("@tranTime", Variable.TranTime());
                SQLCommand.Parameters.AddWithValue("@createdBy", createdBy);
                SQLCommand.Parameters.AddWithValue("@amountPerQty", details.AmountPerQty);
                SQLCommand.Parameters.AddWithValue("@collectionReceiptNo", collectionReceiptNo);
                SQLCommand.Parameters.AddWithValue("@branchCode", details.BranchCode);
                SQLCommand.Parameters.AddWithValue("@discount", details.Discount);

                if (SQLCommand.ExecuteNonQuery() != 0)
                {
                    result = true;
                }
            }

            return Task.FromResult(result);
        }

        public Task<bool> Update(InventoryDetails details, string updatedBy)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(DeleteRequest request, string updatedBy)
        {
            bool result = false;

            using (SqlConnection SQLConnect = new SqlConnection(nekonoAppConfig.Value.DbConn))
            {
                SQLConnect.Open();

                SqlCommand SQLCommand = new SqlCommand(StoredProcedures.InventoryDeleteSP, SQLConnect);

                SQLCommand.CommandType = System.Data.CommandType.StoredProcedure;

                SQLCommand.Parameters.AddWithValue("@inventoryNo", request.Id);
                SQLCommand.Parameters.AddWithValue("@remarks", request.Remarks);
                SQLCommand.Parameters.AddWithValue("@lastUpdatedDate", Variable.TranDate());
                SQLCommand.Parameters.AddWithValue("@lastUpdatedTime", Variable.TranTime());
                SQLCommand.Parameters.AddWithValue("@lastUpdatedBy", updatedBy);

                if (SQLCommand.ExecuteNonQuery() != 0)
                {
                    result = true;
                }

                SQLConnect.Close();
            }

            return Task.FromResult(result);
        }
    }
}
