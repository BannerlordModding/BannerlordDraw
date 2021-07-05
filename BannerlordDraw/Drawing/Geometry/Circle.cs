using System.Collections.Generic;
using BannerlordDraw.Drawing.Units.Angular;
using TaleWorlds.TwoDimension;

namespace BannerlordDraw.Drawing.Geometry
{
    public class Circle
    {
        //private Vertex _centerPoint;
        //private List<Vertex> _edgeVertices;
        //Distance _radius;

        //public Position Position => _centerPoint.Position;

        //public Circle(Position p, Distance radius, int numEdgeVertices)
        //{
        //    _centerPoint = new Vertex(p: p);
        //    _radius = radius;
        //    _edgeVertices = new List<Vertex>(capacity: numEdgeVertices);
        //    ComputeEdgeVertices();
        //}
        //private void ComputeEdgeVertices()
        //{
        //    int numVertices = _edgeVertices.Capacity;
        //    Degrees theta = new Degrees(value: 360.0f / numVertices);
        //    for(int i = 0; i < numVertices; i++)
        //    {
        //        Vertex newEdgeVertex = _centerPoint.Clone();
        //        newEdgeVertex.Project(angle: new Angle(degrees: (double)theta * i), distance: _radius);
        //        _edgeVertices.Add(item: newEdgeVertex);
        //    }
        //}
        //public void Draw(TwoDimensionContext twoDimensionContext, TwoDimensionDrawContext twoDimensionDrawContext)
        //{

        //}
        //public void Triangulate()
        //{

        //}
    }
}
