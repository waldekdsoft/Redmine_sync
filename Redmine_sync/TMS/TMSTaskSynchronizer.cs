using Redmine.Net.Api.Types;
using Redmine_sync.DataSets;
using Redmine_sync.GUI;
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
    using static Redmine_sync.DataSets.MainDS;
    using TMS_TP = Tuple<TMSItem, TMSItem>;
    //https://www.softcomputer.com/itms/gentaskdetails.php?Client=MACBI&ID=02732
    //string ITMS_REDIRECTION_TEMPLATE = "https://www.softcomputer.com/itms/gentaskdetails.php?Client={0}&ID={1}";

    public class TMSTaskSynchronizer
    {
        private static int USER_ID = 26;//waldekd
        private static int PRIORITY_HIGH = 3;

        private string client = string.Empty;
        Stopwatch sw = new Stopwatch();

        TMSDictionary dbTMSDict = null;
        //List<TMSItem> redMineTMSList = null;
        TMSDictionary rmTMSDict = null;
        Dictionary<string, List<TMS_TP>> outputList = null;

        IOutputable output = null;

        static Dictionary<string /*client*/, TMSTaskSynchronizer> instancesDict = new Dictionary<string, TMSTaskSynchronizer>();

       

        public static TMSTaskSynchronizer GetInstance(string client, IOutputable outp)
        {
            TMSTaskSynchronizer instance = null;

            if (!instancesDict.TryGetValue(client, out instance))
            {
                instance = new TMSTaskSynchronizer(client, outp);
                instancesDict.Add(client, instance);
            }
            return instance;
        }

        private TMSTaskSynchronizer(string client, IOutputable outp)
        {
            this.client = client;
            this.output = outp;
        }

        public void ClearCache()
        {
            dbTMSDict = null;
            rmTMSDict = null;
            output.WriteLine("Cache cleared...");
        }

        public void AddMissingTMSTasksToRedmine()
        {
            sw.StartStopwatchAndPrintMessage("Getting TMS tasks from DB and making cache...", output);
            GetherSyncData();

            IdentifiableName p = IdentifiableName.Create<Project>(Consts.PROJECT_NAMES.TMS.MACBI.PROBLEMS); //to-be-changed
            //IdentifiableName u = IdentifiableName.Create<User>(USER_ID);
            //var user = RMManegerService.RMManager.GetObject<User>(USER_ID.ToString(), null);
            IdentifiableName u = IdentifiableName.Create<IdentifiableName>(USER_ID);
            IdentifiableName priority = IdentifiableName.Create<IdentifiableName>(PRIORITY_HIGH);


            //var newIssue = new Issue { Subject = subject, Project = p, Description = details };
            //RMManegerService.RMManager.CreateObject(newIssue);

            int limit = 5;

            List<string> listOfAddedItems = new List<string>();
            List<string> listOfNOTAddedItems = new List<string>();

            foreach (TMSItem item in dbTMSDict.GetNotClosedNotUsedAssignedToDEV1ItemList(TeamService.UsersTMSLogin))
            {
                string description = item.Desctiption;
                //check if really there is no such a TMS in RM (maybe it was already closed)
                TMSItem tmsItem = rmTMSDict.Get(item.TMS);

                //redMineTMSList.Contains()
                if (tmsItem == null)
                {
                    string firstLine = null;

                    if (description.Contains("\r\n") || description.Contains("."))
                    {
                        firstLine = description.Substring(0, description.Contains("\r\n") ? description.IndexOf("\r\n") : description.IndexOf("."));
                    }
                    else
                    {
                        firstLine = description;
                    }

                    if (firstLine.Length > 255)
                    {

                        firstLine = firstLine.Substring(0, 240);//255 is max lenght of subject.

                        int lastDotIndex = firstLine.LastIndexOf(".");
                        if (lastDotIndex > -1)
                        {
                            firstLine = firstLine.Substring(0, lastDotIndex);
                        }                        
                    }

                    string subject = string.Format("{0} - {1}", item.TMS, firstLine);
                    var newIssue = new Issue { Subject = subject, Project = p, Description = description, AssignedTo = u, Priority = priority };
                    RMManegerService.RMManager.CreateObject(newIssue);
                    listOfAddedItems.Add(subject);
                }
                else
                {
                    listOfNOTAddedItems.Add(string.Format("{0}", tmsItem));
                    //Console.WriteLine("Item can't be added as it already exists: {0}", tmsItem);
                }

                if (Consts.TEST_MODE)
                {
                    if (--limit == 0)
                        return;
                }
            }

            output.WriteLine("List of added items ({0})", listOfAddedItems.Count);
            output.WriteLine("List of NOT added items ({0})", listOfNOTAddedItems.Count);

            if (listOfAddedItems.Count > 0)
            {
                output.WriteLine("Cache needs to be refreshed!");
                rmTMSDict = null;
                GetherSyncData();
            }
            sw.StopStopwatchAndPrintDoneMessageWithElapsedTime(output);
        }

        public void GetherSyncData()
        {
            output.WriteLine("Gethering of synchronization data for {0} started...", this.client);

            if (dbTMSDict == null)
            {
                dbTMSDict = GetTMSDataFromDB();
            }
            else
            {
                output.WriteLine("TMS dictionary already exists in memory...");
            }

            if (rmTMSDict == null)
            {
                rmTMSDict = GetTMSDataFromRedMine(output);
            }
            //if (redMineTMSList == null)
            //{
            //    redMineTMSList = GetTMSDataFromRedMine();
            //}
            else
            {
                output.WriteLine("RM list already exists in memory...");
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

            output.WriteLine("Gethering of synchronization data for {0} finished...", this.client);

            return;
        }

        //reason + list of pairs <TMS task, RM TMS task>
        public void CreateSyncOutputList()
        {
            outputList = new Dictionary<string, List<TMS_TP>>();

            foreach (TMSItem itemFromRM in rmTMSDict.GetItemList())
            {
                bool isRedmineAssignedToMyTeam = TeamService.IsPersonFromMyTeam(itemFromRM.AssignedTo);
                bool isTMSAssignedtoMyTeam = false;

                TMSItem dbTMSItem = dbTMSDict.Get(itemFromRM.TMS);

                if (dbTMSItem != null)
                {
                    isTMSAssignedtoMyTeam = TeamService.IsPersonFromMyTeam(dbTMSItem.AssignedTo);

                    if (isRedmineAssignedToMyTeam || isTMSAssignedtoMyTeam)
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
                            /*TODO*/
                            //check if the task is assigned not to my team
                            if (!TeamService.CheckIfSamePersonByTMSLoginAndFullName(dbTMSItem.AssignedTo, itemFromRM.AssignedTo))
                            {
                                TMS_TP tp = new TMS_TP(dbTMSItem, itemFromRM);
                                outputList.UpdateDictionary(Consts.RFC_ASSIGNED_TO_DIFFERENT_PERSON_IN_RM_AND_TMS, tp);
                            }
                            else
                            {
                                //Console.WriteLine("{0}\nRM: {1}", dbTMSItem, itemFromRM);
                                TMS_TP tp = new TMS_TP(dbTMSItem, itemFromRM);
                                outputList.UpdateDictionary(Consts.RFC_BOTH_OK, tp);
                            }
                        }
                    }

                }
                else
                {

                    if (isRedmineAssignedToMyTeam)
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
            }
        }

        internal void UpdateRMWithLastTMSInfo()
        {
            //if (dbTMSDict == null)
            //{
            //    dbTMSDict = GetTMSDataFromDB();
            //}

        }

        public void DisplayStatsForTMSSync()
        {
            MainDS ds = new MainDS();
            TMSWithReasonDataTable dt = ds.TMSWithReason;

            output.WriteLine("");
            foreach (string key in outputList.Keys)
            {
                //output.WriteLine("---------------------------");
                //output.WriteLine(key);
                //output.WriteLine("---------------------------");
                foreach (TMS_TP tp in outputList[key])
                {
                    //output.WriteLine(tp.ToString());
                    DataRow r = dt.NewRow();
                    r["Reason"] = key;
                    if (tp.Item1 != null)
                    {
                        r["TMS"] = tp.Item1.TMS;
                        r["TMS_Assigned"] = tp.Item1.AssignedTo;
                        r["TMS_Status"] = tp.Item1.Status;
                    }

                    r["TMS_ACT_SHOW"] = "X";
                    r["RM_ACT_SHOW"] = "X";

                    if (IsAssignedToMe(tp.ToString()))
                    {
                        r["Me"] = "Y";
                    }
                    
                    if (tp.Item2 != null)
                    {
                        r["RM"] = tp.Item2.RMId;
                        r["RM_Assigned"] = tp.Item2.AssignedTo;
                        r["RM_Status"] = tp.Item2.Status;

                    }
                    dt.Rows.Add(r);
                }
            }
            
            output.WriteLine("\r\n-------TMS not exist in RM-------");
            foreach (TMSItem item in dbTMSDict.GetNotClosedNotUsedAssignedToDEV1ItemList(TeamService.UsersTMSLogin))
            {
                output.WriteLine(item.ToString());
                DataRow r = dt.NewRow();
                FillOutputGridRowWithTMSData(item, r, Consts.RFC_NOT_EXISTS_IN_RM);
                dt.Rows.Add(r);
            }

            output.WriteLine("\r\n-------RM duplicated TMS -------");
            Dictionary<string, List<TMSItem>> duplicates = rmTMSDict.GetDuplicates();
            foreach (string tmsNum in duplicates.Keys)
            {
                output.WriteLine(tmsNum);
                foreach (TMSItem item in duplicates[tmsNum])
                {
                    output.WriteLine("   " + item);
                }
            }

            output.WriteLine("\r\n-------ASSIGNED TO ME IN REDMINE-------");
            List<string> me = new List<string>();
            me.Add("Waldemar Dacko");
            
            foreach (TMSItem item in rmTMSDict.GetNotClosedNotUsedAssignedToDEV1ItemList(me))
            {
                output.WriteLine(item.TMS);
                /*
                DataRow r = dt.NewRow();
                FillOutputGridRowWithTMSRMData(item, r, Consts.RFC_ASSIGNED_TO_ME_IN_RM);
                dt.Rows.Add(r);
                */

            }


            output.WriteLine("\r\n-------ASSIGNED TO ME IN TMS-------");
            me.Add("WALDEMARD");

            foreach (TMSItem item in dbTMSDict.GetNotClosedNotUsedAssignedToDEV1ItemList(me))
            {
                output.WriteLine(item.TMS);

                DataRow r = dt.NewRow();
                FillOutputGridRowWithTMSData(item, r, Consts.RFC_ASSIGNED_TO_ME_IN_TMS);
                dt.Rows.Add(r);
            }


            output.WriteLine("\r\n----------------------------");

            dt.AcceptChanges();
            output.WriteToGrid(dt);

        }

        private static void FillOutputGridRowWithTMSRMData(TMSItem item, DataRow r, string reason)
        {
            r["RM"] = item.RMId;
            r["RM_Assigned"] = item.AssignedTo;
            r["RM_Status"] = item.Status;

            r["Reason"] = reason;

            if (IsAssignedToMe(item.AssignedTo))
            {
                r["Me"] = "Y";
            }
        }


        private static void FillOutputGridRowWithTMSData(TMSItem item, DataRow r, string reason)
        {
            r["Reason"] = reason;
            r["TMS"] = item.TMS;
            r["TMS_Assigned"] = item.AssignedTo;
            r["TMS_Status"] = item.Status;

            if (IsAssignedToMe(item.AssignedTo))
            {
                r["Me"] = "Y";
            }
        }

        private static bool IsAssignedToMe(string text)
        {

            return (text != null && text.Contains("WALDEMAR") || text.Contains("waldekd") || text.Contains("Waldemar"));
        }

        private static TMSDictionary GetTMSDataFromRedMine(IOutputable output)
        {
            Stopwatch sw = new Stopwatch();
            sw.StartStopwatchAndPrintMessage("Getting TMS data from RedMine...", output);
            NameValueCollection parameters = new NameValueCollection { { "status_id", "*" } };
            //List<TMSItem> redMineTMSList = new List<TMSItem>();

            TMSDictionary dict = new TMSDictionary();
            if (!Consts.TEST_MODE)
            {
                foreach (var issue in RMManegerService.RMManager.GetObjects<Issue>(parameters).Where(issue => issue.Project.Id == Consts.PROJECT_NAMES.TMS.MACBI.PROBLEMS))
                {
                   /*string subject = issue.Subject;

                    TMSItem itemFromRM = new TMSItem();
                    itemFromRM.Source = Consts.SRC_RM;
                    itemFromRM.AssignedTo = issue.AssignedTo.TryGetName();
                    itemFromRM.Desctiption = issue.Description;

                    string[] subjectSplitted = issue.Subject.Split('-');
                    itemFromRM.TMS = issue.Subject.Split('-')[0].Trim() + "-" + issue.Subject.Split('-')[1].Trim();
                    itemFromRM.Status = issue.Status.TryGetName();

                    //redMineTMSList.Add(itemFromRM);
                   */
                    dict.Add(new TMSItem(issue));
                }

                //redMineTMSList.SerializeTMSItemData();
                dict.SerializeTMSItemData(Consts.FILE_NAMES.RM_TMS_CACHE);
            }
            else
            {
                dict = TMSDictionary.DeserializeTMSItemData(Consts.FILE_NAMES.RM_TMS_CACHE);
            }

            sw.StopStopwatchAndPrintDoneMessageWithElapsedTime(output);
            return dict;
        }

        //private static List<TMSItem> GetTMSDataFromRedMine()
        //{
        //    Stopwatch sw = new Stopwatch();
        //    sw.StartStopwatchAndPrintMessage("Getting TMS data from RedMine...");
        //    NameValueCollection parameters = new NameValueCollection { { "status_id", "*" } };
        //    List<TMSItem> redMineTMSList = new List<TMSItem>();
        //    if (!Consts.TEST_MODE)
        //    {
        //        foreach (var issue in RMManegerService.RMManager.GetObjects<Issue>(parameters).Where(issue => issue.Project.Id == Consts.PROJECT_NAMES.TMS.MACBI.PROBLEMS))
        //        {
        //            string subject = issue.Subject;

        //            TMSItem itemFromRM = new TMSItem();
        //            itemFromRM.Source = Consts.SRC_RM;
        //            itemFromRM.AssignedTo = issue.AssignedTo.TryGetName();
        //            itemFromRM.Desctiption = issue.Description;

        //            string[] subjectSplitted = issue.Subject.Split('-');
        //            itemFromRM.TMS = issue.Subject.Split('-')[0].Trim() + "-" + issue.Subject.Split('-')[1].Trim();
        //            itemFromRM.Status = issue.Status.TryGetName();

        //            redMineTMSList.Add(itemFromRM);
        //        }

        //        redMineTMSList.SerializeTMSItemData();
        //    }
        //    else
        //    {
        //        redMineTMSList = redMineTMSList.DeserializeTMSItemData();
        //    }

        //    sw.StopStopwatchAndPrintDoneMessageWithElapsedTime();
        //    return redMineTMSList;
        //}

        public DataTable GetActionsForTMS(string number)
        {
            DataTable tmsActions = new DataTable();
            string cacheFileName = string.Format(Consts.FILE_NAMES.TMS_ACTIONS_CACHE, client,number);

            if (!Consts.TEST_MODE)
            {
                sw.StartStopwatchAndPrintMessage("Getting TMS actions and making cache...", output);
                //Task<DataTable> execureQueryTask = DBService.ExecuteQueryAsync(DBService.GET_ALL_TMS_TASKS);
                tmsActions = DBService.ExecuteQuery(String.Format(DBService.GET_ACTIONS_FOR_TMS, client, number));
                tmsActions.TableName = "TMS_ACTIONS";
                tmsActions.WriteXml(cacheFileName);
            }
            else
            {
                sw.StartStopwatchAndPrintMessage("Getting TMS actions from cache...", output);
                DataSet ds = new DataSet();
                ds.ReadXml(cacheFileName, XmlReadMode.InferSchema);
                tmsActions = ds.Tables[0];
            }
            sw.StopStopwatchAndPrintDoneMessageWithElapsedTime(output);

            return tmsActions;
        }

        private TMSDictionary GetTMSDataFromDB()
        {
            DataTable tmsTasksFromDBDataTable = new DataTable();
            if (!Consts.TEST_MODE)
            {
                sw.StartStopwatchAndPrintMessage("Getting TMS tasks from DB and making cache...", output);
                //Task<DataTable> execureQueryTask = DBService.ExecuteQueryAsync(DBService.GET_ALL_TMS_TASKS);
                tmsTasksFromDBDataTable = DBService.ExecuteQuery(DBService.GET_ALL_TMS_TASKS);
                tmsTasksFromDBDataTable.TableName = "TMS_DATA";
                tmsTasksFromDBDataTable.WriteXml(Consts.FILE_NAMES.DB_TMS_CACHE);
            }
            else
            {
                sw.StartStopwatchAndPrintMessage("Getting TMS tasks from cache...", output);
                DataSet ds = new DataSet();
                ds.ReadXml(Consts.FILE_NAMES.DB_TMS_CACHE, XmlReadMode.InferSchema);
                tmsTasksFromDBDataTable = ds.Tables[0];
            }

            sw.StopStopwatchAndPrintDoneMessageWithElapsedTime(output);

            //Dictionary<string, TMSItem> dbTMSDict = new Dictionary<string, TMSItem>();
            TMSDictionary dbTMSDict = new TMSDictionary();
            //List<string> team_members = TeamService.GetDEV1TeamMembersTMSLogins();

            sw.StartStopwatchAndPrintMessage("Conversion DB items to objects...", output);
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

            sw.StopStopwatchAndPrintDoneMessageWithElapsedTime(output);
            return dbTMSDict;
        }

        public static void CreateTMSCache(IOutputable output)
        {
            output.Write("Cache creation...");
            //NameValueCollection parameters = new NameValueCollection { { "status_id", "*" } };

//            Task<List<Issue>> issuesListFromRemineTask = CommonTools.GetIssuesFromRedmine(Consts.PROJECT_NAMES.TMS.MACBI.PROBLEMS);
            List<Issue> issuesListFromRemine = CommonTools.GetIssuesFromRedmine(Consts.PROJECT_NAMES.TMS.MACBI.PROBLEMS);

            foreach (var issue in issuesListFromRemine)
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

            output.WriteLine("done!");
        }
    }
}
