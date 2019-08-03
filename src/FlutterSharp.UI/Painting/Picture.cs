using System;

namespace FlutterSharp.UI
{
    /// An object representing a sequence of recorded graphical operations.
    ///
    /// To create a [Picture], use a [PictureRecorder].
    ///
    /// A [Picture] can be placed in a [Scene] using a [SceneBuilder], via
    /// the [SceneBuilder.addPicture] method. A [Picture] can also be
    /// drawn into a [Canvas], using the [Canvas.drawPicture] method.
    public class Picture : NativeFieldWrapperClass2, IDisposable
    {
        /// This class is created by the engine, and should not be instantiated
        /// or extended directly.
        ///
        /// To create a [Picture], use a [PictureRecorder].
        public Picture(IntPtr paragraphHandle)
            : base(paragraphHandle)
        {
            if (this.Handle == IntPtr.Zero)
            {
                throw new InvalidOperationException("Unable to create a new Picture instance.");
            }
        }

        /// Creates an image from this picture.
        ///
        /// The picture is rasterized using the number of pixels specified by the
        /// given width and height.
        ///
        /// Although the image is returned synchronously, the picture is actually
        /// rasterized the first time the image is drawn and then cached.
        public Future<Image> ToImage(int width, int height)
        {
            if (width <= 0 || height <= 0)
                throw new Exception("Invalid image dimensions.");
            /*return Futurize(
                (_Callback<Image> callback) => ToImage(width, height, callback)
            );*/
            //TODO : implement
            return null;
        }

        private string ToImage(int width, int height, _Callback<Image> callback)
        {
            // TODO : native 'Picture_toImage';
            return null;
        }

        /// Release the resources used by this object. The object is no longer usable
        /// after this method is called.
        public void Dispose()
        {
            // TODO : native 'Picture_dispose';
        }

        /// Returns the approximate number of bytes allocated for this object.
        ///
        /// The actual size of this picture may be larger, particularly if it contains
        /// references to image or other large objects.
        public int ApproximateBytesUsed => 0; // TODO : native 'Picture_GetAllocationSize';
    }
}