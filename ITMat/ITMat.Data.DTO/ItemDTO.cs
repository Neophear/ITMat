namespace ITMat.Data.DTO
{
    public class ItemDTO : AbstractDTO
    {
        public string UniqueIdentifier { get; set; }
        public bool Discarded { get; set; }
        public string Type { get; set; }
    }
}