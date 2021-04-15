using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redmine_sync
{
    class DBService
    {
        private static string CONN_STRING;

        public static string GET_ALL_TMS_TASKS = "SELECT * FROM MACBI_TMS_ADDITIONAL_INFO";
        static DBService()
        {
            CONN_STRING = string.Format("User ID={0}; Password={1}; Data Source={2};", Properties.Resources.User, Properties.Resources.Hidden, Properties.Resources.dbstring);
        }

        public static DataTable EqecuteQuery(string query)
        {
            DataTable dt = new DataTable();
            using (OracleConnection oc = new OracleConnection())
            {
                oc.ConnectionString = CONN_STRING;
                oc.Open();

//                string sql = "SELECT * FROM MACBI_TMS_ADDITIONAL_INFO";

                OracleDataAdapter oda = new OracleDataAdapter(query, oc);
                
                oda.Fill(dt);
                oc.Close();
            }

            return dt;
        }
    }
}
