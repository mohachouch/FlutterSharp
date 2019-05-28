namespace FlutterSharp.UI
{
    /// Defines various ways to vertically bound the boxes returned by
    /// [Paragraph.getBoxesForRange].
    public enum BoxHeightStyle
    {
        /// Provide tight bounding boxes that fit heights per run. This style may result
        /// in uneven bounding boxes that do not nicely connect with adjacent boxes.
        Tight,

        /// The height of the boxes will be the maximum height of all runs in the
        /// line. All boxes in the same line will be the same height. This does not
        /// guarantee that the boxes will cover the entire vertical height of the line
        /// when there is additional line spacing.
        ///
        /// See [RectHeightStyle.includeLineSpacingTop], [RectHeightStyle.includeLineSpacingMiddle],
        /// and [RectHeightStyle.includeLineSpacingBottom] for styles that will cover
        /// the entire line.
        Max,

        /// Extends the top and bottom edge of the bounds to fully cover any line
        /// spacing.
        ///
        /// The top and bottom of each box will cover half of the
        /// space above and half of the space below the line.
        ///
        /// {@template flutter.dart:ui.boxHeightStyle.includeLineSpacing}
        /// The top edge of each line should be the same as the bottom edge
        /// of the line above. There should be no gaps in vertical coverage given any
        /// amount of line spacing. Line spacing is not included above the first line
        /// and below the last line due to no additional space present there.
        /// {@endtemplate}
        IncludeLineSpacingMiddle,

        /// Extends the top edge of the bounds to fully cover any line spacing.
        ///
        /// The line spacing will be added to the top of the box.
        ///
        /// {@macro flutter.dart:ui.rectHeightStyle.includeLineSpacing}
        IncludeLineSpacingTop,

        /// Extends the bottom edge of the bounds to fully cover any line spacing.
        ///
        /// The line spacing will be added to the bottom of the box.
        ///
        /// {@macro flutter.dart:ui.boxHeightStyle.includeLineSpacing}
        IncludeLineSpacingBottom,

        /// Calculate box heights based on the metrics of this paragraph's [StrutStyle].
        ///
        /// Boxes based on the strut will have consistent heights throughout the
        /// entire paragraph.  The top edge of each line will align with the bottom
        /// edge of the previous line.  It is possible for glyphs to extend outside
        /// these boxes.
        ///
        /// Will fall back to tight bounds if the strut is disabled or invalid.
        Strut,
    }
}