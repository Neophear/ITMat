using System;

namespace ITMat.Core.Models.Exceptions
{
    public class OverlapException : Exception
    {
        public OverlapException()
            : base("The loan overlaps with another loan that shares one or more items.") { }

        public OverlapException(string message)
            : base(message) { }

        public OverlapException(string message, Exception inner)
            : base(message, inner) { }
    }
}