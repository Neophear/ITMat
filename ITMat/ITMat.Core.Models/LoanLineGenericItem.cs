using System;

namespace ITMat.Core.Models
{
    public class LoanLineGenericItem : AbstractModel
    {
        public int LoanId { get; set; }
        public GenericItem GenericItem { get; set; }
        public DateTime PickedUp { get; set; }
        public DateTime Returned { get; set; }
    }
}