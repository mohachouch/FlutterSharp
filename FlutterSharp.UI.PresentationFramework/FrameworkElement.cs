namespace FlutterSharp.UI.PresentationFramework
{
    /// <summary>
    /// <see cref="FrameworkElement"/> is a base class for FlutterSharp core level implementations building on FlutterSharp elements and basic presentation characteristics.
    /// </summary>
    public class FrameworkElement
    {
        /// <summary>
        /// Get the X top left coordinate on window
        /// </summary>
        public double X { get; internal set; }

        /// <summary>
        /// Get the Y top left coordinate on window
        /// </summary>
        public double Y { get; internal set; }

        /// <summary>
        /// Gets the rendered width of this element.
        /// </summary>
        public double ActualWidth { get; internal set; }

        /// <summary>
        /// Gets the rendered height of this element.
        /// </summary>
        public double ActualHeight { get; internal set; }

        /// <summary>
        /// Gets or sets the width of the element.
        /// </summary>
        public double Width { get; set; } = double.PositiveInfinity;

        /// <summary>
        /// Gets or sets the height of the element.
        /// </summary>
        public double Height { get; set; } = double.PositiveInfinity;

        /// <summary>
        /// Gets or sets a brush that describes the background of a control.
        /// </summary>
        public Color Background { get; set; }

        internal void Draw(SceneBuilder scene, Rect paintBounds)
        {
            var recorder = new PictureRecorder();

            UI.Canvas canvas = new UI.Canvas(recorder);
            canvas.DrawPaint(new Paint() { Color = this.Background });

            scene.PushClipRect(paintBounds);
            scene.AddPicture(Offset.Zero, recorder.EndRecording());
        }
    }
}
