using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class Connection
    {
        public SqlConnection con;
        public void connection ()
        {
            string conn = "data source=WINDOWS-10\\SQLEXPRESS;database = ADO_EXAMPLE;integrated security = true";
            con = new SqlConnection (conn);
        }
    }
}
