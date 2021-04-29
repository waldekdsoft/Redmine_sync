using Redmine_sync.Team;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace Redmine_sync.TMS
{
    public class TMSDictionary 
    {
        Dictionary<string, TMSItem> _dict = new Dictionary<string, TMSItem>();
        List<string> team_members = TeamService.GetDEV1TeamMembersTMSLogins();

        public void Add(TMSItem tmsItem)
        {
            Add(tmsItem.TMS, tmsItem);
        }


        public void Add(string tmsNum, TMSItem tmsItem)
        {
            if (!_dict.ContainsKey(tmsNum))
            {
                _dict.Add(tmsNum, tmsItem);
            }
        }

        public TMSItem Get(string tmsNum)
        {
            TMSItem item = null;
            if (_dict.TryGetValue(tmsNum, out item))
            {
                item.Used = true;
            }
            return item;
        }

        public List<TMSItem> GetNotClosedNotUsedAssignedToDEV1ItemList()
        {
            Func<TMSItem, bool> filter = i => !i.Used && !i.Status.StartsWith("C") && !i.Status.StartsWith("c") && team_members.Contains(i.AssignedTo);
            return GetItemList(filter);
        }

        public List<TMSItem> GetItemList(Func<TMSItem, bool> wherePredicate = null)
        {
            return _dict.Values.Where(wherePredicate ?? (s => true)).ToList();
        }

        public void SerializeTMSItemData(string fileName)
        {
            //System.IO.File.Delete("tms_db_items.xml");
            System.IO.File.Delete(fileName);
            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                System.Xml.Serialization.XmlSerializer s = new System.Xml.Serialization.XmlSerializer(typeof(List<TMSItem>));
                s.Serialize(fs, this.GetItemList());
            }
        }

        public static TMSDictionary DeserializeTMSItemData(string fileName)
        {
            TMSDictionary dict = null;
            List<TMSItem> list = null;

            using (var reader = new StreamReader(fileName))
            {
                System.Xml.Serialization.XmlSerializer deserializer = new System.Xml.Serialization.XmlSerializer(typeof(List<TMSItem>),
                    new System.Xml.Serialization.XmlRootAttribute("ArrayOfTMSItem"));
                list = (List<TMSItem>)deserializer.Deserialize(reader);
            }

            if (list != null)
            {
                dict = new TMSDictionary();
                list.ForEach(x => dict.Add(x.TMS, x));
            }

            return dict;
        }

        public Dictionary<string, List<TMSItem>> GetDuplicates()
        {
            Dictionary<string, List<TMSItem>> ret = new Dictionary<string, List<TMSItem>>();
            foreach (TMSItem tmsItem in this.GetItemList())
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

    }
   

}
