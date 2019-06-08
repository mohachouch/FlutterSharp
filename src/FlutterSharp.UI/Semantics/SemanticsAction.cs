using System.Collections.Generic;

namespace FlutterSharp.UI
{
    /// The possible actions that can be conveyed from the operating system
    /// accessibility APIs to a semantics node.
    //
    // When changes are made to this class, the equivalent APIs in
    // `lib/ui/semantics/semantics_node.h` and in each of the embedders *must* be
    // updated.
    public class SemanticsAction
    {
        public SemanticsAction(int index)
        {
            this.Index = index;
        }

        internal const int _kTapIndex = 1 << 0;
        internal const int _kLongPressIndex = 1 << 1;
        internal const int _kScrollLeftIndex = 1 << 2;
        internal const int _kScrollRightIndex = 1 << 3;
        internal const int _kScrollUpIndex = 1 << 4;
        internal const int _kScrollDownIndex = 1 << 5;
        internal const int _kIncreaseIndex = 1 << 6;
        internal const int _kDecreaseIndex = 1 << 7;
        internal const int _kShowOnScreenIndex = 1 << 8;
        internal const int _kMoveCursorForwardByCharacterIndex = 1 << 9;
        internal const int _kMoveCursorBackwardByCharacterIndex = 1 << 10;
        internal const int _kSetSelectionIndex = 1 << 11;
        internal const int _kCopyIndex = 1 << 12;
        internal const int _kCutIndex = 1 << 13;
        internal const int _kPasteIndex = 1 << 14;
        internal const int _kDidGainAccessibilityFocusIndex = 1 << 15;
        internal const int _kDidLoseAccessibilityFocusIndex = 1 << 16;
        internal const int _kCustomAction = 1 << 17;
        internal const int _kDismissIndex = 1 << 18;
        internal const int _kMoveCursorForwardByWordIndex = 1 << 19;
        internal const int _kMoveCursorBackwardByWordIndex = 1 << 20;

        /// The numerical value for this action.
        ///
        /// Each action has one bit set in this bit field.
        public readonly int Index;

        /// The equivalent of a user briefly tapping the screen with the finger
        /// without moving it.
        public static readonly SemanticsAction Tap = new SemanticsAction(_kTapIndex);

        /// The equivalent of a user pressing and holding the screen with the finger
        /// for a few seconds without moving it.
        public static readonly SemanticsAction LongPress = new SemanticsAction(_kLongPressIndex);

        /// The equivalent of a user moving their finger across the screen from right
        /// to left.
        ///
        /// This action should be recognized by controls that are horizontally
        /// scrollable.
        public static readonly SemanticsAction ScrollLeft = new SemanticsAction(_kScrollLeftIndex);

        /// The equivalent of a user moving their finger across the screen from left
        /// to right.
        ///
        /// This action should be recognized by controls that are horizontally
        /// scrollable.
        public static readonly SemanticsAction ScrollRight = new SemanticsAction(_kScrollRightIndex);

        /// The equivalent of a user moving their finger across the screen from
        /// bottom to top.
        ///
        /// This action should be recognized by controls that are vertically
        /// scrollable.
        public static readonly SemanticsAction ScrollUp = new SemanticsAction(_kScrollUpIndex);

        /// The equivalent of a user moving their finger across the screen from top
        /// to bottom.
        ///
        /// This action should be recognized by controls that are vertically
        /// scrollable.
        public static readonly SemanticsAction ScrollDown = new SemanticsAction(_kScrollDownIndex);

        /// A request to increase the value represented by the semantics node.
        ///
        /// For example, this action might be recognized by a slider control.
        public static readonly SemanticsAction Increase = new SemanticsAction(_kIncreaseIndex);

        /// A request to decrease the value represented by the semantics node.
        ///
        /// For example, this action might be recognized by a slider control.
        public static readonly SemanticsAction Decrease = new SemanticsAction(_kDecreaseIndex);

        /// A request to fully show the semantics node on screen.
        ///
        /// For example, this action might be send to a node in a scrollable list that
        /// is partially off screen to bring it on screen.
        public static readonly SemanticsAction ShowOnScreen = new SemanticsAction(_kShowOnScreenIndex);

        /// Move the cursor forward by one character.
        ///
        /// This is for example used by the cursor control in text fields.
        ///
        /// The action includes a boolean argument, which indicates whether the cursor
        /// movement should extend (or start) a selection.
        public static readonly SemanticsAction MoveCursorForwardByCharacter = new SemanticsAction(_kMoveCursorForwardByCharacterIndex);

        /// Move the cursor backward by one character.
        ///
        /// This is for example used by the cursor control in text fields.
        ///
        /// The action includes a boolean argument, which indicates whether the cursor
        /// movement should extend (or start) a selection.
        public static readonly SemanticsAction MoveCursorBackwardByCharacter = new SemanticsAction(_kMoveCursorBackwardByCharacterIndex);

        /// Set the text selection to the given range.
        ///
        /// The provided argument is a Map<String, int> which includes the keys `base`
        /// and `extent` indicating where the selection within the `value` of the
        /// semantics node should start and where it should end. Values for both
        /// keys can range from 0 to length of `value` (inclusive).
        ///
        /// Setting `base` and `extent` to the same value will move the cursor to
        /// that position (without selecting anything).
        public static readonly SemanticsAction SetSelection = new SemanticsAction(_kSetSelectionIndex);

        /// Copy the current selection to the clipboard.
        public static readonly SemanticsAction Copy = new SemanticsAction(_kCopyIndex);

        /// Cut the current selection and place it in the clipboard.
        public static readonly SemanticsAction Cut = new SemanticsAction(_kCutIndex);

        /// Paste the current content of the clipboard.
        public static readonly SemanticsAction Paste = new SemanticsAction(_kPasteIndex);

        /// Indicates that the node has gained accessibility focus.
        ///
        /// This handler is invoked when the node annotated with this handler gains
        /// the accessibility focus. The accessibility focus is the
        /// green (on Android with TalkBack) or black (on iOS with VoiceOver)
        /// rectangle shown on screen to indicate what element an accessibility
        /// user is currently interacting with.
        ///
        /// The accessibility focus is different from the input focus. The input focus
        /// is usually held by the element that currently responds to keyboard inputs.
        /// Accessibility focus and input focus can be held by two different nodes!
        public static readonly SemanticsAction DidGainAccessibilityFocus = new SemanticsAction(_kDidGainAccessibilityFocusIndex);

        /// Indicates that the node has lost accessibility focus.
        ///
        /// This handler is invoked when the node annotated with this handler
        /// loses the accessibility focus. The accessibility focus is
        /// the green (on Android with TalkBack) or black (on iOS with VoiceOver)
        /// rectangle shown on screen to indicate what element an accessibility
        /// user is currently interacting with.
        ///
        /// The accessibility focus is different from the input focus. The input focus
        /// is usually held by the element that currently responds to keyboard inputs.
        /// Accessibility focus and input focus can be held by two different nodes!
        public static readonly SemanticsAction DidLoseAccessibilityFocus = new SemanticsAction(_kDidLoseAccessibilityFocusIndex);

        /// Indicates that the user has invoked a custom accessibility action.
        ///
        /// This handler is added automatically whenever a custom accessibility
        /// action is added to a semantics node.
        public static readonly SemanticsAction CustomAction = new SemanticsAction(_kCustomAction);

        /// A request that the node should be dismissed.
        ///
        /// A [Snackbar], for example, may have a dismiss action to indicate to the
        /// user that it can be removed after it is no longer relevant. On Android,
        /// (with TalkBack) special hint text is spoken when focusing the node and
        /// a custom action is available in the local context menu. On iOS,
        /// (with VoiceOver) users can perform a standard gesture to dismiss it.
        public static readonly SemanticsAction Dismiss = new SemanticsAction(_kDismissIndex);

        /// Move the cursor forward by one word.
        ///
        /// This is for example used by the cursor control in text fields.
        ///
        /// The action includes a boolean argument, which indicates whether the cursor
        /// movement should extend (or start) a selection.
        public static readonly SemanticsAction MoveCursorForwardByWord = new SemanticsAction(_kMoveCursorForwardByWordIndex);

        /// Move the cursor backward by one word.
        ///
        /// This is for example used by the cursor control in text fields.
        ///
        /// The action includes a boolean argument, which indicates whether the cursor
        /// movement should extend (or start) a selection.
        public static readonly SemanticsAction MoveCursorBackwardByWord = new SemanticsAction(_kMoveCursorBackwardByWordIndex);

        /// The possible semantics actions.
        ///
        /// The map's key is the [index] of the action and the value is the action
        /// itself.
        public static readonly Dictionary<int, SemanticsAction> Values = new Dictionary<int, SemanticsAction>{
            { _kTapIndex , Tap },
            { _kLongPressIndex , LongPress },
            { _kScrollLeftIndex , ScrollLeft },
            { _kScrollRightIndex , ScrollRight },
            { _kScrollUpIndex , ScrollUp },
            { _kScrollDownIndex , ScrollDown },
            { _kIncreaseIndex , Increase },
            { _kDecreaseIndex , Decrease },
            { _kShowOnScreenIndex , ShowOnScreen },
            { _kMoveCursorForwardByCharacterIndex , MoveCursorForwardByCharacter },
            { _kMoveCursorBackwardByCharacterIndex , MoveCursorBackwardByCharacter },
            { _kSetSelectionIndex , SetSelection },
            { _kCopyIndex , Copy },
            { _kCutIndex , Cut },
            { _kPasteIndex , Paste },
            { _kDidGainAccessibilityFocusIndex , DidGainAccessibilityFocus },
            { _kDidLoseAccessibilityFocusIndex , DidLoseAccessibilityFocus },
            { _kCustomAction , CustomAction },
            { _kDismissIndex , Dismiss },
            { _kMoveCursorForwardByWordIndex , MoveCursorForwardByWord },
            { _kMoveCursorBackwardByWordIndex , MoveCursorBackwardByWord }
        };

        public override string ToString()
        {
            switch (Index)
            {
                case _kTapIndex:
                    return "SemanticsAction.Tap";
                case _kLongPressIndex:
                    return "SemanticsAction.LongPress";
                case _kScrollLeftIndex:
                    return "SemanticsAction.ScrollLeft";
                case _kScrollRightIndex:
                    return "SemanticsAction.ScrollRight";
                case _kScrollUpIndex:
                    return "SemanticsAction.ScrollUp";
                case _kScrollDownIndex:
                    return "SemanticsAction.ScrollDown";
                case _kIncreaseIndex:
                    return "SemanticsAction.Increase";
                case _kDecreaseIndex:
                    return "SemanticsAction.Decrease";
                case _kShowOnScreenIndex:
                    return "SemanticsAction.ShowOnScreen";
                case _kMoveCursorForwardByCharacterIndex:
                    return "SemanticsAction.MoveCursorForwardByCharacter";
                case _kMoveCursorBackwardByCharacterIndex:
                    return "SemanticsAction.MoveCursorBackwardByCharacter";
                case _kSetSelectionIndex:
                    return "SemanticsAction.SetSelection";
                case _kCopyIndex:
                    return "SemanticsAction.Copy";
                case _kCutIndex:
                    return "SemanticsAction.Cut";
                case _kPasteIndex:
                    return "SemanticsAction.Paste";
                case _kDidGainAccessibilityFocusIndex:
                    return "SemanticsAction.DidGainAccessibilityFocus";
                case _kDidLoseAccessibilityFocusIndex:
                    return "SemanticsAction.DidLoseAccessibilityFocus";
                case _kCustomAction:
                    return "SemanticsAction.CustomAction";
                case _kDismissIndex:
                    return "SemanticsAction.Dismiss";
                case _kMoveCursorForwardByWordIndex:
                    return "SemanticsAction.MoveCursorForwardByWord";
                case _kMoveCursorBackwardByWordIndex:
                    return "SemanticsAction.MoveCursorBackwardByWord";
            }
            return null;
        }
    }
}
