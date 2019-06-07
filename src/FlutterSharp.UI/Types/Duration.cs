namespace FlutterSharp.UI
{
    // TODO : implement => https://github.com/dart-lang/sdk/blob/master/sdk/lib/core/duration.dart
    public class Duration
    {
        internal int inMicroseconds;
        private int microseconds;

        public Duration(int microseconds)
        {
            this.microseconds = microseconds;
        }

        public static Duration operator -(Duration b, Duration c)
        {
            return b;
        }
    }
}
