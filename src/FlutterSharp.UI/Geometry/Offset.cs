using System;
using static FlutterSharp.UI.Lerp;
using static FlutterSharp.UI.UITypes;

namespace FlutterSharp.UI
{
    /// An immutable 2D floating-point offset.
    ///
    /// Generally speaking, Offsets can be interpreted in two ways:
    ///
    /// 1. As representing a point in Cartesian space a specified distance from a
    ///    separately-maintained origin. For example, the top-left position of
    ///    children in the [RenderBox] protocol is typically represented as an
    ///    [Offset] from the top left of the parent box.
    ///
    /// 2. As a vector that can be applied to coordinates. For example, when
    ///    painting a [RenderObject], the parent is passed an [Offset] from the
    ///    screen's origin which it can add to the offsets of its children to find
    ///    the [Offset] from the screen's origin to each of the children.
    ///
    /// Because a particular [Offset] can be interpreted as one sense at one time
    /// then as the other sense at a later time, the same class is used for both
    /// senses.
    ///
    /// See also:
    ///
    ///  * [Size], which represents a vector describing the size of a rectangle.
    public class Offset : OffsetBase
    {
        /// Creates an offset. The first argument sets [dx], the horizontal component,
        /// and the second sets [dy], the vertical component.
        public Offset(double _dx, double _dy) : base(_dx, _dy)
        {
        }

        /// Creates an offset from its [direction] and [distance].
        ///
        /// The direction is in radians clockwise from the positive x-axis.
        ///
        /// The distance can be omitted, to create a unit vector (distance = 1.0).
        public Offset FromDirection(double direction, double distance = 1.0)
        {
            return new Offset(distance * Math.Cos(direction), distance * Math.Sin(direction));
        }

        /// The x component of the offset.
        ///
        /// The y component is given by [dy].
        public double Dx => _dx;

        /// The y component of the offset.
        ///
        /// The x component is given by [dx].
        public double Dy => _dy;

        /// The magnitude of the offset.
        ///
        /// If you need this value to compare it to another [Offset]'s distance,
        /// consider using [distanceSquared] instead, since it is cheaper to compute.
        public double Distance => Math.Sqrt(this.Dx * this.Dx + this.Dy * this.Dy);

        /// The square of the magnitude of the offset.
        ///
        /// This is cheaper than computing the [distance] itself.
        public double DistanceSquared => this.Dx * this.Dx + this.Dy * this.Dy;

        /// The angle of this offset as radians clockwise from the positive x-axis, in
        /// the range -[pi] to [pi], assuming positive values of the x-axis go to the
        /// left and positive values of the y-axis go down.
        ///
        /// Zero means that [dy] is zero and [dx] is zero or positive.
        ///
        /// Values from zero to [pi]/2 indicate positive values of [dx] and [dy], the
        /// bottom-right quadrant.
        ///
        /// Values from [pi]/2 to [pi] indicate negative values of [dx] and positive
        /// values of [dy], the bottom-left quadrant.
        ///
        /// Values from zero to -[pi]/2 indicate positive values of [dx] and negative
        /// values of [dy], the top-right quadrant.
        ///
        /// Values from -[pi]/2 to -[pi] indicate negative values of [dx] and [dy],
        /// the top-left quadrant.
        ///
        /// When [dy] is zero and [dx] is negative, the [direction] is [pi].
        ///
        /// When [dx] is zero, [direction] is [pi]/2 if [dy] is positive and -[pi]/2
        /// if [dy] is negative.
        ///
        /// See also:
        ///
        ///  * [distance], to compute the magnitude of the vector.
        ///  * [Canvas.rotate], which uses the same convention for its angle.
        public double Direction => Math.Atan2(this.Dy, this.Dx);

        /// An offset with zero magnitude.
        ///
        /// This can be used to represent the origin of a coordinate space.
        public static readonly Offset Zero = new Offset(0.0, 0.0);

        /// An offset with infinite x and y components.
        ///
        /// See also:
        ///
        ///  * [isInfinite], which checks whether either component is infinite.
        ///  * [isFinite], which checks whether both components are finite.
        // This is included for completeness, because [Size.infinite] exists.
        public static Offset Infinite = new Offset(double.PositiveInfinity, double.PositiveInfinity);

        /// Returns a new offset with the x component scaled by `scaleX` and the y
        /// component scaled by `scaleY`.
        ///
        /// If the two scale arguments are the same, consider using the `*` operator
        /// instead:
        ///
        /// ```dart
        /// Offset a = const Offset(10.0, 10.0);
        /// Offset b = a * 2.0; // same as: a.scale(2.0, 2.0)
        /// ```
        ///
        /// If the two arguments are -1, consider using the unary `-` operator
        /// instead:
        ///
        /// ```dart
        /// Offset a = const Offset(10.0, 10.0);
        /// Offset b = -a; // same as: a.scale(-1.0, -1.0)
        /// ```
        public Offset Scale(double scaleX, double scaleY) => new Offset(this.Dx * scaleX, this.Dy * scaleY);

        /// Returns a new offset with translateX added to the x component and
        /// translateY added to the y component.
        ///
        /// If the arguments come from another [Offset], consider using the `+` or `-`
        /// operators instead:
        ///
        /// ```dart
        /// Offset a = const Offset(10.0, 10.0);
        /// Offset b = const Offset(10.0, 10.0);
        /// Offset c = a + b; // same as: a.translate(b.dx, b.dy)
        /// Offset d = a - b; // same as: a.translate(-b.dx, -b.dy)
        /// ```
        public Offset Translate(double translateX, double translateY) => new Offset(this.Dx + translateX, this.Dy + translateY);

        /// Unary negation operator.
        ///
        /// Returns an offset with the coordinates negated.
        ///
        /// If the [Offset] represents an arrow on a plane, this operator returns the
        /// same arrow but pointing in the reverse direction.
        public static Offset operator -(Offset offset) => new Offset(-offset.Dx, -offset.Dy);

        /// Binary subtraction operator.
        ///
        /// Returns an offset whose [dx] value is the left-hand-side operand's [dx]
        /// minus the right-hand-side operand's [dx] and whose [dy] value is the
        /// left-hand-side operand's [dy] minus the right-hand-side operand's [dy].
        ///
        /// See also [translate].
        public static Offset operator -(Offset offset, Offset other) => new Offset(offset.Dx - other.Dx, offset.Dy - other.Dy);

        /// Binary addition operator.
        ///
        /// Returns an offset whose [dx] value is the sum of the [dx] values of the
        /// two operands, and whose [dy] value is the sum of the [dy] values of the
        /// two operands.
        ///
        /// See also [translate].
        public static Offset operator +(Offset offset, Offset other) => new Offset(offset.Dx + other.Dx, offset.Dy + other.Dy);

        /// Multiplication operator.
        ///
        /// Returns an offset whose coordinates are the coordinates of the
        /// left-hand-side operand (an Offset) multiplied by the scalar
        /// right-hand-side operand (a double).
        ///
        /// See also [scale].
        public static Offset operator *(Offset offset, double operand) => new Offset(offset.Dx * operand, offset.Dy * operand);

        /// Division operator.
        ///
        /// Returns an offset whose coordinates are the coordinates of the
        /// left-hand-side operand (an Offset) divided by the scalar right-hand-side
        /// operand (a double).
        ///
        /// See also [scale].
        public static Offset operator /(Offset offset, double operand) => new Offset(offset.Dx / operand, offset.Dy / operand);

        /* 
        /// Integer (truncating) division operator. => THIS OPERATOR DON'T EXIST ON CSHARP
        ///
        /// Returns an offset whose coordinates are the coordinates of the
        /// left-hand-side operand (an Offset) divided by the scalar right-hand-side
        /// operand (a double), rounded towards zero.
        //Offset operator ~/(double operand) => Offset((dx ~/ operand).toDouble(), (dy ~/ operand).toDouble()); */

        /// Modulo (remainder) operator.
        ///
        /// Returns an offset whose coordinates are the remainder of dividing the
        /// coordinates of the left-hand-side operand (an Offset) by the scalar
        /// right-hand-side operand (a double)
        public static Offset operator %(Offset offset, double operand) => new Offset(offset.Dx % operand, offset.Dy % operand);

        /// Rectangle constructor operator.
        ///
        /// Combines an [Offset] and a [Size] to form a [Rect] whose top-left
        /// coordinate is the point given by adding this offset, the left-hand-side
        /// operand, to the origin, and whose size is the right-hand-side operand.
        ///
        /// ```dart
        /// Rect myRect = Offset.zero & const Size(100.0, 100.0);
        /// // same as: Rect.fromLTWH(0.0, 0.0, 100.0, 100.0)
        /// ```
        public static Rect operator &(Offset offset, Size other) => Rect.FromLTWH(offset.Dx, offset.Dy, other.Width, other.Height);

        /// Linearly interpolate between two offsets.
        ///
        /// If either offset is null, this function interpolates from [Offset.zero].
        ///
        /// The `t` argument represents position on the timeline, with 0.0 meaning
        /// that the interpolation has not started, returning `a` (or something
        /// equivalent to `a`), 1.0 meaning that the interpolation has finished,
        /// returning `b` (or something equivalent to `b`), and values in between
        /// meaning that the interpolation is at the relevant point on the timeline
        /// between `a` and `b`. The interpolation can be extrapolated beyond 0.0 and
        /// 1.0, so negative values and values greater than 1.0 are valid (and can
        /// easily be generated by curves such as [Curves.elasticInOut]).
        ///
        /// Values for `t` are usually obtained from an [Animation<double>], such as
        /// an [AnimationController].
        public static Offset Lerp(Offset a, Offset b, double t)
        {
            if (a == null && b == null)
                return null;
            if (a == null)
                return b * t;
            if (b == null)
                return a * (1.0 - t);
            return new Offset(LerpDouble(a.Dx, b.Dx, t), LerpDouble(a.Dy, b.Dy, t));
        }

        /// Compares two Offsets for equality.
        public override bool Equals(object obj)
        {
            if (obj is Offset offset)
                return Dx == offset.Dx &&
                       Dy == offset.Dy;

            return false;
        }

        public override int GetHashCode()
        {
            return HashValues(Dx, Dy);
        }

        public override string ToString()
        {
            return $"Offset({Dx.ToStringAsFixed(1)}, {Dy.ToStringAsFixed(1)})";
        }
    }
}
