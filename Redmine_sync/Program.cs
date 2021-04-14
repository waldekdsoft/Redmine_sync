using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using Redmine.Net.Api;
using Redmine.Net.Api.Types;

namespace Redmine_sync
{
    class Program
    {
        //private static string PROJECT = "macbi-problems";

        static void Main(string[] args)
        {
            /*
            using (OracleConnection oc = new OracleConnection())
            {
                oc.ConnectionString = "User ID=waldekd; Password=waldekd123; Data Source=DAILY_STAT;";
                oc.Open();

                string sql = "SELECT * FROM high_iss_interface";

                OracleDataAdapter oda = new OracleDataAdapter(sql, oc);
                DataTable dt = new DataTable();
                oda.Fill(dt);
            }
            */

            Console.WriteLine("Started...");
           
            //var parameters = new NameValueCollection();

            //get project
            //MOM problems id: 65
            //var project = manager.GetObject<Project>(/*"mom-problems"*/ "wdtest", null);

            //********************************************************************************************************/
            //write MACBI items to local list
            
            Console.WriteLine("1) Add new items");
            Console.WriteLine("2) Update items (based on single XLSX file)");
            Console.WriteLine("3) Update items (based on all XLSX file from the directory)");
            Console.WriteLine("9) UBuild stats based in Redmine");

            switch (Console.ReadLine())
            {
                case "1":
                    MOMActionsManager.AddNewItems();
                    break;
                case "2":
                    MOMActionsManager.UpdateItems();
                    break;
                case "3":
                    MOMActionsManager.UpdateItems(true);
                    break;
                case "9":
                    MOMActionsManager.BuildFinalStats();
                    break;
            }
        }

        
      

       

       

        
       

      

     


        
     
    }
}
