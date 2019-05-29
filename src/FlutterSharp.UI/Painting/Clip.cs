namespace FlutterSharp.UI
{
    /// Different ways to clip a widget's content.
    public enum Clip
    {
        /// No clip at all.
        ///
        /// This is the default option for most widgets: if the content does not
        /// overflow the widget boundary, don't pay any performance cost for clipping.
        ///
        /// If the content does overflow, please explicitly specify the following
        /// [Clip] options:
        ///  * [hardEdge], which is the fastest clipping, but with lower fidelity.
        ///  * [antiAlias], which is a little slower than [hardEdge], but with smoothed edges.
        ///  * [antiAliasWithSaveLayer], which is much slower than [antiAlias], and should
        ///    rarely be used.
        None,

        /// Clip, but do not apply anti-aliasing.
        ///
        /// This mode enables clipping, but curves and non-axis-aligned straight lines will be
        /// jagged as no effort is made to anti-alias.
        ///
        /// Faster than other clipping modes, but slower than [none].
        ///
        /// This is a reasonable choice when clipping is needed, if the container is an axis-
        /// aligned rectangle or an axis-aligned rounded rectangle with very small corner radii.
        ///
        /// See also:
        ///
        ///  * [antiAlias], which is more reasonable when clipping is needed and the shape is not
        ///    an axis-aligned rectangle.
        HardEdge,

        /// Clip with anti-aliasing.
        ///
        /// This mode has anti-aliased clipping edges to achieve a smoother look.
        ///
        /// It' s much faster than [antiAliasWithSaveLayer], but slower than [hardEdge].
        ///
        /// This will be the common case when dealing with circles and arcs.
        ///
        /// Different from [hardEdge] and [antiAliasWithSaveLayer], this clipping may have
        /// bleeding edge artifacts.
        /// (See https://fiddle.skia.org/c/21cb4c2b2515996b537f36e7819288ae for an example.)
        ///
        /// See also:
        ///
        ///  * [hardEdge], which is a little faster, but with lower fidelity.
        ///  * [antiAliasWithSaveLayer], which is much slower, but can avoid the
        ///    bleeding edges if there's no other way.
        ///  * [Paint.isAntiAlias], which is the anti-aliasing switch for general draw operations.
        AntiAlias,

        /// Clip with anti-aliasing and saveLayer immediately following the clip.
        ///
        /// This mode not only clips with anti-aliasing, but also allocates an offscreen
        /// buffer. All subsequent paints are carried out on that buffer before finally
        /// being clipped and composited back.
        ///
        /// This is very slow. It has no bleeding edge artifacts (that [antiAlias] has)
        /// but it changes the semantics as an offscreen buffer is now introduced.
        /// (See https://github.com/flutter/flutter/issues/18057#issuecomment-394197336
        /// for a difference between paint without saveLayer and paint with saveLayer.)
        ///
        /// This will be only rarely needed. One case where you might need this is if
        /// you have an image overlaid on a very different background color. In these
        /// cases, consider whether you can avoid overlaying multiple colors in one
        /// spot (e.g. by having the background color only present where the image is
        /// absent). If you can, [antiAlias] would be fine and much faster.
        ///
        /// See also:
        ///
        ///  * [antiAlias], which is much faster, and has similar clipping results.
        AntiAliasWithSaveLayer,
    }
}
