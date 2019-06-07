using System;
using static FlutterSharp.UI.Lerp;
using static FlutterSharp.UI.UITypes;

namespace FlutterSharp.UI
{
    /// An immutable rounded rectangle with the custom radii for all four corners. 
    public class RRect
    {
        // This constructor replace the _raw method of the original source code
        public RRect(double left = 0.0,
            double top = 0.0,
            double right = 0.0,
            double bottom = 0.0,
            double tlRadiusX = 0.0,
            double tlRadiusY = 0.0,
            double trRadiusX = 0.0,
            double trRadiusY = 0.0,
            double brRadiusX = 0.0,
            double brRadiusY = 0.0,
            double blRadiusX = 0.0,
            double blRadiusY = 0.0)
        {
            this.Left = left;
            this.Top = top;
            this.Right = right;
            this.TlRadiusX = tlRadiusX;
            this.TlRadiusY = tlRadiusY;
            this.TrRadiusX = trRadiusX;
            this.TrRadiusY = trRadiusY;
            this.BrRadiusX = brRadiusX;
            this.BrRadiusY = brRadiusY;
            this.BlRadiusX = blRadiusX;
            this.BlRadiusY = blRadiusY;
        }

        /// Construct a rounded rectangle from its left, top, right, and bottom edges,
        /// and the same radii along its horizontal axis and its vertical axis.
        public static RRect FromLTRBXY(double left, double top, double right, double bottom, double radiusX,
            double radiusY) => new RRect(
            top: top,
            left: left,
            right: right,
            bottom: bottom,
            tlRadiusX: radiusX,
            tlRadiusY: radiusY,
            trRadiusX: radiusX,
            trRadiusY: radiusY,
            blRadiusX: radiusX,
            blRadiusY: radiusY,
            brRadiusX: radiusX,
            brRadiusY: radiusY);

        /// Construct a rounded rectangle from its left, top, right, and bottom edges,
        /// and the same radius in each corner.
        public static RRect FromLTRBR(double left, double top, double right, double bottom, Radius radius)
            => new RRect(
                top: top,
                left: left,
                right: right,
                bottom: bottom,
                tlRadiusX: radius.X,
                tlRadiusY: radius.Y,
                trRadiusX: radius.X,
                trRadiusY: radius.Y,
                blRadiusX: radius.X,
                blRadiusY: radius.Y,
                brRadiusX: radius.X,
                brRadiusY: radius.Y
            );

        /// Construct a rounded rectangle from its bounding box and the same radii
        /// along its horizontal axis and its vertical axis.
        public static RRect FromRectXY(Rect rect, double radiusX, double radiusY)
            => new RRect(
                top: rect.Top,
                left: rect.Left,
                right: rect.Right,
                bottom: rect.Bottom,
                tlRadiusX: radiusX,
                tlRadiusY: radiusY,
                trRadiusX: radiusX,
                trRadiusY: radiusY,
                blRadiusX: radiusX,
                blRadiusY: radiusY,
                brRadiusX: radiusX,
                brRadiusY: radiusY
            );

        /// Construct a rounded rectangle from its bounding box and a radius that is
        /// the same in each corner.
        public static RRect FromRectAndRadius(Rect rect, Radius radius)
            => new RRect(
                top: rect.Top,
                left: rect.Left,
                right: rect.Right,
                bottom: rect.Bottom,
                tlRadiusX: radius.X,
                tlRadiusY: radius.Y,
                trRadiusX: radius.X,
                trRadiusY: radius.Y,
                blRadiusX: radius.X,
                blRadiusY: radius.Y,
                brRadiusX: radius.X,
                brRadiusY: radius.Y
            );

        /// Construct a rounded rectangle from its left, top, right, and bottom edges,
        /// and topLeft, topRight, bottomRight, and bottomLeft radii.
        ///
        /// The corner radii default to [Radius.zero], i.e. right-angled corners.
        public static RRect FromLTRBAndCorners(
            double left,
            double top,
            double right,
            double bottom,
            Radius topLeft = null,
            Radius topRight = null,
            Radius bottomRight = null,
            Radius bottomLeft = null)
        {
            if (topLeft == null)
                topLeft = Radius.Zero;
            if (topRight == null)
                topRight = Radius.Zero;
            if (bottomRight == null)
                bottomRight = Radius.Zero;
            if (bottomLeft == null)
                bottomLeft = Radius.Zero;

            return new RRect(
                top: top,
                left: left,
                right: right,
                bottom: bottom,
                tlRadiusX: topLeft.X,
                tlRadiusY: topLeft.Y,
                trRadiusX: topRight.X,
                trRadiusY: topRight.Y,
                blRadiusX: bottomLeft.X,
                blRadiusY: bottomLeft.Y,
                brRadiusX: bottomRight.X,
                brRadiusY: bottomRight.Y
            );
        }

        /// Construct a rounded rectangle from its bounding box and and topLeft,
        /// topRight, bottomRight, and bottomLeft radii.
        ///
        /// The corner radii default to [Radius.zero], i.e. right-angled corners
        public static RRect FromRectAndCorners(Rect rect, Radius topLeft = null, Radius topRight = null,
            Radius bottomRight = null, Radius bottomLeft = null)
        {
            if (topLeft == null)
                topLeft = Radius.Zero;
            if (topRight == null)
                topRight = Radius.Zero;
            if (bottomRight == null)
                bottomRight = Radius.Zero;
            if (bottomLeft == null)
                bottomLeft = Radius.Zero;

            return new RRect(
                top: rect.Top,
                left: rect.Left,
                right: rect.Right,
                bottom: rect.Bottom,
                tlRadiusX: topLeft.X,
                tlRadiusY: topLeft.Y,
                trRadiusX: topRight.X,
                trRadiusY: topRight.Y,
                blRadiusX: bottomLeft.X,
                blRadiusY: bottomLeft.Y,
                brRadiusX: bottomRight.X,
                brRadiusY: bottomRight.Y
            );
        }

        internal Float32List _value32 => Float32List.FromList(Left,
            Top,
            Right,
            Bottom,
            TlRadiusX,
            TlRadiusY,
            TrRadiusX,
            TrRadiusY,
            BrRadiusX,
            BrRadiusY,
            BlRadiusX,
            BlRadiusY);

        /// The offset of the left edge of this rectangle from the x axis.
        public readonly double Left;

        /// The offset of the top edge of this rectangle from the y axis.
        public readonly double Top;

        /// The offset of the right edge of this rectangle from the x axis.
        public readonly double Right;

        /// The offset of the bottom edge of this rectangle from the y axis.
        public readonly double Bottom;

        /// The top-left horizontal radius.
        public readonly double TlRadiusX;

        /// The top-left vertical radius.
        public readonly double TlRadiusY;

        /// The top-left [Radius].
        public Radius TlRadius => Radius.Elliptical(TlRadiusX, TlRadiusY);

        /// The top-right horizontal radius.
        public readonly double TrRadiusX;

        /// The top-right vertical radius.
        public readonly double TrRadiusY;

        /// The top-right [Radius].
        public Radius TrRadius => Radius.Elliptical(TrRadiusX, TrRadiusY);

        /// The bottom-right horizontal radius.
        public readonly double BrRadiusX;

        /// The bottom-right vertical radius.
        public readonly double BrRadiusY;

        /// The bottom-right [Radius].
        public Radius BrRadius => Radius.Elliptical(BrRadiusX, BrRadiusY);

        /// The bottom-left horizontal radius.
        public readonly double BlRadiusX;

        /// The bottom-left vertical radius.
        public readonly double BlRadiusY;

        /// The bottom-left [Radius].
        public Radius BlRadius => Radius.Elliptical(BlRadiusX, BlRadiusY);

        /// A rounded rectangle with all the values set to zero.
        public static readonly RRect Zero = new RRect();


        /// Returns a new [RRect] translated by the given offset.
        public RRect Shift(Offset offset)
        {
            return new RRect(
                left: Left + offset.Dx,
                top: Top + offset.Dy,
                right: Right + offset.Dx,
                bottom: Bottom + offset.Dy,
                tlRadiusX: TlRadiusX,
                tlRadiusY: TlRadiusY,
                trRadiusX: TrRadiusX,
                trRadiusY: TrRadiusY,
                blRadiusX: BlRadiusX,
                blRadiusY: BlRadiusY,
                brRadiusX: BrRadiusX,
                brRadiusY: BrRadiusY
            );
        }

        /// Returns a new [RRect] with edges and radii moved outwards by the given
        /// delta.
        public RRect Inflate(double delta)
        {
            return new RRect(
                left: Left - delta,
                top: Top - delta,
                right: Right + delta,
                bottom: Bottom + delta,
                tlRadiusX: TlRadiusX + delta,
                tlRadiusY: TlRadiusY + delta,
                trRadiusX: TrRadiusX + delta,
                trRadiusY: TrRadiusY + delta,
                blRadiusX: BlRadiusX + delta,
                blRadiusY: BlRadiusY + delta,
                brRadiusX: BrRadiusX + delta,
                brRadiusY: BrRadiusY + delta
            );
        }

        /// Returns a new [RRect] with edges and radii moved inwards by the given delta.
        public RRect Deflate(double delta) => Inflate(-delta);

        /// The distance between the left and right edges of this rectangle.
        public double Width => Right - Left;

        /// The distance between the top and bottom edges of this rectangle.
        public double Height => Bottom - Top;

        /// The bounding box of this rounded rectangle (the rectangle with no rounded corners).
        public Rect OuterRect => Rect.FromLTRB(Left, Top, Right, Bottom);

        /// The non-rounded rectangle that is constrained by the smaller of the two
        /// diagonals, with each diagonal traveling through the middle of the curve
        /// corners. The middle of a corner is the intersection of the curve with its
        /// respective quadrant bisector.
        public Rect SafeInnerRect
        {
            get
            {
                const double kInsetFactor = 0.29289321881; // 1-cos(pi/4)

                var leftRadius = Math.Max(BlRadiusX, TlRadiusX);
                var topRadius = Math.Max(TlRadiusY, TrRadiusY);
                var rightRadius = Math.Max(TrRadiusX, BrRadiusX);
                var bottomRadius = Math.Max(BrRadiusY, BlRadiusY);

                return Rect.FromLTRB(
                    Left + leftRadius * kInsetFactor,
                    Top + topRadius * kInsetFactor,
                    Right - rightRadius * kInsetFactor,
                    Bottom - bottomRadius * kInsetFactor
                );
            }
        }

        /// The rectangle that would be formed using the axis-aligned intersection of
        /// the sides of the rectangle, i.e., the rectangle formed from the
        /// inner-most centers of the ellipses that form the corners. This is the
        /// intersection of the [wideMiddleRect] and the [tallMiddleRect]. If any of
        /// the intersections are void, the resulting [Rect] will have negative width
        /// or height.
        public Rect MiddleRect
        {
            get
            {
                var leftRadius = Math.Max(BlRadiusX, TlRadiusX);
                var topRadius = Math.Max(TlRadiusY, TrRadiusY);
                var rightRadius = Math.Max(TrRadiusX, BrRadiusX);
                var bottomRadius = Math.Max(BrRadiusY, BlRadiusY);

                return Rect.FromLTRB(
                    Left + leftRadius,
                    Top + topRadius,
                    Right - rightRadius,
                    Bottom - bottomRadius
                );
            }
        }

        /// The biggest rectangle that is entirely inside the rounded rectangle and
        /// has the full width of the rounded rectangle. If the rounded rectangle does
        /// not have an axis-aligned intersection of its left and right side, the
        /// resulting [Rect] will have negative width or height.
        public Rect WideMiddleRect
        {
            get
            {
                var topRadius = Math.Max(TlRadiusY, TrRadiusY);
                var bottomRadius = Math.Max(BrRadiusY, BlRadiusY);

                return Rect.FromLTRB(
                    Left,
                    Top + topRadius,
                    Right,
                    Bottom - bottomRadius);
            }
        }

        /// The biggest rectangle that is entirely inside the rounded rectangle and
        /// has the full height of the rounded rectangle. If the rounded rectangle
        /// does not have an axis-aligned intersection of its top and bottom side, the
        /// resulting [Rect] will have negative width or height.
        public Rect TallMiddleRect
        {
            get
            {
                var leftRadius = Math.Max(BlRadiusX, TlRadiusX);
                var rightRadius = Math.Max(TrRadiusX, BrRadiusX);

                return Rect.FromLTRB(
                    Left + leftRadius,
                    Top,
                    Right - rightRadius,
                    Bottom
                );
            }
        }

        /// Whether this rounded rectangle encloses a non-zero area.
        /// Negative areas are considered empty.
        public bool IsEmpty => Left >= Right || Top >= Bottom;

        /// Whether all coordinates of this rounded rectangle are finite.
        public bool IsFinite => Left.IsFinite() && Top.IsFinite() && Right.IsFinite() && Bottom.IsFinite();

        /// Whether this rounded rectangle is a simple rectangle with zero
        /// corner radii.
        public bool IsRect =>
            (TlRadiusX == 0.0 || TlRadiusY == 0.0) &&
            (TrRadiusX == 0.0 || TrRadiusY == 0.0) &&
            (BlRadiusX == 0.0 || BlRadiusY == 0.0) &&
            (BrRadiusX == 0.0 || BrRadiusY == 0.0);

        /// Whether this rounded rectangle has a side with no straight section.
        public bool IsStadium =>
            TlRadius == TrRadius
            && TrRadius == BrRadius
            && BrRadius == BlRadius
            && (Width <= 2.0 * TlRadiusX || Height <= 2.0 * TlRadiusY);

        /// Whether this rounded rectangle has no side with a straight section.
        public bool IsEllipse =>
            TlRadius == TrRadius
            && TrRadius == BrRadius
            && BrRadius == BlRadius
            && Width <= 2.0 * TlRadiusX
            && Height <= 2.0 * TlRadiusY;

        /// Whether this rounded rectangle would draw as a circle.
        public bool IsCircle => Width == Height && IsEllipse;

        /// The lesser of the magnitudes of the [width] and the [height] of this
        /// rounded rectangle.
        public double ShortestSide => Math.Min(Math.Abs(Width), Math.Abs(Height));

        /// The greater of the magnitudes of the [width] and the [height] of this
        /// rounded rectangle.
        public double LongestSide => Math.Max(Math.Abs(Width), Math.Abs(Height));

        /// Whether any of the dimensions are `NaN`.
        public bool HasNaN => double.IsNaN(Left) || double.IsNaN(Top) || double.IsNaN(Right) || double.IsNaN(Bottom) ||
                              double.IsNaN(TrRadiusX) || double.IsNaN(TrRadiusY) || double.IsNaN(TlRadiusX) ||
                              double.IsNaN(TlRadiusY) ||
                              double.IsNaN(BrRadiusX) || double.IsNaN(BrRadiusY) || double.IsNaN(BlRadiusX) ||
                              double.IsNaN(BlRadiusY);

        /// The offset to the point halfway between the left and right and the top and
        /// bottom edges of this rectangle.
        public Offset Center => new Offset(Left + Width / 2.0, Top + Height / 2.0);

        // Returns the minimum between min and scale to which radius1 and radius2
        // should be scaled with in order not to exceed the limit.
        private double GetMin(double min, double radius1, double radius2, double limit)
        {
            double sum = radius1 + radius2;
            if (sum > limit && sum != 0.0)
                return Math.Min(min, limit / sum);
            return min;
        }

        // Scales all radii so that on each side their sum will not pass the size of
        // the width/height.
        //
        // Inspired from:
        //   https://github.com/google/skia/blob/master/src/core/SkRRect.cpp#L164
        private RRect ScaleRadii()
        {
            double scale = 1.0;
            scale = GetMin(scale, BlRadiusY, TlRadiusY, Height);
            scale = GetMin(scale, TlRadiusX, TrRadiusX, Width);
            scale = GetMin(scale, TrRadiusY, BrRadiusY, Height);
            scale = GetMin(scale, BrRadiusX, BlRadiusX, Width);

            if (scale < 1.0)
            {
                return new RRect(
                    top: Top,
                    left: Left,
                    right: Right,
                    bottom: Bottom,
                    tlRadiusX: TlRadiusX * scale,
                    tlRadiusY: TlRadiusY * scale,
                    trRadiusX: TrRadiusX * scale,
                    trRadiusY: TrRadiusY * scale,
                    blRadiusX: BlRadiusX * scale,
                    blRadiusY: BlRadiusY * scale,
                    brRadiusX: BrRadiusX * scale,
                    brRadiusY: BrRadiusY * scale
                );
            }

            return new RRect(
                top: Top,
                left: Left,
                right: Right,
                bottom: Bottom,
                tlRadiusX: TlRadiusX,
                tlRadiusY: TlRadiusY,
                trRadiusX: TrRadiusX,
                trRadiusY: TrRadiusY,
                blRadiusX: BlRadiusX,
                blRadiusY: BlRadiusY,
                brRadiusX: BrRadiusX,
                brRadiusY: BrRadiusY
            );
        }

        /// Whether the point specified by the given offset (which is assumed to be
        /// relative to the origin) lies inside the rounded rectangle.
        ///
        /// This method may allocate (and cache) a copy of the object with normalized
        /// radii the first time it is called on a particular [RRect] instance. When
        /// using this method, prefer to reuse existing [RRect]s rather than
        /// recreating the object each time.
        public bool Contains(Offset point)
        {
            if (point.Dx < Left || point.Dx >= Right || point.Dy < Top || point.Dy >= Bottom)
                return false; // outside bounding box

            RRect scaled = ScaleRadii();

            double x;
            double y;
            double radiusX;
            double radiusY;
            // check whether point is in one of the rounded corner areas
            // x, y -> translate to ellipse center
            if (point.Dx < Left + scaled.TlRadiusX &&
                point.Dy < Top + scaled.TlRadiusY)
            {
                x = point.Dx - Left - scaled.TlRadiusX;
                y = point.Dy - Top - scaled.TlRadiusY;
                radiusX = scaled.TlRadiusX;
                radiusY = scaled.TlRadiusY;
            }
            else if (point.Dx > Right - scaled.TrRadiusX &&
                     point.Dy < Top + scaled.TrRadiusY)
            {
                x = point.Dx - Right + scaled.TrRadiusX;
                y = point.Dy - Top - scaled.TrRadiusY;
                radiusX = scaled.TrRadiusX;
                radiusY = scaled.TrRadiusY;
            }
            else if (point.Dx > Right - scaled.BrRadiusX &&
                     point.Dy > Bottom - scaled.BrRadiusY)
            {
                x = point.Dx - Right + scaled.BrRadiusX;
                y = point.Dy - Bottom + scaled.BrRadiusY;
                radiusX = scaled.BrRadiusX;
                radiusY = scaled.BrRadiusY;
            }
            else if (point.Dx < Left + scaled.BlRadiusX &&
                     point.Dy > Bottom - scaled.BlRadiusY)
            {
                x = point.Dx - Left - scaled.BlRadiusX;
                y = point.Dy - Bottom + scaled.BlRadiusY;
                radiusX = scaled.BlRadiusX;
                radiusY = scaled.BlRadiusY;
            }
            else
            {
                return true; // inside and not within the rounded corner area
            }

            x = x / radiusX;
            y = y / radiusY;
            // check if the point is outside the unit circle
            if (x * x + y * y > 1.0)
                return false;
            return true;
        }

        /// Linearly interpolate between two rounded rectangles.
        ///
        /// If either is null, this function substitutes [RRect.zero] instead.
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
        public static RRect Lerp(RRect a, RRect b, double t)
        {
            if (a == null && b == null)
                return null;
            if (a == null)
            {
                return new RRect(
                    left: b.Left * t,
                    top: b.Top * t,
                    right: b.Right * t,
                    bottom: b.Bottom * t,
                    tlRadiusX: b.TlRadiusX * t,
                    tlRadiusY: b.TlRadiusY * t,
                    trRadiusX: b.TrRadiusX * t,
                    trRadiusY: b.TrRadiusY * t,
                    brRadiusX: b.BrRadiusX * t,
                    brRadiusY: b.BrRadiusY * t,
                    blRadiusX: b.BlRadiusX * t,
                    blRadiusY: b.BlRadiusY * t
                );
            }

            if (b == null)
            {
                double k = 1.0 - t;
                return new RRect(
                    left: a.Left * k,
                    top: a.Top * k,
                    right: a.Right * k,
                    bottom: a.Bottom * k,
                    tlRadiusX: a.TlRadiusX * k,
                    tlRadiusY: a.TlRadiusY * k,
                    trRadiusX: a.TrRadiusX * k,
                    trRadiusY: a.TrRadiusY * k,
                    brRadiusX: a.BrRadiusX * k,
                    brRadiusY: a.BrRadiusY * k,
                    blRadiusX: a.BlRadiusX * k,
                    blRadiusY: a.BlRadiusY * k
                );
            }

            return new RRect(
                left: LerpDouble(a.Left, b.Left, t),
                top: LerpDouble(a.Top, b.Top, t),
                right: LerpDouble(a.Right, b.Right, t),
                bottom: LerpDouble(a.Bottom, b.Bottom, t),
                tlRadiusX: LerpDouble(a.TlRadiusX, b.TlRadiusY, t),
                tlRadiusY: LerpDouble(a.TlRadiusY, b.TlRadiusY, t),
                trRadiusX: LerpDouble(a.TrRadiusX, b.TrRadiusX, t),
                trRadiusY: LerpDouble(a.TrRadiusY, b.TrRadiusY, t),
                brRadiusX: LerpDouble(a.BrRadiusX, b.BrRadiusX, t),
                brRadiusY: LerpDouble(a.BrRadiusY, b.BrRadiusY, t),
                blRadiusX: LerpDouble(a.BlRadiusX, b.BlRadiusX, t),
                blRadiusY: LerpDouble(a.BlRadiusY, b.BlRadiusY, t)
            );
        }

        public override bool Equals(object obj)
        {
            if (obj is RRect typedOther)
                return Left == typedOther.Left &&
                       Top == typedOther.Top &&
                       Right == typedOther.Right &&
                       Bottom == typedOther.Bottom &&
                       TlRadiusX == typedOther.TlRadiusX &&
                       TlRadiusY == typedOther.TlRadiusY &&
                       TrRadiusX == typedOther.TrRadiusX &&
                       TrRadiusY == typedOther.TrRadiusY &&
                       BlRadiusX == typedOther.BlRadiusX &&
                       BlRadiusY == typedOther.BlRadiusY &&
                       BrRadiusX == typedOther.BrRadiusX &&
                       BrRadiusY == typedOther.BrRadiusY;

            return false;
        }

        public override int GetHashCode()
        {
            return HashValues(Left,
                Top,
                Right,
                Bottom,
                TlRadiusX,
                TlRadiusY,
                TrRadiusX,
                TrRadiusY,
                BrRadiusX,
                BrRadiusY,
                BlRadiusX,
                BlRadiusY);
        }

        public override string ToString()
        {
            string rect = $"{Left.ToStringAsFixed(1)}, {Top.ToStringAsFixed(1)}, {Right.ToStringAsFixed(1)}, {Bottom.ToStringAsFixed(1)}";

            if (TlRadius == TrRadius &&
                TrRadius == BrRadius &&
                BrRadius == BlRadius)
            {
                if (TlRadius.X == TlRadius.Y)
                    return $"RRect.FromLTRBR({rect}, {TlRadius.X.ToStringAsFixed(1)})";

                return $"RRect.FromLTRBXY({rect}, {TlRadius.X.ToStringAsFixed(1)}, {TlRadius.Y.ToStringAsFixed(1)})";
            }

            return "RRect.fromLTRBAndCorners(" +
                $"{rect}, " +
                $"topLeft: {TlRadius}, " +
                $"topRight: {TrRadius}, " +
                $"bottomRight: {BrRadius}, " +
                $"bottomLeft: {BlRadius})";
        }
    }
}