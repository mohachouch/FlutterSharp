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

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator ==(Point left, Point right) => left.X == right.X && left.Y == right.Y;

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(Point left, Point right) => left.X != right.X || left.Y != right.Y;

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            return obj is Point point &&
                   this.X == point.X &&
                   this.Y == point.Y;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            int hashCode = 1861411795;
            hashCode = hashCode * -1521134295 + this.X.GetHashCode();
            hashCode = hashCode * -1521134295 + this.Y.GetHashCode();
            return hashCode;
        }

    }
}
