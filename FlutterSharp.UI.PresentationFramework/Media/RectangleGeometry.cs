using FlutterSharp.UI.PresentationFramework.Extensions;

namespace FlutterSharp.UI.PresentationFramework.Media
{
    /// <summary>
    /// Describes a two-dimensional rectangle.
    /// </summary>
    public class RectangleGeometry : Geometry
    {
        /// <summary>
        /// Get or set the top left corner radius
        /// </summary>
        public CornerRadius TopLeftCornerRadius { get; set; }

        /// <summary>
        /// Get or set the top right corner radius
        /// </summary>
        public CornerRadius TopRightCornerRadius { get; set; }

        /// <summary>
        /// Get or set the bottom right corner radius
        /// </summary>
        public CornerRadius BottomRightCornerRadius { get; set; }

        /// <summary>
        /// Get or set the bottom left corner radius
        /// </summary>
        public CornerRadius BottomLeftCornerRadius { get; set; }

        /// <summary>
        /// Set the corner radius definition for all corners by a <see cref="CornerRadiusExpression"/>.
        /// Values can be :
        /// "5" // Set all corners to x and y radius to 5
        /// "tl5" // Set tl (Top Left) corner to x and y to 5
        /// "tl5,6 br10,8" // Set tl (Top Left) corner to x 5 and y 6 and br (Bottom Right) to x 10 and y 8
        /// ...
        /// </summary>
        /// <param name="stringExpression">Expression to interpret</param>
        public void SetCornerRadiusExpression(string stringExpression)
        {
            var expression = new CornerRadiusExpression(stringExpression);
            this.TopLeftCornerRadius = expression.TopLeft;
            this.BottomLeftCornerRadius = expression.BottomLeft;
            this.TopRightCornerRadius = expression.TopRight;
            this.BottomRightCornerRadius = expression.BottomRight;
        }

        /// <summary>
        /// Converts to path.
        /// </summary>
        /// <param name="offset">The offset to apply to path vertices.</param>
        /// <param name="drawSize">Size of the draw.</param>
        /// <returns>
        /// The flutter path
        /// </returns>
        public override Path ToPath(Offset offset, Size drawSize)
        {
            var path = new Path();

            if (this.TopLeftCornerRadius != CornerRadius.Zero)
            {
                path
                    .MoveToEx(0 + offset.Dx, this.TopLeftCornerRadius.RadiusY + offset.Dy)
                    .ArcToPoint(new Offset(this.TopLeftCornerRadius.RadiusX + offset.Dx, 0 + offset.Dy), new Radius(this.TopLeftCornerRadius.RadiusX, this.TopLeftCornerRadius.RadiusY), 90);
            }
            else
                path.MoveTo(0 + offset.Dx, 0 + offset.Dx);

            if (this.TopRightCornerRadius != CornerRadius.Zero)
            {
                path
                    .LineToEx(drawSize.Width - this.TopRightCornerRadius.RadiusX + offset.Dx, 0 + offset.Dy)
                    .ArcToPoint(new Offset(drawSize.Width + offset.Dx, this.TopRightCornerRadius.RadiusY + offset.Dy), new Radius(this.TopRightCornerRadius.RadiusX, this.TopRightCornerRadius.RadiusY), 90);
            }
            else
                path.LineTo(drawSize.Width, 0);

            if (this.BottomRightCornerRadius != CornerRadius.Zero)
            {
                path
                    .LineToEx(drawSize.Width + offset.Dx, drawSize.Height - this.BottomRightCornerRadius.RadiusY + offset.Dy)
                    .ArcToPoint(new Offset(drawSize.Width - this.BottomRightCornerRadius.RadiusX + offset.Dx, drawSize.Height + offset.Dy), new Radius(this.BottomRightCornerRadius.RadiusX, this.BottomRightCornerRadius.RadiusY), 90);
            }
            else
                path.LineTo(drawSize.Width + offset.Dx, drawSize.Height + offset.Dy);

            if (this.BottomLeftCornerRadius != CornerRadius.Zero)
            {
                path
                    .LineToEx(this.BottomLeftCornerRadius.RadiusX + offset.Dx, drawSize.Height + offset.Dy)
                    .ArcToPoint(new Offset(0 + offset.Dx, drawSize.Height - this.BottomLeftCornerRadius.RadiusY + offset.Dy), new Radius(this.BottomLeftCornerRadius.RadiusX, this.BottomLeftCornerRadius.RadiusY), 90);
            }
            else
                path.LineTo(0 + offset.Dx, drawSize.Height + offset.Dy);

            return path.CloseEx();
        }
    }
}
