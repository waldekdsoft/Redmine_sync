using Redmine.Net.Api.Types;
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
        public static void UpdateDictionary(this Dictionary<string, List<TMS_TP>> dict, string key, TMS_TP newValue)
        {
            List<TMS_TP> existingList;
            if (!dict.TryGetValue(key, out existingList))
            {
                existingList = new List<TMS_TP>();
                dict.Add(key, existingList);
            }
            existingList.Add(newValue);     
        }

        public static void SerializeTMSItemData(this List<TMSItem> list)
        {
            using (FileStream fs = new FileStream("test.xml", FileMode.OpenOrCreate))
            {
                System.Xml.Serialization.XmlSerializer s = new System.Xml.Serialization.XmlSerializer(typeof(List<TMSItem>));
                s.Serialize(fs, list);
            }
        }

        public static List<TMSItem> DeserializeTMSItemData(this List<TMSItem> list)
        {
            List<TMSItem> list2 = new List<TMSItem>();
            using (var reader = new StreamReader("test.xml"))
            {
                System.Xml.Serialization.XmlSerializer deserializer = new System.Xml.Serialization.XmlSerializer(typeof(List<TMSItem>),
                    new System.Xml.Serialization.XmlRootAttribute("ArrayOfTMSItem"));
                list2 = (List<TMSItem>)deserializer.Deserialize(reader);
            }

            return list2;
        }


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
