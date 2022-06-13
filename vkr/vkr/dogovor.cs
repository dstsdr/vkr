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
using Word = Microsoft.Office.Interop.Word;

namespace vkr
{
    public partial class dogovor : Form
    {
        public dogovor()
        {
            InitializeComponent();
        }
        SqlConnection Connection = new SqlConnection(@"Data Source=LAPTOP-862V88EF\SQLEXPRESS;Initial Catalog=vkr;Integrated Security=True");
        private void Form1_Load(object sender, EventArgs e)
        {
            dataset();
        }
        private void dataset()
        {
            Connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT Договор.[Номер договора] as [№],Договор.[Дата заключения], Договор.[Сумма], Договор.Количество, " +
                "Лекарства.Наименование as [Название товара], CONCAT(Сотрудник.Фамилия, ' ', Должность.Наименование) as Сотрудник, CONCAT(Договор.[ИНН поставщика], ' ', " +
                "Поставщик.Название) as Поставщик, НДС.Проценты as НДС, Договор.[Срок оплаты], Договор.[Срок годности %], Договор.[повреждений вторичной упаковки] as " +
                "[Срок обнаружения повреждений вторичной упаковки] , Договор.[Рассмотрение претензии], Договор.[Скрытые недостатки не поздее], Договор.[Возврат до], " +
                "Договор.Неустойка, Договор.[Процент первой поставки] as [Размер 1 поставки в %], Договор.[Срок первой поставки] as [Срок 1 поставки], Договор.[Процент второй поставки] " +
                "as [Размер 1 поставки в %], Договор.[Срок второй поставки][Срок 2 поставки], Договор.[Срок доп.заявок], Договор.[Срок информирование о форс-мажоре] as " +
                "[Срок информирования о форс-мажоре], Договор.[Информирование в случае невозможности поставки], Договор.Пени, Договор.[Действие договора до] " +
                "FROM Договор inner join(Сотрудник inner join Должность ON Должность.[Код должности] = Сотрудник.[Код должности]) ON Договор.[Код сотрудника] =" +
                " Сотрудник.[Код сотрудника] inner join Поставщик ON Поставщик.[ИНН поставщика] = Договор.[ИНН поставщика] inner join([Характеристики лекарств] inner join Лекарства " +
                "ON Лекарства.[Код лекарства] =[Характеристики лекарств].[Код лекарства]) ON[Характеристики лекарств].[Номер договора] = Договор.[Номер договора] " +
                "inner join НДС ON НДС.[Код НДС] = Договор.НДС", Connection);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "info");
            dataGridView1.DataSource = ds.Tables[0];
            Connection.Close();
            int rows = dataGridView1.Rows.Count - 1;
            label1.Text = "Количество договоров: " + rows.ToString();

           // DateTime date = Convert.ToDateTime(dataGridView1.Rows[0].Cells[1].Value.ToString());            
           // string d = date.ToString("D").Remove(0,3);
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox3.Checked==true)
            {
                checkBox1.Checked = false;
                checkBox6.Checked = false;
                checkBox2.Checked = false;
                int s = Convert.ToInt32(dataGridView1.CurrentCell.Value);
                Connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT Банк.* " +
                    "FROM Банк INNER JOIN Поставщик ON Поставщик.БИК = Банк.БИК WHERE Поставщик.[ИНН поставщика] = (SELECT Поставщик.[ИНН поставщика] " +
                    "FROM Поставщик INNER JOIN Договор ON Поставщик.[ИНН поставщика] = Договор.[ИНН поставщика] WHERE Договор.[Номер договора] = " + s + ")", Connection);
                DataSet ds2 = new DataSet();
                adapter.Fill(ds2, "info");
                dataGridView2.DataSource = ds2.Tables[0];
                Connection.Close();
            }            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dogovor__ frm2 = new Dogovor__();
          //  frm2.save.Visible = false;
           // frm2.button1.Visible = true;
            frm2.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
           /* int k = dataGridView1.CurrentRow.Index;
            string s = dataGridView1[4, k].Value.ToString();
            Connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT Клиенты.* FROM Клиенты INNER JOIN Договор ON Клиенты.ИНН = Договор.[ИНН клиента] WHERE Договор.[№]='" + s + "'", Connection);
            DataSet ds2 = new DataSet();
            adapter.Fill(ds2, "info");
            dataGridView2.DataSource = ds2.Tables[0];
            Connection.Close();*/
        }
        private readonly string document = @"C:\Users\1652090\Downloads\shablon.docx";

        private void button2_Click(object sender, EventArgs e)
        {
             var wordApp = new Word.Application();
             wordApp.Visible = false;
             var wordDocument = wordApp.Documents.Open(document);
             string s = dataGridView1.CurrentCell.Value.ToString();
             Connection.Open();
            int a = 0;
             string sqlExpression = "SELECT Поставщик.Название, Поставщик.[Контактное лицо], CONCAT('г. ', Поставщик.[Почтовый город], ' ул. ', Поставщик.[Почтовая улица], ' ',Поставщик.[Почтовый дом]," +
                " ', ', Поставщик.[Почтовый индекс]), CONCAT('г. ', Поставщик.[Юр.Город], ' ул. ', Поставщик.[Юр.Улица], ' ',Поставщик.[Юр.Дом], ', ', Поставщик.[Юр.Индекс]), Поставщик.[ИНН поставщика]," +
                " Поставщик.КПП, Поставщик.[Расчетный счет], Поставщик.[Кор.счет], Поставщик.БИК, Банк.Название FROM Договор INNER JOIN (Поставщик inner join Банк ON Банк.БИК=Поставщик.БИК) " +
                "ON Поставщик.[ИНН поставщика] = Договор.[ИНН поставщика] where Договор.[Номер договора]= '" + s + "' UNION SELECT Аптека.Название,CONCAT( Сотрудник.Фамилия,' ', SUBSTRING(Сотрудник.Имя,1,1), ' ', " +
                "SUBSTRING(Сотрудник.Отчество,1,1)), CONCAT('г. ', Аптека.[Почтовый город], ' ул. ', Аптека.[Почтовая улица], ' ',Аптека.[Почтовый дом], ', ', Аптека.[Почтовый индекс]), " +
                "CONCAT('г. ', Аптека.[Юр.Город], ' ул. ', Аптека.[Юр.Улица], ' ',Аптека.[Юр.Дом], ', ', Аптека.[Юр.Индекс]), Аптека.ИНН, Аптека.КПП, Аптека.[Расчетный счет], Аптека.[Кор.счет], " +
                "Аптека.БИК, Банк.Название FROM Договор Inner join((Аптека inner join Банк ON Банк.БИК = Аптека.БИК) inner join Сотрудник ON Сотрудник.[ИНН аптеки] = Аптека.ИНН ) " +
                "ON Сотрудник.[Код должности] = Договор.[Код сотрудника] WHERE Договор.[Номер договора]= '" + s + "' AND Сотрудник.[Код должности] = 1";
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

                        ReplaceWordStub("{namepostav}", namepost, wordDocument);
                        ReplaceWordStub("{contactpostav}", contactpost, wordDocument);
                        ReplaceWordStub("{bik.post}", bikPost, wordDocument);
                        ReplaceWordStub("{kshet.post}", korpost, wordDocument);
                        ReplaceWordStub("{rschet.post}", raspost, wordDocument);
                        ReplaceWordStub("{bank.post}", bankpost, wordDocument);
                        ReplaceWordStub("{kpp.post}", kpppost, wordDocument);
                        ReplaceWordStub("{inn.post}", INNpost, wordDocument);
                        ReplaceWordStub("{pocht.adr.post}", pochtapost, wordDocument);
                        ReplaceWordStub("{yr.adr.post}", uridichpost, wordDocument);
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

                        ReplaceWordStub("{aptekaname}", nameapteka, wordDocument);
                        ReplaceWordStub("{contactapteka}", contactapteka, wordDocument);
                        ReplaceWordStub("{bik.apteka}", bikapteka, wordDocument);
                        ReplaceWordStub("{kshet.apteka}", korapteka, wordDocument);
                        ReplaceWordStub("{rschet.apteka}", rasapteka, wordDocument);
                        ReplaceWordStub("{bank.apteka}", bankapteka, wordDocument);
                        ReplaceWordStub("{kpp.apteka}", kppapteka, wordDocument);
                        ReplaceWordStub("{inn.apteka}", INNapteka, wordDocument);
                        ReplaceWordStub("{pocht.adr.apteka}", pochtaapteka, wordDocument);
                        ReplaceWordStub("{yr.adr.apteka}", uridichapteka, wordDocument);
                        ReplaceWordStub("{city}", uridichapteka.ToString().Substring(0, uridichapteka.IndexOf(" ул")), wordDocument);

                    }
                    a++;
                }
                    // данные из таблицы договоры
                    string number= dataGridView1.CurrentRow.Cells[0].Value.ToString();
               //  string adres = "г. " + city.ToString() + ", ул. " + street.ToString() + ", " + hn.ToString();
                DateTime datezakluch = Convert.ToDateTime(dataGridView1.Rows[0].Cells[1].Value.ToString());
		        DateTime vozvrat= Convert.ToDateTime(dataGridView1.Rows[0].Cells[12].Value.ToString());
                DateTime dogvordo = Convert.ToDateTime(dataGridView1.Rows[0].Cells[22].Value.ToString());
                	// где вставка впихнуть Д datezakluch.ToString("D");
 		string onepercent = dataGridView1.CurrentRow.Cells[14].Value.ToString();
		string twopercent = dataGridView1.CurrentRow.Cells[15].Value.ToString();
		string oneday= dataGridView1.CurrentRow.Cells[16].Value.ToString();
		string twoday= dataGridView1.CurrentRow.Cells[17].Value.ToString();
		string srokDop= dataGridView1.CurrentRow.Cells[18].Value.ToString();
		string srokGodnosti= dataGridView1.CurrentRow.Cells[7].Value.ToString();
		string summ= dataGridView1.CurrentRow.Cells[2].Value.ToString();
		string oplata= dataGridView1.CurrentRow.Cells[6].Value.ToString();
		string vtorichka= dataGridView1.CurrentRow.Cells[8].Value.ToString();
		string pretenziya= dataGridView1.CurrentRow.Cells[9].Value.ToString();
		string skritieVtech= dataGridView1.CurrentRow.Cells[10].Value.ToString();
		string skritieNePozdnee= dataGridView1.CurrentRow.Cells[11].Value.ToString();
		string neust= dataGridView1.CurrentRow.Cells[13].Value.ToString();
		string forsmajor=dataGridView1.CurrentRow.Cells[19].Value.ToString();
		string peni=dataGridView1.CurrentRow.Cells[20].Value.ToString();
		string nevozmozno=dataGridView1.CurrentRow.Cells[21].Value.ToString();

                //заполнение
                ReplaceWordStub("{d}", datezakluch.ToString().Substring(0, 2), wordDocument);
                ReplaceWordStub("{date}", datezakluch.ToString("D").Remove(0, 3), wordDocument);
                ReplaceWordStub("{dvoz}", vozvrat.ToString().Substring(0, 2), wordDocument);
                ReplaceWordStub("{datevoz}", vozvrat.ToString("D").Remove(0, 3), wordDocument);
                ReplaceWordStub("{ddo}", dogvordo.ToString().Substring(0, 2), wordDocument);
                ReplaceWordStub("{datedo}", dogvordo.ToString("D").Remove(0, 3), wordDocument);

                 ReplaceWordStub("{number}", number, wordDocument);
                 ReplaceWordStub("{1%}", onepercent, wordDocument);   
                 ReplaceWordStub("{2%}", twopercent, wordDocument); 
                 ReplaceWordStub("{1day}", oneday, wordDocument); 
                 ReplaceWordStub("{2day}", twoday, wordDocument); 
                 ReplaceWordStub("{2day}", twoday, wordDocument); 
                 ReplaceWordStub("{dop}", srokDop, wordDocument); 
                 ReplaceWordStub("{summ}", summ, wordDocument); 
                 ReplaceWordStub("{oplata}", oplata, wordDocument); 
                 ReplaceWordStub("{vtorichka}", vtorichka, wordDocument); 
                 ReplaceWordStub("{pretenziya}", pretenziya, wordDocument); 
                 ReplaceWordStub("{skrutue}", skritieVtech, wordDocument); 
                 ReplaceWordStub("{skrutue2}", skritieNePozdnee, wordDocument); 
                 ReplaceWordStub("{neust}", neust, wordDocument);
                 ReplaceWordStub("{forsmajor}", forsmajor, wordDocument);  
                 ReplaceWordStub("{peni}", peni, wordDocument); 
                 ReplaceWordStub("{nevozmozno}", nevozmozno, wordDocument);   
                 ReplaceWordStub("{srokgodnosti}", srokGodnosti, wordDocument);   


                //  ReplaceWordStub("{polnaya2}", ItogCreditSum.ToString("N2"), wordDocument);
                Connection.Close();
             }

             wordDocument.SaveAs(@"C:\Users\1652090\OneDrive\Рабочий стол\" + s + "");
             wordApp.Visible = true;
             Connection.Close();
        }
        private void ReplaceWordStub(string stubToReplace, string text, Word.Document wordDocument)
         {
             var range = wordDocument.Content;
             range.Find.ClearFormatting();
             range.Find.Execute(FindText: stubToReplace, ReplaceWith: text);
         }

        private void button10_Click(object sender, EventArgs e)
        {

        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                checkBox3.Checked = false;
                checkBox6.Checked = false;
                checkBox2.Checked = false;
                int s = Convert.ToInt32(dataGridView1.CurrentCell.Value);
                Connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT Поставщик.* FROM Поставщик INNER JOIN Договор ON " +
                    "Поставщик.[ИНН поставщика] = Договор.[ИНН поставщика] WHERE Договор.[Номер договора] =  " + s, Connection);
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
                checkBox3.Checked = false;
                checkBox1.Checked = false;
                checkBox2.Checked = false;
                int s = Convert.ToInt32(dataGridView1.CurrentCell.Value);
                Connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT Аптека.* " +
                    "FROM Аптека INNER JOIN Сотрудник ON Аптека.ИНН = Сотрудник.[ИНН аптеки] " +
                    "WHERE Сотрудник.[ИНН аптеки] = (SELECT Сотрудник.[ИНН аптеки] FROM Сотрудник INNER JOIN Договор ON Сотрудник.[Код сотрудника] = Договор.[Код сотрудника]" +
                    " WHERE Договор.[Номер договора] = " + s + ")", Connection);
                DataSet ds2 = new DataSet();
                adapter.Fill(ds2, "info");
                dataGridView2.DataSource = ds2.Tables[0];
                Connection.Close();
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                checkBox3.Checked = false;
                checkBox1.Checked = false;
                checkBox6.Checked = false;
                int s = Convert.ToInt32(dataGridView1.CurrentCell.Value);
                Connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT Сотрудник.[Код сотрудника], Сотрудник.[ИНН аптеки], Должность.Наименование, Сотрудник.Фамилия, Сотрудник.Имя, " +
                    "Сотрудник.Отчество, Сотрудник.[Серия паспорта], Сотрудник.[Номер паспорта], Сотрудник.Телефон " +
                    "FROM Сотрудник INNER JOIN Должность ON Сотрудник.[Код должности] = Должность.[Код должности] " +
                    "INNER JOIN Договор ON Сотрудник.[Код сотрудника] = Договор.[Код сотрудника] WHERE Договор.[Код сотрудника] = " + s , Connection);
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
                checkBox4.CheckState = CheckState.Unchecked;
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT Договор.[Номер договора] as [№],Договор.[Дата заключения], Договор.[Сумма], Договор.Количество, Лекарства.Наименование " +
                    "as [Название товара], CONCAT(Сотрудник.Фамилия, ' ', Должность.Наименование) as Сотрудник, CONCAT(Договор.[ИНН поставщика], ' ', Поставщик.Название) as Поставщик, " +
                    "НДС.Проценты as НДС, Договор.[Срок оплаты], Договор.[Срок годности %], Договор.[повреждений вторичной упаковки] as [Срок обнаружения повреждений вторичной упаковки], " +
                    "Договор.[Рассмотрение претензии], Договор.[Скрытые недостатки не поздее], Договор.[Возврат до], Договор.Неустойка, Договор.[Процент первой поставки] as " +
                    "[Размер 1 поставки в %], Договор.[Срок первой поставки] as [Срок 1 поставки], Договор.[Процент второй поставки] as [Размер 1 поставки в %], " +
                    "Договор.[Срок второй поставки][Срок 2 поставки], Договор.[Срок доп.заявок], Договор.[Срок информирование о форс - мажоре] as [Срок информирования о форс - мажоре], " +
                    "Договор.[Информирование в случае невозможности поставки], Договор.Пени, Договор.[Действие договора до] FROM Договор inner join(Сотрудник inner join Должность ON " +
                    "Должность.[Код должности] = Сотрудник.[Код должности]) ON Договор.[Код сотрудника] = Сотрудник.[Код сотрудника] inner join Поставщик ON Поставщик.[ИНН поставщика] = " +
                    "Договор.[ИНН поставщика] inner join([Характеристики лекарств] inner join Лекарства ON Лекарства.[Код лекарства] =[Характеристики лекарств].[Код лекарства]) ON" +
                    "[Характеристики лекарств].[Номер договора] = Договор.[Номер договора] inner join НДС ON НДС.[Код НДС] = Договор.НДС " +
                    "WHERE Договор.[Действие договора до]< GETDATE()", Connection);
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
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT Договор.[Номер договора] as [№],Договор.[Дата заключения], Договор.[Сумма], Договор.Количество, Лекарства.Наименование " +
                    "as [Название товара], CONCAT(Сотрудник.Фамилия, ' ', Должность.Наименование) as Сотрудник, CONCAT(Договор.[ИНН поставщика], ' ', Поставщик.Название) as Поставщик, " +
                    "НДС.Проценты as НДС, Договор.[Срок оплаты], Договор.[Срок годности %], Договор.[повреждений вторичной упаковки] as [Срок обнаружения повреждений вторичной упаковки], " +
                    "Договор.[Рассмотрение претензии], Договор.[Скрытые недостатки не поздее], Договор.[Возврат до], Договор.Неустойка, Договор.[Процент первой поставки] as " +
                    "[Размер 1 поставки в %], Договор.[Срок первой поставки] as [Срок 1 поставки], Договор.[Процент второй поставки] as [Размер 1 поставки в %], " +
                    "Договор.[Срок второй поставки][Срок 2 поставки], Договор.[Срок доп.заявок], Договор.[Срок информирование о форс - мажоре] as [Срок информирования о форс - мажоре], " +
                    "Договор.[Информирование в случае невозможности поставки], Договор.Пени, Договор.[Действие договора до] FROM Договор inner join(Сотрудник inner join Должность ON " +
                    "Должность.[Код должности] = Сотрудник.[Код должности]) ON Договор.[Код сотрудника] = Сотрудник.[Код сотрудника] inner join Поставщик ON Поставщик.[ИНН поставщика] = " +
                    "Договор.[ИНН поставщика] inner join([Характеристики лекарств] inner join Лекарства ON Лекарства.[Код лекарства] =[Характеристики лекарств].[Код лекарства]) ON" +
                    "[Характеристики лекарств].[Номер договора] = Договор.[Номер договора] inner join НДС ON НДС.[Код НДС] = Договор.НДС " +
                    "WHERE Договор.[Действие договора до]> GETDATE()", Connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "info");
                dataGridView1.DataSource = ds.Tables[0];
                Connection.Close();
                int rows = dataGridView1.Rows.Count - 1;
                label1.Text = "Количество записей " + rows.ToString();
            }
            else { dataset(); }
        }
    }
}
