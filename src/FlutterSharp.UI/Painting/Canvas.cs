using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace FlutterSharp.UI
{
    /// An interface for recording graphical operations.
    ///
    /// [Canvas] objects are used in creating [Picture] objects, which can
    /// themselves be used with a [SceneBuilder] to build a [Scene]. In
    /// normal usage, however, this is all handled by the framework.
    ///
    /// A canvas has a current transformation matrix which is applied to all
    /// operations. Initially, the transformation matrix is the identity transform.
    /// It can be modified using the [translate], [scale], [rotate], [skew],
    /// and [transform] methods.
    ///
    /// A canvas also has a current clip region which is applied to all operations.
    /// Initially, the clip region is infinite. It can be modified using the
    /// [clipRect], [clipRRect], and [clipPath] methods.
    ///
    /// The current transform and clip can be saved and restored using the stack
    /// managed by the [save], [saveLayer], and [restore] methods.
    public class Canvas : NativeFieldWrapperClass2
    {
        /// Creates a canvas for recording graphical operations into the
        /// given picture recorder.
        ///
        /// Graphical operations that affect pixels entirely outside the given
        /// `cullRect` might be discarded by the implementation. However, the
        /// implementation might draw outside these bounds if, for example, a command
        /// draws partially inside and outside the `cullRect`. To ensure that pixels
        /// outside a given region are discarded, consider using a [clipRect]. The
        /// `cullRect` is optional; by default, all operations are kept.
        ///
        /// To end the recording, call [PictureRecorder.endRecording] on the
        /// given recorder.
        public Canvas(PictureRecorder recorder, Rect cullRect = null)
        {
            Debug.Assert(recorder != null);
            if (recorder.IsRecording)
                throw new ArgumentException("recorder must not already be associated with another Canvas.");

            if (cullRect == null)
                cullRect = Rect.Largest;

            Constructor(recorder, cullRect.Left, cullRect.Top, cullRect.Right, cullRect.Bottom);
        }

        private void Constructor(PictureRecorder recorder,
            double left,
            double top,
            double right,
            double bottom)
        {
            // TODO : native 'Canvas_constructor';
        }

        /// Saves a copy of the current transform and clip on the save stack.
        ///
        /// Call [restore] to pop the save stack.
        ///
        /// See also:
        ///
        ///  * [saveLayer], which does the same thing but additionally also groups the
        ///    commands done until the matching [restore].
        public void Save()
        {
            // TODO : native 'Canvas_save';
        }

        /// Saves a copy of the current transform and clip on the save stack, and then
        /// creates a new group which subsequent calls will become a part of. When the
        /// save stack is later popped, the group will be flattened into a layer and
        /// have the given `paint`'s [Paint.colorFilter] and [Paint.blendMode]
        /// applied.
        ///
        /// This lets you create composite effects, for example making a group of
        /// drawing commands semi-transparent. Without using [saveLayer], each part of
        /// the group would be painted individually, so where they overlap would be
        /// darker than where they do not. By using [saveLayer] to group them
        /// together, they can be drawn with an opaque color at first, and then the
        /// entire group can be made transparent using the [saveLayer]'s paint.
        ///
        /// Call [restore] to pop the save stack and apply the paint to the group.
        ///
        /// ## Using saveLayer with clips
        ///
        /// When a rectangular clip operation (from [clipRect]) is not axis-aligned
        /// with the raster buffer, or when the clip operation is not rectilinear
        /// (e.g. because it is a rounded rectangle clip created by [clipRRect] or an
        /// arbitrarily complicated path clip created by [clipPath]), the edge of the
        /// clip needs to be anti-aliased.
        ///
        /// If two draw calls overlap at the edge of such a clipped region, without
        /// using [saveLayer], the first drawing will be anti-aliased with the
        /// background first, and then the second will be anti-aliased with the result
        /// of blending the first drawing and the background. On the other hand, if
        /// [saveLayer] is used immediately after establishing the clip, the second
        /// drawing will cover the first in the layer, and thus the second alone will
        /// be anti-aliased with the background when the layer is clipped and
        /// composited (when [restore] is called).
        ///
        /// For example, this [CustomPainter.paint] method paints a clean white
        /// rounded rectangle:
        ///
        /// ```dart
        /// void paint(Canvas canvas, Size size) {
        ///   Rect rect = Offset.zero & size;
        ///   canvas.save();
        ///   canvas.clipRRect(new RRect.fromRectXY(rect, 100.0, 100.0));
        ///   canvas.saveLayer(rect, Paint());
        ///   canvas.drawPaint(new Paint()..color = Colors.red);
        ///   canvas.drawPaint(new Paint()..color = Colors.white);
        ///   canvas.restore();
        ///   canvas.restore();
        /// }
        /// ```
        ///
        /// On the other hand, this one renders a red outline, the result of the red
        /// paint being anti-aliased with the background at the clip edge, then the
        /// white paint being similarly anti-aliased with the background _including
        /// the clipped red paint_:
        ///
        /// ```dart
        /// void paint(Canvas canvas, Size size) {
        ///   // (this example renders poorly, prefer the example above)
        ///   Rect rect = Offset.zero & size;
        ///   canvas.save();
        ///   canvas.clipRRect(new RRect.fromRectXY(rect, 100.0, 100.0));
        ///   canvas.drawPaint(new Paint()..color = Colors.red);
        ///   canvas.drawPaint(new Paint()..color = Colors.white);
        ///   canvas.restore();
        /// }
        /// ```
        ///
        /// This point is moot if the clip only clips one draw operation. For example,
        /// the following paint method paints a pair of clean white rounded
        /// rectangles, even though the clips are not done on a separate layer:
        ///
        /// ```dart
        /// void paint(Canvas canvas, Size size) {
        ///   canvas.save();
        ///   canvas.clipRRect(new RRect.fromRectXY(Offset.zero & (size / 2.0), 50.0, 50.0));
        ///   canvas.drawPaint(new Paint()..color = Colors.white);
        ///   canvas.restore();
        ///   canvas.save();
        ///   canvas.clipRRect(new RRect.fromRectXY(size.center(Offset.zero) & (size / 2.0), 50.0, 50.0));
        ///   canvas.drawPaint(new Paint()..color = Colors.white);
        ///   canvas.restore();
        /// }
        /// ```
        ///
        /// (Incidentally, rather than using [clipRRect] and [drawPaint] to draw
        /// rounded rectangles like this, prefer the [drawRRect] method. These
        /// examples are using [drawPaint] as a proxy for "complicated draw operations
        /// that will get clipped", to illustrate the point.)
        ///
        /// ## Performance considerations
        ///
        /// Generally speaking, [saveLayer] is relatively expensive.
        ///
        /// There are a several different hardware architectures for GPUs (graphics
        /// processing units, the hardware that handles graphics), but most of them
        /// involve batching commands and reordering them for performance. When layers
        /// are used, they cause the rendering pipeline to have to switch render
        /// target (from one layer to another). Render target switches can flush the
        /// GPU's command buffer, which typically means that optimizations that one
        /// could get with larger batching are lost. Render target switches also
        /// generate a lot of memory churn because the GPU needs to copy out the
        /// current frame buffer contents from the part of memory that's optimized for
        /// writing, and then needs to copy it back in once the previous render target
        /// (layer) is restored.
        ///
        /// See also:
        ///
        ///  * [save], which saves the current state, but does not create a new layer
        ///    for subsequent commands.
        ///  * [BlendMode], which discusses the use of [Paint.blendMode] with
        ///    [saveLayer].
        public void SaveLayer(Rect bounds, Paint paint)
        {
            Debug.Assert(paint != null);
            if (bounds == null)
            {
                SaveLayerWithoutBounds(paint._objects, paint._data);
            }
            else
            {
                Debug.Assert(_rectIsValid(bounds));
                SaveLayer(bounds.Left, bounds.Top, bounds.Right, bounds.Bottom,
                    paint._objects, paint._data);
            }
        }

        private void SaveLayerWithoutBounds(List<dynamic> paintObjects, ByteData paintData)
        {
            // TODO :   native 'Canvas_saveLayerWithoutBounds';
        }


        private void SaveLayer(double left,
            double top,
            double right,
            double bottom,
            List<dynamic> paintObjects,
            ByteData paintData)
        {
            // TODO : native 'Canvas_saveLayer';
        }

        /// Pops the current save stack, if there is anything to pop.
        /// Otherwise, does nothing.
        ///
        /// Use [save] and [saveLayer] to push state onto the stack.
        ///
        /// If the state was pushed with with [saveLayer], then this call will also
        /// cause the new layer to be composited into the previous layer.
        public void Restore()
        {
            // TODO :  native 'Canvas_restore';
        }

        /// Returns the number of items on the save stack, including the
        /// initial state. This means it returns 1 for a clean canvas, and
        /// that each call to [save] and [saveLayer] increments it, and that
        /// each matching call to [restore] decrements it.
        ///
        /// This number cannot go below 1.
        public int GetSaveCount() => 0; // TODO: native 'Canvas_getSaveCount';

        /// Add a translation to the current transform, shifting the coordinate space
        /// horizontally by the first argument and vertically by the second argument.
        public void Translate(double dx, double dy)
        {
            // TODO : native 'Canvas_translate';
        }

        /// Add an axis-aligned scale to the current transform, scaling by the first
        /// argument in the horizontal direction and the second in the vertical
        /// direction.
        ///
        /// If [sy] is unspecified, [sx] will be used for the scale in both
        /// directions.
        public void Scale(double sx, double? sy = null)
        {
            Scale(sx, sy ?? sx);
        }

        private void Scale(double sx, double sy)
        {
            // TODO : ' native 'Canvas_scale';
        }

        /// Add a rotation to the current transform. The argument is in radians clockwise.
        public void Rotate(double radians)
        {
            // TODO : native 'Canvas_rotate';
        }

        /// Add an axis-aligned skew to the current transform, with the first argument
        /// being the horizontal skew in rise over run units clockwise around the
        /// origin, and the second argument being the vertical skew in rise over run
        /// units clockwise around the origin.
        public void Skew(double sx, double sy)
        {
            // TODO : native 'Canvas_skew';
        }

        /// Multiply the current transform by the specified 4⨉4 transformation matrix
        /// specified as a list of values in column-major order.
        public void Transform(Float64List matrix4)
        {
            Debug.Assert(matrix4 != null);
            if (matrix4.Count != 16)
                throw new ArgumentException("'matrix4' must have 16 entries.");

            // TODO : native 'Canvas_transform';
        }

        /// Reduces the clip region to the intersection of the current clip and the
        /// given rectangle.
        ///
        /// If [doAntiAlias] is true, then the clip will be anti-aliased.
        ///
        /// If multiple draw commands intersect with the clip boundary, this can result
        /// in incorrect blending at the clip boundary. See [saveLayer] for a
        /// discussion of how to address that.
        ///
        /// Use [ClipOp.difference] to subtract the provided rectangle from the
        /// current clip.
        public void ClipRect(Rect rect, ClipOp clipOp = ClipOp.Intersect, bool doAntiAlias = true)
        {
            Debug.Assert(_rectIsValid(rect));
            Debug.Assert(clipOp != null);
            Debug.Assert(doAntiAlias != null);

            ClipRect(rect.Left, rect.Top, rect.Right, rect.Bottom, (int)clipOp, doAntiAlias);
        }

        private void ClipRect(double left, double top, double right, double bottom, int clipOp, bool doAntiAlias)
        {
            // TODO : native 'Canvas_clipRect';
        }


        /// Reduces the clip region to the intersection of the current clip and the
        /// given rounded rectangle.
        ///
        /// If [doAntiAlias] is true, then the clip will be anti-aliased.
        ///
        /// If multiple draw commands intersect with the clip boundary, this can result
        /// in incorrect blending at the clip boundary. See [saveLayer] for a
        /// discussion of how to address that and some examples of using [clipRRect].
        public void ClipRRect(RRect rrect, bool doAntiAlias = true)
        {
            Debug.Assert(_rrectIsValid(rrect));
            Debug.Assert(doAntiAlias != null);

            ClipRRect(rrect._value32, doAntiAlias);
        }

        private void ClipRRect(Float32List rrect, bool doAntiAlias)
        {
            // TODO : native 'Canvas_clipRRect';
        }

        /// Reduces the clip region to the intersection of the current clip and the
        /// given [Path].
        ///
        /// If [doAntiAlias] is true, then the clip will be anti-aliased.
        ///
        /// If multiple draw commands intersect with the clip boundary, this can result
        /// multiple draw commands intersect with the clip boundary, this can result
        /// in incorrect blending at the clip boundary. See [saveLayer] for a
        /// discussion of how to address that.
        public void ClipPath(Path path, bool doAntiAlias = true)
        {
            Debug.Assert(path != null); // path is checked on the engine side
            Debug.Assert(doAntiAlias != null);

            // TODO : native 'Canvas_clipPath';
        }

        /// Paints the given [Color] onto the canvas, applying the given
        /// [BlendMode], with the given color being the source and the background
        /// being the destination.
        public void DrawColor(Color color, BlendMode blendMode)
        {
            Debug.Assert(color != null);
            Debug.Assert(blendMode != null);
            DrawColor(color.Value, (int)blendMode);
        }

        private void DrawColor(int color, int blendMode)
        {
            // TODO : native 'Canvas_drawColor';
        }

        /// Draws a line between the given points using the given paint. The line is
        /// stroked, the value of the [Paint.style] is ignored for this call.
        ///
        /// The `p1` and `p2` arguments are interpreted as offsets from the origin.
        public void DrawLine(Offset p1, Offset p2, Paint paint)
        {
            Debug.Assert(_offsetIsValid(p1));
            Debug.Assert(_offsetIsValid(p2));
            Debug.Assert(paint != null);
            DrawLine(p1.Dx, p1.Dy, p2.Dx, p2.Dy, paint._objects, paint._data);
        }

        private void DrawLine(double x1,
            double y1,
            double x2,
            double y2,
            List<dynamic> paintObjects,
            ByteData paintData)
        {
            // TODO : native 'Canvas_drawLine';
        }

        /// Fills the canvas with the given [Paint].
        ///
        /// To fill the canvas with a solid color and blend mode, consider
        /// [drawColor] instead.
        public void DrawPaint(Paint paint)
        {
            Debug.Assert(paint != null);
            DrawPaint(paint._objects, paint._data);
        }

        private void DrawPaint(List<dynamic> paintObjects, ByteData paintData)
        {
            // TODO : native 'Canvas_drawPaint';
        }

        /// Draws a rectangle with the given [Paint]. Whether the rectangle is filled
        /// or stroked (or both) is controlled by [Paint.style].
        public void DrawRect(Rect rect, Paint paint)
        {
            Debug.Assert(_rectIsValid(rect));
            Debug.Assert(paint != null);
            DrawRect(rect.Left, rect.Top, rect.Right, rect.Bottom,
                paint._objects, paint._data);
        }

        private void DrawRect(double left, double top, double right, double bottom, List<dynamic> paintObjects,
            ByteData paintData)
        {
            // TODO : native 'Canvas_drawRect';
        }

        /// Draws a rounded rectangle with the given [Paint]. Whether the rectangle is
        /// filled or stroked (or both) is controlled by [Paint.style].
        public void DrawRRect(RRect rrect, Paint paint)
        {
            Debug.Assert(_rrectIsValid(rrect));
            Debug.Assert(paint != null);
            DrawRRect(rrect._value32, paint._objects, paint._data);
        }

        private void DrawRRect(Float32List rrect, List<dynamic> paintObjects, ByteData paintData)
        {
            // TODO :  native 'Canvas_drawRRect';
        }

        /// Draws a shape consisting of the difference between two rounded rectangles
        /// with the given [Paint]. Whether this shape is filled or stroked (or both)
        /// is controlled by [Paint.style].
        ///
        /// This shape is almost but not quite entirely unlike an annulus.
        public void DrawDRRect(RRect outer, RRect inner, Paint paint)
        {
            Debug.Assert(_rrectIsValid(outer));
            Debug.Assert(_rrectIsValid(inner));
            Debug.Assert(paint != null);
            DrawDRRect(outer._value32, inner._value32, paint._objects, paint._data);
        }

        private void DrawDRRect(Float32List outer, Float32List inner, List<dynamic> paintObjects, ByteData paintData)
        {
            // TODO : native 'Canvas_drawDRRect';
        }

        /// Draws an axis-aligned oval that fills the given axis-aligned rectangle
        /// with the given [Paint]. Whether the oval is filled or stroked (or both) is
        /// controlled by [Paint.style].
        public void DrawOval(Rect rect, Paint paint)
        {
            Debug.Assert(_rectIsValid(rect));
            Debug.Assert(paint != null);
            DrawOval(rect.Left, rect.Top, rect.Right, rect.Bottom,
                paint._objects, paint._data);
        }

        private void DrawOval(double left, double top, double right, double bottom, List<dynamic> paintObjects,
            ByteData paintData)
        {
            // TODO :  native 'Canvas_drawOval';
        }

        /// Draws a circle centered at the point given by the first argument and
        /// that has the radius given by the second argument, with the [Paint] given in
        /// the third argument. Whether the circle is filled or stroked (or both) is
        /// controlled by [Paint.style].
        public void DrawCircle(Offset c, double radius, Paint paint)
        {
            Debug.Assert(_offsetIsValid(c));
            Debug.Assert(paint != null);
            DrawCircle(c.Dx, c.Dy, radius, paint._objects, paint._data);
        }

        private void DrawCircle(double x, double y, double radius, List<dynamic> paintObjects, ByteData paintData)
        {
            // TODO :  native 'Canvas_drawCircle';
        }

        /// Draw an arc scaled to fit inside the given rectangle. It starts from
        /// startAngle radians around the oval up to startAngle + sweepAngle
        /// radians around the oval, with zero radians being the point on
        /// the right hand side of the oval that crosses the horizontal line
        /// that intersects the center of the rectangle and with positive
        /// angles going clockwise around the oval. If useCenter is true, the arc is
        /// closed back to the center, forming a circle sector. Otherwise, the arc is
        /// not closed, forming a circle segment.
        ///
        /// This method is optimized for drawing arcs and should be faster than [Path.arcTo].
        public void DrawArc(Rect rect, double startAngle, double sweepAngle, bool useCenter, Paint paint)
        {
            Debug.Assert(_rectIsValid(rect));
            Debug.Assert(paint != null);
            DrawArc(rect.Left, rect.Top, rect.Right, rect.Bottom, startAngle,
                sweepAngle, useCenter, paint._objects, paint._data);
        }

        private void DrawArc(double left, double top, double right, double bottom, double startAngle, double sweepAngle,
            bool useCenter, List<dynamic> paintObjects, ByteData paintData)
        {
            // TODO :  native 'Canvas_drawArc';
        }

        /// Draws the given [Path] with the given [Paint]. Whether this shape is
        /// filled or stroked (or both) is controlled by [Paint.style]. If the path is
        /// filled, then sub-paths within it are implicitly closed (see [Path.close]).
        public void DrawPath(Path path, Paint paint)
        {
            Debug.Assert(path != null); // path is checked on the engine side
            Debug.Assert(paint != null);
            DrawPath(path, paint._objects, paint._data);
        }

        private void DrawPath(Path path, List<dynamic> paintObjects, ByteData paintData)
        {
            // TODO :  native 'Canvas_drawPath';
        }

        /// Draws the given [Image] into the canvas with its top-left corner at the
        /// given [Offset]. The image is composited into the canvas using the given [Paint].
        public void DrawImage(Image image, Offset p, Paint paint)
        {
            Debug.Assert(image != null); // image is checked on the engine side
            Debug.Assert(_offsetIsValid(p));
            Debug.Assert(paint != null);
            DrawImage(image, p.Dx, p.Dy, paint._objects, paint._data);
        }

        private void DrawImage(Image image,
            double x,
            double y,
            List<dynamic> paintObjects,
            ByteData paintData)
        {
            // TODO : native 'Canvas_drawImage';
        }

        /// Draws the subset of the given image described by the `src` argument into
        /// the canvas in the axis-aligned rectangle given by the `dst` argument.
        ///
        /// This might sample from outside the `src` rect by up to half the width of
        /// an applied filter.
        ///
        /// Multiple calls to this method with different arguments (from the same
        /// image) can be batched into a single call to [drawAtlas] to improve
        /// performance.
        public void DrawImageRect(Image image, Rect src, Rect dst, Paint paint)
        {
            Debug.Assert(image != null); // image is checked on the engine side
            Debug.Assert(_rectIsValid(src));
            Debug.Assert(_rectIsValid(dst));
            Debug.Assert(paint != null);
            DrawImageRect(image,
                src.Left,
                src.Top,
                src.Right,
                src.Bottom,
                dst.Left,
                dst.Top,
                dst.Right,
                dst.Bottom,
                paint._objects,
                paint._data);
        }

        private void DrawImageRect(Image image,
            double srcLeft,
            double srcTop,
            double srcRight,
            double srcBottom,
            double dstLeft,
            double dstTop,
            double dstRight,
            double dstBottom,
            List<dynamic> paintObjects,
            ByteData paintData)
        {
            // TODO :  native 'Canvas_drawImageRect';
        }

        /// Draws the given [Image] into the canvas using the given [Paint].
        ///
        /// The image is drawn in nine portions described by splitting the image by
        /// drawing two horizontal lines and two vertical lines, where the `center`
        /// argument describes the rectangle formed by the four points where these
        /// four lines intersect each other. (This forms a 3-by-3 grid of regions,
        /// the center region being described by the `center` argument.)
        ///
        /// The four regions in the corners are drawn, without scaling, in the four
        /// corners of the destination rectangle described by `dst`. The remaining
        /// five regions are drawn by stretching them to fit such that they exactly
        /// cover the destination rectangle while maintaining their relative
        /// positions.
        public void DrawImageNine(Image image, Rect center, Rect dst, Paint paint)
        {
            Debug.Assert(image != null); // image is checked on the engine side
            Debug.Assert(_rectIsValid(center));
            Debug.Assert(_rectIsValid(dst));
            Debug.Assert(paint != null);
            DrawImageNine(image,
                center.Left,
                center.Top,
                center.Right,
                center.Bottom,
                dst.Left,
                dst.Top,
                dst.Right,
                dst.Bottom,
                paint._objects,
                paint._data);
        }

        private void DrawImageNine(Image image,
            double centerLeft,
            double centerTop,
            double centerRight,
            double centerBottom,
            double dstLeft,
            double dstTop,
            double dstRight,
            double dstBottom,
            List<dynamic> paintObjects,
            ByteData paintData)
        {
            // TODO : native 'Canvas_drawImageNine';
        }

        /// Draw the given picture onto the canvas. To create a picture, see
        /// [PictureRecorder].
        public void DrawPicture(Picture picture)
        {
            Debug.Assert(picture != null); // picture is checked on the engine side
            // TODO : native 'Canvas_drawPicture';
        }

        /// Draws the text in the given [Paragraph] into this canvas at the given
        /// [Offset].
        ///
        /// The [Paragraph] object must have had [Paragraph.layout] called on it
        /// first.
        ///
        /// To align the text, set the `textAlign` on the [ParagraphStyle] object
        /// passed to the [new ParagraphBuilder] constructor. For more details see
        /// [TextAlign] and the discussion at [new ParagraphStyle].
        ///
        /// If the text is left aligned or justified, the left margin will be at the
        /// position specified by the `offset` argument's [Offset.dx] coordinate.
        ///
        /// If the text is right aligned or justified, the right margin will be at the
        /// position described by adding the [ParagraphConstraints.width] given to
        /// [Paragraph.layout], to the `offset` argument's [Offset.dx] coordinate.
        ///
        /// If the text is centered, the centering axis will be at the position
        /// described by adding half of the [ParagraphConstraints.width] given to
        /// [Paragraph.layout], to the `offset` argument's [Offset.dx] coordinate.
        public void DrawParagraph(Paragraph paragraph, Offset offset)
        {
            Debug.Assert(paragraph != null);
            Debug.Assert(_offsetIsValid(offset));
            paragraph.Paint(this, offset.Dx, offset.Dy);
        }

        /// Draws a sequence of points according to the given [PointMode].
        ///
        /// The `points` argument is interpreted as offsets from the origin.
        ///
        /// See also:
        ///
        ///  * [drawRawPoints], which takes `points` as a [Float32List] rather than a
        ///    [List<Offset>].
        public void DrawPoints(PointMode pointMode, List<Offset> points, Paint paint)
        {
            Debug.Assert(pointMode != null);
            Debug.Assert(points != null);
            Debug.Assert(paint != null);
            DrawPoints(paint._objects, paint._data, (int)pointMode, EncodePointList(points));
        }

        /// Draws a sequence of points according to the given [PointMode].
        ///
        /// The `points` argument is interpreted as a list of pairs of floating point
        /// numbers, where each pair represents an x and y offset from the origin.
        ///
        /// See also:
        ///
        ///  * [drawPoints], which takes `points` as a [List<Offset>] rather than a
        ///    [List<Float32List>].
        public void DrawRawPoints(PointMode pointMode, Float32List points, Paint paint)
        {
            Debug.Assert(pointMode != null);
            Debug.Assert(points != null);
            Debug.Assert(paint != null);
            if (points.Count % 2 != 0)
                throw new ArgumentException("'points' must have an even number of values.");
            DrawPoints(paint._objects, paint._data, (int)pointMode, points);
        }

        private void DrawPoints(List<dynamic> paintObjects,
            ByteData paintData,
            int pointMode,
            Float32List points)
        {
            // TODO : native 'Canvas_drawPoints';
        }

        public void DrawVertices(Vertices vertices, BlendMode blendMode, Paint paint)
        {
            Debug.Assert(vertices != null); // vertices is checked on the engine side
            Debug.Assert(paint != null);
            Debug.Assert(blendMode != null);
            DrawVertices(vertices, (int)blendMode, paint._objects, paint._data);
        }

        private void DrawVertices(Vertices vertices,
            int blendMode,
            List<dynamic> paintObjects,
            ByteData paintData)
        {
            // TODO :  native 'Canvas_drawVertices';
        }

        //
        // See also:
        //
        //  * [drawRawAtlas], which takes its arguments as typed data lists rather
        //    than objects.
        public void DrawAtlas(Image atlas,
                        List<RSTransform> transforms,
                        List<Rect> rects,
                        List<Color> colors,
                        BlendMode blendMode,
                        Rect cullRect,
                        Paint paint)
        {
            Debug.Assert(atlas != null); // atlas is checked on the engine side
            Debug.Assert(transforms != null);
            Debug.Assert(rects != null);
            Debug.Assert(colors != null);
            Debug.Assert(blendMode != null);
            Debug.Assert(paint != null);

            int rectCount = rects.Count;
            if (transforms.Count != rectCount)
                throw new ArgumentException("'transforms' and 'rects' lengths must match.");
            if (colors.Any() && colors.Count != rectCount)
                throw new ArgumentException("If non-null, 'colors' length must match that of 'transforms' and 'rects'.");

            Float32List rstTransformBuffer = new Float32List(rectCount * 4);
            Float32List rectBuffer = new Float32List(rectCount * 4);

            for (int i = 0; i < rectCount; ++i)
            {
                int index0 = i * 4;
                int index1 = index0 + 1;
                int index2 = index0 + 2;
                int index3 = index0 + 3;
                RSTransform rstTransform = transforms[i];
                Rect rect = rects[i];
                //Debug.Assert(_rectIsValid(rect));
                rstTransformBuffer[index0] = rstTransform.Scos;
                rstTransformBuffer[index1] = rstTransform.Scos;
                rstTransformBuffer[index2] = rstTransform.Tx;
                rstTransformBuffer[index3] = rstTransform.Tx;
                rectBuffer[index0] = rect.Left;
                rectBuffer[index1] = rect.Top;
                rectBuffer[index2] = rect.Right;
                rectBuffer[index3] = rect.Bottom;
            }

            Int32List colorBuffer = !colors.Any() ? null : EncodeColorList(colors);
            Float32List cullRectBuffer = cullRect?._value32;

            DrawAtlas(
              paint._objects, paint._data, atlas, rstTransformBuffer, rectBuffer,
              colorBuffer, (int)blendMode, cullRectBuffer
            );
        }

        //
        // The `rstTransforms` argument is interpreted as a list of four-tuples, with
        // each tuple being ([RSTransform.scos], [RSTransform.ssin],
        // [RSTransform.tx], [RSTransform.ty]).
        //
        // The `rects` argument is interpreted as a list of four-tuples, with each
        // tuple being ([Rect.left], [Rect.top], [Rect.right], [Rect.bottom]).
        //
        // The `colors` argument, which can be null, is interpreted as a list of
        // 32-bit colors, with the same packing as [Color.value].
        //
        // See also:
        //
        //  * [drawAtlas], which takes its arguments as objects rather than typed
        //    data lists.
        public void DrawRawAtlas(Image atlas,
                          Float32List rstTransforms,
                          Float32List rects,
                          Int32List colors,
                          BlendMode blendMode,
                          Rect cullRect,
                          Paint paint)
        {
            Debug.Assert(atlas != null); // atlas is checked on the engine side
            Debug.Assert(rstTransforms != null);
            Debug.Assert(rects != null);
            Debug.Assert(colors != null);
            Debug.Assert(blendMode != null);
            Debug.Assert(paint != null);

            int rectCount = rects.Count;
            if (rstTransforms.Count != rectCount)
                throw new ArgumentException("'rstTransforms' and 'rects' lengths must match.");
            if (rectCount % 4 != 0)
                throw new ArgumentException("'rstTransforms' and 'rects' lengths must be a multiple of four.");
            if (colors != null && colors.Count * 4 != rectCount)
                throw new ArgumentException("If non-null, 'colors' length must be one fourth the length of 'rstTransforms' and 'rects'.");

            DrawAtlas(
              paint._objects, paint._data, atlas, rstTransforms, rects,
              colors, (int)blendMode, cullRect?._value32
            );
        }

        private void DrawAtlas(List<dynamic> paintObjects,
            ByteData paintData,
            Image atlas,
            Float32List rstTransforms,
            Float32List rects,
            Int32List colors,
            int blendMode,
            Float32List cullRect)
        {
            // TODO :  native 'Canvas_drawAtlas';
        }

        /// Draws a shadow for a [Path] representing the given material elevation.
        ///
        /// The `transparentOccluder` argument should be true if the occluding object
        /// is not opaque.
        ///
        /// The arguments must not be null.
        public void DrawShadow(Path path, Color color, double elevation, bool transparentOccluder)
        {
            Debug.Assert(path != null); // path is checked on the engine side
            Debug.Assert(color != null);
            Debug.Assert(transparentOccluder != null);
            DrawShadow(path, color.Value, elevation, transparentOccluder);
        }

        private void DrawShadow(Path path,
            int color,
            double elevation,
            bool transparentOccluder)
        {
            // TODO : native 'Canvas_drawShadow';
        }
    }
}