using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AEC.UI
{
    public partial class gtDataGridView : DataGridView
    {
        public gtDataGridView()
        {
            InitializeComponent();
        }

        public ScrollBar VScrollbar
        {
            get
            {
                return base.VerticalScrollBar;
            }
        }
    }
}
