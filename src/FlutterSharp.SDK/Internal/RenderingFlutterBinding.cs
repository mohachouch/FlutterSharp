namespace FlutterSharp.SDK.Internal
{
    public class RenderingFlutterBinding : RendererBinding
    {
        public RenderingFlutterBinding(RenderBox root = null)
        {
            RenderView.Child = root;
        }
    }
}
