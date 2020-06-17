using FlutterSharp.UI;
using FlutterSharp.UI.PresentationFramework;

namespace FlutterSharp.Samples
{
    public class App : Application
    {
        public App()
        {
            this.MainWindow = new UI.PresentationFramework.Window { Background = Color.FromARGB(255, 255, 0, 0) };
        }
    }
}
