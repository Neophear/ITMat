using System;

namespace ITMat.Data.DTO
{
    public class LoanDTO : AbstractDTO
    {
        public int EmployeeId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public LoanStatusDTO Status { get; set; } //Created, PickedUp, Returned, Cancelled
        public int RecipientId { get; set; }
        public string Note { get; set; }
    }
}