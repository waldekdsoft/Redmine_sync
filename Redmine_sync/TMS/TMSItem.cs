using Redmine.Net.Api.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redmine_sync.TMS
{
    [Serializable]
    public class TMSItem
    {
        public string Source { get; set; }
        public string TMS { get; set; }
        public string AssignedTo { get; set; }
        public string  Status { get; set; }
        public string Urgency { get; set; }
        public string LastActText { get; set; }
        public DateTime LastActDate { get; set; }
        public string Desctiption { get; set; }
        public string SDId { get; set; }
        public bool Used { get; set; } = false;

        public override string ToString()
        {
            return string.Format("{1} AssignedTo: {2} Status: {3}", Source, TMS, AssignedTo, Status);
        }

        public TMSItem()
        {

        }

        public TMSItem(Issue rmIssue)
        {
            string subject = rmIssue.Subject;

            Source = Consts.SRC_RM;
            AssignedTo = rmIssue.AssignedTo.TryGetName();
            Desctiption = rmIssue.Description;

            string[] subjectSplitted = rmIssue.Subject.Split('-');
            TMS = rmIssue.Subject.Split('-')[0].Trim() + "-" + rmIssue.Subject.Split('-')[1].Trim();
            Status = rmIssue.Status.TryGetName();
        }
    }

    
}
