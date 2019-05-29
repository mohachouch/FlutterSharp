using System;
using System.Diagnostics;
using static FlutterSharp.UI.Lerp;
using static FlutterSharp.UI.UITypes;

namespace FlutterSharp.UI
{
    /// An immutable 32 bit color value in ARGB format.
    ///
    /// Consider the light teal of the Flutter logo. It is fully opaque, with a red
    /// channel value of 0x42 (66), a green channel value of 0xA5 (165), and a blue
    /// channel value of 0xF5 (245). In the common "hash syntax" for color values,
    /// it would be described as `#42A5F5`.
    ///
    /// Here are some ways it could be constructed:
    ///
    /// ```dart
    /// Color c = const Color(0xFF42A5F5);
    /// Color c = const Color.fromARGB(0xFF, 0x42, 0xA5, 0xF5);
    /// Color c = const Color.fromARGB(255, 66, 165, 245);
    /// Color c = const Color.fromRGBO(66, 165, 245, 1.0);
    /// ```
    ///
    /// If you are having a problem with `Color` wherein it seems your color is just
    /// not painting, check to make sure you are specifying the full 8 hexadecimal
    /// digits. If you only specify six, then the leading two digits are assumed to
    /// be zero, which means fully-transparent:
    ///
    /// ```dart
    /// Color c1 = const Color(0xFFFFFF); // fully transparent white (invisible)
    /// Color c2 = const Color(0xFFFFFFFF); // fully opaque white (visible)
    /// ```
    ///
    /// See also:
    ///
    ///  * [Colors](https://docs.flutter.io/flutter/material/Colors-class.html), which
    ///    defines the colors found in the Material Design specification.
    public class Color
    {
        /// Construct a color from the lower 32 bits of an [int].
        ///
        /// The bits are interpreted as follows:
        ///
        /// * Bits 24-31 are the alpha value.
        /// * Bits 16-23 are the red value.
        /// * Bits 8-15 are the green value.
        /// * Bits 0-7 are the blue value.
        ///
        /// In other words, if AA is the alpha value in hex, RR the red value in hex,
        /// GG the green value in hex, and BB the blue value in hex, a color can be
        /// expressed as `const Color(0xAARRGGBB)`.
        ///
        /// For example, to get a fully opaque orange, you would use `const
        /// Color(0xFFFF9000)` (`FF` for the alpha, `FF` for the red, `90` for the
        /// green, and `00` for the blue).
        public Color(long value)
        {
            this.Value = (int)(value & 0xFFFFFFFF);
        }

        /// Construct a color from the lower 8 bits of four integers.
        ///
        /// * `a` is the alpha value, with 0 being transparent and 255 being fully
        ///   opaque.
        /// * `r` is [red], from 0 to 255.
        /// * `g` is [green], from 0 to 255.
        /// * `b` is [blue], from 0 to 255.
        ///
        /// Out of range values are brought into range using modulo 255.
        ///
        /// See also [fromRGBO], which takes the alpha value as a floating point
        /// value.
        public static Color FromARGB(int a, int r, int g, int b)
        {
            var value = (((a & 0xff) << 24) |
             ((r & 0xff) << 16) |
             ((g & 0xff) << 8) |
             ((b & 0xff) << 0)) & 0xFFFFFFFF;

            return new Color(value);
        }

        /// Create a color from red, green, blue, and opacity, similar to `rgba()` in CSS.
        ///
        /// * `r` is [red], from 0 to 255.
        /// * `g` is [green], from 0 to 255.
        /// * `b` is [blue], from 0 to 255.
        /// * `opacity` is alpha channel of this color as a double, with 0.0 being
        ///   transparent and 1.0 being fully opaque.
        ///
        /// Out of range values are brought into range using modulo 255.
        ///
        /// See also [fromARGB], which takes the opacity as an integer value.
        public static Color FromRGBO(int r, int g, int b, double opacity)
        {
            var value = (((int)(Math.Truncate(opacity * 0xff / 1.0)) & 0xff << 24) |
             ((r & 0xff) << 16) |
             ((g & 0xff) << 8) |
             ((b & 0xff) << 0)) & 0xFFFFFFFF;

            return new Color(value);
        }


        /// A 32 bit value representing this color.
        ///
        /// The bits are assigned as follows:
        ///
        /// * Bits 24-31 are the alpha value.
        /// * Bits 16-23 are the red value.
        /// * Bits 8-15 are the green value.
        /// * Bits 0-7 are the blue value.
        public readonly int Value;

        /// The alpha channel of this color in an 8 bit value.
        ///
        /// A value of 0 means this color is fully transparent. A value of 255 means
        /// this color is fully opaque.
        public int Alpha => (int)(0xff000000 & Value) >> 24;

        /// The alpha channel of this color as a double.
        ///
        /// A value of 0.0 means this color is fully transparent. A value of 1.0 means
        /// this color is fully opaque.
        public double Opacity => Alpha / 0xFF;

        /// The red channel of this color in an 8 bit value.
        public int Red => (0x00ff0000 & Value) >> 16;

        /// The green channel of this color in an 8 bit value.
        public int Green => (0x0000ff00 & Value) >> 8;

        /// The blue channel of this color in an 8 bit value.
        public int Blue => (0x000000ff & Value) >> 0;

        /// Returns a new color that matches this color with the alpha channel
        /// replaced with `a` (which ranges from 0 to 255).
        ///
        /// Out of range values will have unexpected effects.
        public Color WithAlpha(int a) => FromARGB(a, Red, Green, Blue);

        /// Returns a new color that matches this color with the alpha channel
        /// replaced with the given `opacity` (which ranges from 0.0 to 1.0).
        ///
        /// Out of range values will have unexpected effects.
        public Color WithOpacity(double opacity)
        {
            Debug.Assert(opacity >= 0.0 && opacity <= 1.0);
            return WithAlpha((int)(255.0 * opacity).Round());
        }

        /// Returns a new color that matches this color with the red channel replaced
        /// with `r` (which ranges from 0 to 255).
        ///
        /// Out of range values will have unexpected effects.
        public Color WithRed(int r) => FromARGB(Alpha, r, Green, Blue);

        /// Returns a new color that matches this color with the green channel
        /// replaced with `g` (which ranges from 0 to 255).
        ///
        /// Out of range values will have unexpected effects.
        public Color WithGreen(int g) => FromARGB(Alpha, Red, g, Blue);
      
        /// Returns a new color that matches this color with the blue channel replaced
        /// with `b` (which ranges from 0 to 255).
        ///
        /// Out of range values will have unexpected effects.
        public Color WithBlue(int b) => FromARGB(Alpha, Red, Green, b);

        // See <https://www.w3.org/TR/WCAG20/#relativeluminancedef>
        private static double LinearizeColorComponent(double component)
        {
            if (component <= 0.03928)
                return component / 12.92;
            return Math.Pow((component + 0.055) / 1.055, 2.4);
        }

        /// Returns a brightness value between 0 for darkest and 1 for lightest.
        ///
        /// Represents the relative luminance of the color. This value is computationally
        /// expensive to calculate.
        ///
        /// See <https://en.wikipedia.org/wiki/Relative_luminance>.
        public double ComputeLuminance()
        {
            // See <https://www.w3.org/TR/WCAG20/#relativeluminancedef>
            double R = LinearizeColorComponent(Red / 0xFF);
            double G = LinearizeColorComponent(Green / 0xFF);
            double B = LinearizeColorComponent(Blue / 0xFF);
            return 0.2126 * R + 0.7152 * G + 0.0722 * B;
        }

        /// Linearly interpolate between two colors.
        ///
        /// This is intended to be fast but as a result may be ugly. Consider
        /// [HSVColor] or writing custom logic for interpolating colors.
        ///
        /// If either color is null, this function linearly interpolates from a
        /// transparent instance of the other color. This is usually preferable to
        /// interpolating from [material.Colors.transparent] (`const
        /// Color(0x00000000)`), which is specifically transparent _black_.
        ///
        /// The `t` argument represents position on the timeline, with 0.0 meaning
        /// that the interpolation has not started, returning `a` (or something
        /// equivalent to `a`), 1.0 meaning that the interpolation has finished,
        /// returning `b` (or something equivalent to `b`), and values in between
        /// meaning that the interpolation is at the relevant point on the timeline
        /// between `a` and `b`. The interpolation can be extrapolated beyond 0.0 and
        /// 1.0, so negative values and values greater than 1.0 are valid (and can
        /// easily be generated by curves such as [Curves.elasticInOut]). Each channel
        /// will be clamped to the range 0 to 255.
        ///
        /// Values for `t` are usually obtained from an [Animation<double>], such as
        /// an [AnimationController].
        public static Color Lerp(Color a, Color b, double t)
        {
            if (a == null && b == null)
                return null;
            if (a == null)
                return ScaleAlpha(b, t);
            if (b == null)
                return ScaleAlpha(a, 1.0 - t);
            return FromARGB(
              LerpDouble(a.Alpha, b.Alpha, t).ToInt().Clamp(0, 255),
              LerpDouble(a.Red, b.Red, t).ToInt().Clamp(0, 255),
              LerpDouble(a.Green, b.Green, t).ToInt().Clamp(0, 255),
              LerpDouble(a.Blue, b.Blue, t).ToInt().Clamp(0, 255));
        }

        /// Combine the foreground color as a transparent color over top
        /// of a background color, and return the resulting combined color.
        ///
        /// This uses standard alpha blending ("SRC over DST") rules to produce a
        /// blended color from two colors. This can be used as a performance
        /// enhancement when trying to avoid needless alpha blending compositing
        /// operations for two things that are solid colors with the same shape, but
        /// overlay each other: instead, just paint one with the combined color.
        public static Color AlphaBlend(Color foreground, Color background)
        {
            int alpha = foreground.Alpha;
            if (alpha == 0x00)
            { // Foreground completely transparent.
                return background;
            }
            int invAlpha = 0xff - alpha;
            int backAlpha = background.Alpha;
            if (backAlpha == 0xff)
            { // Opaque background case
                return Color.FromARGB(
                  0xff,
                  (int)Math.Truncate((double)(alpha * foreground.Red + invAlpha * background.Red) / 0xff),
                  (int)Math.Truncate((double)(alpha * foreground.Green + invAlpha * background.Green) / 0xff),
                  (int)Math.Truncate((double)(alpha * foreground.Blue + invAlpha * background.Blue) / 0xff));
            }
            else
            { // General case
                backAlpha = (int)Math.Truncate((double)(backAlpha * invAlpha) / 0xff);
                int outAlpha = alpha + backAlpha;
                Debug.Assert(outAlpha != 0x00);
                return Color.FromARGB(
                  outAlpha,
                  (int)Math.Truncate((double)(foreground.Red * alpha + background.Red * backAlpha) / outAlpha),
                  (int)Math.Truncate((double)(foreground.Green * alpha + background.Green * backAlpha) / outAlpha),
                  (int)Math.Truncate((double)(foreground.Blue * alpha + background.Blue * backAlpha) / outAlpha));
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is Color typedOther)
                return Value == typedOther.Value;

            return false;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return $"Color(0x:{Value.ToRadixString(16).PadLeft(8, '0')})";
        }
    }
}
