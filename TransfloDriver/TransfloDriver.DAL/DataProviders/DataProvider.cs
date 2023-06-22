using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransfloDriver.Infrastructure;

namespace TransfloDriver.DAL.DataProviders
{
    public class DataProvider
    {
        public static DataRow SelectDataRaw(string Query)
        {
            string ConnectionString = AppSettings.ConnectionString;
            using (SqlConnection SC = new SqlConnection(ConnectionString))
            {
                SC.Open();
                checkDatabaseExist(SC);
                checkTableExist(SC);
                SqlDataAdapter adapter = new SqlDataAdapter(Query, SC);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);
                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    return dataSet.Tables[0].Rows[0];
                }
                return null;
            }
        }


        public static DataTable SelectDataTable(string Query)
        {
            string ConnectionString = AppSettings.ConnectionString;
            using (SqlConnection SC = new SqlConnection(ConnectionString))
            {
                SC.Open();
                checkDatabaseExist(SC);
                checkTableExist(SC);
                SqlDataAdapter adapter = new SqlDataAdapter(Query, SC);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);
                if (dataSet.Tables.Count > 0)
                {
                    return dataSet.Tables[0];
                }
                return null;
            }
        }

        public async static Task<int> ExcuteQueryAsync(SqlCommand cmd)
        {
            string ConnectionString = AppSettings.ConnectionString;
            using (SqlConnection SC = new SqlConnection(ConnectionString))
            {
                SC.Open();
                checkDatabaseExist(SC);
                checkTableExist(SC);
                cmd.Connection = SC;
                int result = await cmd.ExecuteNonQueryAsync();
                return result;
            }
        }


        public async static void InserBulkData(DataTable dt)
        {
            string ConnectionString = AppSettings.ConnectionString;
            using (SqlConnection SC = new SqlConnection(ConnectionString))
            {
                SC.Open();
                checkDatabaseExist(SC);
                checkTableExist(SC);
                using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(SC))
                {
                    sqlBulkCopy.DestinationTableName = "DriverDB";
                    await sqlBulkCopy.WriteToServerAsync(dt);
                }
            }
        }


        private static void checkDatabaseExist(SqlConnection SC)
        {
            var QueryGetDatabaseId = string.Format("SELECT database_id FROM sys.databases WHERE Name   = '{0}'", AppSettings.Database);
            SqlCommand sqlCmd = new SqlCommand(QueryGetDatabaseId, SC);
            object resultObj = sqlCmd.ExecuteScalar();
            if (resultObj == null)
            {
                sqlCmd.CommandText = string.Format("CREATE DATABASE {0};", AppSettings.Database);
                sqlCmd.ExecuteNonQuery();
            }
            SC.ChangeDatabase(AppSettings.Database);
        }

        private static void checkTableExist(SqlConnection SC)
        {
            DataTable table = SC.GetSchema("TABLES", new string[] { null, null, "DriverDB" });
            if (table.Rows.Count == 0)
            {
                var QueryCreateDriverTable = @"use Transflo;
                            CREATE TABLE DriverDB
                            (
                                Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
                                FirstName nvarchar(MAX)  NULL,
                                LastName nvarchar(MAX) NULL,
                                Email nvarchar(MAX) NULL,
                                PhoneNumber nvarchar(MAX) NULL,
                            )";

                SqlCommand sqlCmd = new SqlCommand(QueryCreateDriverTable, SC);
                var result = sqlCmd.ExecuteNonQuery();

            }

        }

    }
}
