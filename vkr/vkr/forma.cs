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
    public partial class forma : Form
    {
        public forma()
        {
            InitializeComponent();
        }

        private void forma_Load(object sender, EventArgs e)
        {
            dataset();
        }
        SqlConnection Connection = new SqlConnection(@"Data Source=LAPTOP-862V88EF\SQLEXPRESS;Initial Catalog=vkr;Integrated Security=True");
        private void dataset()
        {
            Connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT [Форма выпуска].*, [Единицы измерения].Обозначение as [Единицы измерения] " +
                "FROM[Форма выпуска] inner join[Единицы измерения] ON[Единицы измерения].[Код ед.изм] =[Форма выпуска].[Код ед.изм]", Connection);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "info");
            dataGridView1.DataSource = ds.Tables[0];
            Connection.Close();
            int rows = dataGridView1.Rows.Count - 1;
            label1.Text = "Количество форм: " + rows.ToString();
            dataGridView1.Columns[0].Visible=false;
            dataGridView1.Columns[2].Visible = false;
        }
    }
}
