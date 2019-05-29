using static FlutterSharp.UI.UITypes;

namespace FlutterSharp.UI
{
    /// A rectangle enclosing a run of text.
    ///
    /// This is similar to [Rect] but includes an inherent [TextDirection].
    public class TextBox
    {
        public TextBox(double left, double top, double right, double bottom, TextDirection direction)
        {
            this.Left = left;
            this.Top = top;
            this.Right = right;
            this.Bottom = bottom;
            this.Direction = direction;
        }

        /// Creates an object that describes a box containing text.
        public static TextBox FromLTRB(double left, double top, double right, double bottom, TextDirection direction) =>
            new TextBox(left, top, right, bottom, direction);

        /// The left edge of the text box, irrespective of direction.
        ///
        /// To get the leading edge (which may depend on the [direction]), consider [start].
        public readonly double Left;

        /// The top edge of the text box.
        public readonly double Top;

        /// The right edge of the text box, irrespective of direction.
        ///
        /// To get the trailing edge (which may depend on the [direction]), consider [end].
        public readonly double Right;

        /// The bottom edge of the text box.
        public readonly double Bottom;

        /// The direction in which text inside this box flows.
        public readonly TextDirection Direction;


        /// Returns a rect of the same size as this box.
        public Rect ToRect() => Rect.FromLTRB(Left, Top, Right, Bottom);

        /// The [left] edge of the box for left-to-right text; the [right] edge of the box for right-to-left text.
        ///
        /// See also:
        ///
        ///  * [direction], which specifies the text direction.
        public double Start => (Direction == TextDirection.Ltr) ? Left : Right;

        /// The [right] edge of the box for left-to-right text; the [left] edge of the box for right-to-left text.
        ///
        /// See also:
        ///
        ///  * [direction], which specifies the text direction.
        public double End => (Direction == TextDirection.Ltr) ? Right : Left;

        public override bool Equals(object obj)
        {
            if (obj is TextBox typedOther)
                return Left == typedOther.Left &&
                       Top == typedOther.Top &&
                       Right == typedOther.Right &&
                       Bottom == typedOther.Bottom &&
                       Direction == typedOther.Direction;

            return false;
        }

        public override int GetHashCode()
        {
            return HashValues(Left, Top, Right, Bottom, Direction);
        }

        public override string ToString()
        {
            return $"TextBox({Left.ToStringAsFixed(1)}, {Top.ToStringAsFixed(1)}, {Right.ToStringAsFixed(1)}, {Bottom.ToStringAsFixed(1)}, {Direction})";
        }
    }
}