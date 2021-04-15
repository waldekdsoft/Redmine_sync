using Redmine.Net.Api.Types;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redmine_sync
{
    public static class ExtensionMethods
    {
        public static void StartStopwatchAndPrintMessage(this Stopwatch sw, string message)
        {
            Console.Write(message);
            sw.Restart();
        }

        public static void StopStopwatchAndPrintDoneMessageWithElapsedTime(this Stopwatch sw)
        {
            sw.Stop();
            Console.WriteLine("done! ({0}s)", sw.Elapsed.TotalSeconds);
        }


        public static string TryGetName(this IdentifiableName identifiableName)
        {
            if (identifiableName != null && identifiableName.Name != null)
            {
                return identifiableName.Name;
            }
            else
            {
                return null;
            }
        }
    }
}
