using System;

namespace ITMat.Core.Models
{
    public class LoanLineItem : AbstractModel
    {
        public Item Item { get; set; }
        public DateTime PickedUp { get; set; }
        public DateTime Returned { get; set; }
    }
}