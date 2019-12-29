using System;
using System.Collections.Generic;
using System.Text;
using FlutterSharp.UI;

namespace FlutterSharp.SDK.Internal
{
    public class RenderShiftedBox : RenderBoxWithChildMixin<RenderBox>
    {
        public RenderShiftedBox(RenderBox child)
        {
            this.Child = child;
        }

        protected override double ComputeMinIntrinsicWidth(double height)
        {
            if (Child != null)
                return Child.GetMinIntrinsicWidth(height);
            return 0.0;
        }

        protected override double ComputeMaxIntrinsicWidth(double height)
        {
            if (Child != null)
                return Child.GetMaxIntrinsicWidth(height);
            return 0.0;
        }

        protected override double ComputeMinIntrinsicHeight(double width)
        {
            if (Child != null)
                return Child.GetMinIntrinsicHeight(width);
            return 0.0;
        }

        protected override double ComputeMaxIntrinsicHeight(double width)
        {
            if (Child != null)
                return Child.GetMaxIntrinsicHeight(width);
            return 0.0;
        }

        protected override double? ComputeDistanceToActualBaseline(TextBaseline baseline)
        {
            double? result;
            if (Child != null)
            {
                result = Child.GetDistanceToActualBaseline(baseline);
                BoxParentData childParentData = Child.ParentData as BoxParentData; 
                if (result != null)
                    result += childParentData.Offset.Dy;
            }
            else
            {
                result = base.ComputeDistanceToActualBaseline(baseline);
            }
            return result;
        }

        public override void Paint(PaintingContext context, Offset offset)
        {
            if (Child != null)
            {
                BoxParentData childParentData = Child.ParentData as BoxParentData;
                if(childParentData != null)
                    context.PaintChild(Child, childParentData.Offset + offset);
            }
        }
    }
}
