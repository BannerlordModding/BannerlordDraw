//using System;
//using System.Collections.Generic;
//using System.Linq;
//using BannerlordDraw.Drawing.Interfaces;
//using BannerlordDraw.Drawing.Units.Distance;
//using DynamicData;
//using TaleWorlds.Library;
//using TaleWorlds.TwoDimension;

//namespace BannerlordDraw.Drawing.Geometry
//{
//    public class Curve : IDrawable, IMoveable, ITransformable
//    {
//        private readonly List<Line> _lines;
//        private Distance _length;
//        public Distance Length => _length ?? (_length = CalculateLength());

//        //public SourceList<DrawableUpdateArgs> Change { get; }
//        //public event OnUpdateHandler OnUpdate;
//        public Material Material { get; private set; }
//        public float[] TriangulatedVertices { get; private set; }
//        private uint[] _triangulatedIndices;
//        public Curve(List<Line> lines)
//        {
//            _lines = lines;
//        }
//        public void AddEndVertex(Position newVertex)
//        {
//            Line lastLine = _lines.Last();
//            _lines.Add(
//                item: new Line(start: lastLine.EndVertex, end: newVertex, thickness: lastLine.Thickness, material: Material)
//            );
//        }
//        public (List<Triangle>, Material) Draw()
//        {
//            if(TriangulatedVertices is null)
//                Triangulate();
//            _triangulatedIndices = new uint[TriangulatedVertices.Length / 2];

//            for (uint i = 0; i < _triangulatedIndices.Length; i++)
//            {
//                _triangulatedIndices[i] = i;
//            }

//            return (new List<Triangle>(), null);
//        }
//        private Distance CalculateLength()
//        {
//            return new Distance(
//                p: new Pixels(value: _lines.Sum(selector: line => (Pixels)line.Length))
//            );
//        }

//        public SourceCache<Triangle, int> Triangulate()
//        {
//            //int numLines = _lines.Count;
//            //List<Triangle> triangles = new List<Triangle>(capacity: numLines * 2);
//            //for(int j = 0; j < numLines; j++)
//            //{
//            //    triangles.AddRange(collection: _lines[index: j].Triangulate());
//            //}

//            return null;
//        }
        
//    }
//}
