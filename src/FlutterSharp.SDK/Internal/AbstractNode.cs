using System.Diagnostics;

namespace FlutterSharp.SDK.Internal
{
    // This file gets mutated by //dev/devicelab/bin/tasks/flutter_test_performance.dart
    // during device lab performance tests. When editing this file, check to make sure
    // that it didn't break that test.

    /// An abstract node in a tree.
    ///
    /// AbstractNode has as notion of depth, attachment, and parent, but does not
    /// have a model for children.
    ///
    /// When a subclass is changing the parent of a child, it should call either
    /// `parent.adoptChild(child)` or `parent.dropChild(child)` as appropriate.
    /// Subclasses can expose an API for manipulating the tree if desired (e.g. a
    /// setter for a `child` property, or an `add()` method to manipulate a list).
    ///
    /// The current parent node is exposed by the [parent] property.
    ///
    /// The current attachment state is exposed by [attached]. The root of any tree
    /// that is to be considered attached should be manually attached by calling
    /// [attach]. Other than that, the [attach] and [detach] methods should not be
    /// called directly; attachment is managed automatically by the aforementioned
    /// [adoptChild] and [dropChild] methods.
    ///
    /// Subclasses that have children must override [attach] and [detach] as
    /// described in the documentation for those methods.
    ///
    /// Nodes always have a [depth] greater than their ancestors'. There's no
    /// guarantee regarding depth between siblings. The depth of a node is used to
    /// ensure that nodes are processed in depth order. The [depth] of a child can
    /// be more than one greater than the [depth] of the parent, because the [depth]
    /// values are never decreased: all that matters is that it's greater than the
    /// parent. Consider a tree with a root node A, a child B, and a grandchild C.
    /// Initially, A will have [depth] 0, B [depth] 1, and C [depth] 2. If C is
    /// moved to be a child of A, sibling of B, then the numbers won't change. C's
    /// [depth] will still be 2. The [depth] is automatically maintained by the
    /// [adoptChild] and [dropChild] methods.
    public abstract class AbstractNode
    {
        /// The depth of this node in the tree.
        ///
        /// The depth of nodes in a tree monotonically increases as you traverse down
        /// the tree.
        public int Depth => _depth;
        private int _depth = 0;

        /// Adjust the [depth] of the given [child] to be greater than this node's own
        /// [depth].
        ///
        /// Only call this method from overrides of [redepthChildren].
        protected virtual void RedepthChild(AbstractNode child)
        {
            Debug.Assert(child.Owner == Owner);
            if (child._depth <= _depth)
            {
                child._depth = _depth + 1;
                child.RedepthChildren();
            }
        }

        /// Adjust the [depth] of this node's children, if any.
        ///
        /// Override this method in subclasses with child nodes to call [redepthChild]
        /// for each child. Do not call this method directly.
        public virtual void RedepthChildren()
        {
        }

        /// The owner for this node (null if unattached).
        ///
        /// The entire subtree that this node belongs to will have the same owner.
        public object Owner => _owner;
        private object _owner;

        /// Whether this node is in a tree whose root is attached to something.
        ///
        /// This becomes true during the call to [attach].
        ///
        /// This becomes false during the call to [detach].
        public bool Attached => _owner != null;

        /// Mark this node as attached to the given owner.
        ///
        /// Typically called only from the [parent]'s [attach] method, and by the
        /// [owner] to mark the root of a tree as attached.
        ///
        /// Subclasses with children should override this method to first call their
        /// inherited [attach] method, and then [attach] all their children to the
        /// same [owner].
        public virtual void Attach(object owner)
        {
            Debug.Assert(owner != null);
            Debug.Assert(_owner == null);
            _owner = owner;
        }

        /// Mark this node as detached.
        ///
        /// Typically called only from the [parent]'s [detach], and by the [owner] to
        /// mark the root of a tree as detached.
        ///
        /// Subclasses with children should override this method to first call their
        /// inherited [detach] method, and then [detach] all their children.
        public virtual void Detach()
        {
            Debug.Assert(_owner != null);
            _owner = null;
            Debug.Assert(Parent == null || Attached == Parent.Attached);
        }

        /// The parent of this node in the tree.
        public virtual AbstractNode Parent => _parent;
        private AbstractNode _parent;

        /// Mark the given node as being a child of this node.
        ///
        /// Subclasses should call this function when they acquire a new child.
        protected virtual void AdoptChild(AbstractNode child)
        {
            Debug.Assert(child != null);
            Debug.Assert(child._parent == null);
            child._parent = this;
            if (Attached)
                child.Attach(_owner);
            RedepthChild(child);
        }

        /// Disconnect the given node from this node.
        ///
        /// Subclasses should call this function when they lose a child.
        protected virtual void DropChild(AbstractNode child)
        {
            Debug.Assert(child != null);
            Debug.Assert(child._parent == this);
            Debug.Assert(child.Attached == Attached);
            child._parent = null;
            if (Attached)
                child.Detach();
        }
    }
}
