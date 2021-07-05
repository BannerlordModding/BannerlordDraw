using System;
using BannerlordDraw.Drawing.Units.Distance;

namespace BannerlordDraw.Drawing.Interfaces
{
    public interface IMoveable
    {
        void Project(Angle angle, Distance distance);
        void Translate(Offset offset);
        
    }
}
