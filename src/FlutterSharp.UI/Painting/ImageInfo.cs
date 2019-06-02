namespace FlutterSharp.UI
{
    internal class ImageInfo
    {
        public ImageInfo(int width, int height, int format, int? rowBytes)
        {
            this.Width = width;
            this.Height = height;
            this.Format = format;
            this.RowBytes = rowBytes == null ? this.Width * 4 : rowBytes.Value;
        }
        
        public readonly int Width;

        public readonly int Height;

        public readonly int Format;

        public readonly int RowBytes;
    }
}
