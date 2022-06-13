using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace vkr
{
    public partial class proizv : Form
    {
        public proizv()
        {
            InitializeComponent();
        }

        private void proizv_Load(object sender, EventArgs e)
        {
            dataset();
        }
        SqlConnection Connection = new SqlConnection(@"Data Source=LAPTOP-862V88EF\SQLEXPRESS;Initial Catalog=vkr;Integrated Security=True");
        private void dataset()
        {
            Connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT Производитель.* " +
                "FROM Производитель", Connection);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "info");
            dataGridView1.DataSource = ds.Tables[0];
            Connection.Close();
            dataGridView1.Columns[0].Visible = false;
            int rows = dataGridView1.Rows.Count - 1;
            label1.Text = "Количество производителей: " + rows.ToString();
        }
    }
}
