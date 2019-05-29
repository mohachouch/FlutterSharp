using System;
using System.Collections.Generic;

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
                Shadow._encodeShadows(style._shadows),
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