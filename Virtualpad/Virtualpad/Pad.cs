using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Virtualpad
{
    public partial class Pad : Form
    {

        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern long BitBlt(IntPtr hdcDest, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, int dwRop);
        private Bitmap memoryImage;
        private void CaptureScreen()
        {
            Graphics mygraphics = this.CreateGraphics();
            Size s = this.Size;
            memoryImage = new Bitmap(s.Width, s.Height, mygraphics);
            Graphics memoryGraphics = Graphics.FromImage(memoryImage);
            IntPtr dc1 = mygraphics.GetHdc();
            IntPtr dc2 = memoryGraphics.GetHdc();
            BitBlt(dc2, 0, 0, this.ClientRectangle.Width, this.ClientRectangle.Height, dc1, 0, 0, 13369376);
            mygraphics.ReleaseHdc(dc1);
            memoryGraphics.ReleaseHdc(dc2);
        }

        public Pad()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.MaximumSize = this.MinimumSize = this.Size;
        }

        private void Pic_New_Click(object sender, EventArgs e)
        {
            editor.Text = "";
        }

        private void menuNew_Click(object sender, EventArgs e)
        {
            editor.Text = "";
        }

        private void menuOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Text Files (*.txt)|*.txt|" + "All Files(*.*)|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                editor.Text = File.ReadAllText(openFileDialog1.FileName);
        }

        private void Pic_Open_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Text Files (*.txt)|*.txt|" + "All Files(*.*)|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                editor.Text = File.ReadAllText(openFileDialog1.FileName);
        }

        private void menuSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "Text Files (*.txt)|*.txt|" + "All Files(*.*)|*.*";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                File.WriteAllText(saveFileDialog1.FileName, editor.Text);
        }

        private void Pic_Save_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "Text Files (*.txt)|*.txt|" + "All Files(*.*)|*.*";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                File.WriteAllText(saveFileDialog1.FileName, editor.Text);
        }

        private void menuPrint_Click(object sender, EventArgs e)
        {

            PrintPreviewDialog printPreviewDialog1 = new PrintPreviewDialog();
            if (MessageBox.Show("آیا مطمعن هستید که میخواهید از این برگه پرینت بگیرید؟", "پریت از برگه", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                CaptureScreen();
                printPreviewDialog1.Document = printDocument1;
                printPreviewDialog1.ShowDialog();
            }

        }

        private void Pic_Print_Click(object sender, EventArgs e)
        {

            PrintPreviewDialog printPreviewDialog1 = new PrintPreviewDialog();
            if (MessageBox.Show("آیا مطمعن هستید که میخواهید از این برگه پرینت بگیرید؟", "پریت از برگه", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                CaptureScreen();
                printPreviewDialog1.Document = printDocument1;
                printPreviewDialog1.ShowDialog();
            }

        }

        private void Pic_Back_Click(object sender, EventArgs e)
        {
            RichTextBox xundo = (RichTextBox)this.ActiveControl;
            xundo.Undo();
        }

        private void Pic_Cut_Click(object sender, EventArgs e)
        {
            RichTextBox xcut = (RichTextBox)this.ActiveControl;
            xcut.Cut();
        }

        private void Pic_Copy_Click(object sender, EventArgs e)
        {
            RichTextBox xcopy = (RichTextBox)this.ActiveControl;
            xcopy.Copy();
        }

        private void Pic_Paste_Click(object sender, EventArgs e)
        {
            RichTextBox xpast = (RichTextBox)this.ActiveControl;
            xpast.Paste();
        }

        private void Pic_SelectAll_Click(object sender, EventArgs e)
        {
            RichTextBox xselectall = (RichTextBox)this.ActiveControl;
            xselectall.SelectAll();
        }

        private void Pic_Font_Click(object sender, EventArgs e)
        {
            FontDialog fontDialog1 = new FontDialog();

            if (fontDialog1.ShowDialog() == DialogResult.OK)
                editor.Font = fontDialog1.Font;
        }

        private void Pic_Color_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog1 = new ColorDialog();

            if (colorDialog1.ShowDialog() == DialogResult.OK)
                this.editor.ForeColor = colorDialog1.Color;
        }

        private void Pic_ColorText_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog1 = new ColorDialog();

            if (colorDialog1.ShowDialog() == DialogResult.OK)
                this.editor.SelectionColor = colorDialog1.Color;
        }

        private void Pic_FontText_Click(object sender, EventArgs e)
        {
            FontDialog fontDialog1 = new FontDialog();

            if (fontDialog1.ShowDialog() == DialogResult.OK)
                editor.SelectionFont = fontDialog1.Font;
        }

        private void Right_Click(object sender, EventArgs e)
        {
            editor.SelectionAlignment = HorizontalAlignment.Right;
        }

        private void Center_Click(object sender, EventArgs e)
        {
            editor.SelectionAlignment = HorizontalAlignment.Center;
        }

        private void Left_Click(object sender, EventArgs e)
        {
            editor.SelectionAlignment = HorizontalAlignment.Left;
        }

        private void Open_onScreenKeyboard_Click(object sender, EventArgs e)
        {
            KeyBoard KB = new KeyBoard();
            KB.Show();
        }

        private void editor_TextChanged(object sender, EventArgs e)
        {
            Characters.Text = Convert.ToString(editor.Text.Length);

            string[] x = editor.Text.Split('\n');
            Lines.Text = Convert.ToString(x.Length);
        }

        private void ThemeDark_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.DimGray;
            this.ForeColor = Color.White;

            panel1.BackColor = Color.DimGray;
            panel2.BackColor = Color.DimGray;
            panel3.BackColor = Color.DimGray;
            panel4.BackColor = Color.DimGray;

            editor.BackColor = Color.Black;
            editor.ForeColor = Color.White;

            menuStrip1.BackColor = Color.DimGray;
            menuStrip1.ForeColor = Color.Black;

            statusStrip1.BackColor = Color.DimGray;
        }

        private void ThemeLight_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.White;
            this.ForeColor = Color.Black;

            panel1.BackColor = Color.White;
            panel2.BackColor = Color.White;
            panel3.BackColor = Color.White;
            panel4.BackColor = Color.White;

            editor.BackColor = Color.White;
            editor.ForeColor = Color.Black;

            menuStrip1.BackColor = Color.White;
            menuStrip1.ForeColor = Color.Black;

            statusStrip1.BackColor = Color.White;
        }

        private void ThemeBlue_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.DarkCyan;
            this.ForeColor = Color.White;


            panel1.BackColor = Color.DarkCyan;
            panel2.BackColor = Color.DarkCyan;
            panel3.BackColor = Color.DarkCyan;
            panel4.BackColor = Color.DarkCyan;

            editor.BackColor = Color.Turquoise;
            editor.ForeColor = Color.Black;

            menuStrip1.BackColor = Color.DarkCyan;
            menuStrip1.ForeColor = Color.Black;

            statusStrip1.BackColor = Color.DarkCyan;
        }

        private void Status_Click(object sender, EventArgs e)
        {
            if (statusStrip1.Visible == false)
                statusStrip1.Visible = true;
            else
                statusStrip1.Visible = false;

        }

        private void Tool_Click(object sender, EventArgs e)
        {
            if (panel1.Visible == false)
                panel1.Visible = true;
            else
                panel1.Visible = false;
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(memoryImage, 0, 0);
        }

        private void Help_Click(object sender, EventArgs e)
        {
            MessageBox.Show("اخه مومن این برنامه هم راهنما میخواد که کلیک میکنی رو راهنما |: \n یه خورده به مغزت فشار بیار چیزی نمیشه |:" , "فشار بیار (به مغز) با توکل به خدا |:" , MessageBoxButtons.OK , MessageBoxIcon.Information, MessageBoxDefaultButton.Button3 , MessageBoxOptions.RightAlign);
        }

        private void پردازشکارآراToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(" Pardazesh-Karara.ir \n به ما حتما سر بزنید منتظر شما هستیم", "به ما سر بزنید", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
        }

        private void coderAloneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("سانده : AloneCoder \n Pardazesh-Karara.ir \n به ما حتما سر بزنید منتظر شما هستیم", "به ما سر بزنید", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
        }
    }
}
