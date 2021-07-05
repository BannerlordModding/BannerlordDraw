//using System;
//using System.Collections.Generic;
//using System.Numerics;
//using TaleWorlds.GauntletUI;
//using TaleWorlds.Library;
//using TaleWorlds.TwoDimension;
//using System.Drawing;
//using BannerlordDraw.Drawing.Exceptions;
//using BannerlordDraw.Drawing.Interfaces;
//using BannerlordDraw.Drawing.Units.Angular;
//using BannerlordDraw.Drawing.Units.Distance;
//using DynamicData;
//using Color = TaleWorlds.Library.Color;

//namespace BannerlordDraw.Drawing.Geometry
//{
//    public class Line : IDrawable, IMoveable
//    {
//        private Distance _length;
//        public Distance Length => _length ?? (_length = CalculateLength());
//        public Position StartVertex { get; private set; }
//        public Position EndVertex { get; private set; }
//        public Distance Thickness { get; }
//        //public SourceList<DrawableUpdateArgs> Change { get; }
//        //public event OnUpdateHandler OnUpdate;
//        public Material Material { get; private set; }
        
//        public Line(Position start, Position end, Distance thickness, Material material)
//        {
//            Thickness = thickness;
//            StartVertex = start;
//            EndVertex = end;
//            if(StartVertex == EndVertex)
//            {
//                throw new BadGeometryException(message: $"Lines cannot have zero length but line starts and terminates at ${StartVertex}");
//            }
//            Material = material;
//        }

//        public Line(Line line, Angle angle, Distance distance)
//        {
//            Line newLine = line.Clone();
//            newLine.Project(angle: angle, distance: distance);
//            StartVertex = newLine.StartVertex;
//            EndVertex = newLine.EndVertex;
//            Thickness = newLine.Thickness;
//        }

//        public Angle GetInclination()
//        {
//            Radians radians = new Radians(value: Math.Atan(d: (EndVertex.X - StartVertex.X) / (EndVertex.Y - StartVertex.Y)));
//            return new Angle(degrees: new Degrees(value: 90) - (Degrees)radians);
//        }
//        private Distance CalculateLength()
//        {
//            return new Distance(
//                p: new Pixels(value: Math.Sqrt(d: Math.Pow(x: EndVertex.X - StartVertex.X, y: 2) + Math.Pow(x: EndVertex.Y - StartVertex.Y, y: 2)))
//            );
//        }
//        public SourceCache<Triangle, int> Triangulate()
//        {
//            Angle inclination = GetInclination();
//            Angle incL2 = inclination - 90.0;
//            Angle incL1= inclination + 90.0;
//            Line l1 = new Line(line: this, angle: incL1, distance: (Pixels) Thickness / 2.0);
//            Line l2 = new Line(line: this, angle: incL2, distance: (Pixels) Thickness / 2.0);
//            //return new List<Triangle>
//            //{                
//            //    new Triangle(p1: l1.StartVertex, p2: l1.EndVertex, p3: l2.StartVertex, material: Material),
//            //    new Triangle(p1: l2.StartVertex, p2: l1.EndVertex, p3: l2.EndVertex, material: Material)
//            //    //new Triangle(l1.StartVertex, l2.StartVertex, l1.EndVertex, Material),
//            //    //new Triangle(l1.EndVertex, l2.StartVertex, l2.EndVertex, Material)
//            //};
//            return null;
//        }

//        public IObservable<IChangeSet<(IDrawable Drawable, DrawReason LastDrawReason, Position Position), int>> Connect()
//        {
//            throw new NotImplementedException();
//        }

//        public void Project(Angle angle, Distance distance)
//        {
//            //StartVertex = new Vertex(p: StartVertex.Position.Project(angle: angle, distance: distance));
//            //EndVertex = new Vertex(p: EndVertex.Position.Project(angle: angle, distance: distance));
//        }

//        public void Translate(Offset offset)
//        {
//            StartVertex.Translate(offset: offset);
//            EndVertex.Translate(offset: offset);
//        }
//        public Line Clone()
//        {
//            return new Line(start: StartVertex, end: EndVertex, thickness: Thickness, material: Material);
//        }

//        IObservable<IChangeSet<(IDrawable Drawable, DrawReason LastDrawReason, IChangeSet<Position, int> ChangeSet), int>> IDrawable.Connect()
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
