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
    public partial class Form1 : Form
    {
        int[] baze = { 10, 10, 10, 10 };
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            NumericUpDown num = Controls.Find("numericUpDown"+(sender as TextBox).Name.Last(), true)[0] as NumericUpDown;
            if (!char.IsLetterOrDigit(e.KeyChar) || Numar.fromCifra(e.KeyChar) >= num.Value)
                e.Handled = true;
            Calc();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            int cif = int.Parse((sender as NumericUpDown).Name.Last().ToString());
            TextBox tx = Controls.Find("textBox" + cif, true)[0] as TextBox;
            Numar n = new Numar(tx.Text, baze[cif]);
            n.Baza = (int)(sender as NumericUpDown).Value;
            tx.Text = n.ToString();
            Calc();
            baze[cif] = (int)(sender as NumericUpDown).Value;
        }

        private void numericUpDown1_KeyDown(object sender, KeyEventArgs e) => numericUpDown1_ValueChanged(sender, null);

        void Calc()
        {

        }
    }
}
