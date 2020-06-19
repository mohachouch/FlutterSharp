namespace FlutterSharp.UI.PresentationFramework.Media
{
    /// <summary>
    /// Describes the location and color of a transition point in a gradient.
    /// </summary>
    public class GradientStop
    {
        /// <summary>
        /// Gets or sets the color of the gradient stop.
        /// </summary>
        /// <value>
        /// The color of the gradient stop. The default value is <seealso cref="Colors.Transparent"/>.
        /// </value>
        public Color Color { get; set; } = Colors.Transparent;

        /// <summary>
        /// Gets the location of the gradient stop within the gradient vector.
        /// </summary>
        /// <value>
        /// The relative location of this gradient stop along the gradient vector. The default value is 0.0.
        /// </value>
        public double Offset { get; set; }
    }
}
