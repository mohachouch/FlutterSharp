namespace FlutterSharp.UI.PresentationFramework.Media
{
    /// <summary>
    /// Describes a two-dimensional rectangle.
    /// </summary>
    public class RectangleGeometry
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
    }
}
