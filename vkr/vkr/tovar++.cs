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
            Connection.Close();
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
            SqlCommand cmd9 = Connection.CreateCommand();
            cmd9.CommandType = CommandType.Text;
            cmd9.CommandText = "SELECT НДС.Проценты from НДС order by НДС.Проценты asc";
            cmd9.ExecuteNonQuery();
            DataTable dt9 = new DataTable();
            SqlDataAdapter da9 = new SqlDataAdapter(cmd9);
            da9.Fill(dt9);
            foreach (DataRow dr9 in dt9.Rows)
            {
                comboBox10.Items.Add(dr9["Проценты"].ToString());
            }
            Connection.Close();
        }
        private int lsadd ()
        {
            int y, n, f;
            //добавление лекарства            
                Connection.Open();
                SqlCommand cmd8 = Connection.CreateCommand();
                cmd8.CommandType = CommandType.Text;
                cmd8.CommandText = "SELECT [Условие отпуска].[Код условия] AS Условие, Наценка.[Код наценки] AS Наценка, " +
                     "[Фарм группа].[Код группы]  AS Фарм " +
                     "from[Условие отпуска], Наценка, [Фарм группа] where [Условие отпуска].Условие = '" + comboBox7.Text + "' and" +
                     " Наценка.Наценка = " + comboBox9.Text + " and [Фарм группа].Название = '" + comboBox6.Text + "'"; //получили id */
                cmd8.ExecuteNonQuery();
                DataTable dt8 = new DataTable();
                SqlDataAdapter da8 = new SqlDataAdapter(cmd8);
                da8.Fill(dt8);
                y = Convert.ToInt32(dt8.Rows[0]["Условие"]);
                f = Convert.ToInt32(dt8.Rows[0]["Фарм"]);
                n = Convert.ToInt32(dt8.Rows[0]["Наценка"]);
                SqlCommand command = new SqlCommand("insert into [Лекарства]([Наименование],[Код условия], [Код наценки], [Код группы]) Values" +
                " (@date, @number, @OSN, @PERCENT, @OST,@nol)", Connection); //добавили лекарство
                command.Parameters.AddWithValue("@number", textBox1.Text);
                command.Parameters.AddWithValue("@OSN", y);
                command.Parameters.AddWithValue("@PERCENT", n);
                command.Parameters.AddWithValue("@OST", f);
                command.ExecuteNonQuery();
                int id=0;
            // ищем максимальный id,и возвращаем
                return id;
                Connection.Close();
            
        }
        private int lskod()
        {
            int l;
            //получение кода лекарства            
            Connection.Open();
            SqlCommand cmd8 = Connection.CreateCommand();
            cmd8.CommandType = CommandType.Text;
            cmd8.CommandText = "SELECT Лекарства.[Код лекарства] AS Код from Лекарства WHERE Лекарства.Наименование = '" + comboBox1.Text + "'";
            cmd8.ExecuteNonQuery();
            DataTable dt8 = new DataTable();
            SqlDataAdapter da8 = new SqlDataAdapter(cmd8);
            da8.Fill(dt8);
            l = Convert.ToInt32(dt8.Rows[0]["Код"]);
            Connection.Close();
            return l;
        }
       /* private int xaracteristiki ()
        {
            /* n, f;
            Connection.Open(); //получаем ид характеристики
            SqlCommand cmd = Connection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT [Характеристики лекарств].[Код характеристики] AS КодХ from[Характеристики лекарств] inner join Лекарства ON Лекарства.[Код лекарства] " +
                "=[Характеристики лекарств].[Код лекарства] inner join Производитель ON Производитель.[Код производителя] =" +
                "[Характеристики лекарств].[Код производителя] inner join[Форма выпуска] ON[Форма выпуска].[Код формы] =[Характеристики лекарств].[Код формы] " +
                "inner join Договор ON Договор.[Номер договора] =[Характеристики лекарств].[Номер договора] " +
                "Where[Характеристики лекарств].Цена = " + comboBox8.Text + " and Договор.[Номер договора]= " + comboBox4.Text + " and[Форма выпуска].Форма = '" + comboBox3.Text + "' " +
                "and[Характеристики лекарств].Наценка = " + textBox2.Text + " and [Характеристики лекарств].Дозировка = " + comboBox5.Text + " and Производитель.Наименование = '" + comboBox2.Text + "' " +
                "AND Лекарства.Наименование = '" + comboBox1.Text + "'"; //получили id 
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            if (dt.Rows.Count == 0) // добавляем характеристики 
            {
                SqlCommand command = new SqlCommand("insert into [Характеристики лекарств]([Номер договора], [Код НДС], [Код лекарства], [Код формы]" +
                    " [Дозировка],[Цена],[Код производителя],[Наценка] ) Values" +
                               " (@date, @number, @OSN, @PERCENT, @OST,@nol)", Connection); //добавили лекарство
                command.Parameters.AddWithValue("@number", textBox1.Text);
                command.Parameters.AddWithValue("@OSN", y);
                command.Parameters.AddWithValue("@PERCENT", n);
                command.Parameters.AddWithValue("@OST", f);
                command.ExecuteNonQuery();
                Connection.Close();
                return 0;
            }
            else
            {
                int  x = Convert.ToInt32(dt.Rows[0]["КодХ"]); //получаем код
                Connection.Close();
                return x; 
            }
        }*/
        private void button1_Click(object sender, EventArgs e)
        {
            int x=0, l=0, y=0, n=0, f=0;

            Connection.Open();
            //++ ls
            if (comboBox1.SelectedIndex == 0)
            {
                //добавление лекарства            
               // Connection.Open();
                SqlCommand cmd8 = Connection.CreateCommand();
                cmd8.CommandType = CommandType.Text;
                cmd8.CommandText = "SELECT [Условие отпуска].[Код условия] AS Условие, Наценка.[Код наценки] AS Наценка, " +
                     "[Фарм группа].[Код группы]  AS Фарм " +
                     "from[Условие отпуска], Наценка, [Фарм группа] where [Условие отпуска].Условие = '" + comboBox7.Text + "' and" +
                     " Наценка.Наценка = " + comboBox9.Text + " and [Фарм группа].Название = '" + comboBox6.Text + "'"; //получили id */
                cmd8.ExecuteNonQuery();
                DataTable dt8 = new DataTable();
                SqlDataAdapter da8 = new SqlDataAdapter(cmd8);
                da8.Fill(dt8);
                y = Convert.ToInt32(dt8.Rows[0]["Условие"]);
                f = Convert.ToInt32(dt8.Rows[0]["Фарм"]);
                n = Convert.ToInt32(dt8.Rows[0]["Наценка"]);
                SqlCommand command = new SqlCommand("insert into [Лекарства]([Наименование],[Код условия], [Код наценки], [Код группы]) Values" +
                " (@number, @OSN, @PERCENT, @OST)", Connection); //добавили лекарство
                command.Parameters.AddWithValue("@number", textBox1.Text);
                command.Parameters.AddWithValue("@OSN", y);
                command.Parameters.AddWithValue("@PERCENT", n);
                command.Parameters.AddWithValue("@OST", f);
                command.ExecuteNonQuery();
                SqlCommand cmd6 = Connection.CreateCommand();
                cmd6.CommandType = CommandType.Text;
                cmd6.CommandText = "SELECT  MAX(Лекарства.[Код лекарства]) AS id from Лекарства"; //получили мах id */
                cmd6.ExecuteNonQuery();
                if (command.ExecuteNonQuery() != 1)
                {
                    MessageBox.Show("Возникла ошибка при добавлении лекарства");
                }
                DataTable dt6 = new DataTable();
                SqlDataAdapter da6 = new SqlDataAdapter(cmd6);
                da6.Fill(dt6);
                l= Convert.ToInt32(dt6.Rows[0]["id"]);
            }                                    
            if (comboBox1.SelectedIndex != 0) // получили код лекарства
            {
                SqlCommand cmd8 = Connection.CreateCommand();
                cmd8.CommandType = CommandType.Text;
                cmd8.CommandText = "SELECT Лекарства.[Код лекарства] AS Код from Лекарства WHERE Лекарства.Наименование = '" + comboBox1.Text + "'";
                cmd8.ExecuteNonQuery();
                if (cmd8.ExecuteNonQuery() != 1)
                {
                    MessageBox.Show("Возникла ошибка при распознавании лекарства");
                }
                DataTable dt8 = new DataTable();
                SqlDataAdapter da8 = new SqlDataAdapter(cmd8);
                da8.Fill(dt8);
                l = Convert.ToInt32(dt8.Rows[0]["Код"]);
            }
            SqlCommand cmd = Connection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT [Характеристики лекарств].[Код характеристики] AS КодХ from[Характеристики лекарств] inner join Лекарства ON Лекарства.[Код лекарства] " +
                "=[Характеристики лекарств].[Код лекарства] inner join Производитель ON Производитель.[Код производителя] =" +
                "[Характеристики лекарств].[Код производителя] inner join[Форма выпуска] ON[Форма выпуска].[Код формы] =[Характеристики лекарств].[Код формы] " +
                "inner join Договор ON Договор.[Номер договора] =[Характеристики лекарств].[Номер договора] " +
                "Where[Характеристики лекарств].Цена = " + comboBox8.Text.Replace(',','.') + " and Договор.[Номер договора]= " + comboBox4.Text + " and[Форма выпуска].Форма = '" + comboBox3.Text + "' " +
                "and[Характеристики лекарств].Наценка = " + textBox2.Text + " and [Характеристики лекарств].Дозировка = " + comboBox5.Text + " and Производитель.Наименование = '" + comboBox2.Text + "' " +
                "AND Лекарства.Наименование = '" + comboBox1.Text + "'"; //получили id */
            cmd.ExecuteNonQuery();
            if (cmd.ExecuteNonQuery() != 1)
            {
                MessageBox.Show("Возникла ошибка при получении номера характеристики товара");
            }            
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            if (dt.Rows.Count == 0) // добавляем характеристики 
            {
                Connection.Open();
                SqlCommand cmd8 = Connection.CreateCommand();
                cmd8.CommandType = CommandType.Text;
                cmd8.CommandText = "SELECT Производитель.[Код производителя] AS Произв, [Форма выпуска].[Код формы] AS Форма from Производитель, [Форма выпуска] " +
                    "WHERE Производитель.Наименование = '" + comboBox2.Text + "' and [Форма выпуска].Форма = '" + comboBox3.Text+"'"; //получили id */
                cmd8.ExecuteNonQuery();
                DataTable dt8 = new DataTable();
                SqlDataAdapter da8 = new SqlDataAdapter(cmd8);
                da8.Fill(dt8);
               // y = Convert.ToInt32(dt8.Rows[0]["Произв"]);
               // f = Convert.ToInt32(dt8.Rows[0]["Форма"]);
                SqlCommand command = new SqlCommand("insert into [Характеристики лекарств]([Номер договора], [Код НДС], [Код лекарства], [Код формы]" +
                    " [Дозировка],[Цена],[Код производителя],[Наценка] ) Values" +
                               " (@number, @OSN, @ls, @forma,@doz, @cost, @proizv, @nacen)", Connection); //добавили Характеристику
                command.Parameters.AddWithValue("@number", Convert.ToInt32(comboBox4.Text));
                command.Parameters.AddWithValue("@OSN", y);
                command.Parameters.AddWithValue("@ls", l);
                command.Parameters.AddWithValue("@forma", Convert.ToInt32(dt8.Rows[0]["Форма"]));
                command.Parameters.AddWithValue("@doz", Convert.ToInt32(comboBox5.Text));
                command.Parameters.AddWithValue("@cost", (comboBox8.Text));
                command.Parameters.AddWithValue("@proizv", Convert.ToInt32(dt8.Rows[0]["Произв"]));
                command.Parameters.AddWithValue("@nacen", Convert.ToInt32(textBox2.Text));
                command.ExecuteNonQuery();
                // ищем макс ид
                SqlCommand cmd7 = Connection.CreateCommand();
                cmd7.CommandType = CommandType.Text;
                cmd7.CommandText = "SELECT  MAX([Характеристики лекарств].[Код характеристики]) AS id from  [Характеристики лекарств]"; //получили id */
                cmd7.ExecuteNonQuery();
                if (cmd7.ExecuteNonQuery() != 1)
                {
                    MessageBox.Show("Возникла ошибка при добавлении характеристик товара");
                }
                DataTable dt7 = new DataTable();
                SqlDataAdapter da7 = new SqlDataAdapter(cmd7);
                da7.Fill(dt7);
                x = Convert.ToInt32(dt7.Rows[0]["id"]);
            }
            else
            {
                x = Convert.ToInt32(dt.Rows[0]["КодХ"]); //получаем код
            }

            SqlCommand command1 = new SqlCommand("insert into [Серийный номер]([Серийный номер], [Срок годности], [Код характеристики]) Values" +
                              " (@number, @srok, @kodx)", Connection); //добавили Характеристику
            command1.Parameters.AddWithValue("@number", Convert.ToInt32(LastName.Text));
            command1.Parameters.AddWithValue("@srok", dateTimePicker2.Value.ToString("dd'.'MM'.'yyyy"));
            command1.Parameters.AddWithValue("@kodx", x);
            command1.ExecuteNonQuery();
            if (command1.ExecuteNonQuery() != 1)
            {
                MessageBox.Show("Возникла ошибка при добавлении товара");
            }
            else
            {
                MessageBox.Show("товар добавлен");
            }
            Connection.Close();
        }

        private void textBox2_Validated(object sender, EventArgs e)
        {

        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text!="" && comboBox4.SelectedIndex>0 && comboBox10.SelectedIndex>0)
            {
                cost();
            }            
        }
        private void cost ()
        {
            Connection.Open();
            SqlCommand cmd7 = Connection.CreateCommand();
            cmd7.CommandType = CommandType.Text;
            int nacenka = Convert.ToInt32(textBox2.Text);
            cmd7.CommandText = "SELECT (((((Договор.Сумма*((НДС.Проценты)/100))/Договор.Количество)+(((Договор.Сумма*((НДС.Проценты)/100))/Договор.Количество)* " +
            "(" + nacenka + " / 100))))*0.1)+((Договор.Сумма * ((НДС.Проценты) / 100)) / Договор.Количество) * (" + nacenka + " / 100) + (Договор.Сумма * ((НДС.Проценты) / 100)) / " +
            "Договор.Количество AS SUMM from Договор inner join НДС ON Договор.НДС = НДС.[Код НДС] inner join [Характеристики лекарств] ON " +
            "[Характеристики лекарств].[Номер договора] = Договор.[Номер договора] " +
            "WHERE НДС.Проценты = " + comboBox10.Text + " and Договор.[Номер договора]=" + comboBox4.Text; //получили id */
            cmd7.ExecuteNonQuery(); 
            if ( cmd7.ExecuteNonQuery() != 1)
            {
                MessageBox.Show("Возникла ошибка при вычислении цены");
            }
            DataTable dt7 = new DataTable();
            SqlDataAdapter da7 = new SqlDataAdapter(cmd7);
            da7.Fill(dt7);
            double cost = Math.Round((Convert.ToDouble((dt7.Rows[0]["SUMM"]).ToString())),3);
            comboBox8.Text = cost.ToString();
            Connection.Close();
        }

        private void comboBox10_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text != "" && comboBox4.SelectedIndex > 0 && comboBox10.SelectedIndex > 0)
            {
                cost();
            }
        }

        private void comboBox4_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text != "" && comboBox4.SelectedIndex > 0 && comboBox10.SelectedIndex > 0)
            {
                cost();
            }
        }
    }
}
