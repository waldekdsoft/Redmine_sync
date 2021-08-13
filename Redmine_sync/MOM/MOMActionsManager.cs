using Redmine.Net.Api.Types;
using Redmine_sync.Cache;
using Redmine_sync.GUI;
using Redmine_sync.MOM;
using Redmine_sync.Tools;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Redmine_sync
{
    class MOMActionsManager
    {
        static IOutputable output = null;

        private static string FILES_DIR = @"C:\Users\waldekd\Documents\MOMProblems";
        private static string MOM_FILE_PATH = FILES_DIR + @"\moms.xlsx";
        private static string TXT_FILE_PATH = FILES_DIR + @"\moms.txt";

        private static IDatabase cache = null;

        private static Dictionary<string, MOMEnvSettings> MOM_ENV_SETTINGS;
        /*
        private static Dictionary<string, MOMEnvSettings> MOM_ENV_SETTINGS = new Dictionary<string, MOMEnvSettings>() {
            { "L058@MACBI", new MOMEnvSettings("lxc058.softsystem.pl:7701") },
            { "L094@MACBI", new MOMEnvSettings("lxc094.softsystem.pl:8702") },
            { "Q167@Generic", new MOMEnvSettings("wp167.softsystem.pl:7700") },
            { "Q18@Generic", new MOMEnvSettings("wp18.softsystem.pl:7700") },
            { "Q311@AON", new MOMEnvSettings("wp311.softsystem.pl:7700") },
            { "Q337@MAYO", new MOMEnvSettings("wp337.softsystem.pl:7700") },
            { "Q397@UMICH", new MOMEnvSettings("wp397.softsystem.pl:7700") },
            { "Q486@MAYO", new MOMEnvSettings("wp486.softsystem.pl:7700") },
            { "Q501@Generic", new MOMEnvSettings("wp501.softsystem.pl:7700") },
            { "Q507@FCS", new MOMEnvSettings("wp507.softsystem.pl:7700") },
            { "Q26@Generic", new MOMEnvSettings("wp26.softsystem.pl:7700") },
            { "Q336@MACBI", new MOMEnvSettings("wp336.softsystem.pl:7700") },
            { "Q508@AON", new MOMEnvSettings("wp508.softsystem.pl:7700") },
            { "L071@VIBRA", new MOMEnvSettings("lxc071.softsystem.pl:7701") } ,
            { "L081@SCPMG", new MOMEnvSettings("lxc081.softsystem.pl:8265") } ,
            { "Q16@MAYO", new MOMEnvSettings("wp16.softsystem.pl:7700") } ,
            { "Q448@Generic", new MOMEnvSettings("wp448.softsystem.pl:7700") } ,            
            { "L014@MACBI", new MOMEnvSettings("lxc014.softsystem.pl:8425") },
            { "Q414@CHUR", new MOMEnvSettings("wp414.softsystem.pl:7700") }
        };
        */

        public MOMActionsManager(IOutputable out1)
        {
            output = out1;
        }

        static MOMActionsManager()        
        {
            MOM_ENV_SETTINGS = EnvPropertiesFileReader.Read();
        }

        public void UpdateItems(bool allWithinDirectory = false)
        {
            List<StatItem> statItems = new List<StatItem>();
            List<IssueItem> issuesInRedmineProject = new List<IssueItem>();
            List<IssueItem> problematicIssuesInRedmineProject = new List<IssueItem>();            

            CreateMOMCache(issuesInRedmineProject, problematicIssuesInRedmineProject, Consts.PROJECT_NAMES.MOM.PROBLEMS, output);
            UpdateBasedOnExcelFile(issuesInRedmineProject, statItems, allWithinDirectory);
            ShowStats(statItems, false);
        }

        public static void BuildFinalStats()
        {
            List<IssueItem> issuesInRedmineProject = new List<IssueItem>();
            List<IssueItem> problematicIssuesInRedmineProject = new List<IssueItem>();
            CreateMOMCache(issuesInRedmineProject, problematicIssuesInRedmineProject, Consts.PROJECT_NAMES.MOM.PROBLEMS, output);

            Dictionary<string /*env*/, FinalStatItem> finalStatDict = new Dictionary<string, FinalStatItem>();
            GatherFullStats(issuesInRedmineProject, finalStatDict);
            DisplayFullStats(finalStatDict);
        }

        public static void CreateMOMCache(List<IssueItem> issuesInRedmineProject, List<IssueItem> problematicIssuesInRedmineProject, int project_id, IOutputable output)
        {
            output.Write("Cache creation...");

            List<Issue> issuesListFromRemine = CommonTools.GetIssuesFromRedmine(project_id);

            if (output.GetIsRedisUse())
            {
                cache = RedisConnectorHelper.Connection.GetDatabase();
            }

            foreach (var issue in issuesListFromRemine.Where(issue => issue.Project.Id == project_id))
            {
                string subject = issue.Subject;

                //split subject to get env and problem id
                string[] subjectSplitted = subject.Split('-');

                //get env
                string env = subjectSplitted[0].Trim();

                IssueItem item = new IssueItem();
                item.Id = issue.Id;
                item.Status = issue.Status.Name;
                item.Desc = subject;
                item.Env = env;

                if (subjectSplitted.Length >= 4)
                {
                    //get MOM problem is from subject
                    item.ProblemId = subjectSplitted[1].Trim();

                    //look for sender code
                    if (subjectSplitted.Length >= 5)
                    {
                        item.SenderCode = subjectSplitted[4].Trim();
                    }
                    issuesInRedmineProject.Add(item);

                    //cache.HashSetAsync("dd", "ddd", "ddd");
                }
                else
                {
                    problematicIssuesInRedmineProject.Add(item);
                }
            }

            output.WriteLine("done!");
        }

        private static void DisplayFullStats(Dictionary<string, FinalStatItem> finalStatDict)
        {

            output.WriteLine("{0,-20} {1,-10} {2,-10}", "Env", "New", "Others");
            
            output.WriteLine(CommonTools.SEPARAT_LINE);

            foreach (string env in finalStatDict.Keys)
            {
                output.WriteLine("{0,-20} {1,-10} {2,-10}", env, CommonTools.DontDisplayZero(finalStatDict[env].New), CommonTools.DontDisplayZero(finalStatDict[env].Others));
            }

        }

        private static void GatherFullStats(List<IssueItem> issuesInRedmineProject, Dictionary<string, FinalStatItem> finalStatDict)
        {
            foreach (IssueItem issue in issuesInRedmineProject)
            {
                string env = issue.Env;
                FinalStatItem finalStatItem = null;

                //check if such env exists in the dics
                if (!finalStatDict.TryGetValue(env, out finalStatItem))
                {
                    finalStatItem = new FinalStatItem();
                    finalStatDict.Add(env, finalStatItem);
                }

                if (issue.Status == "New")
                {
                    finalStatItem.New++;
                }
                else
                {
                    finalStatItem.Others++;
                }
            }
        }

        public void AddNewItemsFromExcel()
        {
            List<StatItem> statItems = new List<StatItem>();
            List<IssueItem> issuesInRedmineProject = new List<IssueItem>();
            List<IssueItem> problematicIssuesInRedmineProject = new List<IssueItem>();
            List<string> envsNotExistingInConfigs = new List<string>();

            CreateMOMCache(issuesInRedmineProject, problematicIssuesInRedmineProject, Consts.PROJECT_NAMES.MOM.PROBLEMS, output);
            ProcessExcelFile(issuesInRedmineProject, statItems, envsNotExistingInConfigs);
            ShowStats(statItems, true, envsNotExistingInConfigs);
        }

        public void AddNewItemsFromTXT()
        {
            List<StatItem> statItems = new List<StatItem>();
            List<IssueItem> issuesInRedmineProject = new List<IssueItem>();
            List<IssueItem> problematicIssuesInRedmineProject = new List<IssueItem>();
            List<string> envsNotExistingInConfigs = new List<string>();

           CreateMOMCache(issuesInRedmineProject, problematicIssuesInRedmineProject, Consts.PROJECT_NAMES.MOM.PROBLEMS, output);
            ProcessTxtFile(issuesInRedmineProject, statItems, envsNotExistingInConfigs);
            ShowStats(statItems, true, envsNotExistingInConfigs);
        }

        private static void ProcessExcelFile(List<IssueItem> issuesInRedmineProject, List<StatItem> statItems, List<string> envsNotExistingInConfigs)
        {
            //********************************************************************************************************/
            //read data from Excel
            var xlsx = new LinqToExcel.ExcelQueryFactory(MOM_FILE_PATH);

            foreach (string tabName in xlsx.GetWorksheetNames())
            {
                output.WriteLine("--------------------------------------------");
                output.WriteLine("Processing of {0}...", tabName);
                output.WriteLine("--------------------------------------------");

                MOMEnvSettings momEnvSettings = null;
                if (!MOM_ENV_SETTINGS.TryGetValue(tabName, out momEnvSettings))
                {
                    output.WriteLine("No MOMEnvSettings for {0}", tabName);
                    envsNotExistingInConfigs.Add(tabName);
                    //output.ReadKey();
                }
                else
                {
                    output.WriteLine("Start processing: {0}", tabName);

                    StatItem statItem = new StatItem();
                    statItem.Env = tabName;

                    var query =
                      from row in xlsx.Worksheet(tabName)
                      let item = new
                      {
                          ProblemID = row["Problem ID"].Cast<string>(),
                          ProblemCode = row["Problem Code"].Cast<string>(),
                          MessageId = row["Message ID"].Cast<string>(),
                          EventCode = row["Event Code"].Cast<string>(),
                          Details = row["Details"].Cast<string>(),
                          SenderCode = row["Sender Code"].Cast<string>(),
                      }
                      select item;

                    IdentifiableName p = IdentifiableName.Create<Project>(Consts.PROJECT_NAMES.MOM.PROBLEMS);
                    foreach (var itemFromExcel in query)
                    {
                        string subject = string.Format("{0} - {1} - {2} - {3} - {4}", tabName, itemFromExcel.ProblemID, itemFromExcel.EventCode, itemFromExcel.ProblemCode, itemFromExcel.SenderCode);

                        //check if such the item exists in the Redmine project
                        var redmineIssue = issuesInRedmineProject.Where(issueFromRedmine => issueFromRedmine.Env == tabName && issueFromRedmine.ProblemId == itemFromExcel.ProblemID);
                        if (redmineIssue.Count() == 0)
                        {
                            string details = string.Format("{0}\r\nMessage link: {1}\r\nProblem link: {2}", itemFromExcel.Details, momEnvSettings.GetMessageLink(itemFromExcel.MessageId), momEnvSettings.GetProblemLink(itemFromExcel.MessageId));

                            var newIssue = new Issue { Subject = subject, Project = p, Description = details };
                            RMManegerService.RMManager.CreateObject(newIssue);

                            //add a new item to local cached items from redmine
                            IssueItem item = new IssueItem();

                            item.Env = tabName;
                            item.ProblemId = itemFromExcel.ProblemID;

                            issuesInRedmineProject.Add(item);

                            statItem.Added++;
                        }
                        else
                        {
                            output.WriteLine("Issue exists! {0}", subject);
                            statItem.AlreadyExisted++;
                        }
                    }
                    statItems.Add(statItem);
                }
            }
        }

        private static void ProcessTxtFile(List<IssueItem> issuesInRedmineProject, List<StatItem> statItems, List<string> envsNotExistingInConfigs)
        {
            //********************************************************************************************************/
            //read data from Excel
            var txt = System.IO.File.ReadAllLines(TXT_FILE_PATH);
            string env = string.Empty;
            Dictionary<string, List<MOMItem>> mom_items_dict = new Dictionary<string, List<MOMItem>>();

            foreach (string line in txt.Where(line => !string.IsNullOrWhiteSpace(line)))
            {
                string[] splitted_line = line.Split(' ');
                string first_word = string.Empty;

                if (splitted_line.Count() > 1)
                {
                    first_word = splitted_line[0];
                }

                if (!string.IsNullOrWhiteSpace(first_word))
                {
                    if (first_word == "Environment:")
                    {
                        env = splitted_line[1].Trim();
                        continue;
                    }
                    else if (Regex.IsMatch(line, @"^\d"))
                    {
                        MOMItem momitem = new MOMItem();

                        string problemId = splitted_line[0];
                        momitem.ProblemID = problemId;

                        string[] mom_message_splitted = line.Split('\t');

                        //problem link
                        //7228 <http://lxc014.softsystem.pl:8425/mom/console/problem_search.jsp?messageId=8741440&status=ALL>
                        string problemid_with_problemlink = mom_message_splitted[0];
                        string problemlink = problemid_with_problemlink.Trim().Split(' ')[1].Replace("<", "").Replace(">", "");
                        momitem.ProblemLink = problemlink;

                        momitem.ProblemCode = mom_message_splitted[4];

                        //message id + message link
                        string[] messageid_with_messagelink_splitted = mom_message_splitted[5].Trim().Split(' ');
                        string messageId = messageid_with_messagelink_splitted[0];
                        string messagelink = messageid_with_messagelink_splitted[1].Replace("<", "").Replace(">", "");
                        momitem.MessageId = messageId;
                        momitem.MessageLink = messagelink;

                        string eventCode = mom_message_splitted[8];
                        momitem.EventCode = eventCode;

                        string details = mom_message_splitted[10];
                        momitem.Details = details;

                        string senderCode = mom_message_splitted[7];
                        momitem.SenderCode = senderCode;

                        if (!mom_items_dict.ContainsKey(env))
                        {
                            List<MOMItem> items = new List<MOMItem>();
                            items.Add(momitem);
                            mom_items_dict.Add(env, items);
                        }
                        else
                        {
                            mom_items_dict[env].Add(momitem);
                        }
                    }
                }
            }

            foreach (string envName in mom_items_dict.Keys)
            {
                output.WriteLine("--------------------------------------------");
                output.WriteLine("Processing of {0}...", envName);
                output.WriteLine("--------------------------------------------");

                MOMEnvSettings momEnvSettings = null;
                if (!MOM_ENV_SETTINGS.TryGetValue(envName, out momEnvSettings))
                {
                    output.WriteLine("No MOMEnvSettings for {0}", envName);
                    envsNotExistingInConfigs.Add(envName);
                }
                else
                {
                    output.WriteLine("Start processing: {0}", envName);

                    StatItem statItem = new StatItem();
                    statItem.Env = envName;

                    /*
                    var query =
                      from row in xlsx.Worksheet(envName)
                      let item = new
                      {
                          ProblemID = row["Problem ID"].Cast<string>(),
                          ProblemCode = row["Problem Code"].Cast<string>(),
                          MessageId = row["Message ID"].Cast<string>(),
                          EventCode = row["Event Code"].Cast<string>(),
                          Details = row["Details"].Cast<string>(),
                          SenderCode = row["Sender Code"].Cast<string>(),
                      }
                      select item;
                    */

                    IdentifiableName p = IdentifiableName.Create<Project>(Consts.PROJECT_NAMES.MOM.PROBLEMS);
                    foreach (var itemFromTxt in mom_items_dict[envName])
                    {
                        string subject = string.Format("{0} - {1} - {2} - {3} - {4}", envName, itemFromTxt.ProblemID, itemFromTxt.EventCode, itemFromTxt.ProblemCode, itemFromTxt.SenderCode);

                        //check if such the item exists in the Redmine project
                        var redmineIssue = issuesInRedmineProject.Where(issueFromRedmine => issueFromRedmine.Env == envName && issueFromRedmine.ProblemId == itemFromTxt.ProblemID);
                        if (redmineIssue.Count() == 0)
                        {
                            string details = string.Format("{0}\r\nMessage link: {1}\r\nProblem link: {2}", itemFromTxt.Details, itemFromTxt.MessageLink, itemFromTxt.ProblemLink);

                            var newIssue = new Issue { Subject = subject, Project = p, Description = details };
                            RMManegerService.RMManager.CreateObject(newIssue);

                            //add a new item to local cached items from redmine
                            IssueItem item = new IssueItem();

                            item.Env = envName;
                            item.ProblemId = itemFromTxt.ProblemID;

                            issuesInRedmineProject.Add(item);

                            statItem.Added++;
                        }
                        else
                        {
                            output.WriteLine("Issue exists! {0}", subject);
                            statItem.AlreadyExisted++;
                        }
                    }
                    statItems.Add(statItem);
                }
            }

        }



        private static void ShowStats(List<StatItem> statItems, bool added, List<string> envsNotExistingInConfigs = null)
        {
            output.WriteLine("--------------------------------------------");
            //show stats
            foreach (StatItem statItem in statItems)
            {
                if (added)
                {
                    output.WriteLine(statItem.GetItemAddedMessage());
                }
                else
                {
                    output.WriteLine(statItem.GetItemUpdatedMessage());
                }
            }
            output.WriteLine("--------------------------------------------");
            if (envsNotExistingInConfigs != null && envsNotExistingInConfigs.Count > 0)
            {
                output.WriteLine("No configuration for environments:");

                foreach (string envName in envsNotExistingInConfigs)
                {
                    output.WriteLine(envName);
                }
            }
        }

        private static void UpdateBasedOnExcelFile(List<IssueItem> issuesInRedmineProject,
                                                List<StatItem> statItems,
                                                bool allWithinDirectory)
        {
            //********************************************************************************************************/
            //read data from Excel
            List<string> filesToProcess = null;
            if (allWithinDirectory)
            {
                filesToProcess = Directory.EnumerateFiles(FILES_DIR, "*.xlsx").ToList();
            }
            else
            {
                filesToProcess = new List<string>();
                filesToProcess.Add(MOM_FILE_PATH);
            }

            foreach (string singleXSLXfile in filesToProcess)
            {
                var xlsx = new LinqToExcel.ExcelQueryFactory(singleXSLXfile);

                output.WriteLine("File: {0}", singleXSLXfile);

                foreach (string tabName in xlsx.GetWorksheetNames())
                {
                    output.WriteLine("--------------------------------------------");
                    output.WriteLine("Processing of {0}...", tabName);
                    output.WriteLine("--------------------------------------------");

                    StatItem statItem = new StatItem(tabName);
                    statItem.Env = tabName;

                    var query =
                      from row in xlsx.Worksheet(tabName)
                      let item = new
                      {
                          ProblemID = row["Problem ID"].Cast<string>(),
                          ProblemCode = row["Problem Code"].Cast<string>(),
                          MessageId = row["Message ID"].Cast<string>(),
                          EventCode = row["Event Code"].Cast<string>(),
                          Details = row["Details"].Cast<string>(),
                          SenderCode = row["Sender Code"].Cast<string>(),
                      }
                      select item;

                    IdentifiableName p = IdentifiableName.Create<Project>(Consts.PROJECT_NAMES.MOM.PROBLEMS);
                    foreach (var itemFromExcel in query)
                    {
                        //look for the item in RM
                        var redmineIssue = issuesInRedmineProject.Where(issueFromRedmine => issueFromRedmine.Env == tabName && issueFromRedmine.ProblemId == itemFromExcel.ProblemID).FirstOrDefault();

                        if (redmineIssue != null && string.IsNullOrEmpty(redmineIssue.SenderCode))
                        {
                            if (!string.IsNullOrEmpty(itemFromExcel.SenderCode))
                            {
                                var issue = RMManegerService.RMManager.GetObject<Issue>(redmineIssue.Id.ToString(), null);

                                issue.Subject = issue.Subject + string.Format(" - {0}", itemFromExcel.SenderCode);

                                RMManegerService.RMManager.UpdateObject(redmineIssue.Id.ToString(), issue);
                                redmineIssue.SenderCode = itemFromExcel.SenderCode;

                                statItem.Updated++;
                                //  string subject = redmineIssue.sub
                                //string subject = string.Format("{0} - {1} - {2} - {3} - {4}", tabName, itemFromExcel.ProblemID, itemFromExcel.EventCode, itemFromExcel.ProblemCode, itemFromExcel.SenderCode);
                            }
                            else
                            {
                                statItem.NotUpdated++;
                            }

                        }
                        else
                        {
                            statItem.NotUpdated++;
                        }

                    }
                    statItems.Add(statItem);
                }
            }
        }

    }
}
