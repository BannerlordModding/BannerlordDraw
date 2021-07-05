using System;

namespace BannerlordDraw.Drawing.Exceptions
{
    public class DrawingException : Exception
    {
        public DrawingException()
        {
        }

        public DrawingException(string message) : base(message: message)
        {
        }

        public DrawingException(string message, Exception inner) : base(message: message, innerException: inner)
        {
        }
    }
}
