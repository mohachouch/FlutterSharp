namespace FlutterSharp.UI
{
    /// An opaque handle to a color filter engine layer.
    ///
    /// Instances of this class are created by [SceneBuilder.pushColorFilter].
    ///
    /// {@macro dart.ui.sceneBuilder.oldLayerCompatibility}
    public class ColorFilterEngineLayer : EngineLayerWrapper
    {
        public ColorFilterEngineLayer(EngineLayer nativeLayer) 
            : base(nativeLayer)
        {
        }
    }
}