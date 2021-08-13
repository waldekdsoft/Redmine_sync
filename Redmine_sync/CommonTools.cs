using Redmine.Net.Api.Types;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Redmine.Net.Api.Async;
using Redmine.Net.Api.Exceptions;

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

        public static async Task<List<Issue>> GetIssuesFromRedmineAsync(int project_id)
        {
            NameValueCollection parameters = new NameValueCollection { { "project_id", project_id.ToString() } };

            List<Issue> ret = await RMManegerService.RMManager.GetObjectsAsync<Issue>(parameters);
            return ret;
        }

        public static List<Issue> GetIssuesFromRedmine(int project_id, GUI.IOutputable output)
        {
            List<Issue> ret = null;
            //NameValueCollection parameters = new NameValueCollection { { "status_id", "*" } };
            NameValueCollection parameters = new NameValueCollection { { "project_id", project_id.ToString() } };

            int trials = 3;


            while (trials > 0)
            {
                try
                {
                    ret = RMManegerService.RMManager.GetObjects<Issue>(parameters);
                }
                catch (RedmineException ex)
                {
                    trials--;
                    output.WriteLine("Redmine exception occured! - trying again...");
                }

                if (ret != null)
                    break;
            }

            if(trials == 0)
            {
                output.WriteLine("Not able to read due to exceptions!");
            }
            

            return ret;
        }

        public static List<Issue> GetIssuesFromRedmineWD(int project_id)
        {
            NameValueCollection parameters = new NameValueCollection { { "project_id", project_id.ToString() } };

            return RMManegerService.RMManager.GetObjects<Issue>(parameters);
        }


    }
}
