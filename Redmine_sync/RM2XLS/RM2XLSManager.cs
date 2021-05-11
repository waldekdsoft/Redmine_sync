using Oracle.ManagedDataAccess.Client;
using Redmine.Net.Api.Types;
using Redmine_sync.GUI;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redmine_sync.RM2XLS
{
    public class RM2XLSManager
    {
        IOutputable output = null;
        Stopwatch sw = new Stopwatch();

        public RM2XLSManager(IOutputable o)
        {
            output = o;
        }

        public void ConvertRedMineIssuesToDB()
        {
            NameValueCollection parameters = new NameValueCollection { { "query_id", "62" } };
            sw.StartStopwatchAndPrintMessage("Getting all issues (w/o MOM problems) from RedMine...", output);
            List<Issue> issues = RMManegerService.RMManager.GetObjects<Issue>(parameters);
            sw.StopStopwatchAndPrintDoneMessageWithElapsedTime(output);

            sw.StartStopwatchAndPrintMessage("Inserting RedMin issues to RM2XLS table...", output);
            DBService.InsertRMIssuesToRM2XLSTable(issues);
            sw.StopStopwatchAndPrintDoneMessageWithElapsedTime(output);

        }
    }
}
