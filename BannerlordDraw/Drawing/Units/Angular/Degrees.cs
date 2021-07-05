using System;

namespace BannerlordDraw.Drawing.Units.Angular
{
    public class Degrees
    {

        public double Value { get; }
        
        public Degrees(double value)  {
            Value = value;
        }
        public override string ToString()
        {
            return $"{Value}°";
        }

        public Degrees Normalize()
        {
            return new Degrees(value: (360 + Value) % 360);
        }

        public bool IsNormal()
        {
            return Value >= 0 && Value <= 360;
        }
        
        protected bool Equals(Degrees other)
        {
            return Value.Equals(obj: other.Value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(objA: null, objB: obj)) return false;
            if (ReferenceEquals(objA: this, objB: obj)) return true;
            return obj.GetType() == GetType() && Equals(other: (Degrees) obj);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static explicit operator Radians(Degrees d) => new Radians(value: Math.PI / 180 * d.Value);
        public static implicit operator Degrees(double f) => new Degrees(value: f);
        public static implicit operator double(Degrees d) => d.Value;
        public static implicit operator float(Degrees d) => (float)d.Value;
        public static Degrees operator *(Degrees d, double d2)  => new Degrees(value: d.Value * d2);
        public static bool operator ==(Degrees d1, Degrees d2)  => d1.Value == d2.Value;
        public static bool operator !=(Degrees d1, Degrees d2)  => d1.Value != d2.Value;
    }
}
