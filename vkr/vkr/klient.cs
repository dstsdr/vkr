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
    public partial class klient : Form
    {
        public klient()
        {
            InitializeComponent();
        }

        private void klient_Load(object sender, EventArgs e)
        {
            dataset();
        }

        SqlConnection Connection = new SqlConnection(@"Data Source=LAPTOP-862V88EF\SQLEXPRESS;Initial Catalog=vkr;Integrated Security=True");
        private void dataset()
        {
            Connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT [Клиент МК].[№], [Клиент МК].ФИО, [Клиент МК].[Дата рождения], [Клиент МК].[Номер мед.карты] as [№ мед.карты/адрес] " +
                "FROM[Клиент МК] " +
                "UNION " +
                "SELECT[Клиенты МЖ].[№], [Клиенты МЖ].ФИО, [Клиенты МЖ].[Дата рождения], CONCAT([Клиенты МЖ].Город, ' ', [Клиенты МЖ].Улица, ' ', [Клиенты МЖ].Дом) " +
                "FROM[Клиенты МЖ]", Connection);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "info");
            dataGridView1.DataSource = ds.Tables[0];
            Connection.Close();
            int rows = dataGridView1.Rows.Count - 1;
            label1.Text = "Количество клиентов: " + rows.ToString();
        }
    }
}
