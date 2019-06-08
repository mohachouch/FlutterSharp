using System.Diagnostics;
using static FlutterSharp.UI.PaintingMethods;

namespace FlutterSharp.UI
{
    /// An object that creates [SemanticsUpdate] objects.
    ///
    /// Once created, the [SemanticsUpdate] objects can be passed to
    /// [Window.updateSemantics] to update the semantics conveyed to the user.
    public class SemanticsUpdateBuilder : NativeFieldWrapperClass2
    {
        /// Creates an empty [SemanticsUpdateBuilder] object.
        public SemanticsUpdateBuilder()
        {
            Constructor();
        }

        private void Constructor()
        {
            // TODO : native 'SemanticsUpdateBuilder_constructor';
        }

        /// Update the information associated with the node with the given `id`.
        ///
        /// The semantics nodes form a tree, with the root of the tree always having
        /// an id of zero. The `childrenInTraversalOrder` and `childrenInHitTestOrder`
        /// are the ids of the nodes that are immediate children of this node. The
        /// former enumerates children in traversal order, and the latter enumerates
        /// the same children in the hit test order. The two lists must have the same
        /// length and contain the same ids. They may only differ in the order the
        /// ids are listed in. For more information about different child orders, see
        /// [DebugSemanticsDumpOrder].
        ///
        /// The system retains the nodes that are currently reachable from the root.
        /// A given update need not contain information for nodes that do not change
        /// in the update. If a node is not reachable from the root after an update,
        /// the node will be discarded from the tree.
        ///
        /// The `flags` are a bit field of [SemanticsFlag]s that apply to this node.
        ///
        /// The `actions` are a bit field of [SemanticsAction]s that can be undertaken
        /// by this node. If the user wishes to undertake one of these actions on this
        /// node, the [Window.onSemanticsAction] will be called with `id` and one of
        /// the possible [SemanticsAction]s. Because the semantics tree is maintained
        /// asynchronously, the [Window.onSemanticsAction] callback might be called
        /// with an action that is no longer possible.
        ///
        /// The `label` is a string that describes this node. The `value` property
        /// describes the current value of the node as a string. The `increasedValue`
        /// string will become the `value` string after a [SemanticsAction.increase]
        /// action is performed. The `decreasedValue` string will become the `value`
        /// string after a [SemanticsAction.decrease] action is performed. The `hint`
        /// string describes what result an action performed on this node has. The
        /// reading direction of all these strings is given by `textDirection`.
        ///
        /// The fields 'textSelectionBase' and 'textSelectionExtent' describe the
        /// currently selected text within `value`.
        ///
        /// The field `platformViewId` references the platform view, whose semantics
        /// nodes will be added as children to this node. If a platform view is
        /// specified, `childrenInHitTestOrder` and `childrenInTraversalOrder` must be
        /// empty.
        ///
        /// For scrollable nodes `scrollPosition` describes the current scroll
        /// position in logical pixel. `scrollExtentMax` and `scrollExtentMin`
        /// describe the maximum and minimum in-rage values that `scrollPosition` can
        /// be. Both or either may be infinity to indicate unbound scrolling. The
        /// value for `scrollPosition` can (temporarily) be outside this range, for
        /// example during an overscroll. `scrollChildren` is the count of the
        /// total number of child nodes that contribute semantics and `scrollIndex`
        /// is the index of the first visible child node that contributes semantics.
        ///
        /// The `rect` is the region occupied by this node in its own coordinate
        /// system.
        ///
        /// The `transform` is a matrix that maps this node's coordinate system into
        /// its parent's coordinate system.
        ///
        /// The `elevation` describes the distance in z-direction between this node
        /// and the `elevation` of the parent.
        ///
        /// The `thickness` describes how much space this node occupies in the
        /// z-direction starting at `elevation`. Basically, in the z-direction the
        /// node starts at `elevation` above the parent and ends at `elevation` +
        /// `thickness` above the parent.
        public void UpdateNode(
            int? id = null,
            int? flags = null,
            int? actions = null,
            int? textSelectionBase = null,
            int? textSelectionExtent = null,
            int? platformViewId = null,
            int? scrollChildren = null,
            int? scrollIndex = null,
            double? scrollPosition = null,
            double? scrollExtentMax = null,
            double? scrollExtentMin = null,
            double? elevation = null,
            double? thickness = null,
            Rect rect = null,
            string label = null,
            string hint = null,
            string value = null,
            string increasedValue = null,
            string decreasedValue = null,
            TextDirection? textDirection = null,
            Float64List transform = null,
            Int32List childrenInTraversalOrder = null,
            Int32List childrenInHitTestOrder = null,
            Int32List additionalActions = null)
        {
            Debug.Assert(Matrix4IsValid(transform));
            Debug.Assert(scrollChildren == 0 || scrollChildren == null || (scrollChildren > 0 && childrenInHitTestOrder != null),
              "If a node has scrollChildren, it must have childrenInHitTestOrder");

            UpdateNode(
              id,
              flags,
              actions,
              textSelectionBase,
              textSelectionExtent,
              platformViewId,
              scrollChildren,
              scrollIndex,
              scrollPosition,
              scrollExtentMax,
              scrollExtentMin,
              rect.Left,
              rect.Top,
              rect.Right,
              rect.Bottom,
              elevation,
              thickness,
              label,
              hint,
              value,
              increasedValue,
              decreasedValue,
              textDirection != null ? (int)textDirection + 1 : 0,
              transform,
              childrenInTraversalOrder,
              childrenInHitTestOrder,
              additionalActions);
        }

        private void UpdateNode(
            int? id,
            int? flags,
            int? actions,
            int? textSelectionBase,
            int? textSelectionExtent,
            int? platformViewId,
            int? scrollChildren,
            int? scrollIndex,
            double? scrollPosition,
            double? scrollExtentMax,
            double? scrollExtentMin,
            double? left,
            double? top,
            double? right,
            double? bottom,
            double? elevation,
            double? thickness,
            string label,
            string hint,
            string value,
            string increasedValue,
            string decreasedValue,
            int? textDirection,
            Float64List transform,
            Int32List childrenInTraversalOrder,
            Int32List childrenInHitTestOrder,
            Int32List additionalActions)
        {
            // TODO : native 'SemanticsUpdateBuilder_updateNode';
        }

        /// Update the custom semantics action associated with the given `id`.
        ///
        /// The name of the action exposed to the user is the `label`. For overridden
        /// standard actions this value is ignored.
        ///
        /// The `hint` should describe what happens when an action occurs, not the
        /// manner in which a tap is accomplished. For example, use "delete" instead
        /// of "double tap to delete".
        ///
        /// The text direction of the `hint` and `label` is the same as the global
        /// window.
        ///
        /// For overridden standard actions, `overrideId` corresponds with a
        /// [SemanticsAction.index] value. For custom actions this argument should not be
        /// provided.
        public void UpdateCustomAction(int id = 0, string label = null, string hint = null, int overrideId = -1)
        {
            Debug.Assert(id != null);
            Debug.Assert(overrideId != null);
            _updateCustomAction(id, label, hint, overrideId);
        }

        private void _updateCustomAction(int id, string label, string hint, int overrideId)
        {
            // TODO : native 'SemanticsUpdateBuilder_updateCustomAction';
        }
        
        /// Creates a [SemanticsUpdate] object that encapsulates the updates recorded
        /// by this object.
        ///
        /// The returned object can be passed to [Window.updateSemantics] to actually
        /// update the semantics retained by the system.
        public SemanticsUpdate Build()
        {
            // TODO :  native 'SemanticsUpdateBuilder_build';
            return null;
        }
    }
}
