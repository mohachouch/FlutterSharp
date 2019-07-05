using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using FlutterSharp.UI.Android;

namespace FlutterSharp.Samples.Android
{
    [Activity(
        Label = "@string/app_name", 
        MainLauncher = true,
        LaunchMode = LaunchMode.SingleTop,
        Theme = "@android:style/Theme.Light.NoTitleBar",
        HardwareAccelerated = true,
        WindowSoftInputMode = SoftInput.AdjustResize,
        ConfigurationChanges = ConfigChanges.Orientation |
                                ConfigChanges.KeyboardHidden |
                                ConfigChanges.Keyboard |
                                ConfigChanges.ScreenSize |
                                ConfigChanges.Locale |
                                ConfigChanges.LayoutDirection |
                                ConfigChanges.FontScale |
                                ConfigChanges.ScreenLayout |
                                ConfigChanges.Density |
                                ConfigChanges.UiMode)]
    [MetaData("io.flutter.app.android.SplashScreenUntilFirstFrame", Value = "true")]
    public class MainActivity : FlutterSharpActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }
	}
}

