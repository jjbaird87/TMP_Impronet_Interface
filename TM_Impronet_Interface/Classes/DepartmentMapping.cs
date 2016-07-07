using System;
using System.Collections.Generic;

namespace TM_Impronet_Interface.Classes
{
    [Serializable]
    public class DepartmentMapping
    {
        public Department Department { get; set; }
        public List<Terminal> Terminals { get; set; }
    }
}
