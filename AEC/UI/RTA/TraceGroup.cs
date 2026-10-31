using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using AEC.RTA.Core;

namespace AEC.UI.RTA
{
    public partial class TraceGroup : UserControl
    {
        private RefTraceGroupNum _group = RefTraceGroupNum.GroupA;
        private bool _isActive = false;
        private TraceGroupBase trace_group = null;
        private ToolTip toolTip = new ToolTip();

        private Pen active_pen = new Pen(Color.FromArgb(51, 255, 64), 2);

        public EventHandler GroupShowChangedHandler = null;
        public EventHandler GroupCurrIndexChangedHandler = null;

        public TraceGroup()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.Selectable, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.BackColor = Color.Transparent;

            chkBoxSwitch.backNormal = AEC.Properties.Resources.group_a_unchecked;
            chkBoxSwitch.backHover = AEC.Properties.Resources.group_a_unchecked;
            chkBoxSwitch.backPressed = AEC.Properties.Resources.group_a_checked;
            chkBoxSwitch.backDisabled = AEC.Properties.Resources.group_a_unchecked;

            this.chkBoxSwitch.CheckedChanged += chkBoxSwitch_CheckedChanged;

            this.rdoTrace0.CheckedChanged += rdoTrace_CheckedChanged;
            this.rdoTrace1.CheckedChanged += rdoTrace_CheckedChanged;
            this.rdoTrace2.CheckedChanged += rdoTrace_CheckedChanged;
            this.rdoTrace3.CheckedChanged += rdoTrace_CheckedChanged;

            this.rdoTrace0.Actived += Group_Actived;
            this.rdoTrace1.Actived += Group_Actived;
            this.rdoTrace2.Actived += Group_Actived;
            this.rdoTrace3.Actived += Group_Actived;

            this.rdoTrace0.MouseHover += MouseHoveTrace;
            this.rdoTrace1.MouseHover += MouseHoveTrace;
            this.rdoTrace2.MouseHover += MouseHoveTrace;
            this.rdoTrace3.MouseHover += MouseHoveTrace;

            this.rdoTrace0.MouseLeave += MouseLeaveTrace;
            this.rdoTrace1.MouseLeave += MouseLeaveTrace;
            this.rdoTrace2.MouseLeave += MouseLeaveTrace;
            this.rdoTrace3.MouseLeave += MouseLeaveTrace;

            this.toolTip.ToolTipIcon = ToolTipIcon.Info;
        }

        private void chkBoxSwitch_CheckedChanged(object sender, EventArgs e)
        {
            if (GroupShowChangedHandler != null)
            {
                GroupShowChangedHandler(this, new ShowChangedEventArgs(_group, this.chkBoxSwitch.isChecked));
            }
        }

        private void Group_Actived(object sender, EventArgs e)
        {
            if (GroupShowChangedHandler != null)
            {
                this.chkBoxSwitch.isChecked = true;
                GroupShowChangedHandler(this, new ShowChangedEventArgs(_group, this.chkBoxSwitch.isChecked));
            }
        }

        private void MouseHoveTrace(object sender, EventArgs e)
        {
            RTARadio rs = sender as RTARadio;
            int currIndex = 0;
            if (rs.Equals(this.rdoTrace0))
            {
                currIndex = 0;
            }
            else if (rs.Equals(this.rdoTrace1))
            {
                currIndex = 1;
            }
            else if (rs.Equals(this.rdoTrace2))
            {
                currIndex = 2;
            }
            else if (rs.Equals(this.rdoTrace3))
            {
                currIndex = 3;
            }

            if (trace_group != null)
            {
                RefTraceInfoBase refTrace = trace_group.GetTrace(currIndex);
                if (refTrace.isValid)
                {
                    this.toolTip.ToolTipTitle = "参考曲线信息";

                    this.toolTip.Show(refTrace.ToString(), rs);
                }
            }
        }

        private void MouseLeaveTrace(object sender, EventArgs e)
        {
            RTARadio rs = sender as RTARadio;
            
            //int currIndex = 0;
            //if (rs.Equals(this.rdoTrace0))
            //{
            //    currIndex = 0;
            //}
            //else if (rs.Equals(this.rdoTrace1))
            //{
            //    currIndex = 1;
            //}
            //else if (rs.Equals(this.rdoTrace2))
            //{
            //    currIndex = 2;
            //}
            //else if (rs.Equals(this.rdoTrace3))
            //{
            //    currIndex = 3;
            //}
           
            this.toolTip.Hide(rs);
        }

        private void rdoTrace_CheckedChanged(object sender, EventArgs e)
        {
            RTARadio rs = sender as RTARadio;
            if (rs.isChecked)
            {
                int currIndex = 0;
                if (rs.Equals(this.rdoTrace0))
                {
                    currIndex = 0;
                }
                else if (rs.Equals(this.rdoTrace1))
                {
                    currIndex = 1;
                }
                else if (rs.Equals(this.rdoTrace2))
                {
                    currIndex = 2;
                }
                else if (rs.Equals(this.rdoTrace3))
                {
                    currIndex = 3;
                }

                if (GroupCurrIndexChangedHandler != null)
                {
                    GroupCurrIndexChangedHandler(this, new GroupCurrIndexChangedEventArgs(_group, currIndex));
                }
            }
        }

        public RefTraceGroupNum group
        {
            get
            {
                return this._group;
            }
            set
            {
                switch (value)
                {
                    case RefTraceGroupNum.GroupA:
                        chkBoxSwitch.backNormal = AEC.Properties.Resources.group_a_unchecked;
                        chkBoxSwitch.backHover = AEC.Properties.Resources.group_a_unchecked;
                        chkBoxSwitch.backPressed = AEC.Properties.Resources.group_a_checked;
                        chkBoxSwitch.backDisabled = AEC.Properties.Resources.group_a_unchecked;
                        break;

                    case RefTraceGroupNum.GroupB:
                        chkBoxSwitch.backNormal = AEC.Properties.Resources.group_b_unchecked;
                        chkBoxSwitch.backHover = AEC.Properties.Resources.group_b_unchecked;
                        chkBoxSwitch.backPressed = AEC.Properties.Resources.group_b_checked;
                        chkBoxSwitch.backDisabled = AEC.Properties.Resources.group_b_unchecked;
                        break;

                    case RefTraceGroupNum.GroupC:
                        chkBoxSwitch.backNormal = AEC.Properties.Resources.group_c_unchecked;
                        chkBoxSwitch.backHover = AEC.Properties.Resources.group_c_unchecked;
                        chkBoxSwitch.backPressed = AEC.Properties.Resources.group_c_checked;
                        chkBoxSwitch.backDisabled = AEC.Properties.Resources.group_c_unchecked;
                        break;

                    case RefTraceGroupNum.GroupD:
                        chkBoxSwitch.backNormal = AEC.Properties.Resources.group_d_unchecked;
                        chkBoxSwitch.backHover = AEC.Properties.Resources.group_d_unchecked;
                        chkBoxSwitch.backPressed = AEC.Properties.Resources.group_d_checked;
                        chkBoxSwitch.backDisabled = AEC.Properties.Resources.group_d_unchecked;
                        break;

                    case RefTraceGroupNum.GroupE:
                        chkBoxSwitch.backNormal = AEC.Properties.Resources.group_e_unchecked;
                        chkBoxSwitch.backHover = AEC.Properties.Resources.group_e_unchecked;
                        chkBoxSwitch.backPressed = AEC.Properties.Resources.group_e_checked;
                        chkBoxSwitch.backDisabled = AEC.Properties.Resources.group_e_unchecked;
                        break;
                }

                this._group = value;

                this.chkBoxSwitch.Invalidate();
                this.chkBoxSwitch.Update();
            }
        }

        public bool isShowGroup
        {
            get
            {
                return this.chkBoxSwitch.isChecked;
            }
            set
            {
                this.chkBoxSwitch.isChecked = value;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            DrawBackground(e.Graphics);
        }

        private void DrawBackground(Graphics g)
        {
            if (this._isActive)
            {
                Point st = new Point(0, this.Height - 2);
                Point ed = new Point(this.Width - 1, st.Y);
                g.DrawLine(active_pen, st, ed);
            }
        }

        public bool isActive
        {
            get
            {
                return this._isActive;
            }
            set
            {
                if (value)
                {
                    UnactiveOtherGroup();
                }

                bool temp = this._isActive;
                this._isActive = value;
                if (temp != value)
                {
                    this.Invalidate();
                    this.Update();
                }
            }
        }

        private void UnactiveOtherGroup()
        {
            Control parent = this.Parent;
            if (parent != null && parent.HasChildren)
            {
                foreach (Control control in parent.Controls)
                {
                    if (control != this && control is TraceGroup)
                    {
                        TraceGroup group = control as TraceGroup;
                        if (group.isActive)
                        {
                            group.isActive = false;
                        }
                    }
                }
            }
        }

        public int currTraceIndex
        {
            set
            {
                switch (value)
                {
                    case 0:
                        rdoTrace0.Check();
                        break;

                    case 1:
                        rdoTrace1.Check();
                        break;

                    case 2:
                        rdoTrace2.Check();
                        break;

                    case 3:
                        rdoTrace3.Check();
                        break;
                }
            }
        }

        public void UpdateGroupTraceUI(TraceGroupBase group)
        {
            System.Diagnostics.Debug.Assert(group != null);
            trace_group = group;
            for (int i = 0; i < group.traces.Length; ++i)
            {
                UpdateTraceIsValid(i, group.traces[i].isValid);
            }

            this.currTraceIndex = group.currIndex;
            this.isShowGroup = group.isShow;

            this.isActive = false;
        }

        public void UpdateCurrTrace(bool isValid)
        {
            if (rdoTrace0.Checked)
            {
                rdoTrace0.isSignal = isValid;
            }
            else if (rdoTrace1.Checked)
            {
                rdoTrace1.isSignal = isValid;
            }
            else if (rdoTrace2.Checked)
            {
                rdoTrace2.isSignal = isValid;
            }
            else if (rdoTrace3.Checked)
            {
                rdoTrace3.isSignal = isValid;
            }
        }

        private void UpdateTraceIsValid(int index, bool isValid)
        {
            switch (index)
            {
                case 0:
                    rdoTrace0.isSignal = isValid;
                    break;

                case 1:
                    rdoTrace1.isSignal = isValid;
                    break;

                case 2:
                    rdoTrace2.isSignal = isValid;
                    break;

                case 3:
                    rdoTrace3.isSignal = isValid;
                    break;
            }
        }
    }

    public class ShowChangedEventArgs : EventArgs
    {
        public RefTraceGroupNum _gn = RefTraceGroupNum.GroupA;
        public bool _isShow = false;

        public ShowChangedEventArgs(RefTraceGroupNum gn, bool isShow)
        {
            this._gn = gn;
            this._isShow = isShow;
        }
    }

    public class GroupCurrIndexChangedEventArgs : EventArgs
    {
        public RefTraceGroupNum _gn = RefTraceGroupNum.GroupA;
        public int _currIndex = 0;

        public GroupCurrIndexChangedEventArgs(RefTraceGroupNum gn, int currIndex)
        {
            this._gn = gn;
            this._currIndex = currIndex;
        }
    }
}
