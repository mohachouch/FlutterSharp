namespace FlutterSharp.UI.PresentationFramework.Media
{
    /// <summary>
    /// Implements a set of predefined SolidColorBrush objects.
    /// </summary>
    public static class Brushes
    {
        /// <summary>
        /// Gets the solid fill color that has a hexadecimal value of #00FFFFFF.
        /// </summary>
        /// <value>
        /// A solid fill color.
        /// </value>
        public static SolidColorBrush Transparent { get; } = new SolidColorBrush();

        /// <summary>
        /// Gets the solid fill color that has a hexadecimal value of #FFFF0000.
        /// </summary>
        /// <value>
        /// A solid fill color.
        /// </value>
        public static SolidColorBrush Red { get; } = new SolidColorBrush { Color = Colors.Red };

        /// <summary>
        /// Gets the solid fill color that has a hexadecimal value of #FF00FF00.
        /// </summary>
        /// <value>
        /// A solid fill color.
        /// </value>
        public static SolidColorBrush Green { get; } = new SolidColorBrush { Color = Colors.Green };

        /// <summary>
        /// Gets the solid fill color that has a hexadecimal value of #FF0000FF.
        /// </summary>
        /// <value>
        /// A solid fill color.
        /// </value>
        public static SolidColorBrush Blue { get; } = new SolidColorBrush { Color = Colors.Blue };
    }
}
