namespace FlutterSharp.UI
{
    /// Styles to use for line segment joins.
    ///
    /// This only affects line joins for polygons drawn by [Canvas.drawPath] and
    /// rectangles, not points drawn as lines with [Canvas.drawPoints].
    ///
    /// See also:
    ///
    /// * [Paint.strokeJoin] and [Paint.strokeMiterLimit] for how this value is
    ///   used.
    /// * [StrokeCap] for the different kinds of line endings.
    // These enum values must be kept in sync with SkPaint::Join.
    public enum StrokeJoin
    {
        /// Joins between line segments form sharp corners.
        ///
        /// {@animation 300 300 https://flutter.github.io/assets-for-api-docs/assets/dart-ui/miter_4_join.mp4}
        ///
        /// The center of the line segment is colored in the diagram above to
        /// highlight the join, but in normal usage the join is the same color as the
        /// line.
        ///
        /// See also:
        ///
        ///   * [Paint.strokeJoin], used to set the line segment join style to this
        ///     value.
        ///   * [Paint.strokeMiterLimit], used to define when a miter is drawn instead
        ///     of a bevel when the join is set to this value.
        Miter,

        /// Joins between line segments are semi-circular.
        ///
        /// {@animation 300 300 https://flutter.github.io/assets-for-api-docs/assets/dart-ui/round_join.mp4}
        ///
        /// The center of the line segment is colored in the diagram above to
        /// highlight the join, but in normal usage the join is the same color as the
        /// line.
        ///
        /// See also:
        ///
        ///   * [Paint.strokeJoin], used to set the line segment join style to this
        ///     value.
        Round,

        /// Joins between line segments connect the corners of the butt ends of the
        /// line segments to give a beveled appearance.
        ///
        /// {@animation 300 300 https://flutter.github.io/assets-for-api-docs/assets/dart-ui/bevel_join.mp4}
        ///
        /// The center of the line segment is colored in the diagram above to
        /// highlight the join, but in normal usage the join is the same color as the
        /// line.
        ///
        /// See also:
        ///
        ///   * [Paint.strokeJoin], used to set the line segment join style to this
        ///     value.
        Bevel,
    }
}
