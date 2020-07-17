using System;
using System.Collections.Generic;

namespace ITMat.Core.DTO
{
    public class LoanDTO : AbstractDTO
    {
        public int EmployeeId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int RecipientId { get; set; }
        public string Note { get; set; }
        public bool Active { get; set; }
        public IEnumerable<LoanLineItemDTO> ItemLines { get; set; }
        public IEnumerable<LoanLineGenericItemDTO> GenericItemLines { get; set; }
    }
}