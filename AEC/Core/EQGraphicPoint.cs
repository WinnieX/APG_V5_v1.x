using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace AEC.Core
{
    public enum EQPointType
    {
        EQ,
        HPF,
        LPF,
    }

    public class EQGraphicPoint
    {
        public readonly EQPointType type;
        public PointF center;

        public bool visible
        {
            get;
            set;
        }

        public EQGraphicPoint(EQPointType type_, PointF center_)
        {
            this.type = type_;
            this.center = center_;
        }
    }

    public class EQPoint : EQGraphicPoint
    {
        public readonly EQ eq;
        public EQPoint(EQ eq_, PointF center):
            base(EQPointType.EQ, center)
        {
            this.eq = eq_;
            base.visible = this.eq.visible;
        }
    }

    public class HPFPoint : EQGraphicPoint
    {
        public readonly XPFilterBase filter;
        public HPFPoint(XPFilterBase filter_, PointF center) :
            base(EQPointType.HPF, center)
        {
            this.filter = filter_;
            base.visible = this.filter.visible;
        }
    }

    public class LPFPoint : EQGraphicPoint
    {
        public readonly XPFilterBase filter;
        public LPFPoint(XPFilterBase filter_, PointF center) :
            base(EQPointType.LPF, center)
        {
            this.filter = filter_;
            base.visible = this.filter.visible;
        }
    }
}
