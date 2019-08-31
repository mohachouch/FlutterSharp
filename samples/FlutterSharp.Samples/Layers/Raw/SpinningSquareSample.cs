using FlutterSharp.UI;
using System;

namespace FlutterSharp.Samples.Layers.Raw
{
    // This example shows how to perform a simple animation using the raw interface
    // to the engine.
    public class SpinningSquareSample : IFlutterMain
    {
        private void OnBeginFrame(Duration timeStamp)
        {
            // The timeStamp argument to beginFrame indicates the timing information we
            // should use to clock our animations. It's important to use timeStamp rather
            // than reading the system time because we want all the parts of the system to
            // coordinate the timings of their animations. If each component read the
            // system clock independently, the animations that we processed later would be
            // slightly ahead of the animations we processed earlier.

            // PAINT

            Rect paintBounds = Offset.Zero & (Window.Instance.PhysicalSize / Window.Instance.DevicePixelRatio);
            PictureRecorder recorder = new PictureRecorder();
            Canvas canvas = new Canvas(recorder, paintBounds);
            canvas.Translate(paintBounds.Width / 2.0, paintBounds.Height / 2.0);

            // Here we determine the rotation according to the timeStamp given to us by
            // the engine.
            double t = timeStamp.InMicroseconds / Duration.MicrosecondsPerMillisecond / 1800.0;
            canvas.Rotate(Math.PI * (t % 1.0));

            canvas.DrawRect(Rect.FromLTRB(-100.0, -100.0, 100.0, 100.0),
                  new Paint() { Color = Color.FromARGB(255, 0, 255, 0) });
            Picture picture = recorder.EndRecording();

            // COMPOSITE

            double devicePixelRatio = Window.Instance.DevicePixelRatio;
            Float64List deviceTransform = new Float64List(16);
            deviceTransform[0] = devicePixelRatio;
            deviceTransform[5] = devicePixelRatio;
            deviceTransform[10] = 1.0;
            deviceTransform[15] = 1.0;
            SceneBuilder sceneBuilder = new SceneBuilder();
            sceneBuilder.PushTransform(deviceTransform);
            sceneBuilder.AddPicture(Offset.Zero, picture);
            sceneBuilder.AddPerformanceOverlay(0x0F, Rect.FromLTWH(30, 30, paintBounds.Width - 60, 300));
            sceneBuilder.Pop();
            Window.Instance.Render(sceneBuilder.Build());

            // After rendering the current frame of the animation, we ask the engine to
            // schedule another frame. The engine will call beginFrame again when its time
            // to produce the next frame.
            Window.Instance.ScheduleFrame();
        }

        public void Main()
        {
            Window.Instance.OnBeginFrame = OnBeginFrame;
            Window.Instance.ScheduleFrame();
        }
    }
}
