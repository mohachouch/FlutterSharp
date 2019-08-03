using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace FlutterSharp.UI
{
    /// A paragraph of text.
    ///
    /// A paragraph retains the size and position of each glyph in the text and can
    /// be efficiently resized and painted.
    ///
    /// To create a [Paragraph] object, use a [ParagraphBuilder].
    ///
    /// Paragraphs can be displayed on a [Canvas] using the [Canvas.drawParagraph]
    /// method.
    public class Paragraph : NativeFieldWrapperClass2
    {
        /// This class is created by the engine, and should not be instantiated
        /// or extended directly.
        ///
        /// To create a [Paragraph] object, use a [ParagraphBuilder].
        public Paragraph(IntPtr handle)
            : base(handle)
        {

        }

        /// The amount of horizontal space this paragraph occupies.
        ///
        /// Valid only after [layout] has been called.
        public double Width => 0.0; // TODO: native 'Paragraph_width';

        /// The amount of vertical space this paragraph occupies.
        ///
        /// Valid only after [layout] has been called.
        public double Height => Paragraph_height(this.Handle);

        /// The distance from the left edge of the leftmost glyph to the right edge of
        /// the rightmost glyph in the paragraph.
        ///
        /// Valid only after [layout] has been called.
        public double LongestLine => 0.0; // TODO: native 'Paragraph_longestLine';

        /// The minimum width that this paragraph could be without failing to paint
        /// its contents within itself.
        ///
        /// Valid only after [layout] has been called.
        public double MinIntrinsicWidth => 0.0; // TODO: native 'Paragraph_minIntrinsicWidth';

        /// Returns the smallest width beyond which increasing the width never
        /// decreases the height.
        ///
        /// Valid only after [layout] has been called.
        public double MaxIntrinsicWidth => Paragraph_maxIntrinsicWidth(this.Handle);

        /// The distance from the top of the paragraph to the alphabetic
        /// baseline of the first line, in logical pixels.
        public double AlphabeticBaseline => 0.0; // TODO: native 'Paragraph_alphabeticBaseline';

        /// The distance from the top of the paragraph to the ideographic
        /// baseline of the first line, in logical pixels.
        public double IdeographicBaseline => 0.0; // TODO: native 'Paragraph_ideographicBaseline';

        /// True if there is more vertical content, but the text was truncated, either
        /// because we reached `maxLines` lines of text or because the `maxLines` was
        /// null, `ellipsis` was not null, and one of the lines exceeded the width
        /// constraint.
        ///
        /// See the discussion of the `maxLines` and `ellipsis` arguments at
        /// [new ParagraphStyle].
        public double DidExceedMaxLines => 0.0; // TODO: native 'Paragraph_didExceedMaxLines';

        /// Computes the size and position of each glyph in the paragraph.
        ///
        /// The [ParagraphConstraints] control how wide the text is allowed to be.
        public void Layout(ParagraphConstraints constraints) => Paragraph_layout(this.Handle, constraints.Width);
        
        /// Returns a list of text boxes that enclose the given text range.
        ///
        /// The [boxHeightStyle] and [boxWidthStyle] parameters allow customization
        /// of how the boxes are bound vertically and horizontally. Both style
        /// parameters default to the tight option, which will provide close-fitting
        /// boxes and will not account for any line spacing.
        ///
        /// Coordinates of the TextBox are relative to the upper-left corner of the paragraph,
        /// where positive y values indicate down.
        ///
        /// The [boxHeightStyle] and [boxWidthStyle] parameters must not be null.
        ///
        /// See [BoxHeightStyle] and [BoxWidthStyle] for full descriptions of each option.
        public List<TextBox> GetBoxesForRange(int start, int end, BoxHeightStyle boxHeightStyle = BoxHeightStyle.Tight,
            BoxWidthStyle boxWidthStyle = BoxWidthStyle.Tight)
        {
            return GetBoxesForRange(start, end, (int)boxHeightStyle, (int)boxWidthStyle);
        }

        private List<TextBox> GetBoxesForRange(int start, int end, int boxHeightStyle, int boxWidthStyle)
        {
            // TODO : native 'Paragraph_getRectsForRange';
            return null;
        }

        /// Returns a list of text boxes that enclose all placeholders in the paragraph.
        ///
        /// The order of the boxes are in the same order as passed in through [addPlaceholder].
        ///
        /// Coordinates of the [TextBox] are relative to the upper-left corner of the paragraph,
        /// where positive y values indicate down.
        List<TextBox> GetBoxesForPlaceholders()
        {
            // TODO : native 'Paragraph_getRectsForPlaceholders';
            return null;
        }

        /// Returns the text position closest to the given offset.
        public TextPosition GetPositionForOffset(Offset offset)
        {
            var encoded = GetPositionForOffset(offset.Dx, offset.Dy);
            return new TextPosition(offset: encoded[0], affinity: (TextAffinity)encoded[1]);
        }

        private List<int> GetPositionForOffset(double dx, double dy)
        {
            // TODO : native 'Paragraph_getPositionForOffset';
            return null;
        }

        /// Returns the [start, end] of the word at the given offset. Characters not
        /// part of a word, such as spaces, symbols, and punctuation, have word breaks
        /// on both sides. In such cases, this method will return [offset, offset+1].
        /// Word boundaries are defined more precisely in Unicode Standard Annex #29
        /// http://www.unicode.org/reports/tr29/#Word_Boundaries
        public List<int> GetWordBoundary(int offset)
        {
            // TODO : native 'Paragraph_getWordBoundary';
            return null;
        }

        // Redirecting the paint function in this way solves some dependency problems
        // in the C++ code. If we straighten out the C++ dependencies, we can remove
        // this indirection.
        internal void Paint(Canvas canvas, double x, double y)
        {
            Paragraph_paint(this.Handle, canvas.Handle, x, y);
        }

        [DllImport("libflutter")]
        public extern static void Paragraph_layout(IntPtr pParagraph, double width);

        [DllImport("libflutter")]
        public extern static void Paragraph_paint(IntPtr pParagraph, IntPtr canvas, double x, double y);

        [DllImport("libflutter")]
        public extern static double Paragraph_height(IntPtr pParagraph);

        [DllImport("libflutter")]
        public extern static double Paragraph_maxIntrinsicWidth(IntPtr pParagraph);
    }
}