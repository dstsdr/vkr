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
    public partial class Dogovor__ : Form
    {
        public Dogovor__()
        {
            InitializeComponent();
        }

        private void Dogovor___Load(object sender, EventArgs e)
        {
            cmbdata();
            if (button1.Visible==true)
            {
                string s = comboBox1.Text;
                for (int i = 0; i < comboBox1.Items.Count; i++)
                {
                    if (s == comboBox1.Items[i].ToString().Substring(comboBox1.Items[i].ToString().IndexOf(' ')+1)) 
                    {
                       comboBox1.SelectedItem=comboBox1.Items[i];
                    }
                }
                Connection.Open();
                SqlCommand cmd1 = Connection.CreateCommand();
                cmd1.CommandType = CommandType.Text;
                cmd1.CommandText = "SELECT CONCAT (Лекарства.Наименование, ' ', [Форма выпуска].Форма,' ', [Характеристики лекарств].Дозировка,' ', [Единицы измерения].Обозначение) as s " +
                    "from[Характеристики лекарств] " +
                    "inner join Лекарства ON Лекарства.[Код лекарства] =[Характеристики лекарств].[Код лекарства] " +
                    "inner join([Форма выпуска] " +
                    "inner join [Единицы измерения] ON[Единицы измерения].[Код ед.изм]=[Форма выпуска].[Код ед.изм]) " +
                    "ON[Форма выпуска].[Код формы] =[Характеристики лекарств].[Код формы]" +
                    "WHERE [Характеристики лекарств].[Номер договора]="+Convert.ToInt32(label25.Text);
                cmd1.ExecuteNonQuery();
                DataTable dt1 = new DataTable();
                SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                da1.Fill(dt1);
                foreach (DataRow dr1 in dt1.Rows)
                {
                    string k = (dr1["s"].ToString());
                    for (int i = 0; i < comboBox4.Items.Count; i++)
                    {
                        if (k == comboBox4.Items[i].ToString())
                        {
                            comboBox4.SelectedItem = comboBox4.Items[i];
                        }
                    }
                }
                Connection.Close();
            }
        }
        SqlConnection Connection = new SqlConnection(@"Data Source=LAPTOP-862V88EF\SQLEXPRESS;Initial Catalog=vkr;Integrated Security=True");
        private void cmbdata ()
        {
            Connection.Open();
            SqlCommand cmd4 = Connection.CreateCommand();
            cmd4.CommandType = CommandType.Text;
            /*
             *сделать выборку для товаров
             * */
            cmd4.CommandText = "SELECT Поставщик.[ИНН поставщика] as inn from Поставщик " +
                "ORDER BY Поставщик.[ИНН поставщика] ASC";
            cmd4.ExecuteNonQuery();
            DataTable dt4 = new DataTable();
            SqlDataAdapter da4 = new SqlDataAdapter(cmd4);
            da4.Fill(dt4);
            foreach (DataRow dr4 in dt4.Rows)
            {
                comboBox2.Items.Add(dr4["inn"].ToString());
            }
            SqlCommand cmd3 = Connection.CreateCommand();
            cmd3.CommandType = CommandType.Text;
            cmd3.CommandText = "SELECT НДС.Проценты from НДС ORDER BY НДС.Проценты ASC";
            cmd4.ExecuteNonQuery();
            DataTable dt3 = new DataTable();
            SqlDataAdapter da3 = new SqlDataAdapter(cmd3);
            da3.Fill(dt3);
            foreach (DataRow dr3 in dt3.Rows)
            {
                comboBox3.Items.Add(dr3["Проценты"].ToString());
            }

            SqlCommand cmd2 = Connection.CreateCommand();
            cmd2.CommandType = CommandType.Text;
            cmd2.CommandText = "SELECT CONCAT(Сотрудник.[Код сотрудника],' ', Сотрудник.Фамилия, ' ', Должность.Наименование) as sotr " +
                "from Сотрудник inner join Должность ON Должность.[Код должности] = Сотрудник.[Код должности] " +
                "ORDER BY CONCAT(Сотрудник.[Код сотрудника],' ', Сотрудник.Фамилия, ' ', Должность.Наименование) ASC";
            cmd2.ExecuteNonQuery();
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
            da2.Fill(dt2);
            foreach (DataRow dr2 in dt2.Rows)
            {
                comboBox1.Items.Add(dr2["sotr"].ToString());
            }
            SqlCommand cmd1 = Connection.CreateCommand();
            cmd1.CommandType = CommandType.Text;
            cmd1.CommandText = "SELECT CONCAT (Лекарства.Наименование, ' ', [Форма выпуска].Форма,' ', [Характеристики лекарств].Дозировка,' ', [Единицы измерения].Обозначение) as s " +
                "from[Характеристики лекарств] inner join Лекарства ON Лекарства.[Код лекарства] =[Характеристики лекарств].[Код лекарства] " +
                "inner join([Форма выпуска] inner join [Единицы измерения] ON[Единицы измерения].[Код ед.изм]=[Форма выпуска].[Код ед.изм]) ON[Форма выпуска].[Код формы] =[Характеристики лекарств].[Код формы]";
            cmd1.ExecuteNonQuery();
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            da1.Fill(dt1);
            foreach (DataRow dr1 in dt1.Rows)
            {
                comboBox4.Items.Add(dr1["s"].ToString());
            }
            Connection.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string a = "";
            bool check = true;
            if (textBox1.Text != "") { textBox1.BackColor = Color.White; }
            else { textBox1.BackColor = Color.DarkGray; a += "сумма\n"; check = false; }

            if (textBox2.Text != "") { textBox2.BackColor = Color.White; }
            else { textBox2.BackColor = Color.DarkGray; a += "срок оплаты\n"; check = false; }

            if (textBox3.Text != "") { textBox3.BackColor = Color.White; }
            else { textBox3.BackColor = Color.DarkGray; a += "срок годности\n"; check = false; }

            if (textBox4.Text != "") { textBox4.BackColor = Color.White; }
            else { textBox4.BackColor = Color.DarkGray; a += "срок рассмотрения повреждений вторичной упаковки\n"; check = false; }

            if (textBox5.Text != "") { textBox5.BackColor = Color.White; }
            else { textBox5.BackColor = Color.DarkGray; a += "срок рассмотрения претензий\n"; check = false; }

            if (textBox6.Text != "") { textBox6.BackColor = Color.White; }
            else { textBox6.BackColor = Color.DarkGray; a += "срок обнаружения скрытых недостатков\n"; check = false; }

            if (textBox7.Text != "") { textBox7.BackColor = Color.White; }
            else { textBox7.BackColor = Color.DarkGray; a += "срок обнаружения скрытых недостатков не позднее\n"; check = false; }

            if (textBox8.Text != "") { textBox8.BackColor = Color.White; }
            else { textBox8.BackColor = Color.DarkGray; a += "неустойка\n"; check = false; }

            if (textBox9.Text != "") { textBox9.BackColor = Color.White; }
            else { textBox9.BackColor = Color.DarkGray; a += "размер певрой поставки\n"; check = false; }

            if (textBox10.Text != "") { textBox10.BackColor = Color.White; }
            else { textBox10.BackColor = Color.DarkGray; a += "размер второй поставки\n"; check = false; }

            if (textBox11.Text != "") { textBox11.BackColor = Color.White; }
            else { textBox11.BackColor = Color.DarkGray; a += "срок первой поставки\n"; check = false; }

            if (textBox12.Text != "") { textBox12.BackColor = Color.White; }
            else { textBox12.BackColor = Color.DarkGray; a += "срок второй поставки\n"; check = false; }

            if (textBox13.Text != "") { textBox13.BackColor = Color.White; }
            else { textBox13.BackColor = Color.DarkGray; a += "срок доп. заявок\n"; check = false; }

            if (textBox14.Text != "") { textBox14.BackColor = Color.White; }
            else { textBox14.BackColor = Color.DarkGray; a += "срок информирование о форс-мажоре\n"; check = false; }

            if (textBox15.Text != "") { textBox15.BackColor = Color.White; }
            else { textBox15.BackColor = Color.DarkGray; a += "пени\n"; check = false; }

            if (textBox16.Text != "") { textBox16.BackColor = Color.White; }
            else { textBox16.BackColor = Color.DarkGray; a += "срок информирования о невозможности поставки\n"; check = false; }

            if (textBox17.Text != "") { textBox17.BackColor = Color.White; }
            else { textBox17.BackColor = Color.DarkGray; a += "количество\n"; check = false; }

            if (comboBox1.Text != "") { comboBox1.BackColor = Color.White; }
            else { comboBox1.BackColor = Color.DarkGray; a += "сотрудник\n"; check = false; }
            if (comboBox2.Text != "") { comboBox2.BackColor = Color.White; }
            else { comboBox2.BackColor = Color.DarkGray; a += "ИНН поставщика\n"; check = false; }
            if (comboBox3.Text != "") { comboBox3.BackColor = Color.White; }
            else { comboBox3.BackColor = Color.DarkGray; a += "НДС\n"; check = false; }
            if (comboBox4.Text != "") { comboBox4.BackColor = Color.White; }
            else { comboBox4.BackColor = Color.DarkGray; a += "товар\n"; check = false; }

            if (check == false)
            {
                MessageBox.Show("Для добавления записи заполните/выберите следующие поля:\n" + a);
            }
            if (check == true)
            {
                Connection.Open();
                SqlCommand cmd8 = Connection.CreateCommand();
                cmd8.CommandType = CommandType.Text;
                cmd8.CommandText = "SELECT НДС.[Код НДС] as Проценты from НДС  where НДС.Проценты=" + Convert.ToInt32(comboBox3.Text); //получили id */
                cmd8.ExecuteNonQuery();
                DataTable dt8 = new DataTable();
                SqlDataAdapter da8 = new SqlDataAdapter(cmd8);
                da8.Fill(dt8);
                SqlCommand command = new SqlCommand("insert into [Договор]([Дата заключения],[Сумма],[Код сотрудника],[ИНН поставщика],[НДС]," +
                    "[Срок оплаты],[Срок годности %],[повреждений вторичной упаковки ],[Рассмотрение претензии],[Скрытые недостатки в течении]," +
                    "[Скрытые недостатки не поздее],[Возврат до],[Неустойка],[Процент первой поставки],[Процент второй поставки],[Срок первой поставки]," +
                    "[Срок второй поставки],[Срок доп.заявок],[Срок информирование о форс-мажоре],[Пени],[Информирование в случае невозможности поставки]," +
                    "[Действие договора до],[Количество]) Values" +
                    " (@data, @summ, @sotr, @inn, @nds , @oplata , @goden , @vtorich , @pretenziya , @vtechenii , @nepozdnee , @vozvrat , @neust , " +
                    "@p1 , @p2 , @srok1 , @srok2 , @dop , @forsmajor , @peni , @nevozmojno , @do , @kolvo)", Connection);
                command.Parameters.AddWithValue("@data", dateTimePicker1.Value.ToString("dd'.'MM'.'yyyy"));
                command.Parameters.AddWithValue("@summ", textBox1.Text);
                command.Parameters.AddWithValue("@sotr", comboBox1.Text.Substring(0, comboBox1.Text.IndexOf(' ')));
                command.Parameters.AddWithValue("@inn", comboBox2.Text);
                command.Parameters.AddWithValue("@nds", Convert.ToInt32(dt8.Rows[0]["Проценты"]));
                command.Parameters.AddWithValue("@oplata", textBox2.Text);
                command.Parameters.AddWithValue("@goden", textBox3.Text);
                command.Parameters.AddWithValue("@vtorich", textBox4.Text);
                command.Parameters.AddWithValue("@pretenziya", textBox5.Text);
                command.Parameters.AddWithValue("@vtechenii", textBox6.Text);
                command.Parameters.AddWithValue("@nepozdnee", textBox7.Text);
                command.Parameters.AddWithValue("@vozvrat", dateTimePicker2.Value.ToString("dd'.'MM'.'yyyy"));
                command.Parameters.AddWithValue("@neust", textBox8.Text);
                command.Parameters.AddWithValue("@p1", textBox9.Text);
                command.Parameters.AddWithValue("@p2", textBox10.Text);
                command.Parameters.AddWithValue("@srok1", textBox11.Text);
                command.Parameters.AddWithValue("@srok2", textBox12.Text);
                command.Parameters.AddWithValue("@dop", textBox13.Text);
                command.Parameters.AddWithValue("@forsmajor", textBox14.Text);
                command.Parameters.AddWithValue("@peni", textBox15.Text);
                command.Parameters.AddWithValue("@nevozmojno", textBox16.Text);
                command.Parameters.AddWithValue("@do", dateTimePicker3.Value.ToString("dd'.'MM'.'yyyy"));
                command.Parameters.AddWithValue("@kolvo", textBox17.Text);
                command.ExecuteNonQuery();
                if (command.ExecuteNonQuery() != 1)
                {
                    MessageBox.Show("Возникла ошибка при добавлении договора");
                }
                else
                {
                    MessageBox.Show("Договор добавлен");
                }
                Connection.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string a = "";
            bool check = true;
            if (textBox1.Text != "") { textBox1.BackColor = Color.White; }
            else { textBox1.BackColor = Color.DarkGray; a += "сумма\n"; check = false; }

            if (textBox2.Text != "") { textBox2.BackColor = Color.White; }
            else { textBox2.BackColor = Color.DarkGray; a += "срок оплаты\n"; check = false; }

            if (textBox3.Text != "") { textBox3.BackColor = Color.White; }
            else { textBox3.BackColor = Color.DarkGray; a += "срок годности\n"; check = false; }

            if (textBox4.Text != "") { textBox4.BackColor = Color.White; }
            else { textBox4.BackColor = Color.DarkGray; a += "срок рассмотрения повреждений вторичной упаковки\n"; check = false; }

            if (textBox5.Text != "") { textBox5.BackColor = Color.White; }
            else { textBox5.BackColor = Color.DarkGray; a += "срок рассмотрения претензий\n"; check = false; }

            if (textBox6.Text != "") { textBox6.BackColor = Color.White; }
            else { textBox6.BackColor = Color.DarkGray; a += "срок обнаружения скрытых недостатков\n"; check = false; }

            if (textBox7.Text != "") { textBox7.BackColor = Color.White; }
            else { textBox7.BackColor = Color.DarkGray; a += "срок обнаружения скрытых недостатков не позднее\n"; check = false; }

            if (textBox8.Text != "") { textBox8.BackColor = Color.White; }
            else { textBox8.BackColor = Color.DarkGray; a += "неустойка\n"; check = false; }

            if (textBox9.Text != "") { textBox9.BackColor = Color.White; }
            else { textBox9.BackColor = Color.DarkGray; a += "размер певрой поставки\n"; check = false; }

            if (textBox10.Text != "") { textBox10.BackColor = Color.White; }
            else { textBox10.BackColor = Color.DarkGray; a += "размер второй поставки\n"; check = false; }

            if (textBox11.Text != "") { textBox11.BackColor = Color.White; }
            else { textBox11.BackColor = Color.DarkGray; a += "срок первой поставки\n"; check = false; }

            if (textBox12.Text != "") { textBox12.BackColor = Color.White; }
            else { textBox12.BackColor = Color.DarkGray; a += "срок второй поставки\n"; check = false; }

            if (textBox13.Text != "") { textBox13.BackColor = Color.White; }
            else { textBox13.BackColor = Color.DarkGray; a += "срок доп. заявок\n"; check = false; }

            if (textBox14.Text != "") { textBox14.BackColor = Color.White; }
            else { textBox14.BackColor = Color.DarkGray; a += "срок информирование о форс-мажоре\n"; check = false; }

            if (textBox15.Text != "") { textBox15.BackColor = Color.White; }
            else { textBox15.BackColor = Color.DarkGray; a += "пени\n"; check = false; }

            if (textBox16.Text != "") { textBox16.BackColor = Color.White; }
            else { textBox16.BackColor = Color.DarkGray; a += "срок информирования о невозможности поставки\n"; check = false; }

            if (textBox17.Text != "") { textBox17.BackColor = Color.White; }
            else { textBox17.BackColor = Color.DarkGray; a += "количество\n"; check = false; }

            if (comboBox1.Text != "") { comboBox1.BackColor = Color.White; }
            else { comboBox1.BackColor = Color.DarkGray; a += "сотрудник\n"; check = false; }
            if (comboBox2.Text != "") { comboBox2.BackColor = Color.White; }
            else { comboBox2.BackColor = Color.DarkGray; a += "ИНН поставщика\n"; check = false; }
            if (comboBox3.Text != "") { comboBox3.BackColor = Color.White; }
            else { comboBox3.BackColor = Color.DarkGray; a += "НДС\n"; check = false; }
            if (comboBox4.Text != "") { comboBox4.BackColor = Color.White; }
            else { comboBox4.BackColor = Color.DarkGray; a += "товар\n"; check = false; }

            if (check == false)
            {
                MessageBox.Show("Для изменения записи заполните/выберите следующие поля:" + a);
            }
            if (check == true)
            {
                try
                {
                    //изменение
                    Connection.Open();
                    SqlCommand cmd8 = Connection.CreateCommand();
                    cmd8.CommandType = CommandType.Text;
                    cmd8.CommandText = "SELECT НДС.[Код НДС] as Проценты from НДС  where НДС.Проценты=" + Convert.ToInt32(comboBox3.Text); //получили id */
                    cmd8.ExecuteNonQuery();
                    DataTable dt8 = new DataTable();
                    SqlDataAdapter da8 = new SqlDataAdapter(cmd8);
                    da8.Fill(dt8);
                    SqlCommand command = new SqlCommand("UPDATE [Договор]  SET [Дата заключения]=@data,[Сумма]=@summ,[Код сотрудника]=@sotr,[ИНН поставщика]= @inn,[НДС]=@nds," +
                        "[Срок оплаты]=@oplata,[Срок годности %]=@goden ,[повреждений вторичной упаковки ]=@vtorich, [Рассмотрение претензии]=@pretenziya, " +
                        "[Скрытые недостатки в течении]=@vtechenii ," +
                        "[Скрытые недостатки не поздее]=@nepozdnee ,[Возврат до]=@vozvrat ,[Неустойка]=@neust ,[Процент первой поставки]=@p1 ,[Процент второй поставки]=@p2 ," +
                        "[Срок первой поставки]=@srok1 ," +
                        "[Срок второй поставки]=@srok2 ,[Срок доп.заявок]=@dop ,[Срок информирование о форс-мажоре]=@forsmajor ,[Пени]=@peni ," +
                        "[Информирование в случае невозможности поставки]=@nevozmojno ," +
                        "[Действие договора до]=@do ,[Количество]=@kolvo WHERE [Номер договора]= " + Convert.ToInt32(label25.Text), Connection);
                    command.Parameters.AddWithValue("@data", dateTimePicker1.Value.ToString("dd'.'MM'.'yyyy"));
                    command.Parameters.AddWithValue("@summ", textBox1.Text);
                    command.Parameters.AddWithValue("@sotr", comboBox1.Text.Substring(0, comboBox1.Text.IndexOf(' ')));
                    command.Parameters.AddWithValue("@inn", comboBox2.Text);
                    command.Parameters.AddWithValue("@nds", Convert.ToInt32(dt8.Rows[0]["Проценты"]));
                    command.Parameters.AddWithValue("@oplata", textBox2.Text);
                    command.Parameters.AddWithValue("@goden", textBox3.Text);
                    command.Parameters.AddWithValue("@vtorich", textBox4.Text);
                    command.Parameters.AddWithValue("@pretenziya", textBox5.Text);
                    command.Parameters.AddWithValue("@vtechenii", textBox6.Text);
                    command.Parameters.AddWithValue("@nepozdnee", textBox7.Text);
                    command.Parameters.AddWithValue("@vozvrat", dateTimePicker2.Value.ToString("dd'.'MM'.'yyyy"));
                    command.Parameters.AddWithValue("@neust", textBox8.Text);
                    command.Parameters.AddWithValue("@p1", textBox9.Text);
                    command.Parameters.AddWithValue("@p2", textBox10.Text);
                    command.Parameters.AddWithValue("@srok1", textBox11.Text);
                    command.Parameters.AddWithValue("@srok2", textBox12.Text);
                    command.Parameters.AddWithValue("@dop", textBox13.Text);
                    command.Parameters.AddWithValue("@forsmajor", textBox14.Text);
                    command.Parameters.AddWithValue("@peni", textBox15.Text);
                    command.Parameters.AddWithValue("@nevozmojno", textBox16.Text);
                    command.Parameters.AddWithValue("@do", dateTimePicker3.Value.ToString("dd'.'MM'.'yyyy"));
                    command.Parameters.AddWithValue("@kolvo", textBox17.Text);
                    command.ExecuteNonQuery();
                    if (command.ExecuteNonQuery() != 1)
                    {
                        MessageBox.Show("Договор изменен");
                    }
                    else
                    {
                        MessageBox.Show("Возникла ошибка при изменении договора");
                    }
                    Connection.Close();
                }
                catch
                {
                    MessageBox.Show("Отсутствует подключение к серверу");
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ',')
            {
                e.Handled = true;
            }
        }

        private void textBox17_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox17_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox11_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox10_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox12_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox13_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox14_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox15_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox16_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
