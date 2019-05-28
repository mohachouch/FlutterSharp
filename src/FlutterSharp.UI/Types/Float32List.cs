using System.Collections.Generic;

namespace FlutterSharp.UI
{
    public class Float32List : List<double>
    {
        public Float32List(int capacity)
            : base(capacity)
        {

        }

        public Float32List(IEnumerable<double> collection)
            : base(collection)
        {
        }

        public static Float32List FromList(params double[] values)
        {
            return new Float32List(values);
        }
    }
}