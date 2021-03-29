using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redmine_sync
{
    public class IssueItem
    {
        public int Id { get; set; }
        public string TMS { get; private set; }

        public string Status { get; set; }

        public string Env { get; set; }
        /*MOM problem id*/
        public string ProblemId { get; set; }
        public string SenderCode { get; set; }

        private string desc;
        public string Desc {
            get
            {
                return desc;
            }
            set
            {
                desc = value;
                if (desc.Length >= 11)
                {
                    TMS = desc.Substring(0, 11);
                }
            }
        }
    }
}
