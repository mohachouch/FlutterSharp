namespace FlutterSharp.UI
{
    /// Determines the winding rule that decides how the interior of a [Path] is
    /// calculated.
    ///
    /// This enum is used by the [Path.fillType] property.
    public enum PathFillType
    {
        /// The interior is defined by a non-zero sum of signed edge crossings.
        ///
        /// For a given point, the point is considered to be on the inside of the path
        /// if a line drawn from the point to infinity crosses lines going clockwise
        /// around the point a different number of times than it crosses lines going
        /// counter-clockwise around that point.
        ///
        /// See: <https://en.wikipedia.org/wiki/Nonzero-rule>
        NonZero,

        /// The interior is defined by an odd number of edge crossings.
        ///
        /// For a given point, the point is considered to be on the inside of the path
        /// if a line drawn from the point to infinity crosses an odd number of lines.
        ///
        /// See: <https://en.wikipedia.org/wiki/Even-odd_rule>
        EvenOdd,
    }
}
