using FlutterSharp.UI;
using System;

namespace FlutterSharp.SDK.Internal
{
    public class Matrix4
    {
        private Float64List _m4storage;

        public Float64List Storage => _m4storage;

        public Matrix4()
        {
            this._m4storage = new Float64List(16);
        }

        public static Matrix4 Zero()
        {
            return new Matrix4();
        }

        public void SetIdentity()
        {
            _m4storage[0] = 1.0;
            _m4storage[1] = 0.0;
            _m4storage[2] = 0.0;
            _m4storage[3] = 0.0;
            _m4storage[4] = 0.0;
            _m4storage[5] = 1.0;
            _m4storage[6] = 0.0;
            _m4storage[7] = 0.0;
            _m4storage[8] = 0.0;
            _m4storage[9] = 0.0;
            _m4storage[10] = 1.0;
            _m4storage[11] = 0.0;
            _m4storage[12] = 0.0;
            _m4storage[13] = 0.0;
            _m4storage[14] = 0.0;
            _m4storage[15] = 1.0;
        }

        public static Matrix4 Diagonal3Values(double x, double y, double z)
        {
            var matrix = Matrix4.Zero();
            matrix._m4storage[15] = 1.0;
            matrix._m4storage[10] = z;
            matrix._m4storage[5] = y;
            matrix._m4storage[0] = x;
            return matrix;
        }

        public static Matrix4 Identity()
        {
            var matrix = Matrix4.Zero();
            matrix.SetIdentity();
            return matrix;
        }

        public static Matrix4 TranslationValues(double x, double y, double z)
        {
            var matrix = Matrix4.Zero();
            matrix.SetIdentity();
            matrix.SetTranslationRaw(x, y, z);
            return matrix;
        }

        public void SetTranslationRaw(double x, double y, double z)
        {
            _m4storage[14] = z;
            _m4storage[13] = y;
            _m4storage[12] = x;
        }

        public void Translate(object x, double y = 0.0, double z = 0.0)
        {
            double tx = 0;
            double ty = 0;
            double tz = 0;
            double tw = 1.0; /*x is Vector4 ? x.w : 1.0;
            if (x is Vector3)
            {
                tx = x.x;
                ty = x.y;
                tz = x.z;
            }
            else if (x is Vector4)
            {
                tx = x.x;
                ty = x.y;
                tz = x.z;
            }
            else*/
            if (x is double xDouble)
            {
                tx = xDouble;
                ty = y;
                tz = z;
            }
            double t1 = _m4storage[0] * tx +
                _m4storage[4] * ty +
                _m4storage[8] * tz +
                _m4storage[12] * tw;
            double t2 = _m4storage[1] * tx +
               _m4storage[5] * ty +
               _m4storage[9] * tz +
               _m4storage[13] * tw;
            double t3 = _m4storage[2] * tx +
                _m4storage[6] * ty +
                _m4storage[10] * tz +
                _m4storage[14] * tw;
            double t4 = _m4storage[3] * tx +
                _m4storage[7] * ty +
                _m4storage[11] * tz +
                _m4storage[15] * tw;
            _m4storage[12] = t1;
            _m4storage[13] = t2;
            _m4storage[14] = t3;
            _m4storage[15] = t4;
        }

        public void Multiply(Matrix4 arg)
        {
            double m00 = _m4storage[0];
            double m01 = _m4storage[4];
            double m02 = _m4storage[8];
            double m03 = _m4storage[12];
            double m10 = _m4storage[1];
            double m11 = _m4storage[5];
            double m12 = _m4storage[9];
            double m13 = _m4storage[13];
            double m20 = _m4storage[2];
            double m21 = _m4storage[6];
            double m22 = _m4storage[10];
            double m23 = _m4storage[14];
            double m30 = _m4storage[3];
            double m31 = _m4storage[7];
            double m32 = _m4storage[11];
            double m33 = _m4storage[15];
            Float64List argStorage = arg._m4storage;
            double n00 = argStorage[0];
            double n01 = argStorage[4];
            double n02 = argStorage[8];
            double n03 = argStorage[12];
            double n10 = argStorage[1];
            double n11 = argStorage[5];
            double n12 = argStorage[9];
            double n13 = argStorage[13];
            double n20 = argStorage[2];
            double n21 = argStorage[6];
            double n22 = argStorage[10];
            double n23 = argStorage[14];
            double n30 = argStorage[3];
            double n31 = argStorage[7];
            double n32 = argStorage[11];
            double n33 = argStorage[15];
            _m4storage[0] = (m00 * n00) + (m01 * n10) + (m02 * n20) + (m03 * n30);
            _m4storage[4] = (m00 * n01) + (m01 * n11) + (m02 * n21) + (m03 * n31);
            _m4storage[8] = (m00 * n02) + (m01 * n12) + (m02 * n22) + (m03 * n32);
            _m4storage[12] = (m00 * n03) + (m01 * n13) + (m02 * n23) + (m03 * n33);
            _m4storage[1] = (m10 * n00) + (m11 * n10) + (m12 * n20) + (m13 * n30);
            _m4storage[5] = (m10 * n01) + (m11 * n11) + (m12 * n21) + (m13 * n31);
            _m4storage[9] = (m10 * n02) + (m11 * n12) + (m12 * n22) + (m13 * n32);
            _m4storage[13] = (m10 * n03) + (m11 * n13) + (m12 * n23) + (m13 * n33);
            _m4storage[2] = (m20 * n00) + (m21 * n10) + (m22 * n20) + (m23 * n30);
            _m4storage[6] = (m20 * n01) + (m21 * n11) + (m22 * n21) + (m23 * n31);
            _m4storage[10] = (m20 * n02) + (m21 * n12) + (m22 * n22) + (m23 * n32);
            _m4storage[14] = (m20 * n03) + (m21 * n13) + (m22 * n23) + (m23 * n33);
            _m4storage[3] = (m30 * n00) + (m31 * n10) + (m32 * n20) + (m33 * n30);
            _m4storage[7] = (m30 * n01) + (m31 * n11) + (m32 * n21) + (m33 * n31);
            _m4storage[11] = (m30 * n02) + (m31 * n12) + (m32 * n22) + (m33 * n32);
            _m4storage[15] = (m30 * n03) + (m31 * n13) + (m32 * n23) + (m33 * n33);
        }
    }
}