using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Virtualpad
{
    public partial class KeyBoard : Form
    {
        public KeyBoard()
        {
            InitializeComponent();
        }
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x84:
                    base.WndProc(ref m);
                    if ((int)m.Result == 0x1)
                        m.Result = (IntPtr)0x2;
                    return;
            }

            base.WndProc(ref m);
        }
        private void KeyBoard_Load(object sender, EventArgs e)
        {
            PersianKeyBoard.Visible = true;
            EnglishKeyBoard.Visible = false;
        }

        private void button76_Click(object sender, EventArgs e)
        {
            Pad pad = new Pad();
            Button btn = sender as Button;
            richTextBox1.Text += btn.Text.ToString();
        }

        private void PicExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button44_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = richTextBox1.Text.Remove(richTextBox1.TextLength - 1, 1);
        }

        private void button46_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += "\r\n";
        }

        private void button45_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += " ";
        }

        private void Pic_Copy_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void Right_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Right;
        }

        private void Center_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Center;
        }

        private void Left_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Left;
        }

        private void Persian_Click(object sender, EventArgs e)
        {
            PersianKeyBoard.Visible = true;
            EnglishKeyBoard.Visible = false;
        }

        private void Engilsh_Click(object sender, EventArgs e)
        {
            PersianKeyBoard.Visible = false;
            EnglishKeyBoard.Visible = true;
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            Characters.Text = Convert.ToString(richTextBox1.Text.Length);

            string[] x = richTextBox1.Text.Split('\n');
            Lines.Text = Convert.ToString(x.Length);
        }
    }
}
