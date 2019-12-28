using FlutterSharp.UI;

namespace FlutterSharp.SDK.Internal
{
    public class OffsetLayer : ContainerLayer
    {
        /// Creates an offset layer.
        ///
        /// By default, [offset] is zero. It must be non-null before the compositing
        /// phase of the pipeline.
        public OffsetLayer(Offset offset = null)
        {
            if (offset == null)
                offset = Offset.Zero;

            _offset = offset;
        }
        
        /// Offset from parent in the parent's coordinate system.
        ///
        /// The scene must be explicitly recomposited after this property is changed
        /// (as described at [Layer]).
        ///
        /// The [offset] property must be non-null before the compositing phase of the
        /// pipeline.
        private Offset _offset;
        public Offset Offset
        {
            get { return _offset; }
            set
            {
                if (value != _offset)
                {
                    MarkNeedsAddToScene();
                }
                _offset = value;
            }
        }

        public override void ApplyTransform(Layer child, Matrix4 transform)
        {
            transform.Multiply(Matrix4.TranslationValues(Offset.Dx, Offset.Dy, 0.0));
        }

        public override void AddToScene(SceneBuilder builder, Offset layerOffset)
        {
            // Skia has a fast path for concatenating scale/translation only matrices.
            // Hence pushing a translation-only transform layer should be fast. For
            // retained rendering, we don't want to push the offset down to each leaf
            // node. Otherwise, changing an offset layer on the very high level could
            // cascade the change to too many leaves.
            EngineLayer = builder.PushOffset(layerOffset.Dx + Offset.Dx, layerOffset.Dy + Offset.Dy, oldLayer: _engineLayer as OffsetEngineLayer);
            AddChildrenToScene(builder);
            builder.Pop();
        }
    }
}
