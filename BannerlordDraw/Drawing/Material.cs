using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.TwoDimension;

namespace BannerlordDraw.Drawing
{
    public class Material
    {
        private readonly SimpleMaterial _material;
        public Material(TwoDimensionContext twoDimensionContext, Color color, int renderOrder)
        {
            _material = new SimpleMaterial(texture: twoDimensionContext.LoadTexture(name: "snow"), renderOrder: renderOrder, blending: true)
            {
                OverlayEnabled = false,
                CircularMaskingEnabled = false,
                ColorFactor = 1.0f,
                AlphaFactor = 1.0f,
                HueFactor = 1.0f,
                SaturationFactor = 1.0f,
                Color = new TaleWorlds.Library.Color(red: color.R / 255.0f, green: color.G / 255.0f, blue: color.B / 255.0f, alpha: color.A / 255.0f)
            };
            _material.CircularMaskingEnabled = false;
            _material.OverlayEnabled = false;
        }
        
        public bool Equals(Material obj)
        {
            bool result = true;
            result = result && obj._material.Texture == _material.Texture;
            result = result && obj._material.OverlayEnabled == _material.OverlayEnabled;
            result = result && Math.Abs(value: obj._material.ColorFactor - _material.ColorFactor) < 0.0000001;
            result = result && Math.Abs(value: obj._material.AlphaFactor - _material.AlphaFactor) < 0.0000001;
            result = result && Math.Abs(value: obj._material.HueFactor - _material.HueFactor) < 0.0000001;
            result = result && Math.Abs(value: obj._material.SaturationFactor - _material.SaturationFactor) < 0.0000001;
            result = result && obj._material.Color == _material.Color;
            return result;
        }

        public override bool Equals(object obj)
        {
            return obj is Material material && Equals(obj: material);
        }

        public override int GetHashCode()
        {
            return (_material.Texture, _material.OverlayEnabled, _material.ColorFactor, _material.AlphaFactor, _material.HueFactor, _material.SaturationFactor, _material.Color).GetHashCode();
        }
        public static bool operator ==(Material lhs, Material rhs)
        {
            return lhs?.Equals(obj: rhs) ?? rhs is null;
        }
        public static bool operator !=(Material lhs, Material rhs)
        {
            return !(lhs?.Equals(obj: rhs) ?? rhs is null);
        }
        public static implicit operator SimpleMaterial(Material m) => m._material;
    }
}
