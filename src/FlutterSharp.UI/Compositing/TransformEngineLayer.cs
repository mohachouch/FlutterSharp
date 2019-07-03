namespace FlutterSharp.UI
{
    /// An opaque handle to a transform engine layer.
    ///
    /// Instances of this class are created by [SceneBuilder.pushTransform].
    ///
    /// {@template dart.ui.sceneBuilder.oldLayerCompatibility}
    /// `oldLayer` parameter in [SceneBuilder] methods only accepts objects created
    /// by the engine. [SceneBuilder] will throw an [AssertionError] if you pass it
    /// a custom implementation of this class.
    /// {@endtemplate}
    public class TransformEngineLayer : EngineLayerWrapper
    {
        public TransformEngineLayer(EngineLayer nativeLayer) 
            : base(nativeLayer)
        {

        }
    }
}
