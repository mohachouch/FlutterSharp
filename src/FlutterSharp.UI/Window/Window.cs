using System;
using System.Collections.Generic;
using System.Linq;

namespace FlutterSharp.UI
{
    /// Signature of callbacks that have no arguments and return no data. 
    public delegate void VoidCallback();

    /// Signature for [Window.onBeginFrame].
    public delegate void FrameCallback(Duration duration);

    /// Signature for [Window.onPointerDataPacket].
    public delegate void PointerDataPacketCallback(PointerDataPacket packet);

    /// Signature for [Window.onSemanticsAction].
    public delegate void SemanticsActionCallback(int id, SemanticsAction action, ByteData args);

    /// Signature for responses to platform messages.
    ///
    /// Used as a parameter to [Window.sendPlatformMessage] and
    /// [Window.onPlatformMessage].
    public delegate void PlatformMessageResponseCallback(ByteData data);

    /// Signature for [Window.onPlatformMessage].
    public delegate void PlatformMessageCallback(String name, ByteData data, PlatformMessageResponseCallback callback);

    /// The most basic interface to the host operating system's user interface.
    ///
    /// There is a single Window instance in the system, which you can
    /// obtain from the [window] property.
    public class Window
    {
        /// The [Window] singleton. This object exposes the size of the display, the
        /// core scheduler API, the input event callback, the graphics drawing API, and
        /// other such core services.
        public static Window Instance = new Window();

        public Window()
        {
        }

        /// The number of device pixels for each logical pixel. This number might not
        /// be a power of two. Indeed, it might not even be an integer. For example,
        /// the Nexus 6 has a device pixel ratio of 3.5.
        ///
        /// Device pixels are also referred to as physical pixels. Logical pixels are
        /// also referred to as device-independent or resolution-independent pixels.
        ///
        /// By definition, there are roughly 38 logical pixels per centimeter, or
        /// about 96 logical pixels per inch, of the physical display. The value
        /// returned by [devicePixelRatio] is ultimately obtained either from the
        /// hardware itself, the device drivers, or a hard-coded value stored in the
        /// operating system or firmware, and may be inaccurate, sometimes by a
        /// significant margin.
        ///
        /// The Flutter framework operates in logical pixels, so it is rarely
        /// necessary to directly deal with this property.
        ///
        /// When this changes, [onMetricsChanged] is called.
        ///
        /// See also:
        ///
        ///  * [WidgetsBindingObserver], for a mechanism at the widgets layer to
        ///    observe when this value changes.
        public double DevicePixelRatio { get; set; } = 1.0;

        /// The dimensions of the rectangle into which the application will be drawn,
        /// in physical pixels.
        ///
        /// When this changes, [onMetricsChanged] is called.
        ///
        /// At startup, the size of the application window may not be known before Dart
        /// code runs. If this value is observed early in the application lifecycle,
        /// it may report [Size.zero].
        ///
        /// This value does not take into account any on-screen keyboards or other
        /// system UI. The [padding] and [viewInsets] properties provide a view into
        /// how much of each side of the application may be obscured by system UI.
        ///
        /// See also:
        ///
        ///  * [WidgetsBindingObserver], for a mechanism at the widgets layer to
        ///    observe when this value changes.
        public Size PhysicalSize { get; set; } = Size.Zero;

        /// The number of physical pixels on each side of the display rectangle into
        /// which the application can render, but over which the operating system
        /// will likely place system UI, such as the keyboard, that fully obscures
        /// any content.
        ///
        /// When this changes, [onMetricsChanged] is called.
        ///
        /// See also:
        ///
        ///  * [WidgetsBindingObserver], for a mechanism at the widgets layer to
        ///    observe when this value changes.
        ///  * [MediaQuery.of], a simpler mechanism for the same.
        ///  * [Scaffold], which automatically applies the view insets in material
        ///    design applications.
        public WindowPadding ViewInsets { get; set; } = WindowPadding.Zero;

        /// The number of physical pixels on each side of the display rectangle into
        /// which the application can render, but which may be partially obscured by
        /// system UI (such as the system notification area), or or physical
        /// intrusions in the display (e.g. overscan regions on television screens or
        /// phone sensor housings).
        ///
        /// When this changes, [onMetricsChanged] is called.
        ///
        /// See also:
        ///
        ///  * [WidgetsBindingObserver], for a mechanism at the widgets layer to
        ///    observe when this value changes.
        ///  * [MediaQuery.of], a simpler mechanism for the same.
        ///  * [Scaffold], which automatically applies the padding in material design
        ///    applications.
        public WindowPadding Padding { get; set; } = WindowPadding.Zero;

        /// A callback that is invoked whenever the [devicePixelRatio],
        /// [physicalSize], [padding], or [viewInsets] values change, for example
        /// when the device is rotated or when the application is resized (e.g. when
        /// showing applications side-by-side on Android).
        ///
        /// The engine invokes this callback in the same zone in which the callback
        /// was set.
        ///
        /// The framework registers with this callback and updates the layout
        /// appropriately.
        ///
        /// See also:
        ///
        ///  * [WidgetsBindingObserver], for a mechanism at the widgets layer to
        ///    register for notifications when this is called.
        ///  * [MediaQuery.of], a simpler mechanism for the same.
        private VoidCallback _onMetricsChanged;
        private Zone _onMetricsChangedZone;
        public VoidCallback OnMetricsChanged
        {
            get
            {
                return _onMetricsChanged;
            }
            set
            {
                _onMetricsChanged = value;
                _onMetricsChangedZone = Zone.Current;
            }
        }

        /// The system-reported default locale of the device.
        ///
        /// This establishes the language and formatting conventions that application
        /// should, if possible, use to render their user interface.
        ///
        /// This is the first locale selected by the user and is the user's
        /// primary locale (the locale the device UI is displayed in)
        ///
        /// This is equivalent to `locales.first` and will provide an empty non-null locale
        /// if the [locales] list has not been set or is empty.
        /// 
        public Locale Locale => this.Locales.FirstOrDefault();


        /// The full system-reported supported locales of the device.
        ///
        /// This establishes the language and formatting conventions that application
        /// should, if possible, use to render their user interface.
        ///
        /// The list is ordered in order of priority, with lower-indexed locales being
        /// preferred over higher-indexed ones. The first element is the primary [locale].
        ///
        /// The [onLocaleChanged] callback is called whenever this value changes.
        ///
        /// See also:
        ///
        ///  * [WidgetsBindingObserver], for a mechanism at the widgets layer to
        ///    observe when this value changes.
        public List<Locale> Locales { get; set; }

        /// A callback that is invoked whenever [locale] changes value.
        ///
        /// The framework invokes this callback in the same zone in which the
        /// callback was set.
        ///
        /// See also:
        ///
        ///  * [WidgetsBindingObserver], for a mechanism at the widgets layer to
        ///    observe when this callback is invoked.
        private VoidCallback _onLocaleChanged;
        private Zone _onLocaleChangedZone;
        public VoidCallback OnLocaleChanged
        {
            get
            {
                return _onLocaleChanged;
            }
            set
            {
                _onLocaleChanged = value;
                _onLocaleChangedZone = Zone.Current;
            }
        }

        /// The lifecycle state immediately after dart isolate initialization.
        ///
        /// This property will not be updated as the lifecycle changes.
        ///
        /// It is used to initialize [SchedulerBinding.lifecycleState] at startup
        /// with any buffered lifecycle state events.

        public string InitialLifecycleState
        {
            get
            {
                _initialLifecycleStateAccessed = true;
                return _initialLifecycleState;
            }
            set
            {
                _initialLifecycleState = value;
            }
        }

        private string _initialLifecycleState;

        /// Tracks if the initial state has been accessed. Once accessed, we
        /// will stop updating the [initialLifecycleState], as it is not the
        /// preferred way to access the state.
        private bool _initialLifecycleStateAccessed = false;

        /// The system-reported text scale.
        ///
        /// This establishes the text scaling factor to use when rendering text,
        /// according to the user's platform preferences.
        ///
        /// The [onTextScaleFactorChanged] callback is called whenever this value
        /// changes.
        ///
        /// See also:
        ///
        ///  * [WidgetsBindingObserver], for a mechanism at the widgets layer to
        ///    observe when this value changes.
        public double TextScaleFactor { get; set; } = 1.0;

        /// The setting indicating whether time should always be shown in the 24-hour
        /// format.
        ///
        /// This option is used by [showTimePicker].
        public bool AlwaysUse24HourFormat { get; set; } = false;

        /// A callback that is invoked whenever [textScaleFactor] changes value.
        ///
        /// The framework invokes this callback in the same zone in which the
        /// callback was set.
        ///
        /// See also:
        ///
        ///  * [WidgetsBindingObserver], for a mechanism at the widgets layer to
        ///    observe when this callback is invoked.
        private VoidCallback _onTextScaleFactorChanged;
        private Zone _onTextScaleFactorChangedZone;
        public VoidCallback OnTextScaleFactorChanged
        {
            get
            {
                return _onTextScaleFactorChanged;
            }
            set
            {
                _onTextScaleFactorChanged = value;
                _onTextScaleFactorChangedZone = Zone.Current;
            }
        }

        /// The setting indicating the current brightness mode of the host platform.
        /// If the platform has no preference, [platformBrightness] defaults to [Brightness.light].
        public Brightness PlatformBrightness { get; set; } = Brightness.Light;

        /// A callback that is invoked whenever [platformBrightness] changes value.
        ///
        /// The framework invokes this callback in the same zone in which the
        /// callback was set.
        ///
        /// See also:
        ///
        ///  * [WidgetsBindingObserver], for a mechanism at the widgets layer to
        ///    observe when this callback is invoked.
        private VoidCallback _onPlatformBrightnessChanged;
        private Zone _onPlatformBrightnessChangedZone;
        public VoidCallback OnPlatformBrightnessChanged
        {
            get
            {
                return _onPlatformBrightnessChanged;
            }
            set
            {
                _onPlatformBrightnessChanged = value;
                _onPlatformBrightnessChangedZone = Zone.Current;
            }
        }

        /// A callback that is invoked to notify the application that it is an
        /// appropriate time to provide a scene using the [SceneBuilder] API and the
        /// [render] method. When possible, this is driven by the hardware VSync
        /// signal. This is only called if [scheduleFrame] has been called since the
        /// last time this callback was invoked.
        ///
        /// The [onDrawFrame] callback is invoked immediately after [onBeginFrame],
        /// after draining any microtasks (e.g. completions of any [Future]s) queued
        /// by the [onBeginFrame] handler.
        ///
        /// The framework invokes this callback in the same zone in which the
        /// callback was set.
        ///
        /// See also:
        ///
        ///  * [SchedulerBinding], the Flutter framework class which manages the
        ///    scheduling of frames.
        ///  * [RendererBinding], the Flutter framework class which manages layout and
        ///    painting.
        private FrameCallback _onBeginFrame;
        private Zone _onBeginFrameZone;
        public FrameCallback OnBeginFrame
        {
            get
            {
                return _onBeginFrame;
            }
            set
            {
                _onBeginFrame = value;
                _onBeginFrameZone = Zone.Current;
            }
        }

        /// A callback that is invoked for each frame after [onBeginFrame] has
        /// completed and after the microtask queue has been drained. This can be
        /// used to implement a second phase of frame rendering that happens
        /// after any deferred work queued by the [onBeginFrame] phase.
        ///
        /// The framework invokes this callback in the same zone in which the
        /// callback was set.
        ///
        /// See also:
        ///
        ///  * [SchedulerBinding], the Flutter framework class which manages the
        ///    scheduling of frames.
        ///  * [RendererBinding], the Flutter framework class which manages layout and
        ///    painting.
        private VoidCallback _onDrawFrame;
        private Zone _onDrawFrameZone;
        public VoidCallback OnDrawFrame
        {
            get
            {
                return _onDrawFrame;
            }
            set
            {
                _onDrawFrame = value;
                _onDrawFrameZone = Zone.Current;
            }
        }

        /// A callback that is invoked when pointer data is available.
        ///
        /// The framework invokes this callback in the same zone in which the
        /// callback was set.
        ///
        /// See also:
        ///
        ///  * [GestureBinding], the Flutter framework class which manages pointer
        ///    events.
        private PointerDataPacketCallback _onPointerDataPacket;
        private Zone _onPointerDataPacketZone;
        public PointerDataPacketCallback OnPointerDataPacket
        {
            get
            {
                return _onPointerDataPacket;
            }
            set
            {
                _onPointerDataPacket = value;
                _onPointerDataPacketZone = Zone.Current;
            }
        }

        /// The route or path that the embedder requested when the application was
        /// launched.
        ///
        /// This will be the string "`/`" if no particular route was requested.
        ///
        /// ## Android
        ///
        /// On Android, calling
        /// [`FlutterView.setInitialRoute`](/javadoc/io/flutter/view/FlutterView.html#setInitialRoute-java.lang.String-)
        /// will set this value. The value must be set sufficiently early, i.e. before
        /// the [runApp] call is executed in Dart, for this to have any effect on the
        /// framework. The `createFlutterView` method in your `FlutterActivity`
        /// subclass is a suitable time to set the value. The application's
        /// `AndroidManifest.xml` file must also be updated to have a suitable
        /// [`<intent-filter>`](https://developer.android.com/guide/topics/manifest/intent-filter-element.html).
        ///
        /// ## iOS
        ///
        /// On iOS, calling
        /// [`FlutterViewController.setInitialRoute`](/objcdoc/Classes/FlutterViewController.html#/c:objc%28cs%29FlutterViewController%28im%29setInitialRoute:)
        /// will set this value. The value must be set sufficiently early, i.e. before
        /// the [runApp] call is executed in Dart, for this to have any effect on the
        /// framework. The `application:didFinishLaunchingWithOptions:` method is a
        /// suitable time to set this value.
        ///
        /// See also:
        ///
        ///  * [Navigator], a widget that handles routing.
        ///  * [SystemChannels.navigation], which handles subsequent navigation
        ///    requests from the embedder.
        public string DefaultRouteName
        {
            get
            {
                // TODO : native 'Window_defaultRouteName';
                return null;
            }
        }

        /// Requests that, at the next appropriate opportunity, the [onBeginFrame]
        /// and [onDrawFrame] callbacks be invoked.
        ///
        /// See also:
        ///
        ///  * [SchedulerBinding], the Flutter framework class which manages the
        ///    scheduling of frames.
        public void ScheduleFrame()
        {
            // TODO : native 'Window_scheduleFrame';
        }

        /// Updates the application's rendering on the GPU with the newly provided
        /// [Scene]. This function must be called within the scope of the
        /// [onBeginFrame] or [onDrawFrame] callbacks being invoked. If this function
        /// is called a second time during a single [onBeginFrame]/[onDrawFrame]
        /// callback sequence or called outside the scope of those callbacks, the call
        /// will be ignored.
        ///
        /// To record graphical operations, first create a [PictureRecorder], then
        /// construct a [Canvas], passing that [PictureRecorder] to its constructor.
        /// After issuing all the graphical operations, call the
        /// [PictureRecorder.endRecording] function on the [PictureRecorder] to obtain
        /// the final [Picture] that represents the issued graphical operations.
        ///
        /// Next, create a [SceneBuilder], and add the [Picture] to it using
        /// [SceneBuilder.addPicture]. With the [SceneBuilder.build] method you can
        /// then obtain a [Scene] object, which you can display to the user via this
        /// [render] function.
        ///
        /// See also:
        ///
        ///  * [SchedulerBinding], the Flutter framework class which manages the
        ///    scheduling of frames.
        ///  * [RendererBinding], the Flutter framework class which manages layout and
        ///    painting.
        public void Render(Scene scene)
        {
            // TODO : native 'Window_render';
        }

        /// Whether the user has requested that [updateSemantics] be called when
        /// the semantic contents of window changes.
        ///
        /// The [onSemanticsEnabledChanged] callback is called whenever this value
        /// changes.
        public bool SemanticsEnabled { get; set; } = false;

        /// A callback that is invoked when the value of [semanticsEnabled] changes.
        ///
        /// The framework invokes this callback in the same zone in which the
        /// callback was set.
        private VoidCallback _onSemanticsEnabledChanged;
        private Zone _onSemanticsEnabledChangedZone;
        public VoidCallback OnSemanticsEnabledChanged
        {
            get
            {
                return _onSemanticsEnabledChanged;
            }
            set
            {
                _onSemanticsEnabledChanged = value;
                _onSemanticsEnabledChangedZone = Zone.Current;
            }
        }

        /// A callback that is invoked whenever the user requests an action to be
        /// performed.
        ///
        /// This callback is used when the user expresses the action they wish to
        /// perform based on the semantics supplied by [updateSemantics].
        ///
        /// The framework invokes this callback in the same zone in which the
        /// callback was set.
        private SemanticsActionCallback _onSemanticsAction;
        private Zone _onSemanticsActionZone;
        public SemanticsActionCallback OnSemanticsAction
        {
            get
            {
                return _onSemanticsAction;
            }
            set
            {
                _onSemanticsAction = value;
                _onSemanticsActionZone = Zone.Current;
            }
        }

        /// Additional accessibility features that may be enabled by the platform.
        AccessibilityFeatures AccessibilityFeatures { get; set; }

        /// A callback that is invoked when the value of [accessibilityFeatures] changes.
        ///
        /// The framework invokes this callback in the same zone in which the
        /// callback was set.
        private VoidCallback _onAccessibilityFeaturesChanged;
        private Zone _onAccessibilityFlagsChangedZone;
        public VoidCallback OnAccessibilityFeaturesChanged
        {
            get
            {
                return _onAccessibilityFeaturesChanged;
            }
            set
            {
                _onAccessibilityFeaturesChanged = value;
                _onAccessibilityFlagsChangedZone = Zone.Current;
            }
        }

        /// Change the retained semantics data about this window.
        ///
        /// If [semanticsEnabled] is true, the user has requested that this function
        /// be called whenever the semantic content of this window changes.
        ///
        /// In either case, this function disposes the given update, which means the
        /// semantics update cannot be used further.
        public void UpdateSemantics(SemanticsUpdate update)
        {
            // TODO : native 'Window_updateSemantics';
        }

        /// Set the debug name associated with this window's root isolate.
        ///
        /// Normally debug names are automatically generated from the Dart port, entry
        /// point, and source file. For example: `main.dart$main-1234`.
        ///
        /// This can be combined with flutter tools `--isolate-filter` flag to debug
        /// specific root isolates. For example: `flutter attach --isolate-filter=[name]`.
        /// Note that this does not rename any child isolates of the root.
        public void SetIsolateDebugName(string name)
        {
            // TODO : native 'Window_setIsolateDebugName';
        }

        /// Sends a message to a platform-specific plugin.
        ///
        /// The `name` parameter determines which plugin receives the message. The
        /// `data` parameter contains the message payload and is typically UTF-8
        /// encoded JSON but can be arbitrary data. If the plugin replies to the
        /// message, `callback` will be called with the response.
        ///
        /// The framework invokes [callback] in the same zone in which this method
        /// was called.
        public void SendPlatformMessage(string name, ByteData data, PlatformMessageResponseCallback callback)
        {
            string error =
                SendPlatformMessage(name, ZonedPlatformMessageResponseCallback(callback), data);
            if (error != null)
                throw new Exception(error);
        }

        private string SendPlatformMessage(String name, PlatformMessageResponseCallback callback, ByteData data)
        {
            // TODO : native 'Window_sendPlatformMessage';
            return null;
        }

        /// Called whenever this window receives a message from a platform-specific
        /// plugin.
        ///
        /// The `name` parameter determines which plugin sent the message. The `data`
        /// parameter is the payload and is typically UTF-8 encoded JSON but can be
        /// arbitrary data.
        ///
        /// Message handlers must call the function given in the `callback` parameter.
        /// If the handler does not need to respond, the handler should pass null to
        /// the callback.
        ///
        /// The framework invokes this callback in the same zone in which the
        /// callback was set.
        private PlatformMessageCallback _onPlatformMessage;
        private Zone _onPlatformMessageZone;
        public PlatformMessageCallback OnPlatformMessage
        {
            get
            {
                return _onPlatformMessage;
            }
            set
            {
                _onPlatformMessage = value;
                _onPlatformMessageZone = Zone.Current;
            }
        }

        /// Called by [_dispatchPlatformMessage].
        private void RespondToPlatformMessage(int responseId, ByteData data)
        {
            // TODO : native 'Window_respondToPlatformMessage';
        }


        /// Wraps the given [callback] in another callback that ensures that the
        /// original callback is called in the zone it was registered in.
        private static PlatformMessageResponseCallback ZonedPlatformMessageResponseCallback(PlatformMessageResponseCallback callback)
        {
            if (callback == null)
                return null;

            // Store the zone in which the callback is being registered.
            var registrationZone = Zone.Current;

            return null; // TODO : implement
            /*return (ByteData data) {
                registrationZone.runUnaryGuarded(callback, data);
            };*/
        }
    }
}
