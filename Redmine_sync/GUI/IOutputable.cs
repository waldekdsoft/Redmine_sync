using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redmine_sync.GUI
{
    interface IOutputable
    {
        void Write(string s);
        void WriteLine(string s);
    }
}
