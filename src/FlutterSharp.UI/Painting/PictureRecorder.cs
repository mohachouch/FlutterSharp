namespace FlutterSharp.UI
{
    /// Records a [Picture] containing a sequence of graphical operations.
    ///
    /// To begin recording, construct a [Canvas] to record the commands.
    /// To end recording, use the [PictureRecorder.endRecording] method.
    public class PictureRecorder : NativeFieldWrapperClass2
    {
        /// Creates a new idle PictureRecorder. To associate it with a
        /// [Canvas] and begin recording, pass this [PictureRecorder] to the
        /// [Canvas] constructor.
        public PictureRecorder()
        {
            Constructor();
        }

        private void Constructor()
        {
            //TODO : native 'PictureRecorder_constructor';
        }

        /// Whether this object is currently recording commands.
        ///
        /// Specifically, this returns true if a [Canvas] object has been
        /// created to record commands and recording has not yet ended via a
        /// call to [endRecording], and false if either this
        /// [PictureRecorder] has not yet been associated with a [Canvas],
        /// or the [endRecording] method has already been called.
        public bool IsRecording => false;  // TODO : native 'PictureRecorder_isRecording';


        /// Finishes recording graphical operations.
        ///
        /// Returns a picture containing the graphical operations that have been
        /// recorded thus far. After calling this function, both the picture recorder
        /// and the canvas objects are invalid and cannot be used further.
        ///
        /// Returns null if the PictureRecorder is not associated with a canvas.
        public Picture EndRecording()
        {
            // TODO : native 'PictureRecorder_endRecording';
            return null;
        }
    }
}