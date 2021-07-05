using System;

namespace BannerlordDraw.Drawing.Exceptions
{
    public class BadGeometryException : DrawingException
    {
        public BadGeometryException()
        {
        }

        public BadGeometryException(string message) : base(message: message)
        {
        }

        public BadGeometryException(string message, Exception inner) : base(message: message, inner: inner)
        {
        }
    }
}
