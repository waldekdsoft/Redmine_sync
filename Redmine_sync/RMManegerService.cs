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
                    _redmineManager = new RedmineManager(Properties.Resources.RMHost, Properties.Resources.ApiKey);
                }

                return _redmineManager;
            }
        }
    }
}
