using System.Collections.Generic;
using static FlutterSharp.UI.UITypes;

namespace FlutterSharp.UI
{
    /// See also:
    ///
    ///  * [StrutStyle](https://api.flutter.dev/flutter/painting/StrutStyle-class.html), the class in the [painting] library.
    ///
    public class StrutStyle
    {
        /// Creates a new StrutStyle object.
        ///
        /// * `fontFamily`: The name of the font to use when painting the text (e.g.,
        ///   Roboto).
        ///
        /// * `fontFamilyFallback`: An ordered list of font family names that will be searched for when
        ///    the font in `fontFamily` cannot be found.
        ///
        /// * `fontSize`: The size of glyphs (in logical pixels) to use when painting
        ///   the text.
        ///
        /// * `height`: The minimum height of the line boxes, as a multiplier of the
        ///   font size. The lines of the paragraph will be at least `(height + leading)
        ///   * fontSize` tall when fontSize is not null. When fontSize is null, there
        ///   is no minimum line height. Tall glyphs due to baseline alignment or large
        ///   [TextStyle.fontSize] may cause the actual line height after layout to be
        ///   taller than specified here. [fontSize] must be provided for this property
        ///   to take effect.
        ///
        /// * `leading`: The minimum amount of leading between lines as a multiple of
        ///   the font size. [fontSize] must be provided for this property to take effect.
        ///
        /// * `fontWeight`: The typeface thickness to use when painting the text
        ///   (e.g., bold).
        ///
        /// * `fontStyle`: The typeface variant to use when drawing the letters (e.g.,
        ///   italics).
        ///
        /// * `forceStrutHeight`: When true, the paragraph will force all lines to be exactly
        ///   `(height + leading) * fontSize` tall from baseline to baseline.
        ///   [TextStyle] is no longer able to influence the line height, and any tall
        ///   glyphs may overlap with lines above. If a [fontFamily] is specified, the
        ///   total ascent of the first line will be the min of the `Ascent + half-leading`
        ///   of the [fontFamily] and `(height + leading) * fontSize`. Otherwise, it
        ///   will be determined by the Ascent + half-leading of the first text.
        public StrutStyle(
            string fontFamily = null,
            List<string> fontFamilyFallback = null,
            double? fontSize = null,
            double? height = null,
            double? leading = null,
            FontWeight fontWeight = null,
            FontStyle? fontStyle = null,
            bool? forceStrutHeight = null)
        {
            _encoded = EncodeStrut(
                fontFamily,
                fontFamilyFallback,
                fontSize,
                height,
                leading,
                fontWeight,
                fontStyle,
                forceStrutHeight
            );

            _fontFamily = fontFamily;
            _fontFamilyFallback = fontFamilyFallback;
        }

        internal readonly ByteData _encoded; // Most of the data for strut is encoded.
        internal readonly string _fontFamily;
        internal readonly List<string> _fontFamilyFallback;

        public override bool Equals(object obj)
        {
            // TODO : implement this when ByteData is implemented
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            // TODO : implement this when ByteData is implemented
            return base.GetHashCode();
        }
    }
}