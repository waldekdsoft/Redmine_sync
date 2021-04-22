using Redmine.Net.Api.Types;
using Redmine_sync.Team;
using Redmine_sync.TMS;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Diagnostics;
using System.Linq;

namespace Redmine_sync
{
    using TMS_TP = Tuple<TMSItem, TMSItem>;

    public class TMSTaskSynchronizer
    {
        private static int PROJECT_ID = 74;
        private static int USER_ID = 26;//waldekd
        private static int PRIORITY_HIGH = 3;

        private string client = string.Empty;
        private static int TMS_PROJECT_ID = 15; //74; //15;//74;//???????????????
        Stopwatch sw = new Stopwatch();

        TMSDictionary dbTMSDict = null;
        List<TMSItem> redMineTMSList = null;
        Dictionary<string, List<TMS_TP>> outputList = null;

        static Dictionary<string /*client*/, TMSTaskSynchronizer> instancesDict = new Dictionary<string, TMSTaskSynchronizer>();

        public static TMSTaskSynchronizer GetInstance(string client)
        {
            TMSTaskSynchronizer instance = null;

            if (!instancesDict.TryGetValue(client, out instance))
            {
                instance = new TMSTaskSynchronizer(client);
                instancesDict.Add(client, instance);
            }
            return instance;
        }

        private TMSTaskSynchronizer(string client)
        {
            this.client = client;
        }

        public void AddMissingTMSTasksToRedmine()
        {
            sw.StartStopwatchAndPrintMessage("Getting TMS tasks from DB and making cache...");
            GetherSyncData();

            IdentifiableName p = IdentifiableName.Create<Project>(PROJECT_ID);
            //IdentifiableName u = IdentifiableName.Create<User>(USER_ID);
            //var user = RMManegerService.RMManager.GetObject<User>(USER_ID.ToString(), null);
            IdentifiableName u = IdentifiableName.Create<IdentifiableName>(USER_ID);
            IdentifiableName priority = IdentifiableName.Create<IdentifiableName>(PRIORITY_HIGH);


            //var newIssue = new Issue { Subject = subject, Project = p, Description = details };
            //RMManegerService.RMManager.CreateObject(newIssue);

            int limit = 5;

            foreach (TMSItem item in dbTMSDict.GetNotClosedNotUsedAssignedToDEV1ItemList())
            {
                string description = item.Desctiption;
                //check if really there is no such a TMS in RM (maybe it was already closed)
                TMSItem tmsItem = dbTMSDict.Get(item.TMS);
                if (tmsItem == null)
                {
                    string firstLine = description.Substring(0, description.Contains("\r\n") ? description.IndexOf("\r\n") : description.IndexOf("."));
                    string subject = string.Format("{0} - {1}", item.TMS, firstLine);
                    var newIssue = new Issue { Subject = subject, Project = p, Description = description, AssignedTo = u, Priority = priority };
                    RMManegerService.RMManager.CreateObject(newIssue);
                }
                else
                {
                    Console.WriteLine("Item can't be added as it already exists: {0}", tmsItem);
                }
                if (--limit == 0)
                    return;
            }
            sw.StopStopwatchAndPrintDoneMessageWithElapsedTime();
        }

        public void GetherSyncData()
        {
            Console.WriteLine("Gethering of synchronization data for {0} started...", this.client);

            if (dbTMSDict == null)
            {
                dbTMSDict = GetTMSDataFromDB();
            }

            if (redMineTMSList == null)
            {
                redMineTMSList = GetTMSDataFromRedMine();
            }

            //SYNCHRONIZATION
            //reason + list of pairs <TMS task, RM TMS task>
            outputList = new Dictionary<string, List<TMS_TP>>();

            foreach (TMSItem itemFromRM in redMineTMSList)
            {
                TMSItem dbTMSItem = dbTMSDict.Get(itemFromRM.TMS);

                if (dbTMSItem != null)
                {
                    //ommit closed tasks
                    if (dbTMSItem.Status.StartsWith("C") || dbTMSItem.Status.StartsWith("c"))
                    {
                        if (itemFromRM.Status == Consts.RM_CLOSED)
                        {
                            //ignore closed in TMS and in RM
                            TMS_TP tp = new TMS_TP(dbTMSItem, itemFromRM);
                            outputList.UpdateDictionary(Consts.RFC_BOTH_CLOSED, tp);
                        }
                        else
                        {
                            TMS_TP tp = new TMS_TP(dbTMSItem, itemFromRM);
                            outputList.UpdateDictionary(Consts.RFC_DIFFERENT_STATUSES, tp);
                        }
                    }
                    else
                    {
                        //Console.WriteLine("{0}\nRM: {1}", dbTMSItem, itemFromRM);
                        TMS_TP tp = new TMS_TP(dbTMSItem, itemFromRM);
                        outputList.UpdateDictionary(Consts.RFC_BOTH_OK, tp);

                    }

                }
                else
                {
                    TMS_TP tp = new TMS_TP(null, itemFromRM);
                    //if the problem is not related to client TMS (so its subject does not start with "client")
                    if (itemFromRM.TMS != null && !itemFromRM.TMS.StartsWith(client))
                    {
                        outputList.UpdateDictionary(Consts.RFC_NOT_CONNECTED_WITH_TMS, tp);
                        //Console.WriteLine("Element not connected with !: {0}", itemFromRM);
                    }
                    else
                    {
                        outputList.UpdateDictionary(Consts.RFC_NOT_EXISTS_IN_TMS, tp);
                    }
                }
            }

            #region TO BE UNCOMMENTED 

            //foreach (var issue in RMManegerService.RMManager.GetObjects<Issue>(parameters).Where(issue => issue.Project.Id == TMS_PROJECT_ID))
            //{
            //    string subject = issue.Subject;

            //    TMSItem itemFromRM = new TMSItem();
            //    itemFromRM.AssignedTo = issue.AssignedTo.TryGetName();
            //    itemFromRM.Desctiption = issue.Description;

            //    string[] subjectSplitted = issue.Subject.Split('-');
            //    itemFromRM.TMS = issue.Subject.Split('-')[0].Trim() + "-" + issue.Subject.Split('-')[1].Trim();
            //    itemFromRM.Status = issue.Status.TryGetName();

            //    TMSItem dbTMSItem = null;
            //    if (dbTMSDict.TryGetValue(itemFromRM.TMS, out dbTMSItem))
            //    {
            //        Console.WriteLine("Element found\nTMS: {0}\nRM: {1}", itemFromRM.ToString(), dbTMSItem);

            //    }
            //    else
            //    {
            //        Console.WriteLine("Element not found!");
            //    }

            //    redMineTMSList.Add(itemFromRM);
            //}
            #endregion

            Console.WriteLine("Gethering of synchronization data for {0} finished...", this.client);
        }

     

        public void DisplayStatsForTMSSync()
        {
            foreach (string key in outputList.Keys)
            {
                Console.WriteLine("---------------------------");
                Console.WriteLine(key);
                Console.WriteLine("---------------------------");
                foreach (TMS_TP tp in outputList[key])
                {
                    Console.WriteLine(tp);
                }
            }

            Console.WriteLine("-------TMS not exist in RM-------");
            foreach (TMSItem item in dbTMSDict.GetNotClosedNotUsedAssignedToDEV1ItemList())
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("-------RM duplicated TMS -------");
            Dictionary<string, List<TMSItem>> duplicates = redMineTMSList.GetDuplicates();
            foreach (string tmsNum in duplicates.Keys)
            {
                Console.WriteLine(tmsNum);
                foreach (TMSItem item in duplicates[tmsNum])
                {
                    Console.WriteLine("   " + item);
                }
            }
        }

        private static List<TMSItem> GetTMSDataFromRedMine()
        {
            Stopwatch sw = new Stopwatch();
            sw.StartStopwatchAndPrintMessage("Getting TMS data from RedMine...");
            NameValueCollection parameters = new NameValueCollection { { "status_id", "*" } };
            List<TMSItem> redMineTMSList = new List<TMSItem>();
            if (!Consts.TEST_MODE)
            {
                foreach (var issue in RMManegerService.RMManager.GetObjects<Issue>(parameters).Where(issue => issue.Project.Id == TMS_PROJECT_ID))
                {
                    string subject = issue.Subject;

                    TMSItem itemFromRM = new TMSItem();
                    itemFromRM.Source = Consts.SRC_RM;
                    itemFromRM.AssignedTo = issue.AssignedTo.TryGetName();
                    itemFromRM.Desctiption = issue.Description;

                    string[] subjectSplitted = issue.Subject.Split('-');
                    itemFromRM.TMS = issue.Subject.Split('-')[0].Trim() + "-" + issue.Subject.Split('-')[1].Trim();
                    itemFromRM.Status = issue.Status.TryGetName();

                    redMineTMSList.Add(itemFromRM);
                }

                redMineTMSList.SerializeTMSItemData();
            }
            else
            {
                redMineTMSList = redMineTMSList.DeserializeTMSItemData();
            }

            sw.StopStopwatchAndPrintDoneMessageWithElapsedTime();
            return redMineTMSList;
        }

        private TMSDictionary GetTMSDataFromDB()
        {
            DataTable tmsTasksFromDBDataTable = new DataTable();
            if (!Consts.TEST_MODE)
            {
                sw.StartStopwatchAndPrintMessage("Getting TMS tasks from DB and making cache...");
                tmsTasksFromDBDataTable = DBService.ExecuteQuery(DBService.GET_ALL_TMS_TASKS);
                tmsTasksFromDBDataTable.TableName = "TMS_DATA";
                tmsTasksFromDBDataTable.WriteXml(Consts.TMS_CACHE_FILE_NAME);
            }
            else
            {
                sw.StartStopwatchAndPrintMessage("Getting TMS tasks from cache...");
                DataSet ds = new DataSet();
                ds.ReadXml(Consts.TMS_CACHE_FILE_NAME, XmlReadMode.InferSchema);
                tmsTasksFromDBDataTable = ds.Tables[0];
            }

            sw.StopStopwatchAndPrintDoneMessageWithElapsedTime();

            //Dictionary<string, TMSItem> dbTMSDict = new Dictionary<string, TMSItem>();
            TMSDictionary dbTMSDict = new TMSDictionary();
            //List<string> team_members = TeamService.GetDEV1TeamMembersTMSLogins();

            sw.StartStopwatchAndPrintMessage("Conversion DB items to objects...");
            foreach (DataRow tmsTaskFromDB in tmsTasksFromDBDataTable.Rows)
            {
                TMSItem item = new TMSItem();
                item.Source = Consts.SRC_DB;
                item.AssignedTo = Convert.ToString(tmsTaskFromDB["EMPLOYEE"]);
                item.TMS = Convert.ToString(tmsTaskFromDB["TMS_ID"]);
                item.Status = Convert.ToString(tmsTaskFromDB["STATUS"]);
                item.Urgency = Convert.ToString(tmsTaskFromDB["URG"]);
                item.LastActText = Convert.ToString(tmsTaskFromDB["LASTACTION_TXT"]);
                item.LastActDate = Convert.ToDateTime(tmsTaskFromDB["LASTACTION_DATE"]);
                item.Desctiption = Convert.ToString(tmsTaskFromDB["TASK_DESCRIPTION"]);
                item.SDId = Convert.ToString(tmsTaskFromDB["SOFTDEV_ID"]);
                dbTMSDict.Add(item);
            }

            sw.StopStopwatchAndPrintDoneMessageWithElapsedTime();
            return dbTMSDict;
        }

        public static void CreateTMSCache()
        {
            Console.Write("Cache creation...");
            NameValueCollection parameters = new NameValueCollection { { "status_id", "*" } };
            foreach (var issue in RMManegerService.RMManager.GetObjects<Issue>(parameters).Where(issue => issue.Project.Id == TMS_PROJECT_ID))
            {
                string subject = issue.Subject;

                TMSItem itemFromRM = new TMSItem();
                itemFromRM.Source = Consts.SRC_RM;
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
