namespace FlutterSharp.UI.PresentationFramework
{
    /// <summary>
    /// <see cref="FrameworkElement"/> is a base class for FlutterSharp core level implementations building on FlutterSharp elements and basic presentation characteristics.
    /// </summary>
    public class FrameworkElement : DependencyObject
    {
        private bool isLoaded;

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

        /// <summary>
        /// Gets or sets the margin.
        /// </summary>
        /// <value>
        /// The margin.
        /// </value>
        public Thickness Margin { get; set; }

        /// <summary>
        /// Gets the size that this element computed during the measure pass of the layout process.
        /// </summary>
        /// <value>
        /// The computed size, which becomes the desired size for the arrange pass.
        /// </value>
        public Size DesiredSize { get; private set; }

        /// <summary>
        /// Call when this instance is initialized
        /// </summary>
        protected virtual void Load()
        {
        }

        /// <summary>
        /// Updates the DesiredSize of a UIElement. Parent elements call this method from their own MeasureCore(Size) implementations to form a recursive layout update. Calling this method constitutes the first pass (the "Measure" pass) of a layout update.
        /// </summary>
        /// <param name="availableSize">The available space that a parent element can allocate a child element. A child element can request a larger space than what is available; the provided size might be accommodated if scrolling is possible in the content model for the current element.</param>
        public void Measure(Size availableSize)
        {
            if (!this.isLoaded)
            {
                this.isLoaded = true;
                this.Load();
            }

            var availableWidth = double.IsPositiveInfinity(availableSize.Width) ? double.PositiveInfinity : availableSize.Width - this.Margin.Left - this.Margin.Right;
            var availableHeight = double.IsPositiveInfinity(availableSize.Height) ? double.PositiveInfinity : availableSize.Height - this.Margin.Top - this.Margin.Bottom;

            this.DesiredSize = this.MeasureCore(new Size(availableWidth, availableHeight));
        }

        /// <summary>
        /// When overridden in a derived class, provides measurement logic for sizing this element properly, with consideration of the size of any child element content.
        /// </summary>
        /// <param name="availableSize">The available size that the parent element can allocate for the child.</param>
        /// <returns>The desired size of this element in layout.</returns>
        protected virtual Size MeasureCore(Size availableSize)
        {
            double width = availableSize.Width;
            double height = availableSize.Height;

            if (double.IsPositiveInfinity(availableSize.Width))
                width = this.Width;

            if (double.IsPositiveInfinity(availableSize.Height))
                height = this.Height;

            return new Size(width, height);
        }

        /// <summary>
        /// Positions child elements and determines a size for a UIElement. Parent elements call this method from their ArrangeCore(Rect) implementation to form a recursive layout update. This method constitutes the second pass of a layout update.
        /// </summary>
        /// <param name="finalRect">The final rect.</param>
        public void Arrange(Rect finalRect)
        {
            this.ArrangeCore(finalRect);
        }

        /// <summary>
        /// Defines the template for core-level arrange layout definition.
        /// </summary>
        /// <param name="finalRect">The final area within the parent that element should use to arrange itself and its child elements.</param>
        protected virtual void ArrangeCore(Rect finalRect)
        {
            this.X = finalRect.Left + this.Margin.Left;
            this.Y = finalRect.Top + this.Margin.Top;

            var arrangeSize = new Size(finalRect.Width - this.Margin.Left - this.Margin.Right, finalRect.Height - this.Margin.Top - this.Margin.Bottom);

            var elementSize = this.ArrangeOverride(arrangeSize);

            this.ActualWidth = elementSize.Width;
            this.ActualHeight = elementSize.Height;
        }

        /// <summary>
        /// When overridden in a derived class, positions child elements and determines a size for a FrameworkElement derived class.
        /// </summary>
        /// <param name="finalSize">The final area within the parent that this element should use to arrange itself and its children.</param>
        /// <returns>The actual size used.</returns>
        protected virtual Size ArrangeOverride(Size finalSize)
        {
            return finalSize;
        }

        /// <summary>
        /// Draws this component on the specified canvas.
        /// </summary>
        /// <param name="canvas">The canvas.</param>
        public virtual void Draw(UI.Canvas canvas)
        {
            canvas.DrawRect(new Rect(this.X, this.Y, this.X + this.ActualWidth, this.Y + this.ActualHeight), new Paint() { Color = this.Background });
        }
    }
}
