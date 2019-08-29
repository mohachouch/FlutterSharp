using System.Collections.Generic;

namespace FlutterSharp.UI
{
    public class Float64List : List<double>
    {
        public Float64List(int capacity) : base(capacity)
        {
            for (int i = 0; i < capacity; i++)
                this.Add(0);
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