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

        public override void OnCreate()
        {
            // Call register hooks before base OnCreate
            Hooks.Register();

            base.OnCreate();
        }
    }
}