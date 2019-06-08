using System.Collections.Generic;

namespace FlutterSharp.UI
{
    /// A Boolean value that can be associated with a semantics node.
    //
    // When changes are made to this class, the equivalent APIs in
    // `lib/ui/semantics/semantics_node.h` and in each of the embedders *must* be
    // updated.
    public class SemanticsFlag
    {
        internal const int _kHasCheckedStateIndex = 1 << 0;
        internal const int _kIsCheckedIndex = 1 << 1;
        internal const int _kIsSelectedIndex = 1 << 2;
        internal const int _kIsButtonIndex = 1 << 3;
        internal const int _kIsTextFieldIndex = 1 << 4;
        internal const int _kIsFocusedIndex = 1 << 5;
        internal const int _kHasEnabledStateIndex = 1 << 6;
        internal const int _kIsEnabledIndex = 1 << 7;
        internal const int _kIsInMutuallyExclusiveGroupIndex = 1 << 8;
        internal const int _kIsHeaderIndex = 1 << 9;
        internal const int _kIsObscuredIndex = 1 << 10;
        internal const int _kScopesRouteIndex = 1 << 11;
        internal const int _kNamesRouteIndex = 1 << 12;
        internal const int _kIsHiddenIndex = 1 << 13;
        internal const int _kIsImageIndex = 1 << 14;
        internal const int _kIsLiveRegionIndex = 1 << 15;
        internal const int _kHasToggledStateIndex = 1 << 16;
        internal const int _kIsToggledIndex = 1 << 17;
        internal const int _kHasImplicitScrollingIndex = 1 << 18;

        public SemanticsFlag(int index)
        {
            this.Index = index;
        }

        /// The numerical value for this flag.
        ///
        /// Each flag has one bit set in this bit field.
        public readonly int Index;

        /// The semantics node has the quality of either being "checked" or "unchecked".
        ///
        /// This flag is mutually exclusive with [hasToggledState].
        ///
        /// For example, a checkbox or a radio button widget has checked state.
        ///
        /// See also:
        ///
        ///   * [SemanticsFlag.isChecked], which controls whether the node is "checked" or "unchecked".
        public static readonly SemanticsFlag HasCheckedState = new SemanticsFlag(_kHasCheckedStateIndex);

        /// Whether a semantics node that [hasCheckedState] is checked.
        ///
        /// If true, the semantics node is "checked". If false, the semantics node is
        /// "unchecked".
        ///
        /// For example, if a checkbox has a visible checkmark, [isChecked] is true.
        ///
        /// See also:
        ///
        ///   * [SemanticsFlag.hasCheckedState], which enables a checked state.
        public static readonly SemanticsFlag IsChecked = new SemanticsFlag(_kIsCheckedIndex);


        /// Whether a semantics node is selected.
        ///
        /// If true, the semantics node is "selected". If false, the semantics node is
        /// "unselected".
        ///
        /// For example, the active tab in a tab bar has [isSelected] set to true.
        public static readonly SemanticsFlag IsSelected = new SemanticsFlag(_kIsSelectedIndex);

        /// Whether the semantic node represents a button.
        ///
        /// Platforms has special handling for buttons, for example Android's TalkBack
        /// and iOS's VoiceOver provides an additional hint when the focused object is
        /// a button.
        public static readonly SemanticsFlag IsButton = new SemanticsFlag(_kIsButtonIndex);

        /// Whether the semantic node represents a text field.
        ///
        /// Text fields are announced as such and allow text input via accessibility
        /// affordances.
        public static readonly SemanticsFlag IsTextField = new SemanticsFlag(_kIsTextFieldIndex);

        /// Whether the semantic node currently holds the user's focus.
        ///
        /// The focused element is usually the current receiver of keyboard inputs.
        public static readonly SemanticsFlag IsFocused = new SemanticsFlag(_kIsFocusedIndex);

        /// The semantics node has the quality of either being "enabled" or
        /// "disabled".
        ///
        /// For example, a button can be enabled or disabled and therefore has an
        /// "enabled" state. Static text is usually neither enabled nor disabled and
        /// therefore does not have an "enabled" state.
        public static readonly SemanticsFlag HasEnabledState = new SemanticsFlag(_kHasEnabledStateIndex);

        /// Whether a semantic node that [hasEnabledState] is currently enabled.
        ///
        /// A disabled element does not respond to user interaction. For example, a
        /// button that currently does not respond to user interaction should be
        /// marked as disabled.
        public static readonly SemanticsFlag IsEnabled = new SemanticsFlag(_kIsEnabledIndex);

        /// Whether a semantic node is in a mutually exclusive group.
        ///
        /// For example, a radio button is in a mutually exclusive group because
        /// only one radio button in that group can be marked as [isChecked].
        public static readonly SemanticsFlag IsInMutuallyExclusiveGroup = new SemanticsFlag(_kIsInMutuallyExclusiveGroupIndex);

        /// Whether a semantic node is a header that divides content into sections.
        ///
        /// For example, headers can be used to divide a list of alphabetically
        /// sorted words into the sections A, B, C, etc. as can be found in many
        /// address book applications.
        public static readonly SemanticsFlag IsHeader = new SemanticsFlag(_kIsHeaderIndex);

        /// Whether the value of the semantics node is obscured.
        ///
        /// This is usually used for text fields to indicate that its content
        /// is a password or contains other sensitive information.
        public static readonly SemanticsFlag IsObscured = new SemanticsFlag(_kIsObscuredIndex);

        /// Whether the semantics node is the root of a subtree for which a route name
        /// should be announced.
        ///
        /// When a node with this flag is removed from the semantics tree, the
        /// framework will select the last in depth-first, paint order node with this
        /// flag.  When a node with this flag is added to the semantics tree, it is
        /// selected automatically, unless there were multiple nodes with this flag
        /// added.  In this case, the last added node in depth-first, paint order
        /// will be selected.
        ///
        /// From this selected node, the framework will search in depth-first, paint
        /// order for the first node with a [namesRoute] flag and a non-null,
        /// non-empty label. The [namesRoute] and [scopesRoute] flags may be on the
        /// same node. The label of the found node will be announced as an edge
        /// transition. If no non-empty, non-null label is found then:
        ///
        ///   * VoiceOver will make a chime announcement.
        ///   * TalkBack will make no announcement
        ///
        /// Semantic nodes annotated with this flag are generally not a11y focusable.
        ///
        /// This is used in widgets such as Routes, Drawers, and Dialogs to
        /// communicate significant changes in the visible screen.
        public static readonly SemanticsFlag ScopesRoute = new SemanticsFlag(_kScopesRouteIndex);

        /// Whether the semantics node label is the name of a visually distinct
        /// route.
        ///
        /// This is used by certain widgets like Drawers and Dialogs, to indicate
        /// that the node's semantic label can be used to announce an edge triggered
        /// semantics update.
        ///
        /// Semantic nodes annotated with this flag will still receive a11y focus.
        ///
        /// Updating this label within the same active route subtree will not cause
        /// additional announcements.
        public static readonly SemanticsFlag NamesRoute = new SemanticsFlag(_kNamesRouteIndex);

        /// Whether the semantics node is considered hidden.
        ///
        /// Hidden elements are currently not visible on screen. They may be covered
        /// by other elements or positioned outside of the visible area of a viewport.
        ///
        /// Hidden elements cannot gain accessibility focus though regular touch. The
        /// only way they can be focused is by moving the focus to them via linear
        /// navigation.
        ///
        /// Platforms are free to completely ignore hidden elements and new platforms
        /// are encouraged to do so.
        ///
        /// Instead of marking an element as hidden it should usually be excluded from
        /// the semantics tree altogether. Hidden elements are only included in the
        /// semantics tree to work around platform limitations and they are mainly
        /// used to implement accessibility scrolling on iOS.
        public static readonly SemanticsFlag IsHidden = new SemanticsFlag(_kIsHiddenIndex);

        /// Whether the semantics node represents an image.
        ///
        /// Both TalkBack and VoiceOver will inform the user the the semantics node
        /// represents an image.
        public static readonly SemanticsFlag IsImage = new SemanticsFlag(_kIsImageIndex);

        /// Whether the semantics node is a live region.
        ///
        /// A live region indicates that updates to semantics node are important.
        /// Platforms may use this information to make polite announcements to the
        /// user to inform them of updates to this node.
        ///
        /// An example of a live region is a [SnackBar] widget. On Android, A live
        /// region causes a polite announcement to be generated automatically, even
        /// if the user does not have focus of the widget.
        public static readonly SemanticsFlag IsLiveRegion = new SemanticsFlag(_kIsLiveRegionIndex);

        /// The semantics node has the quality of either being "on" or "off".
        ///
        /// This flag is mutually exclusive with [hasCheckedState].
        ///
        /// For example, a switch has toggled state.
        ///
        /// See also:
        ///
        ///    * [SemanticsFlag.isToggled], which controls whether the node is "on" or "off".
        public static readonly SemanticsFlag HasToggledState = new SemanticsFlag(_kHasToggledStateIndex);

        /// If true, the semantics node is "on". If false, the semantics node is
        /// "off".
        ///
        /// For example, if a switch is in the on position, [isToggled] is true.
        ///
        /// See also:
        ///
        ///   * [SemanticsFlag.hasToggledState], which enables a toggled state.
        public static readonly SemanticsFlag IsToggled = new SemanticsFlag(_kIsToggledIndex);

        /// Whether the platform can scroll the semantics node when the user attempts
        /// to move focus to an offscreen child.
        ///
        /// For example, a [ListView] widget has implicit scrolling so that users can
        /// easily move the accessibility focus to the next set of children. A
        /// [PageView] widget does not have implicit scrolling, so that users don't
        /// navigate to the next page when reaching the end of the current one.
        public static readonly SemanticsFlag HasImplicitScrolling = new SemanticsFlag(_kHasImplicitScrollingIndex);

        /// The possible semantics flags.
        ///
        /// The map's key is the [index] of the flag and the value is the flag itself.
        public static readonly Dictionary<int, SemanticsFlag> values = new Dictionary<int, SemanticsFlag>{
            { _kHasCheckedStateIndex , HasCheckedState },
            { _kIsCheckedIndex , IsChecked },
            { _kIsSelectedIndex , IsSelected },
            { _kIsButtonIndex , IsButton },
            { _kIsTextFieldIndex , IsTextField },
            { _kIsFocusedIndex , IsFocused },
            { _kHasEnabledStateIndex , HasEnabledState },
            { _kIsEnabledIndex , IsEnabled },
            { _kIsInMutuallyExclusiveGroupIndex , IsInMutuallyExclusiveGroup },
            { _kIsHeaderIndex , IsHeader },
            { _kIsObscuredIndex , IsObscured },
            { _kScopesRouteIndex , ScopesRoute },
            { _kNamesRouteIndex , NamesRoute },
            { _kIsHiddenIndex , IsHidden },
            { _kIsImageIndex , IsImage },
            { _kIsLiveRegionIndex , IsLiveRegion },
            { _kHasToggledStateIndex , HasToggledState },
            { _kIsToggledIndex , IsToggled },
            { _kHasImplicitScrollingIndex , HasImplicitScrolling }
        };

        public override string ToString()
        {
            switch (Index)
            {
                case _kHasCheckedStateIndex:
                    return "SemanticsFlag.HasCheckedState";
                case _kIsCheckedIndex:
                    return "SemanticsFlag.IsChecked";
                case _kIsSelectedIndex:
                    return "SemanticsFlag.IsSelected";
                case _kIsButtonIndex:
                    return "SemanticsFlag.IsButton";
                case _kIsTextFieldIndex:
                    return "SemanticsFlag.IsTextField";
                case _kIsFocusedIndex:
                    return "SemanticsFlag.IsFocused";
                case _kHasEnabledStateIndex:
                    return "SemanticsFlag.HasEnabledState";
                case _kIsEnabledIndex:
                    return "SemanticsFlag.IsEnabled";
                case _kIsInMutuallyExclusiveGroupIndex:
                    return "SemanticsFlag.IsInMutuallyExclusiveGroup";
                case _kIsHeaderIndex:
                    return "SemanticsFlag.IsHeader";
                case _kIsObscuredIndex:
                    return "SemanticsFlag.IsObscured";
                case _kScopesRouteIndex:
                    return "SemanticsFlag.ScopesRoute";
                case _kNamesRouteIndex:
                    return "SemanticsFlag.NamesRoute";
                case _kIsHiddenIndex:
                    return "SemanticsFlag.IsHidden";
                case _kIsImageIndex:
                    return "SemanticsFlag.IsImage";
                case _kIsLiveRegionIndex:
                    return "SemanticsFlag.IsLiveRegion";
                case _kHasToggledStateIndex:
                    return "SemanticsFlag.HasToggledState";
                case _kIsToggledIndex:
                    return "SemanticsFlag.IsToggled";
                case _kHasImplicitScrollingIndex:
                    return "SemanticsFlag.HasImplicitScrolling";
            }
            return null;
        }
    }
}
