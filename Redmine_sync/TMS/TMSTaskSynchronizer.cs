using Redmine.Net.Api.Types;
using Redmine_sync.TMS;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redmine_sync
{
    public class TMSTaskSynchronizer
    {
        private string client = string.Empty;
        private static int TMS_PROJECT_ID = 74; //15;//74;//???????????????
        Stopwatch sw = new Stopwatch();

        public TMSTaskSynchronizer(string client)
        {
            this.client = client;
        }

        public void Synchronize()
        {
            Console.WriteLine("Synchronization for {0} started...", this.client);

            #region Getting TMS tasks from DB...
            //get all TMS tasks from SD database
            sw.StartStopwatchAndPrintMessage("Getting TMS tasks from DB...");

            //DataTable tmsTasksFromDBDataTable = DBService.EqecuteQuery(DBService.GET_ALL_TMS_TASKS);
            //tmsTasksFromDBDataTable.TableName = "TMS_DATA";

            //serialize to XML
            //tmsTasksFromDBDataTable.WriteXml("tms_data.xml");
            DataTable tmsTasksFromDBDataTable = new DataTable();
            //tmsTasksFromDBDataTable.TableName = "TMS_DATA";

            DataSet ds = new DataSet();
            ds.ReadXml("tms_data.xml", XmlReadMode.InferSchema);
            tmsTasksFromDBDataTable = ds.Tables[0];
            //tmsTasksFromDBDataTable.ReadXml("tms_data.xml");


            Dictionary<string, TMSItem> dbTMSDict = new Dictionary<string, TMSItem>();
            //List<TMSItem> dbTMSList = new List<TMSItem>();

            foreach (DataRow tmsTaskFromDB in tmsTasksFromDBDataTable.Rows)
            {
                TMSItem item = new TMSItem();
                item.AssignedTo = Convert.ToString(tmsTaskFromDB["EMPLOYEE"]);
                item.TMS = Convert.ToString(tmsTaskFromDB["TMS_ID"]);
                item.Status = Convert.ToString(tmsTaskFromDB["STATUS"]);
                item.Urgency = Convert.ToString(tmsTaskFromDB["URG"]);
                item.LastActText= Convert.ToString(tmsTaskFromDB["LASTACTION_TXT"]);
                item.LastActDate = Convert.ToDateTime(tmsTaskFromDB["LASTACTION_DATE"]);
                item.Desctiption = Convert.ToString(tmsTaskFromDB["TASK_DESCRIPTION"]);
                item.SDId = Convert.ToString(tmsTaskFromDB["SOFTDEV_ID"]);
                if (!dbTMSDict.ContainsKey(item.TMS))
                {
                    dbTMSDict.Add(item.TMS, item);
                }
            }
            sw.StopStopwatchAndPrintDoneMessageWithElapsedTime();
            #endregion

            #region Getting TMS data from RedMine...
            sw.StartStopwatchAndPrintMessage("Getting TMS data from RedMine...");
            List<TMSItem> redMineTMSList = new List<TMSItem>();
            NameValueCollection parameters = new NameValueCollection { { "status_id", "*" } };

            foreach (var issue in RMManegerService.RMManager.GetObjects<Issue>(parameters).Where(issue => issue.Project.Id == TMS_PROJECT_ID))
            {
                string subject = issue.Subject;

                TMSItem itemFromRM = new TMSItem();
                itemFromRM.AssignedTo = issue.AssignedTo.TryGetName();
                itemFromRM.Desctiption = issue.Description;

                string[] subjectSplitted = issue.Subject.Split('-');
                itemFromRM.TMS = issue.Subject.Split('-')[0].Trim() + "-" + issue.Subject.Split('-')[1].Trim();
                itemFromRM.Status = issue.Status.TryGetName();

                TMSItem dbTMSItem = null;
                if (dbTMSDict.TryGetValue(itemFromRM.TMS, out dbTMSItem))
                {
                    Console.WriteLine("Element found\nTMS: {0}\nRM: {1}", itemFromRM.ToString(), dbTMSItem);

                }
                else
                {
                    Console.WriteLine("Element not found!");
                }

                redMineTMSList.Add(itemFromRM);
            }

            sw.StopStopwatchAndPrintDoneMessageWithElapsedTime();
            #endregion

            //get all TMS tasks from RM and create similar list of objects

            //iterate over TMS list of task from DB and check the items from 

            Console.WriteLine("Synchronization for {0} finished...", this.client);
        }

        public static void CreateTMSCache()
        {
            Console.Write("Cache creation...");
            NameValueCollection parameters = new NameValueCollection { { "status_id", "*" } };
            foreach (var issue in RMManegerService.RMManager.GetObjects<Issue>(parameters).Where(issue => issue.Project.Id == TMS_PROJECT_ID))
            {
                string subject = issue.Subject;

                TMSItem itemFromRM = new TMSItem();
                itemFromRM.AssignedTo = issue.AssignedTo.TryGetName();
                itemFromRM.Desctiption = issue.Description;
                
                string[] subjectSplitted = issue.Subject.Split('-');
                itemFromRM.TMS = issue.Subject.Split('-')[0].Trim() + "-" + issue.Subject.Split('-')[1].Trim();
                itemFromRM.Status = issue.Status.TryGetName();



            }

            Console.WriteLine("done!");
        }
    }
}
