namespace FlutterSharp.UI
{
    // TODO : implement => https://github.com/dart-lang/sdk/blob/master/sdk/lib/core/duration.dart
    public class Duration
    {
        internal int inMicroseconds;
        private long microseconds;
        private int milliseconds;

        public Duration(long microseconds = 0, int milliseconds = 0)
        {
            this.microseconds = microseconds;
            this.milliseconds = milliseconds;
        }

        public static int MicrosecondsPerMillisecond = 1000;
        public long InMicroseconds => microseconds;

        public static Duration operator -(Duration b, Duration c)
        {
            return b;
        }
    }
}
