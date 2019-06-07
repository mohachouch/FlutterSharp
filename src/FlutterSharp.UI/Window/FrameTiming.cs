using System.Collections.Generic;

namespace FlutterSharp.UI
{
    /// Time-related performance metrics of a frame.
    ///
    /// See [Window.onReportTimings] for how to get this.
    ///
    /// The metrics in debug mode (`flutter run` without any flags) may be very
    /// different from those in profile and release modes due to the debug overhead.
    /// Therefore it's recommended to only monitor and analyze performance metrics
    /// in profile and release modes.
    public class FrameTiming
    {
        /// Construct [FrameTiming] with raw timestamps in microseconds.
        ///
        /// List [timestamps] must have the same number of elements as
        /// [FramePhase.values].
        ///
        /// This constructor is usually only called by the Flutter engine, or a test.
        /// To get the [FrameTiming] of your app, see [Window.onReportTimings].
        public FrameTiming(List<int> timestamps)
        {
            //Debug.Assert(timestamps.length == FramePhase.values.length)  TODO: fix this count need equal of enum count
            _timestamps = timestamps;
        }

        /// This is a raw timestamp in microseconds from some epoch. The epoch in all
        /// [FrameTiming] is the same, but it may not match [DateTime]'s epoch.
        public int TimestampInMicroseconds(FramePhase phase) => _timestamps[(int)phase];

        private Duration RawDuration(FramePhase phase) => new Duration(microseconds: _timestamps[(int)phase]);

        /// The duration to build the frame on the UI thread.
        ///
        /// The build starts approximately when [Window.onBeginFrame] is called. The
        /// [Duration] in the [Window.onBeginFrame] callback is exactly the
        /// `Duration(microseconds: timestampInMicroseconds(FramePhase.buildStart))`.
        ///
        /// The build finishes when [Window.render] is called.
        ///
        /// {@template dart.ui.FrameTiming.fps_smoothness_milliseconds}
        /// To ensure smooth animations of X fps, this should not exceed 1000/X
        /// milliseconds.
        /// {@endtemplate}
        /// {@template dart.ui.FrameTiming.fps_milliseconds}
        /// That's about 16ms for 60fps, and 8ms for 120fps.
        /// {@endtemplate}
        public Duration BuildDuration => RawDuration(FramePhase.BuildFinish) - RawDuration(FramePhase.BuildStart);

        /// The duration to rasterize the frame on the GPU thread.
        ///
        /// {@macro dart.ui.FrameTiming.fps_smoothness_milliseconds}
        /// {@macro dart.ui.FrameTiming.fps_milliseconds}
        public Duration RasterDuration => RawDuration(FramePhase.RasterFinish) - RawDuration(FramePhase.RasterStart);

        /// The timespan between build start and raster finish.
        ///
        /// To achieve the lowest latency on an X fps display, this should not exceed
        /// 1000/X milliseconds.
        /// {@macro dart.ui.FrameTiming.fps_milliseconds}
        ///
        /// See also [buildDuration] and [rasterDuration].
        public Duration TotalSpan => RawDuration(FramePhase.RasterFinish) - RawDuration(FramePhase.BuildStart);

        private readonly List<int> _timestamps;  // in microseconds

        private string FormatMS(Duration duration) => $"{duration.inMicroseconds * 0.001}ms";

        public override string ToString()
        {
            return $"FrameTiming(buildDuration: {FormatMS(BuildDuration)}, rasterDuration: {FormatMS(RasterDuration)}, totalSpan: {FormatMS(TotalSpan)})";
        }
    }
}