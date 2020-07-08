using System;

namespace ITMat.Data.Repositories.Entities
{
    internal class LoanLine : AbstractEntity
    {
        public int LoanId { get; set; }
        public Item Item { get; set; }
        public DateTime PickedUp { get; set; }
        public DateTime Returned { get; set; }
    }
}