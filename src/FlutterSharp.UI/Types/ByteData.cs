using System;
using FlutterSharp.UI.Types;

namespace FlutterSharp.UI
{
    // TODO : implement
    public class ByteData
    {
        public ByteData(int v)
        {
        }

        public void SetUint8(int i, char v)
        {

        }

        public void SetInt32(int number, int value, Endian endian)
        {

        }

        internal void SetInt8(int byteCount, int index)
        {
            throw new NotImplementedException();
        }

        internal void SetFloat32(int byteCount, double leading, Endian kFakeHostEndian)
        {
            throw new NotImplementedException();
        }

        internal int GetInt32(int kMaskFilterOffset, Endian kFakeHostEndian)
        {
            throw new NotImplementedException();
        }

        internal double GetFloat32(int kMaskFilterSigmaOffset, Endian kFakeHostEndian)
        {
            throw new NotImplementedException();
        }
    }
}
