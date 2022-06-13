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
    public partial class sale : Form
    {
        public sale()
        {
            InitializeComponent();
        }

        private void sale_Load(object sender, EventArgs e)
        {
            dataset();
        }
        SqlConnection Connection = new SqlConnection(@"Data Source=LAPTOP-862V88EF\SQLEXPRESS;Initial Catalog=vkr;Integrated Security=True");
        private void dataset()
        {
            Connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT CONCAT(Лекарства.Наименование, ' ', [Форма выпуска].Форма, ' ', [Характеристики лекарств].Дозировка,' ', " +
                "[Единицы измерения].Обозначение) as Товар, Скидки.* from[Характеристики лекарств] INNER JOIN([Форма выпуска] INNER JOIN[Единицы измерения] " +
                "ON[Единицы измерения].[Код ед.изм] =[Форма выпуска].[Код ед.изм]) ON[Форма выпуска].[Код формы] =[Характеристики лекарств].[Код формы] INNER JOIN Лекарства ON " +
                "Лекарства.[Код лекарства] =[Характеристики лекарств].[Код лекарства] INNER JOIN Скидки ON Скидки.[Код характеристики] =" +
                "[Характеристики лекарств].[Код характеристики]", Connection);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "info");
            dataGridView1.DataSource = ds.Tables[0];
            Connection.Close();
            int rows = dataGridView1.Rows.Count - 1;
            label1.Text = "Количество cотрудников: " + rows.ToString();
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[4].HeaderText = "Скидка (%)";
        }
    }
}
