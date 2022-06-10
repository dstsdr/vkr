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
            Connection.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Connection.Open();
            SqlCommand cmd8 = Connection.CreateCommand();
            cmd8.CommandType = CommandType.Text;
            cmd8.CommandText = "S "; //получили id */
            cmd8.ExecuteNonQuery();
            DataTable dt8 = new DataTable();
            SqlDataAdapter da8 = new SqlDataAdapter(cmd8);
            da8.Fill(dt8);
           // y = Convert.ToInt32(dt8.Rows[0]["Условие"]); найти ид ндс

            SqlCommand command = new SqlCommand("insert into [Договор]([Дата заключения],[Сумма],[Код сотрудника],[ИНН поставщика],[НДС]," +
                "[Срок оплаты],[Срок годности %],[повреждений вторичной упаковки ],[Рассмотрение претензии],[Скрытые недостатки в течении]," +
                "[Скрытые недостатки не поздее],[Возврат до],[Неустойка],[Процент первой поставки],[Процент второй поставки],[Срок первой поставки]," +
                "[Срок второй поставки],[Срок доп.заявок],[Срок информирование о форс-мажоре],[Пени],[Информирование в случае невозможности поставки]," +
                "[Действие договора до],[Количество]) Values" +
                " (@data, @summ, @sotr, @inn, @nds , @oplata , @goden , @vtorich , @pretenziya , @vtechenii , @nepozdnee , @vozvrat , @neust , " +
                "@%1 , @%2 , @srok1 , @srok2 , @dop , @forsmajor , @peni , @nevozmojno , @do , @kolvo)", Connection);
            command.Parameters.AddWithValue("@data", dateTimePicker1.Value.ToString("dd'.'MM'.'yyyy")); 
            command.Parameters.AddWithValue("@summ", textBox1.Text);
           // command.Parameters.AddWithValue("@sotr", textBox1.Text);
            command.Parameters.AddWithValue("@inn", comboBox2.Text); 
          //  command.Parameters.AddWithValue("@nds", textBox1.Text);
            command.Parameters.AddWithValue("@oplata", textBox2.Text);
            command.Parameters.AddWithValue("@goden", textBox3.Text);
            command.Parameters.AddWithValue("@vtorich", textBox4.Text);
            command.Parameters.AddWithValue("@pretenziya", textBox5.Text);
            command.Parameters.AddWithValue("@vtechenii", textBox6.Text);
            command.Parameters.AddWithValue("@nepozdnee", textBox7.Text);
            command.Parameters.AddWithValue("@vozvrat", dateTimePicker2.Value.ToString("dd'.'MM'.'yyyy"));
            command.Parameters.AddWithValue("@neust", textBox8.Text);
            command.Parameters.AddWithValue("@%1", textBox9.Text);
            command.Parameters.AddWithValue("@%2", textBox10.Text);
            command.Parameters.AddWithValue("@srok1", textBox11.Text);
            command.Parameters.AddWithValue("@srok2", textBox12.Text);
            command.Parameters.AddWithValue("@dop", textBox13.Text);
            command.Parameters.AddWithValue("@forsmajor", textBox14.Text);
            command.Parameters.AddWithValue("@peni", textBox15.Text);
            command.Parameters.AddWithValue("@nevozmojno", textBox16.Text);
            command.Parameters.AddWithValue("@do", dateTimePicker3.Value.ToString("dd'.'MM'.'yyyy"));
            command.Parameters.AddWithValue("@kolvo", textBox17.Text);
            command.ExecuteNonQuery();
            Connection.Close();
        }
    }
}
