namespace FlutterSharp.UI.PresentationFramework.Media
{
    /// <summary>
    /// Implements a set of predefined colors.
    /// </summary>
    public static class Colors
    {
        /// <summary>
        /// Gets the system-defined color that has an ARGB value of #00FFFFFF.
        /// </summary>
        /// <value>
        /// Represents colors in terms of alpha, red, green, and blue channels.
        /// </value>
        public static Color Transparent { get; } = new Color(0x00FFFFFF);

        /// <summary>
        /// Gets the system-defined color that has an ARGB value of #FFFF0000.
        /// </summary>
        /// <value>
        /// Represents colors in terms of alpha, red, green, and blue channels.
        /// </value>
        public static Color Red { get; } = new Color(0xFFFF0000);

        /// <summary>
        /// Gets the system-defined color that has an ARGB value of #FF00FF00.
        /// </summary>
        /// <value>
        /// Represents colors in terms of alpha, red, green, and blue channels.
        /// </value>
        public static Color Green { get; } = new Color(0xFF00FF00);

        /// <summary>
        /// Gets the system-defined color that has an ARGB value of #FF0000FF.
        /// </summary>
        /// <value>
        /// Represents colors in terms of alpha, red, green, and blue channels.
        /// </value>
        public static Color Blue { get; } = new Color(0xFF0000FF);

    }
}
