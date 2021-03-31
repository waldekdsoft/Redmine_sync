using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;
using Redmine.Net.Api;
using Redmine.Net.Api.Types;

namespace Redmine_sync
{
    class Program
    {
        //private static string PROJECT = "macbi-problems";
        private static int PROJECT_ID = 65;//67 - temporary
        private static string MOM_FILES_DIR = @"C:\Users\waldekd\Documents\MOMProblems";
        private static string MOM_FILE_PATH = MOM_FILES_DIR + @"\moms.xlsx";

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
            { "Q26@Generic", new MOMEnvSettings("wp26.softsystem.pl:7700") }            
        };

        static void Main(string[] args)
        {
            Console.WriteLine("Started...");
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(delegate { return true; });
            string host = "http://pcredmine:3000";
            string apiKey = "0533a992d0c093b3b1592e57e10281156ea6afde";

            var manager = new RedmineManager(host, apiKey);
            var parameters = new NameValueCollection();

            //get project
            //MOM problems id: 65
            //var project = manager.GetObject<Project>(/*"mom-problems"*/ "wdtest", null);

            //********************************************************************************************************/
            //write MACBI items to local list


            Console.WriteLine("0) Update based on all XLSX file from the directory");
            Console.WriteLine("1) Add new items");
            Console.WriteLine("2) Update items");

            switch (Console.ReadLine())
            {
                case "0":
                    UpdateItems(manager, true);
                    break;
                case "1":
                    AddNewItems(manager);
                    break;
                case "2":
                    UpdateItems(manager);
                    break;
            }


        }

        private static void UpdateItems(RedmineManager manager, bool allWithinDirectory = false)
        {
            List<StatItem> statItems = new List<StatItem>();
            List<IssueItem> issuesInRedmineProject = new List<IssueItem>();
            List<IssueItem> problematicIssuesInRedmineProject = new List<IssueItem>();

            CreateCache(manager, issuesInRedmineProject, problematicIssuesInRedmineProject);
            UpdateBasedOnExcelFile(manager, issuesInRedmineProject, statItems, allWithinDirectory);
            ShowStats(statItems, false);
        }

        private static void AddNewItems(RedmineManager manager)
        {
            List<StatItem> statItems = new List<StatItem>();
            List<IssueItem> issuesInRedmineProject = new List<IssueItem>();
            List<IssueItem> problematicIssuesInRedmineProject = new List<IssueItem>();

            CreateCache(manager, issuesInRedmineProject, problematicIssuesInRedmineProject);
            ProcessExcelFile(manager, issuesInRedmineProject, statItems);
            ShowStats(statItems, true);
        }

        private static void ShowStats(List<StatItem> statItems, bool added)
        {
            Console.WriteLine("--------------------------------------------");
            //show stats
            foreach (StatItem statItem in statItems)
            {
                if (added)
                {
                    statItem.ShowStatsAdded();
                }
                else
                {
                    statItem.ShowStatsUpdated();
                }
            }
            Console.WriteLine("--------------------------------------------");
        }

        private static void ProcessExcelFile(RedmineManager manager, List<IssueItem> issuesInRedmineProject, List<StatItem> statItems)
        {
            //********************************************************************************************************/
            //read data from Excel
            var xlsx = new LinqToExcel.ExcelQueryFactory(MOM_FILE_PATH);

            foreach (string tabName in xlsx.GetWorksheetNames())
            {
                Console.WriteLine("--------------------------------------------");
                Console.WriteLine("Processing of {0}...", tabName);
                Console.WriteLine("--------------------------------------------");

                MOMEnvSettings momEnvSettings = null;
                if (!MOM_ENV_SETTINGS.TryGetValue(tabName, out momEnvSettings))
                {
                    Console.WriteLine("No MOMEnvSettings for {0}", tabName);
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Start processing: {0}", tabName);

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

                    IdentifiableName p = IdentifiableName.Create<Project>(PROJECT_ID);
                    foreach (var itemFromExcel in query)
                    {
                        string subject = string.Format("{0} - {1} - {2} - {3} - {4}", tabName, itemFromExcel.ProblemID, itemFromExcel.EventCode, itemFromExcel.ProblemCode, itemFromExcel.SenderCode);

                        //check if such the item exists in the Redmine project
                        var redmineIssue = issuesInRedmineProject.Where(issueFromRedmine => issueFromRedmine.Env == tabName && issueFromRedmine.ProblemId == itemFromExcel.ProblemID);
                        if (redmineIssue.Count() == 0)
                        {
                            string details = string.Format("{0}\r\nMessage link: {1}\r\nProblem link: {2}", itemFromExcel.Details, momEnvSettings.GetMessageLink(itemFromExcel.MessageId), momEnvSettings.GetProblemLink(itemFromExcel.MessageId));

                            var newIssue = new Issue { Subject = subject, Project = p, Description = details };
                            manager.CreateObject(newIssue);

                            //add a new item to local cached items from redmine
                            IssueItem item = new IssueItem();

                            item.Env = tabName;
                            item.ProblemId = itemFromExcel.ProblemID;

                            issuesInRedmineProject.Add(item);

                            statItem.Added++;
                        }
                        else
                        {
                            Console.WriteLine("Issue exists! {0}", subject);
                            statItem.AlreadyExisted++;
                        }
                    }
                    statItems.Add(statItem);
                }
            }
        }

        private static void UpdateBasedOnExcelFile(RedmineManager manager, 
                                                   List<IssueItem> issuesInRedmineProject, 
                                                   List<StatItem> statItems, 
                                                   bool allWithinDirectory)
        {
            //********************************************************************************************************/
            //read data from Excel
            List<string> filesToProcess = null;
            if (allWithinDirectory)
            {
                filesToProcess = Directory.EnumerateFiles(MOM_FILES_DIR, "*.xlsx").ToList();
            }
            else
            {
                filesToProcess = new List<string>();
                filesToProcess.Add(MOM_FILE_PATH);
            }

            foreach (string singleXSLXfile in filesToProcess)
            {
                var xlsx = new LinqToExcel.ExcelQueryFactory(singleXSLXfile);

                Console.WriteLine("File: {0}", singleXSLXfile);

                foreach (string tabName in xlsx.GetWorksheetNames())
                {
                    Console.WriteLine("--------------------------------------------");
                    Console.WriteLine("Processing of {0}...", tabName);
                    Console.WriteLine("--------------------------------------------");

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

                    IdentifiableName p = IdentifiableName.Create<Project>(PROJECT_ID);
                    foreach (var itemFromExcel in query)
                    {
                        //look for the item in RM
                        var redmineIssue = issuesInRedmineProject.Where(issueFromRedmine => issueFromRedmine.Env == tabName && issueFromRedmine.ProblemId == itemFromExcel.ProblemID).FirstOrDefault();

                        if (redmineIssue != null && string.IsNullOrEmpty(redmineIssue.SenderCode))
                        {
                            if (!string.IsNullOrEmpty(itemFromExcel.SenderCode))
                            {
                                var issue = manager.GetObject<Issue>(redmineIssue.Id.ToString(), null);

                                issue.Subject = issue.Subject + string.Format(" - {0}", itemFromExcel.SenderCode);

                                manager.UpdateObject(redmineIssue.Id.ToString(), issue);
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


        private static void CreateCache(RedmineManager manager, List<IssueItem> issuesInRedmineProject, List<IssueItem> problematicIssuesInRedmineProject)
        {
            Console.Write("Cache creation...");
            NameValueCollection parameters = new NameValueCollection { { "status_id", "*" } };
            foreach (var issue in manager.GetObjects<Issue>(parameters).Where(issue => issue.Project.Id == PROJECT_ID))
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
                }
                else
                {
                    problematicIssuesInRedmineProject.Add(item);
                }
            }

            Console.WriteLine("done!");
        }
     
    }
}
