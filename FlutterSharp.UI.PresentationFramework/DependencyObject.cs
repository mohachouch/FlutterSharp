using System.Collections.Generic;

namespace FlutterSharp.UI.PresentationFramework
{
    /// <summary>
    /// Represents an object that participates in the dependency property system.
    /// </summary>
    public class DependencyObject
    {
        private readonly Dictionary<DependencyProperty, object> dependencyValues = new Dictionary<DependencyProperty, object>();

        /// <summary>
        ///Returns the current effective value of a dependency property on this instance of a <see cref="DependencyObject"/>.
        /// </summary>
        /// <param name="property">The dependency property.</param>
        /// <returns>The value.</returns>
        public object GetValue(DependencyProperty property)
        {
            if (dependencyValues.TryGetValue(property, out object value))
                return value;

            return property.DefaultValue;
        }

        /// <summary>
        /// Sets the local value of a dependency property.
        /// </summary>
        /// <param name="property">The dependency property.</param>
        /// <param name="value">The value.</param>
        public void SetValue(DependencyProperty property, object value)
        {
            dependencyValues[property] = value;
        }
    }
}
