using FlutterSharp.UI;

namespace FlutterSharp.SDK.Internal
{
    public class BindingBase
    {
        public BindingBase()
        {
            InitInstances();
        }

        public Window Window => Window.Instance;

        protected virtual void InitInstances()
        {
        }
    }
}
