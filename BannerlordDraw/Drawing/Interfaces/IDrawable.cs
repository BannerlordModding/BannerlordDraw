using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using BannerlordDraw.Drawing.Geometry;
using DynamicData;
using TaleWorlds.TwoDimension;

namespace BannerlordDraw.Drawing.Interfaces
{
    //public delegate void OnUpdateHandler(DrawableUpdateArgs drawableUpdateArgs);
    
    public static class ObservableExtensions
    {
        public static IObservable<(TSource previous, TSource current)> PairWithPrevious<TSource>(this IObservable<TSource> source)
        {
            return source.Scan(
                seed: (Previous: default(TSource), Current: default(TSource)),
                accumulator: (acc, current) => (Previous: acc.Current, Current: current));
        }
    }
    public enum DrawReason
    {
        Add = 0,
        VertexAltered,
        Project,
        Translate,
        Scale,
        Delete
    }
    //public class DrawableUpdateArgs : EventArgs
    //{
    //    public IDrawable Drawable { get; }
    //    public DrawReason DrawReason { get; }
    //    public IChangeSet<Position, int> ChangedPositions { get; }

    //    public DrawableUpdateArgs(IDrawable drawable, DrawReason drawReason, IChangeSet<Position, int> changedPositions)
    //    {
    //        Drawable = drawable;
    //        DrawReason = drawReason;
    //        ChangedPositions = changedPositions;
    //    }
    //} 
    public interface IDrawable
    {
        //event OnUpdateHandler OnUpdate;
        Material Material { get; }
        SourceCache<Triangle, int> Triangulate();
        IObservable<IChangeSet<(IDrawable Drawable, DrawReason LastDrawReason, IChangeSet<Position, int> ChangeSet), int>> Connect();
    }
}
