namespace FlutterSharp.UI
{
    public static class Lerp
    {
        public static double LerpDouble(double a, double b, double t)
        {
            return a + (b - a) * t;
        }
    }
}
