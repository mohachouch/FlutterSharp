using FlutterSharp.UI.PresentationFramework;
using FlutterSharp.UI.PresentationFramework.Media;

namespace FlutterSharp.Samples
{
    public class App : Application
    {
        public App()
        {
            this.MainWindow = new Window { Background = Brushes.Red };
            var canvas = new Canvas { Background = Brushes.Green, Margin = new Thickness(20) };
            this.MainWindow.Content = canvas;

            var firstElement = new FrameworkElement { Width = 100, Height = 50, Background = Brushes.Blue };
            canvas.Children.Add(firstElement);

            var linearGradientBrush = new LinearGradientBrush();
            linearGradientBrush.GradientStops.Add(new GradientStop { Color = Colors.Red });
            linearGradientBrush.GradientStops.Add(new GradientStop { Color = Colors.Green, Offset = 0.5 });
            linearGradientBrush.GradientStops.Add(new GradientStop { Color = Colors.Blue, Offset = 1 });
            var secondElement = new FrameworkElement { Width = 200, Height = 300, Background = linearGradientBrush };
            canvas.Children.Add(secondElement);

            Canvas.SetLeft(firstElement, 10);
            Canvas.SetTop(firstElement, 20);

            Canvas.SetLeft(secondElement, 100);
            Canvas.SetTop(secondElement, 50);
        }
    }
}
