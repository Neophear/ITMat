using System;
using System.Collections.Generic;

namespace ITMat.Core.Models
{
    public class Loan : AbstractModel
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int EmployeeId { get; set; }
        public int? RecipientId { get; set; }
        public string Note { get; set; }
        public bool Active { get; set; }
        public IEnumerable<LoanLineItem> ItemLines { get; set; }
        public IEnumerable<LoanLineGenericItem> GenericItemLines { get; set; }
    }
}