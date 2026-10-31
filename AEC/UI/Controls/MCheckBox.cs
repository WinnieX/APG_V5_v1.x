using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace AEC.UI.Controls
{
    public class MCheckBox : MStatefulButton
    {
        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (e.Button == System.Windows.Forms.MouseButtons.Left && _isAutoCheck)
            {
                this.IsChecked = !this._isChecked;
            }
        }
    }
}
