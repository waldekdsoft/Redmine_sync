using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redmine_sync.Cybersecurity
{
    class CyberActionsManager
    {
        private static string FILE_NAME = @"C:\Users\waldekd\Documents\Cybersecurity\cyber.xlsx";
        private static string TAB_NAME = "Gene EndPoints";

        public static string INSERT_DEV_CYBERSECURITY_RECORD = "insert into RM2XSLTABLE(rm_iss_num, rm_prj_name, rm_tracker, rm_status, rm_priority, rm_subject, rm_assignee, rm_updated, rm_tms_urgency, tms_task, update_dt) values(:rm_iss_num, :rm_prj_name, :rm_tracker, :rm_status, :rm_priority, :rm_subject, :rm_assignee, :rm_updated, :rm_tms_urgency, :tms_task, sysdate)";
        /*
         *        ENDPOINT  VARCHAR2(100),
       MODULE   VARCHAR2(100),
       ARCHID VARCHAR2(30),
       COLUMNE VARCHAR2(10),
       COLUMNF VARCHAR2(10),       
       REFERENCETODESIGN VARCHAR2(150),
       ARCHCOMMENT VARCHAR2(200),
       DEVCOMMENT VARCHAR2(200),
       UPDATE_DT DATE 
         */
        public void LoadData()
        {
            var xlsx = new LinqToExcel.ExcelQueryFactory(FILE_NAME);
            var query =
                from row in xlsx.Worksheet(TAB_NAME)
                let item = new
                {
                    Service = row["Service"].Cast<string>(),
                    Endpoint = row["Endpoint"].Cast<string>(),
                    Module = row["Module"].Cast<string>(),
                    ArchID = row["Arch ID"].Cast<string>(),
                    ColumnE = row["2 - Generic Data Modification Services / Injections"].Cast<string>(),
                    ColumnF = row["3 - Services which allow Denial of Service attacks"].Cast<string>(),
                    ReferenceToDesign = row["Reference to Design Doc_For Threat 2 and 3 "].Cast<string>(),
                    Comment = row["Design/Problem Comment"].Cast<string>(),
                }
                select item;


            foreach (var itemFromExcel in query)
            { 

            }
        }

        /*
         Service Endpoint	Module	Arch ID	2 - Generic Data Modification Services / Injections	3 - Services which allow Denial of Service attacks	"Reference to Design Doc
For Threat 2 and 3 "	Design/Problem Comment
         */
    }
}
