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
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT COUNT([Серийный номер].[Код характеристики]) AS [Кол-во], Лекарства.Наименование, [Фарм группа].Название AS [Фарм группа], " +
                "[Форма выпуска].Форма AS [Форма выпуска], Производитель.Наименование AS[Производитель], [Характеристики лекарств].Дозировка, Лекарства.[Похожее лекарство] from[Характеристики лекарств] " +
                "inner join(Лекарства inner join[Условие отпуска] ON Лекарства.[Код условия] =[Условие отпуска].[Код условия] inner join[Фарм группа] ON Лекарства.[Код группы] =[Фарм группа].[Код группы]) " +
                "ON Лекарства.[Код лекарства] =[Характеристики лекарств].[Код лекарства] inner join[Форма выпуска] ON[Форма выпуска].[Код формы] =[Характеристики лекарств].[Код формы] inner join Производитель" +
                " ON Производитель.[Код производителя] =[Характеристики лекарств].[Код производителя] inner join[Серийный номер] ON[Характеристики лекарств].[Код характеристики] " +
                "=[Серийный номер].[Код характеристики] where[Серийный номер].[Срок годности] > GETDATE() and[Код безрецептурной продажи] is null and [Код рецептурной продажи] is null " +
                "GROUP BY[Характеристики лекарств].[Код характеристики], Лекарства.Наименование, [Фарм группа].Название, [Форма выпуска].Форма, Производитель.Наименование, " +
                "[Характеристики лекарств].Дозировка, Лекарства.[Похожее лекарство]", Connection);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "info");
            dataGridView1.DataSource = ds.Tables[0];
            Connection.Close();
            int rows = dataGridView1.Rows.Count - 1;
            label1.Text = "Количество записей " + rows.ToString();
            dataGridView1.Columns[7].Visible = false;
            // zvnlp();
        }
       
        private void zvnlp()
        {
          /*  bool a = false;
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                DateTime dt = DateTime.Now;
                if (Convert.ToDateTime(dataGridView1.Rows[i].Cells[10].Value) < dt)
                {
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Pink;
                    a=true;
                }
            }
            if (a==true) MessageBox.Show("В списке присутствуют просроченные товары");*/
            //жвнлм
            bool b=false;
            Connection.Open();
            string sqlExpression = "SELECT [Характеристики лекарств].[Код характеристики], [Необходимый минимум].[Код лекарства] from[Необходимый минимум] inner join[Характеристики лекарств] ON " +
                "[Необходимый минимум].[Код лекарства] =[Характеристики лекарств].[Код лекарства]";
            SqlCommand command = new SqlCommand(sqlExpression, Connection);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    if (Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value) == Convert.ToInt32(reader.GetValue(0)))
                    {
                        if (Convert.ToInt32(dataGridView1.Rows[i].Cells[9].Value) <= 1) b = true;
                        dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;

                    }
                }
            }
            Connection.Close();
            if (b == true) MessageBox.Show("Пополните товары из жвнлм");
        }
        private void button9_Click(object sender, EventArgs e)
        {

            

            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT [Характеристики лекарств].[Код характеристики] as '№',Лекарства.Наименование, [Условие отпуска].Условие  AS [Условие отпуска], " +
                "[Фарм группа].Название AS [Фарм группа], [Форма выпуска].Форма  AS [Форма выпуска], " +
                "Производитель.Наименование  AS [Производитель], [Характеристики лекарств].Дозировка, Договор.Количество, [Характеристики лекарств].[Срок годности] from[Характеристики лекарств] inner join (Лекарства inner join[Условие отпуска] ON" +
                " Лекарства.[Код условия] =[Условие отпуска].[Код условия] inner join[Фарм группа] ON Лекарства.[Код группы] =[Фарм группа].[Код группы]) ON Лекарства.[Код лекарства] " +
                "=[Характеристики лекарств].[Код лекарства] inner join[Форма выпуска] ON[Форма выпуска].[Код формы] =[Характеристики лекарств].[Код формы] inner join Производитель ON " +
                "Производитель.[Код производителя] =[Характеристики лекарств].[Код производителя] inner join Договор ON Договор.[Номер договора] =[Характеристики лекарств].[Номер договора] " +
                "WHERE Лекарства.[Похожее лекарство]= '" + dataGridView1.CurrentRow.Cells[9].Value + "'", Connection);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "info");
            dataGridView2.DataSource = ds.Tables[0];
            Connection.Close();
          //  int rows = dataGridView1.Rows.Count - 1;
           // label1.Text = "Количество записей " + rows.ToString();//9
        }
    }
}
