using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redmine_sync
{
    public class TMSTaskSynchronizer
    {
        private string client = string.Empty;

        public TMSTaskSynchronizer(string client)
        {
            this.client = client;
        }


        public void Synchronize()
        {
            Console.WriteLine("Synchronization for {0} started...", this.client);


            Console.WriteLine("Synchronization for {0} finished...", this.client);
        }
    }
}
