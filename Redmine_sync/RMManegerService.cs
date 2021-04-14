using Redmine.Net.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace Redmine_sync
{
    
    static class RMManegerService
    {
        private static RedmineManager _redmineManager;

        public static RedmineManager RMManager
        {
            get
            {
                if (_redmineManager == null)
                {
                    ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(delegate { return true; });
                    string host = "http://pcredmine:3000";
                    string apiKey = "0533a992d0c093b3b1592e57e10281156ea6afde";
                    _redmineManager = new RedmineManager(host, apiKey);
                }

                return _redmineManager;
            }
        }
    }
}
