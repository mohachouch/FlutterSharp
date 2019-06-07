using System.Diagnostics;

namespace FlutterSharp.UI
{
    /// Utilities for measuring a [Path] and extracting sub-paths.
    ///
    /// Iterate over the object returned by [Path.computeMetrics] to obtain
    /// [PathMetric] objects.
    ///
    /// Once created, the methods on this class will only be valid while the
    /// iterator is at the contour for which they were created. It will also only be
    /// valid for the path as it was specified when [Path.computeMetrics] was called.
    /// If additional contours are added or any contours are updated, the metrics
    /// need to be recomputed. Previously created metrics will still refer to a
    /// snapshot of the path at the time they were computed, rather than to the
    /// actual metrics for the new mutations to the path.
    public class PathMetric
    {
        public PathMetric(PathMeasure _measure)
        {
            Debug.Assert(_measure != null);
            this._measure = _measure;
            Length = _measure.Length(_measure.CurrentContourIndex);
            IsClosed = _measure.IsClosed(_measure.CurrentContourIndex);
            ContourIndex = _measure.CurrentContourIndex;
        }

        /// Return the total length of the current contour.
        public readonly double Length;

        /// Whether the contour is closed.
        ///
        /// Returns true if the contour ends with a call to [Path.close] (which may
        /// have been implied when using [Path.addRect]) or if `forceClosed` was
        /// specified as true in the call to [Path.computeMetrics].  Returns false
        /// otherwise.
        public readonly bool IsClosed;

        /// The zero-based index of the contour.
        ///
        /// [Path] objects are made up of zero or more contours. The first contour is
        /// created once a drawing command (e.g. [Path.lineTo]) is issued. A
        /// [Path.moveTo] command after a drawing command may create a new contour,
        /// although it may not if optimizations are applied that determine the move
        /// command did not actually result in moving the pen.
        ///
        /// This property is only valid with reference to its original iterator and
        /// the contours of the path at the time the path's metrics were computed. If
        /// additional contours were added or existing contours updated, this metric
        /// will be invalid for the current state of the path.
        public readonly int ContourIndex;

        private readonly PathMeasure _measure;

        /// Computes the position of the current contour at the given offset, and the
        /// angle of the path at that point.
        ///
        /// For example, calling this method with a distance of 1.41 for a line from
        /// 0.0,0.0 to 2.0,2.0 would give a point 1.0,1.0 and the angle 45 degrees
        /// (but in radians).
        ///
        /// Returns null if the contour has zero [length].
        ///
        /// The distance is clamped to the [length] of the current contour.
        public Tangent GetTangentForOffset(double distance)
        {
            return _measure.GetTangentForOffset(ContourIndex, distance);
        }

        /// Given a start and stop distance, return the intervening segment(s).
        ///
        /// `start` and `end` are pinned to legal values (0..[length])
        /// Returns null if the segment is 0 length or `start` > `stop`.
        /// Begin the segment with a moveTo if `startWithMoveTo` is true.
        public Path ExtractPath(double start, double end, bool startWithMoveTo = true)
        {
            return _measure.ExtractPath(ContourIndex, start, end, startWithMoveTo: startWithMoveTo);
        }

        public override string ToString()
        {
            return $"PathMetrics(length: {Length}, isClosed: {IsClosed}, contourIndex: {ContourIndex})";
        }
    }
}
