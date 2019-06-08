using FlutterSharp.UI;

namespace FlutterSharp.Samples.Layers.Raw
{
    // This example shows how to draw some bi-directional text using the raw
    // interface to the engine.
    public class Text : IFlutterMain
    {
        // A paragraph represents a rectangular region that contains some text.
        Paragraph paragraph;

        private Picture Paint(Rect paintBounds)
        {
            PictureRecorder recorder = new PictureRecorder();
            UI.Canvas canvas = new UI.Canvas(recorder, paintBounds);

            double devicePixelRatio = Window.Instance.DevicePixelRatio;
            Size logicalSize = Window.Instance.PhysicalSize / devicePixelRatio;

            canvas.Translate(logicalSize.Width / 2.0, logicalSize.Height / 2.0);
            canvas.DrawRect(Rect.FromLTRB(-100.0, -100.0, 100.0, 100.0),
                  new Paint() { Color = Color.FromARGB(255, 0, 255, 0) });

            // The paint method of Paragraph draws the contents of the paragraph onto the
            // given canvas.
            canvas.DrawParagraph(paragraph, new Offset(-paragraph.Width / 2.0, (paragraph.Width / 2.0) - 125.0));

            return recorder.EndRecording();
        }

        private Scene Composite(Picture picture,Rect paintBounds)
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

        private void OnBeginFrame(Duration timeStamp)
        {
            Rect paintBounds = Offset.Zero & (Window.Instance.PhysicalSize / Window.Instance.DevicePixelRatio);
            Picture picture = Paint(paintBounds);
            Scene scene = Composite(picture, paintBounds);
            Window.Instance.Render(scene);
        }

        public void Main()
        {
            // To create a paragraph of text, we use ParagraphBuilder.
            ParagraphBuilder builder = new ParagraphBuilder(
              // The text below has a primary direction of left-to-right.
              // The embedded text has other directions.
              // If this was TextDirection.rtl, the "Hello, world" text would end up on
              // the other side of the right-to-left text.
              new ParagraphStyle(textDirection: TextDirection.Ltr)
             );

            // We first push a style that turns the text blue.
            builder.PushStyle(new TextStyle(color: new Color(0xFF0000FF)));
            builder.AddText("Hello, ");
            // The next run of text will be bold.
            builder.PushStyle(new TextStyle(fontWeight: FontWeight.Bold));
            builder.AddText("world. ");
            // The pop() command signals the end of the bold styling.
            builder.Pop();
            // We add text to the paragraph in logical order. The paragraph object
            // understands bi-directional text and will compute the visual ordering
            // during layout.
            builder.AddText("هذا هو قليلا طويلة من النص الذي يجب التفاف .");
            // The second pop() removes the blue color.
            builder.Pop();
            // We can add more text with the default styling.
            builder.AddText(" و أكثر قليلا لجعله أطول. ");
            builder.AddText("สวัสดี");
            // When we're done adding styles and text, we build the Paragraph object, at
            // which time we can apply styling that affects the entire paragraph, such as
            // left, right, or center alignment. Once built, the contents of the paragraph
            // cannot be altered, but sizing and positioning information can be updated.
            paragraph = builder.Build();
              // Next, we supply a width that the text is permitted to occupy and we ask
              // the paragraph to the visual position of each its glyphs as well as its
              // overall size, subject to its sizing constraints.
            paragraph.Layout(new ParagraphConstraints(width: 180.0));

            // Finally, we register our beginFrame callback and kick off the first frame.
            Window.Instance.OnBeginFrame += OnBeginFrame;
            Window.Instance.ScheduleFrame();
        }
    }
}
