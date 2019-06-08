using System.Collections.Generic;

namespace FlutterSharp.UI
{
    public class Float64List : List<double>
    {
        public Float64List(int capacity) : base(capacity)
        {
        }

        public Float64List(IEnumerable<double> collection) : base(collection)
        {
        }

        public static Float64List FromList(params double[] values)
        {
            return new Float64List(values);
        }
    }
}