using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace FlutterSharp.UI
{
    /// Tracks iteration from one segment of a path to the next for measurement.
    public class PathMetricIterator : IEnumerator<PathMetric>
    {
        public PathMetricIterator(PathMeasure _pathMeasure)
        {
            Debug.Assert(_pathMeasure != null);
            this._pathMeasure = _pathMeasure;
        }

        private PathMeasure _pathMeasure;

        public PathMetric Current { get; private set; }

        object IEnumerator.Current => Current;

        public bool MoveNext()
        {
            if (_pathMeasure.NextContour())
            {
                Current = new PathMetric(_pathMeasure);
                return true;
            }
            Current = null;
            return false;
        }

        public void Reset()
        {
        }

        public void Dispose()
        {
        }
    }
}
