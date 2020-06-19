namespace FlutterSharp.UI.PresentationFramework
{
    /// <summary>
    /// Defines an area within which you can explicitly position child elements by using coordinates that are relative to the Canvas area.
    /// </summary>
    /// <seealso cref="FlutterSharp.UI.PresentationFramework.Panel" />
    public class Canvas : Panel
    {
        /// <summary>
        /// Gets or sets a value that represents the distance between the left of an element and the left of its parent Canvas.
        /// </summary>
        public static readonly DependencyProperty LeftProperty = new DependencyProperty(0d);
        /// <summary>
        /// Gets or sets a value that represents the distance between the top of an element and the top of its parent Canvas.
        /// </summary>
        public static readonly DependencyProperty TopProperty = new DependencyProperty(0d);
        /// <summary>
        /// Gets or sets a value that represents the distance between the right of an element and the right of its parent Canvas.
        /// </summary>
        public static readonly DependencyProperty RightProperty = new DependencyProperty(double.NaN);
        /// <summary>
        /// Gets or sets a value that represents the distance between the bottom of an element and the bottom of its parent Canvas.
        /// </summary>
        public static readonly DependencyProperty BottomProperty = new DependencyProperty(double.NaN);

        /// <summary>
        /// Returns the value of the Left attached property for a given dependency object.
        /// </summary>
        /// <param name="element">The element from which the property value is read.</param>
        /// <returns>The Left coordinate of the specified element.</returns>
        public static double GetLeft(DependencyObject element) => (double)element.GetValue(LeftProperty);

        /// <summary>
        /// Sets the value of the Left attached property for a given dependency object.
        /// </summary>
        /// <param name="element">The element to which the property value is written.</param>
        /// <param name="value">Sets the Left coordinate of the specified element.</param>
        public static void SetLeft(DependencyObject element, double value) => element.SetValue(LeftProperty, value);

        /// <summary>
        /// Returns the value of the Top attached property for a given dependency object.
        /// </summary>
        /// <param name="element">The element from which the property value is read.</param>
        /// <returns>The Top coordinate of the specified element.</returns>
        public static double GetTop(FrameworkElement element) => (double)element.GetValue(TopProperty);

        /// <summary>
        /// Sets the value of the Top attached property for a given dependency object.
        /// </summary>
        /// <param name="element">The element to which the property value is written.</param>
        /// <param name="value">Sets the Top coordinate of the specified element.</param>
        public static void SetTop(FrameworkElement element, double value) => element.SetValue(TopProperty, value);

        /// <summary>
        /// Returns the value of the Right attached property for a given dependency object.
        /// </summary>
        /// <param name="element">The element from which the property value is read.</param>
        /// <returns>The Right coordinate of the specified element.</returns>
        public static double GetRight(FrameworkElement element) => (double)element.GetValue(RightProperty);

        /// <summary>
        /// Sets the value of the Right attached property for a given dependency object.
        /// </summary>
        /// <param name="element">The element to which the property value is written.</param>
        /// <param name="value">Sets the Right coordinate of the specified element.</param>
        public static void SetRight(FrameworkElement element, double value) => element.SetValue(RightProperty, value);

        /// <summary>
        /// Returns the value of the Bottom attached property for a given dependency object.
        /// </summary>
        /// <param name="element">The element from which the property value is read.</param>
        /// <returns>The Bottom coordinate of the specified element.</returns>
        public static double GetBottom(FrameworkElement element) => (double)element.GetValue(RightProperty);

        /// <summary>
        /// Sets the value of the Bottom attached property for a given dependency object.
        /// </summary>
        /// <param name="element">The element to which the property value is written.</param>
        /// <param name="value">Sets the Bottom coordinate of the specified element.</param>
        public static void SetBottom(FrameworkElement element, double value) => element.SetValue(RightProperty, value);

        /// <summary>
        /// Measures the override.
        /// </summary>
        /// <param name="availableSize">Size of the available.</param>
        /// <returns></returns>
        protected override Size MeasureOverride(Size availableSize)
        {
            foreach (var child in this.Children)
            {
                child.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            }

            return base.MeasureOverride(availableSize);
        }

        /// <summary>
        /// When overridden in a derived class, positions child elements and determines a size for a FrameworkElement derived class.
        /// </summary>
        /// <param name="finalSize">The final area within the parent that this element should use to arrange itself and its children.</param>
        /// <returns>
        /// The actual size used.
        /// </returns>
        protected override Size ArrangeOverride(Size finalSize)
        {
            foreach (var child in this.Children)
            {
                var x = GetLeft(child);
                var y = GetTop(child);

                child.Arrange(new Rect(x, y, x + child.DesiredSize.Width, y + child.DesiredSize.Height));
            }

            return finalSize;
        }
    }
}
