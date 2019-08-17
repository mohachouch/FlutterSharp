using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using static FlutterSharp.UI.PaintingMethods;

namespace FlutterSharp.UI
{
    /// A complex, one-dimensional subset of a plane.
    ///
    /// A path consists of a number of sub-paths, and a _current point_.
    ///
    /// Sub-paths consist of segments of various types, such as lines,
    /// arcs, or beziers. Sub-paths can be open or closed, and can
    /// self-intersect.
    ///
    /// Closed sub-paths enclose a (possibly discontiguous) region of the
    /// plane based on the current [fillType].
    ///
    /// The _current point_ is initially at the origin. After each
    /// operation adding a segment to a sub-path, the current point is
    /// updated to the end of that segment.
    ///
    /// Paths can be drawn on canvases using [Canvas.drawPath], and can
    /// used to create clip regions using [Canvas.clipPath].
    public class Path : NativeFieldWrapperClass2
    {
        /// Create a new empty [Path] object.
        public Path()
            : base(Path_constructor())
        {
            if (this.Handle == IntPtr.Zero)
            {
                throw new InvalidOperationException("Unable to create a new Path instance.");
            }
        }

        public Path(IntPtr handle)
           : base(handle)
        {
            if (this.Handle == IntPtr.Zero)
            {
                throw new InvalidOperationException("Unable to create a new Path instance.");
            }
        }

        /// Creates a copy of another [Path].
        ///
        /// This copy is fast and does not require additional memory unless either
        /// the `source` path or the path returned by this constructor are modified.
        public static Path From(Path source)
        {
            return source.Clone();
        }

        private Path Clone()
        {
            IntPtr pathHandle = Path_clone(this.Handle);
            return pathHandle != IntPtr.Zero ? new Path(pathHandle) : null;
        }

        /// Determines how the interior of this path is calculated.
        ///
        /// Defaults to the non-zero winding rule, [PathFillType.nonZero].
        public PathFillType FillType
        {
            get => (PathFillType)GetFillType();
            set => SetFillType((int)value);
        }

        private int GetFillType()
        {
            return Path_getFillType(this.Handle);
        }

        private void SetFillType(int fillType)
        {
            Path_setFillType(this.Handle, fillType);
        }

        /// Starts a new sub-path at the given coordinate.
        public void MoveTo(double x, double y)
        {
            Path_moveTo(this.Handle, x, y);
        }

        /// Starts a new sub-path at the given offset from the current point.
        public void RelativeMoveTo(double dx, double dy)
        {
            Path_relativeMoveTo(this.Handle, dx, dy);
        }

        /// Adds a straight line segment from the current point to the given
        /// point.
        public void LineTo(double x, double y)
        {
            Path_lineTo(this.Handle, x, y);
        }

        /// Adds a straight line segment from the current point to the point
        /// at the given offset from the current point.
        public void RelativeLineTo(double dx, double dy)
        {
            Path_relativeLineTo(this.Handle, dx, dy);
        }

        /// Adds a quadratic bezier segment that curves from the current
        /// point to the given point (x2,y2), using the control point
        /// (x1,y1).
        public void QuadraticBezierTo(double x1, double y1, double x2, double y2)
        {
            Path_quadraticBezierTo(this.Handle, x1, y1, x2, y2);
        }

        /// Adds a quadratic bezier segment that curves from the current
        /// point to the point at the offset (x2,y2) from the current point,
        /// using the control point at the offset (x1,y1) from the current
        /// point.
        public void RelativeQuadraticBezierTo(double x1, double y1, double x2, double y2)
        {
            Path_relativeQuadraticBezierTo(this.Handle, x1, y1, x2, y2);
        }

        /// Adds a cubic bezier segment that curves from the current point
        /// to the given point (x3,y3), using the control points (x1,y1) and
        /// (x2,y2).
        public void CubicTo(double x1, double y1, double x2, double y2, double x3, double y3)
        {
            Path_cubicTo(this.Handle, x1, y1, x2, y2, x3, y3);
        }

        /// Adds a cubic bezier segment that curves from the current point
        /// to the point at the offset (x3,y3) from the current point, using
        /// the control points at the offsets (x1,y1) and (x2,y2) from the
        /// current point.
        public void RelativeCubicTo(double x1, double y1, double x2, double y2, double x3, double y3)
        {
            Path_relativeCubicTo(this.Handle, x1, y1, x2, y2, x3, y3);
        }

        /// Adds a bezier segment that curves from the current point to the
        /// given point (x2,y2), using the control points (x1,y1) and the
        /// weight w. If the weight is greater than 1, then the curve is a
        /// hyperbola; if the weight equals 1, it's a parabola; and if it is
        /// less than 1, it is an ellipse.
        public void ConicTo(double x1, double y1, double x2, double y2, double w)
        {
            Path_conicTo(this.Handle, x1, y1, x2, y2, w);
        }

        /// Adds a bezier segment that curves from the current point to the
        /// point at the offset (x2,y2) from the current point, using the
        /// control point at the offset (x1,y1) from the current point and
        /// the weight w. If the weight is greater than 1, then the curve is
        /// a hyperbola; if the weight equals 1, it's a parabola; and if it
        /// is less than 1, it is an ellipse.
        public void RelativeConicTo(double x1, double y1, double x2, double y2, double w)
        {
            Path_relativeConicTo(this.Handle, x1, y1, x2, y2, w);
        }

        /// If the `forceMoveTo` argument is false, adds a straight line
        /// segment and an arc segment.
        ///
        /// If the `forceMoveTo` argument is true, starts a new sub-path
        /// consisting of an arc segment.
        ///
        /// In either case, the arc segment consists of the arc that follows
        /// the edge of the oval bounded by the given rectangle, from
        /// startAngle radians around the oval up to startAngle + sweepAngle
        /// radians around the oval, with zero radians being the point on
        /// the right hand side of the oval that crosses the horizontal line
        /// that intersects the center of the rectangle and with positive
        /// angles going clockwise around the oval.
        ///
        /// The line segment added if `forceMoveTo` is false starts at the
        /// current point and ends at the start of the arc.
        public void ArcTo(Rect rect, double startAngle, double sweepAngle, bool forceMoveTo)
        {
            Debug.Assert(RectIsValid(rect));
            ArcTo(rect.Left, rect.Top, rect.Right, rect.Bottom, startAngle, sweepAngle, forceMoveTo);
        }

        private void ArcTo(double left, double top, double right, double bottom,
                    double startAngle, double sweepAngle, bool forceMoveTo)
        {
            Path_arcTo(this.Handle, left, top, right, bottom, startAngle, sweepAngle, forceMoveTo);
        }

        /// Appends up to four conic curves weighted to describe an oval of `radius`
        /// and rotated by `rotation`.
        ///
        /// The first curve begins from the last point in the path and the last ends
        /// at `arcEnd`. The curves follow a path in a direction determined by
        /// `clockwise` and `largeArc` in such a way that the sweep angle
        /// is always less than 360 degrees.
        ///
        /// A simple line is appended if either either radii are zero or the last
        /// point in the path is `arcEnd`. The radii are scaled to fit the last path
        /// point if both are greater than zero but too small to describe an arc.
        ///
        public void ArcToPoint(Offset arcEnd, Radius radius = null, double rotation = 0.0,
          bool largeArc = false, bool clockwise = true)
        {
            if (radius == null)
                radius = Radius.Zero;

            Debug.Assert(OffsetIsValid(arcEnd));
            Debug.Assert(RadiusIsValid(radius));

            ArcToPoint(arcEnd.Dx, arcEnd.Dy, radius.X, radius.Y, rotation,
                        largeArc, clockwise);
        }

        private void ArcToPoint(double arcEndX, double arcEndY, double radiusX,
                     double radiusY, double rotation, bool largeArc,
                     bool clockwise)
        {
            Path_arcToPoint(this.Handle, arcEndX, arcEndY, radiusX, radiusY, rotation, largeArc, clockwise);
        }

        /// Appends up to four conic curves weighted to describe an oval of `radius`
        /// and rotated by `rotation`.
        ///
        /// The last path point is described by (px, py).
        ///
        /// The first curve begins from the last point in the path and the last ends
        /// at `arcEndDelta.dx + px` and `arcEndDelta.dy + py`. The curves follow a
        /// path in a direction determined by `clockwise` and `largeArc`
        /// in such a way that the sweep angle is always less than 360 degrees.
        ///
        /// A simple line is appended if either either radii are zero, or, both
        /// `arcEndDelta.dx` and `arcEndDelta.dy` are zero. The radii are scaled to
        /// fit the last path point if both are greater than zero but too small to
        /// describe an arc.
        public void RelativeArcToPoint(Offset arcEndDelta, Radius radius = null, double rotation = 0.0,
          bool largeArc = false, bool clockwise = true)
        {
            if (radius == null)
                radius = Radius.Zero;
            Debug.Assert(OffsetIsValid(arcEndDelta));
            Debug.Assert(RadiusIsValid(radius));
            RelativeArcToPoint(arcEndDelta.Dx, arcEndDelta.Dy, radius.X, radius.Y,
                                rotation, largeArc, clockwise);
        }

        private void RelativeArcToPoint(double arcEndX, double arcEndY, double radiusX,
                             double radiusY, double rotation,
                             bool largeArc, bool clockwise)
        {
            Path_relativeArcToPoint(this.Handle, arcEndX, arcEndY, radiusX,
                            radiusY, rotation, largeArc, clockwise);
        }

        /// Adds a new sub-path that consists of four lines that outline the
        /// given rectangle.
        public void AddRect(Rect rect)
        {
            Debug.Assert(RectIsValid(rect));
            AddRect(rect.Left, rect.Top, rect.Right, rect.Bottom);
        }

        private void AddRect(double left, double top, double right, double bottom)
        {
            Path_addRect(this.Handle, left, top, right, bottom);
        }

        /// Adds a new sub-path that consists of a curve that forms the
        /// ellipse that fills the given rectangle.
        ///
        /// To add a circle, pass an appropriate rectangle as `oval`. [Rect.fromCircle]
        /// can be used to easily describe the circle's center [Offset] and radius.
        public void AddOval(Rect oval)
        {
            Debug.Assert(RectIsValid(oval));
            AddOval(oval.Left, oval.Top, oval.Right, oval.Bottom);
        }

        private void AddOval(double left, double top, double right, double bottom)
        {
            Path_addOval(this.Handle, left, top, right, bottom);
        }

        /// Adds a new sub-path with one arc segment that consists of the arc
        /// that follows the edge of the oval bounded by the given
        /// rectangle, from startAngle radians around the oval up to
        /// startAngle + sweepAngle radians around the oval, with zero
        /// radians being the point on the right hand side of the oval that
        /// crosses the horizontal line that intersects the center of the
        /// rectangle and with positive angles going clockwise around the
        /// oval.
        public void AddArc(Rect oval, double startAngle, double sweepAngle)
        {
            Debug.Assert(RectIsValid(oval));
            AddArc(oval.Left, oval.Top, oval.Right, oval.Bottom, startAngle, sweepAngle);
        }

        private void AddArc(double left, double top, double right, double bottom, double startAngle, double sweepAngle)
        {
            Path_addArc(this.Handle, left, top, right, bottom, startAngle, sweepAngle);
        }

        /// Adds a new sub-path with a sequence of line segments that connect the given
        /// points.
        ///
        /// If `close` is true, a final line segment will be added that connects the
        /// last point to the first point.
        ///
        /// The `points` argument is interpreted as offsets from the origin.
        public void AddPolygon(List<Offset> points, bool close)
        {
            Debug.Assert(points != null);
            AddPolygon(EncodePointList(points), close);
        }

        private void AddPolygon(Float32List points, bool close)
        {
            // TODO : native 'Path_addPolygon';
        }

        /// Adds a new sub-path that consists of the straight lines and
        /// curves needed to form the rounded rectangle described by the
        /// argument.
        public void AddRRect(RRect rrect)
        {
            Debug.Assert(RrectIsValid(rrect));
            AddRRect(rrect._value32);
        }

        private void AddRRect(Float32List rrect)
        {
            // TODO : native 'Path_addRRect';
        }

        /// Adds a new sub-path that consists of the given `path` offset by the given
        /// `offset`.
        ///
        /// If `matrix4` is specified, the path will be transformed by this matrix
        /// after the matrix is translated by the given offset. The matrix is a 4x4
        /// matrix stored in column major order.
        public void AddPath(Path path, Offset offset, Float64List matrix4 = null)
        {
            Debug.Assert(path != null); // path is checked on the engine side
            Debug.Assert(OffsetIsValid(offset));
            if (matrix4 != null)
            {
                Debug.Assert(Matrix4IsValid(matrix4));
                AddPathWithMatrix(path, offset.Dx, offset.Dy, matrix4);
            }
            else
            {
                AddPath(path, offset.Dx, offset.Dy);
            }
        }

        private void AddPath(Path path, double dx, double dy)
        {
            Path_addPath(this.Handle, path.Handle, dx, dy);
        }

        private void AddPathWithMatrix(Path path, double dx, double dy, Float64List matrix)
        {
            // TODO :native 'Path_addPathWithMatrix';
        }

        /// Adds the given path to this path by extending the current segment of this
        /// path with the the first segment of the given path.
        ///
        /// If `matrix4` is specified, the path will be transformed by this matrix
        /// after the matrix is translated by the given `offset`.  The matrix is a 4x4
        /// matrix stored in column major order.
        public void ExtendWithPath(Path path, Offset offset, Float64List matrix4 = null)
        {
            Debug.Assert(path != null); // path is checked on the engine side
            Debug.Assert(OffsetIsValid(offset));
            if (matrix4 != null)
            {
                Debug.Assert(Matrix4IsValid(matrix4));
                ExtendWithPathAndMatrix(path, offset.Dx, offset.Dy, matrix4);
            }
            else
            {
                ExtendWithPath(path, offset.Dx, offset.Dy);
            }
        }

        private void ExtendWithPath(Path path, double dx, double dy)
        {
            Path_extendWithPath(this.Handle, path.Handle, dx, dy);
        }

        private void ExtendWithPathAndMatrix(Path path, double dx, double dy, Float64List matrix)
        {
            // TODO : native 'Path_extendWithPathAndMatrix';
        }

        /// Closes the last sub-path, as if a straight line had been drawn
        /// from the current point to the first point of the sub-path.
        public void Close()
        {
            Path_close(this.Handle);
        }
        
        /// Clears the [Path] object of all sub-paths, returning it to the
        /// same state it had when it was created. The _current point_ is
        /// reset to the origin.
        public void Reset()
        {
            Path_reset(this.Handle);
        }
        
        /// Tests to see if the given point is within the path. (That is, whether the
        /// point would be in the visible portion of the path if the path was used
        /// with [Canvas.clipPath].)
        ///
        /// The `point` argument is interpreted as an offset from the origin.
        ///
        /// Returns true if the point is in the path, and false otherwise.
        public bool Contains(Offset point)
        {
            Debug.Assert(OffsetIsValid(point));
            return Contains(point.Dx, point.Dy);
        }

        private bool Contains(double x, double y)
        {
            return Path_contains(this.Handle, x, y);
        }

        /// Returns a copy of the path with all the segments of every
        /// sub-path translated by the given offset.
        public Path Shift(Offset offset)
        {
            Debug.Assert(OffsetIsValid(offset));
            return Shift(offset.Dx, offset.Dy);
        }

        private Path Shift(double dx, double dy)
        {
            IntPtr pathHandle = Path_shift(this.Handle, dx, dy);
            return pathHandle != IntPtr.Zero ? new Path(pathHandle) : null;
        }
        
        /// Returns a copy of the path with all the segments of every
        /// sub-path transformed by the given matrix.
        public Path Transform(Float64List matrix4)
        {
            Debug.Assert(Matrix4IsValid(matrix4));
            // TODO :  native 'Path_transform';
            return null;
        }

        /// Computes the bounding rectangle for this path.
        ///
        /// A path containing only axis-aligned points on the same straight line will
        /// have no area, and therefore `Rect.isEmpty` will return true for such a
        /// path. Consider checking `rect.width + rect.height > 0.0` instead, or
        /// using the [computeMetrics] API to check the path length.
        ///
        /// For many more elaborate paths, the bounds may be inaccurate.  For example,
        /// when a path contains a circle, the points used to compute the bounds are
        /// the circle's implied control points, which form a square around the circle;
        /// if the circle has a transformation applied using [transform] then that
        /// square is rotated, and the (axis-aligned, non-rotated) bounding box
        /// therefore ends up grossly overestimating the actual area covered by the
        /// circle.
        // see https://skia.org/user/api/SkPath_Reference#SkPath_getBounds
        public Rect GetBounds()
        {
            Float32List rect = null; // Float32List GetBounds() => TODO : native 'Path_getBounds';
            return Rect.FromLTRB(rect[0], rect[1], rect[2], rect[3]);
        }

        /// Combines the two paths according to the manner specified by the given
        /// `operation`.
        ///
        /// The resulting path will be constructed from non-overlapping contours. The
        /// curve order is reduced where possible so that cubics may be turned into
        /// quadratics, and quadratics maybe turned into lines.
        public static Path Combine(PathOperation operation, Path path1, Path path2)
        {
            Debug.Assert(path1 != null);
            Debug.Assert(path2 != null);
            Path path = new Path();
            if (path.Op(path1, path2, (int)operation))
            {
                return path;
            }
            throw new StateError("Path.combine() failed.  This may be due an invalid path; in particular, check for NaN values.");
        }

        private bool Op(Path path1, Path path2, int operation)
        {
            return Path_op(this.Handle, path1.Handle, path2.Handle, operation);
        }

        /// Creates a [PathMetrics] object for this path.
        ///
        /// If `forceClosed` is set to true, the contours of the path will be measured
        /// as if they had been closed, even if they were not explicitly closed.
        public PathMetrics ComputeMetrics(bool forceClosed = false)
        {
            return new PathMetrics(this, forceClosed);
        }

        [DllImport("libflutter")]
        public extern static IntPtr Path_constructor();

        [DllImport("libflutter")]
        public extern static IntPtr Path_clone(IntPtr pPath);
            
        [DllImport("libflutter")]
        public extern static int Path_getFillType(IntPtr pPath); 

        [DllImport("libflutter")]
        public extern static void Path_setFillType(IntPtr pPath, int fillType); 

        [DllImport("libflutter")]
        public extern static void Path_moveTo(IntPtr pPath, double x, double y);

        [DllImport("libflutter")]
        public extern static void Path_relativeMoveTo(IntPtr pPath, double dx, double dy);
            
        [DllImport("libflutter")]
        public extern static void Path_lineTo(IntPtr pPath, double x, double y);

        [DllImport("libflutter")]
        public extern static void Path_relativeLineTo(IntPtr pPath, double dx, double dy);
        
        [DllImport("libflutter")]
        public extern static void Path_quadraticBezierTo(IntPtr pPath, double x1, double y1, double x2, double y2);

        [DllImport("libflutter")]
        public extern static void Path_relativeQuadraticBezierTo(IntPtr pPath, double x1, double y1, double x2, double y2);

        [DllImport("libflutter")]
        public extern static void Path_cubicTo(IntPtr pPath, double x1, double y1, double x2, double y2, double x3, double y3);

        [DllImport("libflutter")]
        public extern static void Path_relativeCubicTo(IntPtr pPath, double x1, double y1, double x2, double y2, double x3, double y3);

        [DllImport("libflutter")]
        public extern static void Path_conicTo(IntPtr pPath, double x1, double y1, double x2, double y2, double w);

        [DllImport("libflutter")]
        public extern static void Path_relativeConicTo(IntPtr pPath, double x1, double y1, double x2, double y2, double w);

        [DllImport("libflutter")]
        public extern static void Path_arcTo(IntPtr pPath, double left, double top, double right, double bottom, double startAngle, 
            double sweepAngle, bool forceMoveTo);

        [DllImport("libflutter")]
        public extern static void Path_arcToPoint(IntPtr pPath, double arcEndX, double arcEndY, double radiusX, double radiusY, 
            double rotation, bool largeArc, bool clockwise);
        
        [DllImport("libflutter")]
        public extern static void Path_relativeArcToPoint(IntPtr pPath, double arcEndX, double arcEndY, double radiusX, double radiusY,
            double rotation, bool largeArc, bool clockwise);

        [DllImport("libflutter")]
        public extern static void Path_addRect(IntPtr pPath, double left, double top, double right, double bottom);

        [DllImport("libflutter")]
        public extern static void Path_addOval(IntPtr pPath, double left, double top, double right, double bottom);

        [DllImport("libflutter")]
        public extern static void Path_addArc(IntPtr pPath, double left, double top, double right, double bottom, double startAngle, double sweepAngle);

        [DllImport("libflutter")]
        public extern static void Path_addPath(IntPtr pPath, IntPtr pAddPath, double dx, double dy);

        [DllImport("libflutter")]
        public extern static void Path_extendWithPath(IntPtr pPath, IntPtr pAddPath, double dx, double dy);

        [DllImport("libflutter")]
        public extern static void Path_close(IntPtr pPath);

        [DllImport("libflutter")]
        public extern static void Path_reset(IntPtr pPath); 

        [DllImport("libflutter")]
        public extern static bool Path_contains(IntPtr pPath, double x, double y);

        [DllImport("libflutter")]
        public extern static IntPtr Path_shift(IntPtr pPath, double dx, double dy);

        [DllImport("libflutter")]
        public extern static bool Path_op(IntPtr pPath, IntPtr pPath1, IntPtr pPath2, int operation);
    }
}
