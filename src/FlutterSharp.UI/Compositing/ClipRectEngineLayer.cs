namespace FlutterSharp.UI
{
    /// An opaque handle to a clip rect engine layer.
    ///
    /// Instances of this class are created by [SceneBuilder.pushClipRect].
    ///
    /// {@macro dart.ui.sceneBuilder.oldLayerCompatibility}
    public class ClipRectEngineLayer : EngineLayerWrapper
    {
        public ClipRectEngineLayer(EngineLayer nativeLayer) 
            : base(nativeLayer)
        {

        }
    }
}
