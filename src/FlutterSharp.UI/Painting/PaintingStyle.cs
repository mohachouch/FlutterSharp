namespace FlutterSharp.UI
{
    /// Strategies for painting shapes and paths on a canvas.
    ///
    /// See [Paint.style].
    // These enum values must be kept in sync with SkPaint::Style.
    public enum PaintingStyle
    {
        // This list comes from Skia's SkPaint.h and the values (order) should be kept
        // in sync.

        /// Apply the [Paint] to the inside of the shape. For example, when
        /// applied to the [Canvas.drawCircle] call, this results in a disc
        /// of the given size being painted.
        Fill,

        /// Apply the [Paint] to the edge of the shape. For example, when
        /// applied to the [Canvas.drawCircle] call, this results is a hoop
        /// of the given size being painted. The line drawn on the edge will
        /// be the width given by the [Paint.strokeWidth] property.
        Stroke,
    }
}
