using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace AEC.UI.Controls
{
    public class MRadioButton : MStatefulButton
    {
        public override bool IsCheckedInit
        {
            get
            {
                return _isChecked;
            }
            set
            {
                if (_isChecked != value)
                {
                    _isChecked = value;

                    if(value)
                    {
                        this.UnCheckOtherRadioButton();
                    }

                    base.UpdateUI();
                }
            }
        }

        public override bool IsChecked
        {
            get
            {
                return _isChecked;
            }
            set
            {
                if (_isChecked != value)
                {
                    _isChecked = value;

                    if (value)
                    {
                        this.UnCheckOtherRadioButton();
                    }

                    base.UpdateUI();
                    this.CheckedChangedHandler();
                }
            }
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                this.IsChecked = true;
            }
        }

        private void UnCheckOtherRadioButton()
        {
            Control parent = this.Parent;
            if (parent != null && parent.HasChildren)
            {
                foreach (Control control in parent.Controls)
                {
                    if (control != this && control is MRadioButton)
                    {
                        MRadioButton rdo = control as MRadioButton;
                        if (rdo._isChecked)
                        {
                            rdo.IsChecked = false;
                        }
                    }
                }
            }
        }
    }
}
