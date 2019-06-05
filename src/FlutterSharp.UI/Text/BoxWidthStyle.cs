namespace FlutterSharp.UI
{
    /// Defines various ways to horizontally bound the boxes returned by
    /// [Paragraph.getBoxesForRange].
    public enum BoxWidthStyle
    {
        // Provide tight bounding boxes that fit widths to the runs of each line
        // independently.
        Tight,

        /// Adds up to two additional boxes as needed at the beginning and/or end
        /// of each line so that the widths of the boxes in line are the same width
        /// as the widest line in the paragraph.
        ///
        /// The additional boxes on each line are only added when the relevant box
        /// at the relevant edge of that line does not span the maximum width of
        /// the paragraph.
        Max,
    }
}