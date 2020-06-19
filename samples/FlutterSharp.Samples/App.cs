using FlutterSharp.UI.PresentationFramework;

namespace FlutterSharp.Samples
{
    public class App : Application
    {
        public App()
        {
            this.MainWindow = new Window { Background = UI.Color.FromARGB(255, 255, 0, 0) };
            var canvas = new Canvas { Background = UI.Color.FromARGB(255, 0, 255, 0), Margin = new Thickness(20) };
            this.MainWindow.Content = canvas;

            var firstElement = new FrameworkElement { Width = 100, Height = 50, Background = UI.Color.FromARGB(0xFF, 0xFF, 0xAA, 0x0) };
            canvas.Children.Add(firstElement);

            var secondElement = new FrameworkElement { Width = 50, Height = 100, Background = UI.Color.FromARGB(0xFF, 0xAA, 0xAA, 0x33) };
            canvas.Children.Add(secondElement);

            Canvas.SetLeft(firstElement, 10);
            Canvas.SetTop(firstElement, 20);

            Canvas.SetLeft(secondElement, 100);
            Canvas.SetTop(secondElement, 50);
        }
    }
}
