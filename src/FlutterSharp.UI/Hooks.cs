using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using static FlutterSharp.UI.UITypes;

namespace FlutterSharp.UI
{
    public static class Hooks
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void UpdateWindowMetricsDelegate(double devicePixelRatio,
                                 double width,
                                 double height,
                                 double viewPaddingTop,
                                 double viewPaddingRight,
                                 double viewPaddingBottom,
                                 double viewPaddingLeft,
                                 double viewInsetTop,
                                 double viewInsetRight,
                                 double viewInsetBottom,
                                 double viewInsetLeft);

        public static void Register()
        {
            Hooks_register(UpdateWindowMetrics);
        }

        [DllImport("libflutter.so")]
        private static extern void Hooks_register(UpdateWindowMetricsDelegate updateWindowMetricsDelegate);

        // ignore: unused_element
        internal static string DecodeUTF8(ByteData message)
        {
            // TODO : implement this
            return null; // return message != null ? utf8.decoder.convert(message.buffer.asUint8List()) : null;
        }

        // ignore: unused_element
        internal static dynamic DecodeJSON(string message)
        {
            // TODO : implement this
            return null; //return message != null ? json.decode(message) : null;
        }

        // ignore: unused_element
        private static void UpdateWindowMetrics(double devicePixelRatio,
                                 double width,
                                 double height,
                                 double viewPaddingTop,
                                 double viewPaddingRight,
                                 double viewPaddingBottom,
                                 double viewPaddingLeft,
                                 double viewInsetTop,
                                 double viewInsetRight,
                                 double viewInsetBottom,
                                 double viewInsetLeft)
        {
            Window.Instance.DevicePixelRatio = devicePixelRatio;
            Window.Instance.PhysicalSize = new Size(width, height);
            Window.Instance.ViewPadding = new WindowPadding(
                  top: viewPaddingTop,
                  right: viewPaddingRight,
                  bottom: viewPaddingBottom,
                  left: viewPaddingLeft);
            Window.Instance.ViewInsets = new WindowPadding(
                  top: viewInsetTop,
                  right: viewInsetRight,
                  bottom: viewInsetBottom,
                  left: viewInsetLeft);
            Window.Instance.Padding = new WindowPadding(
                 top: Math.Max(0.0, viewPaddingTop - viewInsetTop),
                 right: Math.Max(0.0, viewPaddingRight - viewInsetRight),
                 bottom: Math.Max(0.0, viewPaddingBottom - viewInsetBottom),
                 left: Math.Max(0.0, viewPaddingLeft - viewInsetLeft));

            //Invoke(() => Window.Instance.OnMetricsChanged(), Window.Instance._onMetricsChangedZone);
        }

        private delegate string _LocaleClosure();

        private static string LocaleClosure()
        {
            if (Window.Instance.Locale == null)
            {
                return null;
            }
            return Window.Instance.Locale.ToString();
        }

        // ignore: unused_element
        private static _LocaleClosure GetLocaleClosure() => LocaleClosure;

        // ignore: unused_element
        private static void UpdateLocales(List<string> locales)
        {
            const int stringsPerLocale = 4;
            int numLocales = (int)Math.Truncate(locales.Count * 1.0 / stringsPerLocale);
            Window.Instance.Locales = new List<Locale>(numLocales);
            for (int localeIndex = 0; localeIndex < numLocales; localeIndex++)
            {
                string countryCode = locales[localeIndex * stringsPerLocale + 1];
                string scriptCode = locales[localeIndex * stringsPerLocale + 2];

                Window.Instance.Locales[localeIndex] = Locale.FromSubtags(
                  languageCode: locales[localeIndex * stringsPerLocale],
                  countryCode: countryCode.IsEmpty() ? null : countryCode,
                  scriptCode: scriptCode.IsEmpty() ? null : scriptCode);
            }
            Invoke(() => Window.Instance.OnLocaleChanged(), Window.Instance._onLocaleChangedZone);
        }

        // ignore: unused_element
        private static void UpdateUserSettingsData(string jsonData)
        {
            Dictionary<string, object> data = new Dictionary<string, object>(); //TODO : DECODE JSON json.decode(jsonData);
            if (data.Count() == 0)
            {
                return;
            }
            UpdateTextScaleFactor((double)data["textScaleFactor"]);
            UpdateAlwaysUse24HourFormat((bool)data["alwaysUse24HourFormat"]);
            UpdatePlatformBrightness((string)data["platformBrightness"]);
        }

        // ignore: unused_element
        private static void UpdateLifecycleState(string state)
        {
            // We do not update the state if the state has already been used to initialize
            // the lifecycleState.
            if (!Window.Instance._initialLifecycleStateAccessed)
                Window.Instance.InitialLifecycleState = state;
        }

        private static void UpdateTextScaleFactor(double textScaleFactor)
        {
            Window.Instance.TextScaleFactor = textScaleFactor;
            Invoke(() => Window.Instance.OnTextScaleFactorChanged(), Window.Instance._onTextScaleFactorChangedZone);
        }

        private static void UpdateAlwaysUse24HourFormat(bool alwaysUse24HourFormat)
        {
            Window.Instance.AlwaysUse24HourFormat = alwaysUse24HourFormat;
        }

        private static void UpdatePlatformBrightness(string brightnessName)
        {
            Window.Instance.PlatformBrightness = brightnessName == "dark" ? Brightness.Dark : Brightness.Light;
            Invoke(() => Window.Instance.OnPlatformBrightnessChanged(), Window.Instance._onPlatformBrightnessChangedZone);
        }

        // ignore: unused_element
        private static void UpdateSemanticsEnabled(bool enabled)
        {
            Window.Instance.SemanticsEnabled = enabled;
            Invoke(() => Window.Instance.OnSemanticsEnabledChanged(), Window.Instance._onSemanticsEnabledChangedZone);
        }

        // ignore: unused_element
        private static void UpdateAccessibilityFeatures(int values)
        {
            AccessibilityFeatures newFeatures = new AccessibilityFeatures(values);
            if (newFeatures == Window.Instance.AccessibilityFeatures)
                return;
            Window.Instance.AccessibilityFeatures = newFeatures;
            Invoke(() => Window.Instance.OnAccessibilityFeaturesChanged(), Window.Instance._onAccessibilityFlagsChangedZone);
        }

        private static void DispatchPlatformMessage(String name, ByteData data, int responseId)
        {
            if (Window.Instance.OnPlatformMessage != null)
            {
                /*Invoke3<string, ByteData, PlatformMessageResponseCallback>(
                  Window.Instance.OnPlatformMessage,
                  Window.Instance._onPlatformMessageZone,
                  name,
                  data,
                  (ByteData responseData) {
                    Window.Instance.RespondToPlatformMessage(responseId, responseData);
                });*/
            }
            else
            {
                Window.Instance.RespondToPlatformMessage(responseId, null);
            }
        }

        // ignore: unused_element
        private static void DispatchPointerDataPacket(ByteData packet)
        {
            if (Window.Instance.OnPointerDataPacket != null)
                Invoke1<PointerDataPacket>((data) => Window.Instance.OnPointerDataPacket(data), Window.Instance._onPointerDataPacketZone, UnpackPointerDataPacket(packet));
        }

        // ignore: unused_element
        private static void DispatchSemanticsAction(int id, int action, ByteData args)
        {
            Invoke3((ida, semanticsAction, byteData) => Window.Instance.OnSemanticsAction(ida, semanticsAction, byteData),
              Window.Instance._onSemanticsActionZone,
              id,
              SemanticsAction.Values[action],
              args);
        }

        // ignore: unused_element
        private static void BeginFrame(int microseconds)
        {
            Invoke1<Duration>((duration) => Window.Instance.OnBeginFrame(duration), Window.Instance._onBeginFrameZone, new Duration(microseconds: microseconds));
        }

        // ignore: unused_element
        private static void ReportTimings(List<int> timings)
        {
            Debug.Assert(timings.Count % Enum.GetNames(typeof(FramePhase)).Length == 0);
            List<FrameTiming> frameTimings = new List<FrameTiming>();
            for (int i = 0; i < timings.Count; i += Enum.GetNames(typeof(FramePhase)).Length)
            {
                frameTimings.Add(new FrameTiming(timings.Sublist(i, i + Enum.GetNames(typeof(FramePhase)).Length)));
            }
            Invoke1((localFrameTimings) => Window.Instance.OnReportTimings(localFrameTimings), Window.Instance._onReportTimingsZone, frameTimings);
        }

        // ignore: unused_element
        private static void DrawFrame()
        {
            Invoke(() => Window.Instance.OnDrawFrame(), Window.Instance._onDrawFrameZone);
        }

        // ignore: always_declare_return_types, prefer_generic_function_type_aliases
        private delegate void _UnaryFunction(object args);

        // ignore: always_declare_return_types, prefer_generic_function_type_aliases
        private delegate void _BinaryFunction(object args, object message);

        // ignore: unused_element
        private static void RunMainZoned(Delegate startMainIsolateFunction, Delegate userMainFunction, List<string> args)
        {
            /* startMainIsolateFunction((){
                 runZoned<Future<void>>(() {
                     if (userMainFunction is _BinaryFunction)
                     {
                         // This seems to be undocumented but supported by the command line VM.
                         // Let's do the same in case old entry-points are ported to Flutter.
                         (userMainFunction as dynamic)(args, '');
                     }
                     else if (userMainFunction is _UnaryFunction)
                     {
                         (userMainFunction as dynamic)(args);
                     }
                     else
                     {
                         userMainFunction();
                     }
                 }, onError: (Object error, StackTrace stackTrace) {
                     _reportUnhandledException(error.toString(), stackTrace.toString());
                 });
             }, null);*/
        }

        private static void ReportUnhandledException(string error, string stackTrace)
        {
            // TODO : native 'Window_reportUnhandledException';
        }

        /// Invokes [callback] inside the given [zone].
        private static void Invoke(Action callback, Zone zone)
        {
            if (callback == null)
                return;

            Debug.Assert(zone != null);

            if (Identical(zone, Zone.Current))
            {
                callback();
            }
            else
            {
                zone.RunGuarded(callback);
            }
        }

        /// Invokes [callback] inside the given [zone] passing it [arg].
        private static void Invoke1<A>(Action<A> callback, Zone zone, A arg)
        {
            if (callback == null)
                return;

            Debug.Assert(zone != null);

            if (Identical(zone, Zone.Current))
            {
                callback(arg);
            }
            else
            {
                zone.RunUnaryGuarded<A>(callback, arg);
            }
        }

        /// Invokes [callback] inside the given [zone] passing it [arg1] and [arg2].
        // ignore: unused_element
        private static void Invoke2<A1, A2>(Action<A1, A2> callback, Zone zone, A1 arg1, A2 arg2)
        {
            if (callback == null)
                return;

            Debug.Assert(zone != null);

            if (Identical(zone, Zone.Current))
            {
                callback(arg1, arg2);
            }
            else
            {
                zone.RunBinaryGuarded<A1, A2>(callback, arg1, arg2);
            }
        }

        /// Invokes [callback] inside the given [zone] passing it [arg1], [arg2] and [arg3].
        private static void Invoke3<A1, A2, A3>(Action<A1, A2, A3> callback, Zone zone, A1 arg1, A2 arg2, A3 arg3)
        {
            if (callback == null)
                return;

            Debug.Assert(zone != null);

            if (Identical(zone, Zone.Current))
            {
                callback(arg1, arg2, arg3);
            }
            else
            {
                zone.RunGuarded(() => {
                    callback(arg1, arg2, arg3);
                });
            }
        }

        // If this value changes, update the encoding code in the following files:
        //
        //  * pointer_data.cc
        //  * FlutterView.java
        internal const int _kPointerDataFieldCount = 24;

        private static PointerDataPacket UnpackPointerDataPacket(ByteData packet)
        {
            const int kStride = Int64List.BytesPerElement;
            const int kBytesPerPointerData = _kPointerDataFieldCount * kStride;
            int length = (int)Math.Truncate(packet.LengthInBytes * 1.0 / kBytesPerPointerData);
            Debug.Assert(length * kBytesPerPointerData == packet.LengthInBytes);
            List<PointerData> data = new List<PointerData>(length);
            for (int i = 0; i < length; ++i)
            {
                int offset = i * _kPointerDataFieldCount;
                data[i] = new PointerData(
                  timeStamp: new Duration(microseconds: packet.GetInt64(kStride * offset++, _kFakeHostEndian)),
                  change: (PointerChange)packet.GetInt64(kStride * offset++, _kFakeHostEndian),
                  kind: (PointerDeviceKind)packet.GetInt64(kStride * offset++, _kFakeHostEndian),
                  signalKind: (PointerSignalKind)packet.GetInt64(kStride * offset++, _kFakeHostEndian),
                  device: packet.GetInt64(kStride * offset++, _kFakeHostEndian),
                  physicalX: packet.GetFloat64(kStride * offset++, _kFakeHostEndian),
                  physicalY: packet.GetFloat64(kStride * offset++, _kFakeHostEndian),
                  buttons: packet.GetInt64(kStride * offset++, _kFakeHostEndian),
                  obscured: packet.GetInt64(kStride * offset++, _kFakeHostEndian) != 0,
                  pressure: packet.GetFloat64(kStride * offset++, _kFakeHostEndian),
                  pressureMin: packet.GetFloat64(kStride * offset++, _kFakeHostEndian),
                  pressureMax: packet.GetFloat64(kStride * offset++, _kFakeHostEndian),
                  distance: packet.GetFloat64(kStride * offset++, _kFakeHostEndian),
                  distanceMax: packet.GetFloat64(kStride * offset++, _kFakeHostEndian),
                  size: packet.GetFloat64(kStride * offset++, _kFakeHostEndian),
                  radiusMajor: packet.GetFloat64(kStride * offset++, _kFakeHostEndian),
                  radiusMinor: packet.GetFloat64(kStride * offset++, _kFakeHostEndian),
                  radiusMin: packet.GetFloat64(kStride * offset++, _kFakeHostEndian),
                  radiusMax: packet.GetFloat64(kStride * offset++, _kFakeHostEndian),
                  orientation: packet.GetFloat64(kStride * offset++, _kFakeHostEndian),
                  tilt: packet.GetFloat64(kStride * offset++, _kFakeHostEndian),
                  platformData: packet.GetInt64(kStride * offset++, _kFakeHostEndian),
                  scrollDeltaX: packet.GetFloat64(kStride * offset++, _kFakeHostEndian),
                  scrollDeltaY: packet.GetFloat64(kStride * offset++, _kFakeHostEndian)
                );
                Debug.Assert(offset == (i + 1) * _kPointerDataFieldCount);
            }
            return new PointerDataPacket(data: data);
        }
    }
}
