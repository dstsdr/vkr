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
    public partial class tovar__ : Form
    {
        public tovar__()
        {
            InitializeComponent();
        }

        private void FirstName_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Connection.Open();
            SqlCommand cmd4 = Connection.CreateCommand();
            cmd4.CommandType = CommandType.Text;
            cmd4.CommandText = "SELECT Лекарства.Наименование AS НАЗВАНИЕ, [Условие отпуска].Условие AS УСЛОВИЕ, Наценка.Наценка AS НАЦЕНКА, [Фарм группа].Название AS ФАРМ " +
                "from Лекарства inner join [Условие отпуска] ON [Условие отпуска].[Код условия] = Лекарства.[Код условия] inner join Наценка ON Наценка.[Код наценки] = " +
                "Лекарства.[Код наценки] inner join [Фарм группа] ON Лекарства.[Код группы] =[Фарм группа].[Код группы]" +
                "WHERE Лекарства.Наименование='"+comboBox1.Text+"'";
            cmd4.ExecuteNonQuery();
            DataTable dt4 = new DataTable();
            SqlDataAdapter da4 = new SqlDataAdapter(cmd4);
            da4.Fill(dt4);
            foreach (DataRow dr4 in dt4.Rows)
            {
                textBox1.Text=(dr4["НАЗВАНИЕ"].ToString());
                comboBox7.Text=(dr4["УСЛОВИЕ"].ToString());
                comboBox9.Text = (dr4["НАЦЕНКА"].ToString());
                comboBox6.Text = (dr4["ФАРМ"].ToString());
            }
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void tovar___Load(object sender, EventArgs e)
        {
            datacmb();
        }
        SqlConnection Connection = new SqlConnection(@"Data Source=LAPTOP-862V88EF\SQLEXPRESS;Initial Catalog=vkr;Integrated Security=True");
        private void datacmb()
        {
            Connection.Open();
            SqlCommand cmd4 = Connection.CreateCommand();
            cmd4.CommandType = CommandType.Text;
            cmd4.CommandText = "SELECT Лекарства.Наименование AS НАЗВАНИЕ from[Характеристики лекарств] inner join Лекарства ON " +
                "Лекарства.[Код лекарства] =[Характеристики лекарств].[Код лекарства] inner join[Серийный номер] ON" +
                "[Характеристики лекарств].[Код характеристики] =[Серийный номер].[Код характеристики] where[Серийный номер].[Срок годности] > " +
                "GETDATE() and[Код безрецептурной продажи] is null and [Код рецептурной продажи] is null GROUP BY Лекарства.Наименование " +
                "ORDER BY Лекарства.Наименование ASC";
            cmd4.ExecuteNonQuery();
            DataTable dt4 = new DataTable();
            SqlDataAdapter da4 = new SqlDataAdapter(cmd4);
            da4.Fill(dt4);
            foreach (DataRow dr4 in dt4.Rows)
            {
                comboBox1.Items.Add(dr4["НАЗВАНИЕ"].ToString());
            }
            SqlCommand cmd3 = Connection.CreateCommand();
            cmd3.CommandType = CommandType.Text;
            cmd3.CommandText = "SELECT Производитель.Наименование AS ПРОИЗВ from Производитель ORDER BY Производитель.Наименование ASC";
            cmd4.ExecuteNonQuery();
            DataTable dt3 = new DataTable();
            SqlDataAdapter da3 = new SqlDataAdapter(cmd3);
            da3.Fill(dt3);
            foreach (DataRow dr3 in dt3.Rows)
            {
                comboBox2.Items.Add(dr3["ПРОИЗВ"].ToString());
            }

            SqlCommand cmd2 = Connection.CreateCommand();
            cmd2.CommandType = CommandType.Text;
            cmd2.CommandText = "SELECT [Форма выпуска].Форма  from[Форма выпуска] ORDER BY [Форма выпуска].Форма ASC";
            cmd2.ExecuteNonQuery();
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
            da2.Fill(dt2);
            foreach (DataRow dr2 in dt2.Rows)
            {
                comboBox3.Items.Add(dr2["Форма"].ToString());
            }

            SqlCommand cmd1 = Connection.CreateCommand();
            cmd1.CommandType = CommandType.Text;
            cmd1.CommandText = "SELECT Договор.[Номер договора] AS НОМЕР from Договор ORDER BY Договор.[Номер договора] ASC";
            cmd1.ExecuteNonQuery();
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            da1.Fill(dt1);
            foreach (DataRow dr1 in dt1.Rows)
            {
                comboBox4.Items.Add(dr1["НОМЕР"].ToString());
            }

            SqlCommand cmd5 = Connection.CreateCommand();
            cmd5.CommandType = CommandType.Text;
            cmd5.CommandText = "SELECT [Фарм группа].Название from [Фарм группа] ORDER BY [Фарм группа].Название ASC";
            cmd5.ExecuteNonQuery();
            DataTable dt5 = new DataTable();
            SqlDataAdapter da5 = new SqlDataAdapter(cmd5);
            da5.Fill(dt5);
            foreach (DataRow dr5 in dt5.Rows)
            {
                comboBox6.Items.Add(dr5["Название"].ToString());
            }

            SqlCommand cmd6 = Connection.CreateCommand();
            cmd6.CommandType = CommandType.Text;
            cmd6.CommandText = "SELECT [Условие отпуска].Условие from [Условие отпуска]";
            cmd6.ExecuteNonQuery();
            DataTable dt6 = new DataTable();
            SqlDataAdapter da6 = new SqlDataAdapter(cmd6);
            da6.Fill(dt6);
            foreach (DataRow dr6 in dt6.Rows)
            {
                comboBox7.Items.Add(dr6["Условие"].ToString());
            }
            SqlCommand cmd7 = Connection.CreateCommand();
            cmd7.CommandType = CommandType.Text;
            cmd7.CommandText = "SELECT Наценка.Наценка from Наценка ORDER BY Наценка.Наценка ASC";
            cmd7.ExecuteNonQuery();
            DataTable dt7 = new DataTable();
            SqlDataAdapter da7 = new SqlDataAdapter(cmd7);
            da7.Fill(dt7);
            foreach (DataRow dr7 in dt7.Rows)
            {
                comboBox9.Items.Add(dr7["Наценка"].ToString());
            }

            SqlCommand cmd8 = Connection.CreateCommand();
            cmd8.CommandType = CommandType.Text;
            cmd8.CommandText = "SELECT [Характеристики лекарств].Дозировка from [Характеристики лекарств] ORDER BY [Характеристики лекарств].Дозировка ASC ";
            cmd8.ExecuteNonQuery();
            DataTable dt8 = new DataTable();
            SqlDataAdapter da8 = new SqlDataAdapter(cmd8);
            da8.Fill(dt8);
            foreach (DataRow dr8 in dt8.Rows)
            {
                comboBox5.Items.Add(dr8["Дозировка"].ToString());
            }
            Connection.Close();
        }
    }
}
