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
using Redmine_sync.Team;
using Redmine_sync.TMS;

namespace Redmine_sync
{
    class Program
    {
        //private static string PROJECT = "macbi-problems";

        static void Main(string[] args)
        {
           /* List<TMSItem> list = new List<TMSItem>();

            for (int i = 0; i < 10; i++)
            {
                TMSItem item = new TMSItem();
                item.TMS = "TMS" + i;
                item.Status = "N";
                item.Desctiption = "tararara";
                list.Add(item);
            }

            list.SerializeTMSItemData();

            List<TMSItem> list2 = new List<TMSItem>();

            list2 = list2.DeserializeTMSItemData();
           */

            /*
            using (FileStream fs = new FileStream("test.xml", FileMode.OpenOrCreate))
            {
                System.Xml.Serialization.XmlSerializer s = new System.Xml.Serialization.XmlSerializer(typeof(List<TMSItem>));
                s.Serialize(fs, list);
            }
            */
            /*
            List<TMSItem> list2 = new List<TMSItem>();
            using (var reader = new StreamReader("test.xml"))
            {
                System.Xml.Serialization.XmlSerializer deserializer = new System.Xml.Serialization.XmlSerializer(typeof(List<TMSItem>),
                    new System.Xml.Serialization.XmlRootAttribute("ArrayOfTMSItem"));
                list2 = (List<TMSItem>)deserializer.Deserialize(reader);
            }
            */
            
            Console.WriteLine("Started...");

            //var parameters = new NameValueCollection();

            //get project
            //MOM problems id: 65
            //var project = RMManegerService.RMManager.GetObject<Project>(/*"mom-problems"*/ "macbi-problems" /*"temporary-macbi-problems"*/, null);


            Console.WriteLine("1) Add new items");
            Console.WriteLine("2) Update items (based on single XLSX file)");
            Console.WriteLine("3) Update items (based on all XLSX file from the directory)");
            Console.WriteLine("4) Synchronize MACBI TMSes");
            Console.WriteLine("5) Cache DEV1 team data");
            Console.WriteLine("9) Build stats based in Redmine");

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
                case "4":
                    new TMSTaskSynchronizer("MACBI").Synchronize();
                    break;
                case "5":
                    TeamService.CacheTeamData();
                    break;
                case "9":
                    MOMActionsManager.BuildFinalStats();
                    break;
            }
        }

        
      

       

       

        
       

      

     


        
     
    }
}
