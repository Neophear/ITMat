namespace ITMat.Core.Models
{
    public class EmployeeStatus : AbstractModel
    {
        public string Name { get; set; }
        public bool CanLend { get; set; }
    }
}