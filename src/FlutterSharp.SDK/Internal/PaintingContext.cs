using FlutterSharp.SDK.Internal;
using FlutterSharp.UI;

namespace FlutterSharp.SDK.Internal
{
    /// Signature for painting into a [PaintingContext].
    ///
    /// The `offset` argument is the offset from the origin of the coordinate system
    /// of the [PaintingContext.canvas] to the coordinate system of the callee.
    ///
    /// Used by many of the methods of [PaintingContext].
    public delegate void PaintingContextCallback(PaintingContext context, Offset offset);

    /// A place to paint.
    ///
    /// Rather than holding a canvas directly, [RenderObject]s paint using a painting
    /// context. The painting context has a [Canvas], which receives the
    /// individual draw operations, and also has functions for painting child
    /// render objects.
    ///
    /// When painting a child render object, the canvas held by the painting context
    /// can change because the draw operations issued before and after painting the
    /// child might be recorded in separate compositing layers. For this reason, do
    /// not hold a reference to the canvas across operations that might paint
    /// child render objects.
    ///
    /// New [PaintingContext] objects are created automatically when using
    /// [PaintingContext.repaintCompositedChild] and [pushLayer].
    public class PaintingContext : ClipContext
    {
        /// Creates a painting context.
        ///
        /// Typically only called by [PaintingContext.repaintCompositedChild]
        /// and [pushLayer].
        public PaintingContext(ContainerLayer _containerLayer, Rect estimatedBounds)
        {
            this._containerLayer = _containerLayer;
            this.EstimatedBounds = estimatedBounds;
        }

        private readonly ContainerLayer _containerLayer;

        /// An estimate of the bounds within which the painting context's [canvas]
        /// will record painting commands. This can be useful for debugging.
        ///
        /// The canvas will allow painting outside these bounds.
        ///
        /// The [estimatedBounds] rectangle is in the [canvas] coordinate system.
        public readonly Rect EstimatedBounds;

        /// Repaint the given render object.
        ///
        /// The render object must be attached to a [PipelineOwner], must have a
        /// composited layer, and must be in need of painting. The render object's
        /// layer, if any, is re-used, along with any layers in the subtree that don't
        /// need to be repainted.
        ///
        /// See also:
        ///
        ///  * [RenderObject.isRepaintBoundary], which determines if a [RenderObject]
        ///    has a composited layer.
        public static void RepaintCompositedChild(RenderObject child, bool debugAlsoPaintedParent = false)
        {
            _repaintCompositedChild(child, debugAlsoPaintedParent: debugAlsoPaintedParent);
        }

        public static void _repaintCompositedChild(RenderObject child, bool debugAlsoPaintedParent = false, PaintingContext childContext = null)
        {
            if (!(child._layer is OffsetLayer childLayer))
            {
                // Not using the `layer` setter because the setter asserts that we not
                // replace the layer for repaint boundaries. That assertion does not
                // apply here because this is exactly the place designed to create a
                // layer for repaint boundaries.
                child._layer = new OffsetLayer();
            }
            else
            {
                childLayer.RemoveAllChildren();
            }

            if (childContext == null)
                childContext = new PaintingContext(child._layer as ContainerLayer, child.PaintBounds);


            child._paintWithContext(childContext, Offset.Zero);

            // Double-check that the paint method did not replace the layer (the first
            // check is done in the [layer] setter itself).
            childContext.StopRecordingIfNeeded();
        }

        /// Paint a child [RenderObject].
        ///
        /// If the child has its own composited layer, the child will be composited
        /// into the layer subtree associated with this painting context. Otherwise,
        /// the child will be painted into the current PictureLayer for this context.
        public void PaintChild(RenderObject child, Offset offset)
        {
            if (child.IsRepaintBoundary)
            {
                StopRecordingIfNeeded();
                _compositeChild(child, offset);
            }
            else
            {
                child._paintWithContext(this, offset);
            }
        }

        private void _compositeChild(RenderObject child, Offset offset)
        {
            // Create a layer for our child, and paint the child into it.
            if (child._needsPaint)
            {
                RepaintCompositedChild(child, debugAlsoPaintedParent: true);
            }

            OffsetLayer childOffsetLayer = child._layer as OffsetLayer;
            childOffsetLayer.Offset = offset;
            AppendLayer(child._layer);
        }

        /// Adds a layer to the recording requiring that the recording is already
        /// stopped.
        ///
        /// Do not call this function directly: call [addLayer] or [pushLayer]
        /// instead. This function is called internally when all layers not
        /// generated from the [canvas] are added.
        ///
        /// Subclasses that need to customize how layers are added should override
        /// this method.
        public void AppendLayer(Layer layer)
        {
            layer.Remove();
            _containerLayer.Append(layer);
        }

        public bool IsRecording
        {
            get
            {
                return _canvas != null;
            }
        }

        // Recording state
        PictureLayer _currentLayer;
        PictureRecorder _recorder;
       
        /// The canvas on which to paint.
        ///
        /// The current canvas can change whenever you paint a child using this
        /// context, which means it's fragile to hold a reference to the canvas
        /// returned by this getter.
        public Canvas Canvas
        {
            get
            {
                if (_canvas == null)
                    _startRecording();
                return _canvas;
            }
        }

        private void _startRecording()
        {
            _currentLayer = new PictureLayer(EstimatedBounds);
            _recorder = new PictureRecorder();
            _canvas = new Canvas(_recorder);
            _containerLayer.Append(_currentLayer);
        }

        /// Stop recording to a canvas if recording has started.
        ///
        /// Do not call this function directly: functions in this class will call
        /// this method as needed. This function is called internally to ensure that
        /// recording is stopped before adding layers or finalizing the results of a
        /// paint.
        ///
        /// Subclasses that need to customize how recording to a canvas is performed
        /// should override this method to save the results of the custom canvas
        /// recordings.
        public void StopRecordingIfNeeded()
        {
            if (!IsRecording)
                return;

            _currentLayer.Picture = _recorder.EndRecording();
            _currentLayer = null;
            _recorder = null;
            _canvas = null;
        }

        /// Hints that the painting in the current layer is complex and would benefit
        /// from caching.
        ///
        /// If this hint is not set, the compositor will apply its own heuristics to
        /// decide whether the current layer is complex enough to benefit from
        /// caching.
        public void SetIsComplexHint()
        {
            if (_currentLayer != null)
                _currentLayer.IsComplexHint = true;
        }

        /// Hints that the painting in the current layer is likely to change next frame.
        ///
        /// This hint tells the compositor not to cache the current layer because the
        /// cache will not be used in the future. If this hint is not set, the
        /// compositor will apply its own heuristics to decide whether the current
        /// layer is likely to be reused in the future.
        public void SetWillChangeHint()
        {
            if (_currentLayer != null)
                _currentLayer.WillChangeHint = true;
        }

        /// Adds a composited leaf layer to the recording.
        ///
        /// After calling this function, the [canvas] property will change to refer to
        /// a new [Canvas] that draws on top of the given layer.
        ///
        /// A [RenderObject] that uses this function is very likely to require its
        /// [RenderObject.alwaysNeedsCompositing] property to return true. That informs
        /// ancestor render objects that this render object will include a composited
        /// layer, which, for example, causes them to use composited clips.
        ///
        /// See also:
        ///
        ///  * [pushLayer], for adding a layer and using its canvas to paint with that
        ///    layer.
        public void AddLayer(Layer layer)
        {
            StopRecordingIfNeeded();
            AppendLayer(layer);
        }

        /// Appends the given layer to the recording, and calls the `painter` callback
        /// with that layer, providing the `childPaintBounds` as the estimated paint
        /// bounds of the child. The `childPaintBounds` can be used for debugging but
        /// have no effect on painting.
        ///
        /// The given layer must be an unattached orphan. (Providing a newly created
        /// object, rather than reusing an existing layer, satisfies that
        /// requirement.)
        ///
        /// The `offset` is the offset to pass to the `painter`.
        ///
        /// If the `childPaintBounds` are not specified then the current layer's paint
        /// bounds are used. This is appropriate if the child layer does not apply any
        /// transformation or clipping to its contents. The `childPaintBounds`, if
        /// specified, must be in the coordinate system of the new layer, and should
        /// not go outside the current layer's paint bounds.
        ///
        /// See also:
        ///
        ///  * [addLayer], for pushing a leaf layer whose canvas is not used.
        public void PushLayer(ContainerLayer childLayer, PaintingContextCallback painter, Offset offset, Rect childPaintBounds = null)
        {
            // If a layer is being reused, it may already contain children. We remove
            // them so that `painter` can add children that are relevant for this frame.
            if (childLayer.HasChildren)
            {
                childLayer.RemoveAllChildren();
            }
            StopRecordingIfNeeded();
            AppendLayer(childLayer);
            PaintingContext childContext = CreateChildContext(childLayer, childPaintBounds ?? EstimatedBounds);
            painter(childContext, offset);
            childContext.StopRecordingIfNeeded();
        }

        /// Creates a compatible painting context to paint onto [childLayer].
        public virtual PaintingContext CreateChildContext(ContainerLayer childLayer, Rect bounds)
        {
            return new PaintingContext(childLayer, bounds);
        }

        /// Clip further painting using a rectangle.
        ///
        /// {@template flutter.rendering.object.needsCompositing}
        /// * `needsCompositing` is whether the child needs compositing. Typically
        ///   matches the value of [RenderObject.needsCompositing] for the caller. If
        ///   false, this method returns null, indicating that a layer is no longer
        ///   necessary. If a render object calling this method stores the `oldLayer`
        ///   in its [RenderObject.layer] field, it should set that field to null.
        /// {@end template}
        /// * `offset` is the offset from the origin of the canvas' coordinate system
        ///   to the origin of the caller's coordinate system.
        /// * `clipRect` is rectangle (in the caller's coordinate system) to use to
        ///   clip the painting done by [painter].
        /// * `painter` is a callback that will paint with the [clipRect] applied. This
        ///   function calls the [painter] synchronously.
        /// * `clipBehavior` controls how the rectangle is clipped.
        /// {@template flutter.rendering.object.oldLayer}
        /// * `oldLayer` is the layer created in the previous frame. Specifying the
        ///   old layer gives the engine more information for performance
        ///   optimizations. Typically this is the value of [RenderObject.layer] that
        ///   a render object creates once, then reuses for all subsequent frames
        ///   until a layer is no longer needed (e.g. the render object no longer
        ///   needs compositing) or until the render object changes the type of the
        ///   layer (e.g. from opacity layer to a clip rect layer).
        /// {@end template}
        public ClipRectLayer PushClipRect(bool needsCompositing, Offset offset, Rect clipRect, PaintingContextCallback painter, Clip clipBehavior = Clip.HardEdge, ClipRectLayer oldLayer = null)
        {
            Rect offsetClipRect = clipRect.Shift(offset);
            if (needsCompositing)
            {
                ClipRectLayer layer = oldLayer ?? new ClipRectLayer();
                layer.ClipRect = offsetClipRect;
                layer.ClipBehavior = clipBehavior;
                PushLayer(layer, painter, offset, childPaintBounds: offsetClipRect);
                return layer;
            }
            else
            {
                ClipRectAndPaint(offsetClipRect, clipBehavior, offsetClipRect, () => painter(this, offset));
                return null;
            }
        }

        /// Clip further painting using a rounded rectangle.
        ///
        /// {@macro flutter.rendering.object.needsCompositing}
        /// * `offset` is the offset from the origin of the canvas' coordinate system
        ///   to the origin of the caller's coordinate system.
        /// * `bounds` is the region of the canvas (in the caller's coordinate system)
        ///   into which `painter` will paint in.
        /// * `clipRRect` is the rounded-rectangle (in the caller's coordinate system)
        ///   to use to clip the painting done by `painter`.
        /// * `painter` is a callback that will paint with the `clipRRect` applied. This
        ///   function calls the `painter` synchronously.
        /// * `clipBehavior` controls how the path is clipped.
        /// {@macro flutter.rendering.object.oldLayer}
        public ClipRRectLayer pushClipRRect(bool needsCompositing, Offset offset, Rect bounds, RRect clipRRect, PaintingContextCallback painter, Clip clipBehavior = Clip.AntiAlias, ClipRRectLayer oldLayer = null)
        {
            Rect offsetBounds = bounds.Shift(offset);
            RRect offsetClipRRect = clipRRect.Shift(offset);
            if (needsCompositing)
            {
                ClipRRectLayer layer = oldLayer ?? new ClipRRectLayer();
                layer.ClipRRect = offsetClipRRect;
                layer.ClipBehavior = clipBehavior;
                PushLayer(layer, painter, offset, childPaintBounds: offsetBounds);
                return layer;
            }
            else
            {
                ClipRRectAndPaint(offsetClipRRect, clipBehavior, offsetBounds, () => painter(this, offset));
                return null;
            }
        }

        /// Clip further painting using a path.
        ///
        /// {@macro flutter.rendering.object.needsCompositing}
        /// * `offset` is the offset from the origin of the canvas' coordinate system
        ///   to the origin of the caller's coordinate system.
        /// * `bounds` is the region of the canvas (in the caller's coordinate system)
        ///   into which `painter` will paint in.
        /// * `clipPath` is the path (in the coordinate system of the caller) to use to
        ///   clip the painting done by `painter`.
        /// * `painter` is a callback that will paint with the `clipPath` applied. This
        ///   function calls the `painter` synchronously.
        /// * `clipBehavior` controls how the rounded rectangle is clipped.
        /// {@macro flutter.rendering.object.oldLayer}
        public ClipPathLayer PushClipPath(bool needsCompositing, Offset offset, Rect bounds, Path clipPath, PaintingContextCallback painter, Clip clipBehavior = Clip.AntiAlias, ClipPathLayer oldLayer = null)
        {
            Rect offsetBounds = bounds.Shift(offset);
            Path offsetClipPath = clipPath.Shift(offset);
            if (needsCompositing)
            {
                ClipPathLayer layer = oldLayer ?? new ClipPathLayer();
                layer.ClipPath = offsetClipPath;
                layer.ClipBehavior = clipBehavior;
                PushLayer(layer, painter, offset, childPaintBounds: offsetBounds);
                return layer;
            }
            else
            {
                ClipPathAndPaint(offsetClipPath, clipBehavior, offsetBounds, () => painter(this, offset));
                return null;
            }
        }

        /// Blend further painting with an alpha value.
        ///
        /// * `offset` is the offset from the origin of the canvas' coordinate system
        ///   to the origin of the caller's coordinate system.
        /// * `alpha` is the alpha value to use when blending the painting done by
        ///   `painter`. An alpha value of 0 means the painting is fully transparent
        ///   and an alpha value of 255 means the painting is fully opaque.
        /// * `painter` is a callback that will paint with the `alpha` applied. This
        ///   function calls the `painter` synchronously.
        /// {@macro flutter.rendering.object.oldLayer}
        ///
        /// A [RenderObject] that uses this function is very likely to require its
        /// [RenderObject.alwaysNeedsCompositing] property to return true. That informs
        /// ancestor render objects that this render object will include a composited
        /// layer, which, for example, causes them to use composited clips.
        public OpacityLayer PushOpacity(Offset offset, int alpha, PaintingContextCallback painter, OpacityLayer oldLayer = null) {
            OpacityLayer layer = oldLayer ?? new OpacityLayer();
            layer.Alpha = alpha;
            layer.Offset = offset;
            PushLayer(layer, painter, Offset.Zero);
            return layer;
        }
    }
}
