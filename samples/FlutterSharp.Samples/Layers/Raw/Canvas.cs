using FlutterSharp.UI;
using System;
using System.Collections.Generic;

namespace FlutterSharp.Samples.Layers.Raw
{
    // This example shows how to use the ui.Canvas interface to draw various shapes
    // with gradients and transforms.
    public class Canvas : IFlutterMain
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

            Paint paint = new Paint();
            canvas.DrawPaint(new Paint() { Color = new Color(0xFFFFFFFF) });

            Size size = paintBounds.Size;
            Offset mid = size.Center(Offset.Zero);
            double radius = size.ShortestSide / 2.0;

            double devicePixelRatio = Window.Instance.DevicePixelRatio;
            Size logicalSize = Window.Instance.PhysicalSize / devicePixelRatio;

            // Saves a copy of current transform onto the save stack
            canvas.Save();

            // Note that transforms that occur after this point apply only to the
            // yellow-bluish rectangle

            // This line will cause the transform to shift entirely outside the paint
            // boundaries, which will cause the canvas interface to discard its
            // commands. Comment it out to see it on screen.
            canvas.Translate(-mid.Dx / 2.0, logicalSize.Height * 2.0);

            // Clips the current transform
            canvas.ClipRect(
              Rect.FromLTRB(0, radius + 50, logicalSize.Width, logicalSize.Height),
              clipOp: ClipOp.Difference
            );

            // Shifts the coordinate space of and rotates the current transform
            canvas.Translate(mid.Dx, mid.Dy);
            canvas.Rotate(Math.PI / 4);

            Gradient yellowBlue = Gradient.Linear(
                new Offset(-radius, -radius),
                new Offset(0.0, 0.0), 
                new List<Color> { new Color(0xFFFFFF00), new Color(0xFF0000FF) });

            // Draws a yellow-bluish rectangle
            canvas.DrawRect(
              Rect.FromLTRB(-radius, -radius, radius, radius),
              new Paint() { Shader = yellowBlue });

            // Note that transforms that occur after this point apply only to the
            // yellow circle

            // Scale x and y by 0.5.
            Float64List scaleMatrix = Float64List.FromList(
                0.5, 0.0, 0.0, 0.0,
                0.0, 0.5, 0.0, 0.0,
                0.0, 0.0, 1.0, 0.0,
                0.0, 0.0, 0.0, 1.0);

            canvas.Transform(scaleMatrix);

            // Sets paint to transparent yellow
            paint.Color = Color.FromARGB(128, 0, 255, 0);

            // Draws a transparent yellow circle
            canvas.DrawCircle(Offset.Zero, radius, paint);

            // Restores the transform from before `save` was called
            canvas.Restore();

            // Sets paint to transparent red
            paint.Color = Color.FromARGB(128, 255, 0, 0);

            // Note that this circle is drawn on top of the previous layer that contains
            // the rectangle and smaller circle
            canvas.DrawCircle(new Offset(150.0, 300.0), radius, paint);

            // When we're done issuing painting commands, we end the recording an receive
            // a Picture, which is an immutable record of the commands we've issued. You
            // can draw a Picture into another canvas or include it as part of a
            // composited scene.
            return recorder.EndRecording();
        }

        private Scene Composite(Picture picture, Rect paintBounds)
        {
            double devicePixelRatio = Window.Instance.DevicePixelRatio;
            Float64List deviceTransform = new Float64List(16);
            deviceTransform[0] = devicePixelRatio;
            deviceTransform[5] = devicePixelRatio;
            deviceTransform[10] = 1.0;
            deviceTransform[15] = 1.0;
            SceneBuilder sceneBuilder = new SceneBuilder();
            sceneBuilder.PushTransform(deviceTransform);
            sceneBuilder.AddPicture(Offset.Zero, picture);
            sceneBuilder.Pop();
            return sceneBuilder.Build();
        }

        private void OnBeginFrame(Duration duration)
        {
            Rect paintBounds = Offset.Zero & (Window.Instance.PhysicalSize / Window.Instance.DevicePixelRatio);
            Picture picture = Paint(paintBounds);
            Scene scene = Composite(picture, paintBounds);
            Window.Instance.Render(scene);
        }

        public void Main()
        {
            Window.Instance.OnBeginFrame += OnBeginFrame;
            Window.Instance.ScheduleFrame();
        }
    }
}
