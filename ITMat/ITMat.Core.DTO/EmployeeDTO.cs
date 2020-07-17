namespace ITMat.Core.DTO
{
    public class EmployeeDTO : AbstractDTO
    {
        public string MANR { get; set; }
        public string Name { get; set; }
        public EmployeeStatusDTO Status { get; set; }
    }
}