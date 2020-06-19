using System.Collections.ObjectModel;

namespace FlutterSharp.UI.PresentationFramework
{
    /// <summary>
    /// Provides a base class for all Panel elements. Use Panel elements to position and arrange child objects in flutter sharp applications.
    /// </summary>
    /// <seealso cref="FlutterSharp.UI.PresentationFramework.FrameworkElement" />
    public abstract class Panel : FrameworkElement
    {
        /// <summary>
        /// Gets a UIElementCollection of child elements of this Panel.
        /// </summary>
        /// <value>
        /// A UIElementCollection. The default is an empty UIElementCollection.
        /// </value>
        public ObservableCollection<FrameworkElement> Children { get; } = new ObservableCollection<FrameworkElement>();

        /// <summary>
        /// When overridden in a derived class, provides measurement logic for sizing this element properly, with consideration of the size of any child element content.
        /// </summary>
        /// <param name="availableSize">The available size that the parent element can allocate for the child.</param>
        /// <returns>
        /// The desired size of this element in layout.
        /// </returns>
        protected override sealed Size MeasureCore(Size availableSize)
        {
            return this.MeasureOverride(availableSize);
        }

        /// <summary>
        /// Measures the override.
        /// </summary>
        /// <param name="availableSize">Size of the available.</param>
        /// <returns></returns>
        protected virtual Size MeasureOverride(Size availableSize)
        {
            return Size.Zero;
        }

        /// <summary>
        /// Draws this component on the specified canvas.
        /// </summary>
        /// <param name="canvas">The canvas.</param>
        public override sealed void Draw(UI.Canvas canvas)
        {
            foreach (var child in this.Children)
            {
                child.Draw(canvas);
            }
        }
    }
}
