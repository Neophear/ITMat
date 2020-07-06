using System;

namespace ITMat.Data.DTO
{
    public class LoanLineDTO : AbstractDTO
    {
        public DateTime PickedUp { get; set; }
        public DateTime Returned { get; set; }
        public ItemDTO Item { get; set; }
    }
}