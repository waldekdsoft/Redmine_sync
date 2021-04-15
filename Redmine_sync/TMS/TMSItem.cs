using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redmine_sync.TMS
{
    class TMSItem
    {
        public string TMS { get; set; }
        public string AssignedTo { get; set; }
        public string  Status { get; set; }
        public string Urgency { get; set; }
        public string LastActText { get; set; }
        public DateTime LastActDate { get; set; }
        public string Desctiption { get; set; }
        public string SDId { get; set; }

        public override string ToString()
        {
            return string.Format("TMS: {0} AssignedTo: {1} Status: {2}", TMS, AssignedTo, Status);
        }
    }

    
}
