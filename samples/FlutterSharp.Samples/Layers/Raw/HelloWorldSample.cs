using System;
using FlutterSharp.SDK.Internal;
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
            new RenderingFlutterBinding
                (
                    root: new RenderPadding(new CustomRenderBox(), new EdgeInsets(100))
                );
        }
    }

    public class CustomRenderBox : RenderBox
    {
       // public override bool IsRepaintBoundary => true;


        public override void Paint(PaintingContext context, Offset offset)
        {
            base.Paint(context, offset);
            
            var bounds = (offset * 1.5) & (Size * 1.5);

            // this.PaintBounds

            // context.Canvas.DrawRect(bounds, new UI.Paint() { Color = Color.FromARGB(255, 140, 100, 155) });


            context.Canvas.ClipRect(bounds);

            context.Canvas.DrawColor(Color.FromARGB(255, 140, 100, 155), BlendMode.Src);
        }

        protected override void PerformLayout()
        {
            var t = Constraints;
            var parent = this.ParentData;

            this.Size = new Size(Constraints.MaxWidth, Constraints.MaxHeight);
        }
    }



    /*class MyApp : StatelessWidget
    {
        public new Widget Build(BuildContext context) {
            return new MaterialApp(
              title: "Welcome to Flutter",
              home: new Scaffold(
                appBar: new AppBar(
                  title: new Text("Welcome to Flutter"),
                ),
                body: new Center(
                  child: new Text("Hello World"),
      
                ),
        
              ),
        
            );
        }
    }*/
}
