namespace FlutterSharp.UI
{
    /// A way to disambiguate a [TextPosition] when its offset could match two
    /// different locations in the rendered string.
    ///
    /// For example, at an offset where the rendered text wraps, there are two
    /// visual positions that the offset could represent: one prior to the line
    /// break (at the end of the first line) and one after the line break (at the
    /// start of the second line). A text affinity disambiguates between these two
    /// cases.
    ///
    /// This affects only line breaks caused by wrapping, not explicit newline
    /// characters. For newline characters, the position is fully specified by the
    /// offset alone, and there is no ambiguity.
    ///
    /// [TextAffinity] also affects bidirectional text at the interface between LTR
    /// and RTL text. Consider the following string, where the lowercase letters
    /// will be displayed as LTR and the uppercase letters RTL: "helloHELLO".  When
    /// rendered, the string would appear visually as "helloOLLEH".  An offset of 5
    /// would be ambiguous without a corresponding [TextAffinity].  Looking at the
    /// string in code, the offset represents the position just after the "o" and
    /// just before the "H".  When rendered, this offset could be either in the
    /// middle of the string to the right of the "o" or at the end of the string to
    /// the right of the "H".
    public enum TextAffinity
    {
        /// The position has affinity for the upstream side of the text position, i.e.
        /// in the direction of the beginning of the string.
        ///
        /// In the example of an offset at the place where text is wrapping, upstream
        /// indicates the end of the first line.
        ///
        /// In the bidirectional text example "helloHELLO", an offset of 5 with
        /// [TextAffinity] upstream would appear in the middle of the rendered text,
        /// just to the right of the "o". See the definition of [TextAffinity] for the
        /// full example.
        Upstream,

        /// The position has affinity for the downstream side of the text position,
        /// i.e. in the direction of the end of the string.
        ///
        /// In the example of an offset at the place where text is wrapping,
        /// downstream indicates the beginning of the second line.
        ///
        /// In the bidirectional text example "helloHELLO", an offset of 5 with
        /// [TextAffinity] downstream would appear at the end of the rendered text,
        /// just to the right of the "H". See the definition of [TextAffinity] for the
        /// full example.
        Downstream,
    }
}