using FlutterSharp.UI;

namespace FlutterSharp.Samples.Layers.Raw
{
    // This example shows how to put some pixels on the screen using the raw
    // interface to the engine.
    public class TouchInputSample : IFlutterMain
    {
        Color color;

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

            // The commands draw a circle in the center of the screen.
            Size size = paintBounds.Size;
            canvas.DrawCircle(
              size.Center(Offset.Zero),
              size.ShortestSide * 0.45,
              new Paint() { Color = color });

            // When we're done issuing painting commands, we end the recording an receive
            // a Picture, which is an immutable record of the commands we've issued. You
            // can draw a Picture into another canvas or include it as part of a
            // composited scene.
            return recorder.EndRecording();
        }

        private Scene Composite(Picture picture, Rect paintBounds)
        {
            // The device pixel ratio gives an approximate ratio of the size of pixels on
            // the device's screen to "normal" sized pixels. We commonly work in logical
            // pixels, which are then scalled by the device pixel ratio before being drawn
            // on the screen.
            double devicePixelRatio = Window.Instance.DevicePixelRatio;

            // This transform scales the x and y coordinates by the devicePixelRatio.
            Float64List deviceTransform = new Float64List(16);
            deviceTransform[0] = devicePixelRatio;
            deviceTransform[5] = devicePixelRatio;
            deviceTransform[10] = 1.0;
            deviceTransform[15] = 1.0;

            // We build a very simple scene graph with two nodes. The root node is a
            // transform that scale its children by the device pixel ratio. This transform
            // lets us paint in "logical" pixels which are converted to device pixels by
            // this scaling operation.
            SceneBuilder sceneBuilder = new SceneBuilder();
            sceneBuilder.PushTransform(deviceTransform);
            sceneBuilder.AddPicture(Offset.Zero, picture);
            sceneBuilder.Pop();

            // When we're done recording the scene, we call build() to obtain an immutable
            // record of the scene we've recorded.
            return sceneBuilder.Build();
        }

        private void OnBeginFrame(Duration timeStamp)
        {
            Rect paintBounds = Offset.Zero & (Window.Instance.PhysicalSize / Window.Instance.DevicePixelRatio);
            // First, record a picture with our painting commands.
            Picture picture = Paint(paintBounds);
            // Second, include that picture in a scene graph.
            Scene scene = Composite(picture, paintBounds);
            // Third, instruct the engine to render that scene graph.
            Window.Instance.Render(scene);
        }

        private void HandlePointerDataPacket(PointerDataPacket packet)
        {
            // The pointer packet contains a number of pointer movements, which we iterate
            // through and process.
            foreach (PointerData datum in packet.Data)
            {
                if (datum.Change == PointerChange.Down)
                {
                    // If the pointer went down, we change the color of the circle to blue.
                    color = new Color(0xFF0000FF);
                    // Rather than calling paint() synchronously, we ask the engine to
                    // schedule a frame. The engine will call onBeginFrame when it is actually
                    // time to produce the frame.
                    Window.Instance.ScheduleFrame();
                }
                else if (datum.Change == PointerChange.Up)
                {
                    // Similarly, if the pointer went up, we change the color of the circle to
                    // green and schedule a frame. It's harmless to call scheduleFrame many
                    // times because the engine will ignore redundant requests up until the
                    // point where the engine calls onBeginFrame, which signals the boundary
                    // between one frame and another.
                    color = new Color(0xFF00FF00);
                    Window.Instance.ScheduleFrame();
                }
            }
        }

        // This function is the primary entry point to your application. The engine
        // calls main() as soon as it has loaded your code.
        public void Main()
        {
            color = new Color(0xFF00FF00);
            // The engine calls onBeginFrame whenever it wants us to produce a frame.
            Window.Instance.OnBeginFrame = OnBeginFrame;
            // The engine calls onPointerDataPacket whenever it had updated information
            // about the pointers directed at our app.
            Window.Instance.OnPointerDataPacket = HandlePointerDataPacket;
            // Here we kick off the whole process by asking the engine to schedule a new
            // frame. The engine will eventually call onBeginFrame when it is time for us
            // to actually produce the frame.
            Window.Instance.ScheduleFrame();
        }
    }
}
