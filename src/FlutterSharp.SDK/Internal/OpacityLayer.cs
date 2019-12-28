using FlutterSharp.UI;

namespace FlutterSharp.SDK.Internal
{
    public class OpacityLayer : ContainerLayer
    {
        /// Creates an opacity layer.
        ///
        /// The [alpha] property must be non-null before the compositing phase of
        /// the pipeline.
        public OpacityLayer(int alpha = 0, Offset offset = null)
        {
            if (offset == null)
                offset = Offset.Zero;

            _alpha = alpha;
            _offset = offset;
        }

        /// The amount to multiply into the alpha channel.
        ///
        /// The opacity is expressed as an integer from 0 to 255, where 0 is fully
        /// transparent and 255 is fully opaque.
        ///
        /// The scene must be explicitly recomposited after this property is changed
        /// (as described at [Layer]).
        private int _alpha;
        public int Alpha
        {
            get { return _alpha; }
            set
            {
                if (value != _alpha)
                {
                    _alpha = value;
                    MarkNeedsAddToScene();
                }
            }
        }

        /// Offset from parent in the parent's coordinate system.
        private Offset _offset;
        public Offset Offset
        {
            get { return _offset; }
            set
            {
                if (value != _offset)
                {
                    _offset = value;
                    MarkNeedsAddToScene();
                }
            }
        }

        public override void ApplyTransform(Layer child, Matrix4 transform)
        {
            transform.Translate(Offset.Dx, Offset.Dy);
        }

        public override void AddToScene(SceneBuilder builder, Offset layerOffset)
        {
            if (layerOffset == null)
                layerOffset = Offset.Zero;

            bool enabled = FirstChild != null;  // don't add this layer if there's no child
           
            if (enabled)
                EngineLayer = builder.PushOpacity(Alpha, offset: Offset + layerOffset, oldLayer: _engineLayer as OpacityEngineLayer);
            else
                EngineLayer = null;
            AddChildrenToScene(builder);
            if (enabled)
                builder.Pop();
        }
    }
}
