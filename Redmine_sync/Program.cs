using Redmine_sync.Team;
using System;
using System.Windows.Forms;

namespace Redmine_sync
{
    class Program
    {
        //private static string PROJECT = "macbi-problems";

        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new GUI.MainForm());

        }

    }
}
