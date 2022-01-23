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
    public class ItemServices : IItemServices
    {
        private readonly IOptions<NekonoAppConfig> nekonoAppConfig;

        public ItemServices(IOptions<NekonoAppConfig> nekonoAppConfig)
        {
            this.nekonoAppConfig = nekonoAppConfig ??
              throw new ArgumentNullException(nameof(nekonoAppConfig));
        }

        public Task<IEnumerable<ItemDetails>> GetAllItemDetails()
        {
            var result = new List<ItemDetails>();

            using (SqlConnection SQLConnect = new SqlConnection(nekonoAppConfig.Value.DbConn))
            {
                SQLConnect.Open();
                StringBuilder xSQL = new StringBuilder();

                xSQL.AppendLine("SELECT itemNo ");
                xSQL.AppendLine("       ,itemName");
                xSQL.AppendLine("       ,itemDesc");
                xSQL.AppendLine("       ,itemGroup");
                xSQL.AppendLine("       ,itemGroupDesc");
                xSQL.AppendLine("       ,specification");
                xSQL.AppendLine("       ,vendorCode");
                xSQL.AppendLine("       ,vendorName");
                xSQL.AppendLine("       ,tranDate");
                xSQL.AppendLine("       ,tranTime");
                xSQL.AppendLine("       ,createdBy");
                xSQL.AppendLine("       ,unitPrice");
                xSQL.AppendLine("       ,retailPrice");
                xSQL.AppendLine("       ,wholesalePrice");
                xSQL.AppendLine("       ,retailDiscount");
                xSQL.AppendLine("       ,wholesaleDiscount");
                xSQL.AppendLine("       ,finalRetailPrice");
                xSQL.AppendLine("       ,finalWholesalePrice");
                xSQL.AppendLine("       ,wholesaleMinQty");
                xSQL.AppendLine("       ,availableStock");
                xSQL.AppendLine("FROM ItemDetailsView ");

                SqlCommand SQLCommand = new SqlCommand(xSQL.ToString(), SQLConnect);

                SqlDataReader reader = SQLCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result.Add(new ItemDetails
                        {
                            ItemNo = (reader["itemNo"] != System.DBNull.Value) ? reader["itemNo"].ToString() : null,
                            ItemName = (reader["itemName"] != System.DBNull.Value) ? reader["itemName"].ToString() : null,
                            ItemDesc = (reader["itemDesc"] != System.DBNull.Value) ? reader["itemDesc"].ToString() : null,
                            ItemGroup = (reader["itemGroup"] != System.DBNull.Value) ? reader["itemGroup"].ToString() : null,
                            ItemGroupDesc = (reader["itemGroupDesc"] != System.DBNull.Value) ? reader["itemGroupDesc"].ToString() : null,
                            Specification = (reader["specification"] != System.DBNull.Value) ? reader["specification"].ToString() : null,
                            VendorCode = (reader["vendorCode"] != System.DBNull.Value) ? reader["vendorCode"].ToString() : null,
                            VendorName = (reader["vendorName"] != System.DBNull.Value) ? reader["vendorName"].ToString() : null,
                            TranDate = (reader["tranDate"] != System.DBNull.Value) ? reader["tranDate"].ToString() : null,
                            TranTime = (reader["tranTime"] != System.DBNull.Value) ? reader["tranTime"].ToString() : null,
                            CreatedBy = (reader["createdBy"] != System.DBNull.Value) ? reader["createdBy"].ToString() : null,
                            UnitPrice = (reader["unitPrice"] != System.DBNull.Value) ? Convert.ToDecimal(reader["unitPrice"].ToString()) : 0,
                            RetailPrice = (reader["retailPrice"] != System.DBNull.Value) ? Convert.ToDecimal(reader["retailPrice"].ToString()) : 0,
                            WholesalePrice = (reader["wholesalePrice"] != System.DBNull.Value) ? Convert.ToDecimal(reader["wholesalePrice"].ToString()) : 0,
                            RetailDiscount = (reader["retailDiscount"] != System.DBNull.Value) ? Convert.ToDecimal(reader["retailDiscount"].ToString()) : 0,
                            WholesaleDiscount = (reader["wholesaleDiscount"] != System.DBNull.Value) ? Convert.ToDecimal(reader["wholesaleDiscount"].ToString()) : 0,
                            FinalRetailPrice = (reader["finalRetailPrice"] != System.DBNull.Value) ? Convert.ToDecimal(reader["finalRetailPrice"].ToString()) : 0,
                            FinalWholesalePrice = (reader["finalWholesalePrice"] != System.DBNull.Value) ? Convert.ToDecimal(reader["finalWholesalePrice"].ToString()) : 0,
                            WholesaleMinQty = (reader["wholesaleMinQty"] != System.DBNull.Value) ? Convert.ToInt32(reader["wholesaleMinQty"].ToString()) : 0,
                            AvailableStocks = (reader["availableStock"] != System.DBNull.Value) ? Convert.ToInt32(reader["availableStock"].ToString()) : 0
                        });
                    }
                }
                reader.Close();
            }

            return Task.FromResult<IEnumerable<ItemDetails>>(result);
        }

        public Task<IEnumerable<ItemHistoryDetails>> GetAllItemHistory()
        {
            var result = new List<ItemHistoryDetails>();

            using (SqlConnection SQLConnect = new SqlConnection(nekonoAppConfig.Value.DbConn))
            {
                SQLConnect.Open();
                StringBuilder xSQL = new StringBuilder();

                xSQL.AppendLine("SELECT itemNo ");
                xSQL.AppendLine("       ,itemName");
                xSQL.AppendLine("       ,itemDesc");
                xSQL.AppendLine("       ,itemGroup");
                xSQL.AppendLine("       ,itemGroupDesc");
                xSQL.AppendLine("       ,specification");
                xSQL.AppendLine("       ,vendorCode");
                xSQL.AppendLine("       ,lastUpdatedDate");
                xSQL.AppendLine("       ,lastUpdatedTime");
                xSQL.AppendLine("       ,lastUpdatedBy");
                xSQL.AppendLine("       ,unitPrice");
                xSQL.AppendLine("       ,retailPrice");
                xSQL.AppendLine("       ,wholesalePrice");
                xSQL.AppendLine("       ,retailDiscount");
                xSQL.AppendLine("       ,wholesaleDiscount");
                xSQL.AppendLine("       ,wholesaleMinQty");
                xSQL.AppendLine("FROM ItemsHistory ");

                SqlCommand SQLCommand = new SqlCommand(xSQL.ToString(), SQLConnect);

                SqlDataReader reader = SQLCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result.Add(new ItemHistoryDetails
                        {
                            ItemNo = (reader["itemNo"] != System.DBNull.Value) ? reader["itemNo"].ToString() : null,
                            ItemName = (reader["itemName"] != System.DBNull.Value) ? reader["itemName"].ToString() : null,
                            ItemDesc = (reader["itemDesc"] != System.DBNull.Value) ? reader["itemDesc"].ToString() : null,
                            ItemGroup = (reader["itemGroup"] != System.DBNull.Value) ? reader["itemGroup"].ToString() : null,
                            ItemGroupDesc = (reader["itemGroupDesc"] != System.DBNull.Value) ? reader["itemGroupDesc"].ToString() : null,
                            Specification = (reader["specification"] != System.DBNull.Value) ? reader["specification"].ToString() : null,
                            VendorCode = (reader["vendorCode"] != System.DBNull.Value) ? reader["vendorCode"].ToString() : null,
                            LastUpdatedBy = (reader["lastUpdatedBy"] != System.DBNull.Value) ? reader["lastUpdatedBy"].ToString() : null,
                            LastUpdatedDate = (reader["lastUpdatedDate"] != System.DBNull.Value) ? reader["lastUpdatedDate"].ToString() : null,
                            LastUpdatedTime = (reader["lastUpdatedTime"] != System.DBNull.Value) ? reader["lastUpdatedTime"].ToString() : null,
                            UnitPrice = (reader["unitPrice"] != System.DBNull.Value) ? Convert.ToDecimal(reader["unitPrice"].ToString()) : 0,
                            RetailPrice = (reader["retailPrice"] != System.DBNull.Value) ? Convert.ToDecimal(reader["retailPrice"].ToString()) : 0,
                            WholesalePrice = (reader["wholesalePrice"] != System.DBNull.Value) ? Convert.ToDecimal(reader["wholesalePrice"].ToString()) : 0,
                            RetailDiscount = (reader["retailDiscount"] != System.DBNull.Value) ? Convert.ToDecimal(reader["retailDiscount"].ToString()) : 0,
                            WholesaleDiscount = (reader["wholesaleDiscount"] != System.DBNull.Value) ? Convert.ToDecimal(reader["wholesaleDiscount"].ToString()) : 0,
                            WholesaleMinQty = (reader["wholesaleMinQty"] != System.DBNull.Value) ? Convert.ToInt32(reader["wholesaleMinQty"].ToString()) : 0
                        });
                    }
                }
                reader.Close();
            }

            return Task.FromResult<IEnumerable<ItemHistoryDetails>>(result);
        }

        public Task<IEnumerable<ItemInPOSDetails>> GetAllItemInPOSDetails()
        {
            var result = new List<ItemInPOSDetails>();

            using (SqlConnection SQLConnect = new SqlConnection(nekonoAppConfig.Value.DbConn))
            {
                SQLConnect.Open();
                StringBuilder xSQL = new StringBuilder();

                xSQL.AppendLine("SELECT itemNo ");
                xSQL.AppendLine("       ,itemName");
                xSQL.AppendLine("       ,retailDiscount");
                xSQL.AppendLine("       ,wholesaleDiscount");
                xSQL.AppendLine("       ,finalRetailPrice");
                xSQL.AppendLine("       ,finalWholesalePrice");
                xSQL.AppendLine("       ,wholesaleMinQty");
                xSQL.AppendLine("       ,vendorCode");
                xSQL.AppendLine("FROM ItemDetailsView ");

                SqlCommand SQLCommand = new SqlCommand(xSQL.ToString(), SQLConnect);

                SqlDataReader reader = SQLCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result.Add(new ItemInPOSDetails
                        {
                            ItemNo = (reader["itemNo"] != System.DBNull.Value) ? reader["itemNo"].ToString() : null,
                            ItemName = (reader["itemName"] != System.DBNull.Value) ? reader["itemName"].ToString() : null,
                            VendorCode = (reader["vendorCode"] != System.DBNull.Value) ? reader["vendorCode"].ToString() : null,
                            RetailDiscount = (reader["retailDiscount"] != System.DBNull.Value) ? Convert.ToDecimal(reader["retailDiscount"].ToString()) : 0,
                            WholesaleDiscount = (reader["wholesaleDiscount"] != System.DBNull.Value) ? Convert.ToDecimal(reader["wholesaleDiscount"].ToString()) : 0,
                            FinalRetailPrice = (reader["finalRetailPrice"] != System.DBNull.Value) ? Convert.ToDecimal(reader["finalRetailPrice"].ToString()) : 0,
                            FinalWholesalePrice = (reader["finalWholesalePrice"] != System.DBNull.Value) ? Convert.ToDecimal(reader["finalWholesalePrice"].ToString()) : 0,
                            WholesaleMinQty = (reader["wholesaleMinQty"] != System.DBNull.Value) ? Convert.ToInt32(reader["wholesaleMinQty"].ToString()) : 0
                        });
                    }
                }
                reader.Close();
            }

            return Task.FromResult<IEnumerable<ItemInPOSDetails>>(result);
        }

        public Task<ItemDetails> GetItemDetails(string itemNo)
        {
            ItemDetails result = null;

            using (SqlConnection SQLConnect = new SqlConnection(nekonoAppConfig.Value.DbConn))
            {
                SQLConnect.Open();
                StringBuilder xSQL = new StringBuilder();

                xSQL.AppendLine("SELECT itemNo ");
                xSQL.AppendLine("       ,itemName");
                xSQL.AppendLine("       ,itemDesc");
                xSQL.AppendLine("       ,itemGroup");
                xSQL.AppendLine("       ,itemGroupDesc");
                xSQL.AppendLine("       ,specification");
                xSQL.AppendLine("       ,vendorCode");
                xSQL.AppendLine("       ,vendorName");
                xSQL.AppendLine("       ,tranDate");
                xSQL.AppendLine("       ,tranTime");
                xSQL.AppendLine("       ,createdBy");
                xSQL.AppendLine("       ,unitPrice");
                xSQL.AppendLine("       ,retailPrice");
                xSQL.AppendLine("       ,wholesalePrice");
                xSQL.AppendLine("       ,retailDiscount");
                xSQL.AppendLine("       ,wholesaleDiscount");
                xSQL.AppendLine("       ,finalRetailPrice");
                xSQL.AppendLine("       ,finalWholesalePrice");
                xSQL.AppendLine("       ,wholesaleMinQty");
                xSQL.AppendLine("       ,availableStock");
                xSQL.AppendLine("FROM ItemDetailsView ");
                xSQL.AppendLine("WHERE itemNo = @itemNo ");

                SqlCommand SQLCommand = new SqlCommand(xSQL.ToString(), SQLConnect);
                SQLCommand.Parameters.AddWithValue("@itemNo", itemNo);

                SqlDataReader reader = SQLCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result = new ItemDetails
                        {
                            ItemNo = (reader["itemNo"] != System.DBNull.Value) ? reader["itemNo"].ToString() : null,
                            ItemName = (reader["itemName"] != System.DBNull.Value) ? reader["itemName"].ToString() : null,
                            ItemDesc = (reader["itemDesc"] != System.DBNull.Value) ? reader["itemDesc"].ToString() : null,
                            ItemGroup = (reader["itemGroup"] != System.DBNull.Value) ? reader["itemGroup"].ToString() : null,
                            ItemGroupDesc = (reader["itemGroupDesc"] != System.DBNull.Value) ? reader["itemGroupDesc"].ToString() : null,
                            Specification = (reader["specification"] != System.DBNull.Value) ? reader["specification"].ToString() : null,
                            VendorCode = (reader["vendorCode"] != System.DBNull.Value) ? reader["vendorCode"].ToString() : null,
                            VendorName = (reader["vendorName"] != System.DBNull.Value) ? reader["vendorName"].ToString() : null,
                            TranDate = (reader["tranDate"] != System.DBNull.Value) ? reader["tranDate"].ToString() : null,
                            TranTime = (reader["tranTime"] != System.DBNull.Value) ? reader["tranTime"].ToString() : null,
                            CreatedBy = (reader["createdBy"] != System.DBNull.Value) ? reader["createdBy"].ToString() : null,
                            UnitPrice = (reader["unitPrice"] != System.DBNull.Value) ? Convert.ToDecimal(reader["unitPrice"].ToString()) : 0,
                            RetailPrice = (reader["retailPrice"] != System.DBNull.Value) ? Convert.ToDecimal(reader["retailPrice"].ToString()) : 0,
                            WholesalePrice = (reader["wholesalePrice"] != System.DBNull.Value) ? Convert.ToDecimal(reader["wholesalePrice"].ToString()) : 0,
                            RetailDiscount = (reader["retailDiscount"] != System.DBNull.Value) ? Convert.ToDecimal(reader["retailDiscount"].ToString()) : 0,
                            WholesaleDiscount = (reader["wholesaleDiscount"] != System.DBNull.Value) ? Convert.ToDecimal(reader["wholesaleDiscount"].ToString()) : 0,
                            FinalRetailPrice = (reader["finalRetailPrice"] != System.DBNull.Value) ? Convert.ToDecimal(reader["finalRetailPrice"].ToString()) : 0,
                            FinalWholesalePrice = (reader["finalWholesalePrice"] != System.DBNull.Value) ? Convert.ToDecimal(reader["finalWholesalePrice"].ToString()) : 0,
                            WholesaleMinQty = (reader["wholesaleMinQty"] != System.DBNull.Value) ? Convert.ToInt32(reader["wholesaleMinQty"].ToString()) : 0,
                            AvailableStocks = (reader["availableStock"] != System.DBNull.Value) ? Convert.ToInt32(reader["availableStock"].ToString()) : 0
                        };
                    }
                }
                reader.Close();
            }

            return Task.FromResult(result);
        }

        public Task<IEnumerable<ItemHistoryDetails>> GetItemHistory(string itemNo)
        {
            var result = new List<ItemHistoryDetails>();

            using (SqlConnection SQLConnect = new SqlConnection(nekonoAppConfig.Value.DbConn))
            {
                SQLConnect.Open();
                StringBuilder xSQL = new StringBuilder();

                xSQL.AppendLine("SELECT itemNo ");
                xSQL.AppendLine("       ,itemName");
                xSQL.AppendLine("       ,itemDesc");
                xSQL.AppendLine("       ,itemGroup");
                xSQL.AppendLine("       ,itemGroupDesc");
                xSQL.AppendLine("       ,specification");
                xSQL.AppendLine("       ,vendorCode");
                xSQL.AppendLine("       ,lastUpdatedDate");
                xSQL.AppendLine("       ,lastUpdatedTime");
                xSQL.AppendLine("       ,lastUpdatedBy");
                xSQL.AppendLine("       ,unitPrice");
                xSQL.AppendLine("       ,retailPrice");
                xSQL.AppendLine("       ,wholesalePrice");
                xSQL.AppendLine("       ,retailDiscount");
                xSQL.AppendLine("       ,wholesaleDiscount");
                xSQL.AppendLine("       ,wholesaleMinQty");
                xSQL.AppendLine("FROM ItemsHistory ");
                xSQL.AppendLine("WHERE itemNo = @itemNo ");

                SqlCommand SQLCommand = new SqlCommand(xSQL.ToString(), SQLConnect);
                SQLCommand.Parameters.AddWithValue("@itemNo", itemNo);

                SqlDataReader reader = SQLCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result.Add(new ItemHistoryDetails
                        {
                            ItemNo = (reader["itemNo"] != System.DBNull.Value) ? reader["itemNo"].ToString() : null,
                            ItemName = (reader["itemName"] != System.DBNull.Value) ? reader["itemName"].ToString() : null,
                            ItemDesc = (reader["itemDesc"] != System.DBNull.Value) ? reader["itemDesc"].ToString() : null,
                            ItemGroup = (reader["itemGroup"] != System.DBNull.Value) ? reader["itemGroup"].ToString() : null,
                            ItemGroupDesc = (reader["itemGroupDesc"] != System.DBNull.Value) ? reader["itemGroupDesc"].ToString() : null,
                            Specification = (reader["specification"] != System.DBNull.Value) ? reader["specification"].ToString() : null,
                            VendorCode = (reader["vendorCode"] != System.DBNull.Value) ? reader["vendorCode"].ToString() : null,
                            LastUpdatedBy = (reader["lastUpdatedBy"] != System.DBNull.Value) ? reader["lastUpdatedBy"].ToString() : null,
                            LastUpdatedDate = (reader["lastUpdatedDate"] != System.DBNull.Value) ? reader["lastUpdatedDate"].ToString() : null,
                            LastUpdatedTime = (reader["lastUpdatedTime"] != System.DBNull.Value) ? reader["lastUpdatedTime"].ToString() : null,
                            UnitPrice = (reader["unitPrice"] != System.DBNull.Value) ? Convert.ToDecimal(reader["unitPrice"].ToString()) : 0,
                            RetailPrice = (reader["retailPrice"] != System.DBNull.Value) ? Convert.ToDecimal(reader["retailPrice"].ToString()) : 0,
                            WholesalePrice = (reader["wholesalePrice"] != System.DBNull.Value) ? Convert.ToDecimal(reader["wholesalePrice"].ToString()) : 0,
                            RetailDiscount = (reader["retailDiscount"] != System.DBNull.Value) ? Convert.ToDecimal(reader["retailDiscount"].ToString()) : 0,
                            WholesaleDiscount = (reader["wholesaleDiscount"] != System.DBNull.Value) ? Convert.ToDecimal(reader["wholesaleDiscount"].ToString()) : 0,
                            WholesaleMinQty = (reader["wholesaleMinQty"] != System.DBNull.Value) ? Convert.ToInt32(reader["wholesaleMinQty"].ToString()) : 0
                        });
                    }
                }
                reader.Close();
            }

            return Task.FromResult<IEnumerable<ItemHistoryDetails>>(result);
        }

        public Task<ItemInPOSDetails> GetItemInPOSDetails(string itemNo)
        {
            ItemInPOSDetails result = null;

            using (SqlConnection SQLConnect = new SqlConnection(nekonoAppConfig.Value.DbConn))
            {
                SQLConnect.Open();
                StringBuilder xSQL = new StringBuilder();

                xSQL.AppendLine("SELECT itemNo ");
                xSQL.AppendLine("       ,itemName");
                xSQL.AppendLine("       ,retailDiscount");
                xSQL.AppendLine("       ,wholesaleDiscount");
                xSQL.AppendLine("       ,finalRetailPrice");
                xSQL.AppendLine("       ,finalWholesalePrice");
                xSQL.AppendLine("       ,wholesaleMinQty");
                xSQL.AppendLine("       ,vendorCode");
                xSQL.AppendLine("FROM ItemDetailsView ");
                xSQL.AppendLine("WHERE itemNo = @itemNo ");

                SqlCommand SQLCommand = new SqlCommand(xSQL.ToString(), SQLConnect);
                SQLCommand.Parameters.AddWithValue("@itemNo", itemNo);

                SqlDataReader reader = SQLCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result = new ItemInPOSDetails
                        {
                            ItemNo = (reader["itemNo"] != System.DBNull.Value) ? reader["itemNo"].ToString() : null,
                            ItemName = (reader["itemName"] != System.DBNull.Value) ? reader["itemName"].ToString() : null,
                            VendorCode = (reader["vendorCode"] != System.DBNull.Value) ? reader["vendorCode"].ToString() : null,
                            RetailDiscount = (reader["retailDiscount"] != System.DBNull.Value) ? Convert.ToDecimal(reader["retailDiscount"].ToString()) : 0,
                            WholesaleDiscount = (reader["wholesaleDiscount"] != System.DBNull.Value) ? Convert.ToDecimal(reader["wholesaleDiscount"].ToString()) : 0,
                            FinalRetailPrice = (reader["finalRetailPrice"] != System.DBNull.Value) ? Convert.ToDecimal(reader["finalRetailPrice"].ToString()) : 0,
                            FinalWholesalePrice = (reader["finalWholesalePrice"] != System.DBNull.Value) ? Convert.ToDecimal(reader["finalWholesalePrice"].ToString()) : 0,
                            WholesaleMinQty = (reader["wholesaleMinQty"] != System.DBNull.Value) ? Convert.ToInt32(reader["wholesaleMinQty"].ToString()) : 0
                        };
                    }
                }
                reader.Close();
            }

            return Task.FromResult(result);
        }

        public Task<bool> Insert(ItemDetails details, string createdBy)
        {
            bool result = false;

            using (SqlConnection SQLConnect = new SqlConnection(nekonoAppConfig.Value.DbConn))
            {
                SQLConnect.Open();

                SqlCommand SQLCommand = new SqlCommand(StoredProcedures.ItemInsertSP, SQLConnect);

                SQLCommand.CommandType = System.Data.CommandType.StoredProcedure;

                SQLCommand.Parameters.AddWithValue("@itemName", details.ItemName);
                SQLCommand.Parameters.AddWithValue("@itemDesc", details.ItemDesc);
                SQLCommand.Parameters.AddWithValue("@itemGroup", details.ItemGroup);
                SQLCommand.Parameters.AddWithValue("@itemGroupDesc", details.ItemGroupDesc ?? "");
                SQLCommand.Parameters.AddWithValue("@specification", details.Specification ?? "");
                SQLCommand.Parameters.AddWithValue("@vendorCode", details.VendorCode);
                SQLCommand.Parameters.AddWithValue("@tranDate", Variable.TranDate());
                SQLCommand.Parameters.AddWithValue("@tranTime", Variable.TranTime());
                SQLCommand.Parameters.AddWithValue("@createdBy", createdBy);
                SQLCommand.Parameters.AddWithValue("@unitPrice", details.UnitPrice);
                SQLCommand.Parameters.AddWithValue("@retailPrice", details.RetailPrice);
                SQLCommand.Parameters.AddWithValue("@wholesalePrice", details.WholesalePrice);
                SQLCommand.Parameters.AddWithValue("@retailDiscount", details.RetailDiscount);
                SQLCommand.Parameters.AddWithValue("@wholesaleDiscount", details.WholesaleDiscount);
                SQLCommand.Parameters.AddWithValue("@wholesaleMinQty", details.WholesaleMinQty);

                if (SQLCommand.ExecuteNonQuery() != 0)
                {
                    result = true;
                }
            }

            return Task.FromResult(result);
        }

        public Task<bool> Update(ItemDetails details, string updatedBy)
        {
            bool result = false;

            using (SqlConnection SQLConnect = new SqlConnection(nekonoAppConfig.Value.DbConn))
            {
                SQLConnect.Open();

                SqlCommand SQLCommand = new SqlCommand(StoredProcedures.ItemUpdateSP, SQLConnect);

                SQLCommand.CommandType = System.Data.CommandType.StoredProcedure;

                SQLCommand.Parameters.AddWithValue("@itemNo", details.ItemNo);
                SQLCommand.Parameters.AddWithValue("@itemName", details.ItemName);
                SQLCommand.Parameters.AddWithValue("@itemDesc", details.ItemDesc);
                SQLCommand.Parameters.AddWithValue("@itemGroup", details.ItemGroup);
                SQLCommand.Parameters.AddWithValue("@itemGroupDesc", details.ItemGroupDesc ?? "");
                SQLCommand.Parameters.AddWithValue("@specification", details.Specification ?? "");
                SQLCommand.Parameters.AddWithValue("@vendorCode", details.VendorCode);
                SQLCommand.Parameters.AddWithValue("@unitPrice", details.UnitPrice);
                SQLCommand.Parameters.AddWithValue("@retailPrice", details.RetailPrice);
                SQLCommand.Parameters.AddWithValue("@wholesalePrice", details.WholesalePrice);
                SQLCommand.Parameters.AddWithValue("@retailDiscount", details.RetailDiscount);
                SQLCommand.Parameters.AddWithValue("@wholesaleDiscount", details.WholesaleDiscount);
                SQLCommand.Parameters.AddWithValue("@wholesaleMinQty", details.WholesaleMinQty);
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

        public Task<bool> Delete(DeleteRequest request, string updatedBy)
        {
            bool result = false;

            using (SqlConnection SQLConnect = new SqlConnection(nekonoAppConfig.Value.DbConn))
            {
                SQLConnect.Open();

                SqlCommand SQLCommand = new SqlCommand(StoredProcedures.ItemDeleteSP, SQLConnect);

                SQLCommand.CommandType = System.Data.CommandType.StoredProcedure;

                SQLCommand.Parameters.AddWithValue("@itemNo", request.Id);
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
