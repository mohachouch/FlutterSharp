using FlutterSharp.UI;

namespace FlutterSharp.SDK.Internal
{
    public class PictureLayer : Layer
    {
        /// Creates a leaf layer for the layer tree.
        public PictureLayer(Rect canvasBounds)
        {
            this.CanvasBounds = canvasBounds;
        }

        /// The bounds that were used for the canvas that drew this layer's [picture].
        ///
        /// This is purely advisory. It is included in the information dumped with
        /// [debugDumpLayerTree] (which can be triggered by pressing "L" when using
        /// "flutter run" at the console), which can help debug why certain drawing
        /// commands are being culled.
        public readonly Rect CanvasBounds;

        /// The picture recorded for this layer.
        ///
        /// The picture's coordinate system matches this layer's coordinate system.
        ///
        /// The scene must be explicitly recomposited after this property is changed
        /// (as described at [Layer]).
        private Picture _picture;
        public Picture Picture
        {
            get
            {
                return _picture;
            }
            set
            {
                _picture = value;
                MarkNeedsAddToScene();
            }
        }

        /// Hints that the painting in this layer is complex and would benefit from
        /// caching.
        ///
        /// If this hint is not set, the compositor will apply its own heuristics to
        /// decide whether the this layer is complex enough to benefit from caching.
        ///
        /// The scene must be explicitly recomposited after this property is changed
        /// (as described at [Layer]).
        private bool _isComplexHint;
        public bool IsComplexHint
        {
            get
            {
                return _isComplexHint;
            }
            set
            {
                if (value != _isComplexHint)
                {
                    _isComplexHint = value;
                    MarkNeedsAddToScene();
                }
            }
        }

        /// Hints that the painting in this layer is likely to change next frame.
        ///
        /// This hint tells the compositor not to cache this layer because the cache
        /// will not be used in the future. If this hint is not set, the compositor
        /// will apply its own heuristics to decide whether this layer is likely to be
        /// reused in the future.
        ///
        /// The scene must be explicitly recomposited after this property is changed
        /// (as described at [Layer]).
        private bool _willChangeHint;
        public bool WillChangeHint
        {
            get
            {
                return _willChangeHint;
            }
            set
            {
                if (value != _willChangeHint)
                {
                    _willChangeHint = value;
                    MarkNeedsAddToScene();
                }
            }
        }

        public override void AddToScene(SceneBuilder builder, Offset layerOffset)
        {
            builder.AddPicture(layerOffset, Picture, isComplexHint: IsComplexHint, willChangeHint: WillChangeHint);
        }
    }
}
