namespace FlutterSharp.UI
{
    /// A handle to an image codec.
    ///
    /// This class is created by the engine, and should not be instantiated
    /// or extended directly.
    ///
    /// To obtain an instance of the [Codec] interface, see
    /// [instantiateImageCodec].
    public class Codec : NativeFieldWrapperClass2
    {
        //
        // This class is created by the engine, and should not be instantiated
        // or extended directly.
        //
        // To obtain an instance of the [Codec] interface, see
        // [instantiateImageCodec].
        public Codec()
        {
        }

        /// Number of frames in this image.
        public int FrameCount => 0; // TODO : native 'Codec_frameCount';

        /// Number of times to repeat the animation.
        ///
        /// * 0 when the animation should be played once.
        /// * -1 for infinity repetitions.
        public int RepetitionCount => 0; //TODO : native 'Codec_repetitionCount';

        /// Fetches the next animation frame.
        ///
        /// Wraps back to the first frame after returning the last frame.
        ///
        /// The returned future can complete with an error if the decoding has failed.
        public Future<FrameInfo> GetNextFrame()
        {
            //return _futurize(_getNextFrame);
            return null;
        }

        /// Returns an error message on failure, null on success.
        private string GetNextFrame(_Callback<FrameInfo> callback)
        {
            // TODO :  native 'Codec_getNextFrame';
            return null;
        }

        /// Release the resources used by this object. The object is no longer usable
        /// after this method is called.
        public void Dispose()
        {
            // TODO : native 'Codec_dispose';
        }
    }
}