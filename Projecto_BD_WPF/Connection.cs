using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projecto_BD_WPF
{
    class Connection
    {
        public static SqlConnection getConnection()
        {
            return new SqlConnection("Data Source=tcp: 193.136.175.33\\SQLSERVER2012,8293;Initial Catalog=p5g1;User ID=p5g1;Password=portugal");
        }

    }
}
