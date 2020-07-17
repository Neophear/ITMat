namespace ITMat.Core.DTO
{
    public class ItemDTO : AbstractDTO
    {
        public string Identifier { get; set; }
        public string Model { get; set; }
        public ItemTypeDTO Type { get; set; }
        public bool Discarded { get; set; }
    }
}