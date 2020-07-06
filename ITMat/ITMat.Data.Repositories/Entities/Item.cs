namespace ITMat.Data.Repositories.Entities
{
    internal class Item : AbstractEntity
    {
        public ItemType Type { get; set; }
        public bool Discarded { get; set; }
    }
}