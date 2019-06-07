namespace FlutterSharp.UI
{
    /// Information for a single frame of an animation.
    ///
    /// To obtain an instance of the [FrameInfo] interface, see
    /// [Codec.getNextFrame].
    public class FrameInfo : NativeFieldWrapperClass2
    {
        /// This class is created by the engine, and should not be instantiated
        /// or extended directly.
        ///
        /// To obtain an instance of the [FrameInfo] interface, see
        /// [Codec.getNextFrame].
        public FrameInfo()
        {

        }

        /// The duration this frame should be shown.
        public Duration Duration => new Duration(milliseconds: DurationMillis);

        private int DurationMillis => 0;// TODO : native 'FrameInfo_durationMillis';

        /// The [Image] object for this frame.
        public Image Image => null; // TODO : native 'FrameInfo_image';
    }
}