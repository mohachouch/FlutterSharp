using FlutterSharp.UI.Types;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FlutterSharp.UI
{
    public static class UITypes
    {
        // If we actually run on big endian machines, we'll need to do something smarter
        // here. We don't use [Endian.Host] because it's not a compile-time
        // constant and can't propagate into the set/get calls.
        public static Endian _kFakeHostEndian = Endian.Little;

        public static int HashValues(params object[] values)
        {
            return values.Sum(x => x.GetHashCode());
        }

        public static int HashList(List<double> values)
        {
            return values.Sum(x => x.GetHashCode());
        }

        public static int HashList(List<int> values)
        {
            return values.Sum(x => x.GetHashCode());
        }

        public static int HashList<T>(List<T> values)
        {
            return values.Sum(x => x.GetHashCode());
        }

        public static bool Identical(object first, object second) => first != null && first.Equals(second);

        public static bool IsFinite(this double value) => !double.IsInfinity(value);

        public static bool IsNaN(this double value) => double.IsNaN(value);

        public static string ToStringAsFixed(this double value, int points)
        {
            return value.ToString($"N{points}");
        }

        public static double Round(this double value) => Math.Round(value);

        public static int ToInt(this double value) => (int)value;

        public static string ToRadixString(this int value, int places) => value.ToString(); // TODO: implement this

        public static bool IsEmpty(this string value) => string.IsNullOrWhiteSpace(value);

        public static int Clamp(this int value, int lower, int upper)
        {
            if (value > upper)
                return upper;

            if (value < lower)
                return lower;

            return value;
        }

        public static List<T> Sublist<T>(this List<T> list, int start, int? end = null)
        {
            if (end == null)
                end = list.Count - 1;

            return list.GetRange(start, end.Value - start);
        }

        public static double Clamp(this double value, int lower, int upper)
        {
            if (value > upper)
                return upper;

            if (value < lower)
                return lower;

            return value;
        }
        
        /// Determines if lists [a] and [b] are deep equivalent.
        ///
        /// Returns true if the lists are both null, or if they are both non-null, have
        /// the same length, and contain the same elements in the same order. Returns
        /// false otherwise.
        public static bool ListEquals<T>(List<T> a, List<T> b)
        {
            if (a == null)
                return b == null;
            if (b == null || a.Count != b.Count)
                return false;
            for (int index = 0; index < a.Count; index += 1)
            {
                if (!a[index].Equals(b[index]))
                    return false;
            }
            return true;
        }
    }
}