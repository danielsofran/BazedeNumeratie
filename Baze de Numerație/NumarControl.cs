using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Baze_de_Numerație
{
    public partial class NumarControl : UserControl
    {
        public static char inm = '\u00D7';
        string last = "";
        int x = 0;

        public NumarControl()
        {
            InitializeComponent();
            textBox1.KeyPress += NumarControl_KeyPress;
        }

        private void NumarControl_FontChanged(object sender, EventArgs e)
        {
            foreach (TextBox t in Controls)
                t.Font = Font;
        }

        private void NumarControl_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox t = sender as TextBox;
            x += (int)t.Font.Size-3;
            t.Width = x;
            if (e.KeyChar == '*' || e.KeyChar == 'x')
                e.KeyChar = inm;
            if (e.KeyChar == (char)8)
                /*if(last=='(')
                    t.*/
            if(e.KeyChar=='(')
            {
                TextBox t1 = new TextBox();
                t1.BorderStyle = BorderStyle.None;
                t1.Font = new Font(t.Font.FontFamily, t.Font.SizeInPoints/2);
                t1.Location = new Point((sender as TextBox).Right, this.Height/2);
                t1.Text += e.KeyChar;
                t1.Width = (int)t1.Font.Size;
                t1.Select(1, 0);
                //(sender as TextBox).KeyPress -= NumarControl_KeyPress;
                t1.KeyPress += NumarControl_KeyPress;
                Controls.Add(t1);
                t1.Focus();
                e.Handled = true;
            }
            last += e.KeyChar;
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
