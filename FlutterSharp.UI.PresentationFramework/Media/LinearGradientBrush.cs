

namespace FlutterSharp.UI.PresentationFramework.Media
{
    /// <summary>
    /// Paints an area with a linear gradient.
    /// </summary>
    /// <seealso cref="FlutterSharp.UI.PresentationFramework.Media.GradientBrush" />
    public class LinearGradientBrush : GradientBrush
    {
        /// <summary>
        /// Gets or sets the starting two-dimensional coordinates of the linear gradient.
        /// </summary>
        /// <value>
        /// The starting two-dimensional coordinates of the linear gradient. The default is (0,0).
        /// </value>
        public Point StartPoint { get; set; }

        /// <summary>
        /// Gets or sets the ending two-dimensional coordinates of the linear gradient.
        /// </summary>
        /// <value>
        /// The ending two-dimensional coordinates of the linear gradient. The default is (1,1).
        /// </value>
        public Point EndPoint { get; set; } = new Point(1, 1);

        /// <summary>
        /// Converts the <see cref="Brush"/> to flutter <see cref="Paint"/> object.
        /// </summary>
        /// <returns>The <see cref="Paint"/> used to draw the <see cref="Brush"/></returns>
        public override Paint ToPaint()
        {
            var gradientLists = this.GradientStopsToFluttersLists();
            return new Paint
            {
                Shader = Gradient.Linear(
                    new Offset(this.StartPoint.X, this.StartPoint.Y),
                    new Offset(this.EndPoint.X, this.EndPoint.Y),
                    gradientLists.colors,
                    gradientLists.offsets)
            };
        }
    }
}
