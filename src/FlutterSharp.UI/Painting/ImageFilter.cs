using System;

namespace FlutterSharp.UI
{
    /// A filter operation to apply to a raster image.
    ///
    /// See also:
    ///
    ///  * [BackdropFilter], a widget that applies [ImageFilter] to its rendering.
    ///  * [SceneBuilder.pushBackdropFilter], which is the low-level API for using
    ///    this class.
    public class ImageFilter : NativeFieldWrapperClass2
    {
        private static void Constructor()
        {
            // native 'ImageFilter_constructor';
        }

        /// Creates an image filter that applies a Gaussian blur.
        public static ImageFilter Blur(double sigmaX = 0.0, double sigmaY = 0.0)
        {
            Constructor();
            InitBlur(sigmaX, sigmaY);
            return new ImageFilter(); // TODO : check this
        }

        private static void InitBlur(double sigmaX, double sigmaY)
        {
            //  native 'ImageFilter_initBlur';
        }

        /// Creates an image filter that applies a matrix transformation.
        ///
        /// For example, applying a positive scale matrix (see [Matrix4.diagonal3])
        /// when used with [BackdropFilter] would magnify the background image.
        public static ImageFilter Matrix(Float64List matrix4, FilterQuality filterQuality = FilterQuality.Low)
        {
            if (matrix4.Count != 16)
                throw new ArgumentException("'matrix4' must have 16 entries.");
            Constructor();
            InitMatrix(matrix4, (int)filterQuality);
            return new ImageFilter(); // TODO : check this 
        }

        private static void InitMatrix(Float64List matrix4, int filterQuality)
        {
            // TODO : native 'ImageFilter_initMatrix';
        }
    }
}
