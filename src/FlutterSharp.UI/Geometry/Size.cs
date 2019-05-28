using System;
using static FlutterSharp.UI.Lerp;
using static FlutterSharp.UI.UITypes;

namespace FlutterSharp.UI
{
    /// Holds a 2D floating-point size.
    ///
    /// You can think of this as an [Offset] from the origin.
    public class Size : OffsetBase
    {
        /// Creates a [Size] with the given [width] and [height].
        public Size(double width, double height) : base(width, height)
        {
        }

        /// Creates an instance of [Size] that has the same values as another.
        // Used by the rendering library's _DebugSize hack.
        public static Size Copy(Size source) => new Size(source.Width, source.Height);

        /// Creates a square [Size] whose [width] and [height] are the given dimension.
        ///
        /// See also:
        ///
        ///  * [Size.fromRadius], which is more convenient when the available size
        ///    is the radius of a circle.
        public static Size Square(double dimension) => new Size(dimension, dimension);

        /// Creates a [Size] with the given [width] and an infinite [height].
        public static Size FromWidth(double width) => new Size(width, double.PositiveInfinity);

        /// Creates a [Size] with the given [height] and an infinite [width].
        public static Size FromHeight(double height) => new Size(double.PositiveInfinity, height);

        /// Creates a square [Size] whose [width] and [height] are twice the given
        /// dimension.
        ///
        /// This is a square that contains a circle with the given radius.
        ///
        /// See also:
        ///
        ///  * [Size.square], which creates a square with the given dimension.
        public static Size FromRadius(double radius) => new Size(radius * 2.0, radius * 2.0);

        /// The horizontal extent of this size.
        public double Width => _dx;

        /// The vertical extent of this size.
        public double Height => _dy;

        /// The aspect ratio of this size.
        ///
        /// This returns the [width] divided by the [height].
        ///
        /// If the [width] is zero, the result will be zero. If the [height] is zero
        /// (and the [width] is not), the result will be [double.infinity] or
        /// [double.negativeInfinity] as determined by the sign of [width].
        ///
        /// See also:
        ///
        ///  * [AspectRatio], a widget for giving a child widget a specific aspect
        ///    ratio.
        ///  * [FittedBox], a widget that (in most modes) attempts to maintain a
        ///    child widget's aspect ratio while changing its size.
        public double AspectRatio
        {
            get
            {
                if (Height != 0.0)
                    return Width / Height;
                if (Width > 0.0)
                    return double.PositiveInfinity;
                if (Width < 0.0)
                    return double.NegativeInfinity;
                return 0.0;
            }
        }

        /// An empty size, one with a zero width and a zero height.
        public static readonly Size Zero = new Size(0.0, 0.0);

        /// A size whose [width] and [height] are infinite.
        ///
        /// See also:
        ///
        ///  * [isInfinite], which checks whether either dimension is infinite.
        ///  * [isFinite], which checks whether both dimensions are finite.
        public static readonly Size Infinite = new Size(double.PositiveInfinity, double.PositiveInfinity);

        /// Whether this size encloses a non-zero area.
        ///
        /// Negative areas are considered empty.
        public bool IsEmpty => Width <= 0.0 || Height <= 0.0;


        /// Binary subtraction operator for [Size].
        ///
        /// Subtracting a [Size] from a [Size] returns the [Offset] that describes how
        /// much bigger the left-hand-side operand is than the right-hand-side
        /// operand. Adding that resulting [Offset] to the [Size] that was the
        /// right-hand-side operand would return a [Size] equal to the [Size] that was
        /// the left-hand-side operand. (i.e. if `sizeA - sizeB -> offsetA`, then
        /// `offsetA + sizeB -> sizeA`)
        ///
        /// Subtracting an [Offset] from a [Size] returns the [Size] that is smaller than
        /// the [Size] operand by the difference given by the [Offset] operand. In other
        /// words, the returned [Size] has a [width] consisting of the [width] of the
        /// left-hand-side operand minus the [Offset.dx] dimension of the
        /// right-hand-side operand, and a [height] consisting of the [height] of the
        /// left-hand-side operand minus the [Offset.dy] dimension of the
        /// right-hand-side operand.
        public static OffsetBase operator -(Size first, OffsetBase other)
        {
            if (other is Size size)
                return new Offset(first.Width - size.Width, first.Height - size.Height);
            if (other is Offset offset)
                return new Size(first.Width - offset.Dx, first.Height - offset.Dy);

            throw new ArgumentException(other.ToString());
        }

        /// Binary addition operator for adding an [Offset] to a [Size].
        ///
        /// Returns a [Size] whose [width] is the sum of the [width] of the
        /// left-hand-side operand, a [Size], and the [Offset.dx] dimension of the
        /// right-hand-side operand, an [Offset], and whose [height] is the sum of the
        /// [height] of the left-hand-side operand and the [Offset.dy] dimension of
        /// the right-hand-side operand.
        public static Size operator +(Size size, Offset other) => new Size(size.Width + other.Dx, size.Height + other.Dy);

        /// Binary addition operator for adding an [Offset] to a [Size].
        ///
        /// Returns a [Size] whose [width] is the sum of the [width] of the
        /// left-hand-side operand, a [Size], and the [Offset.dx] dimension of the
        /// right-hand-side operand, an [Offset], and whose [height] is the sum of the
        /// [height] of the left-hand-side operand and the [Offset.dy] dimension of
        /// the right-hand-side operand.
        public static Size operator *(Size size, double operand) => new Size(size.Width * operand, size.Height * operand);

        /// Division operator.
        ///
        /// Returns a [Size] whose dimensions are the dimensions of the left-hand-side
        /// operand (a [Size]) divided by the scalar right-hand-side operand (a
        /// [double]).
        public static Size operator /(Size size, double operand) => new Size(size.Width / operand, size.Height / operand);

        /*/// Integer (truncating) division operator. => THIS OPERATOR DON'T EXIST ON CSHARP
        ///
        /// Returns a [Size] whose dimensions are the dimensions of the left-hand-side
        /// operand (a [Size]) divided by the scalar right-hand-side operand (a
        /// [double]), rounded towards zero.
        // Size operator ~/(double operand) => Size((width ~/ operand).toDouble(), (height ~/ operand).toDouble());*/

        /// Modulo (remainder) operator.
        ///
        /// Returns a [Size] whose dimensions are the remainder of dividing the
        /// left-hand-side operand (a [Size]) by the scalar right-hand-side operand (a
        /// [double]).
        public static Size operator %(Size size, double operand) => new Size(size.Width % operand, size.Height % operand);

        /// The lesser of the magnitudes of the [width] and the [height].
        public double ShortestSide => Math.Min(Math.Abs(Width), Math.Abs(Height));

        /// The greater of the magnitudes of the [width] and the [height].
        public double LongestSide => Math.Max(Math.Abs(Width), Math.Abs(Height));

        // Convenience methods that do the equivalent of calling the similarly named
        // methods on a Rect constructed from the given origin and this size.

        /// The offset to the intersection of the top and left edges of the rectangle
        /// described by the given [Offset] (which is interpreted as the top-left corner)
        /// and this [Size].
        ///
        /// See also [Rect.topLeft].
        public Offset TopLeft(Offset origin) => origin;

        /// The offset to the center of the top edge of the rectangle described by the
        /// given offset (which is interpreted as the top-left corner) and this size.
        ///
        /// See also [Rect.topCenter].
        public Offset TopCenter(Offset origin) => new Offset(origin.Dx + Width / 2.0, origin.Dy);

        /// The offset to the intersection of the top and right edges of the rectangle
        /// described by the given offset (which is interpreted as the top-left corner)
        /// and this size.
        ///
        /// See also [Rect.topRight].
        public Offset TopRight(Offset origin) => new Offset(origin.Dx + Width, origin.Dy);

        /// The offset to the center of the left edge of the rectangle described by the
        /// given offset (which is interpreted as the top-left corner) and this size.
        ///
        /// See also [Rect.centerLeft].
        public Offset CenterLeft(Offset origin) => new Offset(origin.Dx, origin.Dy + Height / 2.0);

        /// The offset to the point halfway between the left and right and the top and
        /// bottom edges of the rectangle described by the given offset (which is
        /// interpreted as the top-left corner) and this size.
        ///
        /// See also [Rect.center].
        public Offset Center(Offset origin) => new Offset(origin.Dx + Width / 2.0, origin.Dy + Height / 2.0);

        /// The offset to the center of the right edge of the rectangle described by the
        /// given offset (which is interpreted as the top-left corner) and this size.
        ///
        /// See also [Rect.centerLeft].
        public Offset CenterRight(Offset origin) => new Offset(origin.Dx + Width, origin.Dy + Height / 2.0);

        /// The offset to the intersection of the bottom and left edges of the
        /// rectangle described by the given offset (which is interpreted as the
        /// top-left corner) and this size.
        ///
        /// See also [Rect.bottomLeft].
        public Offset BottomLeft(Offset origin) => new Offset(origin.Dx, origin.Dy + Height);

        /// The offset to the center of the bottom edge of the rectangle described by
        /// the given offset (which is interpreted as the top-left corner) and this
        /// size.
        ///
        /// See also [Rect.bottomLeft].
        public Offset BottomCenter(Offset origin) => new Offset(origin.Dx + Width / 2.0, origin.Dy + Height);

        /// The offset to the intersection of the bottom and right edges of the
        /// rectangle described by the given offset (which is interpreted as the
        /// top-left corner) and this size.
        ///
        /// See also [Rect.bottomRight].
        public Offset BottomRight(Offset origin) => new Offset(origin.Dx + Width, origin.Dy + Height);

        /// Whether the point specified by the given offset (which is assumed to be
        /// relative to the top left of the size) lies between the left and right and
        /// the top and bottom edges of a rectangle of this size.
        ///
        /// Rectangles include their top and left edges but exclude their bottom and
        /// right edges.
        public bool Contains(Offset offset)
        {
            return offset.Dx >= 0.0 && offset.Dx < Width && offset.Dy >= 0.0 && offset.Dy < Height;
        }

        /// A [Size] with the [width] and [height] swapped.
        public Size Flipped => new Size(Height, Width);

        /// Linearly interpolate between two sizes
        ///
        /// If either size is null, this function interpolates from [Size.zero].
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
        public static Size Lerp(Size a, Size b, double t)
        {
            if (a == null && b == null)
                return null;
            if (a == null)
                return b * t;
            if (b == null)
                return a * (1.0 - t);
            return new Size(LerpDouble(a.Width, b.Width, t), LerpDouble(a.Height, b.Height, t));
        }

        /// Compares two Sizes for equality.
        // We don't compare the runtimeType because of _DebugSize in the framework.
        public override bool Equals(object obj)
        {
            if (obj is Size size)
                return Width == size.Width &&
                       Height == size.Height;

            return false;
        }

        public override int GetHashCode()
        {
            return HashValues(Width, Height);
        }

        public override string ToString()
        {
            return $"Size({Width.ToStringAsFixed(1)}, {Height.ToStringAsFixed(1)})";
        }
    }
}
