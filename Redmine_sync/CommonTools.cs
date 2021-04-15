using Redmine.Net.Api.Types;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redmine_sync
{
    class CommonTools
    {
        public static string SEPARAT_LINE = new string('-', 50);


        public static string TryGetName(IdentifiableName s)
        {
            if (s == null)
            {
                return null;
            }
            else
            {
                return s.Name;
            }
        }


        public static string DontDisplayZero(int i)
        {
            return i == 0 ? "" : Convert.ToString(i);
        }

        public static void CreateMOMCache(List<IssueItem> issuesInRedmineProject, List<IssueItem> problematicIssuesInRedmineProject, int project_id)
        {
            Console.Write("Cache creation...");
            NameValueCollection parameters = new NameValueCollection { { "status_id", "*" } };
            foreach (var issue in RMManegerService.RMManager.GetObjects<Issue>(parameters).Where(issue => issue.Project.Id == project_id))
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
