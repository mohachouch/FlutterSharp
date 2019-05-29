using static FlutterSharp.UI.UITypes;

namespace FlutterSharp.UI
{
    /// A position in a string of text.
    ///
    /// A TextPosition can be used to locate a position in a string in code (using
    /// the [offset] property), and it can also be used to locate the same position
    /// visually in a rendered string of text (using [offset] and, when needed to
    /// resolve ambiguity, [affinity]).
    ///
    /// The location of an offset in a rendered string is ambiguous in two cases.
    /// One happens when rendered text is forced to wrap. In this case, the offset
    /// where the wrap occurs could visually appear either at the end of the first
    /// line or the beginning of the second line. The second way is with
    /// bidirectional text.  An offset at the interface between two different text
    /// directions could have one of two locations in the rendered text.
    ///
    /// See the documentation for [TextAffinity] for more information on how
    /// TextAffinity disambiguates situations like these.
    public class TextPosition
    {
        public TextPosition(int offset = 0, TextAffinity affinity = TextAffinity.Downstream)
        {
            this.Offset = offset;
            this.Affinity = affinity;
        }

        /// The index of the character that immediately follows the position in the
        /// string representation of the text.
        ///
        /// For example, given the string `'Hello'`, offset 0 represents the cursor
        /// being before the `H`, while offset 5 represents the cursor being just
        /// after the `o`.
        public readonly int Offset;

        /// Disambiguates cases where the position in the string given by [offset]
        /// could represent two different visual positions in the rendered text. For
        /// example, this can happen when text is forced to wrap, or when one string
        /// of text is rendered with multiple text directions.
        ///
        /// See the documentation for [TextAffinity] for more information on how
        /// TextAffinity disambiguates situations like these.
        public readonly TextAffinity Affinity;

        public override bool Equals(object obj)
        {
            if (obj is TextPosition typedOther)
                return typedOther.Offset == Offset
                       && typedOther.Affinity == Affinity;

            return false;
        }

        public override int GetHashCode()
        {
            return HashValues(Offset, Affinity);
        }

        public override string ToString()
        {
            return $"TextPosition(offset: {Offset}, affinity: {Affinity})";
        }
    }
}