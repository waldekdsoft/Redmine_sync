using Redmine_sync.Team;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return GetItemList(null);
        }

        public List<TMSItem> GetItemList(Func<TMSItem, bool> wherePredicate = null)
        {
            return _dict.Values.Where(wherePredicate ?? (s => true)).ToList();
        }

    }
}
