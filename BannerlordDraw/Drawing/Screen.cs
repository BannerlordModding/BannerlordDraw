using System.Collections.Generic;

namespace BannerlordDraw.Drawing
{
    public class Screen
    {
        private List<Canvas> Canvases = new List<Canvas>();
        public static float Width => TaleWorlds.Engine.Screen.RealScreenResolutionWidth;
        public static float Height => TaleWorlds.Engine.Screen.RealScreenResolutionWidth;
        public static float AspectRatio => TaleWorlds.Engine.Screen.AspectRatio;
    }
}
    