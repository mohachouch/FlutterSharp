using FlutterSharp.UI.PresentationFramework.Media;

namespace FlutterSharp.UI.PresentationFramework
{
    /// <summary>
    /// Provides a base class for shape elements, such as Ellipse, Polygon, and Rectangle.
    /// </summary>
    public abstract class Shape : FrameworkElement
    {
        /// <summary>
        /// Gets a value that represents the <see cref="Geometry"/> of the <see cref="Shape"/>.
        /// </summary>
        protected abstract Geometry DefiningGeometry { get; }

        public override void Draw(UI.Canvas canvas)
        {
            canvas.DrawPath(this.DefiningGeometry.ToPath(new Size(this.ActualWidth, this.ActualHeight)), this.Background.ToPaint());
        }
    }
}
