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

        public override Path ToPath(Size drawSize)
        {
            var path = new Path();

            if (this.TopLeftCornerRadius != CornerRadius.Zero)
            {
                path
                    .MoveToEx(0, this.TopLeftCornerRadius.RadiusY)
                    .RelativeArcToPoint(new Offset(this.TopLeftCornerRadius.RadiusX, 0), new Radius(this.TopLeftCornerRadius.RadiusX, this.TopLeftCornerRadius.RadiusY), 90);
            }
            else
                path.MoveTo(0, 0);

            if (this.TopRightCornerRadius != CornerRadius.Zero)
            {
                path
                    .LineToEx(drawSize.Width - this.TopRightCornerRadius.RadiusX, 0)
                    .ArcToPoint(new Offset(drawSize.Width, this.TopRightCornerRadius.RadiusY), new Radius(this.TopRightCornerRadius.RadiusX, this.TopRightCornerRadius.RadiusY), 90);
            }
            else
                path.LineTo(drawSize.Width, 0);

            if (this.BottomRightCornerRadius != CornerRadius.Zero)
            {
                path
                    .LineToEx(drawSize.Width, drawSize.Height - this.BottomRightCornerRadius.RadiusY)
                    .ArcToPoint(new Offset(drawSize.Width - this.BottomRightCornerRadius.RadiusX, drawSize.Height), new Radius(this.BottomRightCornerRadius.RadiusX, this.BottomRightCornerRadius.RadiusY), 90);
            }
            else
                path.LineTo(drawSize.Width, drawSize.Height);

            if (this.BottomLeftCornerRadius != CornerRadius.Zero)
            {
                path
                    .LineToEx(this.BottomLeftCornerRadius.RadiusX, drawSize.Height)
                    .ArcToPoint(new Offset(0, drawSize.Height - this.BottomLeftCornerRadius.RadiusY), new Radius(this.BottomLeftCornerRadius.RadiusX, this.BottomLeftCornerRadius.RadiusY), 90);
            }
            else
                path.LineTo(0, drawSize.Height);

            return path.CloseEx();
        }
    }
}
