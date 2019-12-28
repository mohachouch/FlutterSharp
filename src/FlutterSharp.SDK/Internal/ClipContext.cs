using System;
using FlutterSharp.UI;

namespace FlutterSharp.SDK.Internal
{
    public class ClipContext
    {
        /// The canvas on which to paint.
        protected Canvas _canvas;

        private void _clipAndPaint(Action<bool> canvasClipCall, Clip clipBehavior, Rect bounds, Action painter)
        {
            _canvas.Save();
            switch (clipBehavior)
            {
                case Clip.None:
                    break;
                case Clip.HardEdge:
                    canvasClipCall(false);
                    break;
                case Clip.AntiAlias:
                    canvasClipCall(true);
                    break;
                case Clip.AntiAliasWithSaveLayer:
                    canvasClipCall(true);
                    _canvas.SaveLayer(bounds, new Paint());
                    break;
            }
            painter();
            if (clipBehavior == Clip.AntiAliasWithSaveLayer)
            {
                _canvas.Restore();
            }
            _canvas.Restore();
        }

        /// Clip [canvas] with [Path] according to [Clip] and then paint. [canvas] is
        /// restored to the pre-clip status afterwards.
        ///
        /// `bounds` is the saveLayer bounds used for [Clip.antiAliasWithSaveLayer].
        public void ClipPathAndPaint(Path path, Clip clipBehavior, Rect bounds, Action painter)
        {
            _clipAndPaint((bool doAntiAias) => _canvas.ClipPath(path, doAntiAlias: doAntiAias), clipBehavior, bounds, painter);
        }

        /// Clip [canvas] with [Path] according to [RRect] and then paint. [canvas] is
        /// restored to the pre-clip status afterwards.
        ///
        /// `bounds` is the saveLayer bounds used for [Clip.antiAliasWithSaveLayer].
        public void ClipRRectAndPaint(RRect rrect, Clip clipBehavior, Rect bounds, Action painter)
        {
            _clipAndPaint((bool doAntiAias) => _canvas.ClipRRect(rrect, doAntiAlias: doAntiAias), clipBehavior, bounds, painter);
        }

        /// Clip [canvas] with [Path] according to [Rect] and then paint. [canvas] is
        /// restored to the pre-clip status afterwards.
        ///
        /// `bounds` is the saveLayer bounds used for [Clip.antiAliasWithSaveLayer].
        public void ClipRectAndPaint(Rect rect, Clip clipBehavior, Rect bounds, Action painter)
        {
            _clipAndPaint((bool doAntiAias) => _canvas.ClipRect(rect, doAntiAlias: doAntiAias), clipBehavior, bounds, painter);
        }
    }
}
