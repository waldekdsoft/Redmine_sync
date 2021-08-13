using Microsoft.Office.Interop.Outlook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Redmine_sync.Email
{
    class EmailContentProvider
    {
        public void DoWork()
        {/*
            Microsoft.Office.Interop.Outlook.Application app = GetApplicationObject();

            Microsoft.Office.Interop.Outlook.Folder inbox = app.ActiveExplorer().Session.GetDefaultFolder(Microsoft.Office.Interop.Outlook.OlDefaultFolders.olFolderInbox);
            Microsoft.Office.Interop.Outlook.Items items = inbox.Items;
            Microsoft.Office.Interop.Outlook.MailItem mailItem = null;
            object folderItem;
            string subjectName = string.Empty;
            string filter = "[Subject] > 's' And [Subject] <'u'";
            folderItem = items.Find(filter);
            while (folderItem != null)
            {
                mailItem = folderItem as Microsoft.Office.Interop.Outlook.MailItem;
                if (mailItem != null)
                {
                    subjectName += "\n" + mailItem.Subject;
                }
                folderItem = items.FindNext();
            }
            subjectName = " The following e-mail messages were found: " +
                subjectName;
            MessageBox.Show(subjectName);
            */
        }
        /*
        Outlook.Application GetApplicationObject()
        {

            Outlook.Application application = null;

            // Check whether there is an Outlook process running.
            if (Process.GetProcessesByName("OUTLOOK").Count() > 0)
            {

                // If so, use the GetActiveObject method to obtain the process and cast it to an Application object.
                application = Marshal.GetActiveObject("Outlook.Application") as Outlook.Application;
            }
            else
            {

                // If not, create a new instance of Outlook and sign in to the default profile.
                application = new Outlook.Application();
                Outlook.NameSpace nameSpace = application.GetNamespace("MAPI");
                nameSpace.Logon("", "", Missing.Value, Missing.Value);
                nameSpace = null;
            }

            // Return the Outlook Application object.
            return application;
        }*/
    }
}