using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BannerlordDraw.Drawing.Geometry;
using BannerlordDraw.Drawing.Interfaces;
using DynamicData;
using DynamicData.Binding;
using DynamicData.Kernel;
using ReactiveUI;
using TaleWorlds.Engine;
using TaleWorlds.TwoDimension;

namespace BannerlordDraw.Drawing
{
    class Canvas
    {
        private ConcurrentDictionary<Material, ConcurrentDictionary<Position, uint>> _materialVertices;
        private ConcurrentDictionary<(Material, Position), uint> _materialIndices;
        private ConcurrentDictionary<Material, SynchronizedCollection<float>> _materialRenderVertices;
        private ConcurrentDictionary<Material, SynchronizedCollection<uint>> _materialRenderIndices;
        private ConcurrentDictionary<Material, SynchronizedCollection<IDrawable>> _materialDrawables;
        private HashSet<IDrawable> drawables;
        private ConcurrentDictionary<IDrawable, SynchronizedCollection<Position>> drawableVertices;
        private ConcurrentDictionary<IDrawable, SynchronizedCollection<uint>> drawableIndices;

        private ConcurrentDictionary<Material, float[]> _cachedVertices;
        private ConcurrentDictionary<Material, uint[]> _cachedIndices;
        private ConcurrentDictionary<Material, float[]> _cachedUVs;
        private bool isDirty = true;
        public Canvas()
        {
            drawables = new HashSet<IDrawable>();
            drawableVertices = new ConcurrentDictionary<IDrawable, SynchronizedCollection<Position>>();
            _materialVertices = new ConcurrentDictionary<Material, ConcurrentDictionary<Position, uint>>();
            _materialRenderVertices = new ConcurrentDictionary<Material, SynchronizedCollection<float>>();
            _materialRenderIndices = new ConcurrentDictionary<Material, SynchronizedCollection<uint>>();
            _materialIndices = new ConcurrentDictionary<(Material, Position), uint>();
            drawableIndices = new ConcurrentDictionary<IDrawable, SynchronizedCollection<uint>>();
            _materialDrawables = new ConcurrentDictionary<Material, SynchronizedCollection<IDrawable>>();
        }

        ~Canvas()
        {

        }

        public void AddDrawable(IDrawable drawable)
        {
            if (drawables.Contains(drawable)) return;
            drawable.Connect().Subscribe(OnNext);
            drawables.Add(drawable);
            if (!_materialDrawables.ContainsKey(drawable.Material))
                _materialDrawables.TryAdd(drawable.Material, new SynchronizedCollection<IDrawable>());
            _materialDrawables[drawable.Material].Add(drawable);
        }

        private void OnNext(IChangeSet<(IDrawable Drawable, DrawReason LastDrawReason, IChangeSet<Position, int> ChangeSet), int> obj)
        {
            foreach (Change<(IDrawable Drawable, DrawReason LastDrawReason, IChangeSet<Position, int> ChangeSet), int> cs in obj)
            {
                (IDrawable Drawable, DrawReason LastDrawReason, IChangeSet<Position, int> ChangeSet) currentSet = cs.Current;
                foreach (Change<Position, int> change in currentSet.ChangeSet)
                {
                    Optional<Position> previous = change.Previous;
                    Position current = change.Current;
                    //Remove the previous position if applicable
                    if (previous.HasValue)
                    {
                        RemovePosition(currentSet.Drawable, previous.Value);
                    }
                    AddPosition(currentSet.Drawable, current);
                }
            }
        }

        private void RemovePosition(IDrawable drawable, Position position)
        {
            // If we're not deleting the position no additional logic is necessary
            _materialVertices.TryGetValue(drawable.Material, out ConcurrentDictionary<Position, uint> drawablePositions);
            drawablePositions.TryGetValue(position, out uint referenceCount);
            drawablePositions.TryUpdate(position, referenceCount--, referenceCount);
            if (referenceCount > 0) return;
            // Update the renderable vertices, remove the previous position if it exists
            drawablePositions?.RemoveIfContained(position);
            if (!_materialRenderVertices.ContainsKey(drawable.Material))
            {
                Debug.Print("This should never happen, throw an exception here.");
            }
            
            _materialRenderVertices[drawable.Material].Remove(position.X);
            _materialRenderVertices[drawable.Material].Remove(position.Y);
            isDirty = true;
        }

        private void AddPosition(IDrawable drawable, Position position)
        {
            if (!drawableVertices.TryGetValue(drawable, out SynchronizedCollection<Position> drawablePositions))
            {
                drawableVertices[drawable] = new SynchronizedCollection<Position>();
            }
            drawableVertices[drawable].Add(position);
            // Add the new position if no vertex was there previously
            if (!_materialVertices.TryGetValue(drawable.Material, out ConcurrentDictionary<Position, uint> positionDictionary))
            {
                positionDictionary = new ConcurrentDictionary<Position, uint>();
                _materialVertices[drawable.Material] = positionDictionary;
            }
            // If no vertex was at this position create a new reference to the position
            if (!positionDictionary.TryGetValue(position, out uint referenceCount))
            {
                referenceCount = 0;
            }

            _materialVertices[drawable.Material][position] = referenceCount;
            // Increase the reference count to the new position of the vertex
            referenceCount += 1;
            positionDictionary[position] = referenceCount;

            // No vertex previously existed so we need to add a new render vertex
            if (!_materialRenderVertices.Keys.Contains(drawable.Material))
            {
                _materialRenderVertices[drawable.Material] = new SynchronizedCollection<float>();
            }
            isDirty = true;
        }

        private void GenerateRenderIndices()
        {
            _cachedIndices = new ConcurrentDictionary<Material, uint[]>();
            _cachedVertices = new ConcurrentDictionary<Material, float[]>();
            _cachedUVs = new ConcurrentDictionary<Material, float[]>();
            foreach (Material m in _materialVertices.Keys)
            {
                if (!_materialRenderIndices.ContainsKey(m))
                {
                    _materialRenderIndices[m] = new SynchronizedCollection<uint>(_materialVertices[m].Keys.Count * 2);
                }
                // Should I remove the following line?
                _materialRenderVertices.TryAdd(m, new SynchronizedCollection<float>());
                _materialVertices.TryGetValue(m, out ConcurrentDictionary<Position, uint> materialDictionary);
                if (materialDictionary is null)
                {
                    materialDictionary = new ConcurrentDictionary<Position, uint>();
                    _materialVertices.TryAdd(m, materialDictionary);
                }
                uint i = (uint)_materialRenderVertices[m].Count;
                foreach (Position p in materialDictionary.Keys)
                {
                    _materialRenderVertices[m].Add(p.X);
                    _materialRenderVertices[m].Add(p.Y);
                    _materialIndices.TryAdd((m, p), i);
                    i++;
                    //_materialRenderIndices[m].Add((uint)_materialRenderVertices[m].Count() / 2 - 1);
                }
                _materialDrawables.TryGetValue(m, out SynchronizedCollection<IDrawable> materialDrawables);
                foreach (IDrawable d in materialDrawables)
                {
                    foreach(Position p in drawableVertices[d])
                    {
                        if (!drawableIndices.ContainsKey(d))
                        {
                            drawableIndices.TryAdd(d, new SynchronizedCollection<uint>());
                        }
                        var value = _materialIndices[(m, p)];
                        drawableIndices[d].Add(value);
                    }
                    _materialRenderIndices[m].Add(drawableIndices[d].ToList());
                }
                _cachedVertices.TryAdd(m, _materialRenderVertices[m].AsArray());
                _cachedIndices.TryAdd(m, _materialRenderIndices[m].AsArray());
                _cachedUVs.TryAdd(m, new float[_materialRenderVertices[m].Count * 2]);
            }
            isDirty = false;
        }
        public void Render(TwoDimensionContext twoDimensionContext, TwoDimensionDrawContext drawContext)
        {
            var start = DateTime.Now;
            if (isDirty)
            {
                GenerateRenderIndices();
            }
            foreach (Material m in _materialVertices.Keys)
            {
                bool success = _materialRenderVertices.TryGetValue(m, out SynchronizedCollection<float> renderVertices);
                int numVertices = renderVertices.Count;
                if (!success || renderVertices is null || numVertices < 3) return;
                success = _materialRenderIndices.TryGetValue(m, out SynchronizedCollection<uint> renderIndices);
                int numIndices = renderIndices.Count;
                if (!success || renderIndices is null || numIndices % 3 != 0) return;
                DrawObject2D do2D = new DrawObject2D(topology: MeshTopology.Triangles, vertices: _cachedVertices[m], uvs: _cachedUVs[m], indices: _cachedIndices[m], vertexCount: numIndices);
                twoDimensionContext.Draw(
                    x: 0.0f,
                    y: 0.0f,
                    material: m,
                    drawObject2D: do2D,
                    layer: 0
                );
            }
            var end = DateTime.Now;
            var total = end - start;
        }
    }
}
