namespace FlutterSharp.UI.PresentationFramework
{
    /// <summary>
    /// Represents a control with a single piece of content of any type.
    /// </summary>
    /// <seealso cref="FlutterSharp.UI.PresentationFramework.FrameworkElement" />
    public class ContentControl : FrameworkElement
    {
        private FrameworkElement actualContentControl;
        private object content;

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>
        /// The content.
        /// </value>
        public object Content 
        { 
            get => this.content;
            set 
            {
                if (this.content == value)
                    return;

                this.content = value;
                this.ResolveContent();
            }
        }

        /// <summary>
        /// When overridden in a derived class, provides measurement logic for sizing this element properly, with consideration of the size of any child element content.
        /// </summary>
        /// <param name="availableSize">The available size that the parent element can allocate for the child.</param>
        /// <returns>
        /// The desired size of this element in layout.
        /// </returns>
        protected override Size MeasureCore(Size availableSize)
        {
            if (this.actualContentControl != null)
                this.actualContentControl.Measure(availableSize);

            return base.MeasureCore(availableSize);
        }

        /// <summary>
        /// When overridden in a derived class, positions child elements and determines a size for a FrameworkElement derived class.
        /// </summary>
        /// <param name="finalSize">The final area within the parent that this element should use to arrange itself and its children.</param>
        /// <returns>
        /// The actual size used.
        /// </returns>
        protected override sealed Size ArrangeOverride(Size finalSize)
        {
            this.actualContentControl.Arrange(Offset.Zero & finalSize);
            return finalSize;
        }

        /// <summary>
        /// Draws this component on the specified canvas.
        /// </summary>
        /// <param name="canvas">The canvas.</param>
        public override void Draw(UI.Canvas canvas)
        {
            base.Draw(canvas);

            if (this.actualContentControl != null)
                this.actualContentControl.Draw(canvas);
        }

        private void ResolveContent()
        {
            if (this.Content is FrameworkElement frameworkElement)
                this.actualContentControl = frameworkElement;
        }
    }
}
