using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 自定义事件
{
    public delegate void MyClick(Point p);
    public partial class UserControl1 : UserControl
    {
        public MyClick mc;
        public UserControl1()
        {
            InitializeComponent();
            this.BackColor = Color.Red;
        }

        private void UserControl1_MouseClick(object sender, MouseEventArgs e)
        {
            mc(new Point( MousePosition.X,MousePosition.Y ));
        }
    }
}
