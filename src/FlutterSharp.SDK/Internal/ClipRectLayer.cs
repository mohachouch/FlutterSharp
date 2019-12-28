using FlutterSharp.UI;

namespace FlutterSharp.SDK.Internal
{
    public class ClipRectLayer : ContainerLayer
    {
        /// Creates a layer with a rectangular clip.
        ///
        /// The [clipRect] argument must not be null before the compositing phase of
        /// the pipeline.
        ///
        /// The [clipBehavior] argument must not be null, and must not be [Clip.none].
        public ClipRectLayer(Rect clipRect = null, Clip clipBehavior = Clip.HardEdge)
        {
            _clipRect = clipRect;
            _clipBehavior = clipBehavior;
        }

        /// The rectangle to clip in the parent's coordinate system.
        ///
        /// The scene must be explicitly recomposited after this property is changed
        /// (as described at [Layer]).
        private Rect _clipRect;
        public Rect ClipRect
        {
            get { return _clipRect; }
            set
            {
                if (value != _clipRect)
                {
                    _clipRect = value;
                    MarkNeedsAddToScene();
                }
            }
        }
        
        /// {@template flutter.clipper.clipBehavior}
        /// Controls how to clip.
        ///
        /// Must not be set to null or [Clip.none].
        /// {@endtemplate}
        ///
        /// Defaults to [Clip.hardEdge].
        private Clip _clipBehavior;
        public Clip ClipBehavior
        {
            get { return _clipBehavior; }
            set
            {
                if (value != _clipBehavior)
                {
                    _clipBehavior = value;
                    MarkNeedsAddToScene();
                }
            }
        }

        public override void AddToScene(SceneBuilder builder, Offset layerOffset)
        {
            if (layerOffset == null)
                layerOffset = Offset.Zero;

            bool enabled = true;
           
            if (enabled)
            {
                Rect shiftedClipRect = layerOffset == Offset.Zero ? ClipRect : ClipRect.Shift(layerOffset);
                EngineLayer = builder.PushClipRect(shiftedClipRect, clipBehavior: ClipBehavior, oldLayer: _engineLayer as ClipRectEngineLayer);
            }
            else
            {
                EngineLayer = null;
            }
            AddChildrenToScene(builder, layerOffset);
            if (enabled)
                builder.Pop();
        }
    }
}
