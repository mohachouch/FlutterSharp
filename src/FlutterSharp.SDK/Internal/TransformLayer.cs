using FlutterSharp.UI;

namespace FlutterSharp.SDK.Internal
{
    internal class TransformLayer : OffsetLayer
    {
        public TransformLayer(Matrix4 transform = null, Offset offset = null)
            : base(offset)
        {
            this._transform = transform;
        }

        /// The matrix to apply.
        ///
        /// The scene must be explicitly recomposited after this property is changed
        /// (as described at [Layer]).
        ///
        /// This transform is applied before [offset], if both are set.
        ///
        /// The [transform] property must be non-null before the compositing phase of
        /// the pipeline.
        Matrix4 _transform;
        public Matrix4 Transform
        {
            get
            {
                return _transform;
            }
            set
            {
                if (value == _transform)
                    return;
                _transform = value;
                _inverseDirty = true;
                MarkNeedsAddToScene();
            }
        }

        Matrix4 _lastEffectiveTransform;
        Matrix4 _invertedTransform;
        bool _inverseDirty = true;

        public override void AddToScene(SceneBuilder builder, Offset layerOffset)
        {
            if (layerOffset == null)
                layerOffset = Offset.Zero;

            _lastEffectiveTransform = Transform;
            Offset totalOffset = Offset + layerOffset;
            if (totalOffset != Offset.Zero)
            {
                var tempMatrix4 = Matrix4.TranslationValues(totalOffset.Dx, totalOffset.Dy, 0.0);
                _lastEffectiveTransform.Multiply(_lastEffectiveTransform);
                _lastEffectiveTransform = tempMatrix4;
            }
            EngineLayer = builder.PushTransform(_lastEffectiveTransform.Storage, oldLayer: _engineLayer as TransformEngineLayer);
            AddChildrenToScene(builder);
            builder.Pop();
        }

        public override void ApplyTransform(Layer child, Matrix4 transform)
        {
            if (_lastEffectiveTransform == null)
            {
                transform.Multiply(this.Transform);
            }
            else
            {
                transform.Multiply(_lastEffectiveTransform);
            }
        }
    }
}