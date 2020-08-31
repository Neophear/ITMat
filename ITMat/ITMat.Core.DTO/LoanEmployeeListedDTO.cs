using System;

namespace ITMat.Core.DTO
{
    public class LoanEmployeeListedDTO : AbstractDTO
    {
        public string MANR { get; set; }
        public string Name { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public bool Active { get; set; }
    }
}