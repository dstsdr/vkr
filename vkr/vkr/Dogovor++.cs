﻿using System;
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
            Connection.Open();
            SqlCommand cmd8 = Connection.CreateCommand();
            cmd8.CommandType = CommandType.Text;
            cmd8.CommandText = "SELECT НДС.[Код НДС] as Проценты from НДС  where НДС.Проценты="+Convert.ToInt32(comboBox3.Text); //получили id */
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
}
