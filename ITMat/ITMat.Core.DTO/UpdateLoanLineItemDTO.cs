using System;

namespace ITMat.Core.DTO
{
    public class UpdateLoanLineItemDTO
    {
        public DateTime? PickedUp { get; set; }
        public DateTime? Returned { get; set; }
    }
}