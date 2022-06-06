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
    public partial class prodazhi__ : Form
    {
        public prodazhi__()
        {
            InitializeComponent();
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            groupBox4MK.Visible = false;
            groupBox1MZ.Visible = true;
            checkBox4.Visible=false;
            checkBox4.Checked=false;
            checkBox1.Visible = true;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            groupBox4MK.Visible = true;
            groupBox1MZ.Visible = false;
            checkBox4.Visible = true;
            checkBox1.Visible = false;
            checkBox1.Checked = false;

        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            groupBox4MK.Visible = false;
            groupBox3.Visible = false;
            groupBox2.Visible = false;
            groupBox1MZ.Visible = false;
            checkBox4.Visible = false;
            checkBox1.Visible = false;
            checkBox1.Checked = false;
            checkBox4.Checked = false;
            checkBox2.Visible = true;
            checkBox5.Checked = false;
            checkBox5.Visible = false;
            this.Height = 330;

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            groupBox4MK.Visible = true;
            groupBox1MZ.Visible = false;
            checkBox4.Visible = true;
            checkBox1.Visible = false;
            checkBox1.Checked = false;
            checkBox4.Checked = false;
            checkBox2.Visible = false;
            checkBox5.Visible = true;
            checkBox2.Checked = false;
            groupBox3.Visible = true;
            groupBox2.Visible = true;
            this.Height= 637;
            recepttovar();
        }
        private void recepttovar()
        {

        }
    }
}
