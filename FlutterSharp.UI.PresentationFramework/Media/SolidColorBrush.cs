namespace FlutterSharp.UI.PresentationFramework.Media
{
    /// <summary>
    /// Paints an area with a solid color.
    /// </summary>
    /// <seealso cref="FlutterSharp.UI.PresentationFramework.Media.Brush" />
    public class SolidColorBrush : Brush
    {
        /// <summary>
        /// Gets or sets the color of this SolidColorBrush.
        /// </summary>
        /// <value>
        /// The brush's color. The default value is Transparent.
        /// </value>
        public Color Color { get; set; } = Colors.Transparent;

        /// <summary>
        /// Converts the <see cref="Brush"/> to flutter <see cref="Paint"/> object.
        /// </summary>
        /// <returns>The <see cref="Paint"/> used to draw the <see cref="Brush"/></returns>
        public override Paint ToPaint()
        {
            return new Paint { Color = this.ApplyOpacity(this.Color) };
        }
    }
}
