using FlutterSharp.UI.PresentationFramework.Media;

namespace FlutterSharp.UI.PresentationFramework.Shapes
{
    /// <summary>
    /// Provides a base class for shape elements, such as Ellipse, Polygon, and Rectangle.
    /// </summary>
    public abstract class Shape : FrameworkElement
    {
        /// <summary>
        /// Gets or sets the <see cref="Color"/> that specifies how the <see cref="Shape"/> outline is painted.
        /// </summary>
        /// <value>
        /// A <see cref="Color"/> that specifies how the <see cref="Shape"/> outline is painted. The default is null.
        /// </value>
        public Color Stroke { get; set; }

        /// <summary>
        /// Gets or sets the stroke thickness.
        /// </summary>
        /// <value>
        /// The stroke thickness.
        /// </value>
        public double StrokeThickness { get; set; }

        /// <summary>
        /// Gets or sets a <see cref="StrokeCap"/> enumeration value that specifies how the ends of a dash are drawn.
        /// </summary>
        /// <value>
        /// One of the enumeration values for <see cref="StrokeCap"/>. The default is Butt.
        /// </value>
        public StrokeCap StrokeDashCap { get; set; }

        /// <summary>
        /// Gets or sets a <see cref="StrokeJoin"/> enumeration value that specifies the type of join that is used at the vertices of a <see cref="Shape"/>.
        /// </summary>
        /// <value>
        /// One of the enumeration values for <see cref="StrokeJoin"/>
        /// </value>
        public StrokeJoin StrokeLineJoin { get; set; }

        /// <summary>
        /// Gets or sets a limit on the ratio of the miter length to half the <see cref="StrokeThickness"/> of a <see cref="Shape"/> element.
        /// </summary>
        /// <value>
        /// The limit on the ratio of the miter length to the <see cref="StrokeThickness"/> of a <see cref="Shape"/> element. This value is always a positive number that is greater than or equal to 1.
        /// </value>
        public double StrokeMiterLimit { get; set; }

        /// <summary>
        /// Gets a value that represents the <see cref="Geometry"/> of the <see cref="Shape"/>.
        /// </summary>
        protected abstract Geometry DefiningGeometry { get; }

        /// <summary>
        /// Draws this component on the specified canvas.
        /// </summary>
        /// <param name="canvas">The canvas.</param>
        public override void Draw(UI.Canvas canvas)
        {
            var drawSize = new Size(this.ActualWidth, this.ActualHeight);
            var path = this.DefiningGeometry.ToPath(new Offset(this.X, this.Y), drawSize);

            if (this.Background != null)
                canvas.DrawPath(path, this.Background.ToPaint());

            if (this.Stroke != null && this.StrokeThickness > 0d)
            {
                var strokePaint = new Paint
                {
                    Style = PaintingStyle.Stroke,
                    Color = this.Stroke,
                    StrokeWidth = this.StrokeThickness,
                    StrokeCap = this.StrokeDashCap,
                    StrokeJoin = this.StrokeLineJoin,
                    StrokeMiterLimit = this.StrokeMiterLimit
                };

                canvas.DrawPath(path, strokePaint);
            }
        }
    }
}
