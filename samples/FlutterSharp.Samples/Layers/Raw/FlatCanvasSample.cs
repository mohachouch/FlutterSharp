using FlutterSharp.UI;

namespace FlutterSharp.Samples.Layers.Raw
{
    public class FlatCanvasSample : IFlutterMain
    {
        private Picture Paint(Rect paintBounds)
        {
            // First we create a PictureRecorder to record the commands we're going to
            // feed in the canvas. The PictureRecorder will eventually produce a Picture,
            // which is an immutable record of those commands.
            PictureRecorder recorder = new PictureRecorder();

            // Next, we create a canvas from the recorder. The canvas is an interface
            // which can receive drawing commands. The canvas interface is modeled after
            // the SkCanvas interface from Skia. The paintBounds establishes a "cull rect"
            // for the canvas, which lets the implementation discard any commands that
            // are entirely outside this rectangle.
            UI.Canvas canvas = new UI.Canvas(recorder, paintBounds);

            canvas.DrawPaint(new Paint() { Color = new Color(0xFF0000FF) });

            // Saves a copy of current transform onto the save stack
            canvas.Save();

            return recorder.EndRecording();
        }

        private void OnBeginFrame(Duration duration)
        {
            var paintBounds = Offset.Zero & Window.Instance.PhysicalSize;
            Picture picture = Paint(paintBounds);

            SceneBuilder sceneBuilder = new SceneBuilder();
            sceneBuilder.PushClipRect(paintBounds);
            sceneBuilder.AddPicture(Offset.Zero, picture);
            sceneBuilder.Pop();

            Window.Instance.Render(sceneBuilder.Build());
        }

        public void Main()
        {
            Window.Instance.OnBeginFrame += OnBeginFrame;
            Window.Instance.ScheduleFrame();
        }
    }
}
