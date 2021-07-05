using BannerlordDraw.Drawing.Units.Angular;

namespace BannerlordDraw.Drawing
{
    public class Angle
    {
        private Degrees _degrees;
        private Degrees _normalizedDegrees;
        
        public Degrees Degrees
        {
            get => _degrees;
            private set
            {
                _degrees = value;
                _normalizedDegrees = _degrees.IsNormal() ? _degrees : _degrees.Normalize();
            }
        }
        public Radians Radians => (Radians)_degrees;

        public Angle(Degrees degrees)
        {
            Degrees = degrees;
        }
        public Angle(Radians radians)
        {
            Degrees = (Degrees)radians;
        }

        public Angle FlipVertically()
        {
            return new Angle(degrees: 360 - _normalizedDegrees);
        }
        public Angle FlipHorizontally()
        {
            return new Angle(degrees: _normalizedDegrees - 90);
        }

        public Angle FlipDiagonally()
        {
            return new Angle(degrees: _degrees + 180);
        }

        public Angle Normalize()
        {
            return new Angle(degrees: _normalizedDegrees);
        }
        
        public static explicit operator Degrees(Angle a) => a._degrees;
        public static explicit operator Radians(Angle a) => (Radians)a._degrees;
        public static Angle operator +(Angle a, Degrees d) => new Angle(degrees: a._degrees + d);
        public static Angle operator -(Angle a, Degrees d) => new Angle(degrees: a._degrees - d);
    }
}