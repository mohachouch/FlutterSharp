namespace FlutterSharp.UI
{
    /// Defines how a new clip region should be merged with the existing clip
    /// region.
    ///
    /// Used by [Canvas.clipRect].
    public enum ClipOp
    {
        /// Subtract the new region from the existing region.
        Difference,

        /// Intersect the new region from the existing region.
        Intersect,
    }
}
