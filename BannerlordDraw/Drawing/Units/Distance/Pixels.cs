using System;

namespace BannerlordDraw.Drawing.Units.Distance
{
    public class Pixels
    {
        public double Value { get; }

        public Pixels(double value) {
            Value = value;
        }
        
        public override bool Equals(object obj)
        {
            if (obj is Pixels result) {
                return Math.Abs(value: result.Value - Value) < 0.00001;
            }
            return false;
        }

        public override string ToString()
        {
            return $"{Value}";
        }

        public override int GetHashCode()
        {
            return (Value).GetHashCode();
        }
        public static bool operator ==(Pixels lhs, Pixels rhs)
        {
            if (!(lhs is null)) return lhs.Equals(obj: rhs);
            return rhs is null;
        }
        public static bool operator !=(Pixels lhs, Pixels rhs)
        {
            if (!(lhs is null)) return lhs.Equals(obj: rhs);
            return rhs is null;
        }
        
        public static implicit operator int(Pixels p) => (int)Math.Round(a: p.Value);
        public static implicit operator double(Pixels p) => p.Value;
        public static implicit operator float(Pixels p) => (float)p.Value;
        public static implicit operator Pixels(float p) => new Pixels(value: p);
        public static implicit operator Pixels(double p) => new Pixels(value: p);
        public static implicit operator Pixels(int p) => new Pixels(value: p);
        public static Pixels operator +(Pixels lhs, Pixels rhs) => new Pixels(value: lhs.Value + rhs.Value);
        public static Pixels operator -(Pixels lhs, Pixels rhs) => new Pixels(value: lhs.Value - rhs.Value);
        public static Pixels operator /(Pixels lhs, Pixels rhs) => new Pixels(value: lhs.Value / rhs.Value);
        public static Pixels operator *(Pixels lhs, Pixels rhs) => new Pixels(value: lhs.Value * rhs.Value);
    }
}
