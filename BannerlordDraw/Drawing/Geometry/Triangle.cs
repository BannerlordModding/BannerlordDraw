using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BannerlordDraw.Annotations;
using BannerlordDraw.Drawing.Interfaces;
using DynamicData;
using DynamicData.Alias;
using ReactiveUI;
using TaleWorlds.TwoDimension;
using Color = TaleWorlds.Library.Color;

namespace BannerlordDraw.Drawing.Geometry
{
    public class Triangle : ReactiveObject, IDrawable, IMoveable, ITransformable
    {
        public SourceCache<Position, int> Vertices;
        private DrawReason lastDrawReason;
        public Material Material { get; set; }
        private SourceCache<(IDrawable drawable, DrawReason lastDrawReason, IChangeSet<Position, int>), int> Change;
        public Triangle(Position p1, Position p2, Position p3, Material material)
        {
            Change = new SourceCache<(IDrawable drawable, DrawReason lastDrawReason, IChangeSet<Position, int>), int>(_ => Change.Count);
            Vertices = new SourceCache<Position, int>(position => Vertices.Count);
            Material = material;
            Vertices.Connect().Distinct().Subscribe(OnNext);
            Vertices.Edit(innerCache =>
            {
                innerCache.AddOrUpdate(p1);
                innerCache.AddOrUpdate(p2);
                innerCache.AddOrUpdate(p3);
            });
            //ChangeSet<Position, int> init = new ChangeSet<Position, int>();
            //init.AddRange(Vertices.KeyValues.Select(kvp => new Change<Position, int>(ChangeReason.Add, kvp.Key, kvp.Value)));
            //OnNext(init);
        }

        private void OnNext(IChangeSet<Position, int> obj)
        {
            Change.Edit(innerCache =>
            {
                innerCache.AddOrUpdate((this, this.lastDrawReason, obj));
            });
        }

        public IObservable<IChangeSet<(IDrawable Drawable, DrawReason LastDrawReason, IChangeSet<Position, int> ChangeSet), int>> Connect()
        {
             return Change.Connect();
        }

        public SourceCache<Triangle, int> Triangulate()
        {
            SourceCache<Triangle, int> triangles = new SourceCache<Triangle, int>(_ => 0);;
            triangles.AddOrUpdate(this);
            return triangles;
        }
        
        public void Project(Angle angle, Distance distance)
        {
            Vertices.Edit(innerCache =>
            {
                //doing it this way will only product a single changeset
                for (int i = 0; i < innerCache.Items.Count(); i++)
                {
                    KeyValuePair<int, Position> newKVP = new KeyValuePair<int, Position>(i, innerCache.Items.ElementAt(i).Project(angle, distance));
                    innerCache.AddOrUpdate(newKVP);
                }
            });
        }

        public void Translate(Offset offset)
        {
            lastDrawReason = DrawReason.Translate;
            Vertices.Edit(innerCache =>
            {
                //doing it this way will only product a single changeset
                for (int i = 0; i < innerCache.Items.Count(); i++)
                {
                    KeyValuePair<int, Position> newKVP = new KeyValuePair<int, Position>(i, innerCache.Items.ElementAt(i).Translate(offset));
                    innerCache.AddOrUpdate(newKVP);
                }
            });
        }

        public void Scale(float scaleFactor)
        {
            lastDrawReason = DrawReason.Scale;
            throw new NotImplementedException();
        }
    }
}
