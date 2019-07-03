namespace FlutterSharp.UI
{
    /// An opaque handle to a physical shape engine layer.
    ///
    /// Instances of this class are created by [SceneBuilder.pushPhysicalShape].
    ///
    /// {@macro dart.ui.sceneBuilder.oldLayerCompatibility}
    public class PhysicalShapeEngineLayer : EngineLayerWrapper
    {
        public PhysicalShapeEngineLayer(EngineLayer nativeLayer)
            : base(nativeLayer)
        {
        }
    }
}
