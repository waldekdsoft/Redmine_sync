using Redmine.Net.Api.Types;
using Redmine_sync.GUI;
using Redmine_sync.TMS;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redmine_sync
{
    using TMS_TP = Tuple<TMSItem, TMSItem>;
    
    
    public static class ExtensionMethods
    {
        //public static bool TryGetValueWithMarkingUsedValue(this Dictionary<string, TMSItem> dict, string key, out TMSItem value)
        //{
        //    bool ret = dict.TryGetValue(key, out value);
        //    if (ret)
        //    {
        //        value.Used = true;
        //    }
        //    return ret;
        //}

        //public static Dictionary<string, TMSItem> GetAllNotUsedItems(this Dictionary<string, TMSItem> dict)
        //{
        //    return dict.Where(i => !i.Value.Used).ToDictionary(i => i.Key, i => i.Value);
        //}

        public static Dictionary<string, List<TMSItem>> GetDuplicates(this List<TMSItem> list)
        {
            Dictionary<string, List<TMSItem>> ret = new Dictionary<string, List<TMSItem>>();
            foreach (TMSItem tmsItem in list)
            {
                if (ret.ContainsKey(tmsItem.TMS))
                {
                    ret[tmsItem.TMS].Add(tmsItem);
                }
                else
                {
                    List<TMSItem> l = new List<TMSItem>();
                    l.Add(tmsItem);
                    ret.Add(tmsItem.TMS, l);
                }
            }
            //numerable.GroupBy(x => x.Key).All(g => g.Count() == 1);
            return ret.Where(x => x.Value.Count() > 1).ToDictionary(x => x.Key, x => x.Value);
        }


        public static void UpdateDictionary(this Dictionary<string, List<TMS_TP>> dict, string key, TMS_TP newValue, IOutputable ouotput = null)
        {
            List<TMS_TP> existingList;
            if (!dict.TryGetValue(key, out existingList))
            {
                existingList = new List<TMS_TP>();
                dict.Add(key, existingList);
            }
            existingList.Add(newValue);

            if (Consts.VERBOSE  && ouotput != null)
            {
                ouotput.WriteLine("{0}: {1}", key, newValue);
            }
        }

        public static string GetTMSUrgencyCustomFieldValue(this Redmine.Net.Api.Types.Issue issue)
        {
            return issue.GetCustomFieldValue("TMS Urgency");
        }

        public static string GetCustomFieldValue(this Redmine.Net.Api.Types.Issue issue, string customFieldName)
        {
            foreach (var customField in issue.CustomFields)
            {
                if (customField.Name == customFieldName)
                {
                    if (customField.Values != null)
                    {
                        foreach (CustomFieldValue cfValue in customField.Values) 
                        {
                            if (cfValue != null && cfValue.Info != null)
                            {
                                return cfValue.Info;
                            }
                        }
                    }
                }
            }
            return null;

        }
        public static void SerializeTMSItemData(this List<TMSItem> list)
        {
            System.IO.File.Delete("tms_db_items.xml");
            using (FileStream fs = new FileStream("tms_db_items.xml", FileMode.OpenOrCreate))
            {
                System.Xml.Serialization.XmlSerializer s = new System.Xml.Serialization.XmlSerializer(typeof(List<TMSItem>));
                s.Serialize(fs, list);
            }
        }

        public static List<TMSItem> DeserializeTMSItemData(this List<TMSItem> list)
        {
            List<TMSItem> list2 = new List<TMSItem>();
            using (var reader = new StreamReader("tms_db_items.xml"))
            {
                System.Xml.Serialization.XmlSerializer deserializer = new System.Xml.Serialization.XmlSerializer(typeof(List<TMSItem>),
                    new System.Xml.Serialization.XmlRootAttribute("ArrayOfTMSItem"));
                list2 = (List<TMSItem>)deserializer.Deserialize(reader);
            }

            return list2;
        }


        public static void StartStopwatchAndPrintMessage(this Stopwatch sw, string message, IOutputable output)
        {
            output.Write(message);
            sw.Restart();
        }

        public static void StopStopwatchAndPrintDoneMessageWithElapsedTime(this Stopwatch sw, IOutputable output)
        {
            sw.Stop();
            output.WriteLine("done! ({0}s)", sw.Elapsed.TotalSeconds);
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
