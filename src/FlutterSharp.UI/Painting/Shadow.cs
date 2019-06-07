using System;
using System.Collections.Generic;
using System.Diagnostics;
using static FlutterSharp.UI.Lerp;
using static FlutterSharp.UI.UITypes;

namespace FlutterSharp.UI
{
    /// A single shadow.
    ///
    /// Multiple shadows are stacked together in a [TextStyle].
    public class Shadow
    {
        /// Construct a shadow.
        ///
        /// The default shadow is a black shadow with zero offset and zero blur.
        /// Default shadows should be completely covered by the casting element,
        /// and not be visible.
        ///
        /// Transparency should be adjusted through the [color] alpha.
        ///
        /// Shadow order matters due to compositing multiple translucent objects not
        /// being commutative.
        public Shadow(Color color = null, Offset offset = null, double blurRadius = 0.0)
        {
            if (color == null)
                color = new Color(_kColorDefault);

            if (offset == null)
                offset = Offset.Zero;

            Debug.Assert(color != null, "Text shadow color was null.");
            Debug.Assert(offset != null, "Text shadow offset was null.");
            Debug.Assert(blurRadius >= 0.0, "Text shadow blur radius should be non-negative.");

            this.Color = color;
            this.Offset = offset;
            this.BlurRadius = blurRadius;
        }

        public static readonly uint _kColorDefault = 0xFF000000;

        // Constants for shadow encoding.
        public static readonly int _kBytesPerShadow = 16;
        public static readonly int _kColorOffset = 0 << 2;
        public static readonly int _kXOffset = 1 << 2;
        public static readonly int _kYOffset = 2 << 2;
        public static readonly int _kBlurOffset = 3 << 2;

        /// Color that the shadow will be drawn with.
        ///
        /// The shadows are shapes composited directly over the base canvas, and do not
        /// represent optical occlusion.
        public readonly Color Color;

        /// The displacement of the shadow from the casting element.
        ///
        /// Positive x/y offsets will shift the shadow to the right and down, while
        /// negative offsets shift the shadow to the left and up. The offsets are
        /// relative to the position of the element that is casting it.
        public readonly Offset Offset;

        /// The standard deviation of the Gaussian to convolve with the shadow's shape.
        public readonly double BlurRadius;

        /// Converts a blur radius in pixels to sigmas.
        ///
        /// See the sigma argument to [MaskFilter.blur].
        ///
        // See SkBlurMask::ConvertRadiusToSigma().
        // <https://github.com/google/skia/blob/bb5b77db51d2e149ee66db284903572a5aac09be/src/effects/SkBlurMask.cpp#L23>
        public static double ConvertRadiusToSigma(double radius)
        {
            return radius * 0.57735 + 0.5;
        }

        /// The [blurRadius] in sigmas instead of logical pixels.
        ///
        /// See the sigma argument to [MaskFilter.blur].
        public double BlurSigma => ConvertRadiusToSigma(BlurRadius);

        /// Create the [Paint] object that corresponds to this shadow description.
        ///
        /// The [offset] is not represented in the [Paint] object.
        /// To honor this as well, the shape should be translated by [offset] before
        /// being filled using this [Paint].
        ///
        /// This class does not provide a way to disable shadows to avoid
        /// inconsistencies in shadow blur rendering, primarily as a method of
        /// reducing test flakiness. [toPaint] should be overridden in subclasses to
        /// provide this functionality.
        public Paint ToPaint()
        {
            return new Paint
            {
                Color = Color,
                MaskFilter = MaskFilter.Blur(BlurStyle.Normal, BlurSigma)
            };
        }

        /// Returns a new shadow with its [offset] and [blurRadius] scaled by the given
        /// factor.
        public Shadow Scale(double factor)
        {
            return new Shadow
            (
                color: Color,
                offset: Offset * factor,
                blurRadius: BlurRadius * factor
            );
        }

        /// Linearly interpolate between two shadows.
        ///
        /// If either shadow is null, this function linearly interpolates from a
        /// a shadow that matches the other shadow in color but has a zero
        /// offset and a zero blurRadius.
        ///
        /// {@template dart.ui.shadow.lerp}
        /// The `t` argument represents position on the timeline, with 0.0 meaning
        /// that the interpolation has not started, returning `a` (or something
        /// equivalent to `a`), 1.0 meaning that the interpolation has finished,
        /// returning `b` (or something equivalent to `b`), and values in between
        /// meaning that the interpolation is at the relevant point on the timeline
        /// between `a` and `b`. The interpolation can be extrapolated beyond 0.0 and
        /// 1.0, so negative values and values greater than 1.0 are valid (and can
        /// easily be generated by curves such as [Curves.elasticInOut]).
        ///
        /// Values for `t` are usually obtained from an [Animation<double>], such as
        /// an [AnimationController].
        /// {@endtemplate}
        public static Shadow Lerp(Shadow a, Shadow b, double t)
        {
            if (a == null && b == null)
                return null;
            if (a == null)
                return b.Scale(t);
            if (b == null)
                return a.Scale(1.0 - t);
            return new Shadow(
                color: Color.Lerp(a.Color, b.Color, t),
                offset: Offset.Lerp(a.Offset, b.Offset, t),
                blurRadius: LerpDouble(a.BlurRadius, b.BlurRadius, t));
        }

        /// Linearly interpolate between two lists of shadows.
        ///
        /// If the lists differ in length, excess items are lerped with null.
        ///
        /// {@macro dart.ui.shadow.lerp}
        public static List<Shadow> LerpList(List<Shadow> a, List<Shadow> b, double t)
        {
            if (a == null && b == null)
                return null;

            if (a == null)
                a = new List<Shadow>();

            if (b == null)
                b = new List<Shadow>();

            List<Shadow> result = new List<Shadow>();
            int commonLength = Math.Min(a.Count, b.Count);
            for (int i = 0; i < commonLength; i += 1)
                result.Add(Shadow.Lerp(a[i], b[i], t));
            for (int i = commonLength; i < a.Count; i += 1)
                result.Add(a[i].Scale(1.0 - t));
            for (int i = commonLength; i < b.Count; i += 1)
                result.Add(b[i].Scale(t));
            return result;
        }

        public override bool Equals(object obj)
        {
            if (obj is Shadow typedOther)
                return Color == typedOther.Color &&
                       Offset == typedOther.Offset &&
                       BlurRadius == typedOther.BlurRadius;

            return false;
        }

        public override int GetHashCode()
        {
            return HashValues(Color, Offset, BlurRadius);
        }

        // Serialize [shadows] into ByteData. The format is a single uint_32_t at
        // the beginning indicating the number of shadows, followed by _kBytesPerShadow
        // bytes for each shadow.
        private static ByteData EncodeShadows(List<Shadow> shadows)
        {
            if (shadows == null)
                return new ByteData(0);

            int byteCount = shadows.Count * _kBytesPerShadow;
            ByteData shadowsData = new ByteData(byteCount);

            int shadowOffset = 0;
            for (int shadowIndex = 0; shadowIndex < shadows.Count; ++shadowIndex)
            {
                Shadow shadow = shadows[shadowIndex];
                if (shadow == null)
                    continue;
                shadowOffset = shadowIndex * _kBytesPerShadow;

                shadowsData.SetInt32(_kColorOffset + shadowOffset,
                    (int)(shadow.Color.Value ^ Shadow._kColorDefault), _kFakeHostEndian); // TODO : show cast to int is valid

                shadowsData.SetFloat32(_kXOffset + shadowOffset,
                    shadow.Offset.Dx, _kFakeHostEndian);

                shadowsData.SetFloat32(_kYOffset + shadowOffset,
                    shadow.Offset.Dy, _kFakeHostEndian);

                shadowsData.SetFloat32(_kBlurOffset + shadowOffset,
                    shadow.BlurRadius, _kFakeHostEndian);
            }

            return shadowsData;
        }

        public override string ToString()
        {
            return $"TextShadow({Color}, {Offset}, {BlurRadius})";
        }
    }
}