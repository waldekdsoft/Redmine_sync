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

        public static void CacheTeamData()
        {
            //get all TMS tasks from SD database
            sw.StartStopwatchAndPrintMessage("Caching of team data started...");
            DataTable usersDataTable = new DataTable();
            usersDataTable = DBService.ExecuteQuery(DBService.GET_ALL_DEV1_USERS);
            usersDataTable.TableName = "USER_DATA";
            usersDataTable.WriteXml(Consts.USERS_CACHE_FILE_NAME);
            sw.StopStopwatchAndPrintDoneMessageWithElapsedTime();
        }

        

        public static List<string> GetDEV1TeamMembersTMSLogins()
        {
            if (dev1TeamMembersTMSLogins == null)
            {
                dev1TeamMembersTMSLogins = new List<string>();
                DataSet ds = new DataSet();
                try
                {
                    ds.ReadXml(Consts.USERS_CACHE_FILE_NAME, XmlReadMode.InferSchema);
                }
                catch (Exception)
                {
                    CacheTeamData();
                    ds.ReadXml(Consts.USERS_CACHE_FILE_NAME, XmlReadMode.InferSchema);
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
