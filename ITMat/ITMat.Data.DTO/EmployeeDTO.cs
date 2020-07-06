namespace ITMat.Data.DTO
{
    public class EmployeeDTO : AbstractDTO
    {
        public string MANR { get; set; }
        public string Name { get; set; }
        public bool Blacklisted { get; set; }
    }
}