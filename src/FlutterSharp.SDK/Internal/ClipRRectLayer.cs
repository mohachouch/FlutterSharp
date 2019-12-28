using FlutterSharp.UI;

namespace FlutterSharp.SDK.Internal
{
    public class ClipRRectLayer : ContainerLayer
    {
        public ClipRRectLayer(RRect clipRect = null, Clip clipBehavior = Clip.AntiAlias)
        {
            _clipRRect = clipRect;
            _clipBehavior = clipBehavior;
        }

        /// The rectangle to clip in the parent's coordinate system.
        ///
        /// The scene must be explicitly recomposited after this property is changed
        /// (as described at [Layer]).
        private RRect _clipRRect;
        public RRect ClipRRect
        {
            get { return _clipRRect; }
            set
            {
                if (value != _clipRRect)
                {
                    _clipRRect = value;
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
        /// Defaults to [Clip.AntiAlias].
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
                RRect shiftedClipRRect = layerOffset == Offset.Zero ? ClipRRect : ClipRRect.Shift(layerOffset);
                EngineLayer = builder.PushClipRRect(shiftedClipRRect, clipBehavior: ClipBehavior, oldLayer: _engineLayer as ClipRRectEngineLayer);
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
