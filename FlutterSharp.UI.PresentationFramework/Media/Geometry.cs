namespace FlutterSharp.UI.PresentationFramework.Media
{
    /// <summary>
    /// Classes that derive from this abstract base class define geometric shapes. Geometry objects can be used for clipping, hit-testing, and rendering 2-D graphic data.
    /// </summary>
    public abstract class Geometry
    {
        /// <summary>
        /// Converts the geometry to flutter path.
        /// </summary>
        /// <param name="offset">The offset to apply to path vertices.</param>
        /// <param name="drawSize">Size of the draw.</param>
        /// <returns>The flutter path</returns>
        public abstract Path ToPath(Offset offset, Size drawSize);
    }
}
