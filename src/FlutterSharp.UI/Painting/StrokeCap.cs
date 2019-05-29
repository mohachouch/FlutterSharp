namespace FlutterSharp.UI
{
    /// Styles to use for line endings.
    ///
    /// See also:
    ///
    ///  * [Paint.strokeCap] for how this value is used.
    ///  * [StrokeJoin] for the different kinds of line segment joins.
    // These enum values must be kept in sync with SkPaint::Cap.
    public enum StrokeCap
    {
        /// Begin and end contours with a flat edge and no extension.
        ///
        /// ![A butt cap ends line segments with a square end that stops at the end of
        /// the line segment.](https://flutter.github.io/assets-for-api-docs/assets/dart-ui/butt_cap.png)
        ///
        /// Compare to the [square] cap, which has the same shape, but extends past
        /// the end of the line by half a stroke width.
        Butt,

        /// Begin and end contours with a semi-circle extension.
        ///
        /// ![A round cap adds a rounded end to the line segment that protrudes
        /// by one half of the thickness of the line (which is the radius of the cap)
        /// past the end of the segment.](https://flutter.github.io/assets-for-api-docs/assets/dart-ui/round_cap.png)
        ///
        /// The cap is colored in the diagram above to highlight it: in normal use it
        /// is the same color as the line.
        Round,

        /// Begin and end contours with a half square extension. This is
        /// similar to extending each contour by half the stroke width (as
        /// given by [Paint.strokeWidth]).
        ///
        /// ![A square cap has a square end that effectively extends the line length
        /// by half of the stroke width.](https://flutter.github.io/assets-for-api-docs/assets/dart-ui/square_cap.png)
        ///
        /// The cap is colored in the diagram above to highlight it: in normal use it
        /// is the same color as the line.
        ///
        /// Compare to the [butt] cap, which has the same shape, but doesn't extend
        /// past the end of the line.
        Square,
    }
}
