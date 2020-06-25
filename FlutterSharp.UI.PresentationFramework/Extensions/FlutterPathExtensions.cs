namespace FlutterSharp.UI.PresentationFramework.Extensions
{
    /// <summary>
    /// Extensions for <see cref="FlutterSharp.UI.Path"/> class
    /// </summary>
    public static class FlutterPathExtensions
    {
        /// <summary>
        /// Moves to x, y point without drawing path.
        /// </summary>
        /// <param name="path">The source path.</param>
        /// <param name="x">The x coordinate of point to move to.</param>
        /// <param name="y">The y coordinate of point to move to.</param>
        /// <returns>The source path.</returns>
        public static Path MoveToEx(this Path path, double x, double y)
        {
            path.MoveTo(x, y);
            return path;
        }

        /// <summary>
        /// Moves to point without drawing path.
        /// </summary>
        /// <param name="path">The source path.</param>
        /// <param name="point">The point to move to.</param>
        /// <returns>The source path.</returns>
        public static Path MoveToEx(this Path path, Point point)
        {
            path.MoveTo(point.X, point.Y);
            return path;
        }

        /// <summary>
        /// Starts a new sub-path at the given offset from the current point.
        /// </summary>
        /// <param name="path">The source path.</param>
        /// <param name="x">The dx.</param>
        /// <param name="y">The dy.</param>
        /// <returns>The source path.</returns>
        public static Path RelativeMoveToEx(this Path path, double x, double y)
        {
            path.RelativeMoveTo(x, y);
            return path;
        }

        /// <summary>
        /// Starts a new sub-path at the given offset from the current point.
        /// </summary>
        /// <param name="path">The source path.</param>
        /// <param name="offset">The relative offset to apply.</param>
        /// <returns>The source path.</returns>
        public static Path RelativeMoveToEx(this Path path, Offset offset)
        {
            path.RelativeMoveTo(offset.Dx, offset.Dy);
            return path;
        }

        /// <summary>
        /// Adds a straight line segment from the current point to the given point.
        /// </summary>
        /// <param name="path">The source path.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns>The source path.</returns>
        public static Path LineToEx(this Path path, double x, double y)
        {
            path.LineTo(x, y);
            return path;
        }

        /// <summary>
        /// Adds a straight line segment from the current point to the given point.
        /// </summary>
        /// <param name="path">The source path.</param>
        /// <param name="point">The end point of the line.</param>
        /// <returns>The source path.</returns>
        public static Path LineToEx(this Path path, Point point)
        {
            path.LineTo(point.X, point.Y);
            return path;
        }

        /// <summary>
        /// Adds a straight line segment from the current point to the point at the given offset from the current point.
        /// </summary>
        /// <param name="path">The source path.</param>
        /// <param name="dx">The dx.</param>
        /// <param name="dy">The dy.</param>
        /// <returns>The source path.</returns>
        public static Path RelativeLineToEx(this Path path, double dx, double dy)
        {
            path.RelativeLineTo(dx, dy);
            return path;
        }

        /// <summary>
        /// Adds a straight line segment from the current point to the point at the given offset from the current point.
        /// </summary>
        /// <param name="path">The source path.</param>
        /// <param name="offset">The relative offset to apply.</param>
        /// <returns>The source path.</returns>
        public static Path RelativeLineToEx(this Path path, Offset offset)
        {
            path.RelativeLineTo(offset.Dx, offset.Dy);
            return path;
        }

        /// <summary>
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
        /// </summary>
        public static Path ArcToPointEx(this Path path, Offset arcEnd, Radius radius = null, double rotation = 0.0, bool largeArc = false, bool clockwise = true)
        {
            path.ArcToPoint(arcEnd, radius, rotation, largeArc, clockwise);
            return path;
        }

        /// <summary>
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
        /// </summary>
        public static Path RelativeArcToPointEx(this Path path, Offset arcEndDelta, Radius radius = null, double rotation = 0.0, bool largeArc = false, bool clockwise = true)
        {
            path.RelativeArcToPoint(arcEndDelta, radius, rotation, largeArc, clockwise);
            return path;
        }

        /// <summary>
        /// Closes the last sub-path, as if a straight line had been drawn
        /// from the current point to the first point of the sub-path.
        /// </summary>
        public static Path CloseEx(this Path path)
        {
            path.Close();
            return path;
        }

        ///// Adds a quadratic bezier segment that curves from the current
        ///// point to the given point (x2,y2), using the control point
        ///// (x1,y1).
        //public void QuadraticBezierTo(double x1, double y1, double x2, double y2)
        //{
        //    Path_quadraticBezierTo(this.Handle, x1, y1, x2, y2);
        //}

        ///// Adds a quadratic bezier segment that curves from the current
        ///// point to the point at the offset (x2,y2) from the current point,
        ///// using the control point at the offset (x1,y1) from the current
        ///// point.
        //public void RelativeQuadraticBezierTo(double x1, double y1, double x2, double y2)
        //{
        //    Path_relativeQuadraticBezierTo(this.Handle, x1, y1, x2, y2);
        //}

        ///// Adds a cubic bezier segment that curves from the current point
        ///// to the given point (x3,y3), using the control points (x1,y1) and
        ///// (x2,y2).
        //public void CubicTo(double x1, double y1, double x2, double y2, double x3, double y3)
        //{
        //    Path_cubicTo(this.Handle, x1, y1, x2, y2, x3, y3);
        //}

        ///// Adds a cubic bezier segment that curves from the current point
        ///// to the point at the offset (x3,y3) from the current point, using
        ///// the control points at the offsets (x1,y1) and (x2,y2) from the
        ///// current point.
        //public void RelativeCubicTo(double x1, double y1, double x2, double y2, double x3, double y3)
        //{
        //    Path_relativeCubicTo(this.Handle, x1, y1, x2, y2, x3, y3);
        //}

        ///// Adds a bezier segment that curves from the current point to the
        ///// given point (x2,y2), using the control points (x1,y1) and the
        ///// weight w. If the weight is greater than 1, then the curve is a
        ///// hyperbola; if the weight equals 1, it's a parabola; and if it is
        ///// less than 1, it is an ellipse.
        //public void ConicTo(double x1, double y1, double x2, double y2, double w)
        //{
        //    Path_conicTo(this.Handle, x1, y1, x2, y2, w);
        //}

        ///// Adds a bezier segment that curves from the current point to the
        ///// point at the offset (x2,y2) from the current point, using the
        ///// control point at the offset (x1,y1) from the current point and
        ///// the weight w. If the weight is greater than 1, then the curve is
        ///// a hyperbola; if the weight equals 1, it's a parabola; and if it
        ///// is less than 1, it is an ellipse.
        //public void RelativeConicTo(double x1, double y1, double x2, double y2, double w)
        //{
        //    Path_relativeConicTo(this.Handle, x1, y1, x2, y2, w);
        //}

        ///// If the `forceMoveTo` argument is false, adds a straight line
        ///// segment and an arc segment.
        /////
        ///// If the `forceMoveTo` argument is true, starts a new sub-path
        ///// consisting of an arc segment.
        /////
        ///// In either case, the arc segment consists of the arc that follows
        ///// the edge of the oval bounded by the given rectangle, from
        ///// startAngle radians around the oval up to startAngle + sweepAngle
        ///// radians around the oval, with zero radians being the point on
        ///// the right hand side of the oval that crosses the horizontal line
        ///// that intersects the center of the rectangle and with positive
        ///// angles going clockwise around the oval.
        /////
        ///// The line segment added if `forceMoveTo` is false starts at the
        ///// current point and ends at the start of the arc.
        //public void ArcTo(Rect rect, double startAngle, double sweepAngle, bool forceMoveTo)
        //{
        //    Debug.Assert(RectIsValid(rect));
        //    ArcTo(rect.Left, rect.Top, rect.Right, rect.Bottom, startAngle, sweepAngle, forceMoveTo);
        //}

        ///// Adds a new sub-path that consists of four lines that outline the
        ///// given rectangle.
        //public void AddRect(Rect rect)
        //{
        //    Debug.Assert(RectIsValid(rect));
        //    AddRect(rect.Left, rect.Top, rect.Right, rect.Bottom);
        //}

        ///// Adds a new sub-path that consists of a curve that forms the
        ///// ellipse that fills the given rectangle.
        /////
        ///// To add a circle, pass an appropriate rectangle as `oval`. [Rect.fromCircle]
        ///// can be used to easily describe the circle's center [Offset] and radius.
        //public void AddOval(Rect oval)
        //{
        //    Debug.Assert(RectIsValid(oval));
        //    AddOval(oval.Left, oval.Top, oval.Right, oval.Bottom);
        //}

        ///// Adds a new sub-path with one arc segment that consists of the arc
        ///// that follows the edge of the oval bounded by the given
        ///// rectangle, from startAngle radians around the oval up to
        ///// startAngle + sweepAngle radians around the oval, with zero
        ///// radians being the point on the right hand side of the oval that
        ///// crosses the horizontal line that intersects the center of the
        ///// rectangle and with positive angles going clockwise around the
        ///// oval.
        //public void AddArc(Rect oval, double startAngle, double sweepAngle)
        //{
        //    Debug.Assert(RectIsValid(oval));
        //    AddArc(oval.Left, oval.Top, oval.Right, oval.Bottom, startAngle, sweepAngle);
        //}

        ///// Adds a new sub-path with a sequence of line segments that connect the given
        ///// points.
        /////
        ///// If `close` is true, a final line segment will be added that connects the
        ///// last point to the first point.
        /////
        ///// The `points` argument is interpreted as offsets from the origin.
        //public void AddPolygon(List<Offset> points, bool close)
        //{
        //    Debug.Assert(points != null);
        //    AddPolygon(EncodePointList(points), close);
        //}

        ///// Adds a new sub-path that consists of the straight lines and
        ///// curves needed to form the rounded rectangle described by the
        ///// argument.
        //public void AddRRect(RRect rrect)
        //{
        //    Debug.Assert(RrectIsValid(rrect));
        //    AddRRect(rrect._value32);
        //}

        ///// Adds a new sub-path that consists of the given `path` offset by the given
        ///// `offset`.
        /////
        ///// If `matrix4` is specified, the path will be transformed by this matrix
        ///// after the matrix is translated by the given offset. The matrix is a 4x4
        ///// matrix stored in column major order.
        //public void AddPath(Path path, Offset offset, Float64List matrix4 = null)
        //{
        //    Debug.Assert(path != null); // path is checked on the engine side
        //    Debug.Assert(OffsetIsValid(offset));
        //    if (matrix4 != null)
        //    {
        //        Debug.Assert(Matrix4IsValid(matrix4));
        //        AddPathWithMatrix(path, offset.Dx, offset.Dy, matrix4);
        //    }
        //    else
        //    {
        //        AddPath(path, offset.Dx, offset.Dy);
        //    }
        //}

        ///// Adds the given path to this path by extending the current segment of this
        ///// path with the the first segment of the given path.
        /////
        ///// If `matrix4` is specified, the path will be transformed by this matrix
        ///// after the matrix is translated by the given `offset`.  The matrix is a 4x4
        ///// matrix stored in column major order.
        //public void ExtendWithPath(Path path, Offset offset, Float64List matrix4 = null)
        //{
        //    Debug.Assert(path != null); // path is checked on the engine side
        //    Debug.Assert(OffsetIsValid(offset));
        //    if (matrix4 != null)
        //    {
        //        Debug.Assert(Matrix4IsValid(matrix4));
        //        ExtendWithPathAndMatrix(path, offset.Dx, offset.Dy, matrix4);
        //    }
        //    else
        //    {
        //        ExtendWithPath(path, offset.Dx, offset.Dy);
        //    }
        //}
    }
}
