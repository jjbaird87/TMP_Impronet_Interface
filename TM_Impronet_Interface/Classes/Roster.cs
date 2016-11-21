using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TM_Impronet_Interface.Classes
{
    public class Roster
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public List<RosterDate> RosterDates { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
