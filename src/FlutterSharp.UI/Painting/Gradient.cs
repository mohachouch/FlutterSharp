using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace FlutterSharp.UI
{
    /// A shader (as used by [Paint.shader]) that renders a color gradient.
    ///
    /// There are several types of gradients, represented by the various constructors
    /// on this class.
    ///
    /// See also:
    ///
    ///  * [Gradient](https://api.flutter.dev/flutter/painting/Gradient-class.html), the class in the [painting] library.
    ///
    public class Gradient : Shader
    {
        private void Constructor()
        {
            // TODO : native 'Gradient_constructor';
        }

        /// Creates a linear gradient from `from` to `to`.
        ///
        /// If `colorStops` is provided, `colorStops[i]` is a number from 0.0 to 1.0
        /// that specifies where `color[i]` begins in the gradient. If `colorStops` is
        /// not provided, then only two stops, at 0.0 and 1.0, are implied (and
        /// `color` must therefore only have two entries).
        ///
        /// The behavior before `from` and after `to` is described by the `tileMode`
        /// argument. For details, see the [TileMode] enum.
        ///
        /// ![](https://flutter.github.io/assets-for-api-docs/assets/dart-ui/tile_mode_clamp_linear.png)
        /// ![](https://flutter.github.io/assets-for-api-docs/assets/dart-ui/tile_mode_mirror_linear.png)
        /// ![](https://flutter.github.io/assets-for-api-docs/assets/dart-ui/tile_mode_repeated_linear.png)
        ///
        /// If `from`, `to`, `colors`, or `tileMode` are null, or if `colors` or
        /// `colorStops` contain null values, this constructor will throw a
        /// [NoSuchMethodError].
        ///
        /// If `matrix4` is provided, the gradient fill will be transformed by the
        /// specified 4x4 matrix relative to the local coordinate system. `matrix4` must
        /// be a column-major matrix packed into a list of 16 values.
        public static Gradient Linear(
          Offset from,
          Offset to,
          List<Color> colors,
          List<double> colorStops = null,
          TileMode tileMode = TileMode.Clamp,
          Float64List matrix4 = null)
        {
            Debug.Assert(_offsetIsValid(from));
            Debug.Assert(_offsetIsValid(to));
            Debug.Assert(colors != null);
            Debug.Assert(tileMode != null);
            Debug.Assert(matrix4 == null || _matrix4IsValid(matrix4));

            ValidateColorStops(colors, colorStops);

            Float32List endPointsBuffer = _encodeTwoPoints(from, to);
            Int32List colorsBuffer = _encodeColorList(colors);
            Float32List colorStopsBuffer = colorStops == null ? null : Float32List.FromList(colorStops);

            var gradient = new Gradient();
            gradient.Constructor();
            gradient.InitLinear(endPointsBuffer, colorsBuffer, colorStopsBuffer, (int)tileMode, matrix4);
            return gradient;
        }

        private void InitLinear(Float32List endPoints, Int32List colors, Float32List colorStops, int tileMode, Float64List matrix4)
        {
            // TODO : native 'Gradient_initLinear';
        }

        /// Creates a radial gradient centered at `center` that ends at `radius`
        /// distance from the center.
        ///
        /// If `colorStops` is provided, `colorStops[i]` is a number from 0.0 to 1.0
        /// that specifies where `color[i]` begins in the gradient. If `colorStops` is
        /// not provided, then only two stops, at 0.0 and 1.0, are implied (and
        /// `color` must therefore only have two entries).
        ///
        /// The behavior before and after the radius is described by the `tileMode`
        /// argument. For details, see the [TileMode] enum.
        ///
        /// ![](https://flutter.github.io/assets-for-api-docs/assets/dart-ui/tile_mode_clamp_radial.png)
        /// ![](https://flutter.github.io/assets-for-api-docs/assets/dart-ui/tile_mode_mirror_radial.png)
        /// ![](https://flutter.github.io/assets-for-api-docs/assets/dart-ui/tile_mode_repeated_radial.png)
        ///
        /// If `center`, `radius`, `colors`, or `tileMode` are null, or if `colors` or
        /// `colorStops` contain null values, this constructor will throw a
        /// [NoSuchMethodError].
        ///
        /// If `matrix4` is provided, the gradient fill will be transformed by the
        /// specified 4x4 matrix relative to the local coordinate system. `matrix4` must
        /// be a column-major matrix packed into a list of 16 values.
        ///
        /// If `focal` is provided and not equal to `center` and `focalRadius` is
        /// provided and not equal to 0.0, the generated shader will be a two point
        /// conical radial gradient, with `focal` being the center of the focal
        /// circle and `focalRadius` being the radius of that circle. If `focal` is
        /// provided and not equal to `center`, at least one of the two offsets must
        /// not be equal to [Offset.zero].
        public static Gradient Radial(
          Offset center,
          double radius,
          List<Color> colors,
          List<double> colorStops = null,
          TileMode tileMode = TileMode.Clamp,
          Float64List matrix4 = null,
          Offset focal = null,
          double focalRadius = 0.0)
        {
            Debug.Assert(_offsetIsValid(center));
            Debug.Assert(colors != null);
            Debug.Assert(tileMode != null);
            Debug.Assert(matrix4 == null || _matrix4IsValid(matrix4));

            //focalRadius ??= 0.0; is not nullable double in CSharp

            ValidateColorStops(colors, colorStops);
            Int32List colorsBuffer = EncodeColorList(colors);
            Float32List colorStopsBuffer = colorStops == null ? null : Float32List.FromList(colorStops);

            Gradient gradient = new Gradient();

            // If focal is null or focal radius is null, this should be treated as a regular radial gradient
            // If focal == center and the focal radius is 0.0, it's still a regular radial gradient
            if (focal == null || (focal == center && focalRadius == 0.0))
            {
                gradient.Constructor();
                gradient.InitRadial(center.Dx, center.Dy, radius, colorsBuffer, colorStopsBuffer, (int)tileMode, matrix4);
            }
            else
            {
                Debug.Assert(center != Offset.Zero || focal != Offset.Zero); // will result in exception(s) in Skia side
                gradient.Constructor();
                gradient.InitConical(focal.Dx, focal.Dy, focalRadius, center.Dx, center.Dy, radius, colorsBuffer, colorStopsBuffer, (int)tileMode, matrix4);
            }

            return gradient;
        }

        private void InitRadial(double centerX, double centerY, double radius, Int32List colors, Float32List colorStops, int tileMode, Float64List matrix4)
        {
            // TODO : native 'Gradient_initRadial';
        }

        private void InitConical(double startX, double startY, double startRadius, double endX, double endY, double endRadius, Int32List colors, Float32List colorStops, int tileMode, Float64List matrix4)
        {
            // TODO : native 'Gradient_initTwoPointConical';
        }

        /// Creates a sweep gradient centered at `center` that starts at `startAngle`
        /// and ends at `endAngle`.
        ///
        /// `startAngle` and `endAngle` should be provided in radians, with zero
        /// radians being the horizontal line to the right of the `center` and with
        /// positive angles going clockwise around the `center`.
        ///
        /// If `colorStops` is provided, `colorStops[i]` is a number from 0.0 to 1.0
        /// that specifies where `color[i]` begins in the gradient. If `colorStops` is
        /// not provided, then only two stops, at 0.0 and 1.0, are implied (and
        /// `color` must therefore only have two entries).
        ///
        /// The behavior before `startAngle` and after `endAngle` is described by the
        /// `tileMode` argument. For details, see the [TileMode] enum.
        ///
        /// ![](https://flutter.github.io/assets-for-api-docs/assets/dart-ui/tile_mode_clamp_sweep.png)
        /// ![](https://flutter.github.io/assets-for-api-docs/assets/dart-ui/tile_mode_mirror_sweep.png)
        /// ![](https://flutter.github.io/assets-for-api-docs/assets/dart-ui/tile_mode_repeated_sweep.png)
        ///
        /// If `center`, `colors`, `tileMode`, `startAngle`, or `endAngle` are null,
        /// or if `colors` or `colorStops` contain null values, this constructor will
        /// throw a [NoSuchMethodError].
        ///
        /// If `matrix4` is provided, the gradient fill will be transformed by the
        /// specified 4x4 matrix relative to the local coordinate system. `matrix4` must
        /// be a column-major matrix packed into a list of 16 values.
        public static Gradient Sweep(
          Offset center,
          List<Color> colors,
          List<double> colorStops = null,
          TileMode tileMode = TileMode.Clamp,
          double startAngle = 0.0,
          double endAngle = Math.PI * 2,
          Float64List matrix4 = null)
        {
            Debug.Assert(OffsetIsValid(center));
            Debug.Assert(colors != null);
            Debug.Assert(tileMode != null);
            Debug.Assert(startAngle != null);
            Debug.Assert(endAngle != null);
            Debug.Assert(startAngle < endAngle);
            Debug.Assert(matrix4 == null || Matrix4IsValid(matrix4));

            ValidateColorStops(colors, colorStops);
            Int32List colorsBuffer = _encodeColorList(colors);
            Float32List colorStopsBuffer = colorStops == null ? null : Float32List.FromList(colorStops);

            var gradient = new Gradient();
            gradient.Constructor();
            gradient.InitSweep(center.Dx, center.Dy, colorsBuffer, colorStopsBuffer, (int)tileMode, startAngle, endAngle, matrix4);
            return gradient;
        }

        private void InitSweep(double centerX, double centerY, Int32List colors, Float32List colorStops, int tileMode, double startAngle, double endAngle, Float64List matrix)
        {
            // TODO : native 'Gradient_initSweep';
        }

        public static void ValidateColorStops(List<Color> colors, List<double> colorStops)
        {
            if (colorStops == null)
            {
                if (colors.Count != 2)
                    throw new ArgumentException("'colors' must have length 2 if 'colorStops' is omitted.");
            }
            else
            {
                if (colors.Count != colorStops.Count)
                    throw new ArgumentException("'colors' and 'colorStops' arguments must have equal length.");
            }
        }
    }
}
    

