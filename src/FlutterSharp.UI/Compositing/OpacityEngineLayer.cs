namespace FlutterSharp.UI
{
    /// An opaque handle to an opacity engine layer.
    ///
    /// Instances of this class are created by [SceneBuilder.pushOpacity].
    ///
    /// {@macro dart.ui.sceneBuilder.oldLayerCompatibility}
    public class OpacityEngineLayer : EngineLayerWrapper
    {
        public OpacityEngineLayer(EngineLayer nativeLayer)
            : base(nativeLayer)
        {
        }
    }
}