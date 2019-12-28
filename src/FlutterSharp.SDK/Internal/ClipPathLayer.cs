using FlutterSharp.UI;

namespace FlutterSharp.SDK.Internal
{
    public class ClipPathLayer : ContainerLayer
    {
        public ClipPathLayer(Path clipPath = null, Clip clipBehavior = Clip.AntiAlias)
        {
            _clipPath = clipPath;
            _clipBehavior = clipBehavior;
        }
        
        private Path _clipPath;
        public Path ClipPath
        {
            get { return _clipPath; }
            set
            {
                if (value != _clipPath)
                {
                    _clipPath = value;
                    MarkNeedsAddToScene();
                }
            }
        }
        
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
                Path shiftedPath = layerOffset == Offset.Zero ? ClipPath : ClipPath.Shift(layerOffset);
                EngineLayer = builder.PushClipPath(shiftedPath, clipBehavior: ClipBehavior, oldLayer: _engineLayer as ClipPathEngineLayer);
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
