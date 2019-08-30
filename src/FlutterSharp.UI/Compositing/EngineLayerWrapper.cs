using System.Collections.Generic;
using System.Diagnostics;

namespace FlutterSharp.UI
{
    // Lightweight wrapper of a native layer object.
    //
    // This is used to provide a typed API for engine layers to prevent
    // incompatible layers from being passed to [SceneBuilder]'s push methods.
    // For example, this prevents a layer returned from `pushOpacity` from being
    // passed as `oldLayer` to `pushTransform`. This is achieved by having one
    // concrete subclass of this class per push method.
    public abstract class EngineLayerWrapper : EngineLayer
    {
        public EngineLayerWrapper(EngineLayer _nativeLayer)
            : base(_nativeLayer.Handle)
        {
            this._nativeLayer = _nativeLayer;
        }

        internal readonly EngineLayer _nativeLayer;

        // Children of this layer.
        //
        // Null if this layer has no children. This field is populated only in debug
        // mode.
        internal List<EngineLayerWrapper> _debugChildren;

        // Whether this layer was used as `oldLayer` in a past frame.
        //
        // It is illegal to use a layer object again after it is passed as an
        // `oldLayer` argument.
        internal bool _debugWasUsedAsOldLayer = false;

        internal bool DebugCheckNotUsedAsOldLayer()
        {
            Debug.Assert(
              !_debugWasUsedAsOldLayer,
              "Layer $runtimeType was previously used as oldLayer.\n" +
              "Once a layer is used as oldLayer, it may not be used again. Instead, " +
              "after calling one of the SceneBuilder.push* methods and passing an oldLayer " +
              "to it, use the layer returned by the method as oldLayer in subsequent " +
              "frames."
            );
            return true;
        }
    }
}
