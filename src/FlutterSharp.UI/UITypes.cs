using FlutterSharp.UI.Types;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FlutterSharp.UI
{
    public static class UITypes
    {
        // If we actually run on big endian machines, we'll need to do something smarter
        // here. We don't use [Endian.Host] because it's not a compile-time
        // constant and can't propagate into the set/get calls.
        public static Endian _kFakeHostEndian = Endian.Little;

        public static int HashValues(params object[] values)
        {
            return values.Sum(x => x.GetHashCode());
        }

        public static int HashList(List<double> values)
        {
            return values.Sum(x => x.GetHashCode());
        }

        public static int HashList(List<int> values)
        {
            return values.Sum(x => x.GetHashCode());
        }

        public static int HashList<T>(List<T> values)
        {
            return values.Sum(x => x.GetHashCode());
        }

        public static bool Identical(object first, object second) => first != null && first.Equals(second);

        public static bool IsFinite(this double value) => !double.IsInfinity(value);

        public static bool IsNaN(this double value) => double.IsNaN(value);

        public static string ToStringAsFixed(this double value, int points)
        {
            return value.ToString($"N{points}");
        }

        public static double Round(this double value) => Math.Round(value);

        public static int ToInt(this double value) => (int)value;

        public static string ToRadixString(this int value, int places) => value.ToString(); // TODO: implement this

        public static bool IsEmpty(this string value) => string.IsNullOrWhiteSpace(value);

        public static int Clamp(this int value, int lower, int upper)
        {
            if (value > upper)
                return upper;

            if (value < lower)
                return lower;

            return value;
        }

        public static List<T> Sublist<T>(this List<T> list, int start, int? end = null)
        {
            if (end == null)
                end = list.Count - 1;

            return list.GetRange(start, end.Value - start);
        }

        public static double Clamp(this double value, int lower, int upper)
        {
            if (value > upper)
                return upper;

            if (value < lower)
                return lower;

            return value;
        }

        /* Static method in text.dart */

        /// Determines if lists [a] and [b] are deep equivalent.
        ///
        /// Returns true if the lists are both null, or if they are both non-null, have
        /// the same length, and contain the same elements in the same order. Returns
        /// false otherwise.
        public static bool ListEquals<T>(List<T> a, List<T> b)
        {
            if (a == null)
                return b == null;
            if (b == null || a.Count != b.Count)
                return false;
            for (int index = 0; index < a.Count; index += 1)
            {
                if (!a[index].Equals(b[index]))
                    return false;
            }
            return true;
        }

        // This encoding must match the C++ version of ParagraphBuilder::pushStyle.
        //
        // The encoded array buffer has 8 elements.
        //
        //  - Element 0: A bit field where the ith bit indicates whether the ith element
        //    has a non-null value. Bits 8 to 12 indicate whether |fontFamily|,
        //    |fontSize|, |letterSpacing|, |wordSpacing|, and |height| are non-null,
        //    respectively. Bit 0 is unused.
        //
        //  - Element 1: The |color| in ARGB with 8 bits per channel.
        //
        //  - Element 2: A bit field indicating which text decorations are present in
        //    the |textDecoration| list. The ith bit is set if there's a TextDecoration
        //    with enum index i in the list.
        //
        //  - Element 3: The |decorationColor| in ARGB with 8 bits per channel.
        //
        //  - Element 4: The bit field of the |decorationStyle|.
        //
        //  - Element 5: The index of the |fontWeight|.
        //
        //  - Element 6: The enum index of the |fontStyle|.
        //
        //  - Element 7: The enum index of the |textBaseline|.
        //
        public static Int32List EncodeTextStyle(
          Color color,
          TextDecoration decoration,
          Color decorationColor,
          TextDecorationStyle? decorationStyle,
          double? decorationThickness,
          FontWeight fontWeight,
          FontStyle? fontStyle,
          TextBaseline? textBaseline,
          string fontFamily,
          List<string> fontFamilyFallback,
          double? fontSize,
          double? letterSpacing,
          double? wordSpacing,
          double? height,
          Locale locale,
          Paint background,
          Paint foreground,
          List<Shadow> shadows,
          List<FontFeature> fontFeatures)
        {
            Int32List result = new Int32List(8);
            if (color != null)
            {
                result[0] |= 1 << 1;
                result[1] = color.Value;
            }
            if (decoration != null)
            {
                result[0] |= 1 << 2;
                result[2] = decoration.Mask;
            }
            if (decorationColor != null)
            {
                result[0] |= 1 << 3;
                result[3] = decorationColor.Value;
            }
            if (decorationStyle != null)
            {
                result[0] |= 1 << 4;
                result[4] = (int)decorationStyle;
            }
            if (fontWeight != null)
            {
                result[0] |= 1 << 5;
                result[5] = fontWeight.Index;
            }
            if (fontStyle != null)
            {
                result[0] |= 1 << 6;
                result[6] = (int)fontStyle;
            }
            if (textBaseline != null)
            {
                result[0] |= 1 << 7;
                result[7] = (int)textBaseline;
            }
            if (decorationThickness != null)
            {
                result[0] |= 1 << 8;
            }
            if (fontFamily != null || (fontFamilyFallback != null && fontFamilyFallback.Count > 0))
            {
                result[0] |= 1 << 9;
                // Passed separately to native.
            }
            if (fontSize != null)
            {
                result[0] |= 1 << 10;
                // Passed separately to native.
            }
            if (letterSpacing != null)
            {
                result[0] |= 1 << 11;
                // Passed separately to native.
            }
            if (wordSpacing != null)
            {
                result[0] |= 1 << 12;
                // Passed separately to native.
            }
            if (height != null)
            {
                result[0] |= 1 << 13;
                // Passed separately to native.
            }
            if (locale != null)
            {
                result[0] |= 1 << 14;
                // Passed separately to native.
            }
            if (background != null)
            {
                result[0] |= 1 << 15;
                // Passed separately to native.
            }
            if (foreground != null)
            {
                result[0] |= 1 << 16;
                // Passed separately to native.
            }
            if (shadows != null)
            {
                result[0] |= 1 << 17;
                // Passed separately to native.
            }
            if (fontFeatures != null)
            {
                result[0] |= 1 << 18;
                // Passed separately to native.
            }
            return result;
        }

        // This encoding must match the C++ version ParagraphBuilder::build.
        //
        // The encoded array buffer has 6 elements.
        //
        //  - Element 0: A bit mask indicating which fields are non-null.
        //    Bit 0 is unused. Bits 1-n are set if the corresponding index in the
        //    encoded array is non-null.  The remaining bits represent fields that
        //    are passed separately from the array.
        //
        //  - Element 1: The enum index of the |textAlign|.
        //
        //  - Element 2: The enum index of the |textDirection|.
        //
        //  - Element 3: The index of the |fontWeight|.
        //
        //  - Element 4: The enum index of the |fontStyle|.
        //
        //  - Element 5: The value of |maxLines|.
        //
        public static Int32List EncodeParagraphStyle(
          TextAlign? textAlign,
          TextDirection? textDirection,
          int? maxLines,
          string fontFamily,
          double? fontSize,
          double? height,
          FontWeight fontWeight,
          FontStyle? fontStyle,
          StrutStyle strutStyle,
          string ellipsis,
          Locale locale)
        {
            Int32List result = new Int32List(6); // also update paragraph_builder.cc
            if (textAlign != null)
            {
                result[0] |= 1 << 1;
                result[1] = (int)textAlign;
            }
            if (textDirection != null)
            {
                result[0] |= 1 << 2;
                result[2] = (int)textDirection;
            }
            if (fontWeight != null)
            {
                result[0] |= 1 << 3;
                result[3] = fontWeight.Index;
            }
            if (fontStyle != null)
            {
                result[0] |= 1 << 4;
                result[4] = (int)fontStyle;
            }
            if (maxLines != null)
            {
                result[0] |= 1 << 5;
                result[5] = maxLines.Value;
            }
            if (fontFamily != null)
            {
                result[0] |= 1 << 6;
                // Passed separately to native.
            }
            if (fontSize != null)
            {
                result[0] |= 1 << 7;
                // Passed separately to native.
            }
            if (height != null)
            {
                result[0] |= 1 << 8;
                // Passed separately to native.
            }
            if (strutStyle != null)
            {
                result[0] |= 1 << 9;
                // Passed separately to native.
            }
            if (ellipsis != null)
            {
                result[0] |= 1 << 10;
                // Passed separately to native.
            }
            if (locale != null)
            {
                result[0] |= 1 << 11;
                // Passed separately to native.
            }
            return result;
        }

        // Serialize strut properties into ByteData. This encoding errs towards
        // compactness. The first 8 bits is a bitmask that records which properties
        // are null. The rest of the values are encoded in the same order encountered
        // in the bitmask. The final returned value truncates any unused bytes
        // at the end.
        //
        // We serialize this more thoroughly than ParagraphStyle because it is
        // much more likely that the strut is empty/null and we wish to add
        // minimal overhead for non-strut cases.
        public static ByteData EncodeStrut(
          string fontFamily,
          List<string> fontFamilyFallback,
          double? fontSize,
          double? height,
          double? leading,
          FontWeight fontWeight,
          FontStyle? fontStyle,
          bool? forceStrutHeight)
        {
            if (fontFamily == null &&
              fontSize == null &&
              height == null &&
              leading == null &&
              fontWeight == null &&
              fontStyle == null &&
              forceStrutHeight == null)
                return new ByteData(0);

            ByteData data = new ByteData(15); // Max size is 15 bytes
            int bitmask = 0; // 8 bit mask
            int byteCount = 1;
            if (fontWeight != null)
            {
                bitmask |= 1 << 0;
                data.SetInt8(byteCount, fontWeight.Index);
                byteCount += 1;
            }
            if (fontStyle != null)
            {
                bitmask |= 1 << 1;
                data.SetInt8(byteCount, (int)fontStyle);
                byteCount += 1;
            }
            if (fontFamily != null || (fontFamilyFallback != null && fontFamilyFallback.Count > 0))
            {
                bitmask |= 1 << 2;
                // passed separately to native
            }
            if (fontSize != null)
            {
                bitmask |= 1 << 3;
                data.SetFloat32(byteCount, fontSize.Value, _kFakeHostEndian);
                byteCount += 4;
            }
            if (height != null)
            {
                bitmask |= 1 << 4;
                data.SetFloat32(byteCount, height.Value, _kFakeHostEndian);
                byteCount += 4;
            }
            if (leading != null)
            {
                bitmask |= 1 << 5;
                data.SetFloat32(byteCount, leading.Value, _kFakeHostEndian);
                byteCount += 4;
            }
            if (forceStrutHeight != null)
            {
                bitmask |= 1 << 6;
                // We store this boolean directly in the bitmask since there is
                // extra space in the 16 bit int.
                bitmask |= (forceStrutHeight.Value ? 1 : 0) << 7;
            }

            data.SetInt8(0, bitmask);

            // TODO : Show this return value is valid !!!
            return data;  //ByteData.view(data.buffer, 0, byteCount);
        }
        
    }
}