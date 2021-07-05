using BannerlordDraw.Drawing.Interfaces;
using DynamicData;
using ReactiveUI;
using System;
using System.Reactive.Linq;

namespace BannerlordDraw.Drawing.Geometry
{
    public class Quad : ReactiveObject, IDrawable, ITransformable
    {
        private Triangle t1;
        private Triangle t2;
        private DrawReason lastDrawReason;
        public Material Material { get; set; }

        private SourceCache<(IDrawable drawable, DrawReason lastDrawReason, IChangeSet<Position, int>), int> Change;
        public Quad(Position p1, Position p2, Position p3, Position p4, Material material)
        {
            Change = new SourceCache<(IDrawable drawable, DrawReason lastDrawReason, IChangeSet<Position, int>), int>(_ => Change.Count);
            Material = material;
            t1 = new Triangle(p1, p2, p3, material);
            t2 = new Triangle(p2, p3, p4, material);
            t1.Vertices.Connect().Distinct().Subscribe(OnNext);
            t2.Vertices.Connect().Distinct().Subscribe(OnNext);
        }

        private void OnNext(IChangeSet<Position, int> obj)
        {
            Change.Edit(innerCache =>
            {
                innerCache.AddOrUpdate((this, lastDrawReason, obj));
            });
        }

        public SourceCache<Triangle, int> Triangulate()
        {
            SourceCache<Triangle, int> triangles = new SourceCache<Triangle, int>(_ => 0); ;
            triangles.AddOrUpdate(t1);
            triangles.AddOrUpdate(t2);
            return triangles;
        }
        public IObservable<IChangeSet<(IDrawable Drawable, DrawReason LastDrawReason, IChangeSet<Position, int> ChangeSet), int>> Connect()
        {
            return Change.Connect();
        }

        public void Scale(float scaleFactor)
        {
            t1.Scale(scaleFactor);
            t2.Scale(scaleFactor);
        }
    }
}