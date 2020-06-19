namespace FlutterSharp.UI.PresentationFramework
{
    /// <summary>
    /// Base class to declare dependency property
    /// </summary>
    public class DependencyProperty
    {
        /// <summary>
        /// Gets the default value.
        /// </summary>
        /// <value>
        /// The default value.
        /// </value>
        public object DefaultValue { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DependencyProperty"/> class.
        /// </summary>
        /// <param name="defaultValue">Default value for this property.</param>
        public DependencyProperty(object defaultValue = null)
        {
            this.DefaultValue = defaultValue;
        }
    }
}
