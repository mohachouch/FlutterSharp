using System;
using System.Collections.Generic;
using System.Diagnostics;
using FlutterSharp.UI;

namespace FlutterSharp.SDK.Internal
{



    /// Base class for data associated with a [RenderObject] by its parent.
    ///
    /// Some render objects wish to store data on their children, such as their
    /// input parameters to the parent's layout algorithm or their position relative
    /// to other children.
    public class ParentData
    {
        /// Called when the RenderObject is removed from the tree.
        public virtual void Detach()
        {
        }
    }

    public abstract class Constraints
    {
        /// Abstract const constructor. This constructor enables subclasses to provide
        /// const constructors so that they can be used in const expressions.
        public Constraints()
        {

        }

        /// Whether there is exactly one size possible given these constraints
        public bool IsTight;

        /// Whether the constraint is expressed in a consistent manner.
        public bool IsNormalized;
    }

    public delegate void RenderObjectVisitor(RenderObject child);

    public delegate void LayoutCallback<T>(T constraint) where T : Constraints;

    public class RenderObject : AbstractNode
    {
        /// Initializes internal fields for subclasses.
        public RenderObject()
        {
            _needsCompositing = IsRepaintBoundary || AlwaysNeedsCompositing;
        }

        /// Cause the entire subtree rooted at the given [RenderObject] to be marked
        /// dirty for layout, paint, etc, so that the effects of a hot reload can be
        /// seen, or so that the effect of changing a global debug flag (such as
        /// [debugPaintSizeEnabled]) can be applied.
        ///
        /// This is called by the [RendererBinding] in response to the
        /// `ext.flutter.reassemble` hook, which is used by development tools when the
        /// application code has changed, to cause the widget tree to pick up any
        /// changed implementations.
        ///
        /// This is expensive and should not be called except during development.
        ///
        /// See also:
        ///
        ///  * [BindingBase.reassembleApplication]
        public void Reassemble()
        {
            MarkNeedsLayout();
            MarkNeedsCompositingBitsUpdate();
            MarkNeedsPaint();
            VisitChildren((child) => child.Reassemble());
        }

        // LAYOUT

        /// Data for use by the parent render object.
        ///
        /// The parent data is used by the render object that lays out this object
        /// (typically this object's parent in the render tree) to store information
        /// relevant to itself and to any other nodes who happen to know exactly what
        /// the data means. The parent data is opaque to the child.
        ///
        ///  * The parent data field must not be directly set, except by calling
        ///    [setupParentData] on the parent node.
        ///  * The parent data can be set before the child is added to the parent, by
        ///    calling [setupParentData] on the future parent node.
        ///  * The conventions for using the parent data depend on the layout protocol
        ///    used between the parent and child. For example, in box layout, the
        ///    parent data is completely opaque but in sector layout the child is
        ///    permitted to read some fields of the parent data.
        public ParentData ParentData;

        /// Override to setup parent data correctly for your children.
        ///
        /// You can call this function to set up the parent data for child before the
        /// child is added to the parent's child list.
        public virtual void SetupParentData(RenderObject child)
        {
            if (!(child.ParentData is ParentData))
                child.ParentData = new ParentData();
        }

        /// Called by subclasses when they decide a render object is a child.
        ///
        /// Only for use by subclasses when changing their child lists. Calling this
        /// in other cases will lead to an inconsistent tree and probably cause crashes.
        protected override void AdoptChild(AbstractNode child)
        {
            SetupParentData(child as RenderObject);
            MarkNeedsLayout();
            MarkNeedsCompositingBitsUpdate();
            base.AdoptChild(child);
        }

        /// Called by subclasses when they decide a render object is no longer a child.
        ///
        /// Only for use by subclasses when changing their child lists. Calling this
        /// in other cases will lead to an inconsistent tree and probably cause crashes.

        protected override void DropChild(AbstractNode child)
        {
            var childRenderObject = child as RenderObject;
            childRenderObject._cleanRelayoutBoundary();
            childRenderObject.ParentData.Detach();
            childRenderObject.ParentData = null;
            base.DropChild(childRenderObject);
            MarkNeedsLayout();
            MarkNeedsCompositingBitsUpdate();
        }

        /// Calls visitor for each immediate child of this render object.
        ///
        /// Override in subclasses with children and call the visitor for each child.
        public void VisitChildren(RenderObjectVisitor visitor)
        {
        }

        /// The object responsible for creating this render object.
        ///
        /// Used in debug messages.
        public object DebugCreator;

        internal void _debugReportException(String method, object exception, StackTrace stack)
        {
        }

        /// Whether [performResize] for this render object is currently running.
        ///
        /// Only valid when asserts are enabled. In release builds, always returns
        /// false.
        public bool DebugDoingThisResize => _debugDoingThisResize;
        private bool _debugDoingThisResize = false;

        /// Whether [performLayout] for this render object is currently running.
        ///
        /// Only valid when asserts are enabled. In release builds, always returns
        /// false.
        public bool DebugDoingThisLayout => _debugDoingThisLayout;
        private bool _debugDoingThisLayout = false;

        /// The render object that is actively computing layout.
        ///
        /// Only valid when asserts are enabled. In release builds, always returns
        /// null.
        public static RenderObject DebugActiveLayout => _debugActiveLayout;
        private static RenderObject _debugActiveLayout;

        /// Whether the parent render object is permitted to use this render object's
        /// size.
        ///
        /// Determined by the `parentUsesSize` parameter to [layout].
        ///
        /// Only valid when asserts are enabled. In release builds, always returns
        /// null.
        public bool DebugCanParentUseSize => _debugCanParentUseSize;
        private bool _debugCanParentUseSize;

        bool _debugMutationsLocked = false;

        /// Whether tree mutations are currently permitted.
        ///
        /// Only valid when asserts are enabled. In release builds, always returns
        /// null.
        public bool _debugCanPerformMutations => false;

        public virtual new PipelineOwner Owner => base.Owner as PipelineOwner;

        public override void Attach(object owner)
        {
            base.Attach(owner);
            // If the node was dirtied in some way while unattached, make sure to add
            // it to the appropriate dirty list now that an owner is available
            if (_needsLayout && _relayoutBoundary != null)
            {
                // Don't enter this block if we've never laid out at all;
                // scheduleInitialLayout() will handle it
                _needsLayout = false;
                MarkNeedsLayout();
            }
            if (_needsCompositingBitsUpdate)
            {
                _needsCompositingBitsUpdate = false;
                MarkNeedsCompositingBitsUpdate();
            }
            if (_needsPaint && _layer != null)
            {
                // Don't enter this block if we've never painted at all;
                // scheduleInitialPaint() will handle it
                _needsPaint = false;
                MarkNeedsPaint();
            }
        }

        /// Whether this render object's layout information is dirty.
        ///
        /// This is only set in debug mode. In general, render objects should not need
        /// to condition their runtime behavior on whether they are dirty or not,
        /// since they should only be marked dirty immediately prior to being laid
        /// out and painted.
        ///
        /// It is intended to be used by tests and asserts.
        public bool DebugNeedsLayout => false;
        internal bool _needsLayout = true;

        private RenderObject _relayoutBoundary;
        private bool _doingThisLayoutWithCallback = false;

        /// The layout constraints most recently supplied by the parent.

        public virtual Constraints Constraints => _constraints;
        private Constraints _constraints;

        /// Verify that the object's constraints are being met. Override
        /// this function in a subclass to verify that your state matches
        /// the constraints object. This function is only called in checked
        /// mode and only when needsLayout is false. If the constraints are
        /// not met, it should assert or throw an exception.
        public virtual void DebugAssertDoesMeetConstraints()
        {

        }

        /// When true, debugAssertDoesMeetConstraints() is currently
        /// executing asserts for verifying the consistent behavior of
        /// intrinsic dimensions methods.
        ///
        /// This should only be set by debugAssertDoesMeetConstraints()
        /// implementations. It is used by tests to selectively ignore
        /// custom layout callbacks. It should not be set outside of
        /// debugAssertDoesMeetConstraints(), and should not be checked in
        /// release mode (where it will always be false).
        public static bool DebugCheckingIntrinsics = false;
        private bool _debugSubtreeRelayoutRootAlreadyMarkedNeedsLayout()
        {
            if (_relayoutBoundary == null)
                return true; // we don't know where our relayout boundary is yet
            RenderObject node = this;
            while (node != _relayoutBoundary)
            {
                node = node.Parent as RenderObject;
                if ((!node._needsLayout) && (!node._debugDoingThisLayout))
                    return false;
            }
            return true;
        }

        public virtual void MarkNeedsLayout()
        {
            if (_needsLayout)
            {
                return;
            }
            if (_relayoutBoundary != this)
            {
                MarkParentNeedsLayout();
            }
            else
            {
                _needsLayout = true;
                if (Owner != null)
                {
                    Owner._nodesNeedingLayout.Add(this);
                    Owner.RequestVisualUpdate();
                }
            }
        }

        /// Mark this render object's layout information as dirty, and then defer to
        /// the parent.
        ///
        /// This function should only be called from [markNeedsLayout] or
        /// [markNeedsLayoutForSizedByParentChange] implementations of subclasses that
        /// introduce more reasons for deferring the handling of dirty layout to the
        /// parent. See [markNeedsLayout] for details.
        ///
        /// Only call this if [parent] is not null.
        public virtual void MarkParentNeedsLayout()
        {
            _needsLayout = true;
            RenderObject parent = this.Parent as RenderObject;
            if (!_doingThisLayoutWithCallback)
            {
                parent.MarkNeedsLayout();
            }
        }

        /// Mark this render object's layout information as dirty (like
        /// [markNeedsLayout]), and additionally also handle any necessary work to
        /// handle the case where [sizedByParent] has changed value.
        ///
        /// This should be called whenever [sizedByParent] might have changed.
        ///
        /// Only call this if [parent] is not null.
        public void MarkNeedsLayoutForSizedByParentChange()
        {
            MarkNeedsLayout();
            MarkParentNeedsLayout();
        }

        private void _cleanRelayoutBoundary()
        {
            if (_relayoutBoundary != this)
            {
                _relayoutBoundary = null;
                _needsLayout = true;
                VisitChildren((child) => child._cleanRelayoutBoundary());
            }
        }

        /// Bootstrap the rendering pipeline by scheduling the very first layout.
        ///
        /// Requires this render object to be attached and that this render object
        /// is the root of the render tree.
        ///
        /// See [RenderView] for an example of how this function is used.
        public void ScheduleInitialLayout()
        {
            _relayoutBoundary = this;
            Owner._nodesNeedingLayout.Add(this);
        }

        internal void _layoutWithoutResize()
        {
            RenderObject debugPreviousActiveLayout;
            try
            {
                PerformLayout();
            }
            catch
            {
            }
            _needsLayout = false;
            MarkNeedsPaint();
        }

        /// Compute the layout for this render object.
        ///
        /// This method is the main entry point for parents to ask their children to
        /// update their layout information. The parent passes a constraints object,
        /// which informs the child as to which layouts are permissible. The child is
        /// required to obey the given constraints.
        ///
        /// If the parent reads information computed during the child's layout, the
        /// parent must pass true for `parentUsesSize`. In that case, the parent will
        /// be marked as needing layout whenever the child is marked as needing layout
        /// because the parent's layout information depends on the child's layout
        /// information. If the parent uses the default value (false) for
        /// `parentUsesSize`, the child can change its layout information (subject to
        /// the given constraints) without informing the parent.
        ///
        /// Subclasses should not override [layout] directly. Instead, they should
        /// override [performResize] and/or [performLayout]. The [layout] method
        /// delegates the actual work to [performResize] and [performLayout].
        ///
        /// The parent's [performLayout] method should call the [layout] of all its
        /// children unconditionally. It is the [layout] method's responsibility (as
        /// implemented here) to return early if the child does not need to do any
        /// work to update its layout information.
        public void Layout(Constraints constraints, bool parentUsesSize = false)
        {
            RenderObject relayoutBoundary;
            if (!parentUsesSize || SizedByParent || constraints.IsTight || !(Parent is RenderObject)) {
                relayoutBoundary = this;
            } else {
                RenderObject parent = this.Parent as RenderObject;
                relayoutBoundary = parent._relayoutBoundary;
            }

            if (!_needsLayout && constraints == _constraints && relayoutBoundary == _relayoutBoundary) {
                return;
            }
            _constraints = constraints;
            if (_relayoutBoundary != null && relayoutBoundary != _relayoutBoundary) {
                // The local relayout boundary has changed, must notify children in case
                // they also need updating. Otherwise, they will be confused about what
                // their actual relayout boundary is later.
                VisitChildren((child) => child._cleanRelayoutBoundary());
            }
            _relayoutBoundary = relayoutBoundary;

            if (SizedByParent)
            {
                try
                {
                    PerformResize();
                }
                catch
                {
                }

            }

            try
            {
                PerformLayout();
            }
            catch
            {
            }
            _needsLayout = false;
            MarkNeedsPaint();
        }

        /// If a subclass has a "size" (the state controlled by `parentUsesSize`,
        /// whatever it is in the subclass, e.g. the actual `size` property of
        /// [RenderBox]), and the subclass verifies that in checked mode this "size"
        /// property isn't used when [debugCanParentUseSize] isn't set, then that
        /// subclass should override [debugResetSize] to reapply the current values of
        /// [debugCanParentUseSize] to that state.
        protected virtual void DebugResetSize()
        {
        }

        /// Whether the constraints are the only input to the sizing algorithm (in
        /// particular, child nodes have no impact).
        ///
        /// Returning false is always correct, but returning true can be more
        /// efficient when computing the size of this render object because we don't
        /// need to recompute the size if the constraints don't change.
        ///
        /// Typically, subclasses will always return the same value. If the value can
        /// change, then, when it does change, the subclass should make sure to call
        /// [markNeedsLayoutForSizedByParentChange].
        protected virtual bool SizedByParent => false;

        /// Updates the render objects size using only the constraints.
        ///
        /// Do not call this function directly: call [layout] instead. This function
        /// is called by [layout] when there is actually work to be done by this
        /// render object during layout. The layout constraints provided by your
        /// parent are available via the [constraints] getter.
        ///
        /// Subclasses that set [sizedByParent] to true should override this method
        /// to compute their size.
        ///
        /// This function is called only if [sizedByParent] is true.
        protected virtual void PerformResize()
        {
        }

        /// Do the work of computing the layout for this render object.
        ///
        /// Do not call this function directly: call [layout] instead. This function
        /// is called by [layout] when there is actually work to be done by this
        /// render object during layout. The layout constraints provided by your
        /// parent are available via the [constraints] getter.
        ///
        /// If [sizedByParent] is true, then this function should not actually change
        /// the dimensions of this render object. Instead, that work should be done by
        /// [performResize]. If [sizedByParent] is false, then this function should
        /// both change the dimensions of this render object and instruct its children
        /// to layout.
        ///
        /// In implementing this function, you must call [layout] on each of your
        /// children, passing true for parentUsesSize if your layout information is
        /// dependent on your child's layout information. Passing true for
        /// parentUsesSize ensures that this render object will undergo layout if the
        /// child undergoes layout. Otherwise, the child can change its layout
        /// information without informing this render object.
        protected virtual void PerformLayout()
        {

        }

        /// Allows mutations to be made to this object's child list (and any
        /// descendants) as well as to any other dirty nodes in the render tree owned
        /// by the same [PipelineOwner] as this object. The `callback` argument is
        /// invoked synchronously, and the mutations are allowed only during that
        /// callback's execution.
        ///
        /// This exists to allow child lists to be built on-demand during layout (e.g.
        /// based on the object's size), and to enable nodes to be moved around the
        /// tree as this happens (e.g. to handle [GlobalKey] reparenting), while still
        /// ensuring that any particular node is only laid out once per frame.
        ///
        /// Calling this function disables a number of assertions that are intended to
        /// catch likely bugs. As such, using this function is generally discouraged.
        ///
        /// This function can only be called during layout.
        protected void InvokeLayoutCallback<T>(LayoutCallback<T> callback)
            where T : Constraints
        {
            _doingThisLayoutWithCallback = true;
            try
            {
                //callback<Constraints>(this.Constraints);


                //Owner._enableMutationsToDirtySubtrees(() => { callback(this.Constraints); });
            }
            finally
            {
                _doingThisLayoutWithCallback = false;
            }
        }

        /// Rotate this render object (not yet implemented).
        public virtual void Rotate(int oldAngle = 0, int newAngle = 0, Duration time = null)
        {
        }

        // when the parent has rotated (e.g. when the screen has been turned
        // 90 degrees), immediately prior to layout() being called for the
        // new dimensions, rotate() is called with the old and new angles.
        // The next time paint() is called, the coordinate space will have
        // been rotated N quarter-turns clockwise, where:
        //    N = newAngle-oldAngle
        // ...but the rendering is expected to remain the same, pixel for
        // pixel, on the output device. Then, the layout() method or
        // equivalent will be called.


        // PAINTING

        /// Whether [paint] for this render object is currently running.
        ///
        /// Only valid when asserts are enabled. In release builds, always returns
        /// false.
        public bool DebugDoingThisPaint => _debugDoingThisPaint;
        private bool _debugDoingThisPaint = false;

        /// The render object that is actively painting.
        ///
        /// Only valid when asserts are enabled. In release builds, always returns
        /// null.
        public static RenderObject DebugActivePaint => _debugActivePaint;
        private static RenderObject _debugActivePaint;

        /// Whether this render object repaints separately from its parent.
        ///
        /// Override this in subclasses to indicate that instances of your class ought
        /// to repaint independently. For example, render objects that repaint
        /// frequently might want to repaint themselves without requiring their parent
        /// to repaint.
        ///
        /// If this getter returns true, the [paintBounds] are applied to this object
        /// and all descendants. The framework automatically creates an [OffsetLayer]
        /// and assigns it to the [layer] field. Render objects that declare
        /// themselves as repaint boundaries must not replace the layer created by
        /// the framework.
        ///
        /// Warning: This getter must not change value over the lifetime of this object.
        public virtual bool IsRepaintBoundary => false;

        /// Called, in checked mode, if [isRepaintBoundary] is true, when either the
        /// this render object or its parent attempt to paint.
        ///
        /// This can be used to record metrics about whether the node should actually
        /// be a repaint boundary.
        public void DebugRegisterRepaintBoundaryPaint(bool includedParent = true, bool includedChild = false)
        {
        }

        /// Whether this render object always needs compositing.
        ///
        /// Override this in subclasses to indicate that your paint function always
        /// creates at least one composited layer. For example, videos should return
        /// true if they use hardware decoders.
        ///
        /// You must call [markNeedsCompositingBitsUpdate] if the value of this getter
        /// changes. (This is implied when [adoptChild] or [dropChild] are called.)
        protected bool AlwaysNeedsCompositing => false;

        /// The compositing layer that this render object uses to repaint.
        ///
        /// If this render object is not a repaint boundary, it is the responsibility
        /// of the [paint] method to populate this field. If [needsCompositing] is
        /// true, this field may be populated with the root-most layer used by the
        /// render object implementation. When repainting, instead of creating a new
        /// layer the render object may update the layer stored in this field for better
        /// performance. It is also OK to leave this field as null and create a new
        /// layer on every repaint, but without the performance benefit. If
        /// [needsCompositing] is false, this field must be set to null either by
        /// never populating this field, or by setting it to null when the value of
        /// [needsCompositing] changes from true to false.
        ///
        /// If this render object is a repaint boundary, the framework automatically
        /// creates an [OffsetLayer] and populates this field prior to calling the
        /// [paint] method. The [paint] method must not replace the value of this
        /// field.
        internal ContainerLayer _layer;
        public ContainerLayer Layer
        {
            get
            {
                return _layer;
            }
            set
            {
                _layer = value;
            }
        }

        /// In debug mode, the compositing layer that this render object uses to repaint.
        ///
        /// This getter is intended for debugging purposes only. In release builds, it
        /// always returns null. In debug builds, it returns the layer even if the layer
        /// is dirty.
        ///
        /// For production code, consider [layer].
        public ContainerLayer DebugLayer => _layer;


        internal bool _needsCompositingBitsUpdate = false;

        public void MarkNeedsCompositingBitsUpdate()
        {
            if (_needsCompositingBitsUpdate)
                return;
            _needsCompositingBitsUpdate = true;
            if (Parent is RenderObject parent)
            {
                if (parent._needsCompositingBitsUpdate)
                    return;
                if (!IsRepaintBoundary && !parent.IsRepaintBoundary)
                {
                    parent.MarkNeedsCompositingBitsUpdate();
                    return;
                }
            }
            // parent is fine (or there isn't one), but we are dirty
            if (Owner != null)
                Owner._nodesNeedingCompositingBitsUpdate.Add(this);
        }

        internal bool _needsCompositing; // initialized in the constructor
                                         /// Whether we or one of our descendants has a compositing layer.
                                         ///
                                         /// If this node needs compositing as indicated by this bit, then all ancestor
                                         /// nodes will also need compositing.
                                         ///
                                         /// Only legal to call after [PipelineOwner.flushLayout] and
                                         /// [PipelineOwner.flushCompositingBits] have been called.
        public bool NeedsCompositing => _needsCompositing;

        internal void _updateCompositingBits()
        {
            if (!_needsCompositingBitsUpdate)
                return;
            bool oldNeedsCompositing = _needsCompositing;
            _needsCompositing = false;
            VisitChildren((RenderObject child) => {
                child._updateCompositingBits();
                if (child.NeedsCompositing)
                    _needsCompositing = true;
            });
            if (IsRepaintBoundary || AlwaysNeedsCompositing)
                _needsCompositing = true;
            if (oldNeedsCompositing != _needsCompositing)
                MarkNeedsPaint();
            _needsCompositingBitsUpdate = false;
        }

        /// Whether this render object's paint information is dirty.
        ///
        /// This is only set in debug mode. In general, render objects should not need
        /// to condition their runtime behavior on whether they are dirty or not,
        /// since they should only be marked dirty immediately prior to being laid
        /// out and painted.
        ///
        /// It is intended to be used by tests and asserts.
        ///
        /// It is possible (and indeed, quite common) for [debugNeedsPaint] to be
        /// false and [debugNeedsLayout] to be true. The render object will still be
        /// repainted in the next frame when this is the case, because the
        /// [markNeedsPaint] method is implicitly called by the framework after a
        /// render object is laid out, prior to the paint phase.
        public bool DebugNeedsPaint => _needsPaint;
        internal bool _needsPaint = true;

        public void MarkNeedsPaint()
        {
            if (_needsPaint)
                return;
            _needsPaint = true;
            if (IsRepaintBoundary)
            {
                if (Owner != null)
                {
                    Owner._nodesNeedingPaint.Add(this);
                    Owner.RequestVisualUpdate();
                }
            }
            else if (Parent is RenderObject parent)
            {
                parent.MarkNeedsPaint();
            }
            else
            {
                // If we're the root of the render tree (probably a RenderView),
                // then we have to paint ourselves, since nobody else can paint
                // us. We don't add ourselves to _nodesNeedingPaint in this
                // case, because the root is always told to paint regardless.
                if (Owner != null)
                    Owner.RequestVisualUpdate();
            }
        }

        // Called when flushPaint() tries to make us paint but our layer is detached.
        // To make sure that our subtree is repainted when it's finally reattached,
        // even in the case where some ancestor layer is itself never marked dirty, we
        // have to mark our entire detached subtree as dirty and needing to be
        // repainted. That way, we'll eventually be repainted.
        internal void _skippedPaintingOnLayer()
        {
            AbstractNode ancestor = Parent;
            while (ancestor is RenderObject node)
            {
                if (node.IsRepaintBoundary)
                {
                    if (node._layer == null)
                        break; // looks like the subtree here has never been painted. let it handle itself.
                    if (node._layer.Attached)
                        break; // it's the one that detached us, so it's the one that will decide to repaint us.
                    node._needsPaint = true;
                }
                ancestor = node.Parent;
            }
        }

        /// Bootstrap the rendering pipeline by scheduling the very first paint.
        ///
        /// Requires that this render object is attached, is the root of the render
        /// tree, and has a composited layer.
        ///
        /// See [RenderView] for an example of how this function is used.
        public void ScheduleInitialPaint(ContainerLayer rootLayer)
        {
            _layer = rootLayer;
            Owner._nodesNeedingPaint.Add(this);
        }

        /// Replace the layer. This is only valid for the root of a render
        /// object subtree (whatever object [scheduleInitialPaint] was
        /// called on).
        ///
        /// This might be called if, e.g., the device pixel ratio changed.
        public void ReplaceRootLayer(OffsetLayer rootLayer)
        {
            _layer?.Detach();
            _layer = rootLayer;
            MarkNeedsPaint();
        }

        internal void _paintWithContext(PaintingContext context, Offset offset)
        {
            // If we still need layout, then that means that we were skipped in the
            // layout phase and therefore don't need painting. We might not know that
            // yet (that is, our layer might not have been detached yet), because the
            // same node that skipped us in layout is above us in the tree (obviously)
            // and therefore may not have had a chance to paint yet (since the tree
            // paints in reverse order). In particular this will happen if they have
            // a different layer, because there's a repaint boundary between us.
            if (_needsLayout)
                return;

            _needsPaint = false;
            try
            {
                Paint(context, offset);
            }
            catch
            {
            }
        }

        /// An estimate of the bounds within which this render object will paint.
        /// Useful for debugging flags such as [debugPaintLayerBordersEnabled].
        ///
        /// These are also the bounds used by [showOnScreen] to make a [RenderObject]
        /// visible on screen.
        public Rect PaintBounds;

        /// Override this method to paint debugging information.
        public virtual void DebugPaint(PaintingContext context, Offset offset)
        {
        }

        /// Paint this render object into the given context at the given offset.
        ///
        /// Subclasses should override this method to provide a visual appearance
        /// for themselves. The render object's local coordinate system is
        /// axis-aligned with the coordinate system of the context's canvas and the
        /// render object's local origin (i.e, x=0 and y=0) is placed at the given
        /// offset in the context's canvas.
        ///
        /// Do not call this function directly. If you wish to paint yourself, call
        /// [markNeedsPaint] instead to schedule a call to this function. If you wish
        /// to paint one of your children, call [PaintingContext.paintChild] on the
        /// given `context`.
        ///
        /// When painting one of your children (via a paint child function on the
        /// given context), the current canvas held by the context might change
        /// because draw operations before and after painting children might need to
        /// be recorded on separate compositing layers.
        public virtual void Paint(PaintingContext context, Offset offset)
        {
        }

        /// Applies the transform that would be applied when painting the given child
        /// to the given matrix.
        ///
        /// Used by coordinate conversion functions to translate coordinates local to
        /// one render object into coordinates local to another render object.
        public virtual void ApplyPaintTransform(RenderObject child, Matrix4 transform)
        {
        }

        /// Applies the paint transform up the tree to `ancestor`.
        ///
        /// Returns a matrix that maps the local paint coordinate system to the
        /// coordinate system of `ancestor`.
        ///
        /// If `ancestor` is null, this method returns a matrix that maps from the
        /// local paint coordinate system to the coordinate system of the
        /// [PipelineOwner.rootNode]. For the render tree owner by the
        /// [RendererBinding] (i.e. for the main render tree displayed on the device)
        /// this means that this method maps to the global coordinate system in
        /// logical pixels. To get physical pixels, use [applyPaintTransform] from the
        /// [RenderView] to further transform the coordinate.
        public Matrix4 GetTransformTo(RenderObject ancestor)
        {
            bool ancestorSpecified = ancestor != null;
            if (ancestor == null)
            {
                AbstractNode rootNode = Owner.RootNode;
                if (rootNode is RenderObject)
                    ancestor = rootNode as RenderBox;
            }
            List<RenderObject> renderers = new List<RenderObject>();
            for (RenderObject renderer = this; renderer != ancestor; renderer = renderer.Parent as RenderObject)
            {
                renderers.Add(renderer);
            }
            if (ancestorSpecified)
                renderers.Add(ancestor);
            Matrix4 transform = Matrix4.Identity();
            for (int index = renderers.Count - 1; index > 0; index -= 1)
            {
                renderers[index].ApplyPaintTransform(renderers[index - 1], transform);
            }
            return transform;
        }


    }
}
