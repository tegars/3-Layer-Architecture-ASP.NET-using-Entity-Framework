using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System;

using System.Configuration;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Collections.Generic;
namespace YUHU1.BLL.Kelas
{
    class KelasBll
    {
        public void coba() {
            //CONNECTION FOR SP
            EntityConnectionStringBuilder e = new EntityConnectionStringBuilder(ConfigurationManager.ConnectionStrings["YUHU_Entities"].ConnectionString);
            string connectionString = e.ProviderConnectionString;
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            //END CONNECTION
            var hrQuery = " SELECT * FROM KELAS ";
            SqlCommand query = new SqlCommand(hrQuery, con);
            SqlDataReader reader = query.ExecuteReader();
        }
    }
}
