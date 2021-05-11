using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redmine_sync
{
    static class  Consts
    {
        public static string RM_NEW = "New";
        public static string RM_CLOSED = "Closed";
        public static string RM_INVESTIGATED = "Investigated";
        public static string RM_INPROGRESS = "In Progress";
        public static string RM_ONHOLE = "On Hold";
        public static string RM_REASSIGNED = "Reassigned";
        public static string RM_FEEDBACK = "Feedback";

        //reasons for checking
        public static string RFC_NOT_EXISTS_IN_TMS = "RFC_NOT_EXISTS_IN_TMS";
        public static string RFC_NOT_EXISTS_IN_RM = "RFC_NOT_EXISTS_IN_RM";
        public static string RFC_DIFFERENT_STATUSES = "RFC_DIFFERENT_STATUSES";
        public static string RFC_ASSIGNED_TO_DIFFERENT_PERSON_IN_RM_AND_TMS = "RFC_ASSIGNED_TO_DIFFERENT_PERSON_IN_RM_AND_TMS";
        public static string RFC_NOT_CONNECTED_WITH_TMS = "RFC_NOT_CONNECTED_WITH_TMS";
        public static string RFC_BOTH_CLOSED = "RFC_BOTH_CLOSED";
        public static string RFC_BOTH_OK = "RFC_BOTH_OK";

        public static bool TEST_MODE = false;
        public static bool VERBOSE = false;

        public static string SRC_RM = "Redmine";
        public static string SRC_DB = "DB";

        public static string NA = "N/A";
        public static string EOL = @"\r\n";
        public static string EOL_SPECIAL = "\r\n";

        public static class FILE_NAMES
        {
            public static string DB_TMS_CACHE = "db_tms_data.xml";
            public static string RM_TMS_CACHE = "rm_tms_data.xml";
            public static string USERS_CACHE = "user_data.xml";
        }

        public static class PROJECT_NAMES
        {
            public static class TMS
            {
                public static class MACBI
                {
                    public static int TEMPORARY_PROBLEMS = 74;
                    public static int PROBLEMS = 15;
                }
            }

            public static class MOM
            {
                public static int PROBLEMS = 65;
                public static int TEMPORARY_PROBLEMS = 67;
            }
        }
        //"macbi-problems" 15
        //"temporary-macbi-problems" 74
        //"mom-problems" 65
        //"temporary-mom-problems" (wdtest) 67


    }
}
