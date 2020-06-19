namespace FlutterSharp.UI.PresentationFramework
{
    /// <summary>
    /// Represents an x- and y-coordinate pair in two-dimensional space.
    /// </summary>
    public struct Point
    {
        /// <summary>
        /// Creates a new <see cref="Point"/> structure that contains the specified coordinates.
        /// </summary>
        /// <param name="x">The x-coordinate of the new <see cref="Point"/> structure.</param>
        /// <param name="y">The y-coordinate of the new <see cref="Point"/> structure.</param>
        public Point(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }

        /// <summary>
        /// Gets or sets the X-coordinate value of this <see cref="Point"/> structure.
        /// </summary>
        /// <value>
        /// The X-coordinate value of this <see cref="Point"/> structure. The default value is 0.
        /// </value>
        public double X { get; set; }

        /// <summary>
        /// Gets or sets the Y-coordinate value of this <see cref="Point"/> structure.
        /// </summary>
        /// <value>
        /// The Y-coordinate value of this <see cref="Point"/> structure. The default value is 0.
        /// </value>
        public double Y { get; set; }
    }
}
