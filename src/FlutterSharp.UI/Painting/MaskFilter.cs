using System.Diagnostics;
using static FlutterSharp.UI.UITypes;

namespace FlutterSharp.UI
{
    /// A mask filter to apply to shapes as they are painted. A mask filter is a
    /// function that takes a bitmap of color pixels, and returns another bitmap of
    /// color pixels.
    ///
    /// Instances of this class are used with [Paint.maskFilter] on [Paint] objects.
    public class MaskFilter
    {
        public MaskFilter(BlurStyle style, double sigma)
        {
            this._style = style;
            this._sigma = sigma;
        }

        /// Creates a mask filter that takes the shape being drawn and blurs it.
        ///
        /// This is commonly used to approximate shadows.
        ///
        /// The `style` argument controls the kind of effect to draw; see [BlurStyle].
        ///
        /// The `sigma` argument controls the size of the effect. It is the standard
        /// deviation of the Gaussian blur to apply. The value must be greater than
        /// zero. The sigma corresponds to very roughly half the radius of the effect
        /// in pixels.
        ///
        /// A blur is an expensive operation and should therefore be used sparingly.
        ///
        /// The arguments must not be null.
        ///
        /// See also:
        ///
        ///  * [Canvas.drawShadow], which is a more efficient way to draw shadows.
        public static MaskFilter Blur(BlurStyle _style, double _sigma)
        {
            Debug.Assert(_style != null);
            Debug.Assert(_sigma != null);
            return new MaskFilter(_style, _sigma);
        }
            
        internal readonly BlurStyle _style;
        internal readonly double _sigma;

        // The type of MaskFilter class to create for Skia.
        // These constants must be kept in sync with MaskFilterType in paint.cc.
        internal const int _TypeNone = 0; // null
        internal const int _TypeBlur = 1; // SkBlurMaskFilter

        public override bool Equals(object obj)
        {
            if (obj is MaskFilter typedOther)
                return _style == typedOther._style &&
                       _sigma == typedOther._sigma;

            return false;
        }

        public override int GetHashCode()
        {
            return HashValues(_sigma, _sigma);
        }

        public override string ToString()
        {
            return $"MaskFilter.Blur({_style}, {_sigma.ToStringAsFixed(1)})";
        }
    }
}
