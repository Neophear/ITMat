namespace ITMat.Core.DTO
{
    public class EmployeeStatusDTO : AbstractDTO
    {
        public string Name { get; set; }
        public bool CanLend { get; set; }
    }
}