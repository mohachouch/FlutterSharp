using System;

namespace FlutterSharp.UI
{
    public class Zone
    {
        public static Zone Current = new Zone();

        internal void RunGuarded(Action callback)
        {
            callback();
        }

        internal void RunUnaryGuarded<A>(Action<A> callback, A arg)
        {
            throw new NotImplementedException();
        }

        internal void RunBinaryGuarded<A1, A2>(Action<A1, A2> callback, A1 arg1, A2 arg2)
        {
            throw new NotImplementedException();
        }
    }
}
