namespace FlutterSharp.UI.PresentationFramework.Media
{
    /// <summary>
    /// Classes that derive from this abstract base class define geometric shapes. Geometry objects can be used for clipping, hit-testing, and rendering 2-D graphic data.
    /// </summary>
    public abstract class Geometry
    {
        public abstract Path ToPath(Size drawSize);
    }
}
