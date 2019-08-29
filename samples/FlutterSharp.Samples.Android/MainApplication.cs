using System;
using Android.App;
using Android.Runtime;
using FlutterSharp.UI;
using FlutterSharp.UI.Android;

namespace FlutterSharp.Samples.Android
{
    [Preserve]
    [Application]
    public class MainApplication : FlutterSharpApplication
    {
        public MainApplication()
        {
        }

        protected MainApplication(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }
    }
}