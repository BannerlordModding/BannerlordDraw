using BannerlordDraw.Drawing.Units.Distance;
using BannerlordDraw.Drawing.Units.Angular;

namespace BannerlordDraw.Drawing
{
    public class Offset
    {

        public Offset(Pixels xOffset, Pixels yOffset)
        {
            X = xOffset;
            Y = yOffset;
        }
        public Pixels X { get; }
        public Pixels Y { get; }
        public static Offset operator +(Offset lhs, Offset rhs) => new Offset(xOffset: lhs.X + rhs.X, yOffset: lhs.Y + rhs.Y);
    }
}
