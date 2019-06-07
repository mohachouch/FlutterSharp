using System;

namespace FlutterSharp.UI
{
    public class StateError : Exception
    {
        public StateError(string message) : base(message)
        {
        }
    }
}
