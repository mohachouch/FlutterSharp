using System;
using FlutterSharp.UI;

namespace FlutterSharp.SDK.Internal
{
    public class RendererBinding : SchedulerBinding
    {
        protected override void InitInstances()
        {
            base.InitInstances();
            _instance = this;
            _pipelineOwner = new PipelineOwner(
              onNeedVisualUpdate: EnsureVisualUpdate,
              onSemanticsOwnerCreated: null,
              onSemanticsOwnerDisposed: null
            );

            Window.OnMetricsChanged = HandleMetricsChanged;

            InitRenderView();
            this.PersistentCallback += _handlePersistentFrameCallback;
        }

        /// The current [RendererBinding], if one has been created.
        public static RendererBinding Instance => _instance;
        private static RendererBinding _instance;

        /// Creates a [RenderView] object to be the root of the
        /// [RenderObject] rendering tree, and initializes it so that it
        /// will be rendered when the next frame is requested.
        ///
        /// Called automatically when the binding is created.
        public void InitRenderView()
        {
            RenderView = new RenderView(configuration: CreateViewConfiguration(), window: this.Window);
            RenderView.PrepareInitialFrame();
        }

        /// The render tree's owner, which maintains dirty state for layout,
        /// composite, paint, and accessibility semantics
        public PipelineOwner PipelineOwner => _pipelineOwner;
        PipelineOwner _pipelineOwner;

        public RenderView RenderView
        {
            get
            {
                return _pipelineOwner.RootNode as RenderView;
            }
            set
            {
                _pipelineOwner.RootNode = value;    
            }
        }

        /// Called when the system metrics change.
        ///
        /// See [Window.onMetricsChanged].
        protected void HandleMetricsChanged()
        {
            RenderView.Configuration = CreateViewConfiguration();
            ScheduleForcedFrame();
        }

        /// Returns a [ViewConfiguration] configured for the [RenderView] based on the
        /// current environment.
        ///
        /// This is called during construction and also in response to changes to the
        /// system metrics.
        ///
        /// Bindings can override this method to change what size or device pixel
        /// ratio the [RenderView] will use. For example, the testing framework uses
        /// this to force the display into 800x600 when a test is run on the device
        /// using `flutter run`.
        public ViewConfiguration CreateViewConfiguration()
        {
            double devicePixelRatio = Window.DevicePixelRatio;
            return new ViewConfiguration(
              size: Window.PhysicalSize / devicePixelRatio,
              devicePixelRatio: devicePixelRatio
            );
        }

        private void _handlePersistentFrameCallback(Duration timeStamp)
        {
            DrawFrame();
        }

        public void DrawFrame()
        {
            PipelineOwner.FlushLayout();
            PipelineOwner.FlushCompositingBits();
            PipelineOwner.FlushPaint();
            RenderView.CompositeFrame(); // this sends the bits to the GPU
           // PipelineOwner.FlushSemantics(); // this also sends the semantics to the OS.
        }
    }
}
