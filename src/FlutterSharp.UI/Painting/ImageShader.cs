using System;
using System.Diagnostics;

namespace FlutterSharp.UI
{
    /// A shader (as used by [Paint.shader]) that tiles an image.
    public class ImageShader : Shader
    {
        /// Creates an image-tiling shader. The first argument specifies the image to
        /// tile. The second and third arguments specify the [TileMode] for the x
        /// direction and y direction respectively. The fourth argument gives the
        /// matrix to apply to the effect. All the arguments are required and must not
        /// be null.
        public ImageShader(Image image, TileMode tmx, TileMode tmy, Float64List matrix4)
        {
            Debug.Assert(image != null); // image is checked on the engine side
            Debug.Assert(tmx != null);
            Debug.Assert(tmy != null);
            Debug.Assert(matrix4 != null);
            if (matrix4.Count != 16)
                throw new ArgumentException("'matrix4' must have 16 entries.");
            Constructor();
            InitWithImage(image, (int)tmx, (int)tmy, matrix4);
        }
   
        private void Constructor()
        {
            // TODO : native 'ImageShader_constructor';
        }

        private void InitWithImage(Image image, int tmx, int tmy, Float64List matrix4)
        {
            // TODO : native 'ImageShader_initWithImage';
        }
    }
}
