namespace FlutterSharp.UI
{
    /// Opaque handle to raw decoded image data (pixels).
    ///
    /// To obtain an [Image] object, use [instantiateImageCodec].
    ///
    /// To draw an [Image], use one of the methods on the [Canvas] class, such as
    /// [Canvas.drawImage].
    ///
    /// See also:
    ///
    ///  * [Image](https://api.flutter.dev/flutter/widgets/Image-class.html), the class in the [widgets] library.
    ///
    public class Image : NativeFieldWrapperClass2
    {
        // This class is created by the engine, and should not be instantiated
        // or extended directly.
        //
        // To obtain an [Image] object, use [instantiateImageCodec].
        public Image()
        {
        }

        /// The number of image pixels along the image's horizontal axis.
        public int Width => 0; // TODO : native 'Image_width';

        /// The number of image pixels along the image's vertical axis.
        public int Height => 0; // TODO : native 'Image_height';

        /// Converts the [Image] object into a byte array.
        ///
        /// The [format] argument specifies the format in which the bytes will be
        /// returned.
        ///
        /// Returns a future that completes with the binary image data or an error
        /// if encoding fails.
        public Future<ByteData> ToByteData(ImageByteFormat format = ImageByteFormat.RawRgba)
        {
            /*return _futurize((_Callback<ByteData> callback) {
                return _toByteData(format.index, (Uint8List encoded) {
                    callback(encoded?.buffer?.asByteData());
                });
            });*/

            return null;
        }

        /// Returns an error message on failure, null on success.
        // TODO :  private string ToByteData(int format, _Callback<Uint8List> callback) native 'Image_toByteData';

        /// Release the resources used by this object. The object is no longer usable
        /// after this method is called.
        public void Dispose()
        {
            // TODO : native 'Image_dispose';
        }

        public override string ToString()
        {
            return $"{this.Width} - {this.Height}";
        }
    }
}