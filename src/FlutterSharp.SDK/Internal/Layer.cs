using System;
using FlutterSharp.UI;

namespace FlutterSharp.SDK.Internal
{
    public abstract class Layer : AbstractNode
    {
        /// This layer's parent in the layer tree.
        ///
        /// The [parent] of the root node in the layer tree is null.
        ///
        /// Only subclasses of [ContainerLayer] can have children in the layer tree.
        /// All other layer classes are used for leaves in the layer tree.
        public new ContainerLayer Parent => base.Parent as ContainerLayer;

        // Whether this layer has any changes since its last call to [addToScene].
        //
        // Initialized to true as a new layer has never called [addToScene], and is
        // set to false after calling [addToScene]. The value can become true again
        // if [markNeedsAddToScene] is called, or when [updateSubtreeNeedsAddToScene]
        // is called on this layer or on an ancestor layer.
        //
        // The values of [_needsAddToScene] in a tree of layers are said to be
        // _consistent_ if every layer in the tree satisfies the following:
        //
        // - If [alwaysNeedsAddToScene] is true, then [_needsAddToScene] is also true.
        // - If [_needsAddToScene] is true and [parent] is not null, then
        //   `parent._needsAddToScene` is true.
        //
        // Typically, this value is set during the paint phase and during compositing.
        // During the paint phase render objects create new layers and call
        // [markNeedsAddToScene] on existing layers, causing this value to become
        // true. After the paint phase the tree may be in an inconsistent state.
        // During compositing [ContainerLayer.buildScene] first calls
        // [updateSubtreeNeedsAddToScene] to bring this tree to a consistent state,
        // then it calls [addToScene], and finally sets this field to false.
        internal bool _needsAddToScene = true;

        /// Mark that this layer has changed and [addToScene] needs to be called.
        public void MarkNeedsAddToScene()
        {
            // Already marked. Short-circuit.
            if (_needsAddToScene)
            {
                return;
            }

            _needsAddToScene = true;
        }

        /// Mark that this layer is in sync with engine.
        ///
        /// This is for debugging and testing purposes only. In release builds
        /// this method has no effect.
        public void DebugMarkClean()
        {
        }

        /// Subclasses may override this to true to disable retained rendering.
        public virtual bool AlwaysNeedsAddToScene => false;

        /// Whether this or any descendant layer in the subtree needs [addToScene].
        ///
        /// This is for debug and test purpose only. It only becomes valid after
        /// calling [updateSubtreeNeedsAddToScene].
        public bool DebugSubtreeNeedsAddToScene => false;

        internal EngineLayer _engineLayer;
        public EngineLayer EngineLayer
        {
            get
            {
                return _engineLayer;
            }
            set
            {
                _engineLayer = value;
                if (!AlwaysNeedsAddToScene)
                {
                    // The parent must construct a new engine layer to add this layer to, and
                    // so we mark it as needing [addToScene].
                    //
                    // This is designed to handle two situations:
                    //
                    // 1. When rendering the complete layer tree as normal. In this case we
                    // call child `addToScene` methods first, then we call `set engineLayer`
                    // for the parent. The children will call `markNeedsAddToScene` on the
                    // parent to signal that they produced new engine layers and therefore
                    // the parent needs to update. In this case, the parent is already adding
                    // itself to the scene via [addToScene], and so after it's done, its
                    // `set engineLayer` is called and it clears the `_needsAddToScene` flag.
                    //
                    // 2. When rendering an interior layer (e.g. `OffsetLayer.toImage`). In
                    // this case we call `addToScene` for one of the children but not the
                    // parent, i.e. we produce new engine layers for children but not for the
                    // parent. Here the children will mark the parent as needing
                    // `addToScene`, but the parent does not clear the flag until some future
                    // frame decides to render it, at which point the parent knows that it
                    // cannot retain its engine layer and will call `addToScene` again.
                    if (Parent != null && !Parent.AlwaysNeedsAddToScene)
                    {
                        Parent.MarkNeedsAddToScene();
                    }
                }
            }
        }

        /// Traverses the layer subtree starting from this layer and determines whether it needs [addToScene].
        ///
        /// A layer needs [addToScene] if any of the following is true:
        ///
        /// - [alwaysNeedsAddToScene] is true.
        /// - [markNeedsAddToScene] has been called.
        /// - Any of its descendants need [addToScene].
        ///
        /// [ContainerLayer] overrides this method to recursively call it on its children.
        public virtual void UpdateSubtreeNeedsAddToScene()
        {
            _needsAddToScene = _needsAddToScene || AlwaysNeedsAddToScene;
        }

        /// This layer's next sibling in the parent layer's child list.
        public Layer NextSibling => _nextSibling;
        internal Layer _nextSibling;

        /// This layer's previous sibling in the parent layer's child list.
        public Layer PreviousSibling => _previousSibling;
        internal Layer _previousSibling;

        protected override void DropChild(AbstractNode child)
        {
            if (!AlwaysNeedsAddToScene)
            {
                MarkNeedsAddToScene();
            }

            base.DropChild(child);
        }

        protected override void AdoptChild(AbstractNode child)
        {
            if (!AlwaysNeedsAddToScene)
            {
                MarkNeedsAddToScene();
            }

            base.AdoptChild(child);
        }

        /// Removes this layer from its parent layer's child list.
        ///
        /// This has no effect if the layer's parent is already null.
        public void Remove()
        {
            Parent?._removeChild(this);
        }

        /// Override this method to upload this layer to the engine.
        ///
        /// Return the engine layer for retained rendering. When there's no
        /// corresponding engine layer, null is returned.
        public virtual void AddToScene(SceneBuilder builder, Offset layerOffset)
        {
        }

        internal void _addToSceneWithRetainedRendering(SceneBuilder builder)
        {
            // There can't be a loop by adding a retained layer subtree whose
            // _needsAddToScene is false.
            //
            // Proof by contradiction:
            //
            // If we introduce a loop, this retained layer must be appended to one of
            // its descendant layers, say A. That means the child structure of A has
            // changed so A's _needsAddToScene is true. This contradicts
            // _needsAddToScene being false.
            if (!_needsAddToScene && _engineLayer != null)
            {
                builder.AddRetained(_engineLayer);
                return;
            }
            AddToScene(builder, Offset.Zero);
            // Clearing the flag _after_ calling `addToScene`, not _before_. This is
            // because `addToScene` calls children's `addToScene` methods, which may
            // mark this layer as dirty.
            _needsAddToScene = false;
        }

        /// The object responsible for creating this layer.
        ///
        /// Defaults to the value of [RenderObject.debugCreator] for the render object
        /// that created this layer. Used in debug messages.
        dynamic debugCreator;
    }
}

