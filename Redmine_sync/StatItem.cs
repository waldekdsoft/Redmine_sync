using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redmine_sync
{
    public class StatItem
    {
        public string Env { get; set; }
        public int Added { get; set; } = 0;

        public int Updated { get; set; } = 0;

        public int NotUpdated { get; set; } = 0;

        public int AlreadyExisted { get; set; } = 0;


        public void ShowStatsAdded()
        {
            Console.WriteLine("{0} - Added: {1} Existed: {2}", Env, Added, AlreadyExisted);
        }

        public void ShowStatsUpdated()
        {
            Console.WriteLine("{0} - Updated: {1} Existed: {2}", Env, Updated, NotUpdated);
        }

    }
}
