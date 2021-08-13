using Redmine.Net.Api;
using Redmine.Net.Api.Types;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Windows.Forms;

namespace Redmine_sync
{
    class Program
    {
        //private static string PROJECT = "macbi-problems";
        public static void WriteLn(string text, params object[] args)
        {
            string msg = string.Format(text, args);
            Console.WriteLine(msg);
        }


        [STAThread]
        static void Main(string[] args)
        {
            //NameValueCollection parameters = new NameValueCollection { { "status_id", "*" } };
            //var parameters = new NameValueCollection { { "status_id", "*" } };
            foreach (var issue in RMManegerService.RMManager.GetObjects<Project>())
            {
                Console.WriteLine("#{0}: {1}", issue.Id, issue.Name);
            }


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new GUI.MainForm());
        }

    }
}
