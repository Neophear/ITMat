namespace ITMat.Core.Models
{
    public class Item : AbstractModel
    {
        public string Identifier { get; set; }
        public string Model { get; set; }
        public ItemType Type { get; set; }
        public bool Discarded { get; set; }
    }
}