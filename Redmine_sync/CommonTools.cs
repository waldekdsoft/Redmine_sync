using Redmine.Net.Api.Types;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Redmine.Net.Api.Async;

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

        public static List<Issue> GetIssuesFromRedmine(int project_id)
        {
            //NameValueCollection parameters = new NameValueCollection { { "status_id", "*" } };
            NameValueCollection parameters = new NameValueCollection { { "project_id", project_id.ToString() } };

            return RMManegerService.RMManager.GetObjects<Issue>(parameters);
        }

        public static List<Issue> GetIssuesFromRedmineWD(int project_id)
        {
            NameValueCollection parameters = new NameValueCollection { { "project_id", project_id.ToString() } };

            return RMManegerService.RMManager.GetObjects<Issue>(parameters);
        }


    }
}
