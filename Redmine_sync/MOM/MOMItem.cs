using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redmine_sync.MOM
{
    class MOMItem
    {
        public string ProblemID { get; set; }
        public string ProblemCode { get; set; }
        public string MessageId { get; set; }
        public string EventCode { get; set; }
        public string Details { get; set; }
        public string SenderCode { get; set; }

        public string ProblemLink { get; set; }
        public string MessageLink { get; set; }
    }
}
