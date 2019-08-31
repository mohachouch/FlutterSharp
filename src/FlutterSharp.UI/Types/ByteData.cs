using System;
using System.IO;
using FlutterSharp.UI.Types;

namespace FlutterSharp.UI
{
    // TODO : implement
    public class ByteData
    {
        private BinaryWriter _dataWriter;
        private BinaryReader _dataReader;
        private byte[] array;

        public byte[] Data { get; private set; }

        public int LengthInBytes => Data.Length;

        public ByteData(int capacity)
        {
            Data = new byte[capacity];
            _dataWriter = new BinaryWriter(new MemoryStream(Data));
            _dataReader = new BinaryReader(new MemoryStream(this.Data));
        }

        public ByteData(byte[] array)
        {
            Data = array;
            _dataWriter = new BinaryWriter(new MemoryStream(Data));
            _dataReader = new BinaryReader(new MemoryStream(this.Data));
        }

        public void SetUint8(int i, char v)
        {

        }

        public void SetInt32(int offset, int value, Endian endian)
        {
            this._dataWriter.BaseStream.Position = offset;
            this._dataWriter.Write(value);
        }

        public int GetInt32(int offset, Endian endian)
        {
            this._dataReader.BaseStream.Position = offset;
            return this._dataReader.ReadInt32();
        } 

        internal void SetInt8(int byteCount, int index)
        {

        }

        internal void SetFloat32(int offset, double value, Endian kFakeHostEndian)
        {
            this._dataWriter.BaseStream.Position = offset;
            this._dataWriter.Write((float)value);
        }

        internal double GetFloat32(int offset, Endian kFakeHostEndian)
        {
            this._dataReader.BaseStream.Position = offset;
            return this._dataReader.ReadSingle();
        }

        internal long GetInt64(int offset, Endian kFakeHostEndian)
        {
            this._dataReader.BaseStream.Position = offset;
            return this._dataReader.ReadInt64();
        }

        internal double GetFloat64(int offset, Endian kFakeHostEndian)
        {
            this._dataReader.BaseStream.Position = offset;
            return this._dataReader.ReadDouble();
        }
    }
}
