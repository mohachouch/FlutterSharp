using System;
using System.Collections.Generic;
using FlutterSharp.UI;

namespace FlutterSharp.SDK.Internal
{
    public class ContainerLayer : Layer
    {
        /// The first composited layer in this layer's child list.
        public Layer FirstChild => _firstChild;
        private Layer _firstChild;

        /// The last composited layer in this layer's child list.
        public Layer LastChild => _lastChild;
        private Layer _lastChild;

        /// Returns whether this layer has at least one child layer.
        public bool HasChildren => _firstChild != null;

        /// Consider this layer as the root and build a scene (a tree of layers)
        /// in the engine.
        // The reason this method is in the `ContainerLayer` class rather than
        // `PipelineOwner` or other singleton level is because this method can be used
        // both to render the whole layer tree (e.g. a normal application frame) and
        // to render a subtree (e.g. `OffsetLayer.toImage`).
        public Scene BuildScene(SceneBuilder builder)
        {
            base.UpdateSubtreeNeedsAddToScene();
            AddToScene(builder, Offset.Zero);
            // Clearing the flag _after_ calling `addToScene`, not _before_. This is
            // because `addToScene` calls children's `addToScene` methods, which may
            // mark this layer as dirty.
            _needsAddToScene = false;
            Scene scene = builder.Build();
            return scene;
        }

        private bool _debugUltimatePreviousSiblingOf(Layer child, Layer equals = null)
        {
            while (child.PreviousSibling != null)
            {
                child = child.PreviousSibling;
            }
            return child == equals;
        }

        private bool _debugUltimateNextSiblingOf(Layer child, Layer equals = null)
        {

            while (child.NextSibling != null)
            {
                child = child.NextSibling;
            }
            return child == equals;
        }

        public override void UpdateSubtreeNeedsAddToScene()
        {
            base.UpdateSubtreeNeedsAddToScene();
            Layer child = FirstChild;
            while (child != null)
            {
                child.UpdateSubtreeNeedsAddToScene();
                _needsAddToScene = _needsAddToScene || child._needsAddToScene;
                child = child.NextSibling;
            }
        }

        public override void Attach(object owner)
        {
            base.Attach(owner);
            Layer child = FirstChild;
            while (child != null)
            {
                child.Attach(owner);
                child = child.NextSibling;
            }
        }

        public override void Detach()
        {
            base.Detach();
            Layer child = FirstChild;
            while (child != null)
            {
                child.Detach();
                child = child.NextSibling;
            }
        }

        /// Adds the given layer to the end of this layer's child list.
        public void Append(Layer child)
        {
            AdoptChild(child);
            child._previousSibling = LastChild;
            if (LastChild != null)
                LastChild._nextSibling = child;
            _lastChild = child;
            _firstChild = _firstChild ?? child;
        }

        // Implementation of [Layer.remove].
        internal void _removeChild(Layer child)
        {
            if (child._previousSibling == null)
            {
                _firstChild = child._nextSibling;
            }
            else
            {
                child._previousSibling._nextSibling = child.NextSibling;
            }
            if (child._nextSibling == null)
            {
                _lastChild = child.PreviousSibling;
            }
            else
            {
                child.NextSibling._previousSibling = child.PreviousSibling;
            }
            child._previousSibling = null;
            child._nextSibling = null;
            DropChild(child);
        }

        /// Removes all of this layer's children from its child list.
        public void RemoveAllChildren()
        {
            Layer child = FirstChild;
            while (child != null)
            {
                Layer next = child.NextSibling;
                child._previousSibling = null;
                child._nextSibling = null;
                DropChild(child);
                child = next;
            }
            _firstChild = null;
            _lastChild = null;
        }

        public override void AddToScene(SceneBuilder builder, Offset layerOffset)
        {
            AddChildrenToScene(builder, layerOffset);
        }

        /// Uploads all of this layer's children to the engine.
        ///
        /// This method is typically used by [addToScene] to insert the children into
        /// the scene. Subclasses of [ContainerLayer] typically override [addToScene]
        /// to apply effects to the scene using the [SceneBuilder] API, then insert
        /// their children using [addChildrenToScene], then reverse the aforementioned
        /// effects before returning from [addToScene].
        public void AddChildrenToScene(SceneBuilder builder, Offset childOffset = null)
        {
            if (childOffset == null)
                childOffset = Offset.Zero;

            Layer child = FirstChild;
            while (child != null)
            {
                if (childOffset == Offset.Zero)
                {
                    child._addToSceneWithRetainedRendering(builder);
                }
                else
                {
                    child.AddToScene(builder, childOffset);
                }
                child = child.NextSibling;
            }
        }

        /// Applies the transform that would be applied when compositing the given
        /// child to the given matrix.
        ///
        /// Specifically, this should apply the transform that is applied to child's
        /// _origin_. When using [applyTransform] with a chain of layers, results will
        /// be unreliable unless the deepest layer in the chain collapses the
        /// `layerOffset` in [addToScene] to zero, meaning that it passes
        /// [Offset.zero] to its children, and bakes any incoming `layerOffset` into
        /// the [SceneBuilder] as (for instance) a transform (which is then also
        /// included in the transformation applied by [applyTransform]).
        ///
        /// For example, if [addToScene] applies the `layerOffset` and then
        /// passes [Offset.zero] to the children, then it should be included in the
        /// transform applied here, whereas if [addToScene] just passes the
        /// `layerOffset` to the child, then it should not be included in the
        /// transform applied here.
        ///
        /// This method is only valid immediately after [addToScene] has been called,
        /// before any of the properties have been changed.
        ///
        /// The default implementation does nothing, since [ContainerLayer], by
        /// default, composites its children at the origin of the [ContainerLayer]
        /// itself.
        ///
        /// The `child` argument should generally not be null, since in principle a
        /// layer could transform each child independently. However, certain layers
        /// may explicitly allow null as a value, for example if they know that they
        /// transform all their children identically.
        ///
        /// The `transform` argument must not be null.
        ///
        /// Used by [FollowerLayer] to transform its child to a [LeaderLayer]'s
        /// position.
        public void ApplyTransform(Layer child, object transform)
        {

        }

        /// Returns the descendants of this layer in depth first order.
        public List<Layer> DepthFirstIterateChildren()
        {
            if (FirstChild == null)
                return new List<Layer>();

            List<Layer> children = new List<Layer>();
            Layer child = FirstChild;
            while (child != null)
            {
                children.Add(child);
                if (child is ContainerLayer containerLayer)
                {
                    children.AddRange(containerLayer.DepthFirstIterateChildren());
                }
                child = child.NextSibling;
            }
            return children;
        }

    }
}
