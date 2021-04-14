using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redmine_sync
{
    public class MOMEnvSettings
    {
        private static string PROBLEM_LINK = "http://{0}/mom/console/problem_search.jsp?messageId={1}&status=ALL";
        private static string MESSAGE_LINK = "http://{0}/mom/console/messages.jsp?attributeDates=DONTSET&msgIds={1}";

        public string Host { private get; set; }

        public MOMEnvSettings(string host)
        {
            Host = host;
        }

        public string GetProblemLink(string msgId)
        {
            return string.Format(PROBLEM_LINK,Host, msgId);
        }

        public string GetMessageLink(string msgId)
        {
            return string.Format(MESSAGE_LINK, Host, msgId);
        }

    }
}
