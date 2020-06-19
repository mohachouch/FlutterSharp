using System;

namespace FlutterSharp.UI.PresentationFramework.Media
{
    /// <summary>
    /// Defines objects used to paint graphical objects. Classes that derive from <see cref="Brush"/> describe how the area is painted.
    /// </summary>
    public abstract class Brush
    {
        /// <summary>
        /// Gets or sets the degree of opacity of a Brush.
        /// </summary>
        /// <value>
        /// The value of the Opacity property is expressed as a value between 0.0 and 1.0. The default value is 1.0.
        /// </value>
        public double Opacity { get; set; } = 1.0;

        /// <summary>
        /// Converts the <see cref="Brush"/> to flutter <see cref="Paint"/> object.
        /// </summary>
        /// <returns>The <see cref="Paint"/> used to draw the <see cref="Brush"/></returns>
        public abstract Paint ToPaint();

        /// <summary>
        /// Applies the <see cref="Opacity"/> to the color <paramref name="from"/>.
        /// </summary>
        /// <param name="from">The color to apply <see cref="Opacity"/>.</param>
        /// <returns>The color with opacity</returns>
        protected Color ApplyOpacity(Color from)
        {
            if (this.Opacity == 1.0)
                return from;

            return Color.FromARGB((int)Math.Round(from.Alpha * this.Opacity), from.Red, from.Green, from.Blue);
        }
    }
}
