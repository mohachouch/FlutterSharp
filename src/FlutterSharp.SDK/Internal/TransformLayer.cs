using FlutterSharp.UI;

namespace FlutterSharp.SDK.Internal
{
    internal class TransformLayer : OffsetLayer
    {
        private object transform;

        public TransformLayer(object transform)
        {
            this.transform = transform;
        }

        public override void AddToScene(SceneBuilder builder, Offset layerOffset)
        {
            base.AddToScene(builder, layerOffset);
        }
    }
}