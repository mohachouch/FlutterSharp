namespace FlutterSharp.UI
{
    /// An opaque handle to a clip path engine layer.
    ///
    /// Instances of this class are created by [SceneBuilder.pushClipPath].
    ///
    /// {@macro dart.ui.sceneBuilder.oldLayerCompatibility}
    public class ClipPathEngineLayer : EngineLayerWrapper
    {
        public ClipPathEngineLayer(EngineLayer nativeLayer)
            : base(nativeLayer)
        {
        }
    }
}
