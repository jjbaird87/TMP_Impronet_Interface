namespace TM_Impronet_Interface.Classes
{
    public class Department
    {
        public int Id { get; set; }

        public string SiteSla { get; set; }

        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
