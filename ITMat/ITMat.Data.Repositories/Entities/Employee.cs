namespace ITMat.Data.Repositories.Entities
{
    internal class Employee : AbstractEntity
    {
        public string MANR { get; set; }
        public string Name { get; set; }
        public bool Blacklisted { get; set; }
    }
}