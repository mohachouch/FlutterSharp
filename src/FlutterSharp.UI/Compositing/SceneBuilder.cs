using System.Diagnostics;

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
        {
            // TODO : native 'SceneBuilder_constructor';
        }

        /// Pushes a transform operation onto the operation stack.
        ///
        /// The objects are transformed by the given matrix before rasterization.
        ///
        /// See [pop] for details about the operation stack.
        public EngineLayer PushTransform(Float64List matrix4)
        {
            Debug.Assert(_matrix4IsValid(matrix4));

            // TODO :native 'SceneBuilder_pushTransform'
            return null;
        }

        /// Pushes an offset operation onto the operation stack.
        ///
        /// This is equivalent to [pushTransform] with a matrix with only translation.
        ///
        /// See [pop] for details about the operation stack.
        public EngineLayer PushOffset(double dx, double dy)
        {
            // TODO : native 'SceneBuilder_pushOffset';
            return null;
        }

        /// Pushes a rectangular clip operation onto the operation stack.
        ///
        /// Rasterization outside the given rectangle is discarded.
        ///
        /// See [pop] for details about the operation stack, and [Clip] for different clip modes.
        /// By default, the clip will be anti-aliased (clip = [Clip.antiAlias]).
        public EngineLayer PushClipRect(Rect rect, Clip clipBehavior = Clip.AntiAlias)
        {
            Debug.Assert(clipBehavior != Clip.None);
            /*  TODO : return _pushClipRect(rect.left, rect.right, rect.top, rect.bottom, clipBehavior.index);
              EngineLayer _pushClipRect(double left,
                  double right,
                  double top,
                  double bottom,
                  int clipBehavior) native 'SceneBuilder_pushClipRect';*/

            return null;
        }

        /// Pushes a rounded-rectangular clip operation onto the operation stack.
        ///
        /// Rasterization outside the given rounded rectangle is discarded.
        ///
        /// See [pop] for details about the operation stack, and [Clip] for different clip modes.
        /// By default, the clip will be anti-aliased (clip = [Clip.antiAlias]).
        public EngineLayer PushClipRRect(RRect rrect, Clip clipBehavior = Clip.AntiAlias)
        {
            Debug.Assert(clipBehavior != Clip.None);

            /*TODO : return _pushClipRRect(rrect._value32, clipBehavior.index);
               EngineLayer _pushClipRRect(Float32List rrect, int clipBehavior) native 'SceneBuilder_pushClipRRect';*/

            return null;
        }

        /// Pushes a path clip operation onto the operation stack.
        ///
        /// Rasterization outside the given path is discarded.
        ///
        /// See [pop] for details about the operation stack. See [Clip] for different clip modes.
        /// By default, the clip will be anti-aliased (clip = [Clip.antiAlias]).
        public EngineLayer PushClipPath(Path path, Clip clipBehavior = Clip.AntiAlias)
        {
            Debug.Assert(clipBehavior != Clip.None);

            /*TODO : return _pushClipPath(path, clipBehavior.index);
               EngineLayer _pushClipPath(Path path, int clipBehavior) native 'SceneBuilder_pushClipPath';*/

            return null;
        }

        /// Pushes an opacity operation onto the operation stack.
        ///
        /// The given alpha value is blended into the alpha value of the objects'
        /// rasterization. An alpha value of 0 makes the objects entirely invisible.
        /// An alpha value of 255 has no effect (i.e., the objects retain the current
        /// opacity).
        ///
        /// See [pop] for details about the operation stack.
        public EngineLayer PushOpacity(int alpha, Offset offset = null)
        {
            if (offset == null)
                offset = Offset.Zero;

            /*TODO : return _pushOpacity(alpha, offset.dx, offset.dy);
              EngineLayer _pushOpacity(int alpha, double dx, double dy) native 'SceneBuilder_pushOpacity';
             */
            return null;
        }

        /// Pushes a color filter operation onto the operation stack.
        ///
        /// The given color is applied to the objects' rasterization using the given
        /// blend mode.
        ///
        /// See [pop] for details about the operation stack.
        public EngineLayer PushColorFilter(Color color, BlendMode blendMode)
        {
            /*return _pushColorFilter(color.Value, blendMode.index);
        EngineLayer _pushColorFilter(int color, int blendMode) native 'SceneBuilder_pushColorFilter';
             */

            return null;
        }

        /// Pushes a backdrop filter operation onto the operation stack.
        ///
        /// The given filter is applied to the current contents of the scene prior to
        /// rasterizing the given objects.
        ///
        /// See [pop] for details about the operation stack.
        public EngineLayer PushBackdropFilter(ImageFilter filter)
        {
            // TODO :  native 'SceneBuilder_pushBackdropFilter';
            return null;
        }


        /// Pushes a shader mask operation onto the operation stack.
        ///
        /// The given shader is applied to the object's rasterization in the given
        /// rectangle using the given blend mode.
        ///
        /// See [pop] for details about the operation stack.
        public EngineLayer PushShaderMask(Shader shader, Rect maskRect, BlendMode blendMode)
        {
            /*TODO : return _pushShaderMask(shader,
                maskRect.left,
                maskRect.right,
                maskRect.top,
                maskRect.bottom,
                blendMode.index);
                 EngineLayer _pushShaderMask(Shader shader,
            double maskRectLeft,
            double maskRectRight,
            double maskRectTop,
            double maskRectBottom,
            int blendMode) native 'SceneBuilder_pushShaderMask';
             */
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
        /// See [pop] for details about the operation stack, and [Clip] for different clip modes.
        // ignore: deprecated_member_use
        public EngineLayer PushPhysicalShape(Path path, double elevation, Color color, Color shadowColor, Clip clipBehavior = Clip.None)
        {
            /*TODO : return _pushPhysicalShape(path, elevation, color.value, shadowColor?.value ?? 0xFF000000, clipBehavior.index);
               EngineLayer _pushPhysicalShape(Path path, double elevation, int color, int shadowColor, int clipBehavior) native
        'SceneBuilder_pushPhysicalShape';
         */
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
            // TODO :  native 'SceneBuilder_pop';
        }

        /// Add a retained engine layer subtree from previous frames.
        ///
        /// All the engine layers that are in the subtree of the retained layer will
        /// be automatically appended to the current engine layer tree.
        ///
        /// Therefore, when implementing a subclass of the [Layer] concept defined in
        /// the rendering layer of Flutter's framework, once this is called, there's
        /// no need to call [addToScene] for its children layers.
        public void AddRetained(EngineLayer retainedLayer)
        {
            // TODO :  native 'SceneBuilder_addRetained';
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
            /*TODO:_addPerformanceOverlay(enabledOptions,
                bounds.left,
                bounds.right,
                bounds.top,
                bounds.bottom);
                 void _addPerformanceOverlay(int enabledOptions,
            double left,
            double right,
            double top,
            double bottom) native 'SceneBuilder_addPerformanceOverlay';*/
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

            /*TODO : _addPicture(offset.dx, offset.dy, picture, hints);
            void _addPicture(double dx, double dy, Picture picture, int hints) native 'SceneBuilder_addPicture';*/
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

            /*TODO :_addTexture(offset.dx, offset.dy, width, height, textureId, freeze);
             void _addTexture(double dx, double dy, double width, double height, int textureId, bool freeze) native 'SceneBuilder_addTexture';
             */
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

            /*TODO: _addPlatformView(offset.dx, offset.dy, width, height, viewId);
              void _addPlatformView(double dx, double dy, double width, double height, int viewId) native 'SceneBuilder_addPlatformView';
             */
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
            // TODO :  native 'SceneBuilder_setRasterizerTracingThreshold';
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
            // TODO : native 'SceneBuilder_setCheckerboardRasterCacheImages';
        }

        /// Sets whether the compositor should checkerboard layers that are rendered
        /// to offscreen bitmaps.
        ///
        /// This is only useful for debugging purposes.
        public void SetCheckerboardOffscreenLayers(bool checkerboard)
        {
            // TODO : native 'SceneBuilder_setCheckerboardOffscreenLayers';
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
            // TODO : native 'SceneBuilder_build';
            return null;
        }

        // TODO : A NETTOYER
        private bool _matrix4IsValid(Float64List matrix4)
        {
            return true;
        }
    }

    public class Path
    {

    }

    public class ImageFilter
    {

    }

    public class Shader
    {

    }

    public class Picture
    {

    }
    
    public enum Clip
    {
        None,
        AntiAlias
    }
}