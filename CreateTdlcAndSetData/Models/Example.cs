using System.Data;

namespace CreateTdlcAndSetData.Models
{
        public class Example
        {
            public string Name { get; set; }

            public string LastName { get; set; }

            public DataSet GetParam()
            {
                DataSet ds = new DataSet();
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("Name", typeof(System.String));
                dataTable.Columns.Add("LastName", typeof(System.String));
                ds.Tables.Add(dataTable);
                return ds;
            }
        }

}
