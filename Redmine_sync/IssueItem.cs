using StackExchange.Redis;
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

        public HashEntry[] GetRedisHashEntries()
        {
            List<HashEntry> heList = new List<HashEntry>();

            heList.Add(new HashEntry("Id", Id));
            heList.Add(new HashEntry("TMS", TMS));
            heList.Add(new HashEntry("Status", Status));
            heList.Add(new HashEntry("Env", Env));
            heList.Add(new HashEntry("ProblemId", ProblemId));
            heList.Add(new HashEntry("SenderCode", SenderCode));
            heList.Add(new HashEntry("Desc", Desc));
            return heList.ToArray();
        }

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
