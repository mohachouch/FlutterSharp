using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace FlutterSharp.UI
{
    public static class PaintingMethods
    {
        // Some methods in this file assert that their arguments are not null. These
        // asserts are just to improve the error messages; they should only cover
        // arguments that are either dereferenced _in Dart_, before being passed to the
        // engine, or that the engine explicitly null-checks itself (after attempting to
        // convert the argument to a native type). It should not be possible for a null
        // or invalid value to be used by the engine even in release mode, since that
        // would cause a crash. It is, however, acceptable for error messages to be much
        // less useful or correct in release mode than in debug mode.
        //
        // Painting APIs will also warn about arguments representing NaN coordinates,
        // which can not be rendered by Skia.

        // Update this list when changing the list of supported codecs.
        /// {@template flutter.dart:ui.imageFormats}
        /// JPEG, PNG, GIF, Animated GIF, WebP, Animated WebP, BMP, and WBMP
        /// {@endtemplate}

        internal static bool RectIsValid(Rect rect)
        {
            Debug.Assert(rect != null, "Rect argument was null.");
            Debug.Assert(!rect.HasNaN, "Rect argument contained a NaN value.");
            return true;
        }

        internal static bool RrectIsValid(RRect rrect)
        {
            Debug.Assert(rrect != null, "RRect argument was null.");
            Debug.Assert(!rrect.HasNaN, "RRect argument contained a NaN value.");
            return true;
        }

        internal static bool OffsetIsValid(Offset offset)
        {
            Debug.Assert(offset != null, "Offset argument was null.");
            Debug.Assert(!offset.Dx.IsNaN() && !offset.Dy.IsNaN(), "Offset argument contained a NaN value.");
            return true;
        }

        internal static bool Matrix4IsValid(Float64List matrix4)
        {
            Debug.Assert(matrix4 != null, "Matrix4 argument was null.");
            Debug.Assert(matrix4.Count == 16, "Matrix4 must have 16 entries.");
            Debug.Assert(matrix4.All((double value) => value.IsFinite()), "Matrix4 entries must be finite.");
            return true;
        }

        internal static bool RadiusIsValid(Radius radius)
        {
            Debug.Assert(radius != null, "Radius argument was null.");
            Debug.Assert(!radius.X.IsNaN() && !radius.Y.IsNaN(), "Radius argument contained a NaN value.");
            return true;
        }

        internal static Int32List EncodeColorList(List<Color> colors)
        {
            int colorCount = colors.Count;
            Int32List result = new Int32List(colorCount);
            for (int i = 0; i < colorCount; ++i)
                result[i] = colors[i].Value;
            return result;
        }

        internal static Float32List EncodePointList(List<Offset> points)
        {
            Debug.Assert(points != null);
            int pointCount = points.Count;
            Float32List result = new Float32List(pointCount * 2);
            for (int i = 0; i < pointCount; ++i)
            {
                int xIndex = i * 2;
                int yIndex = xIndex + 1;
                Offset point = points[i];
                Debug.Assert(OffsetIsValid(point));
                result[xIndex] = point.Dx;
                result[yIndex] = point.Dy;
            }
            return result;
        }

        internal static Float32List EncodeTwoPoints(Offset pointA, Offset pointB)
        {
            Debug.Assert(OffsetIsValid(pointA));
            Debug.Assert(OffsetIsValid(pointB));
            Float32List result = new Float32List(4);
            result[0] = pointA.Dx;
            result[1] = pointA.Dy;
            result[2] = pointB.Dx;
            result[3] = pointB.Dy;
            return result;
        }
    }
}
