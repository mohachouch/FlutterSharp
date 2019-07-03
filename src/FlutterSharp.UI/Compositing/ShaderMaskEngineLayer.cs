namespace FlutterSharp.UI
{
    /// An opaque handle to a shader mask engine layer.
    ///
    /// Instances of this class are created by [SceneBuilder.pushShaderMask].
    ///
    /// {@macro dart.ui.sceneBuilder.oldLayerCompatibility}
    public class ShaderMaskEngineLayer : EngineLayerWrapper
    {
        public ShaderMaskEngineLayer(EngineLayer nativeLayer) 
            : base(nativeLayer)
        {
        }
    }
}
