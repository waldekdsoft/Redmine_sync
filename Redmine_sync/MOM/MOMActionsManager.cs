using Redmine.Net.Api.Types;
using Redmine_sync.GUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redmine_sync
{
    class MOMActionsManager
    {
        static IOutputable output = null;

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
            { "Q26@Generic", new MOMEnvSettings("wp26.softsystem.pl:7700") },
            { "Q336@MACBI", new MOMEnvSettings("wp336.softsystem.pl:7700") },
            { "L014@MACBI", new MOMEnvSettings("lxc014.softsystem.pl:8425") }
        };

        public MOMActionsManager(IOutputable out1)
        {
            output = out1;
        }

        public void UpdateItems(bool allWithinDirectory = false)
        {
            List<StatItem> statItems = new List<StatItem>();
            List<IssueItem> issuesInRedmineProject = new List<IssueItem>();
            List<IssueItem> problematicIssuesInRedmineProject = new List<IssueItem>();

            CreateMOMCache(issuesInRedmineProject, problematicIssuesInRedmineProject, Consts.PROJECT_NAMES.MOM.PROBLEMS);
            UpdateBasedOnExcelFile(issuesInRedmineProject, statItems, allWithinDirectory);
            ShowStats(statItems, false);
        }

        public static void BuildFinalStats()
        {
            List<StatItem> statItems = new List<StatItem>();
            List<IssueItem> issuesInRedmineProject = new List<IssueItem>();
            List<IssueItem> problematicIssuesInRedmineProject = new List<IssueItem>();
            CreateMOMCache(issuesInRedmineProject, problematicIssuesInRedmineProject, Consts.PROJECT_NAMES.MOM.PROBLEMS);

            Dictionary<string /*env*/, FinalStatItem> finalStatDict = new Dictionary<string, FinalStatItem>();
            GatherFullStats(issuesInRedmineProject, finalStatDict);
            DisplayFullStats(finalStatDict);
        }

        public static void CreateMOMCache(List<IssueItem> issuesInRedmineProject, List<IssueItem> problematicIssuesInRedmineProject, int project_id)
        {
            Console.Write("Cache creation...");

            List<Issue> issuesListFromRemine = CommonTools.GetIssuesFromRedmine(project_id);

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
                }
                else
                {
                    problematicIssuesInRedmineProject.Add(item);
                }
            }

            Console.WriteLine("done!");
        }

        private static void DisplayFullStats(Dictionary<string, FinalStatItem> finalStatDict)
        {

            output.WriteLine(string.Format("{0,-20} {1,-10} {2,-10}", "Env", "New", "Others"));
            
            output.WriteLine(CommonTools.SEPARAT_LINE);

            foreach (string env in finalStatDict.Keys)
            {
                output.WriteLine(string.Format("{0,-20} {1,-10} {2,-10}", env, CommonTools.DontDisplayZero(finalStatDict[env].New), CommonTools.DontDisplayZero(finalStatDict[env].Others)));
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

        public void AddNewItems()
        {
            List<StatItem> statItems = new List<StatItem>();
            List<IssueItem> issuesInRedmineProject = new List<IssueItem>();
            List<IssueItem> problematicIssuesInRedmineProject = new List<IssueItem>();

            CreateMOMCache(issuesInRedmineProject, problematicIssuesInRedmineProject, Consts.PROJECT_NAMES.MOM.PROBLEMS);
            ProcessExcelFile(issuesInRedmineProject, statItems);
            ShowStats(statItems, true);
        }

        private static void ProcessExcelFile(List<IssueItem> issuesInRedmineProject, List<StatItem> statItems)
        {
            //********************************************************************************************************/
            //read data from Excel
            var xlsx = new LinqToExcel.ExcelQueryFactory(MOM_FILE_PATH);

            foreach (string tabName in xlsx.GetWorksheetNames())
            {
                output.WriteLine("--------------------------------------------");
                output.WriteLine(string.Format("Processing of {0}...", tabName));
                output.WriteLine("--------------------------------------------");

                MOMEnvSettings momEnvSettings = null;
                if (!MOM_ENV_SETTINGS.TryGetValue(tabName, out momEnvSettings))
                {
                    output.WriteLine(string.Format("No MOMEnvSettings for {0}", tabName));
                    //output.ReadKey();
                }
                else
                {
                    output.WriteLine(string.Format("Start processing: {0}", tabName));

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
                            output.WriteLine(string.Format("Issue exists! {0}", subject));
                            statItem.AlreadyExisted++;
                        }
                    }
                    statItems.Add(statItem);
                }
            }
        }

        private static void ShowStats(List<StatItem> statItems, bool added)
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

                output.WriteLine(string.Format("File: {0}", singleXSLXfile));

                foreach (string tabName in xlsx.GetWorksheetNames())
                {
                    output.WriteLine("--------------------------------------------");
                    output.WriteLine(string.Format("Processing of {0}...", tabName));
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
