using FlutterSharp.UI;

namespace FlutterSharp.Samples.Layers.Raw
{
    /// <summary>
    /// This example shows how to show the text 'Hello, world.' using using the raw interface to the engine.
    /// </summary>
    public class HelloWorldSample : IFlutterMain
    {
        private void OnBeginFrame(Duration duration)
        {
            double devicePixelRatio = Window.Instance.DevicePixelRatio;
            Size logicalSize = Window.Instance.PhysicalSize / devicePixelRatio;

            ParagraphBuilder paragraphBuilder = new ParagraphBuilder(new ParagraphStyle(textDirection: TextDirection.Ltr));
            paragraphBuilder.AddText("Hello, world.");

            Paragraph paragraph = paragraphBuilder.Build();
            paragraph.Layout(new ParagraphConstraints(width: logicalSize.Width));

            Rect physicalBounds = Offset.Zero & (logicalSize * devicePixelRatio);
            PictureRecorder recorder = new PictureRecorder();
            UI.Canvas canvas = new UI.Canvas(recorder, physicalBounds);
            canvas.Scale(devicePixelRatio, devicePixelRatio);
            canvas.DrawParagraph(paragraph, new Offset(
              (logicalSize.Width - paragraph.MaxIntrinsicWidth) / 2.0,
              (logicalSize.Height - paragraph.Height) / 2.0));

            Picture picture = recorder.EndRecording();

            SceneBuilder sceneBuilder = new SceneBuilder();
            // TODO(abarth): We should be able to add a picture without pushing a
            // container layer first.
            sceneBuilder.PushClipRect(physicalBounds);
            sceneBuilder.AddPicture(Offset.Zero, picture);
            sceneBuilder.Pop();

            Window.Instance.Render(sceneBuilder.Build());
        }

        // This function is the primary entry point to your application. The engine
        // calls main() as soon as it has loaded your code.
        public void Main()
        {
            // The engine calls onBeginFrame whenever it wants us to produce a frame.
            Window.Instance.OnBeginFrame += OnBeginFrame;
            // Here we kick off the whole process by asking the engine to schedule a new
            // frame. The engine will eventually call onBeginFrame when it is time for us
            // to actually produce the frame.
            Window.Instance.ScheduleFrame();
        }
    }
}
