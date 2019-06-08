using static FlutterSharp.UI.UITypes;
using static FlutterSharp.UI.TextMethods;

namespace FlutterSharp.UI
{
    /// An opaque object that determines the configuration used by
    /// [ParagraphBuilder] to position lines within a [Paragraph] of text.
    public class ParagraphStyle
    {
        /// Creates a new ParagraphStyle object.
        ///
        /// * `textAlign`: The alignment of the text within the lines of the
        ///   paragraph. If the last line is ellipsized (see `ellipsis` below), the
        ///   alignment is applied to that line after it has been truncated but before
        ///   the ellipsis has been added.
        //   See: https://github.com/flutter/flutter/issues/9819
        ///
        /// * `textDirection`: The directionality of the text, left-to-right (e.g.
        ///   Norwegian) or right-to-left (e.g. Hebrew). This controls the overall
        ///   directionality of the paragraph, as well as the meaning of
        ///   [TextAlign.start] and [TextAlign.end] in the `textAlign` field.
        ///
        /// * `maxLines`: The maximum number of lines painted. Lines beyond this
        ///   number are silently dropped. For example, if `maxLines` is 1, then only
        ///   one line is rendered. If `maxLines` is null, but `ellipsis` is not null,
        ///   then lines after the first one that overflows the width constraints are
        ///   dropped. The width constraints are those set in the
        ///   [ParagraphConstraints] object passed to the [Paragraph.layout] method.
        ///
        /// * `fontFamily`: The name of the font family to apply when painting the text,
        ///   in the absence of a `textStyle` being attached to the span.
        ///
        /// * `fontSize`: The fallback size of glyphs (in logical pixels) to
        ///   use when painting the text. This is used when there is no [TextStyle].
        ///
        /// * `height`: The height of the spans as a multiplier of the font size. The
        ///   fallback height to use when no height is provided in through
        ///   [TextStyle.height].
        ///
        /// * `fontWeight`: The typeface thickness to use when painting the text
        ///   (e.g., bold).
        ///
        /// * `fontStyle`: The typeface variant to use when drawing the letters (e.g.,
        ///   italics).
        ///
        /// * `strutStyle`: The properties of the strut. Strut defines a set of minimum
        ///   vertical line height related metrics and can be used to obtain more
        ///   advanced line spacing behavior.
        ///
        /// * `ellipsis`: String used to ellipsize overflowing text. If `maxLines` is
        ///   not null, then the `ellipsis`, if any, is applied to the last rendered
        ///   line, if that line overflows the width constraints. If `maxLines` is
        ///   null, then the `ellipsis` is applied to the first line that overflows
        ///   the width constraints, and subsequent lines are dropped. The width
        ///   constraints are those set in the [ParagraphConstraints] object passed to
        ///   the [Paragraph.layout] method. The empty string and the null value are
        ///   considered equivalent and turn off this behavior.
        ///
        /// * `locale`: The locale used to select region-specific glyphs.
        public ParagraphStyle(
            TextAlign? textAlign = null,
            TextDirection? textDirection = null,
            int? maxLines = null,
            string fontFamily = null,
            double? fontSize = null,
            double? height = null,
            FontWeight fontWeight = null,
            FontStyle? fontStyle = null,
            StrutStyle strutStyle = null,
            string ellipsis = null,
            Locale locale = null)
        {
            _encoded = EncodeParagraphStyle(
                textAlign,
                textDirection,
                maxLines,
                fontFamily,
                fontSize,
                height,
                fontWeight,
                fontStyle,
                strutStyle,
                ellipsis,
                locale
            );

            _fontFamily = fontFamily;
            _fontSize = fontSize;
            _height = height;
            _strutStyle = strutStyle;
            _ellipsis = ellipsis;
            _locale = locale;
        }

        internal readonly Int32List _encoded;
        internal readonly string _fontFamily;
        internal readonly double? _fontSize;
        internal readonly double? _height;
        internal readonly StrutStyle _strutStyle;
        internal readonly string _ellipsis;
        internal readonly Locale _locale;

        public override bool Equals(object other)
        {
            if (Identical(this, other))
                return true;
            if (!(other is ParagraphStyle))
                return false;

            ParagraphStyle typedOther = other as ParagraphStyle;

            if (_fontFamily != typedOther._fontFamily ||
                _fontSize != typedOther._fontSize ||
                _height != typedOther._height ||
                _strutStyle != typedOther._strutStyle ||
                _ellipsis != typedOther._ellipsis ||
                _locale != typedOther._locale)
                return false;

            for (int index = 0; index < _encoded.Count; index++)
            {
                if (_encoded[index] != typedOther._encoded[index])
                    return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            return HashValues(HashList(_encoded), _fontFamily, _fontSize, _height, _ellipsis, _locale);
        }

        public override string ToString()
        {
            // TODO : implement this
            return "ParagraphStyle()";
        }
    }
}