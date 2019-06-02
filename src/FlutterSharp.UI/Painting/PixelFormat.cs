namespace FlutterSharp.UI
{
    /// The format of pixel data given to [decodeImageFromPixels].
    public enum PixelFormat
    {
        /// Each pixel is 32 bits, with the highest 8 bits encoding red, the next 8
        /// bits encoding green, the next 8 bits encoding blue, and the lowest 8 bits
        /// encoding alpha.
        Rgba8888,

        /// Each pixel is 32 bits, with the highest 8 bits encoding blue, the next 8
        /// bits encoding green, the next 8 bits encoding red, and the lowest 8 bits
        /// encoding alpha.
        Bgra8888,
    }
}
