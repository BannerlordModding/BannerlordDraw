using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BannerlordDraw.Drawing.Units.Distance;

namespace BannerlordDraw.Drawing
{
    public class Distance
    {
        private readonly Pixels _p;
        public Distance(Pixels p)
        {
            _p = p;
        }
        public static explicit operator Pixels(Distance d) => d._p;
        public static implicit operator Distance(Pixels p) => new Distance(p: p);
    }
}
