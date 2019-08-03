using System;

namespace FlutterSharp.UI
{
    public class NativeFieldWrapperClass2
    {
        private readonly bool zero;

        internal NativeFieldWrapperClass2()
        {
            zero = true;
        }

        internal NativeFieldWrapperClass2(IntPtr handle)
        {
            Handle = handle;
            zero = true;
        }

        internal NativeFieldWrapperClass2(IntPtr handle , bool zero)
        {
            Handle = handle;
            this.zero = zero;
        }

        ~NativeFieldWrapperClass2()
        {
            Dispose(false);
        }

        public virtual IntPtr Handle { get; protected set; }

        protected virtual void Dispose(bool disposing)
        {
            if (zero)
                Handle = IntPtr.Zero;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
