namespace FlutterSharp.UI
{
    /// Where to vertically align the placeholder relative to the surrounding text.
    ///
    /// Used by [ParagraphBuilder.addPlaceholder].
    public enum PlaceholderAlignment
    {
        /// Match the baseline of the placeholder with the baseline.
        ///
        /// The [TextBaseline] to use must be specified and non-null when using this
        /// alignment mode.
        Baseline,

        /// Align the bottom edge of the placeholder with the baseline such that the
        /// placeholder sits on top of the baseline.
        ///
        /// The [TextBaseline] to use must be specified and non-null when using this
        /// alignment mode.
        AboveBaseline,

        /// Align the top edge of the placeholder with the baseline specified
        /// such that the placeholder hangs below the baseline.
        ///
        /// The [TextBaseline] to use must be specified and non-null when using this
        /// alignment mode.
        BelowBaseline,

        /// Align the top edge of the placeholder with the top edge of the font.
        ///
        /// When the placeholder is very tall, the extra space will hang from
        /// the top and extend through the bottom of the line.
        Top,

        /// Align the bottom edge of the placeholder with the top edge of the font.
        ///
        /// When the placeholder is very tall, the extra space will rise from the
        /// bottom and extend through the top of the line.
        Bottom,

        /// Align the middle of the placeholder with the middle of the text.
        ///
        /// When the placeholder is very tall, the extra space will grow equally
        /// from the top and bottom of the line.
        Middle,
    }
}
