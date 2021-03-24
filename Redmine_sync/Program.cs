using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
            { "Q507@FCS", new MOMEnvSettings("wp507.softsystem.pl:7700") }
        };

        static void Main(string[] args)
        {

            Console.WriteLine("Started...");
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback (   delegate { return true; });
            string host = "http://pcredmine:3000";
            string apiKey = "0533a992d0c093b3b1592e57e10281156ea6afde";

            var manager = new RedmineManager(host, apiKey);
            var parameters = new NameValueCollection();

            //get project
            //MOM problems id: 65
            //var project = manager.GetObject<Project>(/*"mom-problems"*/ "wdtest", null);

            //********************************************************************************************************/
            //write MACBI items to local list
            List<IssueItem> issuesInRedmineProject = new List<IssueItem>();
            List<IssueItem> problematicIssuesInRedmineProject = new List<IssueItem>();
            List<StatItem> statItems = new List<StatItem>();

            Console.Write("Cache creation...");
            parameters = new NameValueCollection { { "status_id", "*" } };
            foreach (var issue in manager.GetObjects<Issue>(parameters).Where(issue => issue.Project.Id == PROJECT_ID))
            {
                
                string subject = issue.Subject;

                //split subject to get env and problem id
                string[] subjectSplitted = subject.Split('-');

                //get env
                string env = subjectSplitted[0].Trim(); ;

                IssueItem item = new IssueItem();
                item.Id = issue.Id;
                item.Status = issue.Status.Name;
                item.Desc = subject;
                item.Env = env;

                if (subjectSplitted.Length == 4)
                {
                    //get MOM problem is from subject
                    item.MOMProblemId = subjectSplitted[1].Trim();
                    issuesInRedmineProject.Add(item);
                }
                else
                {
                    problematicIssuesInRedmineProject.Add(item);
                }

            }

            Console.Write("done!");
            //********************************************************************************************************/
            //read data from Excel
            var xlsx = new LinqToExcel.ExcelQueryFactory(@"C:\Users\waldekd\Documents\MOMProblems\moms.xlsx");

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
                      }
                      select item;



                    IdentifiableName p = IdentifiableName.Create<Project>(PROJECT_ID);
                    foreach (var itemFromExcel in query)
                    {
                        string subject = string.Format("{0} - {1} - {2} - {3}", tabName, itemFromExcel.ProblemID, itemFromExcel.EventCode, itemFromExcel.ProblemCode);

                        //check if such the item exists in the Redmine project
                        var redmineIssue = issuesInRedmineProject.Where(issueFromRedmine => issueFromRedmine.Env == tabName && issueFromRedmine.MOMProblemId == itemFromExcel.ProblemID);
                        if (redmineIssue.Count() == 0)
                        {

                            string details = string.Format("{0}\r\nMessage link: {1}\r\nProblem link: {2}", itemFromExcel.Details, momEnvSettings.GetMessageLink(itemFromExcel.MessageId), momEnvSettings.GetProblemLink(itemFromExcel.MessageId));

                            var newIssue = new Issue { Subject = subject, Project = p, Description = details };
                            manager.CreateObject(newIssue);

                            //add a new item to local cached items from redmine
                            IssueItem item = new IssueItem();

                            item.Env = tabName;
                            item.MOMProblemId = itemFromExcel.ProblemID;

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

            Console.WriteLine("--------------------------------------------");
            //show stats
            foreach (StatItem statItem in statItems)
            {
                statItem.ShowStats();
            }

           
            return;

            #region commented
            //foreach (var item in query)
            //{
            //    Console.WriteLine(item.MessageId);  

            //}
            //temp task
            //string TMS = "MACBI-02580";

            /*
                       string subject = string.Format("{0} - {1} - {2} - {3}", tabName, item.ProblemID, item.EventCode, item.ProblemCode);
                       var newIssue = new Issue { Subject = subject, Project = p };
                       manager.CreateObject(newIssue);
                       */
            //Console.WriteLine();

            //parameters.Add(RedmineKeys.SUBJECT, "~MACBI-02576");

            //List<Issue> issues = manager.GetObjects<Issue>(parameters);

            //int a = 0;
            //manager.GetObjec


            //foreach (var proj in manager.GetObjects<Project>())
            //{
            //    Console.WriteLine("Id:{0} Identifier:{1} Name:{2} Status:{3}", proj.Id.ToString(), proj.Identifier, proj.Name, proj.Status);
            //}
            ////int a = 0;

            //parameter - get all issues
            //var parameters = new NameValueCollection { { RedmineKeys.STATUS_ID, RedmineKeys.ALL } };



            /*
            //parameter - fetch issues for a date range
            parameters.Add(RedmineKeys.PROJECT_ID, "15");
            //parameters.Add(RedmineKeys.STA, PROJECT);

            foreach (var issue in manager.GetObjects<Issue>(parameters))
            {
                Console.WriteLine("Issue: {0}.", issue.Id);
            }

            */


            //foreach (var issue in manager.GetObjects<Issue>(parameters))
            //{
            //    Console.WriteLine("Issue: {0}.", issue.Id);
            //}
            //ts<Project>.

            //foreach (var proj in manager.GetObjects<Project>())
            //{
            //    Console.WriteLine("Id:{0} Identifier:{1} Name:{2} Status:{3}", proj.Id.ToString(), proj.Identifier, proj.Name, proj.Status);
            //}

            //id: 28
            //var project = manager.GetObject<Project>("28", null);

            //Console.WriteLine("test");
            //var parameters = new NameValueCollection { { "project_id", "28" } };

            //foreach (var issue in manager.GetObjects<Issue>(parameters))
            //{
            //    if (issue.AssignedTo != null)
            //    {
            //        Console.WriteLine(issue.AssignedTo.Name);
            //    }
            //}

            //    var parameters = new NameValueCollection { { "issues", "*" } };
            //var parameters = new NameValueCollection { { "assigned_to", "twolak" } };




            //foreach (var issue in manager.GetObjects<Issue>(parameters))
            //{
            //    Console.WriteLine("#{0}: {1}", issue.Id, issue.Subject);
            //}

            //foreach (var project in manager.GetObjects<Project>(parameters))
            //{
            //    Console.WriteLine("#{0}: {1}", project.Id, project.Name);
            //}

            /*
            Project p = new Project();
            p.Name = "4.1.16.1_STS_DEV1";
            p.Identifier = "41161stsdev1";

            manager.CreateObject<Project>(p);
            */

            //foreach (var issue in manager.GetObjects<Issue>(parameters))
            //{
            //    Console.WriteLine("#{0}: {1}", issue.Id, issue.Subject);
            //}

            //Create a issue.

            //var newIssue = new Issue { Subject = "test", Project = new IdentifiableName { Id = 1 } };
            //manager.CreateObject(newIssue);

            #endregion

        }
    }
}
