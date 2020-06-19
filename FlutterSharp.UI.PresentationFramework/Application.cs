using System;

namespace FlutterSharp.UI.PresentationFramework
{
    /// <summary>
    /// Encapsulates a FlutterSharp application.
    /// </summary>
    public class Application
    {
        private Window mainWindow;
        private Size logicalSize;

        /// <summary>
        /// Initializes a new instance of the <see cref="Application"/> class.
        /// </summary>
        public Application()
        {
            UI.Window.Instance.OnBeginFrame = this.OnBeginFrame;
        }

        /// <summary>
        /// Gets or sets the main window of the application.
        /// </summary>
        protected Window MainWindow
        { 
            get => this.mainWindow;
            set 
            {
                if (this.mainWindow == value)
                    return;

                this.mainWindow = value;
                UI.Window.Instance.ScheduleFrame();
            }
        }

        private void MeasureLoop()
        {
            this.logicalSize = new Size(
                UI.Window.Instance.PhysicalSize.Width - UI.Window.Instance.ViewPadding.Left - UI.Window.Instance.ViewPadding.Right
                , UI.Window.Instance.PhysicalSize.Height - UI.Window.Instance.ViewPadding.Top - UI.Window.Instance.ViewPadding.Bottom);

            this.MainWindow.Measure(this.logicalSize);
        }

        private void ArrangeLoop()
        {
            this.MainWindow.Arrange(Offset.Zero & this.logicalSize);
        }

        private void DrawLoop()
        {
            var paintBounds = Offset.Zero & UI.Window.Instance.PhysicalSize;

            SceneBuilder scene = new SceneBuilder();
            var recorder = new PictureRecorder();
            UI.Canvas canvas = new UI.Canvas(recorder);

            this.MainWindow.Draw(canvas);

            scene.PushClipRect(paintBounds);
            scene.AddPicture(new Offset(UI.Window.Instance.ViewPadding.Left, UI.Window.Instance.ViewPadding.Top), recorder.EndRecording());

            UI.Window.Instance.Render(scene.Build());
        }

        private void OnBeginFrame(Duration duration)
        {
            if (this.MainWindow == null)
                return;

            if (UI.Window.Instance.PhysicalSize == Size.Zero)
                return;

            this.MeasureLoop();
            this.ArrangeLoop();
            this.DrawLoop();
        }
    }
}
