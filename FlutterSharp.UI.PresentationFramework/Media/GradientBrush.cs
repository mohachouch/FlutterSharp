using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FlutterSharp.UI.PresentationFramework.Media
{
    /// <summary>
    /// An abstract class that describes a gradient, composed of gradient stops. Classes that inherit from <see cref="GradientBrush"/> describe different ways of interpreting gradient stops.
    /// </summary>
    /// <seealso cref="FlutterSharp.UI.PresentationFramework.Media.Brush" />
    public abstract class GradientBrush : Brush
    {
        /// <summary>
        ///Gets or sets the brush's gradient stops.
        /// </summary>
        /// <value>
        /// A collection of the <seealso cref="GradientStop"/> objects associated with the brush, each of which specifies a color and an offset along the brush's gradient axis. The default is an empty gradient stop collection.
        /// </value>
        public ObservableCollection<GradientStop> GradientStops { get; set; } = new ObservableCollection<GradientStop>();

        /// <summary>
        /// Transforms the <see cref="Gradient"/> collection into separate lists of colors and offsets for flutter's <see cref="Gradient"/> class.
        /// </summary>
        /// <returns>List of colors and list of offsets</returns>
        protected (List<Color> colors, List<double> offsets) GradientStopsToFluttersLists()
        {
            var colors = new List<Color>(this.GradientStops.Count);
            var offsets = new List<double>(this.GradientStops.Count);

            foreach (var gradientStop in this.GradientStops)
            {
                colors.Add(this.ApplyOpacity(gradientStop.Color));
                offsets.Add(gradientStop.Offset);
            }

            return (colors, offsets);
        }
    }
}
