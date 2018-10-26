using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace AssignmentProject.Core.Services
{
    public class CsvToDataTableService
    {
        public static DataTable CsvToDataTable(string strFilePath)
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[10] {
                new DataColumn("Key", typeof(string)),
                new DataColumn("ArtikelCode", typeof(string)),
                new DataColumn("ColorCode",typeof(string)),
                new DataColumn("Description",typeof(string)),
                new DataColumn("Price",typeof(decimal)),
                new DataColumn("DiscountPrice", typeof(string)),
                new DataColumn("DeliveredIn",typeof(string)),
                new DataColumn("Q1",typeof(string)),
                new DataColumn("Size",typeof(int)),
                new DataColumn("Color",typeof(string))
            });

            using (StreamReader sr = new StreamReader(strFilePath))
            {
                sr.ReadLine();// skip first
                while (!sr.EndOfStream)
                {
                    string[] rows = sr.ReadLine().Split(',');
                    DataRow dr = dt.NewRow();

                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        dr[i] = rows[i];
                    }
                    dt.Rows.Add(dr);
                }

            }
            return dt;
        }

        public static void InsertDataIntoSQLServerUsingSQLBulkCopy(DataTable csvFileData)
        {
            using (SqlConnection dbConnection = new SqlConnection(@"data source=.;initial catalog=AssignmentProject;integrated security=true"))
            {
                dbConnection.Open();
                using (SqlBulkCopy s = new SqlBulkCopy(dbConnection))
                {
                    s.DestinationTableName = "Products";
                    s.ColumnMappings.Add("Key", "Key");
                    s.ColumnMappings.Add("ArtikelCode", "ArtikelCode");
                    s.ColumnMappings.Add("ColorCode", "ColorCode");
                    s.ColumnMappings.Add("Description", "Description");
                    s.ColumnMappings.Add("Price", "Price");
                    s.ColumnMappings.Add("DiscountPrice", "DiscountPrice");
                    s.ColumnMappings.Add("DeliveredIn", "DeliveredIn");
                    s.ColumnMappings.Add("Q1", "Q1");
                    s.ColumnMappings.Add("Size", "Size");
                    s.ColumnMappings.Add("Color", "Color");


                    s.WriteToServer(csvFileData);
                }
            }
        }
    }
}
