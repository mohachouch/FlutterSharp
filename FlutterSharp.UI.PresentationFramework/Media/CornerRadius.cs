namespace FlutterSharp.UI.PresentationFramework.Media
{
    /// <summary>
    /// Define a corner radius with X and Y radius
    /// </summary>
    public struct CornerRadius
    {
        /// <summary>
        /// Define the corner radius with X and Y radius
        /// </summary>
        /// <param name="radiusX">X radius</param>
        /// <param name="radiusY">Y radius</param>
        public CornerRadius(double radiusX, double radiusY)
        {
            this.RadiusX = radiusX;
            this.RadiusY = radiusY;
        }

        /// <summary>
        /// Get a X=0, Y=0 corner radius
        /// </summary>
        public static CornerRadius Zero { get; } = new CornerRadius();

        /// <summary>
        /// Get or set the X radius
        /// </summary>
        public double RadiusX { get; set; }

        /// <summary>
        /// Get or set the Y radius
        /// </summary>
        public double RadiusY { get; set; }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator ==(CornerRadius left, CornerRadius right) => left.RadiusX == right.RadiusX && left.RadiusY == right.RadiusY;

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(CornerRadius left, CornerRadius right) => left.RadiusX != right.RadiusX || left.RadiusY != right.RadiusY;

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            return obj is CornerRadius radius &&
                   this.RadiusX == radius.RadiusX &&
                   this.RadiusY == radius.RadiusY;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            int hashCode = -2072950857;
            hashCode = hashCode * -1521134295 + this.RadiusX.GetHashCode();
            hashCode = hashCode * -1521134295 + this.RadiusY.GetHashCode();
            return hashCode;
        }

    }
}
