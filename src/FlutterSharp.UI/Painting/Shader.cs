using System;

namespace FlutterSharp.UI
{
    /// Base class for objects such as [Gradient] and [ImageShader] which
    /// correspond to shaders as used by [Paint.shader].
    public class Shader : NativeFieldWrapperClass2
    {
        /// This class is created by the engine, and should not be instantiated
        /// or extended directly.
        public Shader()
        {
        }

        public Shader(IntPtr intPtr)
        {
        }
    }
}