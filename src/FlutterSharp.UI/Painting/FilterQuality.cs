namespace FlutterSharp.UI
{
    /// Quality levels for image filters.
    ///
    /// See [Paint.filterQuality].
    public enum FilterQuality
    {
        // This list comes from Skia's SkFilterQuality.h and the values (order) should
        // be kept in sync.

        /// Fastest possible filtering, albeit also the lowest quality.
        ///
        /// Typically this implies nearest-neighbor filtering.
        None,

        /// Better quality than [none], faster than [medium].
        ///
        /// Typically this implies bilinear interpolation.
        Low,

        /// Better quality than [low], faster than [high].
        ///
        /// Typically this implies a combination of bilinear interpolation and
        /// pyramidal parametric pre-filtering (mipmaps).
        Medium,

        /// Best possible quality filtering, albeit also the slowest.
        ///
        /// Typically this implies bicubic interpolation or better.
        High,
    }
}
