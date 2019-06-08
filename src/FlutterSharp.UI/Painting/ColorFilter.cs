using System.Collections.Generic;
using static FlutterSharp.UI.UITypes;

namespace FlutterSharp.UI
{
    /// A description of a color filter to apply when drawing a shape or compositing
    /// a layer with a particular [Paint]. A color filter is a function that takes
    /// two colors, and outputs one color. When applied during compositing, it is
    /// independently applied to each pixel of the layer being drawn before the
    /// entire layer is merged with the destination.
    ///
    /// Instances of this class are used with [Paint.colorFilter] on [Paint]
    /// objects.
    public class ColorFilter
    {
        public ColorFilter(int typeMode, Color color = null, BlendMode? blendMode = null, List<double> matrix = null)
        {
            _color = color;
            _blendMode = blendMode;
            _matrix = matrix;
            _type = typeMode;
        }

        /// Creates a color filter that applies the blend mode given as the second
        /// argument. The source color is the one given as the first argument, and the
        /// destination color is the one from the layer being composited.
        ///
        /// The output of this filter is then composited into the background according
        /// to the [Paint.blendMode], using the output of this filter as the source
        /// and the background as the destination.
        public static ColorFilter Mode(Color color, BlendMode blendMode)
        {
            return new ColorFilter(_TypeMode, color: color, blendMode: blendMode);
        }

        /// Construct a color filter that transforms a color by a 4x5 matrix. The
        /// matrix is in row-major order and the translation column is specified in
        /// unnormalized, 0...255, space.
        public static ColorFilter Matrix(List<double> matrix)
        {
            return new ColorFilter(_TypeMatrix, matrix: matrix);
        }

        /// Construct a color filter that applies the sRGB gamma curve to the RGB
        /// channels.
        public static ColorFilter LinearToSrgbGamma()
        {
            return new ColorFilter(_TypeLinearToSrgbGamma);
        }

        /// Creates a color filter that applies the inverse of the sRGB gamma curve
        /// to the RGB channels.
        public static ColorFilter SrgbToLinearGamma()
        {
            return new ColorFilter(_TypeSrgbToLinearGamma);
        }

        internal readonly Color _color;
        internal readonly BlendMode? _blendMode;
        internal readonly List<double> _matrix;
        internal readonly int _type;
        
        // The type of SkColorFilter class to create for Skia.
        // These constants must be kept in sync with ColorFilterType in paint.cc.
        internal const int _TypeNone = 0; // null
        internal const int _TypeMode = 1; // MakeModeFilter
        internal const int _TypeMatrix = 2; // MakeMatrixFilterRowMajor255
        internal const int _TypeLinearToSrgbGamma = 3; // MakeLinearToSRGBGamma
        internal const int _TypeSrgbToLinearGamma = 4; // MakeSRGBToLinearGamma

        public override bool Equals(object obj)
        {
            if (obj is ColorFilter typedOther)
            {
                if (_type != typedOther._type)
                {
                    return false;
                }
                if (!ListEquals<double>(_matrix, typedOther._matrix))
                {
                    return false;
                }

                return _color == typedOther._color && _blendMode == typedOther._blendMode;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return HashValues(_color, _blendMode, HashList(_matrix), _type);
        }

        public override string ToString()
        {
            switch (_type)
            {
                case _TypeMode:
                    return $"ColorFilter.Mode({_color}, {_blendMode})";
                case _TypeMatrix:
                    return $"ColorFilter.Matrix({_matrix})";
                case _TypeLinearToSrgbGamma:
                    return "ColorFilter.LinearToSrgbGamma()";
                case _TypeSrgbToLinearGamma:
                    return "ColorFilter.SrgbToLinearGamma()";
                default:
                    return "Unknown ColorFilter type. This is an error. If you\'re seeing this, please file an issue at https://github.com/flutter/flutter/issues/new.";
            }
        }
    }
}
