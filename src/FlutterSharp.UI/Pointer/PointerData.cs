namespace FlutterSharp.UI
{
    /// Information about the state of a pointer.
    public class PointerData
    {
        public PointerData(Duration timeStamp,
                PointerChange change,
                PointerDeviceKind kind,
                PointerSignalKind signalKind,
                int device,
                double physicalX,
                double physicalY,
                int buttons,
                bool obscured,
                double pressure,
                double pressureMin,
                double pressureMax,
                double distance,
                double distanceMax,
                double size,
                double radiusMajor,
                double radiusMinor,
                double radiusMin,
                double radiusMax,
                double orientation,
                double tilt,
                int platformData,
                double scrollDeltaX,
                double scrollDeltaY)
        {
            this.TimeStamp = timeStamp;
            this.Change = change;
            this.Kind = kind;
            this.SignalKind = signalKind;
            this.Device = device;
            this.PhysicalX = physicalX;
            this.PhysicalY = physicalY;
            this.Buttons = buttons;
            this.Obscured = obscured;
            this.Pressure = pressure;
            this.PressureMin = pressureMin;
            this.PressureMax = pressureMax;
            this.Distance = distance;
            this.DistanceMax = distanceMax;
            this.Size = size;
            this.RadiusMajor = radiusMajor;
            this.RadiusMinor = radiusMinor;
            this.RadiusMin = radiusMin;
            this.RadiusMax = radiusMax;
            this.Orientation = orientation;
            this.Tilt = tilt;
            this.PlatformData = platformData;
            this.ScrollDeltaX = scrollDeltaX;
            this.ScrollDeltaY = scrollDeltaY;
        }

        /// Time of event dispatch, relative to an arbitrary timeline.
        public readonly Duration TimeStamp;

        /// How the pointer has changed since the last report.
        public readonly PointerChange Change;

        /// The kind of input device for which the event was generated.
        public readonly PointerDeviceKind Kind;

        /// The kind of signal for a pointer signal event.
        public readonly PointerSignalKind SignalKind;

        /// Unique identifier for the pointing device, reused across interactions.
        public readonly int Device;

        /// X coordinate of the position of the pointer, in physical pixels in the
        /// global coordinate space.
        public readonly double PhysicalX;

        /// Y coordinate of the position of the pointer, in physical pixels in the
        /// global coordinate space.
        public readonly double PhysicalY;

        /// Bit field using the *Button constants (primaryMouseButton,
        /// secondaryStylusButton, etc). For example, if this has the value 6 and the
        /// [kind] is [PointerDeviceKind.invertedStylus], then this indicates an
        /// upside-down stylus with both its primary and secondary buttons pressed.
        public readonly int Buttons;

        /// Set if an application from a different security domain is in any way
        /// obscuring this application's window. (Aspirational; not currently
        /// implemented.)
        public readonly bool Obscured;

        /// The pressure of the touch as a number ranging from 0.0, indicating a touch
        /// with no discernible pressure, to 1.0, indicating a touch with "normal"
        /// pressure, and possibly beyond, indicating a stronger touch. For devices
        /// that do not detect pressure (e.g. mice), returns 1.0.
        public readonly double Pressure;

        /// The minimum value that [pressure] can return for this pointer. For devices
        /// that do not detect pressure (e.g. mice), returns 1.0. This will always be
        /// a number less than or equal to 1.0.
        public readonly double PressureMin;

        /// The maximum value that [pressure] can return for this pointer. For devices
        /// that do not detect pressure (e.g. mice), returns 1.0. This will always be
        /// a greater than or equal to 1.0.
        public readonly double PressureMax;

        /// The distance of the detected object from the input surface (e.g. the
        /// distance of a stylus or finger from a touch screen), in arbitrary units on
        /// an arbitrary (not necessarily linear) scale. If the pointer is down, this
        /// is 0.0 by definition.
        public readonly double Distance;

        /// The maximum value that a distance can return for this pointer. If this
        /// input device cannot detect "hover touch" input events, then this will be
        /// 0.0.
        public readonly double DistanceMax;

        /// The area of the screen being pressed, scaled to a value between 0 and 1.
        /// The value of size can be used to determine fat touch events. This value
        /// is only set on Android, and is a device specific approximation within
        /// the range of detectable values. So, for example, the value of 0.1 could
        /// mean a touch with the tip of the finger, 0.2 a touch with full finger,
        /// and 0.3 the full palm.
        public readonly double Size;

        /// The radius of the contact ellipse along the major axis, in logical pixels.
        public readonly double RadiusMajor;

        /// The radius of the contact ellipse along the minor axis, in logical pixels.
        public readonly double RadiusMinor;

        /// The minimum value that could be reported for radiusMajor and radiusMinor
        /// for this pointer, in logical pixels.
        public readonly double RadiusMin;

        /// The minimum value that could be reported for radiusMajor and radiusMinor
        /// for this pointer, in logical pixels.
        public readonly double RadiusMax;

        /// For PointerDeviceKind.touch events:
        ///
        /// The angle of the contact ellipse, in radius in the range:
        ///
        ///    -pi/2 orientation pi/2
        ///
        /// giving the angle of the major axis of the ellipse with the y-axis
        /// (negative angles indicating an orientation along the top-left /
        /// bottom-right diagonal, positive angles indicating an orientation along the
        /// top-right / bottom-left diagonal, and zero indicating an orientation
        /// parallel with the y-axis).
        ///
        /// For PointerDeviceKind.stylus and PointerDeviceKind.invertedStylus events:
        ///
        /// The angle of the stylus, in radians in the range:
        ///
        ///    -pi orientation pi
        ///
        /// ...giving the angle of the axis of the stylus projected onto the input
        /// surface, relative to the positive y-axis of that surface (thus 0.0
        /// indicates the stylus, if projected onto that surface, would go from the
        /// contact point vertically up in the positive y-axis direction, pi would
        /// indicate that the stylus would go down in the negative y-axis direction;
        /// pi/4 would indicate that the stylus goes up and to the right, -pi/2 would
        /// indicate that the stylus goes to the left, etc).
        public readonly double Orientation;

        /// For PointerDeviceKind.stylus and PointerDeviceKind.invertedStylus events:
        ///
        /// The angle of the stylus, in radians in the range:
        ///
        ///    0  tilt pi/2
        ///
        /// ...giving the angle of the axis of the stylus, relative to the axis
        /// perpendicular to the input surface (thus 0.0 indicates the stylus is
        /// orthogonal to the plane of the input surface, while pi/2 indicates that
        /// the stylus is flat on that surface).
        public readonly double Tilt;

        /// Opaque platform-specific data associated with the event.
        public readonly int PlatformData;

        /// For events with signalKind of PointerSignalKind.scroll:
        ///
        /// The amount to scroll in the x direction, in physical pixels.
        public readonly double ScrollDeltaX;

        /// For events with signalKind of PointerSignalKind.scroll:
        ///
        /// The amount to scroll in the y direction, in physical pixels.
        public readonly double ScrollDeltaY;

        public override string ToString()
        {
            return $"PointerData(x: {this.PhysicalX}, y: {this.PhysicalY})";
        }

        /// Returns a complete textual description of the information in this object.
        public string ToStringFull()
        {
            return $"PointerData(" +
                $"timeStamp: {this.TimeStamp}, " +
                $"change: {this.Change}, " +
                $"kind: {this.Kind}, " +
                $"signalKind: {this.SignalKind}, " +
                $"device: {this.Device}, " +
                $"physicalX: {this.PhysicalX}, " +
                $"physicalY: {this.PhysicalY}, " +
                $"buttons: {this.Buttons}," +
                $" pressure: {this.Pressure}, " +
                $"pressureMin: {this.PressureMin}, " +
                $"pressureMax: {this.PressureMax}, " +
                $"distance: {this.Distance}, " +
                $"distanceMax: {this.DistanceMax}, " +
                $"size: {this.Size}, " +
                $"radiusMajor: {this.RadiusMajor}," +
                $"radiusMinor: {this.RadiusMinor}, " +
                $"radiusMin: {this.RadiusMin}, " +
                $"radiusMax: , {this.RadiusMax}, " +
                $"orientation: {this.Orientation}, " +
                $"tilt: {this.Tilt}, " +
                $"platformData: {this.PlatformData}, " +
                $"scrollDeltaX: {this.ScrollDeltaX}, " +
                $"scrollDeltaY: {this.ScrollDeltaY})";
        }
    }
}
