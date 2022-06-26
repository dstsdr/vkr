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
                "[Форма выпуска].Форма AS[Форма выпуска], [Характеристики лекарств].Дозировка,[Единицы измерения].Обозначение, Производитель.Наименование AS[Производитель], " +
                " [Условие отпуска].Условие, Лекарства.[Похожее лекарство], [Характеристики лекарств].Цена, [Характеристики лекарств].[Код характеристики]  " +
                "from[Характеристики лекарств] " +
                "inner join(Лекарства inner join[Условие отпуска] ON Лекарства.[Код условия] =[Условие отпуска].[Код условия]" +
                " inner join[Фарм группа] ON Лекарства.[Код группы] =[Фарм группа].[Код группы]) ON Лекарства.[Код лекарства] =[Характеристики лекарств].[Код лекарства] " +
                "inner join ([Форма выпуска] inner join[Единицы измерения] ON[Единицы измерения].[Код ед.изм] =[Форма выпуска].[Код ед.изм])ON[Форма выпуска].[Код формы] =[Характеристики лекарств].[Код формы] " +
                "inner join Производитель ON Производитель.[Код производителя] =[Характеристики лекарств].[Код производителя]" +
                " inner join [Серийный номер] ON[Характеристики лекарств].[Код характеристики] =[Серийный номер].[Код характеристики] " +
                "where[Серийный номер].[Срок годности] > GETDATE() and[Код безрецептурной продажи] is null and [Код рецептурной продажи] is null " +
                "GROUP BY[Характеристики лекарств].[Код характеристики], Лекарства.Наименование, [Фарм группа].Название, [Форма выпуска].Форма, Производитель.Наименование, [Характеристики лекарств].Дозировка, " +
                "Лекарства.[Похожее лекарство],[Единицы измерения].Обозначение, [Условие отпуска].Условие, [Характеристики лекарств].Цена", Connection);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "info");
            dataGridView1.DataSource = ds.Tables[0];
            Connection.Close();
            int rows = dataGridView1.Rows.Count - 1;
            label1.Text = "Количество товаров: " + rows.ToString();
            dataGridView1.Columns[8].Visible = false;
            dataGridView1.Columns[10].Visible = false;
        }

        private void zvnlp()
        {
            bool b=false;
            Connection.Open();
            string sqlExpression = "SELECT [Характеристики лекарств].[Код характеристики], [Необходимый минимум].[Код лекарства] " +
                "from[Необходимый минимум] " +
                "inner join[Характеристики лекарств] ON [Необходимый минимум].[Код лекарства] =[Характеристики лекарств].[Код лекарства]";
            SqlCommand command = new SqlCommand(sqlExpression, Connection);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    if (Convert.ToInt32(dataGridView1.Rows[i].Cells[10].Value) == Convert.ToInt32(reader.GetValue(0)))
                    {
                        if (Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value) <= 2) b = true;
                        dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                    }
                }
            }
            Connection.Close();
            if (b == true) MessageBox.Show("Пополните товары из жнвлп");
        }
        private void button9_Click(object sender, EventArgs e)
        {

            

            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT [Характеристики лекарств].[Код характеристики] as '№',Лекарства.Наименование, " +
                "[Условие отпуска].Условие  AS [Условие отпуска], [Фарм группа].Название AS[Фарм группа], [Форма выпуска].Форма  AS[Форма выпуска], " +
                "Производитель.Наименование  AS[Производитель], CONCAT([Характеристики лекарств].Дозировка, ' ', [Единицы измерения].Обозначение)  as Дозировка," +
                "Договор.Количество, [Характеристики лекарств].Цена " +
                "from[Характеристики лекарств] inner join(Лекарства inner join[Условие отпуска] ON Лекарства.[Код условия] =[Условие отпуска].[Код условия] " +
                "inner join[Фарм группа] ON Лекарства.[Код группы] =[Фарм группа].[Код группы]) ON Лекарства.[Код лекарства] =[Характеристики лекарств].[Код лекарства] " +
                "inner join([Форма выпуска] " +
                "inner join [Единицы измерения] ON[Единицы измерения].[Код ед.изм]=[Форма выпуска].[Код ед.изм])" +
                "ON[Форма выпуска].[Код формы] =[Характеристики лекарств].[Код формы] " +
                "inner join Производитель ON Производитель.[Код производителя] =[Характеристики лекарств].[Код производителя] " +
                "inner join Договор ON Договор.[Номер договора] =[Характеристики лекарств].[Номер договора] " +
                "WHERE Лекарства.[Похожее лекарство]='" + dataGridView1.CurrentRow.Cells[8].Value + "'", Connection);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "info");
            dataGridView2.DataSource = ds.Tables[0];
            Connection.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int l = 0;
            Connection.Open();
            SqlCommand cmd8 = Connection.CreateCommand();
            cmd8.CommandType = CommandType.Text;
            cmd8.CommandText = "SELECT [Характеристики лекарств].[Код характеристики] as Код " +
                "from[Характеристики лекарств] " +
                "inner join[Форма выпуска] ON[Характеристики лекарств].[Код формы]=[Форма выпуска].[Код формы] " +
                "inner join(Лекарства " +
                "inner join[Условие отпуска] ON [Условие отпуска].[Код условия]= Лекарства.[Код условия]) " +
                "ON Лекарства.[Код лекарства]=[Характеристики лекарств].[Код лекарства] " +
                "inner join Производитель ON Производитель.[Код производителя] = [Характеристики лекарств].[Код производителя] " +
                "where[Лекарства].Наименование = '"+ dataGridView1.CurrentRow.Cells[1].Value+"' and [Форма выпуска].Форма = '"+dataGridView1.CurrentRow.Cells[3].Value +"' " +
                "and [Характеристики лекарств].Дозировка = "+dataGridView1.CurrentRow.Cells[4].Value + " and Производитель.Наименование = '"+dataGridView1.CurrentRow.Cells[6].Value + "' " +
                "and [Условие отпуска].Условие = '"+ dataGridView1.CurrentRow.Cells[7].Value+"' " + "and [Характеристики лекарств].Цена ="+ dataGridView1.CurrentRow.Cells[9].Value.ToString().Replace(",", ".");
            cmd8.ExecuteNonQuery();
            DataTable dt8 = new DataTable();
            SqlDataAdapter da8 = new SqlDataAdapter(cmd8);
            da8.Fill(dt8);
            l = Convert.ToInt32(dt8.Rows[0]["Код"]);
            Connection.Close();
            prodazhi__ frm2 = new prodazhi__();
            frm2.button2.Visible = false;
            frm2.label15.Text = l.ToString();
            if (dataGridView1.CurrentRow.Cells[7].Value.ToString() == "Рецептурное") 
            {
                frm2.checkBox2.Checked = true;
            } 
            else frm2.checkBox5.Checked = true;
            frm2.button3.Visible = true;
            frm2.Show();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                checkBox6.Checked = false;
                int s = Convert.ToInt32(dataGridView1.CurrentCell.RowIndex);
                Connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT [Серийный номер].[Серийный номер], [Серийный номер].[Срок годности], [Характеристики лекарств].Наценка " +
                    "as [Текущая наценка], Наценка.Наценка as [Максимальная наценка], Лекарства.Наименование, CONCAT(Скидки.Размер, '% от ', Скидки.[Минимальное количество]," +
                    " ' шт.') as Скидки " +
                    "from[Характеристики лекарств] " +
                    "inner join(Лекарства " +
                    "inner join[Условие отпуска] ON Лекарства.[Код условия] = [Условие отпуска].[Код условия] " +
                    "inner join[Фарм группа] ON Лекарства.[Код группы] =[Фарм группа].[Код группы] " +
                    "inner join Наценка ON Наценка.[Код наценки] = Лекарства.[Код наценки]) " +
                    "ON Лекарства.[Код лекарства] =[Характеристики лекарств].[Код лекарства] " +
                    "inner join[Форма выпуска] ON [Форма выпуска].[Код формы] =[Характеристики лекарств].[Код формы] " +
                    "inner join Производитель ON Производитель.[Код производителя] = [Характеристики лекарств].[Код производителя] " +
                    "inner join[Серийный номер] ON[Характеристики лекарств].[Код характеристики] = [Серийный номер].[Код характеристики] " +
                    "inner join Скидки ON Скидки.[Код характеристики] =[Характеристики лекарств].[Код характеристики] " +
                    "WHERE[Серийный номер].[Код рецептурной продажи] is null and [Серийный номер].[Код безрецептурной продажи] is null and " +
                    "Лекарства.Наименование = '"+ Convert.ToString(dataGridView1[1, s].Value) + "' and[Форма выпуска].Форма = '"+ Convert.ToString(dataGridView1[3, s].Value) + "' " +
                    "and[Характеристики лекарств].Дозировка = "+ Convert.ToString(dataGridView1[4, s].Value) + " and Производитель.Наименование = '"+ Convert.ToString(dataGridView1[6, s].Value) + "'", Connection);
                DataSet ds2 = new DataSet();
                adapter.Fill(ds2, "info");
                dataGridView2.DataSource = ds2.Tables[0];
                Connection.Close();
            }
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox6.Checked == true)
            {
                checkBox1.Checked = false;
                int s = Convert.ToInt32(dataGridView1.CurrentCell.RowIndex);
                Connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT Производитель.* " +
                    "from Производитель " +
                    "where Производитель.Наименование = '" + Convert.ToString(dataGridView1[6, s].Value) + "'", Connection);
                DataSet ds2 = new DataSet();
                adapter.Fill(ds2, "info");
                dataGridView2.DataSource = ds2.Tables[0];
                Connection.Close();
            }
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.CheckState == CheckState.Checked)
            {
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT COUNT([Серийный номер].[Код характеристики]) AS [Кол-во], Лекарства.Наименование, [Фарм группа].Название AS [Фарм группа]," +
                    "[Форма выпуска].Форма AS[Форма выпуска], [Характеристики лекарств].Дозировка,[Единицы измерения].Обозначение, Производитель.Наименование AS[Производитель], " +
                    "[Условие отпуска].Условие, Лекарства.[Похожее лекарство], [Характеристики лекарств].Цена " +
                    "from[Характеристики лекарств] " +
                    "inner join(Лекарства inner join[Условие отпуска] ON Лекарства.[Код условия] =[Условие отпуска].[Код условия] " +
                    "inner join[Фарм группа] ON Лекарства.[Код группы] =[Фарм группа].[Код группы]) ON Лекарства.[Код лекарства]=[Характеристики лекарств].[Код лекарства] " +
                    "inner join ([Форма выпуска] " +
                    "inner join[Единицы измерения] ON[Единицы измерения].[Код ед.изм] =[Форма выпуска].[Код ед.изм])" +
                    "ON[Форма выпуска].[Код формы] = [Характеристики лекарств].[Код формы] " +
                    "inner join Производитель ON Производитель.[Код производителя] = [Характеристики лекарств].[Код производителя] " +
                    "inner join [Серийный номер] ON[Характеристики лекарств].[Код характеристики] =[Серийный номер].[Код характеристики] " +
                    "where[Серийный номер].[Срок годности] > GETDATE() and[Код безрецептурной продажи] is null and [Код рецептурной продажи] is null and [Условие отпуска].Условие = " +
                    "'Рецептурное' " +
                    "GROUP BY[Характеристики лекарств].[Код характеристики], Лекарства.Наименование, [Фарм группа].Название, [Форма выпуска].Форма, " +
                    "Производитель.Наименование, [Характеристики лекарств].Дозировка, Лекарства.[Похожее лекарство], [Единицы измерения].Обозначение, [Условие отпуска].Условие, " +
                    "[Характеристики лекарств].Цена", Connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "info");
                dataGridView1.DataSource = ds.Tables[0];
                Connection.Close();
                int rows = dataGridView1.Rows.Count - 1;
                label1.Text = "Количество записей " + rows.ToString();
                checkBox4.CheckState = CheckState.Unchecked;
                checkBox7.CheckState = CheckState.Unchecked;
            }
            else { dataset(); }
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox7.CheckState == CheckState.Checked)
            {
                checkBox4.CheckState = CheckState.Unchecked;
                checkBox5.CheckState = CheckState.Unchecked;
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT COUNT([Серийный номер].[Код характеристики]) AS [Кол-во], Лекарства.Наименование, [Фарм группа].Название AS [Фарм группа]," +
                    "[Форма выпуска].Форма AS[Форма выпуска], [Характеристики лекарств].Дозировка,[Единицы измерения].Обозначение, Производитель.Наименование AS[Производитель], " +
                    "[Условие отпуска].Условие, Лекарства.[Похожее лекарство], [Характеристики лекарств].Цена, [Необходимый минимум].[Код лекарства] " +
                    "from[Характеристики лекарств] " +
                    "inner join (Лекарства " +
                    "inner join[Условие отпуска] ON Лекарства.[Код условия] =[Условие отпуска].[Код условия] " +
                    "inner join[Фарм группа] ON Лекарства.[Код группы] =" + "[Фарм группа].[Код группы] " +
                    "inner join[Необходимый минимум] on[Необходимый минимум].[Код лекарства] = Лекарства.[Код лекарства]) " +
                    "ON Лекарства.[Код лекарства] =" +  "[Характеристики лекарств].[Код лекарства] " +
                    "inner join ([Форма выпуска] " +
                    "inner join[Единицы измерения] ON[Единицы измерения].[Код ед.изм] =[Форма выпуска].[Код ед.изм])" +
                    "ON[Форма выпуска].[Код формы] = [Характеристики лекарств].[Код формы] " +
                    "inner join Производитель ON Производитель.[Код производителя] = [Характеристики лекарств].[Код производителя] " +
                    "inner join [Серийный номер] ON[Характеристики лекарств].[Код характеристики] =[Серийный номер].[Код характеристики] " +
                    "where[Серийный номер].[Срок годности] > GETDATE() and[Код безрецептурной продажи] is null and [Код рецептурной продажи] is null and [Необходимый минимум].[Код лекарства]" +
                    " is not null " +
                    "GROUP BY[Характеристики лекарств].[Код характеристики], Лекарства.Наименование, [Фарм группа].Название, [Форма выпуска].Форма, Производитель.Наименование," +
                    " [Характеристики лекарств].Дозировка, Лекарства.[Похожее лекарство], [Единицы измерения].Обозначение, [Условие отпуска].Условие, [Характеристики лекарств].Цена, " +
                    "[Необходимый минимум].[Код лекарства]", Connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "info");
                dataGridView1.DataSource = ds.Tables[0];
                Connection.Close();
                int rows = dataGridView1.Rows.Count - 1;
                label1.Text = "Количество записей " + rows.ToString();
                dataGridView1.Columns["Код лекарства"].Visible = false;
            }
            else { dataset(); }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.CheckState == CheckState.Checked)
            {
                checkBox5.CheckState = CheckState.Unchecked;
                checkBox7.CheckState = CheckState.Unchecked;
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT COUNT([Серийный номер].[Код характеристики]) AS [Кол-во], Лекарства.Наименование, [Фарм группа].Название AS [Фарм группа]," +
                    "[Форма выпуска].Форма AS[Форма выпуска], [Характеристики лекарств].Дозировка,[Единицы измерения].Обозначение, Производитель.Наименование AS[Производитель], " +
                    "[Условие отпуска].Условие, Лекарства.[Похожее лекарство], [Характеристики лекарств].Цена from[Характеристики лекарств] inner join(Лекарства inner join[Условие отпуска] " +
                    "ON Лекарства.[Код условия] =[Условие отпуска].[Код условия] inner join[Фарм группа] ON Лекарства.[Код группы] =[Фарм группа].[Код группы]) ON Лекарства.[Код лекарства] " +
                    "=[Характеристики лекарств].[Код лекарства] inner join ([Форма выпуска] inner join[Единицы измерения] ON[Единицы измерения].[Код ед.изм] =[Форма выпуска].[Код ед.изм])" +
                    "ON[Форма выпуска].[Код формы] = [Характеристики лекарств].[Код формы] inner join Производитель ON Производитель.[Код производителя] =" +
                    "[Характеристики лекарств].[Код производителя] inner join [Серийный номер] ON[Характеристики лекарств].[Код характеристики] =[Серийный номер].[Код характеристики] " +
                    "where[Серийный номер].[Срок годности] > GETDATE() and[Код безрецептурной продажи] is null and [Код рецептурной продажи] is null and [Условие отпуска].Условие = " +
                    "'Безрецептурное' GROUP BY[Характеристики лекарств].[Код характеристики], Лекарства.Наименование, [Фарм группа].Название, [Форма выпуска].Форма, " +
                    "Производитель.Наименование, [Характеристики лекарств].Дозировка, Лекарства.[Похожее лекарство], [Единицы измерения].Обозначение, [Условие отпуска].Условие, " +
                    "[Характеристики лекарств].Цена", Connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "info");
                dataGridView1.DataSource = ds.Tables[0];
                Connection.Close();
                int rows = dataGridView1.Rows.Count - 1;
                label1.Text = "Количество записей " + rows.ToString();
            }
            else { dataset(); }
        }

        private void katalog_Shown(object sender, EventArgs e)
        {
            zvnlp();
        }
    }
}
