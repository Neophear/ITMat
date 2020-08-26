using System;

namespace ITMat.Core.Models.Exceptions
{
    public class LendingProhibitedException : Exception
    {
        public LendingProhibitedException()
            : base("The Employee's Status prohibits the creation of new Loans.") { }

        public LendingProhibitedException(string message)
            : base(message) { }

        public LendingProhibitedException(string message, Exception inner)
            : base(message, inner) { }
    }
}