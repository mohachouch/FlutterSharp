namespace FlutterSharp.UI
{
    /// An opaque handle to a backdrop filter engine layer.
    ///
    /// Instances of this class are created by [SceneBuilder.pushBackdropFilter].
    ///
    /// {@macro dart.ui.sceneBuilder.oldLayerCompatibility}
    public class BackdropFilterEngineLayer : EngineLayerWrapper
    {
        public BackdropFilterEngineLayer(EngineLayer nativeLayer) 
            : base(nativeLayer)
        {
        }
    }
}
