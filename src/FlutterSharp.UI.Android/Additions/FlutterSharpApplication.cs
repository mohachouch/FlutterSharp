using System;
using Android.App;
using Android.Runtime;
using IO.Flutter.App;

namespace FlutterSharp.UI.Android
{
    [Preserve(AllMembers = true)]
    public class FlutterSharpApplication : FlutterApplication
    {
        public FlutterSharpApplication()
        {


        }

        public FlutterSharpApplication(IntPtr javaReference, JniHandleOwnership transfer)
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