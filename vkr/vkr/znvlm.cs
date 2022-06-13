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
    public partial class znvlm : Form
    {
        public znvlm()
        {
            InitializeComponent();
        }

        private void znvlm_Load(object sender, EventArgs e)
        {
            dataset();
        }
        SqlConnection Connection = new SqlConnection(@"Data Source=LAPTOP-862V88EF\SQLEXPRESS;Initial Catalog=vkr;Integrated Security=True");
        private void dataset()
        {
            Connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT Лекарства.Наименование as Лекарсвто, [Условие отпуска].Условие as [Условие отпуска], [Фарм группа].Название as [Фарм группа]" +
                "FROM[Необходимый минимум] INNER JOIN(Лекарства inner join[Условие отпуска] ON[Условие отпуска].[Код условия] = Лекарства.[Код условия] " +
                "inner join[Фарм группа] on[Фарм группа].[Код группы] = Лекарства.[Код группы]) ON[Необходимый минимум].[Код лекарства] = Лекарства.[Код лекарства]", Connection);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "info");
            dataGridView1.DataSource = ds.Tables[0];
            Connection.Close();
            int rows = dataGridView1.Rows.Count - 1;
            label1.Text = "Количество лекарственных средств: " + rows.ToString();
        }
    }
}
