namespace FlutterSharp.UI
{
    /// Defines how a list of points is interpreted when drawing a set of points.
    ///
    // ignore: deprecated_member_use
    /// Used by [Canvas.drawPoints].
    // These enum values must be kept in sync with SkCanvas::PointMode.
    public enum PointMode
    {
        /// Draw each point separately.
        ///
        /// If the [Paint.strokeCap] is [StrokeCap.round], then each point is drawn
        /// as a circle with the diameter of the [Paint.strokeWidth], filled as
        /// described by the [Paint] (ignoring [Paint.style]).
        ///
        /// Otherwise, each point is drawn as an axis-aligned square with sides of
        /// length [Paint.strokeWidth], filled as described by the [Paint] (ignoring
        /// [Paint.style]).
        Points,

        /// Draw each sequence of two points as a line segment.
        ///
        /// If the number of points is odd, then the last point is ignored.
        ///
        /// The lines are stroked as described by the [Paint] (ignoring
        /// [Paint.style]).
        Lines,

        /// Draw the entire sequence of point as one line.
        ///
        /// The lines are stroked as described by the [Paint] (ignoring
        /// [Paint.style]).
        Polygon,
    }
}
