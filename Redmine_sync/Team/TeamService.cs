using Redmine_sync.GUI;
using Redmine_sync.Users;
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
        public static List<User> Users { get; set; } = new List<User>();

        public static List<string> UsersTMSLogin { get; set; } = new List<string>();
        public static List<string> UsersSDLogin { get; set; } = new List<string>();
        public static List<string> UsersFullName { get; set; } = new List<string>();

        static TeamService()
        {
            DataSet ds = new DataSet();
            try
            {
                ds.ReadXml(Consts.FILE_NAMES.USERS_CACHE, XmlReadMode.InferSchema);
            }
            catch (Exception)
            {
                CacheTeamData();
                ds.ReadXml(Consts.FILE_NAMES.USERS_CACHE, XmlReadMode.InferSchema);
            }

            DataTable usersDataTable = ds.Tables[0];
            foreach (DataRow userRow in usersDataTable.Rows)
            {
                User u = new User();
                u.TMS_LOGIN = Convert.ToString(userRow["GUS_USER_ID_EXT2"]);
                u.SD_LOGIN = Convert.ToString(userRow["GUS_USER_ID"]);
                u.FULL_NAME = string.Format("{0} {1}", Convert.ToString(userRow["GUS_USER_FIRSTNAME"]), Convert.ToString(userRow["GUS_USER_LASTNAME"]));

                Users.Add(u);

                UsersTMSLogin.Add(u.TMS_LOGIN);
                UsersSDLogin.Add(u.SD_LOGIN);
                UsersFullName.Add(u.FULL_NAME);
            }
        }

        public static bool CheckIfSamePersonByTMSLoginAndFullName(string tmsLogin, string fullName)
        {
            return Users.Where(u => u.TMS_LOGIN == tmsLogin && u.FULL_NAME == fullName).Count() == 1;
        }

        public static bool IsPersonFromMyTeam(string name)
        {
            return UsersTMSLogin.Contains(name) || UsersSDLogin.Contains(name) || UsersFullName.Contains(name);
        }

        public static bool IsPersonFromMyTeam(string tmsLogin, string fullName, string sdLogin)
        {
            bool foundByTMSLogin = !string.IsNullOrEmpty(tmsLogin) && UsersTMSLogin.Contains(tmsLogin);
            bool foundBySDLogin = !string.IsNullOrEmpty(sdLogin) && UsersSDLogin.Contains(sdLogin);
            bool foundByFullName = !string.IsNullOrEmpty(fullName) && UsersFullName.Contains(fullName);

            return foundByTMSLogin || foundBySDLogin || foundByFullName;
        }


        public static void CacheTeamData(IOutputable output)
        {
            //get all TMS tasks from SD database
            sw.StartStopwatchAndPrintMessage("Caching of team data started...", output);
            CacheTeamData();
            sw.StopStopwatchAndPrintDoneMessageWithElapsedTime(output);
        }

        public static void CacheTeamData()
        {
            DataTable usersDataTable = new DataTable();
            usersDataTable = DBService.ExecuteQuery(DBService.GET_ALL_DEV1_USERS);
            usersDataTable.TableName = "USER_DATA";
            usersDataTable.WriteXml(Consts.FILE_NAMES.USERS_CACHE);
        }



    }
}
