﻿using System;

namespace ITMat.Core.DTO
{
    public class LoanListedDTO : AbstractDTO
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public bool Active { get; set; }
    }
}