namespace FlutterSharp.UI
{
    // TODO : implement => https://github.com/dart-lang/sdk/blob/master/sdk/lib/core/duration.dart
    public class Duration
    {
        internal int inMicroseconds;
        private int microseconds;
        private int milliseconds;

        public Duration(int microseconds = 0, int milliseconds = 0)
        {
            this.microseconds = microseconds;
            this.milliseconds = milliseconds;
        }

        public static int MicrosecondsPerMillisecond = 1000;
        public int InMicroseconds => microseconds;

        public static Duration operator -(Duration b, Duration c)
        {
            return b;
        }
    }
}
