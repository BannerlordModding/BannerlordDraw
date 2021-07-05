using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.TwoDimension;
using System.Numerics;
using System.Reactive.Linq;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.GauntletUI;
using TaleWorlds.Engine.Screens;
using TaleWorlds.GauntletUI.Data;
using BannerlordDraw.Drawing;
using BannerlordDraw.Drawing.Geometry;
using BannerlordDraw.Drawing.Units.Angular;
using BannerlordDraw.Drawing.Units.Distance;
using Canvas = BannerlordDraw.Drawing.Canvas;
using Material = BannerlordDraw.Drawing.Material;
using Screen = BannerlordDraw.Drawing.Screen;
using Quad = BannerlordDraw.Drawing.Geometry.Quad;
using Bannerlord.UIExtenderEx.Attributes;
using System.Xml;
using Bannerlord.UIExtenderEx.Prefabs2;
using Bannerlord.UIExtenderEx;

namespace BannerlordDraw
{
    public class Submodule : MBSubModuleBase
    {
        public class ScreenRenderer : BrushWidget
        {
            private static Canvas canvas;
            private static Triangle t;
            public ScreenRenderer(UIContext context) : base(context: context)
            {
            }
            protected override void OnRender(TwoDimensionContext twoDimensionContext, TwoDimensionDrawContext drawContext)
            {
                if(canvas is null)
                {
                    canvas = new Canvas();
                    //canvas.AddDrawable(drawable: t2);
                    //Line line;
                    //line.Draw(twoDimensionContext: twoDimensionContext, twoDimensionDrawContext: drawContext);

                    //for (int i = 40; i < 3440-40; i+=40)
                    //{
                    //    line = new Line(
                    //        start: new Vertex(p: new Position(offset: new Offset(xOffset: 1, yOffset: i), origin: EOrigin.Center)),
                    //        end: new Vertex(p: new Position(offset: new Offset(xOffset: twoDimensionContext.Width-1, yOffset: i), origin: EOrigin.Center)),
                    //        thickness: new Distance(p: 2),
                    //        material: m
                    //    );
                    //    canvas.AddDrawable(line);
                    //}

                    //for (int i = 20; i < 40; i += 20)
                    //{
                    //    line = new Line(
                    //        start: new Vertex(p: new Position(offset: new Offset(xOffset: i, yOffset: 0), origin: EOrigin.Center)),
                    //        end: new Vertex(p: new Position(
                    //            offset: new Offset(xOffset: i, yOffset: twoDimensionContext.Width), origin: EOrigin.Center)),
                    //        thickness: new Distance(p: 2),
                    //        material: m
                    //    );
                    //    canvas.AddDrawable(line);
                    //}
                }
                else
                {
                    //Triangle t2 = new Triangle(
                    //    p1: new Vertex(p: new Position(offset: new Offset(xOffset: 700, yOffset: 700), origin: EOrigin.TopLeft)),
                    //    p2: new Vertex(p: new Position(offset: new Offset(xOffset: 900, yOffset: 900), origin: EOrigin.TopLeft)),
                    //    p3: new Vertex(p: new Position(offset: new Offset(xOffset: 500, yOffset: 900), origin: EOrigin.TopLeft)),
                    //    material: m
                    //);
                    if (t is null)
                    {
                        List<Material> mats = new List<Material>();
                        for (int i = 0; i < 5; i++)
                        {
                            mats.Add(new Material(twoDimensionContext: twoDimensionContext, color: ColorTranslator.FromHtml(htmlColor: $"#FF{Guid.NewGuid().ToString().Substring(0, 6)}"), renderOrder: 0));
                        }
                        Random r = new Random();
                        for (int i = 0; i < 1000000; i++)
                        {
                            Material m = mats[r.Next(0, mats.Count - 1)];
                            (int x1, int y1) = (r.Next(100, 1000), r.Next(100, 1000));
                            (int x2, int y2) = (r.Next(100, 1000), r.Next(100, 1000));
                            (int x3, int y3) = (r.Next(100, 1000), r.Next(100, 1000));
                            (int x4, int y4) = (r.Next(100, 1000), r.Next(100, 1000));                            
                            t = new Triangle(
                                p1: new Position(offset: new Offset(xOffset: x1, yOffset: y1), origin: EOrigin.TopLeft),
                                p2: new Position(offset: new Offset(xOffset: x2, yOffset: y2), origin: EOrigin.TopLeft),
                                p3: new Position(offset: new Offset(xOffset: x3, yOffset: y3), origin: EOrigin.TopLeft),
                                material: m
                            );
                            canvas.AddDrawable(drawable: t);
                        }
                    }
                    //Observable.Interval(TimeSpan.FromSeconds(2)).Subscribe(_ => Timer1OnTick(null, null));

                }
                //Curve c = new Curve(new List<Line>() {line});
                //c.AddEndVertex(new Vertex(new Position(new Offset(500,500), EOrigin.Center, twoDimensionContext.Width, twoDimensionContext.Height)));
                //c.Draw(twoDimensionContext, drawContext);
                //Draw vertical line   
                //DrawObject2D do2D = CreateTriangleTopologyMeshFromTriangulatedPolygon(
                //    LineToTriangulatedPolygon(
                //        new List<Vector2>()
                //        {
                //            new Vector2(x: 50.0f, y: 100.0f),
                //            new Vector2(x: 200.0f, y: 100.0f),
                //        },
                //        thickness: 10.0f
                //    )
                //);
                //Debug.Print("Drawing!");
                //Vector2 position = new Vector2(x: 0.0f, y: 0.0f);
                //SimpleMaterial sm = drawContext.CreateSimpleMaterial();
                //sm.Color = Color.White;
                //sm.OverlayEnabled = false;
                //sm.CircularMaskingEnabled = false;
                //sm.ColorFactor = 1.0f;
                //sm.AlphaFactor = 1.0f;
                //sm.HueFactor = 1.0f;
                ////sm.Texture = new Texture(new EngineTexture(GetTexture()));
                //ResourceTextureProvider rtp = new ResourceTextureProvider();
                //sm.Texture = rtp.GetTexture(twoDimensionContext, "Test.tif");
                //twoDimensionContext.Draw(
                //    50.0f,
                //    50.0f,
                //    sm,
                //    do2D
                //);
                // JUNK
                //sm.Texture = new Texture(new EngineTexture(GetTexture()));
                // This also seems to work, what's the difference? I don't know, above version seems cleaner
                //drawContext.Draw(
                //    x: position.X,
                //    y: position.Y,
                //    material: sm,
                //    drawObject2D: do2Daltalt,
                //    width: do2D.Width,    // This doesn't seem to do anything be it 0, float.NaN, or some obscenely large value
                //    height: do2D.Height   // This doesn't seem to do anything be it 0, float.NaN, or some obscenely large value
                //);
                canvas.Render(twoDimensionContext: twoDimensionContext, drawContext: drawContext);
                base.OnRender(twoDimensionContext: twoDimensionContext, drawContext: drawContext);
            }

            private void Timer1OnTick(object sender, EventArgs e)
            {
                //t.Project(new Angle(0), new Distance(1));
            }
        }

        public class CustomScreenBase : ScreenBase
        {
            //private AutoBlockerConfigMenuVM _dataSource;
            private ViewModel _dataSource = null;
            private GauntletLayer _gauntletLayer;   
            private GauntletMovie _movie;

            protected override void OnInitialize()
            {
                Debug.Print("Initializing ScreenRenderer...");
                base.OnInitialize();
                //this._dataSource = new AutoBlockerConfigMenuVM();
                GauntletLayer gauntletLayer = new GauntletLayer(localOrder: 100)
                {
                    IsFocusLayer = true
                };
                _gauntletLayer = gauntletLayer;
                AddLayer(layer: _gauntletLayer);
                _gauntletLayer.InputRestrictions.SetInputRestrictions();
                _movie = (GauntletMovie)_gauntletLayer.LoadMovie(movieName: "CustomTestVM", dataSource: _dataSource);
            }

            protected override void OnActivate()
            {
                base.OnActivate();
                ScreenManager.TrySetFocus(layer: _gauntletLayer);
            }

            protected override void OnDeactivate()
            {
                base.OnDeactivate();
                _gauntletLayer.IsFocusLayer = false;
                ScreenManager.TryLoseFocus(layer: _gauntletLayer);
            }

            protected override void OnFinalize()
            {
                base.OnFinalize();
                RemoveLayer(layer: _gauntletLayer);
                _dataSource = null;
                _gauntletLayer = null;
            }
        }
        protected override void OnSubModuleLoad()
        {
            _uiExtender.Register(typeof(Submodule).Assembly);
            _uiExtender.Enable();
        }
        private UIExtender _uiExtender = new UIExtender("BannerlordDraw");
    }
    [PrefabExtension("InitialScreen", "/Prefab/Window/Widget/Children", autoGenWidgetName: "InitialScreen")]
    public sealed class CraftingScreenWidgetPatch : PrefabExtensionInsertPatch
    {
        public override InsertType Type => InsertType.Child;
        public override int Index => 0;

        private List<XmlNode> nodes;

        public CraftingScreenWidgetPatch()
        {
            XmlDocument firstChild = new XmlDocument();
            firstChild.LoadXml("<ScreenRenderer/>");

            nodes = new List<XmlNode> { firstChild };
        }

        // Just to demonstrate that both Properties and Methods are supported.
        [PrefabExtensionXmlNodes]
        public IEnumerable<XmlNode> Nodes => nodes;
    }
}
