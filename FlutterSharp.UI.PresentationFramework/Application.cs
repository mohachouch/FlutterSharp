namespace FlutterSharp.UI.PresentationFramework
{
    /// <summary>
    /// Encapsulates a FlutterSharp application.
    /// </summary>
    public class Application
    {
        private Window mainWindow;

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

        }

        private void ArrangeLoop()
        {

        }

        private void DrawLoop()
        {
            var paintBounds = Offset.Zero & UI.Window.Instance.PhysicalSize;

            SceneBuilder scene = new SceneBuilder();

            this.MainWindow.Draw(scene, paintBounds);

            UI.Window.Instance.Render(scene.Build());
        }

        private void OnBeginFrame(Duration duration)
        {
            if (this.MainWindow == null)
                return;

            this.MeasureLoop();
            this.ArrangeLoop();
            this.DrawLoop();
        }
    }
}
