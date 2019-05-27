using static FlutterSharp.UI.UITypes;

namespace FlutterSharp.UI
{
    /// Base class for [Size] and [Offset], which are both ways to describe
    /// a distance as a two-dimensional axis-aligned vector.
    public abstract class OffsetBase
    {
        /// Abstract const constructor. This constructor enables subclasses to provide
        /// const constructors so that they can be used in const expressions.
        ///
        /// The first argument sets the horizontal component, and the second the
        /// vertical component.
        protected OffsetBase(double _dx, double _dy)
        {
            this._dx = _dx;
            this._dy = _dy;
        }

        protected readonly double _dx;
        protected readonly double _dy;

        /// Returns true if either component is [double.infinity], and false if both
        /// are finite (or negative infinity, or NaN).
        ///
        /// This is different than comparing for equality with an instance that has
        /// _both_ components set to [double.infinity].
        ///
        /// See also:
        ///
        ///  * [isFinite], which is true if both components are finite (and not NaN).
        public bool IsInfinite => _dx >= double.PositiveInfinity || _dy >= double.PositiveInfinity;

        /// Whether both components are finite (neither infinite nor NaN).
        ///
        /// See also:
        ///
        ///  * [isInfinite], which returns true if either component is equal to
        ///    positive infinity.
        public bool IsFinite => _dx.IsFinite() && _dy.IsFinite();

        /// Less-than operator. Compares an [Offset] or [Size] to another [Offset] or
        /// [Size], and returns true if both the horizontal and vertical values of the
        /// left-hand-side operand are smaller than the horizontal and vertical values
        /// of the right-hand-side operand respectively. Returns false otherwise.
        ///
        /// This is a partial ordering. It is possible for two values to be neither
        /// less, nor greater than, nor equal to, another.
        public static bool operator <(OffsetBase offset, OffsetBase other) => offset._dx < other._dx && offset._dy < other._dy;

        /// Less-than-or-equal-to operator. Compares an [Offset] or [Size] to another
        /// [Offset] or [Size], and returns true if both the horizontal and vertical
        /// values of the left-hand-side operand are smaller than or equal to the
        /// horizontal and vertical values of the right-hand-side operand
        /// respectively. Returns false otherwise.
        ///
        /// This is a partial ordering. It is possible for two values to be neither
        /// less, nor greater than, nor equal to, another.
        public static bool operator <=(OffsetBase offset, OffsetBase other) => offset._dx <= other._dx && offset._dy <= other._dy;

        /// Greater-than operator. Compares an [Offset] or [Size] to another [Offset]
        /// or [Size], and returns true if both the horizontal and vertical values of
        /// the left-hand-side operand are bigger than the horizontal and vertical
        /// values of the right-hand-side operand respectively. Returns false
        /// otherwise.
        ///
        /// This is a partial ordering. It is possible for two values to be neither
        /// less, nor greater than, nor equal to, another.
        public static bool operator >(OffsetBase offset, OffsetBase other) => offset._dx > other._dx && offset._dy > other._dy;

        /// Greater-than-or-equal-to operator. Compares an [Offset] or [Size] to
        /// another [Offset] or [Size], and returns true if both the horizontal and
        /// vertical values of the left-hand-side operand are bigger than or equal to
        /// the horizontal and vertical values of the right-hand-side operand
        /// respectively. Returns false otherwise.
        ///
        /// This is a partial ordering. It is possible for two values to be neither
        /// less, nor greater than, nor equal to, another.
        public static bool operator >=(OffsetBase offset, OffsetBase other) => offset._dx >= other._dx && offset._dy >= other._dy;

        /// Equality operator. Compares an [Offset] or [Size] to another [Offset] or
        /// [Size], and returns true if the horizontal and vertical values of the
        /// left-hand-side operand are equal to the horizontal and vertical values of
        /// the right-hand-side operand respectively. Returns false otherwise.
        public override bool Equals(object obj)
        {
            if (obj is OffsetBase offsetBase)
                return this._dx == offsetBase._dx &&
                    this._dy == offsetBase._dy;

            return false;
        }

        public override int GetHashCode()
        {
            return HashValues(_dx, _dy);
        }

        public override string ToString()
        {
            return $"OffsetBase({_dx.ToStringAsFixed(1)}, {_dy.ToStringAsFixed(1)})";
        }
    }
}
