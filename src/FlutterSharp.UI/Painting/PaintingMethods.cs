using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

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

        // Indicates that the image should not be resized in this dimension.
        //
        // Used by [instantiateImageCodec] as a magical value to disable resizing
        // in the given dimension.
        //
        // This needs to be kept in sync with "kDoNotResizeDimension" in codec.cc
        internal const int _kDoNotResizeDimension = -1;

        internal static Color ScaleAlpha(Color a, double factor)
        {
            return a.WithAlpha((int)(a.Alpha * factor).Round().Clamp(0, 255));
        }

        /// Callback signature for [decodeImageFromList].
        public delegate void ImageDecoderCallback(Image result);
        
        /// Instantiates an image codec [Codec] object.
        ///
        /// [list] is the binary image data (e.g a PNG or GIF binary data).
        /// The data can be for either static or animated images. The following image
        /// formats are supported: {@macro flutter.dart:ui.imageFormats}
        ///
        /// The [targetWidth] and [targetHeight] arguments specify the size of the output
        /// image, in image pixels. If they are not equal to the intrinsic dimensions of the
        /// image, then the image will be scaled after being decoded. If exactly one of
        /// these two arguments is specified, then the aspect ratio will be maintained
        /// while forcing the image to match the specified dimension. If both are not
        /// specified, then the image maintains its real size.
        ///
        /// The returned future can complete with an error if the image decoding has
        /// failed.
        public static Future<Codec> InstantiateImageCodec(Uint8List list, int? targetWidth = null, int? targetHeight = null)
        {
            return Futurize(
              (_Callback<Codec> callback) => InstantiateImageCodec(list, callback, null, targetWidth ?? _kDoNotResizeDimension, targetHeight ?? _kDoNotResizeDimension)
            );
        }

        /// Instantiates a [Codec] object for an image binary data.
        ///
        /// The [targetWidth] and [targetHeight] arguments specify the size of the output
        /// image, in image pixels. Image in this context refers to image in every frame of the [Codec].
        /// If [targetWidth] and [targetHeight] are not equal to the intrinsic dimensions of the
        /// image, then the image will be scaled after being decoded. If exactly one of
        /// these two arguments is not equal to [_kDoNotResizeDimension], then the aspect
        /// ratio will be maintained while forcing the image to match the given dimension.
        /// If both are equal to [_kDoNotResizeDimension], then the image maintains its real size.
        ///
        /// Returns an error message if the instantiation has failed, null otherwise.
        private static string InstantiateImageCodec(Uint8List list, _Callback<Codec> callback, ImageInfo imageInfo, int targetWidth, int targetHeight)
        {
            // TODO : native 'instantiateImageCodec';
            return null;
        }

        /// Loads a single image frame from a byte array into an [Image] object.
        ///
        /// This is a convenience wrapper around [instantiateImageCodec]. Prefer using
        /// [instantiateImageCodec] which also supports multi frame images.
        public static void DecodeImageFromList(Uint8List list, ImageDecoderCallback callback)
        {
            DecodeImageFromListAsync(list, callback);
        }

        // TODO : check this Future<Null> retun type 
        public static async void DecodeImageFromListAsync(Uint8List list, ImageDecoderCallback callback)
        {
            Codec codec = await InstantiateImageCodec(list);
            FrameInfo frameInfo = await codec.GetNextFrame();
            callback(frameInfo.Image);
        }

        /// Convert an array of pixel values into an [Image] object.
        ///
        /// [pixels] is the pixel data in the encoding described by [format].
        ///
        /// [rowBytes] is the number of bytes consumed by each row of pixels in the
        /// data buffer.  If unspecified, it defaults to [width] multiplied by the
        /// number of bytes per pixel in the provided [format].
        ///
        /// The [targetWidth] and [targetHeight] arguments specify the size of the output
        /// image, in image pixels. If they are not equal to the intrinsic dimensions of the
        /// image, then the image will be scaled after being decoded. If exactly one of
        /// these two arguments is specified, then the aspect ratio will be maintained
        /// while forcing the image to match the other given dimension. If neither is
        /// specified, then the image maintains its real size.
        public static void DecodeImageFromPixels(Uint8List pixels, int width, int height, PixelFormat format,
          ImageDecoderCallback callback, int? rowBytes = null, int? targetWidth = null, int? targetHeight = null) {

            ImageInfo imageInfo = new ImageInfo(width, height, (int)format, rowBytes);

            // TODO : implement this

            /*Future<Codec> codecFuture = Futurize(
              (_Callback<Codec> callback) => InstantiateImageCodec(pixels, callback, imageInfo, targetWidth ?? _kDoNotResizeDimension, targetHeight ?? _kDoNotResizeDimension)
            );*/

            // codecFuture.ContinueWith(codec => codec.GetNextFrame())
            // .then((FrameInfo frameInfo) => callback(frameInfo.image));
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

        /// Generic callback signature, used by [_futurize].
        public delegate void _Callback<T>(T result);

        /// Signature for a method that receives a [_Callback].
        ///
        /// Return value should be null on success, and a string error message on
        /// failure.
        public delegate string _Callbacker<T>(_Callback<T> callback);

        /// Converts a method that receives a value-returning callback to a method that
        /// returns a Future.
        ///
        /// Return a [String] to cause an [Exception] to be synchronously thrown with
        /// that string as a message.
        ///
        /// If the callback is called with null, the future completes with an error.
        ///
        /// Example usage:
        ///
        /// ```dart
        /// typedef IntCallback = void Function(int result);
        ///
        /// String _doSomethingAndCallback(IntCallback callback) {
        ///   Timer(new Duration(seconds: 1), () { callback(1); });
        /// }
        ///
        /// Future<int> doSomething() {
        ///   return _futurize(_doSomethingAndCallback);
        /// }
        /// ```
        private static Future<T> Futurize<T>(_Callbacker<T> callbacker)
        {
            // TODO : implement this;
            return null;

            /*final Completer<T> completer = Completer<T>.sync();
            final String error = callbacker((T t) {
                if (t == null)
                {
                    completer.completeError(Exception('operation failed'));
                }
                else
                {
                    completer.complete(t);
                }
            });
            if (error != null)
                throw Exception(error);
            return completer.future;*/
        }
    }
}
