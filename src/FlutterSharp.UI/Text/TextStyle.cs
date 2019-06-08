using System.Collections.Generic;
using System.Diagnostics;
using static FlutterSharp.UI.UITypes;
using static FlutterSharp.UI.TextMethods;

namespace FlutterSharp.UI
{
    /// An opaque object that determines the size, position, and rendering of text.
    ///
    /// See also:
    ///
    ///  * [TextStyle](https://api.flutter.dev/flutter/painting/TextStyle-class.html), the class in the [painting] library.
    ///
    public class TextStyle
    {
        /// Creates a new TextStyle object.
        ///
        /// * `color`: The color to use when painting the text. If this is specified, `foreground` must be null.
        /// * `decoration`: The decorations to paint near the text (e.g., an underline).
        /// * `decorationColor`: The color in which to paint the text decorations.
        /// * `decorationStyle`: The style in which to paint the text decorations (e.g., dashed).
        /// * `decorationThickness`: The thickness of the decoration as a muliplier on the thickness specified by the font.
        /// * `fontWeight`: The typeface thickness to use when painting the text (e.g., bold).
        /// * `fontStyle`: The typeface variant to use when drawing the letters (e.g., italics).
        /// * `fontFamily`: The name of the font to use when painting the text (e.g., Roboto). If a `fontFamilyFallback` is
        ///   provided and `fontFamily` is not, then the first font family in `fontFamilyFallback` will take the position of
        ///   the preferred font family. When a higher priority font cannot be found or does not contain a glyph, a lower
        ///   priority font will be used.
        /// * `fontFamilyFallback`: An ordered list of the names of the fonts to fallback on when a glyph cannot
        ///   be found in a higher priority font. When the `fontFamily` is null, the first font family in this list
        ///   is used as the preferred font. Internally, the 'fontFamily` is concatenated to the front of this list.
        ///   When no font family is provided through 'fontFamilyFallback' (null or empty) or `fontFamily`, then the
        ///   platform default font will be used.
        /// * `fontSize`: The size of glyphs (in logical pixels) to use when painting the text.
        /// * `letterSpacing`: The amount of space (in logical pixels) to add between each letter.
        /// * `wordSpacing`: The amount of space (in logical pixels) to add at each sequence of white-space (i.e. between each word).
        /// * `textBaseline`: The common baseline that should be aligned between this text span and its parent text span, or, for the root text spans, with the line box.
        /// * `height`: The height of this text span, as a multiplier of the font size.
        /// * `locale`: The locale used to select region-specific glyphs.
        /// * `background`: The paint drawn as a background for the text.
        /// * `foreground`: The paint used to draw the text. If this is specified, `color` must be null.
        /// * `fontFeatures`: The font features that should be applied to the text.
        public TextStyle(
            Color color = null,
            TextDecoration decoration = null,
            Color decorationColor = null,
            TextDecorationStyle? decorationStyle = null,
            double? decorationThickness = null,
            FontWeight fontWeight = null,
            FontStyle? fontStyle = null,
            TextBaseline? textBaseline = null,
            string fontFamily = null,
            List<string> fontFamilyFallback = null,
            double? fontSize = null,
            double? letterSpacing = null,
            double? wordSpacing = null,
            double? height = null,
            Locale locale = null,
            Paint background = null,
            Paint foreground = null,
            List<Shadow> shadows = null,
            List<FontFeature> fontFeatures = null)
        {
            Debug.Assert(color == null || foreground == null,
                "Cannot provide both a color and a foreground\n" +
                "The color argument is just a shorthand for 'foreground: Paint()..color = color'.");

            _encoded = EncodeTextStyle(
                color,
                decoration,
                decorationColor,
                decorationStyle,
                decorationThickness,
                fontWeight,
                fontStyle,
                textBaseline,
                fontFamily,
                fontFamilyFallback,
                fontSize,
                letterSpacing,
                wordSpacing,
                height,
                locale,
                background,
                foreground,
                shadows,
                fontFeatures);

            _fontFamily = fontFamily ?? "";
            _fontFamilyFallback = fontFamilyFallback;
            _fontSize = fontSize;
            _letterSpacing = letterSpacing;
            _wordSpacing = wordSpacing;
            _height = height;
            _decorationThickness = decorationThickness;
            _locale = locale;
            _background = background;
            _foreground = foreground;
            _shadows = shadows;
            _fontFeatures = fontFeatures;
        }

        internal readonly Int32List _encoded;
        internal readonly string _fontFamily;
        internal readonly List<string> _fontFamilyFallback;
        internal readonly double? _fontSize;
        internal readonly double? _letterSpacing;
        internal readonly double? _wordSpacing;
        internal readonly double? _height;
        internal readonly double? _decorationThickness;
        internal readonly Locale _locale;
        internal readonly Paint _background;
        internal readonly Paint _foreground;
        internal readonly List<Shadow> _shadows;
        internal readonly List<FontFeature> _fontFeatures;

        public override bool Equals(object other)
        {
            if (Identical(this, other))
                return true;
            if (!(other is TextStyle))
                return false;

            TextStyle typedOther = other as TextStyle;

            if (_fontFamily != typedOther._fontFamily ||
                _fontSize != typedOther._fontSize ||
                _letterSpacing != typedOther._letterSpacing ||
                _wordSpacing != typedOther._wordSpacing ||
                _height != typedOther._height ||
                _decorationThickness != typedOther._decorationThickness ||
                _locale != typedOther._locale ||
                _background != typedOther._background ||
                _foreground != typedOther._foreground)
                return false;

            for (int index = 0; index < _encoded.Count; index++)
            {
                if (_encoded[index] != typedOther._encoded[index])
                    return false;
            }

            if (!ListEquals<Shadow>(_shadows, typedOther._shadows))
                return false;
            if (!ListEquals<string>(_fontFamilyFallback, typedOther._fontFamilyFallback))
                return false;
            if (!ListEquals<FontFeature>(_fontFeatures, typedOther._fontFeatures))
                return false;
            return true;
        }

        public override int GetHashCode()
        {
            return HashValues(HashList(_encoded), _fontFamily, _fontFamilyFallback, _fontSize, _letterSpacing, _wordSpacing, _height, _locale, _background, _foreground, HashList(_shadows), _decorationThickness, HashList(_fontFeatures));
        }

        public override string ToString()
        {
            // TODO : implement this
            return "TextStyle()";
        }
    }
}