using System;

namespace FlutterSharp.UI
{
    /// A transform consisting of a translation, a rotation, and a uniform scale.
    ///
    /// Used by [Canvas.drawAtlas]. This is a more efficient way to represent these
    /// simple transformations than a full matrix.
    // Modeled after Skia's SkRSXform.
    public class RSTransform
    {
        /// Creates an RSTransform.
        ///
        /// An [RSTransform] expresses the combination of a translation, a rotation
        /// around a particular point, and a scale factor.
        ///
        /// The first argument, `scos`, is the cosine of the rotation, multiplied by
        /// the scale factor.
        ///
        /// The second argument, `ssin`, is the sine of the rotation, multiplied by
        /// that same scale factor.
        ///
        /// The third argument is the x coordinate of the translation, minus the
        /// `scos` argument multiplied by the x-coordinate of the rotation point, plus
        /// the `ssin` argument multiplied by the y-coordinate of the rotation point.
        ///
        /// The fourth argument is the y coordinate of the translation, minus the `ssin`
        /// argument multiplied by the x-coordinate of the rotation point, minus the
        /// `scos` argument multiplied by the y-coordinate of the rotation point.
        ///
        /// The [RSTransform.fromComponents] method may be a simpler way to
        /// construct these values. However, if there is a way to factor out the
        /// computations of the sine and cosine of the rotation so that they can be
        /// reused over multiple calls to this constructor, it may be more efficient
        /// to directly use this constructor instead.
        public RSTransform(double scos, double ssin, double tx, double ty)
        {
            _value[0] = scos;
            _value[1] = ssin;
            _value[2] = tx;
            _value[3] = ty;
        }

        /// Creates an RSTransform from its individual components.
        ///
        /// The `rotation` parameter gives the rotation in radians.
        ///
        /// The `scale` parameter describes the uniform scale factor.
        ///
        /// The `anchorX` and `anchorY` parameters give the coordinate of the point
        /// around which to rotate.
        ///
        /// The `translateX` and `translateY` parameters give the coordinate of the
        /// offset by which to translate.
        ///
        /// This constructor computes the arguments of the [new RSTransform]
        /// constructor and then defers to that constructor to actually create the
        /// object. If many [RSTransform] objects are being created and there is a way
        /// to factor out the computations of the sine and cosine of the rotation
        /// (which are computed each time this constructor is called) and reuse them
        /// over multiple [RSTransform] objects, it may be more efficient to directly
        /// use the more direct [new RSTransform] constructor instead.
        public static RSTransform FromComponents(double rotation = 0.0, double scale = 0.0, double anchorX = 0.0,
            double anchorY = 0.0, double translateX = 0.0, double translateY = 0.0)
        {
            var scos = Math.Cos(rotation) * scale;
            var ssin = Math.Sin(rotation) * scale;
            var tx = translateX + -scos * anchorX + ssin * anchorY;
            var ty = translateY + -ssin * anchorX - scos * anchorY;
            return new RSTransform(scos, ssin, tx, ty);
        }

        private readonly Float32List _value = new Float32List(4);

        /// The cosine of the rotation multiplied by the scale factor.
        public double Scos => _value[0];

        /// The sine of the rotation multiplied by that same scale factor.
        public double Ssin => _value[1];

        /// The x coordinate of the translation, minus [scos] multiplied by the
        /// x-coordinate of the rotation point, plus [ssin] multiplied by the
        /// y-coordinate of the rotation point.
        public double Tx => _value[2];

        /// The y coordinate of the translation, minus [ssin] multiplied by the
        /// x-coordinate of the rotation point, minus [scos] multiplied by the
        /// y-coordinate of the rotation point.
        public double Ty => _value[3];
    }
}