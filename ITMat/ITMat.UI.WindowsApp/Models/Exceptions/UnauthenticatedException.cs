using System;

namespace ITMat.UI.WindowsApp.Models.Exceptions
{
    public class UnauthenticatedException : Exception
    {
        public UnauthenticatedException()
            : base("Ikke logget ind.") { }

        public UnauthenticatedException(string message)
            : base(message) { }

        public UnauthenticatedException(string message, Exception inner)
            : base(message, inner) { }
    }
}