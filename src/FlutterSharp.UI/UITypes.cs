using System.Linq;

namespace FlutterSharp.UI
{
    public static class UITypes
    {
        public static int HashValues(params object[] values)
        {
            return values.Sum(x => x.GetHashCode());
        }

        public static bool Identical(object first, object second) => first != null && first.Equals(second);

        public static bool IsFinite(this double value) => !double.IsInfinity(value);

        public static string ToStringAsFixed(this double value, int points)
        {
            return value.ToString($"N{points}");
        }
    }
}
