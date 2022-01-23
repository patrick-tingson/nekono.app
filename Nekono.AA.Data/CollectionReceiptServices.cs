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
    public class CollectionReceiptServices : ICollectionReceiptsServices
    {
        private readonly IOptions<NekonoAppConfig> nekonoAppConfig;

        public CollectionReceiptServices(IOptions<NekonoAppConfig> nekonoAppConfig)
        {
            this.nekonoAppConfig = nekonoAppConfig ??
              throw new ArgumentNullException(nameof(nekonoAppConfig));
        }

        public Task<string> Sale(CollectionReceiptDetails details, string createdBy)
        {
            string result = "";

            using (SqlConnection SQLConnect = new SqlConnection(nekonoAppConfig.Value.DbConn))
            {
                SQLConnect.Open();

                SqlCommand SQLCommand = new SqlCommand(StoredProcedures.CollectionRecieptInsertSP, SQLConnect);

                SQLCommand.CommandType = System.Data.CommandType.StoredProcedure;

                SQLCommand.Parameters.AddWithValue("@tranDate", Variable.TranDate());
                SQLCommand.Parameters.AddWithValue("@tranTime", Variable.TranTime());
                SQLCommand.Parameters.AddWithValue("@createdBy", createdBy);
                SQLCommand.Parameters.AddWithValue("@totalAmount", details.TotalAmount);
                SQLCommand.Parameters.AddWithValue("@branchCode", details.BranchCode);
                SQLCommand.Parameters.AddWithValue("@type", details.Type);
                SQLCommand.Parameters.AddWithValue("@refNo", details.RefNo);
                SQLCommand.Parameters.AddWithValue("@bank", details.Bank);

                SqlDataReader reader = SQLCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result = (reader["collectionReceiptNo"] != System.DBNull.Value) ? reader["collectionReceiptNo"].ToString() : null;
                    }
                }
            }

            return Task.FromResult(result);
        }

        public Task<bool> Void(DeleteRequest request, string updatedBy)
        {
            bool result = false;

            using (SqlConnection SQLConnect = new SqlConnection(nekonoAppConfig.Value.DbConn))
            {
                SQLConnect.Open();

                SqlCommand SQLCommand = new SqlCommand(StoredProcedures.CollectionRecieptVoidSP, SQLConnect);

                SQLCommand.CommandType = System.Data.CommandType.StoredProcedure;

                SQLCommand.Parameters.AddWithValue("@collectionReceiptNo", request.Id);
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

        public Task<CollectionReceiptDetails> GetByCollectionReceiptNo(string collectionReceiptNo)
        {
            CollectionReceiptDetails result = null;

            using (SqlConnection SQLConnect = new SqlConnection(nekonoAppConfig.Value.DbConn))
            {
                SQLConnect.Open();
                StringBuilder xSQL = new StringBuilder();

                xSQL.AppendLine("SELECT * ");
                xSQL.AppendLine("FROM CollectionReceipts ");
                xSQL.AppendLine("WHERE collectionReceiptNo = @collectionReceiptNo ");

                SqlCommand SQLCommand = new SqlCommand(xSQL.ToString(), SQLConnect);
                SQLCommand.Parameters.AddWithValue("@collectionReceiptNo", collectionReceiptNo);

                SqlDataReader reader = SQLCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result = new CollectionReceiptDetails
                        {
                            CollectionReceiptNo = (reader["collectionReceiptNo"] != System.DBNull.Value) ? reader["collectionReceiptNo"].ToString() : null,
                            LastUpdatedBy = (reader["lastUpdatedBy"] != System.DBNull.Value) ? reader["lastUpdatedBy"].ToString() : null,
                            LastUpdatedDate = (reader["lastUpdatedDate"] != System.DBNull.Value) ? reader["lastUpdatedDate"].ToString() : null,
                            LastUpdatedTime = (reader["lastUpdatedTime"] != System.DBNull.Value) ? reader["lastUpdatedTime"].ToString() : null,
                            Remarks = (reader["remarks"] != System.DBNull.Value) ? reader["remarks"].ToString() : null,
                            BranchCode = (reader["branchCode"] != System.DBNull.Value) ? reader["branchCode"].ToString() : null,
                            TranDate = (reader["tranDate"] != System.DBNull.Value) ? reader["tranDate"].ToString() : null,
                            TranTime = (reader["tranTime"] != System.DBNull.Value) ? reader["tranTime"].ToString() : null,
                            CreatedBy = (reader["createdBy"] != System.DBNull.Value) ? reader["createdBy"].ToString() : null,
                            Type = (reader["type"] != System.DBNull.Value) ? reader["type"].ToString() : null,
                            RefNo = (reader["refNo"] != System.DBNull.Value) ? reader["refNo"].ToString() : null,
                            Bank = (reader["bank"] != System.DBNull.Value) ? reader["bank"].ToString() : null,
                            TotalAmount = (reader["totalAmount"] != System.DBNull.Value) ? Convert.ToDecimal(reader["totalAmount"].ToString()) : 0,
                            Active = (reader["active"] != System.DBNull.Value) ? Convert.ToInt32(reader["active"].ToString()) : 0
                        };
                    }
                }
                reader.Close();
            }

            return Task.FromResult(result);
        }

        public Task<IEnumerable<CollectionReceiptDetails>> GetByDate(string startDate, string endDate)
        {
            var result = new List<CollectionReceiptDetails>();

            using (SqlConnection SQLConnect = new SqlConnection(nekonoAppConfig.Value.DbConn))
            {
                SQLConnect.Open();
                StringBuilder xSQL = new StringBuilder();

                xSQL.AppendLine("SELECT * ");
                xSQL.AppendLine("FROM CollectionReceipts ");
                xSQL.AppendLine("WHERE tranDate BETWEEN @startDate AND @endDate ");

                SqlCommand SQLCommand = new SqlCommand(xSQL.ToString(), SQLConnect);
                SQLCommand.Parameters.AddWithValue("@startDate", startDate);
                SQLCommand.Parameters.AddWithValue("@endDate", endDate);

                SqlDataReader reader = SQLCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result.Add(new CollectionReceiptDetails
                        {
                            CollectionReceiptNo = (reader["collectionReceiptNo"] != System.DBNull.Value) ? reader["collectionReceiptNo"].ToString() : null,
                            LastUpdatedBy = (reader["lastUpdatedBy"] != System.DBNull.Value) ? reader["lastUpdatedBy"].ToString() : null,
                            LastUpdatedDate = (reader["lastUpdatedDate"] != System.DBNull.Value) ? reader["lastUpdatedDate"].ToString() : null,
                            LastUpdatedTime = (reader["lastUpdatedTime"] != System.DBNull.Value) ? reader["lastUpdatedTime"].ToString() : null,
                            Remarks = (reader["remarks"] != System.DBNull.Value) ? reader["remarks"].ToString() : null,
                            BranchCode = (reader["branchCode"] != System.DBNull.Value) ? reader["branchCode"].ToString() : null,
                            TranDate = (reader["tranDate"] != System.DBNull.Value) ? reader["tranDate"].ToString() : null,
                            TranTime = (reader["tranTime"] != System.DBNull.Value) ? reader["tranTime"].ToString() : null,
                            CreatedBy = (reader["createdBy"] != System.DBNull.Value) ? reader["createdBy"].ToString() : null,
                            Type = (reader["type"] != System.DBNull.Value) ? reader["type"].ToString() : null,
                            RefNo = (reader["refNo"] != System.DBNull.Value) ? reader["refNo"].ToString() : null,
                            Bank = (reader["bank"] != System.DBNull.Value) ? reader["bank"].ToString() : null,
                            TotalAmount = (reader["totalAmount"] != System.DBNull.Value) ? Convert.ToDecimal(reader["totalAmount"].ToString()) : 0,
                            Active = (reader["active"] != System.DBNull.Value) ? Convert.ToInt32(reader["active"].ToString()) : 0
                        });
                    }
                }
                reader.Close();
            }

            return Task.FromResult<IEnumerable<CollectionReceiptDetails>>(result);
        }

        public Task<IEnumerable<CollectionReceiptDetails>> GetAll()
        {
            var result = new List<CollectionReceiptDetails>();

            using (SqlConnection SQLConnect = new SqlConnection(nekonoAppConfig.Value.DbConn))
            {
                SQLConnect.Open();
                StringBuilder xSQL = new StringBuilder();

                xSQL.AppendLine("SELECT * ");
                xSQL.AppendLine("FROM CollectionReceipts ");

                SqlCommand SQLCommand = new SqlCommand(xSQL.ToString(), SQLConnect);

                SqlDataReader reader = SQLCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result.Add(new CollectionReceiptDetails
                        {
                            CollectionReceiptNo = (reader["collectionReceiptNo"] != System.DBNull.Value) ? reader["collectionReceiptNo"].ToString() : null,
                            LastUpdatedBy = (reader["lastUpdatedBy"] != System.DBNull.Value) ? reader["lastUpdatedBy"].ToString() : null,
                            LastUpdatedDate = (reader["lastUpdatedDate"] != System.DBNull.Value) ? reader["lastUpdatedDate"].ToString() : null,
                            LastUpdatedTime = (reader["lastUpdatedTime"] != System.DBNull.Value) ? reader["lastUpdatedTime"].ToString() : null,
                            Remarks = (reader["remarks"] != System.DBNull.Value) ? reader["remarks"].ToString() : null,
                            BranchCode = (reader["branchCode"] != System.DBNull.Value) ? reader["branchCode"].ToString() : null,
                            TranDate = (reader["tranDate"] != System.DBNull.Value) ? reader["tranDate"].ToString() : null,
                            TranTime = (reader["tranTime"] != System.DBNull.Value) ? reader["tranTime"].ToString() : null,
                            CreatedBy = (reader["createdBy"] != System.DBNull.Value) ? reader["createdBy"].ToString() : null,
                            Type = (reader["type"] != System.DBNull.Value) ? reader["type"].ToString() : null,
                            RefNo = (reader["refNo"] != System.DBNull.Value) ? reader["refNo"].ToString() : null,
                            Bank = (reader["bank"] != System.DBNull.Value) ? reader["bank"].ToString() : null,
                            TotalAmount = (reader["totalAmount"] != System.DBNull.Value) ? Convert.ToDecimal(reader["totalAmount"].ToString()) : 0,
                            Active = (reader["active"] != System.DBNull.Value) ? Convert.ToInt32(reader["active"].ToString()) : 0
                        });
                    }
                }
                reader.Close();
            }

            return Task.FromResult<IEnumerable<CollectionReceiptDetails>>(result);
        }

        public Task<IEnumerable<CollectionReceiptDetails>> GetToday(string createdBy)
        {
            var result = new List<CollectionReceiptDetails>();

            using (SqlConnection SQLConnect = new SqlConnection(nekonoAppConfig.Value.DbConn))
            {
                SQLConnect.Open();
                StringBuilder xSQL = new StringBuilder();

                xSQL.AppendLine("SELECT * ");
                xSQL.AppendLine("FROM CollectionReceipts ");
                xSQL.AppendLine("WHERE createdBy = @createdBy ");
                xSQL.AppendLine("   AND tranDate = @tranDate ");

                SqlCommand SQLCommand = new SqlCommand(xSQL.ToString(), SQLConnect);
                SQLCommand.Parameters.AddWithValue("@createdBy", createdBy);
                SQLCommand.Parameters.AddWithValue("@tranDate", Variable.TranDate());

                SqlDataReader reader = SQLCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result.Add(new CollectionReceiptDetails
                        {
                            CollectionReceiptNo = (reader["collectionReceiptNo"] != System.DBNull.Value) ? reader["collectionReceiptNo"].ToString() : null,
                            LastUpdatedBy = (reader["lastUpdatedBy"] != System.DBNull.Value) ? reader["lastUpdatedBy"].ToString() : null,
                            LastUpdatedDate = (reader["lastUpdatedDate"] != System.DBNull.Value) ? reader["lastUpdatedDate"].ToString() : null,
                            LastUpdatedTime = (reader["lastUpdatedTime"] != System.DBNull.Value) ? reader["lastUpdatedTime"].ToString() : null,
                            Remarks = (reader["remarks"] != System.DBNull.Value) ? reader["remarks"].ToString() : null,
                            BranchCode = (reader["branchCode"] != System.DBNull.Value) ? reader["branchCode"].ToString() : null,
                            TranDate = (reader["tranDate"] != System.DBNull.Value) ? reader["tranDate"].ToString() : null,
                            TranTime = (reader["tranTime"] != System.DBNull.Value) ? reader["tranTime"].ToString() : null,
                            CreatedBy = (reader["createdBy"] != System.DBNull.Value) ? reader["createdBy"].ToString() : null,
                            Type = (reader["type"] != System.DBNull.Value) ? reader["type"].ToString() : null,
                            RefNo = (reader["refNo"] != System.DBNull.Value) ? reader["refNo"].ToString() : null,
                            Bank = (reader["bank"] != System.DBNull.Value) ? reader["bank"].ToString() : null,
                            TotalAmount = (reader["totalAmount"] != System.DBNull.Value) ? Convert.ToDecimal(reader["totalAmount"].ToString()) : 0,
                            Active = (reader["active"] != System.DBNull.Value) ? Convert.ToInt32(reader["active"].ToString()) : 0
                        });
                    }
                }
                reader.Close();
            }

            return Task.FromResult<IEnumerable<CollectionReceiptDetails>>(result);
        }
    }
}
