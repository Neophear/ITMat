using System;
using System.Collections.Generic;

namespace ITMat.Core.DTO
{
    public class CreateLoanDTO
    {
        public int EmployeeId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int? RecipientId { get; set; }
        public string Note { get; set; }
        public IEnumerable<int> ItemIds { get; set; }
        public IEnumerable<int> GenericItemIds { get; set; }
    }
}