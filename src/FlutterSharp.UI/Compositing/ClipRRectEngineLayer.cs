namespace FlutterSharp.UI
{
    /// An opaque handle to a clip rounded rect engine layer.
    ///
    /// Instances of this class are created by [SceneBuilder.pushClipRRect].
    ///
    /// {@macro dart.ui.sceneBuilder.oldLayerCompatibility}
    public class ClipRRectEngineLayer : EngineLayerWrapper
    {
        public ClipRRectEngineLayer(EngineLayer nativeLayer) 
            : base(nativeLayer)
        {
        }
    }
}
