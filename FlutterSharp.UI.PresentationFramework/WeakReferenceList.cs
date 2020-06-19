using System;
using System.Collections;
using System.Collections.Generic;

namespace FlutterSharp.UI.PresentationFramework
{
    /// <summary>
    /// Manage a list of weak references.
    /// </summary>
    /// <typeparam name="T">Type of collection element</typeparam>
    public class WeakReferenceList<T> : IEnumerable<T> where T : class
    {
        private readonly List<WeakReference<T>> sourceList = new List<WeakReference<T>>();

        /// <summary>
        /// Add a new weak reference on <paramref name="instance"/>
        /// </summary>
        /// <param name="instance">Instance to add</param>
        public void Add(T instance)
        {
            this.sourceList.Add(new WeakReference<T>(instance));
        }

        /// <summary>
        /// Clean all non alive weak reference
        /// </summary>
        public void Clean()
        {
            for (int i = this.sourceList.Count - 1; i >= 0; i--)
            {
                var weakReference = this.sourceList[i];
                if (!weakReference.TryGetTarget(out _))
                    this.sourceList.RemoveAt(i);
            } 
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.</summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < sourceList.Count; i++)
            {
                T target;
                if (!this.sourceList[i].TryGetTarget(out target))
                    this.sourceList.RemoveAt(i);

                yield return target;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}
