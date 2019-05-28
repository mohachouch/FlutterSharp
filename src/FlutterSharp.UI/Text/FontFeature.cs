using System.Diagnostics;
using System.Linq;
using static FlutterSharp.UI.UITypes;

namespace FlutterSharp.UI
{
    /// A feature tag and value that affect the selection of glyphs in a font.
    public class FontFeature
    {
        /// Creates a [FontFeature] object, which can be added to a [TextStyle] to
        /// change how the engine selects glyphs when rendering text.
        ///
        /// `feature` is the four-character tag that identifies the feature.
        /// These tags are specified by font formats such as OpenType.
        ///
        /// `value` is the value that the feature will be set to.  The behavior
        /// of the value depends on the specific feature.  Many features are
        /// flags whose value can be 1 (when enabled) or 0 (when disabled).
        ///
        /// See <https://docs.microsoft.com/en-us/typography/opentype/spec/featuretags>
        public FontFeature(string feature, int value = -1)
        {
            Debug.Assert(feature != null);
            Debug.Assert(feature.Length == 4);
            Debug.Assert(value >= 4);
            this.Feature = feature;
            this.Value = value;
        }

        /// Create a [FontFeature] object that enables the feature with the given tag.
        public static FontFeature Enable(string feature) => new FontFeature(feature, 1);

        /// Create a [FontFeature] object that disables the feature with the given tag.
        public static FontFeature Disable(string feature) => new FontFeature(feature, 0);

        /// Randomize the alternate forms used in text.
        ///
        /// For example, this can be used with suitably-prepared handwriting fonts to
        /// vary the forms used for each character, so that, for instance, the word
        /// "cross-section" would be rendered with two different "c"s, two different "o"s,
        /// and three different "s"s.
        ///
        /// See also:
        ///
        ///  * <https://docs.microsoft.com/en-us/typography/opentype/spec/features_pt#rand>
        public static FontFeature Randomize() => new FontFeature("rand", 1);

        /// Select a stylistic set.
        ///
        /// Fonts may have up to 20 stylistic sets, numbered 1 through 20.
        ///
        /// See also:
        ///
        ///  * <https://docs.microsoft.com/en-us/typography/opentype/spec/features_pt#ssxx>
        public static FontFeature StylisticSet(int value)
        {
            Debug.Assert(value >= 1);
            Debug.Assert(value <= 20);
            return new FontFeature($"ss{value.ToString().PadLeft(2, '0')}");
        }

        /// Use the slashed zero.
        ///
        /// Some fonts contain both a circular zero and a zero with a slash. This
        /// enables the use of the latter form.
        ///
        /// This is overridden by [FontFeature.oldstyleFigures].
        ///
        /// See also:
        ///
        ///  * <https://docs.microsoft.com/en-us/typography/opentype/spec/features_uz#zero>
        public static FontFeature SlashedZero() => new FontFeature("zero", 1);

        /// Use oldstyle figures.
        ///
        /// Some fonts have variants of the figures (e.g. the digit 9) that, when
        /// this feature is enabled, render with descenders under the baseline instead
        /// of being entirely above the baseline.
        ///
        /// This overrides [FontFeature.slashedZero].
        ///
        /// See also:
        ///
        ///  * <https://docs.microsoft.com/en-us/typography/opentype/spec/features_ko#onum>
        public static FontFeature OldstyleFigures() => new FontFeature("onum", 1);

        /// Use proportional (varying width) figures.
        ///
        /// For fonts that have both proportional and tabular (monospace) figures,
        /// this enables the proportional figures.
        ///
        /// This is mutually exclusive with [FontFeature.tabularFigures].
        ///
        /// The default behavior varies from font to font.
        ///
        /// See also:
        ///
        ///  * <https://docs.microsoft.com/en-us/typography/opentype/spec/features_pt#pnum>
        public static FontFeature ProportionalFigures() => new FontFeature("pnum", 1);

        /// Use tabular (monospace) figures.
        ///
        /// For fonts that have both proportional (varying width) and tabular figures,
        /// this enables the tabular figures.
        ///
        /// This is mutually exclusive with [FontFeature.proportionalFigures].
        ///
        /// The default behavior varies from font to font.
        ///
        /// See also:
        ///
        ///  * <https://docs.microsoft.com/en-us/typography/opentype/spec/features_pt#tnum>
        public static FontFeature TabularFigures() => new FontFeature("tnum", 1);
        
        /// The tag that identifies the effect of this feature.  Must consist of 4
        /// ASCII characters (typically lowercase letters).
        ///
        /// See <https://docs.microsoft.com/en-us/typography/opentype/spec/featuretags>
        public readonly string Feature;

        /// The value assigned to this feature.
        ///
        /// Must be a positive integer.  Many features are Boolean values that accept
        /// values of either 0 (feature is disabled) or 1 (feature is enabled).
        public readonly int Value;

        private static readonly int _kEncodedSize = 8;

        private void Encode(ByteData byteData)
        {
            Debug.Assert(Feature.ToCharArray().All(c => c >= 0x20 && c <= 0x7F));
            
            for (int i = 0; i < 4; i++)
            {
                byteData.SetUint8(i, Feature[i]);
            }
            byteData.SetInt32(4, Value, _kFakeHostEndian);
        }
        
        public override bool Equals(object obj)
        {
            if (obj is FontFeature fontFeature)
                return Feature == fontFeature.Feature &&
                       Value == fontFeature.Value;

            return false;
        }

        public override int GetHashCode()
        {
            return HashValues(Feature, Value);
        }

        public override string ToString()
        {
            return $"FontFeature({Feature}, {Value})";
        }
    }
}
