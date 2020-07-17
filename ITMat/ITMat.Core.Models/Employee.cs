namespace ITMat.Core.Models
{
    public class Employee : AbstractModel
    {
        public string MANR { get; set; }
        public string Name { get; set; }
        public EmployeeStatus Status { get; set; }
    }
}