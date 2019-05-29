using System.Collections.Generic;

namespace FlutterSharp.UI
{
    /// A linear decoration to draw near the text.
    public class TextDecoration
    {
        public TextDecoration(int _mask)
        {
            this.Mask = _mask;
        }

        /// Creates a decoration that paints the union of all the given decorations.
        public static TextDecoration Combine(List<TextDecoration> decorations)
        {
            int mask = 0;

            foreach (var decoration in decorations)
                mask |= decoration.Mask;

            return new TextDecoration(mask);
        }

        public readonly int Mask;

        /// Whether this decoration will paint at least as much decoration as the given decoration.
        public bool Contains(TextDecoration other)
        {
            return (Mask | other.Mask) == Mask;
        }

        /// Do not draw a decoration
        public static readonly TextDecoration None = new TextDecoration(0x0);

        /// Draw a line underneath each line of text
        public static readonly TextDecoration Underline = new TextDecoration(0x1);

        /// Draw a line above each line of text
        public static readonly TextDecoration Overline = new TextDecoration(0x2);

        /// Draw a line through each line of text
        public static readonly TextDecoration LineThrough = new TextDecoration(0x4);

        public override bool Equals(object obj)
        {
            if (obj is TextDecoration textDecoration)
                return Mask == textDecoration.Mask;

            return false;
        }

        public override int GetHashCode()
        {
            return Mask.GetHashCode();
        }

        public override string ToString()
        {
            if (Mask == 0)
                return "TextDecoration.None";

            var values = new List<string>();
            if ((Mask & Underline.Mask) != 0)
                values.Add("Underline");
            if ((Mask & Overline.Mask) != 0)
                values.Add("Overline");
            if ((Mask & LineThrough.Mask) != 0)
                values.Add("LineThrough");
            if (values.Count == 1)
                return $"TextDecoration.{values[0]}";

            return $"TextDecoration.Combine([{string.Join(", ", values)}])";
        }
    }
}