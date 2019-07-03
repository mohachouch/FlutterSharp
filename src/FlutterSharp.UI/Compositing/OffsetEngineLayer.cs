namespace FlutterSharp.UI
{
    /// An opaque handle to an offset engine layer.
    ///
    /// Instances of this class are created by [SceneBuilder.pushOffset].
    ///
    /// {@macro dart.ui.sceneBuilder.oldLayerCompatibility}
    public class OffsetEngineLayer : EngineLayerWrapper
    {
        public OffsetEngineLayer(EngineLayer nativeLayer) 
            : base(nativeLayer)
        {

        }
    }
}
