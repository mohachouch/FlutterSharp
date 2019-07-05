using System;
using Android.App;
using Android.Runtime;
using IO.Flutter.App;

namespace FlutterSharp.UI.Android
{
    [Application]
    public class FlutterSharpApplication : FlutterApplication
    {
        public FlutterSharpApplication()
        {
        }

        protected FlutterSharpApplication(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }
    }
}