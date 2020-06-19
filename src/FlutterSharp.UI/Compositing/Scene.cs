using System;
using System.Threading.Tasks;

namespace FlutterSharp.UI
{
    /// An opaque object representing a composited scene.
    ///
    /// To create a Scene object, use a [SceneBuilder].
    ///
    /// Scene objects can be displayed on the screen using the
    /// [Window.render] method.
    public class Scene : NativeFieldWrapperClass2
    {
        public Scene(IntPtr handle)
            : base(handle)
        {
            if (this.Handle == IntPtr.Zero)
            {
                throw new InvalidOperationException("Unable to create a new Scene instance.");
            }
        }

        // Creates a raster image representation of the current state of the scene.
        /// This is a slow operation that is performed on a background thread.
        public Future<Image> ToImage(int width, int height)
        {
            if (width <= 0 || height <= 0)
                throw new Exception("Invalid image dimensions.");
            return _futurize(
                (_Callback<Image> callback) => this.ToImage(width, height, callback)
            );
        }


        private string ToImage(int width, int height, _Callback<Image> callback)
        {
            // TODO : native 'Scene_toImage';
            return null;
        }

        /// Releases the resources used by this scene.
        ///
        /// After calling this function, the scene is cannot be used further.
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            // TODO : native 'Scene_dispose';
        }

        // A NETTOYER
        public static Future<T> _futurize<T>(Action<_Callback<T>> callback)
        {
            var result = default(T);

            var resolve = new _Callback<T>((t) => { result = t; });

            return new Future<T>(() => { return result; });
        }
    }

    // A NETTOYER
    public delegate void _Callback<T>(T result);

    public delegate void _Callback();

    public class Future<T> : Task<T>
    {
        public Future(Func<T> function) : base(function)
        {
        }
    }


    public class Future : Task
    {
        public Future(Action action) : base(action)
        {
        }
    }
}
