using FlutterSharp.UI;

namespace FlutterSharp.Samples.Layers.Raw
{
    /// <summary>
    /// This example shows how to show the text 'Hello, world.' using using the raw interface to the engine.
    /// </summary>
    public class HelloWorld : IFlutterMain
    {
        private void OnDrawFrame()
        {

        }
        
        public void Main()
        {
            Window.Instance.OnDrawFrame += OnDrawFrame;
        }
    }
}
