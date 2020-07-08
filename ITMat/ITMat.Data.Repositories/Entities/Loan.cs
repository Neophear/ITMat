using System;

namespace ITMat.Data.Repositories.Entities
{
    internal class Loan : AbstractEntity
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public LoanStatus Status { get; set; } //Created, PickedUp, Returned, Cancelled
        public Employee Employee { get; set; }
        public Employee Recipient { get; set; }
        public string Note { get; set; }
    }
}