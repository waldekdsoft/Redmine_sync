using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redmine_sync.Tools
{
    static class EnvPropertiesFileReader
    {
        private static string ENV_PROP_FILE = "environments.properties";
        public static Dictionary<string, MOMEnvSettings> Read() 
        {
            var data = new Dictionary<string, MOMEnvSettings>();
            foreach (var row in File.ReadAllLines(ENV_PROP_FILE))
                data.Add(row.Split('=')[0], new MOMEnvSettings(string.Join("=", row.Split('=').Skip(1).ToArray())));

            return data;
        }
    }
}
