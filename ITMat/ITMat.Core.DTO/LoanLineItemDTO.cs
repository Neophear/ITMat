using System;

namespace ITMat.Core.DTO
{
    public class LoanLineItemDTO : AbstractDTO
    {
        public ItemDTO Item { get; set; }
        public DateTime? PickedUp { get; set; }
        public DateTime? Returned { get; set; }
    }
}