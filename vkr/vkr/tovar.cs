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
    public partial class tovar : Form
    {
        public tovar()
        {
            InitializeComponent();
        }
        SqlConnection Connection = new SqlConnection(@"Data Source=LAPTOP-862V88EF\SQLEXPRESS;Initial Catalog=vkr;Integrated Security=True");

        private void tovar_Load(object sender, EventArgs e)
        {
            dataset();
            cenoobrazovanie();
        }
        private void dataset()
        {
            Connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT [Серийный номер].[Серийный номер], [Серийный номер].[Срок годности], [Характеристики лекарств].Дозировка," +
                " [Характеристики лекарств].[Цена],[Характеристики лекарств].Наценка," +
                " Наценка.Наценка as [Максимальная наценка], Лекарства.Наименование, [Фарм группа].Название AS[Фарм группа], [Форма выпуска].Форма" +
                " AS[Форма выпуска], Производитель.Наименование AS[Производитель], [Условие отпуска].Условие from[Характеристики лекарств] inner join(Лекарства inner join[Условие отпуска] ON " +
                "Лекарства.[Код условия] =[Условие отпуска].[Код условия] inner join[Фарм группа] ON Лекарства.[Код группы] =[Фарм группа].[Код группы] inner join Наценка ON " +
                "Наценка.[Код наценки] = Лекарства.[Код наценки]) ON Лекарства.[Код лекарства] =[Характеристики лекарств].[Код лекарства] inner join[Форма выпуска] ON" +
                "[Форма выпуска].[Код формы] =[Характеристики лекарств].[Код формы] inner join Производитель ON Производитель.[Код производителя] =[Характеристики лекарств].[Код производителя] " +
                "inner join[Серийный номер] ON[Характеристики лекарств].[Код характеристики] =[Серийный номер].[Код характеристики] WHERE [Серийный номер].[Код рецептурной продажи] " +
                "is null and [Серийный номер].[Код безрецептурной продажи] is null", Connection);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "info");
            dataGridView1.DataSource = ds.Tables[0];
            Connection.Close();
            int rows = dataGridView1.Rows.Count - 1;
            label1.Text = "Количество записей " + rows.ToString();
            dataGridView1.Columns[7].Visible = false;
        }
        private void cenoobrazovanie()
        {
           /* Connection.Open();
            int a = 0;
            string sqlExpression = "";
            SqlCommand command = new SqlCommand(sqlExpression, Connection);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows) //данные из банка и договора
            {
                while (reader.Read())
                {
                    if (a == 1)
                    {
                        var namepost = reader.GetValue(0).ToString(); //поставщик
                        var contactpost = reader.GetValue(1).ToString();
                        var pochtapost = reader.GetValue(2).ToString();
                        var uridichpost = reader.GetValue(3).ToString();
                        var INNpost = reader.GetValue(4).ToString();
                        var kpppost = reader.GetValue(5).ToString();
                        var raspost = reader.GetValue(6).ToString();
                        var korpost = reader.GetValue(7).ToString();
                        var bikPost = reader.GetValue(8).ToString();
                        var bankpost = reader.GetValue(9).ToString();
                     
                    }
                    else
                    {
                        var nameapteka = reader.GetValue(0).ToString();
                        var contactapteka = reader.GetValue(1).ToString();
                        var pochtaapteka = reader.GetValue(2).ToString();
                        var uridichapteka = reader.GetValue(3).ToString();
                        var INNapteka = reader.GetValue(4).ToString();
                        var kppapteka = reader.GetValue(5).ToString();
                        var rasapteka = reader.GetValue(6).ToString();
                        var korapteka = reader.GetValue(7).ToString();
                        var bikapteka = reader.GetValue(8).ToString();
                        var bankapteka = reader.GetValue(9).ToString();
                    }
                    a++;
                }*/
            }

        private void button1_Click(object sender, EventArgs e)
        {
            tovar__ frm2 = new tovar__();           
            frm2.Show();
        }
    }
}
