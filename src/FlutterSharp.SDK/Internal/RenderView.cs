using System;
using FlutterSharp.UI;

namespace FlutterSharp.SDK.Internal
{
    /// The layout constraints for the root render object.
    public class ViewConfiguration
    {
        /// Creates a view configuration.
        ///
        /// By default, the view has zero [size] and a [devicePixelRatio] of 1.0.
        public ViewConfiguration(Size size = null, double devicePixelRatio = 1.0)
        {
            if (size == null)
                size = Size.Zero;

            this.Size = size;
            this.DevicePixelRatio = devicePixelRatio;
        }

        /// The size of the output surface.
        public readonly Size Size;

        /// The pixel density of the output surface.
        public readonly double DevicePixelRatio;

        public Matrix4 ToMatrix()
        {
            return Matrix4.Diagonal3Values(DevicePixelRatio, DevicePixelRatio, 1.0);
        }

        /// Creates a transformation matrix that applies the [devicePixelRatio].
       /*
        @override
        String toString() => '$size at ${debugFormatDouble(devicePixelRatio)}x';*/
    }

    public class RenderObjectWithChildMixin<T> : RenderObject
    {
        public T Child;

        public RenderObjectWithChildMixin(T child)
        {
            this.Child = child;
        }
    }

    public class RenderBoxWithChildMixin<ChildType> : RenderBox
        where ChildType : RenderObject
    {
        private ChildType _child;
        /// The render object's unique child
        public ChildType Child
        {
            get { return _child; }
            set
            {
                if (_child != null)
                    DropChild(_child);
                _child = value;
                if (_child != null)
                    AdoptChild(_child);
            }
        }
        
        public override void Attach(object owner)
        {
            base.Attach(owner);
            if (_child != null)
                _child.Attach(owner);
        }

        public override void Detach()
        {
            base.Detach();
            if (_child != null)
                _child.Detach();
        }

        public override void RedepthChildren()
        {
            if (_child != null)
                RedepthChild(_child);
        }

        public override void VisitChildren(RenderObjectVisitor visitor)
        {
            if (_child != null)
                visitor(_child);
        }
    }

    public class RenderView : RenderObjectWithChildMixin<RenderBox>
    {
        /// Creates the root of the render tree.
        ///
        /// Typically created by the binding (e.g., [RendererBinding]).
        ///
        /// The [configuration] must not be null.
        public RenderView(RenderBox child = null, ViewConfiguration configuration = null, Window window = null)
            : base(child)
        {
            this.Configuration = configuration;
            this._window = window;
        }

        /// The current layout size of the view.
        public Size Size => _size;
        private Size _size = Size.Zero;

        /// The constraints used for the root layout.
        /// The configuration is initially set by the `configuration` argument
        /// passed to the constructor.
        ///
        /// Always call [prepareInitialFrame] before changing the configuration
        private ViewConfiguration _configuration;
        public ViewConfiguration Configuration
        {
            get
            {
                return _configuration;
            }
            set
            {
                if (_configuration == value)
                    return;
                _configuration = value;
                ReplaceRootLayer(_updateMatricesAndCreateNewRootLayer() as OffsetLayer);
                MarkNeedsLayout();
            }
        }

        internal readonly Window _window;

        /// Whether Flutter should automatically compute the desired system UI.
        ///
        /// When this setting is enabled, Flutter will hit-test the layer tree at the
        /// top and bottom of the screen on each frame looking for an
        /// [AnnotatedRegionLayer] with an instance of a [SystemUiOverlayStyle]. The
        /// hit-test result from the top of the screen provides the status bar settings
        /// and the hit-test result from the bottom of the screen provides the system
        /// nav bar settings.
        ///
        /// Setting this to false does not cause previous automatic adjustments to be
        /// reset, nor does setting it to true cause the app to update immediately.
        ///
        /// If you want to imperatively set the system ui style instead, it is
        /// recommended that [automaticSystemUiAdjustment] is set to false.
        ///
        /// See also:
        ///
        ///  * [AnnotatedRegion], for placing [SystemUiOverlayStyle] in the layer tree.
        ///  * [SystemChrome.setSystemUIOverlayStyle], for imperatively setting the system ui style.
        public bool AutomaticSystemUiAdjustment = true;

        /// Bootstrap the rendering pipeline by preparing the first frame.
        ///
        /// This should only be called once, and must be called before changing
        /// [configuration]. It is typically called immediately after calling the
        /// constructor.
        ///
        /// This does not actually schedule the first frame. Call
        /// [PipelineOwner.requestVisualUpdate] on [owner] to do that.
        public void PrepareInitialFrame()
        {
            ScheduleInitialLayout();
            ScheduleInitialPaint(_updateMatricesAndCreateNewRootLayer() as ContainerLayer);
        }

        Matrix4 _rootTransform; 

        private Layer _updateMatricesAndCreateNewRootLayer()
        {
            _rootTransform = Configuration.ToMatrix();
            ContainerLayer rootLayer = new TransformLayer(transform: _rootTransform);
            rootLayer.Attach(this);
            return rootLayer;
        }

        // We never call layout() on this class, so this should never get
        // checked. (This class is laid out using scheduleInitialLayout().)
        public override void DebugAssertDoesMeetConstraints()
        {
        }

        protected override void PerformResize()
        {
        }

        protected override void PerformLayout()
        {
            _size = Configuration.Size;

            if (Child != null)
                Child.Layout(BoxConstraints.Tight(_size));
        }

        public override void Rotate(int oldAngle = 0, int newAngle = 0, Duration time = null)
        {
        }

        public override bool IsRepaintBoundary => true;

        public override void Paint(PaintingContext context, Offset offset)
        {
            if (Child != null)
                context.PaintChild(Child, offset);
        }

        public void ApplyPaintTransform(RenderBox child, Matrix4 transform)
        {
            transform.Multiply(_rootTransform);
            base.ApplyPaintTransform(child, transform);
        }

        /// Uploads the composited layer tree to the engine.
        ///
        /// Actually causes the output of the rendering pipeline to appear on screen.
        public void CompositeFrame()
        {
            SceneBuilder builder = new SceneBuilder();
            Scene scene = Layer.BuildScene(builder);
            if (AutomaticSystemUiAdjustment)
                _updateSystemChrome();
            _window.Render(scene);
            scene.Dispose();
        }

        void _updateSystemChrome()
        {
            Rect bounds = PaintBounds;
            Offset top = new Offset(bounds.Center.Dx, _window.Padding.Top / _window.DevicePixelRatio);
            Offset bottom = new Offset(bounds.Center.Dx, bounds.Center.Dy - _window.Padding.Bottom / _window.DevicePixelRatio);

            /*SystemUiOverlayStyle upperOverlayStyle = layer.find<SystemUiOverlayStyle>(top);
            // Only android has a customizable system navigation bar.
            SystemUiOverlayStyle lowerOverlayStyle;
            switch (defaultTargetPlatform)
            {
                case TargetPlatform.android:
                    lowerOverlayStyle = layer.find<SystemUiOverlayStyle>(bottom);
                    break;
                case TargetPlatform.iOS:
                case TargetPlatform.fuchsia:
                    break;
            }
            // If there are no overlay styles in the UI don't bother updating.
            if (upperOverlayStyle != null || lowerOverlayStyle != null)
            {
                final SystemUiOverlayStyle overlayStyle = SystemUiOverlayStyle(
                  statusBarBrightness: upperOverlayStyle?.statusBarBrightness,
                  statusBarIconBrightness: upperOverlayStyle?.statusBarIconBrightness,
                  statusBarColor: upperOverlayStyle?.statusBarColor,
                  systemNavigationBarColor: lowerOverlayStyle?.systemNavigationBarColor,
                  systemNavigationBarDividerColor: lowerOverlayStyle?.systemNavigationBarDividerColor,
                  systemNavigationBarIconBrightness: lowerOverlayStyle?.systemNavigationBarIconBrightness,
          
                );
                SystemChrome.setSystemUIOverlayStyle(overlayStyle);
            }*/
        }

        public new Rect PaintBounds => Offset.Zero & (Size * Configuration.DevicePixelRatio);

        public Rect SemanticBounds = Rect.Zero;
    }
}
