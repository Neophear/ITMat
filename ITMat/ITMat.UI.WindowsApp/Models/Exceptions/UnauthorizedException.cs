using System;

namespace ITMat.UI.WindowsApp.Models.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException()
            : base("Ingen adgang.") { }

        public UnauthorizedException(string message)
            : base(message) { }

        public UnauthorizedException(string message, Exception inner)
            : base(message, inner) { }
    }
}