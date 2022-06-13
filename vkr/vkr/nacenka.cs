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
    public partial class nacenka : Form
    {
        public nacenka()
        {
            InitializeComponent();
        }

        private void nacenka_Load(object sender, EventArgs e)
        {
            dataset();
        }
        SqlConnection Connection = new SqlConnection(@"Data Source=LAPTOP-862V88EF\SQLEXPRESS;Initial Catalog=vkr;Integrated Security=True");
        private void dataset()
        {
            Connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT Наценка.* " +
                "FROM Наценка", Connection);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "info");
            dataGridView1.DataSource = ds.Tables[0];
            Connection.Close();
            int rows = dataGridView1.Rows.Count - 1;
            label1.Text = "Количество наценок: " + rows.ToString();
            dataGridView1.Columns[0].Visible = false;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
