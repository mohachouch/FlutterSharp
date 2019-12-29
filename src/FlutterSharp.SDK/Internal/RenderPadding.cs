using FlutterSharp.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlutterSharp.SDK.Internal
{
    public class RenderPadding : RenderShiftedBox
    {
        public RenderPadding(RenderBox child, EdgeInsets padding) : base(child)
        {
            _resolvedPadding = padding;
        }

        EdgeInsets _resolvedPadding;

        protected override void PerformLayout()
        {
            if (Child == null)
            {
                Size = Constraints.Constrain(new Size(
                  _resolvedPadding.Left + _resolvedPadding.Right,
                  _resolvedPadding.Top + _resolvedPadding.Bottom));
                return;
            }
            BoxConstraints innerConstraints = Constraints.Deflate(_resolvedPadding);
            Child.Layout(innerConstraints, parentUsesSize: true);
            BoxParentData childParentData = Child.ParentData as BoxParentData;
            childParentData.Offset = new Offset(_resolvedPadding.Left, _resolvedPadding.Top);
            Size = Constraints.Constrain(new Size(
              _resolvedPadding.Left + Child.Size.Width + _resolvedPadding.Right,
              _resolvedPadding.Top + Child.Size.Height + _resolvedPadding.Bottom));
        }
    }
}
