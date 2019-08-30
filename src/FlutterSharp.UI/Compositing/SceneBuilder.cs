using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using static FlutterSharp.UI.PaintingMethods;

namespace FlutterSharp.UI
{
    /// Builds a [Scene] containing the given visuals.
    ///
    /// A [Scene] can then be rendered using [Window.render].
    ///
    /// To draw graphical operations onto a [Scene], first create a
    /// [Picture] using a [PictureRecorder] and a [Canvas], and then add
    /// it to the scene using [addPicture].
    public class SceneBuilder : NativeFieldWrapperClass2
    {
        public SceneBuilder() 
            : base(SceneBuilder_constructor())
        {
            if (this.Handle == IntPtr.Zero)
            {
                throw new InvalidOperationException("Unable to create a new SceneBuilder instance.");
            }
        }

        // Layers used in this scene.
        //
        // The key is the layer used. The value is the description of what the layer
        // is used for, e.g. "pushOpacity" or "addRetained".
        private Dictionary<EngineLayer, string> _usedLayers = new Dictionary<EngineLayer, string>();

        // In debug mode checks that the `layer` is only used once in a given scene.
        private bool _debugCheckUsedOnce(EngineLayer layer, string usage)
        {
            Debug.Assert(_debugCheckUsedOnceAssert(layer, usage));
            return true;
        }

        private bool _debugCheckUsedOnceAssert(EngineLayer layer, string usage)
        {
            if (layer == null)
            {
                return true;
            }

            Debug.Assert(
              !_usedLayers.ContainsKey(layer),
              "Layer ${layer.runtimeType} already used.\n" +
              "The layer is already being used as ${_usedLayers[layer]} in this scene.\n" +
              "A layer may only be used once in a given scene."
            );

            _usedLayers[layer] = usage;
            return true;
        }

        private bool _debugCheckCanBeUsedAsOldLayer(EngineLayerWrapper layer, string methodName)
        {
            Debug.Assert(_debugCheckCanBeUsedAsOldLayerAssert(layer, methodName));
            return true;
        }

        private bool _debugCheckCanBeUsedAsOldLayerAssert(EngineLayerWrapper layer, string methodName)
        {
            if (layer == null)
            {
                return true;
            }
            layer.DebugCheckNotUsedAsOldLayer();
            Debug.Assert(_debugCheckUsedOnce(layer, $"oldLayer in {methodName}"));
            layer._debugWasUsedAsOldLayer = true;
            return true;
        }

        private List<EngineLayerWrapper> _layerStack = new List<EngineLayerWrapper>();

        // Pushes the `newLayer` onto the `_layerStack` and adds it to the
        // `_debugChildren` of the current layer in the stack, if any.
        private bool _debugPushLayer(EngineLayerWrapper newLayer)
        {
            Debug.Assert(_debugPushLayerAssert(newLayer));
            return true;
        }

        private bool _debugPushLayerAssert(EngineLayerWrapper newLayer)
        {
            if (_layerStack.Count > 0)
            {
                EngineLayerWrapper currentLayer = _layerStack[_layerStack.Count - 1];
                if (currentLayer._debugChildren == null)
                    currentLayer._debugChildren = new List<EngineLayerWrapper>();
                currentLayer._debugChildren.Add(newLayer);
            }
            _layerStack.Add(newLayer);
            return true;
        }

        /// Pushes a transform operation onto the operation stack.
        ///
        /// The objects are transformed by the given matrix before rasterization.
        ///
        /// {@template dart.ui.sceneBuilder.oldLayer}
        /// If `oldLayer` is not null the engine will attempt to reuse the resources
        /// allocated for the old layer when rendering the new layer. This is purely
        /// an optimization. It has no effect on the correctness of rendering.
        /// {@endtemplate}
        ///
        /// {@template dart.ui.sceneBuilder.oldLayerVsRetained}
        /// Passing a layer to [addRetained] or as `oldLayer` argument to a push
        /// method counts as _usage_. A layer can be used no more than once in a scene.
        /// For example, it may not be passed simultaneously to two push methods, or
        /// to a push method and to `addRetained`.
        ///
        /// When a layer is passed to [addRetained] all descendant layers are also
        /// considered as used in this scene. The same single-usage restriction
        /// applies to descendants.
        ///
        /// When a layer is passed as an `oldLayer` argument to a push method, it may
        /// no longer be used in subsequent frames. If you would like to continue
        /// reusing the resources associated with the layer, store the layer object
        /// returned by the push method and use that in the next frame instead of the
        /// original object.
        /// {@endtemplate}
        ///
        /// See [pop] for details about the operation stack.
        public TransformEngineLayer PushTransform(Float64List matrix4, TransformEngineLayer oldLayer = null)
        {
            Debug.Assert(Matrix4IsValid(matrix4));
            Debug.Assert(_debugCheckCanBeUsedAsOldLayer(oldLayer, "pushTransform"));
            TransformEngineLayer layer = null; // TODO :native 'SceneBuilder_pushTransform' TransformEngineLayer(_pushTransform(matrix4));
            Debug.Assert(_debugPushLayer(layer));
            return layer;
        }

        /// Pushes an offset operation onto the operation stack.
        ///
        /// This is equivalent to [pushTransform] with a matrix with only translation.
        ///
        /// {@macro dart.ui.sceneBuilder.oldLayer}
        ///
        /// {@macro dart.ui.sceneBuilder.oldLayerVsRetained}
        ///
        /// See [pop] for details about the operation stack.
        public OffsetEngineLayer PushOffset(double dx, double dy, OffsetEngineLayer oldLayer = null)
        {
            Debug.Assert(_debugCheckCanBeUsedAsOldLayer(oldLayer, "pushOffset"));
            OffsetEngineLayer layer = new OffsetEngineLayer(this.PushOffset(dx, dy));
            Debug.Assert(_debugPushLayer(layer));
            return layer;
        }

        private EngineLayer PushOffset(double dx, double dy)
        {
            IntPtr engineHandle = SceneBuilder_pushOffset(this.Handle, dx, dy);
            return engineHandle != IntPtr.Zero ? new EngineLayer(engineHandle) : null;
        }

        /// Pushes a rectangular clip operation onto the operation stack.
        ///
        /// Rasterization outside the given rectangle is discarded.
        ///
        /// {@macro dart.ui.sceneBuilder.oldLayer}
        ///
        /// {@macro dart.ui.sceneBuilder.oldLayerVsRetained}
        ///
        /// See [pop] for details about the operation stack, and [Clip] for different clip modes.
        /// By default, the clip will be anti-aliased (clip = [Clip.antiAlias]).
        public ClipRectEngineLayer PushClipRect(Rect rect, Clip clipBehavior = Clip.AntiAlias, ClipRectEngineLayer oldLayer = null)
        {
            Debug.Assert(clipBehavior != Clip.None);

            Debug.Assert(_debugCheckCanBeUsedAsOldLayer(oldLayer, "pushClipRect"));
            ClipRectEngineLayer layer = new ClipRectEngineLayer(this.PushClipRect(rect.Left, rect.Right, rect.Top, rect.Bottom, (int)clipBehavior));
            Debug.Assert(_debugPushLayer(layer));
            return layer;
        }

        private EngineLayer PushClipRect(double left,
                            double right,
                            double top,
                            double bottom,
                            int clipBehavior)
        {
            
            IntPtr engineHandle = SceneBuilder_pushClipRect(this.Handle, left, right, top, bottom, clipBehavior);
            return engineHandle != IntPtr.Zero ? new EngineLayer(engineHandle) : null;
        }

        /// Pushes a rounded-rectangular clip operation onto the operation stack.
        ///
        /// Rasterization outside the given rounded rectangle is discarded.
        ///
        /// {@macro dart.ui.sceneBuilder.oldLayer}
        ///
        /// {@macro dart.ui.sceneBuilder.oldLayerVsRetained}
        ///
        /// See [pop] for details about the operation stack, and [Clip] for different clip modes.
        /// By default, the clip will be anti-aliased (clip = [Clip.antiAlias]).
        public ClipRRectEngineLayer PushClipRRect(RRect rrect, Clip clipBehavior = Clip.AntiAlias, ClipRRectEngineLayer oldLayer = null)
        {
            Debug.Assert(clipBehavior != Clip.None);
            Debug.Assert(_debugCheckCanBeUsedAsOldLayer(oldLayer, "pushClipRRect"));
            ClipRRectEngineLayer layer = new ClipRRectEngineLayer(PushClipRRect(rrect._value32, (int)clipBehavior));
            Debug.Assert(_debugPushLayer(layer));
            return layer;
        }

        private EngineLayer PushClipRRect(Float32List rrect, int clipBehavior)
        {
            // TODO : native 'SceneBuilder_pushClipRRect';
            return null;
        }

        /// Pushes a path clip operation onto the operation stack.
        ///
        /// Rasterization outside the given path is discarded.
        ///
        /// {@macro dart.ui.sceneBuilder.oldLayer}
        ///
        /// {@macro dart.ui.sceneBuilder.oldLayerVsRetained}
        ///
        /// See [pop] for details about the operation stack. See [Clip] for different clip modes.
        /// By default, the clip will be anti-aliased (clip = [Clip.antiAlias]).
        public ClipPathEngineLayer PushClipPath(Path path, Clip clipBehavior = Clip.AntiAlias, ClipPathEngineLayer oldLayer = null)
        {
            Debug.Assert(clipBehavior != Clip.None);
            Debug.Assert(_debugCheckCanBeUsedAsOldLayer(oldLayer, "pushClipPath"));
            ClipPathEngineLayer layer = new ClipPathEngineLayer(PushClipPath(path, (int)clipBehavior));
            Debug.Assert(_debugPushLayer(layer));
            return layer;
        }

        private EngineLayer PushClipPath(Path path, int clipBehavior)
        {
            IntPtr engineHandle = SceneBuilder_pushClipPath(this.Handle, path.Handle, clipBehavior);
            return engineHandle != IntPtr.Zero ? new EngineLayer(engineHandle) : null;
        }

        /// Pushes an opacity operation onto the operation stack.
        ///
        /// The given alpha value is blended into the alpha value of the objects'
        /// rasterization. An alpha value of 0 makes the objects entirely invisible.
        /// An alpha value of 255 has no effect (i.e., the objects retain the current
        /// opacity).
        ///
        /// {@macro dart.ui.sceneBuilder.oldLayer}
        ///
        /// {@macro dart.ui.sceneBuilder.oldLayerVsRetained}
        ///
        /// See [pop] for details about the operation stack.
        public OpacityEngineLayer PushOpacity(int alpha, Offset offset = null, OpacityEngineLayer oldLayer = null)
        {
            if (offset == null)
                offset = Offset.Zero;

            Debug.Assert(_debugCheckCanBeUsedAsOldLayer(oldLayer, "pushOpacity"));
            OpacityEngineLayer layer = new OpacityEngineLayer(PushOpacity(alpha, offset.Dx, offset.Dy));
            Debug.Assert(_debugPushLayer(layer));
            return layer;
        }

        private EngineLayer PushOpacity(int alpha, double dx, double dy)
        {
            // TODO : native 'SceneBuilder_pushOpacity';
            return null;
        }

        /// Pushes a color filter operation onto the operation stack.
        ///
        /// The given color is applied to the objects' rasterization using the given
        /// blend mode.
        ///
        /// {@macro dart.ui.sceneBuilder.oldLayer}
        ///
        /// {@macro dart.ui.sceneBuilder.oldLayerVsRetained}
        ///
        /// See [pop] for details about the operation stack.
        public ColorFilterEngineLayer PushColorFilter(Color color, BlendMode blendMode, ColorFilterEngineLayer oldLayer = null)
        {
            Debug.Assert(_debugCheckCanBeUsedAsOldLayer(oldLayer, "pushColorFilter"));
            ColorFilterEngineLayer layer = new ColorFilterEngineLayer(PushColorFilter(color.Value, (int)blendMode));
            Debug.Assert(_debugPushLayer(layer));
            return layer;
        }

        private EngineLayer PushColorFilter(int color, int blendMode)
        {
            // TODO : native 'SceneBuilder_pushColorFilter';
            return null;
        }

        /// Pushes a backdrop filter operation onto the operation stack.
        ///
        /// The given filter is applied to the current contents of the scene prior to
        /// rasterizing the given objects.
        ///
        /// {@macro dart.ui.sceneBuilder.oldLayer}
        ///
        /// {@macro dart.ui.sceneBuilder.oldLayerVsRetained}
        ///
        /// See [pop] for details about the operation stack.
        public BackdropFilterEngineLayer PushBackdropFilter(ImageFilter filter, BackdropFilterEngineLayer oldLayer = null)
        {
            Debug.Assert(_debugCheckCanBeUsedAsOldLayer(oldLayer, "pushBackdropFilter"));
            BackdropFilterEngineLayer layer = null; // TODO :  native 'SceneBuilder_pushBackdropFilter'; new BackdropFilterEngineLayer(_pushBackdropFilter(filter));
            Debug.Assert(_debugPushLayer(layer));
            return layer;
        }

        /// Pushes a shader mask operation onto the operation stack.
        ///
        /// The given shader is applied to the object's rasterization in the given
        /// rectangle using the given blend mode.
        ///
        /// {@macro dart.ui.sceneBuilder.oldLayer}
        ///
        /// {@macro dart.ui.sceneBuilder.oldLayerVsRetained}
        ///
        /// See [pop] for details about the operation stack.
        public ShaderMaskEngineLayer PushShaderMask(Shader shader, Rect maskRect, BlendMode blendMode, ShaderMaskEngineLayer oldLayer = null)
        {
            Debug.Assert(_debugCheckCanBeUsedAsOldLayer(oldLayer, "pushShaderMask"));
            ShaderMaskEngineLayer layer = new ShaderMaskEngineLayer(PushShaderMask(shader,
                                   maskRect.Left,
                                   maskRect.Right,
                                   maskRect.Top,
                                   maskRect.Bottom,
                                   (int)blendMode));
            Debug.Assert(_debugPushLayer(layer));
            return layer;
        }

        private EngineLayer PushShaderMask(Shader shader,
                             double maskRectLeft,
                             double maskRectRight,
                             double maskRectTop,
                             double maskRectBottom,
                             int blendMode)
        {
            // TODO : native 'SceneBuilder_pushShaderMask';
            return null;
        }

        /// Pushes a physical layer operation for an arbitrary shape onto the
        /// operation stack.
        ///
        /// By default, the layer's content will not be clipped (clip = [Clip.none]).
        /// If clip equals [Clip.hardEdge], [Clip.antiAlias], or [Clip.antiAliasWithSaveLayer],
        /// then the content is clipped to the given shape defined by [path].
        ///
        /// If [elevation] is greater than 0.0, then a shadow is drawn around the layer.
        /// [shadowColor] defines the color of the shadow if present and [color] defines the
        /// color of the layer background.
        ///
        /// {@macro dart.ui.sceneBuilder.oldLayer}
        ///
        /// {@macro dart.ui.sceneBuilder.oldLayerVsRetained}
        ///
        /// See [pop] for details about the operation stack, and [Clip] for different clip modes.
        // ignore: deprecated_member_use
        public PhysicalShapeEngineLayer PushPhysicalShape(Path path, double elevation, Color color, Color shadowColor, Clip clipBehavior = Clip.None, PhysicalShapeEngineLayer oldLayer = null)
        {
            Debug.Assert(_debugCheckCanBeUsedAsOldLayer(oldLayer, "pushPhysicalShape"));
            PhysicalShapeEngineLayer layer = null; // new PhysicalShapeEngineLayer(_pushPhysicalShape(path, elevation, color.Value, shadowColor?.Value ?? 0xFF000000, (int)clipBehavior));
            Debug.Assert(_debugPushLayer(layer));
            return layer;
        }

        private EngineLayer PushPhysicalShape(Path path, double elevation, int color, int shadowColor, int clipBehavior)
        {
            // TODO : native 'SceneBuilder_pushPhysicalShape';
            return null;
        } 
        
        /// Ends the effect of the most recently pushed operation.
        ///
        /// Internally the scene builder maintains a stack of operations. Each of the
        /// operations in the stack applies to each of the objects added to the scene.
        /// Calling this function removes the most recently added operation from the
        /// stack.
        public void Pop()
        {
            if (_layerStack.Count > 0)
            {
                _layerStack.RemoveAt(_layerStack.Count - 1);
            }

            SceneBuilder_pop(this.Handle);
        }

        /// Add a retained engine layer subtree from previous frames.
        ///
        /// All the engine layers that are in the subtree of the retained layer will
        /// be automatically appended to the current engine layer tree.
        ///
        /// Therefore, when implementing a subclass of the [Layer] concept defined in
        /// the rendering layer of Flutter's framework, once this is called, there's
        /// no need to call [addToScene] for its children layers.
        ///
        /// {@macro dart.ui.sceneBuilder.oldLayerVsRetained}
        public void AddRetained(EngineLayer retainedLayer)
        {
            Debug.Assert(retainedLayer is EngineLayerWrapper);
            Debug.Assert(_addRetainedAssert(retainedLayer));
            EngineLayerWrapper wrapper = retainedLayer as EngineLayerWrapper;
            AddRetained(wrapper._nativeLayer.Handle);
        }

        private void AddRetained(IntPtr pRetainedLayer)
        {
            SceneBuilder_addRetained(this.Handle, pRetainedLayer);
        }

        private bool _addRetainedAssert(EngineLayer retainedLayer)
        {
            EngineLayerWrapper layer = retainedLayer as EngineLayerWrapper;

            void recursivelyCheckChildrenUsedOnce(EngineLayerWrapper parentLayer)
            {
                _debugCheckUsedOnce(parentLayer, "retained layer");
                parentLayer.DebugCheckNotUsedAsOldLayer();

                if (parentLayer._debugChildren == null || parentLayer._debugChildren.Count == 0)
                {
                    return;
                }

                parentLayer._debugChildren.ForEach(x => recursivelyCheckChildrenUsedOnce(x));
            }

            recursivelyCheckChildrenUsedOnce(layer);

            return true;
        }
        
        /// Adds an object to the scene that displays performance statistics.
        ///
        /// Useful during development to assess the performance of the application.
        /// The enabledOptions controls which statistics are displayed. The bounds
        /// controls where the statistics are displayed.
        ///
        /// enabledOptions is a bit field with the following bits defined:
        ///  - 0x01: displayRasterizerStatistics - show GPU thread frame time
        ///  - 0x02: visualizeRasterizerStatistics - graph GPU thread frame times
        ///  - 0x04: displayEngineStatistics - show UI thread frame time
        ///  - 0x08: visualizeEngineStatistics - graph UI thread frame times
        /// Set enabledOptions to 0x0F to enable all the currently defined features.
        ///
        /// The "UI thread" is the thread that includes all the execution of
        /// the main Dart isolate (the isolate that can call
        /// [Window.render]). The UI thread frame time is the total time
        /// spent executing the [Window.onBeginFrame] callback. The "GPU
        /// thread" is the thread (running on the CPU) that subsequently
        /// processes the [Scene] provided by the Dart code to turn it into
        /// GPU commands and send it to the GPU.
        ///
        /// See also the [PerformanceOverlayOption] enum in the rendering library.
        /// for more details.
        // Values above must match constants in //engine/src/sky/compositor/performance_overlay_layer.h
        public void AddPerformanceOverlay(int enabledOptions, Rect bounds)
        {
            AddPerformanceOverlay(enabledOptions, bounds.Left, bounds.Right, bounds.Top, bounds.Bottom);
        }

        private void AddPerformanceOverlay(int enabledOptions, double left, double right, double top, double bottom)
        {
            SceneBuilder_addPerformanceOverlay(this.Handle, enabledOptions, left, right, top, bottom);
        }

        /// Adds a [Picture] to the scene.
        ///
        /// The picture is rasterized at the given offset.
        public void AddPicture(Offset offset, Picture picture, bool isComplexHint = false, bool willChangeHint = false)
        {
            int hints = 0;
            if (isComplexHint)
                hints |= 1;
            if (willChangeHint)
                hints |= 2;

            SceneBuilder_addPicture(this.Handle, offset.Dx, offset.Dy, picture.Handle, hints);
        }

        /// Adds a backend texture to the scene.
        ///
        /// The texture is scaled to the given size and rasterized at the given offset.
        ///
        /// If `freeze` is true the texture that is added to the scene will not
        /// be updated with new frames. `freeze` is used when resizing an embedded
        /// Android view: When resizing an Android view there is a short period during
        /// which the framework cannot tell if the newest texture frame has the
        /// previous or new size, to workaround this the framework "freezes" the
        /// texture just before resizing the Android view and un-freezes it when it is
        /// certain that a frame with the new size is ready.
        public void AddTexture(int textureId, Offset offset = null, double width = 0.0, double height = 0.0, bool freeze = false)
        {
            if (offset == null)
                offset = Offset.Zero;

            AddTexture(offset.Dx, offset.Dy, width, height, textureId, freeze);
        }

        private void AddTexture(double dx, double dy, double width, double height, int textureId, bool freeze)
        {
            SceneBuilder_addTexture(this.Handle, dx, dy, width, height, textureId, freeze);
        }

        /// Adds a platform view (e.g an iOS UIView) to the scene.
        ///
        /// Only supported on iOS, this is currently a no-op on other platforms.
        ///
        /// On iOS this layer splits the current output surface into two surfaces, one for the scene nodes
        /// preceding the platform view, and one for the scene nodes following the platform view.
        ///
        /// ## Performance impact
        ///
        /// Adding an additional surface doubles the amount of graphics memory directly used by Flutter
        /// for output buffers. Quartz might allocated extra buffers for compositing the Flutter surfaces
        /// and the platform view.
        ///
        /// With a platform view in the scene, Quartz has to composite the two Flutter surfaces and the
        /// embedded UIView. In addition to that, on iOS versions greater than 9, the Flutter frames are
        /// synchronized with the UIView frames adding additional performance overhead.
        public void AddPlatformView(int viewId, Offset offset = null, double width = 0.0, double height = 0.0)
        {
            if (offset == null)
                offset = Offset.Zero;

            AddPlatformView(offset.Dx, offset.Dy, width, height, viewId);
        }

        private void AddPlatformView(double dx, double dy, double width, double height, int viewId)
        {
            SceneBuilder_addPlatformView(this.Handle, dx, dy, width, height, viewId);
        }

        /// (Fuchsia-only) Adds a scene rendered by another application to the scene
        /// for this application.
        public void AddChildScene(Offset offset = null, double width = 0.0, double height = 0.0, SceneHost sceneHost = null, bool hitTestable = true)
        {
            if (offset == null)
                offset = Offset.Zero;

            /* TODO: _addChildScene(offset.dx,
                 offset.dy,
                 width,
                 height,
                 sceneHost,
                 hitTestable);
                 void _addChildScene(double dx,
             double dy,
             double width,
             double height,
             SceneHost sceneHost,
             bool hitTestable) native 'SceneBuilder_addChildScene'; */
        }

        /// Sets a threshold after which additional debugging information should be recorded.
        ///
        /// Currently this interface is difficult to use by end-developers. If you're
        /// interested in using this feature, please contact [flutter-dev](https://groups.google.com/forum/#!forum/flutter-dev).
        /// We'll hopefully be able to figure out how to make this feature more useful
        /// to you.
        public void SetRasterizerTracingThreshold(int frameInterval)
        {
            SceneBuilder_setRasterizerTracingThreshold(this.Handle, frameInterval);
        }

        /// Sets whether the raster cache should checkerboard cached entries. This is
        /// only useful for debugging purposes.
        ///
        /// The compositor can sometimes decide to cache certain portions of the
        /// widget hierarchy. Such portions typically don't change often from frame to
        /// frame and are expensive to render. This can speed up overall rendering. However,
        /// there is certain upfront cost to constructing these cache entries. And, if
        /// the cache entries are not used very often, this cost may not be worth the
        /// speedup in rendering of subsequent frames. If the developer wants to be certain
        /// that populating the raster cache is not causing stutters, this option can be
        /// set. Depending on the observations made, hints can be provided to the compositor
        /// that aid it in making better decisions about caching.
        ///
        /// Currently this interface is difficult to use by end-developers. If you're
        /// interested in using this feature, please contact [flutter-dev](https://groups.google.com/forum/#!forum/flutter-dev).
        public void SetCheckerboardRasterCacheImages(bool checkerboard)
        {
            SceneBuilder_setCheckerboardRasterCacheImages(this.Handle, checkerboard);
        }

        /// Sets whether the compositor should checkerboard layers that are rendered
        /// to offscreen bitmaps.
        ///
        /// This is only useful for debugging purposes.
        public void SetCheckerboardOffscreenLayers(bool checkerboard)
        {
            SceneBuilder_setCheckerboardOffscreenLayers(this.Handle, checkerboard);
        }

        /// Finishes building the scene.
        ///
        /// Returns a [Scene] containing the objects that have been added to
        /// this scene builder. The [Scene] can then be displayed on the
        /// screen with [Window.render].
        ///
        /// After calling this function, the scene builder object is invalid and
        /// cannot be used further.
        public Scene Build()
        {
            IntPtr sceneHandle = SceneBuilder_build(this.Handle);
            return sceneHandle != IntPtr.Zero ? new Scene(sceneHandle) : null;
        }

        [DllImport("libflutter")]
        private extern static IntPtr SceneBuilder_constructor();

        [DllImport("libflutter")]
        private extern static IntPtr SceneBuilder_pushOffset(IntPtr pSceneBuilder, double dx, double dy);

        [DllImport("libflutter")]
        private extern static IntPtr SceneBuilder_pushClipRect(IntPtr pSceneBuilder,
            double left,
            double right,
            double top,
            double bottom,
            int clipBehavior);

        [DllImport("libflutter")]
        private extern static IntPtr SceneBuilder_pushClipPath(IntPtr pSceneBuilder, IntPtr pPath, int clipBehavior);

        [DllImport("libflutter")]
        private extern static void SceneBuilder_addPerformanceOverlay(IntPtr pSceneBuilder,
            int enabledOptions,
            double left, 
            double right,
            double top, 
            double bottom);

        [DllImport("libflutter")]
        private extern static void SceneBuilder_pop(IntPtr pSceneBuilder);

        [DllImport("libflutter")]
        private extern static void SceneBuilder_addRetained(IntPtr pSceneBuilder, IntPtr pRetainedLayer);

        [DllImport("libflutter")]
        private extern static void SceneBuilder_addPicture(IntPtr pSceneBuilder, double dx, double dy, IntPtr picture, int hints);

        [DllImport("libflutter")]
        private extern static void SceneBuilder_addTexture(IntPtr pSceneBuilder, double dx, double dy, double width, double height,
            int textureId, bool freeze);

        [DllImport("libflutter")]
        private extern static void SceneBuilder_addPlatformView(IntPtr pSceneBuilder, double dx, double dy, double width, 
            double height, int viewId);

        [DllImport("libflutter")]
        private extern static void SceneBuilder_setRasterizerTracingThreshold(IntPtr pSceneBuilder, int frameInterval);

        [DllImport("libflutter")]
        private extern static void SceneBuilder_setCheckerboardRasterCacheImages(IntPtr pSceneBuilder, bool checkerboard);

        [DllImport("libflutter")]
        private extern static void SceneBuilder_setCheckerboardOffscreenLayers(IntPtr pSceneBuilder, bool checkerboard);

        [DllImport("libflutter")]
        private extern static IntPtr SceneBuilder_build(IntPtr pSceneBuilder);
    }
}