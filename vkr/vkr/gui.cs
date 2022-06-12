using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace vkr
{
    public partial class @interface : Form
    {
        public @interface()
        {
            InitializeComponent();
        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {

        }

        private void interface_Load(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.panel1.Controls.Clear();
            katalog katalog = new katalog() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            katalog.FormBorderStyle = FormBorderStyle.None;
            this.panel1.Controls.Add(katalog);
            katalog.Show();
            
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            this.panel1.Controls.Clear();
            dogovor dogovor = new dogovor() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            dogovor.FormBorderStyle = FormBorderStyle.None;
            this.panel1.Controls.Add(dogovor);
            dogovor.Show();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            this.panel1.Controls.Clear();
            statistika dogovor = new statistika() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            dogovor.FormBorderStyle = FormBorderStyle.None;
            this.panel1.Controls.Add(dogovor);
            dogovor.Show();
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            this.panel1.Controls.Clear();
            progznoz dogovor = new progznoz() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            dogovor.FormBorderStyle = FormBorderStyle.None;
            this.panel1.Controls.Add(dogovor);
            dogovor.Show();
        }

        private void товарыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.panel1.Controls.Clear();
            tovar dogovor = new tovar() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            dogovor.FormBorderStyle = FormBorderStyle.None;
            this.panel1.Controls.Add(dogovor);
            dogovor.Show();
        }

        private void toolStripMenuItem2_Click_1(object sender, EventArgs e)
        {
            this.panel1.Controls.Clear();
            dogovor dogovor = new dogovor() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            dogovor.FormBorderStyle = FormBorderStyle.None;
            this.panel1.Controls.Add(dogovor);
            dogovor.Show();
        }

        private void toolStripMenuItem3_Click_1(object sender, EventArgs e)
        {
            this.panel1.Controls.Clear();
            statistika dogovor = new statistika() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            dogovor.FormBorderStyle = FormBorderStyle.None;
            this.panel1.Controls.Add(dogovor);
            dogovor.Show();
        }

        private void toolStripMenuItem4_Click_1(object sender, EventArgs e)
        {
            this.panel1.Controls.Clear();
            progznoz dogovor = new progznoz() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            dogovor.FormBorderStyle = FormBorderStyle.None;
            this.panel1.Controls.Add(dogovor);
            dogovor.Show();
        }

        private void toolStripMenuItem14_Click(object sender, EventArgs e)
        {
            help frm2 = new help();
            frm2.Show();
        }
    }
}
