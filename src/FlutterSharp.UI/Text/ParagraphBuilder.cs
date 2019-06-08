using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace FlutterSharp.UI
{
    /// Builds a [Paragraph] containing text with the given styling information.
    ///
    /// To set the paragraph's alignment, truncation, and ellipsizing behavior, pass
    /// an appropriately-configured [ParagraphStyle] object to the
    /// [new ParagraphBuilder] constructor.
    ///
    /// Then, call combinations of [pushStyle], [addText], and [pop] to add styled
    /// text to the object.
    ///
    /// Finally, call [build] to obtain the constructed [Paragraph] object. After
    /// this point, the builder is no longer usable.
    ///
    /// After constructing a [Paragraph], call [Paragraph.layout] on it and then
    /// paint it with [Canvas.drawParagraph].
    public class ParagraphBuilder : NativeFieldWrapperClass2
    {
        /// Creates a new [ParagraphBuilder] object, which is used to create a
        /// [Paragraph].
        public ParagraphBuilder(ParagraphStyle style)
        {
            _placeholderCount = 0;
            List<string> strutFontFamilies = null;
            if (style._strutStyle != null)
            {
                strutFontFamilies = new List<string>();
                if (style._strutStyle._fontFamily != null)
                    strutFontFamilies.Add(style._strutStyle._fontFamily);
                if (style._strutStyle._fontFamilyFallback != null)
                    strutFontFamilies.AddRange(style._strutStyle._fontFamilyFallback);
            }
            Constructor(
                style._encoded,
                style._strutStyle?._encoded,
                style._fontFamily,
                strutFontFamilies,
                style._fontSize,
                style._height,
                style._ellipsis,
                EncodeLocale(style._locale)
            );
        }

        private void Constructor(
            Int32List encoded,
            ByteData strutData,
            string fontFamily,
            List<string> strutFontFamily,
            double? fontSize,
            double? height,
            string ellipsis,
            string locale
        )
        {
            // TODO : native 'ParagraphBuilder_constructor';
        }

        /// The number of placeholders currently in the paragraph.
        public int PlaceholderCount => _placeholderCount;
        private int _placeholderCount;

        /// The scales of the placeholders in the paragraph.
        public List<double> PlaceholderScales => _placeholderScales;
        private List<double> _placeholderScales = new List<double>();

        /// Applies the given style to the added text until [pop] is called.
        ///
        /// See [pop] for details.
        public void PushStyle(TextStyle style)
        {
            var fullFontFamilies = new List<string>();
            if (style._fontFamily != null)
                fullFontFamilies.Add(style._fontFamily);
            if (style._fontFamilyFallback != null)
                fullFontFamilies.AddRange(style._fontFamilyFallback);

            ByteData encodedFontFeatures = null;
            if (style._fontFeatures != null)
            {
                encodedFontFeatures = new ByteData(style._fontFeatures.Count * FontFeature._kEncodedSize);
                int byteOffset = 0;
                foreach (var feature in style._fontFeatures)
                {
                    //feature.Encode(ByteData.view(encodedFontFeatures.buffer, byteOffset, FontFeature._kEncodedSize)); TODO : implement this
                    byteOffset += FontFeature._kEncodedSize;
                }
            }

            PushStyle(
                style._encoded,
                fullFontFamilies,
                style._fontSize,
                style._letterSpacing,
                style._wordSpacing,
                style._height,
                style._decorationThickness,
                EncodeLocale(style._locale),
                style._background?._objects,
                style._background?._data,
                style._foreground?._objects,
                style._foreground?._data,
                Shadow.EncodeShadows(style._shadows),
                encodedFontFeatures
            );
        }

        private void PushStyle(
            Int32List encoded,
            List<string> fontFamilies,
            double? fontSize,
            double? letterSpacing,
            double? wordSpacing,
            double? height,
            double? decorationThickness,
            string locale,
            List<dynamic> backgroundObjects,
            ByteData backgroundData,
            List<dynamic> foregroundObjects,
            ByteData foregroundData,
            ByteData shadowsData,
            ByteData fontFeaturesData)
        {
            // TODO : native 'ParagraphBuilder_pushStyle';
        }

        private static string EncodeLocale(Locale locale) => locale?.ToString() ?? "";

        /// Ends the effect of the most recent call to [pushStyle].
        ///
        /// Internally, the paragraph builder maintains a stack of text styles. Text
        /// added to the paragraph is affected by all the styles in the stack. Calling
        /// [pop] removes the topmost style in the stack, leaving the remaining styles
        /// in effect.
        public void Pop()
        {
            // TODO : native 'ParagraphBuilder_pop';
        }

        /// Adds the given text to the paragraph.
        ///
        /// The text will be styled according to the current stack of text styles.
        public void AddText(string text)
        {
            string error = _addText(text);
            if (error != null)
                throw new ArgumentException(error);
        }

        private string _addText(string text)
        {
            // TODO : native 'ParagraphBuilder_addText';
            return null;
        }

        /// Adds an inline placeholder space to the paragraph.
        ///
        /// The paragraph will contain a rectangular space with no text of the dimensions
        /// specified.
        ///
        /// The `width` and `height` parameters specify the size of the placeholder rectangle.
        ///
        /// The `alignment` parameter specifies how the placeholder rectangle will be vertically
        /// aligned with the surrounding text. When [PlaceholderAlignment.baseline],
        /// [PlaceholderAlignment.aboveBaseline], and [PlaceholderAlignment.belowBaseline]
        /// alignment modes are used, the baseline needs to be set with the `baseline`.
        /// When using [PlaceholderAlignment.baseline], `baselineOffset` indicates the distance
        /// of the baseline down from the top of of the rectangle. The default `baselineOffset`
        /// is the `height`.
        ///
        /// Examples:
        ///
        /// * For a 30x50 placeholder with the bottom edge aligned with the bottom of the text, use:
        /// `addPlaceholder(30, 50, PlaceholderAlignment.bottom);`
        /// * For a 30x50 placeholder that is vertically centered around the text, use:
        /// `addPlaceholder(30, 50, PlaceholderAlignment.middle);`.
        /// * For a 30x50 placeholder that sits completely on top of the alphabetic baseline, use:
        /// `addPlaceholder(30, 50, PlaceholderAlignment.aboveBaseline, baseline: TextBaseline.alphabetic)`.
        /// * For a 30x50 placeholder with 40 pixels above and 10 pixels below the alphabetic baseline, use:
        /// `addPlaceholder(30, 50, PlaceholderAlignment.baseline, baseline: TextBaseline.alphabetic, baselineOffset: 40)`.
        ///
        /// Lines are permitted to break around each placeholder.
        ///
        /// Decorations will be drawn based on the font defined in the most recently
        /// pushed [TextStyle]. The decorations are drawn as if unicode text were present
        /// in the placeholder space, and will draw the same regardless of the height and
        /// alignment of the placeholder. To hide or manually adjust decorations to fit,
        /// a text style with the desired decoration behavior should be pushed before
        /// adding a placeholder.
        ///
        /// Any decorations drawn through a placeholder will exist on the same canvas/layer
        /// as the text. This means any content drawn on top of the space reserved by
        /// the placeholder will be drawn over the decoration, possibly obscuring the
        /// decoration.
        ///
        /// Placeholders are represented by a unicode 0xFFFC "object replacement character"
        /// in the text buffer. For each placeholder, one object replacement character is
        /// added on to the text buffer.
        ///
        /// The `scale` parameter will scale the `width` and `height` by the specified amount,
        /// and keep track of the scale. The scales of placeholders added can be accessed
        /// through [placeholderScales]. This is primarily used for acessibility scaling.
        public void AddPlaceholder(double width, double height, PlaceholderAlignment alignment,
            double scale = 1.0, double? baselineOffset = null, TextBaseline? baseline = null) {
            // Require a baseline to be specified if using a baseline-based alignment.
            Debug.Assert((alignment == PlaceholderAlignment.AboveBaseline ||
                    alignment == PlaceholderAlignment.BelowBaseline ||
                    alignment == PlaceholderAlignment.Baseline) ? baseline != null : true);
            // Default the baselineOffset to height if null. This will place the placeholder
            // fully above the baseline, similar to [PlaceholderAlignment.aboveBaseline].
            baselineOffset = baselineOffset ?? height;
            _addPlaceholder(width * scale, height * scale, (int)alignment, (baselineOffset == null ? height : baselineOffset.Value) * scale, baseline == null ? (int?)null : (int)baseline.Value);
            _placeholderCount++;
            _placeholderScales.Add(scale);
        }

        private string _addPlaceholder(double width, double height, int alignment, double baselineOffset, int? baseline) {
            // TODO : native 'ParagraphBuilder_addPlaceholder';
            return null;
        }
        
        /// Applies the given paragraph style and returns a [Paragraph] containing the
        /// added text and associated styling.
        ///
        /// After calling this function, the paragraph builder object is invalid and
        /// cannot be used further.
        public Paragraph Build()
        {
            // TODO : native 'ParagraphBuilder_build';
            return null;
        }
    }
}