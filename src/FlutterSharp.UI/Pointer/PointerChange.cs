namespace FlutterSharp.UI
{
    /// How the pointer has changed since the last report.
    public enum PointerChange
    {
        /// The input from the pointer is no longer directed towards this receiver.
        Cancel,

        /// The device has started tracking the pointer.
        ///
        /// For example, the pointer might be hovering above the device, having not yet
        /// made contact with the surface of the device.
        Add,

        /// The device is no longer tracking the pointer.
        ///
        /// For example, the pointer might have drifted out of the device's hover
        /// detection range or might have been disconnected from the system entirely.
        Remove,

        /// The pointer has moved with respect to the device while not in contact with
        /// the device.
        Hover,

        /// The pointer has made contact with the device.
        Down,

        /// The pointer has moved with respect to the device while in contact with the
        /// device.
        Move,

        /// The pointer has stopped making contact with the device.
        Up,
    }
}
