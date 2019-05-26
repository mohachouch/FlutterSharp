using Android.App;
using Android.OS;

namespace FlutterSharp.Samples.Android
{
    [Activity(Label = "@string/app_name", Theme = "@style/LaunchTheme", MainLauncher = true)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
        }
	}
}

