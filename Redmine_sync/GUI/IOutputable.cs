using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redmine_sync.GUI
{
    public interface IOutputable
    {
        void Write(string s, params object[] args);
        void WriteLine(string s, params object[] args);

        void WriteToGrid(DataTable dt);

        void FlushWriteLines();

        void WriteLineToBuffer(string s, params object[] args);

        bool GetIsRedisUse();
        //void WriteToGrid(DataSource ds);  
    }
}
