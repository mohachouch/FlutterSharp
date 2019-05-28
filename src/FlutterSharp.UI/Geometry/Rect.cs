using System;
using static FlutterSharp.UI.Lerp;
using static FlutterSharp.UI.UITypes;

namespace FlutterSharp.UI
{
    /// An immutable, 2D, axis-aligned, floating-point rectangle whose coordinates
    /// are relative to a given origin.
    ///
    /// A Rect can be created with one its constructors or from an [Offset] and a
    /// [Size] using the `&` operator:
    ///
    /// ```dart
    /// Rect myRect = const Offset(1.0, 2.0) & const Size(3.0, 4.0);
    /// ```
    public class Rect
    {
        public Rect(double left, double top, double right, double bottom)
        {
            this.Left = left;
            this.Top = top;
            this.Right = right;
            this.Bottom = bottom;
        }

        /// Construct a rectangle from its left, top, right, and bottom edges.
        public static Rect FromLTRB(double left, double top, double right, double bottom) => new Rect(left, top, right, bottom);

        /// Construct a rectangle from its left and top edges, its width, and its
        /// height.
        ///
        /// To construct a [Rect] from an [Offset] and a [Size], you can use the
        /// rectangle constructor operator `&`. See [Offset.&].
        public static Rect FromLTWH(double left, double top, double width, double height) => new Rect(left, top, left + width, top + height);

        /// Construct a rectangle that bounds the given circle.
        ///
        /// The `center` argument is assumed to be an offset from the origin.
        public static Rect FromCircle(Offset center = null, double radius = 0.0) => Rect.FromCenter(center, radius * 2, radius * 2);

        /// Constructs a rectangle from its center point, width, and height.
        ///
        /// The `center` argument is assumed to be an offset from the origin.
        /// 
        public static Rect FromCenter(Offset center = null, double width = 0.0, double height = 0.0)
        {
            if (center == null)
                center = Offset.Zero;

            return Rect.FromLTRB(center.Dx - width / 2,
                center.Dy - height / 2,
                center.Dx + width / 2,
                center.Dy + height / 2);
        }

        /// Construct the smallest rectangle that encloses the given offsets, treating
        /// them as vectors from the origin.
        public static Rect FromPoints(Offset a, Offset b)
        {
            return Rect.FromLTRB(
                    Math.Min(a.Dx, b.Dx),
                    Math.Min(a.Dy, b.Dy),
                    Math.Max(a.Dx, b.Dx),
                    Math.Max(a.Dy, b.Dy));
        }

        private Float32List _value32 => Float32List.FromList(Left, Top, Right, Bottom);

        /// The offset of the left edge of this rectangle from the x axis.
        public readonly double Left;

        /// The offset of the top edge of this rectangle from the y axis.
        public readonly double Top;

        /// The offset of the right edge of this rectangle from the x axis.
        public readonly double Right;

        /// The offset of the bottom edge of this rectangle from the y axis.
        public readonly double Bottom;

        /// The distance between the left and right edges of this rectangle.
        public double Width => Right - Left;

        /// The distance between the top and bottom edges of this rectangle.
        public double Height => Bottom - Top;

        /// The distance between the upper-left corner and the lower-right corner of
        /// this rectangle.
        public Size Size => new Size(Width, Height);

        /// Whether any of the dimensions are `NaN`.
        public bool HasNaN => double.IsNaN(Left) || double.IsNaN(Top) || double.IsNaN(Right) || double.IsNaN(Bottom);

        /// A rectangle with left, top, right, and bottom edges all at zero.
        public static readonly Rect Zero = Rect.FromLTRB(0.0, 0.0, 0.0, 0.0);

        private static readonly double _giantScalar = 1.0E+9; // matches kGiantRect from layer.h

        /// A rectangle that covers the entire coordinate space.
        ///
        /// This covers the space from -1e9,-1e9 to 1e9,1e9.
        /// This is the space over which graphics operations are valid.
        public static readonly Rect Largest = Rect.FromLTRB(-_giantScalar, -_giantScalar, _giantScalar, _giantScalar);

        /// Whether any of the coordinates of this rectangle are equal to positive infinity.
        // included for consistency with Offset and Size
        public bool IsInfinite => Left >= double.PositiveInfinity || Top >= double.PositiveInfinity || Right >= double.PositiveInfinity || Bottom >= double.PositiveInfinity;

        /// Whether all coordinates of this rectangle are finite.
        public bool IsFinite => Left.IsFinite() && Top.IsFinite() && Right.IsFinite() && Bottom.IsFinite();

        /// Whether this rectangle encloses a non-zero area. Negative areas are
        /// considered empty.
        public bool IsEmpty => Left >= Right || Top >= Bottom;

        /// Returns a new rectangle translated by the given offset.
        ///
        /// To translate a rectangle by separate x and y components rather than by an
        /// [Offset], consider [translate].
        public Rect Shift(Offset offset)
        {
            return Rect.FromLTRB(Left + offset.Dx, Top + offset.Dy, Right + offset.Dx, Bottom + offset.Dy);
        }

        /// Returns a new rectangle with translateX added to the x components and
        /// translateY added to the y components.
        ///
        /// To translate a rectangle by an [Offset] rather than by separate x and y
        /// components, consider [shift].
        public Rect Translate(double translateX, double translateY)
        {
            return Rect.FromLTRB(Left + translateX, Top + translateY, Right + translateX, Bottom + translateY);
        }

        /// Returns a new rectangle with edges moved outwards by the given delta.
        public Rect Inflate(double delta)
        {
            return Rect.FromLTRB(Left - delta, Top - delta, Right + delta, Bottom + delta);
        }

        /// Returns a new rectangle with edges moved inwards by the given delta.
        public Rect Deflate(double delta) => this.Inflate(-delta);

        /// Returns a new rectangle that is the intersection of the given
        /// rectangle and this rectangle. The two rectangles must overlap
        /// for this to be meaningful. If the two rectangles do not overlap,
        /// then the resulting Rect will have a negative width or height.
        public Rect Intersect(Rect other)
        {
            return Rect.FromLTRB(
                Math.Max(Left, other.Left),
                Math.Max(Top, other.Top),
                Math.Min(Right, other.Right),
                Math.Min(Bottom, other.Bottom)
            );
        }

        /// Returns a new rectangle which is the bounding box containing this
        /// rectangle and the given rectangle.
        public Rect ExpandToInclude(Rect other)
        {
            return Rect.FromLTRB(
                Math.Min(Left, other.Left),
                Math.Min(Top, other.Top),
                Math.Max(Right, other.Right),
                Math.Max(Bottom, other.Bottom)
            );
        }

        /// Whether `other` has a nonzero area of overlap with this rectangle.
        public bool Overlaps(Rect other)
        {
            if (Right <= other.Left || other.Right <= Left)
                return false;
            if (Bottom <= other.Top || other.Bottom <= Top)
                return false;
            return true;
        }

        /// The lesser of the magnitudes of the [width] and the [height] of this
        /// rectangle.
        public double ShortestSide => Math.Min(Math.Abs(Width), Math.Abs(Height));

        /// The greater of the magnitudes of the [width] and the [height] of this
        /// rectangle.
        public double LongestSide => Math.Max(Math.Abs(Width), Math.Abs(Height));

        /// The offset to the intersection of the top and left edges of this rectangle.
        ///
        /// See also [Size.topLeft].
        public Offset TopLeft => new Offset(Left, Top);

        /// The offset to the center of the top edge of this rectangle.
        ///
        /// See also [Size.topCenter].
        public Offset TopCenter => new Offset(Left + Width / 2.0, Top);

        /// The offset to the intersection of the top and right edges of this rectangle.
        ///
        /// See also [Size.topRight].
        public Offset TopRight => new Offset(Right, Top);

        /// The offset to the center of the left edge of this rectangle.
        ///
        /// See also [Size.centerLeft].
        public Offset CenterLeft => new Offset(Left, Top + Height / 2.0);

        /// The offset to the point halfway between the left and right and the top and
        /// bottom edges of this rectangle.
        ///
        /// See also [Size.center].
        public Offset Center => new Offset(Left + Width / 2.0, Top + Height / 2.0);

        /// The offset to the center of the right edge of this rectangle.
        ///
        /// See also [Size.centerLeft].
        public Offset CenterRight => new Offset(Right, Top + Height / 2.0);

        /// The offset to the intersection of the bottom and left edges of this rectangle.
        ///
        /// See also [Size.bottomLeft].
        public Offset BottomLeft => new Offset(Left, Bottom);

        /// The offset to the center of the bottom edge of this rectangle.
        ///
        /// See also [Size.bottomLeft].
        public Offset BottomCenter => new Offset(Left + Width / 2.0, Bottom);

        /// The offset to the intersection of the bottom and right edges of this rectangle.
        ///
        /// See also [Size.bottomRight].
        public Offset BottomRight => new Offset(Right, Bottom);

        /// Whether the point specified by the given offset (which is assumed to be
        /// relative to the origin) lies between the left and right and the top and
        /// bottom edges of this rectangle.
        ///
        /// Rectangles include their top and left edges but exclude their bottom and
        /// right edges.
        public bool Contains(Offset offset)
        {
            return offset.Dx >= Left && offset.Dx < Right && offset.Dy >= Top && offset.Dy < Bottom;
        }

        /// Linearly interpolate between two rectangles.
        ///
        /// If either rect is null, [Rect.zero] is used as a substitute.
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
        public static Rect Lerp(Rect a, Rect b, double t)
        {
            if (a == null && b == null)
                return null;
            if (a == null)
                return Rect.FromLTRB(b.Left * t, b.Top * t, b.Right * t, b.Bottom * t);
            if (b == null)
            {
                double k = 1.0 - t;
                return Rect.FromLTRB(a.Left * k, a.Top * k, a.Right * k, a.Bottom * k);
            }

            return Rect.FromLTRB(
                LerpDouble(a.Left, b.Left, t),
                LerpDouble(a.Top, b.Top, t),
                LerpDouble(a.Right, b.Right, t),
                LerpDouble(a.Bottom, b.Bottom, t)
            );
        }

        public override bool Equals(object obj)
        {
            if (obj is Rect typedOther)
                return Left == typedOther.Left &&
                       Top == typedOther.Top &&
                       Right == typedOther.Right &&
                       Bottom == typedOther.Bottom;

            return false;
        }

        public override int GetHashCode()
        {
            return HashValues(Left, Top, Right, Bottom);
        }

        public override string ToString()
        {
            return $"Rect({Left.ToStringAsFixed(1)}, {Top.ToStringAsFixed(1)}, {Right.ToStringAsFixed(1)}, {Bottom.ToStringAsFixed(1)})";
        }
    }
}