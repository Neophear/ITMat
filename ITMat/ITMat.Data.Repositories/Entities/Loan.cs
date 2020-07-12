using System;

namespace ITMat.Data.Repositories.Entities
{
    internal class Loan : AbstractEntity
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public LoanStatus Status { get; set; } //Created, PickedUp, Returned, Cancelled
        public int EmployeeId { get; set; }
        public int RecipientId { get; set; }
        public string Note { get; set; }
    }
}