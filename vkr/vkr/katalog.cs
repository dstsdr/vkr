using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace vkr
{
    public partial class katalog : Form
    {
        public katalog()
        {
            InitializeComponent();
        }

        private void katalog_Load(object sender, EventArgs e)
        {
            dataset();
        }
        SqlConnection Connection = new SqlConnection(@"Data Source=LAPTOP-862V88EF\SQLEXPRESS;Initial Catalog=vkr;Integrated Security=True");

        private void dataset()
        {
            Connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT [Характеристики лекарств].[Код характеристики],Лекарства.Наименование, [Условие отпуска].Условие  AS [Условие отпуска], " +
                "[Фарм группа].Название AS [Фарм группа], [Форма выпуска].Форма  AS [Форма выпуска], " +
                "Производитель.Наименование  AS [Производитель], [Характеристики лекарств].Дозировка, Договор.Количество from[Характеристики лекарств] inner join (Лекарства inner join[Условие отпуска] ON" +
                " Лекарства.[Код условия] =[Условие отпуска].[Код условия] inner join[Фарм группа] ON Лекарства.[Код группы] =[Фарм группа].[Код группы]) ON Лекарства.[Код лекарства] " +
                "=[Характеристики лекарств].[Код лекарства] inner join[Форма выпуска] ON[Форма выпуска].[Код формы] =[Характеристики лекарств].[Код формы] inner join Производитель ON " +
                "Производитель.[Код производителя] =[Характеристики лекарств].[Код производителя] inner join Договор ON Договор.[Номер договора] =[Характеристики лекарств].[Номер договора]", Connection);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "info");
            dataGridView1.DataSource = ds.Tables[0];
            Connection.Close();
            int rows = dataGridView1.Rows.Count - 1;
            label1.Text = "Количество записей " + rows.ToString();
            kolichestvo();
        }
        private void kolichestvo()
        {
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                if (dataGridView1.Rows[i].Cells[3].Value.ToString() == "Безрецептурное")
                {
                    Connection.Open();
                    string sqlExpression = "SELECT [Безрецептурные продажи].Количество from [Безрецептурные продажи] WHERE [Безрецептурные продажи].[Код характеристики] ='" + dataGridView1.Rows[i].Cells[1].Value + "'";
                    SqlCommand command = new SqlCommand(sqlExpression, Connection);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        dataGridView1.Rows[i].Cells[8].Value = Convert.ToInt32(dataGridView1.Rows[i].Cells[8].Value) - Convert.ToInt32(reader.GetValue(0));
                    }
                    Connection.Close();
                }
                else
                {
                    Connection.Open();
                    string sqlExpression = "SELECT [Рецептурные продажи].Количество from [Рецептурные продажи] WHERE [Рецептурные продажи].[Код характеристики] = '" + dataGridView1.Rows[i].Cells[1].Value + "'";
                    SqlCommand command = new SqlCommand(sqlExpression, Connection);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        dataGridView1.Rows[i].Cells[8].Value = Convert.ToInt32(dataGridView1.Rows[i].Cells[8].Value) - Convert.ToInt32(reader.GetValue(0)); //
                    }
                    Connection.Close();
                }
            }
        }
        private void button9_Click(object sender, EventArgs e)
        {

            

            
        }
    }
}
