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
            nacenka frm2 = new nacenka();
            frm2.Show();
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

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            prodazhi frm2 = new prodazhi();
            frm2.Show();
        }

        private void скидкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sale frm2 = new sale();
            frm2.Show();
        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            znvlm frm2 = new znvlm();
            frm2.Show();
        }

        private void toolStripMenuItem11_Click(object sender, EventArgs e)
        {
            postav frm2 = new postav();
            frm2.Show();
        }

        private void toolStripMenuItem12_Click(object sender, EventArgs e)
        {
            proizv frm2 = new proizv();
            frm2.Show();
        }

        private void банкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            prodazhi frm2 = new prodazhi();
            frm2.Show();
        }

        private void toolStripMenuItem10_Click(object sender, EventArgs e)
        {
            vrach frm2 = new vrach();
            frm2.Show();
        }

        private void toolStripMenuItem13_Click(object sender, EventArgs e)
        {
            klient frm2 = new klient();
            frm2.Show();
        }

        private void сотрудникиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sotr frm2 = new sotr();
            frm2.Show();
        }

        private void должностиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dolzn frm2 = new dolzn();
            frm2.Show();
        }

        private void формаВыпускаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            forma frm2 = new forma();
            frm2.Show();
        }

        private void фармакологическаяГруппаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            farm frm2 = new farm();
            frm2.Show();
        }

        private void нДСToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nds frm2 = new nds();
            frm2.Show();
        }

        private void банкиToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            bank frm2 = new bank();
            frm2.Show();
        }

        private void аптекиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            apteka frm2 = new apteka();
            frm2.Show();
        }
    }
}
