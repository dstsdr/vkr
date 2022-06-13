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
                "is null and [Серийный номер].[Код безрецептурной продажи] is null  and [Серийный номер].[Срок годности]>GETDATE()", Connection);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "info");
            dataGridView1.DataSource = ds.Tables[0];
            Connection.Close();
            int rows = dataGridView1.Rows.Count - 1;
            label1.Text = "Количество товаров: " + rows.ToString();
            dataGridView1.Columns[7].Visible = false;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            tovar__ frm2 = new tovar__();           
            frm2.Show();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                checkBox6.Checked = false;
                int s = Convert.ToInt32(dataGridView1.CurrentCell.Value);
                Connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT Производитель.* " +
                    "from[Характеристики лекарств] inner join[Серийный номер] ON[Серийный номер].[Код характеристики] =[Характеристики лекарств].[Код характеристики] " +
                    "inner join Производитель ON Производитель.[Код производителя] =[Характеристики лекарств].[Код производителя] " +
                    "WHERE[Серийный номер].[Серийный номер] = " + s, Connection);
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
                int s = Convert.ToInt32(dataGridView1.CurrentCell.Value);
                Connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT Поставщик.* " +
                    "from[Характеристики лекарств] inner join[Серийный номер] ON[Серийный номер].[Код характеристики] =[Характеристики лекарств].[Код характеристики] " +
                    "inner join(Договор inner join Поставщик on Поставщик.[ИНН поставщика] = Договор.[ИНН поставщика]) ON Договор.[Номер договора] = [Характеристики лекарств].[Номер договора]" +
                    " WHERE[Серийный номер].[Серийный номер] = " + s, Connection);
                DataSet ds2 = new DataSet();
                adapter.Fill(ds2, "info");
                dataGridView2.DataSource = ds2.Tables[0];
                Connection.Close();
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.CheckState == CheckState.Checked)
            {
                checkBox4.CheckState = CheckState.Unchecked;
                checkBox7.CheckState = CheckState.Unchecked;
                checkBox8.CheckState = CheckState.Unchecked;
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT [Серийный номер].[Серийный номер], [Серийный номер].[Срок годности], [Характеристики лекарств].Дозировка, " +
                    "[Характеристики лекарств].[Цена],[Характеристики лекарств].Наценка, Наценка.Наценка as [Максимальная наценка], Лекарства.Наименование, [Фарм группа].Название " +
                    "AS[Фарм группа], [Форма выпуска].Форма AS[Форма выпуска], Производитель.Наименование AS[Производитель], [Условие отпуска].Условие from[Характеристики лекарств]" +
                    " inner join(Лекарства inner join[Условие отпуска] ON Лекарства.[Код условия] =[Условие отпуска].[Код условия] inner join[Фарм группа] ON Лекарства.[Код группы] =" +
                    "[Фарм группа].[Код группы] inner join Наценка ON Наценка.[Код наценки] = Лекарства.[Код наценки]) ON Лекарства.[Код лекарства] =[Характеристики лекарств].[Код лекарства]" +
                    " inner join[Форма выпуска] ON [Форма выпуска].[Код формы] =[Характеристики лекарств].[Код формы] inner join Производитель ON Производитель.[Код производителя] " +
                    "=[Характеристики лекарств].[Код производителя] inner join[Серийный номер] ON[Характеристики лекарств].[Код характеристики] =[Серийный номер].[Код характеристики] " +
                    "WHERE[Серийный номер].[Код рецептурной продажи] is null and [Серийный номер].[Код безрецептурной продажи] is null and [Условие отпуска].Условие = 'Рецептурное' " +
                    "and[Серийный номер].[Срок годности] > GETDATE() ", Connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "info");
                dataGridView1.DataSource = ds.Tables[0];
                Connection.Close();
                int rows = dataGridView1.Rows.Count - 1;
                label1.Text = "Количество записей " + rows.ToString();
                

            }
            else { dataset(); }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.CheckState == CheckState.Checked)
            {
                checkBox5.CheckState = CheckState.Unchecked;
                checkBox7.CheckState = CheckState.Unchecked;
                checkBox8.CheckState = CheckState.Unchecked;
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT [Серийный номер].[Серийный номер], [Серийный номер].[Срок годности], [Характеристики лекарств].Дозировка, " +
                    "[Характеристики лекарств].[Цена],[Характеристики лекарств].Наценка, Наценка.Наценка as [Максимальная наценка], Лекарства.Наименование, [Фарм группа].Название " +
                    "AS[Фарм группа], [Форма выпуска].Форма AS[Форма выпуска], Производитель.Наименование AS[Производитель], [Условие отпуска].Условие from[Характеристики лекарств]" +
                    " inner join(Лекарства inner join[Условие отпуска] ON Лекарства.[Код условия] =[Условие отпуска].[Код условия] inner join[Фарм группа] ON Лекарства.[Код группы] =" +
                    "[Фарм группа].[Код группы] inner join Наценка ON Наценка.[Код наценки] = Лекарства.[Код наценки]) ON Лекарства.[Код лекарства] =[Характеристики лекарств].[Код лекарства]" +
                    " inner join[Форма выпуска] ON [Форма выпуска].[Код формы] =[Характеристики лекарств].[Код формы] inner join Производитель ON Производитель.[Код производителя] " +
                    "=[Характеристики лекарств].[Код производителя] inner join[Серийный номер] ON[Характеристики лекарств].[Код характеристики] =[Серийный номер].[Код характеристики] " +
                    "WHERE[Серийный номер].[Код рецептурной продажи] is null and [Серийный номер].[Код безрецептурной продажи] is null and [Условие отпуска].Условие = 'Безрецептурное' " +
                    "and[Серийный номер].[Срок годности] > GETDATE() ", Connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "info");
                dataGridView1.DataSource = ds.Tables[0];
                Connection.Close();
                int rows = dataGridView1.Rows.Count - 1;
                label1.Text = "Количество записей " + rows.ToString();
            }
            else { dataset(); }
        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox8.CheckState == CheckState.Checked)
            {
                checkBox5.CheckState = CheckState.Unchecked;
                checkBox7.CheckState = CheckState.Unchecked;
                checkBox4.CheckState = CheckState.Unchecked;
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT [Серийный номер].[Серийный номер], [Серийный номер].[Срок годности], [Характеристики лекарств].Дозировка," +
                " [Характеристики лекарств].[Цена],[Характеристики лекарств].Наценка," +
                " Наценка.Наценка as [Максимальная наценка], Лекарства.Наименование, [Фарм группа].Название AS[Фарм группа], [Форма выпуска].Форма" +
                " AS[Форма выпуска], Производитель.Наименование AS[Производитель], [Условие отпуска].Условие from[Характеристики лекарств] inner join(Лекарства inner join[Условие отпуска] ON " +
                "Лекарства.[Код условия] =[Условие отпуска].[Код условия] inner join[Фарм группа] ON Лекарства.[Код группы] =[Фарм группа].[Код группы] inner join Наценка ON " +
                "Наценка.[Код наценки] = Лекарства.[Код наценки]) ON Лекарства.[Код лекарства] =[Характеристики лекарств].[Код лекарства] inner join[Форма выпуска] ON" +
                "[Форма выпуска].[Код формы] =[Характеристики лекарств].[Код формы] inner join Производитель ON Производитель.[Код производителя] =[Характеристики лекарств].[Код производителя] " +
                "inner join[Серийный номер] ON[Характеристики лекарств].[Код характеристики] =[Серийный номер].[Код характеристики] WHERE [Серийный номер].[Код рецептурной продажи] " +
                "is null and [Серийный номер].[Код безрецептурной продажи] is null  and [Серийный номер].[Срок годности]<GETDATE() ", Connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "info");
                dataGridView1.DataSource = ds.Tables[0];
                Connection.Close();
                int rows = dataGridView1.Rows.Count - 1;
                label1.Text = "Количество записей " + rows.ToString();
            }
            else { dataset(); }
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox7.CheckState == CheckState.Checked)
            {
                checkBox5.CheckState = CheckState.Unchecked;
                checkBox8.CheckState = CheckState.Unchecked;
                checkBox4.CheckState = CheckState.Unchecked;
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT [Серийный номер].[Серийный номер], [Серийный номер].[Срок годности], [Характеристики лекарств].Дозировка, " +
                    "[Характеристики лекарств].[Цена],[Характеристики лекарств].Наценка, Наценка.Наценка as [Максимальная наценка], Лекарства.Наименование, [Фарм группа].Название " +
                    "AS[Фарм группа], [Форма выпуска].Форма AS[Форма выпуска], Производитель.Наименование AS[Производитель], [Условие отпуска].Условие, [Необходимый минимум].[Код лекарства] " +
                    "from[Характеристики лекарств] inner join(Лекарства inner join[Условие отпуска] ON Лекарства.[Код условия] =[Условие отпуска].[Код условия] inner join[Фарм группа] " +
                    "ON Лекарства.[Код группы] =[Фарм группа].[Код группы] inner join Наценка ON Наценка.[Код наценки] = Лекарства.[Код наценки] inner join[Необходимый минимум] " +
                    "ON[Необходимый минимум].[Код лекарства] = Лекарства.[Код лекарства]) ON Лекарства.[Код лекарства] =[Характеристики лекарств].[Код лекарства] inner join[Форма выпуска] " +
                    "ON[Форма выпуска].[Код формы] =[Характеристики лекарств].[Код формы] inner join Производитель ON Производитель.[Код производителя] =" +
                    "[Характеристики лекарств].[Код производителя] inner join[Серийный номер] ON[Характеристики лекарств].[Код характеристики] =[Серийный номер].[Код характеристики] " +
                    "WHERE[Серийный номер].[Код рецептурной продажи] is null and [Серийный номер].[Код безрецептурной продажи] is null and [Необходимый минимум].[Код лекарства] " +
                    "is not null and [Серийный номер].[Срок годности] > GETDATE() ", Connection);
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
    }
}
