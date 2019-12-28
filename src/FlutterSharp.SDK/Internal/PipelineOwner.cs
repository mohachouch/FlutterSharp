using System;
using System.Collections.Generic;
using System.Linq;
using FlutterSharp.UI;

namespace FlutterSharp.SDK.Internal
{
    public class PipelineOwner
    {
        /// Creates a pipeline owner.
        ///
        /// Typically created by the binding (e.g., [RendererBinding]), but can be
        /// created separately from the binding to drive off-screen render objects
        /// through the rendering pipeline.
        public PipelineOwner(VoidCallback onNeedVisualUpdate = null, VoidCallback onSemanticsOwnerCreated = null, VoidCallback onSemanticsOwnerDisposed = null)
        {
            this.OnNeedVisualUpdate = onNeedVisualUpdate;
            this.OnSemanticsOwnerCreated = onSemanticsOwnerCreated;
            this.OnSemanticsOwnerDisposed = onSemanticsOwnerDisposed;
        }

        /// Called when a render object associated with this pipeline owner wishes to
        /// update its visual appearance.
        ///
        /// Typical implementations of this function will schedule a task to flush the
        /// various stages of the pipeline. This function might be called multiple
        /// times in quick succession. Implementations should take care to discard
        /// duplicate calls quickly.
        public VoidCallback OnNeedVisualUpdate;

        /// Called whenever this pipeline owner creates a semantics object.
        ///
        /// Typical implementations will schedule the creation of the initial
        /// semantics tree.
        public VoidCallback OnSemanticsOwnerCreated;

        /// Called whenever this pipeline owner disposes its semantics owner.
        ///
        /// Typical implementations will tear down the semantics tree.
        public VoidCallback OnSemanticsOwnerDisposed;

        /// Calls [onNeedVisualUpdate] if [onNeedVisualUpdate] is not null.
        ///
        /// Used to notify the pipeline owner that an associated render object wishes
        /// to update its visual appearance.
        public void RequestVisualUpdate()
        {
            OnNeedVisualUpdate?.Invoke();
        }

        /// The unique object managed by this pipeline that has no parent.
        ///
        /// This object does not have to be a [RenderObject].
        private AbstractNode _rootNode;
        public AbstractNode RootNode
        {
            get
            {
                return _rootNode;
            }
            set
            {
                if (_rootNode == value)
                    return;
                _rootNode?.Detach();
                _rootNode = value;
                _rootNode?.Attach(this);
            }
        }

        internal List<RenderObject> _nodesNeedingLayout = new List<RenderObject>();

        /// Whether this pipeline is currently in the layout phase.
        ///
        /// Specifically, whether [flushLayout] is currently running.
        ///
        /// Only valid when asserts are enabled.
        public bool DebugDoingLayout => _debugDoingLayout;
        private bool _debugDoingLayout = false;

        /// Update the layout information for all dirty render objects.
        ///
        /// This function is one of the core stages of the rendering pipeline. Layout
        /// information is cleaned prior to painting so that render objects will
        /// appear on screen in their up-to-date locations.
        ///
        /// See [RendererBinding] for an example of how this function is used.
        public void FlushLayout()
        {
            try
            {
                // TODO(ianh): assert that we're not allowing previously dirty nodes to redirty themselves
                while (_nodesNeedingLayout.Count > 0)
                {
                     List<RenderObject> dirtyNodes = _nodesNeedingLayout;
                    _nodesNeedingLayout = new List<RenderObject>();

                    foreach (RenderObject node in dirtyNodes.OrderBy(x => x.Depth))
                    {
                        if (node._needsLayout && node.Owner == this)
                            node._layoutWithoutResize();
                    }
                }
            }
            finally
            {
            }
        }

        // This flag is used to allow the kinds of mutations performed by GlobalKey
        // reparenting while a LayoutBuilder is being rebuilt and in so doing tries to
        // move a node from another LayoutBuilder subtree that hasn't been updated
        // yet. To set this, call [_enableMutationsToDirtySubtrees], which is called
        // by [RenderObject.invokeLayoutCallback].
        private bool _debugAllowMutationsToDirtySubtrees = false;

        // See [RenderObject.invokeLayoutCallback].
        internal void _enableMutationsToDirtySubtrees(VoidCallback callback)
        {
            try
            {
                callback();
            }
            finally
            {
            }
        }

        internal readonly List<RenderObject> _nodesNeedingCompositingBitsUpdate = new List<RenderObject>();

        /// Updates the [RenderObject.needsCompositing] bits.
        ///
        /// Called as part of the rendering pipeline after [flushLayout] and before
        /// [flushPaint].
        public void FlushCompositingBits()
        {
            foreach (RenderObject node in _nodesNeedingCompositingBitsUpdate.OrderBy(x => x.Depth))
            {
                if (node._needsCompositingBitsUpdate && node.Owner == this)
                    node._updateCompositingBits();
            }
            _nodesNeedingCompositingBitsUpdate.Clear();
        }

        internal List<RenderObject> _nodesNeedingPaint = new List<RenderObject>();

        /// Whether this pipeline is currently in the paint phase.
        ///
        /// Specifically, whether [flushPaint] is currently running.
        ///
        /// Only valid when asserts are enabled.
        private bool DebugDoingPaint => _debugDoingPaint;
        bool _debugDoingPaint = false;

        /// Update the display lists for all render objects.
        ///
        /// This function is one of the core stages of the rendering pipeline.
        /// Painting occurs after layout and before the scene is recomposited so that
        /// scene is composited with up-to-date display lists for every render object.
        ///
        /// See [RendererBinding] for an example of how this function is used.
        public void FlushPaint()
        {
             List<RenderObject> dirtyNodes = _nodesNeedingPaint;
            _nodesNeedingPaint = new List<RenderObject>();

            // Sort the dirty nodes in reverse order (deepest first).
            foreach (RenderObject node in dirtyNodes.OrderBy(x => x.Depth))
            {
                if (node._needsPaint && node.Owner == this)
                {
                    if (node._layer.Attached)
                    {
                        PaintingContext.RepaintCompositedChild(node);
                    }
                    else
                    {
                        node._skippedPaintingOnLayer();
                    }
                }
            }
        }
    }
}
