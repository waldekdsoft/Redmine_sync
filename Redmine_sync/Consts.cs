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

    }
}
