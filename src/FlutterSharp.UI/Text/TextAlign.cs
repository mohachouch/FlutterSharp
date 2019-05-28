namespace FlutterSharp.UI
{
    /// Whether and how to align text horizontally.
    // The order of this enum must match the order of the values in RenderStyleConstants.h's ETextAlign.
    public enum TextAlign
    {
        /// Align the text on the left edge of the container.
        Left,

        /// Align the text on the right edge of the container.
        Right,

        /// Align the text in the center of the container.
        Center,

        /// Stretch lines of text that end with a soft line break to fill the width of
        /// the container.
        ///
        /// Lines that end with hard line breaks are aligned towards the [start] edge.
        Justify,

        /// Align the text on the leading edge of the container.
        ///
        /// For left-to-right text ([TextDirection.ltr]), this is the left edge.
        ///
        /// For right-to-left text ([TextDirection.rtl]), this is the right edge.
        Start,

        /// Align the text on the trailing edge of the container.
        ///
        /// For left-to-right text ([TextDirection.ltr]), this is the right edge.
        ///
        /// For right-to-left text ([TextDirection.rtl]), this is the left edge.
        End,
    }
}