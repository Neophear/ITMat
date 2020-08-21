using System;

namespace ITMat.Core.DTO
{
    public class LoanLineGenericItemDTO : AbstractDTO
    {
        public GenericItemDTO GenericItem { get; set; }
        public DateTime? PickedUp { get; set; }
        public DateTime? Returned { get; set; }
    }
}