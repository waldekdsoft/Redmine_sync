using Redmine_sync.GUI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redmine_sync.Team
{
    public  class TeamService
    {
        static Stopwatch sw = new Stopwatch();
        private static List<string> dev1TeamMembersTMSLogins = null;

        public static void CacheTeamData(IOutputable output)
        {
            //get all TMS tasks from SD database
            sw.StartStopwatchAndPrintMessage("Caching of team data started...", output);
            DataTable usersDataTable = new DataTable();
            usersDataTable = DBService.ExecuteQuery(DBService.GET_ALL_DEV1_USERS);
            usersDataTable.TableName = "USER_DATA";
            usersDataTable.WriteXml(Consts.FILE_NAMES.USERS_CACHE);
            sw.StopStopwatchAndPrintDoneMessageWithElapsedTime(output);
        }

        

        public static List<string> GetDEV1TeamMembersTMSLogins(IOutputable output)
        {
            if (dev1TeamMembersTMSLogins == null)
            {
                dev1TeamMembersTMSLogins = new List<string>();
                DataSet ds = new DataSet();
                try
                {
                    ds.ReadXml(Consts.FILE_NAMES.USERS_CACHE, XmlReadMode.InferSchema);
                }
                catch (Exception)
                {
                    CacheTeamData(output);
                    ds.ReadXml(Consts.FILE_NAMES.USERS_CACHE, XmlReadMode.InferSchema);
                }
                DataTable usersDataTable = ds.Tables[0];
                foreach (DataRow userRow in usersDataTable.Rows)
                {
                    string login = Convert.ToString(userRow["GUS_USER_ID_EXT2"]);
                    if (login != Consts.NA)
                    {
                        dev1TeamMembersTMSLogins.Add(login);
                    }
                }
            }
            return dev1TeamMembersTMSLogins;
        }

    }
}
