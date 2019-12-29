using System;
using FlutterSharp.UI;

namespace FlutterSharp.SDK.Internal
{
    /// Parent data used by [RenderBox] and its subclasses.
    public class BoxParentData : ParentData
    {
        /// The offset at which to paint the child in the parent's coordinate system.
        public Offset Offset = Offset.Zero;
    }

    public class BoxConstraints : Constraints
    {
        public BoxConstraints(double minWidth = 0.0, double maxWidth = double.MaxValue, double minHeight = 0.0, double maxHeight = double.MaxValue)
        {
            MinWidth = minWidth;
            MaxWidth = maxWidth;
            MinHeight = minHeight;
            MaxHeight = maxHeight;
        }

        internal Size Constrain(Size size)
        {
            Size result = new Size(ConstrainWidth(size.Width), ConstrainHeight(size.Height));
            
            return result;
        }

        private double ConstrainHeight(double height = double.PositiveInfinity)
        {
            return height.Clamp(MinHeight, MaxHeight);
        }

        private double ConstrainWidth(double width = double.PositiveInfinity)
        {
            return width.Clamp(MinWidth, MaxWidth);
        }

        public readonly double MinWidth;
        public readonly double MaxWidth;
        public readonly double MinHeight;
        public readonly double MaxHeight;

        internal Size Smallest = new Size(0, 0);

        internal static Constraints Tight(Size size)
        {
            return new BoxConstraints(size.Width, size.Width, size.Height, size.Height);
        }

        public BoxConstraints Deflate(EdgeInsets edges)
        {
            double horizontal = edges.Horizontal;
            double vertical = edges.Vertical;
            double deflatedMinWidth = Math.Max(0.0, MinWidth - horizontal);
            double deflatedMinHeight = Math.Max(0.0, MinHeight - vertical);
            return new BoxConstraints(
              minWidth: deflatedMinWidth,
              maxWidth: Math.Max(deflatedMinWidth, MaxWidth - horizontal),
              minHeight: deflatedMinHeight,
              maxHeight: Math.Max(deflatedMinHeight, MaxHeight - vertical)
        
            );
        }
    }

    public class EdgeInsets
    {
        public double Vertical => Top + Bottom;
        public double Horizontal => Left + Right;
        public int Left { get; internal set; }
        public int Top { get; internal set; }
        public int Right { get; internal set; }
        public int Bottom { get; internal set; }

        public EdgeInsets(int uniform)
        {
            Left = uniform;
            Top = uniform;
            Right = uniform;
            Bottom = uniform;
        }
    }

    internal enum _IntrinsicDimension { MinWidth, MaxWidth, MinHeight, MaxHeight }

    internal class _IntrinsicDimensionsCacheEntry
    {
        internal _IntrinsicDimensionsCacheEntry(_IntrinsicDimension dimension, double argument)
        {
            Argument = argument;
            Dimension = dimension;
        }

        internal _IntrinsicDimension Dimension;
        internal double Argument;
    }

    public class RenderBox : RenderObject
    {
        public override void SetupParentData(RenderObject child)
        {
            if (!(child.ParentData is BoxParentData))
                child.ParentData = new BoxParentData();
        }

        Map<_IntrinsicDimensionsCacheEntry, double> _cachedIntrinsicDimensions;

        private double _computeIntrinsicDimension(_IntrinsicDimension dimension, double argument, Func<double, double> computer)
        {
            return computer(argument);
        }

        public virtual double GetMinIntrinsicWidth(double height)
        {
            return _computeIntrinsicDimension(_IntrinsicDimension.MinWidth, height, ComputeMinIntrinsicWidth);
        }

        protected virtual double ComputeMinIntrinsicWidth(double height)
        {
            return 0.0;
        }

        public virtual double GetMaxIntrinsicWidth(double height)
        {
            return _computeIntrinsicDimension(_IntrinsicDimension.MaxWidth, height, ComputeMaxIntrinsicWidth);
        }

        protected virtual double ComputeMaxIntrinsicWidth(double height)
        {
            return 0.0;
        }

        public virtual double GetMinIntrinsicHeight(double width)
        {
            return _computeIntrinsicDimension(_IntrinsicDimension.MinHeight, width, ComputeMinIntrinsicHeight);
        }

        protected virtual double ComputeMinIntrinsicHeight(double width)
        {
            return 0.0;
        }

        public virtual double GetMaxIntrinsicHeight(double width)
        {
            return _computeIntrinsicDimension(_IntrinsicDimension.MaxHeight, width, ComputeMaxIntrinsicHeight);
        }

        protected virtual double ComputeMaxIntrinsicHeight(double width)
        {
            return 0.0;
        }

        /// Whether this render object has undergone layout and has a [size].
        public bool HasSize => _size != null;

        private Size _size;
        public Size Size
        {
            get
            {
                return _size;
            }
            set
            {
                _size = value;
            }
        }

        public Size DebugAdoptSize(Size value) => value;

        public Rect SemanticBounds => Offset.Zero & Size;

        protected override void DebugResetSize()
        {
            // updates the value of size._canBeUsedByParent if necessary
            Size = Size;
        }

        Map<TextBaseline, double> _cachedBaselines;
        static bool _debugDoingBaseline = false;
        static bool _debugSetDoingBaseline(bool value)
        {
            _debugDoingBaseline = value;
            return true;
        }

        public double? GetDistanceToBaseline(TextBaseline baseline, bool onlyReal = false)
        {
            double? result = GetDistanceToActualBaseline(baseline);
            if (result == null && !onlyReal)
                return Size.Height;
            return result;
        }

        public virtual double? GetDistanceToActualBaseline(TextBaseline baseline)
        {
            _cachedBaselines = _cachedBaselines ?? new Map<TextBaseline, double>();
            //_cachedBaselines.putIfAbsent(baseline, () => computeDistanceToActualBaseline(baseline));
            return ComputeDistanceToActualBaseline(baseline);// null; //_cachedBaselines[baseline];
        }

        protected virtual double? ComputeDistanceToActualBaseline(TextBaseline baseline)
        {
            return null;
        }

        public new virtual BoxConstraints Constraints => base.Constraints as BoxConstraints;

        public override void DebugAssertDoesMeetConstraints()
        {
        }

        public override void MarkNeedsLayout()
        {
            if ((_cachedBaselines != null && _cachedBaselines.Count > 0) || (_cachedIntrinsicDimensions != null && _cachedIntrinsicDimensions.Count > 0))
            {
                // If we have cached data, then someone must have used our data.
                // Since the parent will shortly be marked dirty, we can forget that they
                // used the baseline and/or intrinsic dimensions. If they use them again,
                // then we'll fill the cache again, and if we get dirty again, we'll
                // notify them again.
                _cachedBaselines?.Clear();
                _cachedIntrinsicDimensions?.Clear();
                if (Parent is RenderObject)
                {
                    MarkParentNeedsLayout();
                    return;
                }
            }

            base.MarkNeedsLayout();
        }

        protected override void PerformResize()
        {
            Size = Constraints.Smallest;
        }

        public override void ApplyPaintTransform(RenderObject child, Matrix4 transform)
        {
            BoxParentData childParentData = child.ParentData as BoxParentData;
            Offset offset = childParentData.Offset;
            transform.Translate(offset.Dx, offset.Dy);
        }

        public virtual new Rect PaintBounds => Offset.Zero & Size;
    }
}
