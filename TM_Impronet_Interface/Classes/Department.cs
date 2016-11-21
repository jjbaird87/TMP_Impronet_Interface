using System;

namespace TM_Impronet_Interface.Classes
{
    [Serializable]
    public class Department
    {
        public string Id { get; set; }

        public string SiteSla { get; set; }

        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
