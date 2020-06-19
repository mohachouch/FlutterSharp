using System;

namespace FlutterSharp.UI.PresentationFramework
{
    /// <summary>
    /// Describes the thickness of a frame around a rectangle. Four Double values describe the Left, Top, Right, and Bottom sides of the rectangle, respectively.
    /// </summary>
    public struct Thickness : IEquatable<Thickness>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Thickness"/> structure that has the specified uniform length on each side.
        /// </summary>
        /// <param name="uniformLength">The uniform length applied to all four sides of the bounding rectangle.</param>
        public Thickness(double uniformLength)
        {
            this.Left = uniformLength;
            this.Top = uniformLength;
            this.Right = uniformLength;
            this.Bottom = uniformLength;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Thickness"/> structure that has specific lengths (supplied as a Double) applied to each side of the rectangle.
        /// </summary>
        /// <param name="leftRight">The thickness for the left and right side of the rectangle.</param>
        /// <param name="topBottom">The thickness for the upper and lower side of the rectangle.</param>
        public Thickness(double leftRight, double topBottom)
        {
            this.Left = leftRight;
            this.Top = topBottom;
            this.Right = leftRight;
            this.Bottom = topBottom;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Thickness"/> structure that has specific lengths (supplied as a Double) applied to each side of the rectangle.
        /// </summary>
        /// <param name="left">The thickness for the left side of the rectangle.</param>
        /// <param name="top">The thickness for the upper side of the rectangle.</param>
        /// <param name="right">The thickness for the right side of the rectangle.</param>
        /// <param name="bottom">The thickness for the lower side of the rectangle.</param>
        public Thickness(double left, double top, double right, double bottom)
        {
            this.Left = left;
            this.Top = top;
            this.Right = right;
            this.Bottom = bottom;
        }

        /// <summary>
        /// A Double that represents the width of the left side of the bounding rectangle for this instance of <see cref="Thickness"/>.
        /// </summary>
        /// <value>
        /// The left.
        /// </value>
        public double Left { get; }

        /// <summary>
        /// A Double that represents the width of the upper side of the bounding rectangle for this instance of <see cref="Thickness"/>.
        /// </summary>
        /// <value>
        /// The top.
        /// </value>
        public double Top { get; }

        /// <summary>
        /// A Double that represents the width of the right side of the bounding rectangle for this instance of <see cref="Thickness"/>.
        /// </summary>
        /// <value>
        /// The right.
        /// </value>
        public double Right { get; }

        /// <summary>
        /// A Double that represents the width of the lower side of the bounding rectangle for this instance of <see cref="Thickness"/>.
        /// </summary>
        /// <value>
        /// The bottom.
        /// </value>
        public double Bottom { get; }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other">other</paramref> parameter; otherwise, false.
        /// </returns>
        public bool Equals(Thickness other)
        {
            return other.Left == this.Left
                    && other.Top == this.Top
                    && other.Right == this.Right
                    && other.Bottom == this.Bottom;
        }

        /// <summary>
        /// Determines whether the specified <see cref="Thickness" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj is Thickness thickness)
                return this.Equals(thickness);

            return false;
        }

        /// <summary>
        /// Returns a hash code for the structure.
        /// </summary>
        /// <returns>
        /// A hash code for this instance of <see cref="Thickness"/>.
        /// </returns>
        public override int GetHashCode()
        {
            int hashCode = -1819631549;
            hashCode = hashCode * -1521134295 + this.Left.GetHashCode();
            hashCode = hashCode * -1521134295 + this.Top.GetHashCode();
            hashCode = hashCode * -1521134295 + this.Right.GetHashCode();
            hashCode = hashCode * -1521134295 + this.Bottom.GetHashCode();
            return hashCode;
        }
    }
}
