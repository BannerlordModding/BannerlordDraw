using System;

namespace BannerlordDraw.Drawing.Units.Angular
{
    public class Radians
    {
        public double Value { get; set; }
        
        public Radians(double value) {
            Value = value;
        }
        public override string ToString()
        {
            return $"{Value} Rad";
        }

        public static explicit operator Degrees(Radians r) => new Degrees(value: 180 / Math.PI * r.Value);
        public static explicit operator Radians(float f) => new Radians(value: f);
        public static explicit operator Radians(double f) => new Radians(value: f);
        public static implicit operator float(Radians r) => (float)r.Value;
        public static implicit operator double(Radians r) => r.Value;
        public static Radians operator *(Radians r, double d) => new Radians(value: r.Value * d);

    }
}
