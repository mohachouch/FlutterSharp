namespace FlutterSharp.UI
{
    /// Various important time points in the lifetime of a frame.
    ///
    /// [FrameTiming] records a timestamp of each phase for performance analysis.
    public enum FramePhase
    {
        /// When the UI thread starts building a frame.
        ///
        /// See also [FrameTiming.buildDuration].
        BuildStart,

        /// When the UI thread finishes building a frame.
        ///
        /// See also [FrameTiming.buildDuration].
        BuildFinish,

        /// When the GPU thread starts rasterizing a frame.
        ///
        /// See also [FrameTiming.rasterDuration].
        RasterStart,

        /// When the GPU thread finishes rasterizing a frame.
        ///
        /// See also [FrameTiming.rasterDuration].
        RasterFinish,
    }
}
