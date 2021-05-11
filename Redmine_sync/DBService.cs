using Oracle.ManagedDataAccess.Client;
using Redmine.Net.Api.Types;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Threading.Tasks;

namespace Redmine_sync
{
    class DBService
    {
        private static string CONN_STRING;

        public static string GET_ALL_TMS_TASKS = "SELECT * FROM MACBI_TMS_ADDITIONAL_INFO";
        public static string GET_ALL_DEV1_USERS = "SELECT * FROM DEV1_MEMBERS_VIEW";

        public static string DELETE_RM2XSLTABLE_CONTENT_FROM_TODAY = "delete from RM2XSLTABLE where TO_DATE(update_dt, 'yyyy/mm/dd') = TO_DATE(sysdate, 'yyyy/mm/dd')";
        public static string INSERT_RM2XSLTABLE_RECORD = "insert into RM2XSLTABLE(rm_iss_num, rm_prj_name, rm_tracker, rm_status, rm_priority, rm_subject, rm_assignee, rm_updated, tms_task, update_dt) values(:rm_iss_num, :rm_prj_name, :rm_tracker, :rm_status, :rm_priority, :rm_subject, :rm_assignee, :rm_updated, :tms_task, sysdate)";
        static DBService()
        {
            CONN_STRING = string.Format("User ID={0}; Password={1}; Data Source={2};", Properties.Resources.User, Properties.Resources.Hidden, Properties.Resources.dbstring);
        }



        /*
         
          var commandText = "insert into Emp_table (SL_NO,empane,empid,salaray) values(:SL_NO,:empane,:empid,:salary)";

using (OracleConnection connection = new OracleConnection(connectionString))
using (OracleCommand command = new OracleCommand(commandText, connection))
{
    command.Parameters.AddWithValue("SL_NO", 1);
    command.Parameters.AddWithValue("empane", "sree");
    command.Parameters.AddWithValue("empid", 1002);
    command.Parameters.AddWithValue("salaray", 20000);
    command.Connection.Open();
    command.ExecuteNonQuery();
    command.Connection.Close();
}
         */

        public static void InsertRMIssuesToRM2XLSTable(List<Issue> issues)
        {
            if (issues.Count > 0)
            {
                using (OracleConnection connection = new OracleConnection(CONN_STRING))
                {
                    connection.Open();
                    using (OracleCommand command = new OracleCommand(DELETE_RM2XSLTABLE_CONTENT_FROM_TODAY, connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    using (OracleCommand command = new OracleCommand(INSERT_RM2XSLTABLE_RECORD, connection))
                    {
                        foreach (Issue issueFromRm in issues)
                        {
                            command.Parameters.Clear();
                            command.Parameters.Add("rm_iss_num", issueFromRm.Id);
                            command.Parameters.Add("rm_prj_name", issueFromRm.Project.Name);
                            command.Parameters.Add("rm_tracker", issueFromRm.Tracker.Name);
                            command.Parameters.Add("rm_status", issueFromRm.Status.Name);
                            command.Parameters.Add("rm_priority", issueFromRm.Priority.Name);
                            command.Parameters.Add("rm_subject", issueFromRm.Subject);
                            command.Parameters.Add("rm_assignee", issueFromRm.AssignedTo.Name);
                            command.Parameters.Add("rm_updated", issueFromRm.UpdatedOn.Value);
                            string subject = issueFromRm.Subject;
                            string tms = string.Empty;
                            if (subject.StartsWith("MACBI-"))
                            {
                                tms = subject.Substring(0, 11);
                            }
                            command.Parameters.Add("tms_task", tms);

                            command.ExecuteNonQuery();
                        }
                    }
                    connection.Clone();
                }
            }
        }

        public static DataTable ExecuteQuery(string query)
        {
            DataTable dt = new DataTable();
            using (OracleConnection oc = new OracleConnection())
            {
                oc.ConnectionString = CONN_STRING;
                oc.Open();

//                string sql = "SELECT * FROM MACBI_TMS_ADDITIONAL_INFO";

                OracleDataAdapter oda = new OracleDataAdapter(query, oc);
                
                oda.Fill(dt);
                oc.Close();
            }

            return dt;
        }

        public static async Task<DataTable> ExecuteQueryAsync(string query)
        {
            DataTable ret = null;
            await Task.Run(() => ret = ExecuteQuery(query));
            return ret;
        }
    }
}
