using System;
using System.ComponentModel;
using System.Diagnostics;
using BannerlordDraw.Drawing.Units.Angular;
using BannerlordDraw.Drawing.Units.Distance;
using TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.EncyclopediaList;

namespace BannerlordDraw.Drawing
{
    public enum EOrigin
    {
        TopLeft,
        Center
    }
    public class Position
    {
        public EOrigin Origin { get; }
        
        public Offset Offset { get; }
        
        public Pixels X => Offset.X;
        public Pixels Y => Offset.Y;

        public float GridWidth { get; }

        public float GridHeight { get; }

        public Position(Offset offset, EOrigin origin)
        {
            Offset = offset;
            Origin = origin;
            GridWidth = Screen.Width;
            GridHeight = Screen.Height;
            
            //IF in_pix_coord.origin = 'center' THEN
            //    v_dest_origin = 'top-left';
            //RETURN (in_pix_coord.x + in_img_width / 2, -in_pix_coord.y + (in_img_height / 2), v_dest_origin);
            //END IF;
            //IF in_pix_coord.origin = 'top-left' THEN
            //    v_dest_origin = 'center';
            //RETURN (in_pix_coord.x - in_img_width / 2, -in_pix_coord.y + (in_img_height / 2), v_dest_origin);
            //END IF;
        }
        public Position Project(Angle angle, Distance distance)
        {
            Radians angleInRadians = (Radians) angle.Normalize();
            Pixels distanceInPixels = (Pixels) distance;
            Pixels newX = X + distanceInPixels * Math.Cos(d: angleInRadians);
            Pixels newY = Y + distanceInPixels * Math.Sin(a: angleInRadians);
            return new Position(offset: new Offset(xOffset: newX, yOffset: newY), origin: Origin);
        }
        public Position ToOrigin(EOrigin newOrigin, float gridWidth, float gridHeight)
        {
            if (Origin == newOrigin) return this;
            switch (newOrigin)
            {
                case EOrigin.Center:
                    return new Position(offset: new Offset(xOffset: Offset.X + gridWidth / 2, yOffset: -Offset.Y + gridHeight / 2), origin: EOrigin.Center);
                case EOrigin.TopLeft:
                    return new Position(offset: new Offset(xOffset: Offset.X - gridWidth / 2, yOffset: -Offset.Y + gridHeight / 2), origin: EOrigin.Center);
                default:
                    throw new InvalidEnumArgumentException(message: $"Invalid EOrigin {Origin} for Position.AsCenterOrigin, no conversion logic available.");
            }
        }
        public override string ToString()
        {
            return $"{X},{Y}";
        }
        public Position Translate(Offset offset)
        {
            return new Position(offset, Origin);
        }
        public override int GetHashCode()
        {
            return (X, Y).GetHashCode();
        }
        
        public bool Equals(Position obj)
        {
            return obj.X == X && obj.Y == Y;
        }

        public override bool Equals(object obj)
        {
            return obj is Position position && Equals(obj: position);
        }
        public static bool operator ==(Position lhs, Position rhs)
        {
            return lhs?.Equals(obj: rhs) ?? rhs is null;
        }
        public static bool operator !=(Position lhs, Position rhs)
        {
            if (!ReferenceEquals(objA: lhs, objB: null)) return lhs.Equals(obj: rhs);
            return ReferenceEquals(objA: rhs, objB: null);
        }
    }
}