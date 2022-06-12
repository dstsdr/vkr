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
    public partial class progznoz : Form
    {
        public progznoz()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("Select MONTH([Безрецептурные продажи].Дата) AS Месяц, COUNT([Серийный номер].[Код характеристики]) AS Количество " +
                "FROM [Серийный номер] inner join([Характеристики лекарств] inner join Лекарства ON Лекарства.[Код лекарства] = [Характеристики лекарств].[Код лекарства])ON " +
                "[Серийный номер].[Код характеристики] =[Характеристики лекарств].[Код характеристики]  inner join [Безрецептурные продажи] ON [Серийный номер].[Код безрецептурной продажи]" +
                "=[Безрецептурные продажи].[№] WHERE Лекарства.Наименование = '" + comboBox1.Text + "' Group by MONTH([Безрецептурные продажи].Дата), Лекарства.Наименование UNION Select" +
                " MONTH([Рецептурные продажи].Дата) AS Месяц, SUM([Рецептурные продажи].Количество) AS Количество " +
                "FROM[Рецептурные продажи] inner join([Характеристики лекарств] inner join Лекарства ON Лекарства.[Код лекарства]=" +
                "[Характеристики лекарств].[Код лекарства])ON[Рецептурные продажи].[Код характеристики] =[Характеристики лекарств].[Код характеристики] " +
                "WHERE Лекарства.Наименование = '" + comboBox1.Text + "' Group by MONTH([Рецептурные продажи].Дата), Лекарства.Наименование", Connection);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "info");
            dataGridView2.DataSource = ds.Tables[0];
            Connection.Close();
            method();
        }
        SqlConnection Connection = new SqlConnection(@"Data Source=LAPTOP-862V88EF\SQLEXPRESS;Initial Catalog=vkr;Integrated Security=True");
        private void method ()
        {
            int n = dataGridView2.Rows.Count-1;
           if (dataGridView2.Rows[n - 1].Cells[0].Value.ToString() != "12") dataGridView2.Rows[n].Cells[0].Value = Convert.ToInt32(dataGridView2.Rows[n - 1].Cells[0].Value);
         //  else dataGridView2.Rows[n].Cells[0].Value = 0;
            dataGridView2.Columns.Add("column2","Cреднее");
            
            if (dataGridView2.Rows.Count <4)
            {
                int i = 2;

                dataGridView2.Rows[i].Cells[2].Value = (Convert.ToDouble(dataGridView2.Rows[i - 1].Cells[1].Value) + Convert.ToDouble(dataGridView2.Rows[i - 2].Cells[1].Value)) / 2;
                dataGridView2.Columns[2].HeaderText = "Рекомендуемое число";
            }
            if (dataGridView2.Rows.Count <13 & dataGridView2.Rows.Count >2 )
            {
                int i = dataGridView2.Rows.Count-1;
            dataGridView2.Rows[i].Cells[2].Value = ((Convert.ToDouble(dataGridView2.Rows[i - 3].Cells[1].Value)+Convert.ToDouble(dataGridView2.Rows[i - 2].Cells[1].Value) + Convert.ToDouble(dataGridView2.Rows[i - 1].Cells[1].Value))/3) + ((1 / 3) * (Convert.ToDouble(dataGridView2.Rows[i - 2].Cells[1].Value) - Convert.ToDouble(dataGridView2.Rows[i - 1].Cells[1].Value)));

            }
            /* if (dataGridView2.Rows.Count - 1 ==12)
             {
                 dataGridView2.Columns.Add("column3", "Cезонный коэффициент");
                 dataGridView2.Columns.Add("column4", "Рекомендуемое число");
                 double summ = 0;
                 for (int i = 0; i < dataGridView2.Rows.Count - 1; i++)
                     {
                         summ = summ + Convert.ToDouble(dataGridView2.Rows[i].Cells[1].Value);
                     }
                 double years = summ / 12;
                 for (int i = 0; i < dataGridView2.Rows.Count - 2; i++)
                     {
                         dataGridView2.Rows[i].Cells[3].Value = Convert.ToDouble(dataGridView2.Rows[i+1].Cells[1].Value) / Convert.ToDouble(dataGridView2.Rows[i].Cells[1].Value);
                     }
                 for (int i = 1; i < dataGridView2.Rows.Count - 1; i++)
                 {
                     dataGridView2.Rows[i].Cells[4].Value =(Convert.ToDouble(dataGridView2.Rows[i].Cells[1].Value) + Convert.ToDouble(dataGridView2.Rows[i-1].Cells[1].Value))/2 * Convert.ToDouble(dataGridView2.Rows[i].Cells[3].Value);
                 }
                 int j = dataGridView2.Rows.Count - 1;
                 dataGridView2.Rows[12].Cells[1].Value = ((Convert.ToDouble(dataGridView2.Rows[j - 3].Cells[1].Value) + Convert.ToDouble(dataGridView2.Rows[j - 2].Cells[1].Value) + Convert.ToDouble(dataGridView2.Rows[j - 1].Cells[1].Value)) / 3) + ((1 / 3) * (Convert.ToDouble(dataGridView2.Rows[j - 2].Cells[1].Value) - Convert.ToDouble(dataGridView2.Rows[j - 1].Cells[1].Value)));

                 dataGridView2.Rows[0].Cells[4].Value = (Convert.ToDouble(dataGridView2.Rows[0].Cells[1].Value) + Convert.ToDouble(dataGridView2.Rows[12].Cells[1].Value))/2 * Convert.ToDouble(dataGridView2.Rows[0].Cells[3].Value);

             }  */
            dataGridView2.Columns.Add("column3", "Cезонный коэффициент");
            dataGridView2.Columns.Add("column4", "Рекомендуемое число");
            double summ = 0;
            for (int i = 0; i < dataGridView2.Rows.Count - 1; i++)
            {
                summ = summ + Convert.ToDouble(dataGridView2.Rows[i].Cells[1].Value);
            }
            summ = summ / 12;
            if (dataGridView2.Rows.Count-1 ==12)
            {
                for (int i = 1; i < dataGridView2.Rows.Count - 1; i++)
                {
                    dataGridView2.Rows[i].Cells[2].Value = (Convert.ToDouble(dataGridView2.Rows[i].Cells[1].Value) +
                        Convert.ToDouble(dataGridView2.Rows[i - 1].Cells[1].Value) + Convert.ToDouble(dataGridView2.Rows[i +1].Cells[1].Value)) / 3;
                    dataGridView2.Rows[i].Cells[3].Value = (Convert.ToDouble(dataGridView2.Rows[i].Cells[1].Value) / summ);
                }
                dataGridView2.Rows[0].Cells[2].Value = (Convert.ToDouble(dataGridView2.Rows[0].Cells[1].Value) +
                        Convert.ToDouble(dataGridView2.Rows[11].Cells[1].Value) + Convert.ToDouble(dataGridView2.Rows[1].Cells[1].Value)) / 3;
                dataGridView2.Rows[0].Cells[3].Value = (Convert.ToDouble(dataGridView2.Rows[0].Cells[1].Value) / summ);
                dataGridView2.Rows[11].Cells[2].Value = (Convert.ToDouble(dataGridView2.Rows[0].Cells[1].Value) +
                       Convert.ToDouble(dataGridView2.Rows[11].Cells[1].Value) + Convert.ToDouble(dataGridView2.Rows[10].Cells[1].Value)) / 3;
                dataGridView2.Rows[11].Cells[3].Value = (Convert.ToDouble(dataGridView2.Rows[11].Cells[1].Value) / summ);
            }
            for (int i = 0; i < dataGridView2.Rows.Count - 1; i++)
            {
                dataGridView2.Rows[i].Cells[4].Value= Convert.ToDouble(dataGridView2.Rows[i].Cells[3].Value)* Convert.ToDouble(dataGridView2.Rows[i].Cells[2].Value);
                dataGridView2.Rows[i].Cells[4].Value = Math.Round(Convert.ToDouble(dataGridView2.Rows[i].Cells[4].Value) + (Convert.ToDouble(dataGridView2.Rows[i].Cells[4].Value) * 10 / 100));
                dataGridView2.Rows[i].Cells[3].Value = Math.Round(Convert.ToDouble(dataGridView2.Rows[i].Cells[3].Value),2);
                dataGridView2.Rows[i].Cells[2].Value = Math.Round(Convert.ToDouble(dataGridView2.Rows[i].Cells[2].Value), 2);
            }

            int rows = dataGridView2.Rows.Count - 1;
            label1.Text = "Количество месяцев: " + rows.ToString();
            label1.Visible = true;
        }

    private void progznoz_Load(object sender, EventArgs e)
    {
            comboBox1.Visible = false;       
            label1.Visible=false;
            groupBox1.Visible = false;
            groupBox2.Visible=false;
            dataGridView2.Visible=false;
            button1.Visible=false;
    }

        private void button3_Click(object sender, EventArgs e)
        {
            method();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.dataGridView2.DataSource = null;
            this.dataGridView2.DataBindings.Clear();
            while (dataGridView2.Rows.Count > 1)
                for (int i = 0; i < dataGridView2.Rows.Count - 1; i++)
                    dataGridView2.Rows.Remove(dataGridView2.Rows[i]); int count = this.dataGridView2.Columns.Count;
            for (int i = 0; i < count; i++)     // цикл удаления всех столбцов
            {
                this.dataGridView2.Columns.RemoveAt(0);
            }
            if (comboBox2.SelectedIndex == 0)
            {
                comboBox1.Visible = true;
                label3.Visible = true;
                groupBox1.Visible = false;
                groupBox2.Visible = false;
                dataGridView2.Visible = true;
                button1.Visible = false;
                Connection.Open();
                SqlCommand cmd2 = Connection.CreateCommand();
                cmd2.CommandType = CommandType.Text;
                cmd2.CommandText = "SELECT Наименование FROM Лекарства";
                cmd2.ExecuteNonQuery();
                DataTable dt2 = new DataTable();
                SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                da2.Fill(dt2);
                foreach (DataRow dr2 in dt2.Rows)
                {
                    comboBox1.Items.Add(dr2["Наименование"].ToString());
                }
                Connection.Close();
                comboBox1.Visible = true;
            }
            else
            {
                comboBox1.Visible = false;
                groupBox2.Visible = true;
                groupBox1.Visible = true;
                dataGridView2.Visible = false;
                button1.Visible = true;
                label3.Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            comboBox1.Visible = false;
            label1.Visible = false;
            groupBox1.Visible = false;
            groupBox2.Visible= false;
            dataGridView2.Visible = true;
            if (radioButton3.Checked == true) zvnlpapteki();
            else zvnlp();
            if (radioButton6.Checked == true) sleep();
            if (radioButton6.Checked == true) sleep(); 
            if (radioButton5.Checked == true) centr();
            if (radioButton4.Checked == true) trc();
            button1.Visible = false;
            int rows = dataGridView2.Rows.Count - 1;
            label1.Visible = true;
            label1.Text = "Количество лекарственных средств: " + rows.ToString();
        }
        private void zvnlpapteki()
        {
            dataGridView2.Columns.Add("Категория", "Категория");
            dataGridView2.Columns.Add("Товар", "Товар");
            dataGridView2.RowCount = 49;
            dataGridView2.Rows[0].Cells[0].Value = "Обязательные препараты";
            dataGridView2.Rows[0].Cells[1].Value = "Ранитидин";
 	    dataGridView2.Rows[1].Cells[1].Value = "Фамотодин";
 	    dataGridView2.Rows[2].Cells[1].Value = "Омепразол";
 	    dataGridView2.Rows[3].Cells[1].Value = "Висмута трикалия дицитрат";
 	    dataGridView2.Rows[4].Cells[1].Value = "Дротаверин";
 	    dataGridView2.Rows[5].Cells[1].Value = "Бисакодил";
 	    dataGridView2.Rows[6].Cells[1].Value = "Сеннозиды А и В";
 	    dataGridView2.Rows[7].Cells[1].Value = "Лоперамид";
 	    dataGridView2.Rows[8].Cells[1].Value = "Бифидобактерии бифидум";
 	    dataGridView2.Rows[9].Cells[1].Value = "Панкреатин";
 	    dataGridView2.Rows[10].Cells[1].Value = "Аскорбиновая кислота";
 	    dataGridView2.Rows[11].Cells[1].Value = "Изосорбида динитрат";
 	    dataGridView2.Rows[12].Cells[1].Value = "Изосорбида мононитрат";
 	    dataGridView2.Rows[13].Cells[1].Value = "Нитроглицерин";
 	    dataGridView2.Rows[14].Cells[1].Value = "Гидрохлоротиазид";
 	    dataGridView2.Rows[15].Cells[1].Value = "Фуросемид";
 	    dataGridView2.Rows[16].Cells[1].Value = "Спиронолактон";
 	    dataGridView2.Rows[17].Cells[1].Value = "Атенолол";
 	    dataGridView2.Rows[18].Cells[1].Value = "Нифедипин";
 	    dataGridView2.Rows[19].Cells[1].Value = "Верапамил";
 	    dataGridView2.Rows[20].Cells[1].Value = "Каптоприл";
 	    dataGridView2.Rows[21].Cells[1].Value = "Эналаприл";
 	    dataGridView2.Rows[22].Cells[1].Value = "Лозартан";
 	    dataGridView2.Rows[23].Cells[1].Value = "Аторвастатин";
 	    dataGridView2.Rows[24].Cells[1].Value = "Клотримазол";
 	    dataGridView2.Rows[25].Cells[1].Value = "Гидрокортизон";
 	    dataGridView2.Rows[26].Cells[1].Value = "Дексаметазон";
 	    dataGridView2.Rows[27].Cells[1].Value = "Доксициклин";
 	    dataGridView2.Rows[28].Cells[1].Value = "Хлорамфеникол";
 	    dataGridView2.Rows[29].Cells[1].Value = "Амоксициллин";
 	    dataGridView2.Rows[30].Cells[1].Value = "Ко-тримоксазол";
 	    dataGridView2.Rows[31].Cells[1].Value = "Ципрофлоксацин";
 	    dataGridView2.Rows[32].Cells[1].Value = "Ацикловир";
 	    dataGridView2.Rows[33].Cells[1].Value = "Осельтамивир";
 	    dataGridView2.Rows[34].Cells[1].Value = "Кагоцел";
 	    dataGridView2.Rows[35].Cells[1].Value = "Умифеновир";
 	    dataGridView2.Rows[36].Cells[1].Value = "Диклофенак";
 	    dataGridView2.Rows[37].Cells[1].Value = "Ибупрофен";
 	    dataGridView2.Rows[38].Cells[1].Value = "Ацетилсалициловая кислота";
 	    dataGridView2.Rows[39].Cells[1].Value = "Парацетамол";
 	    dataGridView2.Rows[40].Cells[1].Value = "Сальбутамол";
 	    dataGridView2.Rows[41].Cells[1].Value = "Беклометазон";
 	    dataGridView2.Rows[42].Cells[1].Value = "Аминофиллин";
 	    dataGridView2.Rows[43].Cells[1].Value = "Ацетилцистеин";
 	    dataGridView2.Rows[44].Cells[1].Value = "Хлоропирамин";
 	    dataGridView2.Rows[45].Cells[1].Value = "Лоратадин";
 	    dataGridView2.Rows[46].Cells[1].Value = "Тетрациклин";
 	    dataGridView2.Rows[47].Cells[1].Value = "Пилокарпин";
 	    dataGridView2.Rows[48].Cells[1].Value = "Тимолол";
        }
	private void zvnlp()
        {
            dataGridView2.Columns.Add("Категория", "Категория");
            dataGridView2.Columns.Add("Товар", "Товар");
            dataGridView2.RowCount = 20;
            dataGridView2.Rows[0].Cells[0].Value = "Обязательные препараты";
 	    dataGridView2.Rows[0].Cells[1].Value = "Висмута трикалия дицитрат";
 	    dataGridView2.Rows[1].Cells[1].Value = "Дротаверин";
 	    dataGridView2.Rows[2].Cells[1].Value = "Бисакодил";
 	    dataGridView2.Rows[3].Cells[1].Value = "Сеннозиды А и В";
 	    dataGridView2.Rows[4].Cells[1].Value = "Лоперамид";
 	    dataGridView2.Rows[5].Cells[1].Value = "Бифидобактерии бифидум";
 	    dataGridView2.Rows[6].Cells[1].Value = "Панкреатин";
 	    dataGridView2.Rows[7].Cells[1].Value = "Аскорбиновая кислота";
 	    dataGridView2.Rows[8].Cells[1].Value = "Нитроглицерин";
 	    dataGridView2.Rows[9].Cells[1].Value = "Клотримазол";
 	    dataGridView2.Rows[10].Cells[1].Value = "Гидрокортизон";
 	    dataGridView2.Rows[11].Cells[1].Value = "Кагоцел";
 	    dataGridView2.Rows[12].Cells[1].Value = "Умифеновир";
 	    dataGridView2.Rows[13].Cells[1].Value = "Диклофенак";
 	    dataGridView2.Rows[14].Cells[1].Value = "Ибупрофен";
 	    dataGridView2.Rows[15].Cells[1].Value = "Ацетилсалициловая кислота";
 	    dataGridView2.Rows[16].Cells[1].Value = "Парацетамол";
 	    dataGridView2.Rows[17].Cells[1].Value = "Ацетилцистеин";
 	    dataGridView2.Rows[18].Cells[1].Value = "Лоратадин";
 	    dataGridView2.Rows[19].Cells[1].Value = "Тетрациклин";
        }
	private void sleep()
        {
            if (radioButton1.Checked==true)
            {
                int j = dataGridView2.RowCount;
                dataGridView2.RowCount = j + 110;
                dataGridView2.Rows[j + 0].Cells[0].Value = "От кашля";
                dataGridView2.Rows[j + 0].Cells[1].Value = "Флуифор";
                dataGridView2.Rows[j + 1].Cells[1].Value = "Гербион Плюща";
                dataGridView2.Rows[j + 2].Cells[1].Value = "Гербион Первоцвета";
                dataGridView2.Rows[j + 3].Cells[1].Value = "Эльмуцин";
                dataGridView2.Rows[j + 4].Cells[1].Value = "Флюдитек";
                dataGridView2.Rows[j + 5].Cells[1].Value = "Аскорил";
                dataGridView2.Rows[j + 6].Cells[1].Value = "Доктор Мом";
                dataGridView2.Rows[j + 7].Cells[1].Value = "Бронхипрет";
                dataGridView2.Rows[j + 8].Cells[1].Value = "Флуимуцил";
                dataGridView2.Rows[j + 9].Cells[1].Value = "Лазолван";
                dataGridView2.Rows[j + 10].Cells[1].Value = "Синекод";
                dataGridView2.Rows[j + 11].Cells[1].Value = "Омнитус";
                dataGridView2.Rows[j + 12].Cells[0].Value = "От боли в горле";
                dataGridView2.Rows[j + 12].Cells[1].Value = "Стрепсилс";
                dataGridView2.Rows[j + 13].Cells[1].Value = "Тонзилгон";
                dataGridView2.Rows[j + 14].Cells[1].Value = "Доритрицин";
                dataGridView2.Rows[j + 15].Cells[1].Value = "Оки";
                dataGridView2.Rows[j + 16].Cells[1].Value = "Септолете Тотал";
                dataGridView2.Rows[j + 17].Cells[1].Value = "Анти-Ангин";
                dataGridView2.Rows[j + 18].Cells[1].Value = "Ангидак";
                dataGridView2.Rows[j + 19].Cells[1].Value = "Гексаспрей";
                dataGridView2.Rows[j + 20].Cells[1].Value = "Гексорал";

                dataGridView2.Rows[j + 21].Cells[0].Value = "От насморка";
                dataGridView2.Rows[j + 22].Cells[1].Value = "Аквалор Актив Форте";
                dataGridView2.Rows[j + 23].Cells[1].Value = "Лазолван Рино";
                dataGridView2.Rows[j + 24].Cells[1].Value = "Пиносол";
                dataGridView2.Rows[j + 25].Cells[1].Value = "Линаква Форте";
                dataGridView2.Rows[j + 26].Cells[1].Value = "Ринофлуимуцил";
                dataGridView2.Rows[j + 27].Cells[1].Value = "Изофра";
                dataGridView2.Rows[j + 28].Cells[1].Value = "Долфин";
                dataGridView2.Rows[j + 29].Cells[1].Value = "Отривин";
                dataGridView2.Rows[j + 30].Cells[1].Value = "Трамицент";
                dataGridView2.Rows[j + 31].Cells[1].Value = "Риностоп";
                dataGridView2.Rows[j + 32].Cells[0].Value = "От симптомов простуды";
                dataGridView2.Rows[j + 32].Cells[1].Value = "Антигриппин";
                dataGridView2.Rows[j + 33].Cells[1].Value = "Ринзасип";
                dataGridView2.Rows[j + 34].Cells[1].Value = "Фервекс";
                dataGridView2.Rows[j + 35].Cells[1].Value = "Анвимакс";
                dataGridView2.Rows[j + 36].Cells[1].Value = "ТераФлю";
                dataGridView2.Rows[j + 37].Cells[1].Value = "Стопгрипан";
                dataGridView2.Rows[j + 38].Cells[1].Value = "Колдакт Флю";
                dataGridView2.Rows[j + 39].Cells[1].Value = "Гриппофлю";
                dataGridView2.Rows[j + 40].Cells[1].Value = "Звездочка Флю";
                dataGridView2.Rows[j + 41].Cells[1].Value = "Колдрекс";
                dataGridView2.Rows[j + 42].Cells[0].Value = "От гриппа";
                dataGridView2.Rows[j + 42].Cells[1].Value = "Ингавирин";
                dataGridView2.Rows[j + 43].Cells[1].Value = "Арбидол";
                dataGridView2.Rows[j + 44].Cells[1].Value = "Триазавирин";
                dataGridView2.Rows[j + 45].Cells[1].Value = "Генферон";
                dataGridView2.Rows[j + 46].Cells[1].Value = "Бак-Сет Колд/Флю";
                dataGridView2.Rows[j + 47].Cells[1].Value = "Тамифлю";
                dataGridView2.Rows[j + 48].Cells[1].Value = "Осельтамивир-Акрихин";
                dataGridView2.Rows[j + 49].Cells[1].Value = "Орвирем";
                dataGridView2.Rows[j + 50].Cells[0].Value = "Для детей и мам";
                dataGridView2.Rows[j + 50].Cells[1].Value = "Лидент Бэби";
                dataGridView2.Rows[j + 51].Cells[1].Value = "Медела Пурелан";
                dataGridView2.Rows[j + 52].Cells[1].Value = "Бепантен";
                dataGridView2.Rows[j + 53].Cells[1].Value = "Судокрем";
                dataGridView2.Rows[j + 54].Cells[1].Value = "Камистад Бэби";
                dataGridView2.Rows[j + 55].Cells[1].Value = "Дантинорм Бэби";
                dataGridView2.Rows[j + 56].Cells[1].Value = "Ла-Кри гель";
                dataGridView2.Rows[j + 57].Cells[1].Value = "Неотанин";
                dataGridView2.Rows[j + 58].Cells[1].Value = "Отривин Бэби";
                dataGridView2.Rows[j + 59].Cells[1].Value = "Аквалор Беби";
                dataGridView2.Rows[j + 60].Cells[1].Value = "Фэст Бандаж";
                dataGridView2.Rows[j + 61].Cells[1].Value = "Бутылочки для кормления";
                dataGridView2.Rows[j + 62].Cells[1].Value = "Слюнявчики";
                dataGridView2.Rows[j + 63].Cells[0].Value = "Детские смеси";
                dataGridView2.Rows[j + 63].Cells[1].Value = "Педиашур Малоежка";
                dataGridView2.Rows[j + 64].Cells[1].Value = "Неокейт Джуниор";
                dataGridView2.Rows[j + 65].Cells[1].Value = "Нестле Нан Оптипро";
                dataGridView2.Rows[j + 66].Cells[1].Value = "Дп Неокейт Lcp";
                dataGridView2.Rows[j + 67].Cells[1].Value = "Симилак Голд";
                dataGridView2.Rows[j + 68].Cells[1].Value = "Пептамен Юниор";
                dataGridView2.Rows[j + 69].Cells[1].Value = "Клинутрен Юниор";
                dataGridView2.Rows[j + 70].Cells[1].Value = "Нэнни";
                dataGridView2.Rows[j + 71].Cells[0].Value = "Средства личной гигиены";
                dataGridView2.Rows[j + 71].Cells[1].Value = "Лакалют Фикс";
                dataGridView2.Rows[j + 72].Cells[1].Value = "Корега Экстра";
                dataGridView2.Rows[j + 73].Cells[1].Value = "Лактацид";
                dataGridView2.Rows[j + 74].Cells[1].Value = "Асепта Актив";
                dataGridView2.Rows[j + 75].Cells[1].Value = "Протефикс";
                dataGridView2.Rows[j + 76].Cells[1].Value = "А-Церумен";
                dataGridView2.Rows[j + 77].Cells[1].Value = "Гинокомфорт";
                dataGridView2.Rows[j + 78].Cells[1].Value = "Св12";
                dataGridView2.Rows[j + 79].Cells[1].Value = "Анауретте";
                dataGridView2.Rows[j + 80].Cells[0].Value = "Расходные медицинские материалы";
                dataGridView2.Rows[j + 80].Cells[1].Value = "Маска медицинская";
                dataGridView2.Rows[j + 81].Cells[1].Value = "Спрей антибактериальный";
                dataGridView2.Rows[j + 82].Cells[1].Value = "Антисептик";
                dataGridView2.Rows[j + 83].Cells[1].Value = "Перчатки медицинские";
                dataGridView2.Rows[j + 84].Cells[1].Value = "Бахилы";
                dataGridView2.Rows[j + 85].Cells[1].Value = "Пипетка";
                dataGridView2.Rows[j + 86].Cells[1].Value = "Таблетницы";
                dataGridView2.Rows[j + 87].Cells[1].Value = "Беруши";
                dataGridView2.Rows[j + 88].Cells[1].Value = "Дезинфицирующее средство";
                dataGridView2.Rows[j + 89].Cells[1].Value = "Растворы для контактных линз";
                dataGridView2.Rows[j + 90].Cells[1].Value = "Пластыри";
                dataGridView2.Rows[j + 91].Cells[0].Value = "Витамины и минералы";
                dataGridView2.Rows[j + 91].Cells[1].Value = "Бэби Формула";
                dataGridView2.Rows[j + 92].Cells[1].Value = "Магнерот";
                dataGridView2.Rows[j + 93].Cells[1].Value = "Витамин C";
                dataGridView2.Rows[j + 94].Cells[1].Value = "Супрадин";
                dataGridView2.Rows[j + 95].Cells[1].Value = "Магне В6";
                dataGridView2.Rows[j + 96].Cells[1].Value = "Детримакс";
                dataGridView2.Rows[j + 97].Cells[1].Value = "Кальций-Д3";
                dataGridView2.Rows[j + 98].Cells[1].Value = "Селмевит";
                dataGridView2.Rows[j + 99].Cells[1].Value = "Витрум";
                dataGridView2.Rows[j + 100].Cells[0].Value = "Успокоительные";
                dataGridView2.Rows[j + 100].Cells[1].Value = "Тенотен";
                dataGridView2.Rows[j + 101].Cells[1].Value = "Пустырник";
                dataGridView2.Rows[j + 102].Cells[1].Value = "Персен";
                dataGridView2.Rows[j + 103].Cells[1].Value = "Ново-Пассит";
                dataGridView2.Rows[j + 104].Cells[1].Value = "Лотосоник";
                dataGridView2.Rows[j + 105].Cells[1].Value = "Валемидин";
                dataGridView2.Rows[j + 106].Cells[1].Value = "Корвалол";
                dataGridView2.Rows[j + 107].Cells[1].Value = "Стрессовит";
                dataGridView2.Rows[j + 108].Cells[1].Value = "Валериана";
                dataGridView2.Rows[j + 109].Cells[1].Value = "Релаксен";
            }
            else
            {
                int i = dataGridView2.RowCount;
                dataGridView2.RowCount = i + 120;
                dataGridView2.Rows[i + 0].Cells[0].Value = "От кашля";
                dataGridView2.Rows[i + 0].Cells[1].Value = "Флуифор";
                dataGridView2.Rows[i + 1].Cells[1].Value = "Гербион Плюща";
                dataGridView2.Rows[i + 2].Cells[1].Value = "Гербион Первоцвета";
                dataGridView2.Rows[i + 3].Cells[1].Value = "Эльмуцин";
                dataGridView2.Rows[i + 4].Cells[1].Value = "Флюдитек";
                dataGridView2.Rows[i + 5].Cells[1].Value = "Аскорил";
                dataGridView2.Rows[i + 6].Cells[1].Value = "Доктор Мом";
                dataGridView2.Rows[i + 7].Cells[1].Value = "Бронхипрет";
                dataGridView2.Rows[i + 8].Cells[1].Value = "Флуимуцил";
                dataGridView2.Rows[i + 9].Cells[1].Value = "Лазолван";
                dataGridView2.Rows[i + 10].Cells[1].Value = "Синекод";
                dataGridView2.Rows[i + 11].Cells[1].Value = "Омнитус";
                dataGridView2.Rows[i + 12].Cells[0].Value = "От боли в горле";
                dataGridView2.Rows[i + 12].Cells[1].Value = "Стрепсилс";
                dataGridView2.Rows[i + 13].Cells[1].Value = "Тонзилгон";
                dataGridView2.Rows[i + 14].Cells[1].Value = "Доритрицин";
                dataGridView2.Rows[i + 15].Cells[1].Value = "Оки";
                dataGridView2.Rows[i + 16].Cells[1].Value = "Септолете Тотал";
                dataGridView2.Rows[i + 17].Cells[1].Value = "Анти-Ангин";
                dataGridView2.Rows[i + 18].Cells[1].Value = "Ангидак";
                dataGridView2.Rows[i + 19].Cells[1].Value = "Гексаспрей";
                dataGridView2.Rows[i + 20].Cells[1].Value = "Гексорал";

                dataGridView2.Rows[i + 21].Cells[0].Value = "От насморка";
                dataGridView2.Rows[i + 22].Cells[1].Value = "Аквалор Актив Форте";
                dataGridView2.Rows[i + 23].Cells[1].Value = "Лазолван Рино";
                dataGridView2.Rows[i + 24].Cells[1].Value = "Пиносол";
                dataGridView2.Rows[i + 25].Cells[1].Value = "Линаква Форте";
                dataGridView2.Rows[i + 26].Cells[1].Value = "Ринофлуимуцил";
                dataGridView2.Rows[i + 27].Cells[1].Value = "Изофра";
                dataGridView2.Rows[i + 28].Cells[1].Value = "Долфин";
                dataGridView2.Rows[i + 29].Cells[1].Value = "Отривин";
                dataGridView2.Rows[i + 30].Cells[1].Value = "Трамицент";
                dataGridView2.Rows[i + 31].Cells[1].Value = "Риностоп";
                dataGridView2.Rows[i + 32].Cells[0].Value = "От симптомов простуды";
                dataGridView2.Rows[i + 32].Cells[1].Value = "Антигриппин";
                dataGridView2.Rows[i + 33].Cells[1].Value = "Ринзасип";
                dataGridView2.Rows[i + 34].Cells[1].Value = "Фервекс";
                dataGridView2.Rows[i + 35].Cells[1].Value = "Анвимакс";
                dataGridView2.Rows[i + 36].Cells[1].Value = "ТераФлю";
                dataGridView2.Rows[i + 37].Cells[1].Value = "Стопгрипан";
                dataGridView2.Rows[i + 38].Cells[1].Value = "Колдакт Флю";
                dataGridView2.Rows[i + 39].Cells[1].Value = "Гриппофлю";
                dataGridView2.Rows[i + 40].Cells[1].Value = "Звездочка Флю";
                dataGridView2.Rows[i + 41].Cells[1].Value = "Колдрекс";
                dataGridView2.Rows[i + 42].Cells[0].Value = "От гриппа";
                dataGridView2.Rows[i + 42].Cells[1].Value = "Ингавирин";
                dataGridView2.Rows[i + 43].Cells[1].Value = "Арбидол";
                dataGridView2.Rows[i + 44].Cells[1].Value = "Триазавирин";
                dataGridView2.Rows[i + 45].Cells[1].Value = "Генферон";
                dataGridView2.Rows[i + 46].Cells[1].Value = "Бак-Сет Колд/Флю";
                dataGridView2.Rows[i + 47].Cells[1].Value = "Тамифлю";
                dataGridView2.Rows[i + 48].Cells[1].Value = "Осельтамивир-Акрихин";
                dataGridView2.Rows[i + 49].Cells[1].Value = "Орвирем";
                dataGridView2.Rows[i + 50].Cells[0].Value = "Для детей и мам";
                dataGridView2.Rows[i + 50].Cells[1].Value = "Лидент Бэби";
                dataGridView2.Rows[i + 51].Cells[1].Value = "Медела Пурелан";
                dataGridView2.Rows[i + 52].Cells[1].Value = "Бепантен";
                dataGridView2.Rows[i + 53].Cells[1].Value = "Судокрем";
                dataGridView2.Rows[i + 54].Cells[1].Value = "Камистад Бэби";
                dataGridView2.Rows[i + 55].Cells[1].Value = "Дантинорм Бэби";
                dataGridView2.Rows[i + 56].Cells[1].Value = "Ла-Кри гель";
                dataGridView2.Rows[i + 57].Cells[1].Value = "Неотанин";
                dataGridView2.Rows[i + 58].Cells[1].Value = "Отривин Бэби";
                dataGridView2.Rows[i + 59].Cells[1].Value = "Аквалор Беби";
                dataGridView2.Rows[i + 60].Cells[1].Value = "Фэст Бандаж";
                dataGridView2.Rows[i + 61].Cells[1].Value = "Бутылочки для кормления";
                dataGridView2.Rows[i + 62].Cells[1].Value = "Слюнявчики";
                dataGridView2.Rows[i + 63].Cells[0].Value = "Детские смеси";
                dataGridView2.Rows[i + 63].Cells[1].Value = "Педиашур Малоежка";
                dataGridView2.Rows[i + 64].Cells[1].Value = "Неокейт Джуниор";
                dataGridView2.Rows[i + 65].Cells[1].Value = "Нестле Нан Оптипро";
                dataGridView2.Rows[i + 66].Cells[1].Value = "Дп Неокейт Lcp";
                dataGridView2.Rows[i + 67].Cells[1].Value = "Симилак Голд";
                dataGridView2.Rows[i + 68].Cells[1].Value = "Пептамен Юниор";
                dataGridView2.Rows[i + 69].Cells[1].Value = "Клинутрен Юниор";
                dataGridView2.Rows[i + 70].Cells[1].Value = "Нэнни";
                dataGridView2.Rows[i + 71].Cells[0].Value = "Средства личной гигиены";
                dataGridView2.Rows[i + 71].Cells[1].Value = "Лакалют Фикс";
                dataGridView2.Rows[i + 72].Cells[1].Value = "Корега Экстра";
                dataGridView2.Rows[i + 73].Cells[1].Value = "Лактацид";
                dataGridView2.Rows[i + 74].Cells[1].Value = "Асепта Актив";
                dataGridView2.Rows[i + 75].Cells[1].Value = "Протефикс";
                dataGridView2.Rows[i + 76].Cells[1].Value = "А-Церумен";
                dataGridView2.Rows[i + 77].Cells[1].Value = "Гинокомфорт";
                dataGridView2.Rows[i + 78].Cells[1].Value = "Св12";
                dataGridView2.Rows[i + 79].Cells[1].Value = "Анауретте";
                dataGridView2.Rows[i + 80].Cells[0].Value = "Расходные медицинские материалы";
                dataGridView2.Rows[i + 80].Cells[1].Value = "Маска медицинская";
                dataGridView2.Rows[i + 81].Cells[1].Value = "Спрей антибактериальный";
                dataGridView2.Rows[i + 82].Cells[1].Value = "Антисептик";
                dataGridView2.Rows[i + 83].Cells[1].Value = "Перчатки медицинские";
                dataGridView2.Rows[i + 84].Cells[1].Value = "Бахилы";
                dataGridView2.Rows[i + 85].Cells[1].Value = "Пипетка";
                dataGridView2.Rows[i + 86].Cells[1].Value = "Таблетницы";
                dataGridView2.Rows[i + 87].Cells[1].Value = "Беруши";
                dataGridView2.Rows[i + 88].Cells[1].Value = "Дезинфицирующее средство";
                dataGridView2.Rows[i + 89].Cells[1].Value = "Растворы для контактных линз";
                dataGridView2.Rows[i + 90].Cells[1].Value = "Пластыри";
                dataGridView2.Rows[i + 91].Cells[0].Value = "Витамины и минералы";
                dataGridView2.Rows[i + 91].Cells[1].Value = "Бэби Формула";
                dataGridView2.Rows[i + 92].Cells[1].Value = "Магнерот";
                dataGridView2.Rows[i + 93].Cells[1].Value = "Витамин C";
                dataGridView2.Rows[i + 94].Cells[1].Value = "Супрадин";
                dataGridView2.Rows[i + 95].Cells[1].Value = "Магне В6";
                dataGridView2.Rows[i + 96].Cells[1].Value = "Детримакс";
                dataGridView2.Rows[i + 97].Cells[1].Value = "Кальций-Д3";
                dataGridView2.Rows[i + 98].Cells[1].Value = "Селмевит";
                dataGridView2.Rows[i + 99].Cells[1].Value = "Витрум";
                dataGridView2.Rows[i + 100].Cells[0].Value = "Успокоительные";
                dataGridView2.Rows[i + 100].Cells[1].Value = "Тенотен";
                dataGridView2.Rows[i + 101].Cells[1].Value = "Пустырник";
                dataGridView2.Rows[i + 102].Cells[1].Value = "Персен";
                dataGridView2.Rows[i + 103].Cells[1].Value = "Ново-Пассит";
                dataGridView2.Rows[i + 104].Cells[1].Value = "Лотосоник";
                dataGridView2.Rows[i + 105].Cells[1].Value = "Валемидин";
                dataGridView2.Rows[i + 106].Cells[1].Value = "Корвалол";
                dataGridView2.Rows[i + 107].Cells[1].Value = "Стрессовит";
                dataGridView2.Rows[i + 108].Cells[1].Value = "Валериана";
                dataGridView2.Rows[i + 109].Cells[1].Value = "Релаксен";
                dataGridView2.Rows[i + 110].Cells[0].Value = "Антидепрессанты";
                dataGridView2.Rows[i + 110].Cells[1].Value = "Пароксетин";
                dataGridView2.Rows[i + 111].Cells[1].Value = "Селектра";
                dataGridView2.Rows[i + 112].Cells[1].Value = "Велаксин";
                dataGridView2.Rows[i + 113].Cells[1].Value = "Циталопрам";
                dataGridView2.Rows[i + 114].Cells[1].Value = "Симбалта";
                dataGridView2.Rows[i + 115].Cells[1].Value = "Серлифт";
                dataGridView2.Rows[i + 116].Cells[1].Value = "Депратал";
                dataGridView2.Rows[i + 117].Cells[1].Value = "Золофт";
                dataGridView2.Rows[i + 118].Cells[1].Value = "Флуоксетин";
                dataGridView2.Rows[i + 119].Cells[1].Value = "Амитриптилин";
            }
          
        }
	private void centr()
        {
            if (radioButton1.Checked == true)
            {
                int i = dataGridView2.RowCount;
                dataGridView2.RowCount = i + 120;
                dataGridView2.Rows[i + 0].Cells[0].Value = "От кашля";
                dataGridView2.Rows[i + 0].Cells[1].Value = "Флуифор";
                dataGridView2.Rows[i + 1].Cells[1].Value = "Гербион Плюща";
                dataGridView2.Rows[i + 2].Cells[1].Value = "Гербион Первоцвета";
                dataGridView2.Rows[i + 3].Cells[1].Value = "Эльмуцин";
                dataGridView2.Rows[i + 4].Cells[1].Value = "Флюдитек";
                dataGridView2.Rows[i + 5].Cells[1].Value = "Аскорил";
                dataGridView2.Rows[i + 6].Cells[1].Value = "Доктор Мом";
                dataGridView2.Rows[i + 7].Cells[1].Value = "Бронхипрет";
                dataGridView2.Rows[i + 8].Cells[1].Value = "Флуимуцил";
                dataGridView2.Rows[i + 9].Cells[1].Value = "Лазолван";
                dataGridView2.Rows[i + 10].Cells[1].Value = "Синекод";
                dataGridView2.Rows[i + 11].Cells[1].Value = "Омнитус";

                dataGridView2.Rows[i + 12].Cells[0].Value = "От боли в горле";
                dataGridView2.Rows[i + 12].Cells[1].Value = "Стрепсилс";
                dataGridView2.Rows[i + 13].Cells[1].Value = "Тонзилгон";
                dataGridView2.Rows[i + 14].Cells[1].Value = "Доритрицин";
                dataGridView2.Rows[i + 15].Cells[1].Value = "Оки";
                dataGridView2.Rows[i + 16].Cells[1].Value = "Септолете Тотал";
                dataGridView2.Rows[i + 17].Cells[1].Value = "Анти-Ангин";
                dataGridView2.Rows[i + 18].Cells[1].Value = "Ангидак";
                dataGridView2.Rows[i + 19].Cells[1].Value = "Гексаспрей";
                dataGridView2.Rows[i + 20].Cells[1].Value = "Гексорал";

                dataGridView2.Rows[i + 21].Cells[0].Value = "От насморка";
                dataGridView2.Rows[i + 21].Cells[1].Value = "Аквалор Актив Форте";
                dataGridView2.Rows[i + 22].Cells[1].Value = "Лазолван Рино";
                dataGridView2.Rows[i + 23].Cells[1].Value = "Пиносол";
                dataGridView2.Rows[i + 24].Cells[1].Value = "Линаква Форте";
                dataGridView2.Rows[i + 25].Cells[1].Value = "Ринофлуимуцил";
                dataGridView2.Rows[i + 26].Cells[1].Value = "Изофра";
                dataGridView2.Rows[i + 27].Cells[1].Value = "Долфин";
                dataGridView2.Rows[i + 28].Cells[1].Value = "Отривин";
                dataGridView2.Rows[i + 29].Cells[1].Value = "Трамицент";
                dataGridView2.Rows[i + 30].Cells[1].Value = "Риностоп";

                dataGridView2.Rows[i + 31].Cells[0].Value = "От симптомов простуды";
                dataGridView2.Rows[i + 31].Cells[1].Value = "Антигриппин";
                dataGridView2.Rows[i + 32].Cells[1].Value = "Ринзасип";
                dataGridView2.Rows[i + 33].Cells[1].Value = "Фервекс";
                dataGridView2.Rows[i + 34].Cells[1].Value = "Анвимакс";
                dataGridView2.Rows[i + 35].Cells[1].Value = "ТераФлю";
                dataGridView2.Rows[i + 36].Cells[1].Value = "Стопгрипан";
                dataGridView2.Rows[i + 37].Cells[1].Value = "Колдакт Флю";
                dataGridView2.Rows[i + 38].Cells[1].Value = "Гриппофлю";
                dataGridView2.Rows[i + 39].Cells[1].Value = "Звездочка Флю";
                dataGridView2.Rows[i + 40].Cells[1].Value = "Колдрекс";

                dataGridView2.Rows[i + 41].Cells[0].Value = "От гриппа";
                dataGridView2.Rows[i + 41].Cells[1].Value = "Ингавирин";
                dataGridView2.Rows[i + 42].Cells[1].Value = "Арбидол";
                dataGridView2.Rows[i + 43].Cells[1].Value = "Триазавирин";
                dataGridView2.Rows[i + 44].Cells[1].Value = "Генферон";
                dataGridView2.Rows[i + 45].Cells[1].Value = "Бак-Сет Колд/Флю";
                dataGridView2.Rows[i + 46].Cells[1].Value = "Тамифлю";
                dataGridView2.Rows[i + 47].Cells[1].Value = "Осельтамивир-Акрихин";
                dataGridView2.Rows[i + 48].Cells[1].Value = "Орвирем";

                dataGridView2.Rows[i + 49].Cells[0].Value = "Для детей и мам";
                dataGridView2.Rows[i + 49].Cells[1].Value = "Лидент Бэби";
                dataGridView2.Rows[i + 50].Cells[1].Value = "Медела Пурелан";
                dataGridView2.Rows[i + 51].Cells[1].Value = "Бепантен";
                dataGridView2.Rows[i + 52].Cells[1].Value = "Судокрем";
                dataGridView2.Rows[i + 53].Cells[1].Value = "Камистад Бэби";
                dataGridView2.Rows[i + 54].Cells[1].Value = "Дантинорм Бэби";
                dataGridView2.Rows[i + 55].Cells[1].Value = "Ла-Кри гель";
                dataGridView2.Rows[i + 56].Cells[1].Value = "Неотанин";
                dataGridView2.Rows[i + 57].Cells[1].Value = "Отривин Бэби";
                dataGridView2.Rows[i + 58].Cells[1].Value = "Аквалор Беби";
                dataGridView2.Rows[i + 59].Cells[1].Value = "Фэст Бандаж";
                dataGridView2.Rows[i + 60].Cells[1].Value = "Бутылочки для кормления";
                dataGridView2.Rows[i + 61].Cells[1].Value = "Слюнявчики";

                dataGridView2.Rows[i + 62].Cells[0].Value = "Детские смеси";
                dataGridView2.Rows[i + 62].Cells[1].Value = "Педиашур Малоежка";
                dataGridView2.Rows[i + 63].Cells[1].Value = "Неокейт Джуниор";
                dataGridView2.Rows[i + 64].Cells[1].Value = "Нестле Нан Оптипро";
                dataGridView2.Rows[i + 65].Cells[1].Value = "Дп Неокейт Lcp";
                dataGridView2.Rows[i + 66].Cells[1].Value = "Симилак Голд";
                dataGridView2.Rows[i + 67].Cells[1].Value = "Пептамен Юниор";
                dataGridView2.Rows[i + 68].Cells[1].Value = "Клинутрен Юниор";
                dataGridView2.Rows[i + 69].Cells[1].Value = "Нэнни";

                dataGridView2.Rows[i + 70].Cells[0].Value = "Средства личной гигиены";
                dataGridView2.Rows[i + 70].Cells[1].Value = "Лакалют Фикс";
                dataGridView2.Rows[i + 71].Cells[1].Value = "Корега Экстра";
                dataGridView2.Rows[i + 72].Cells[1].Value = "Лактацид";
                dataGridView2.Rows[i + 73].Cells[1].Value = "Асепта Актив";
                dataGridView2.Rows[i + 74].Cells[1].Value = "Протефикс";
                dataGridView2.Rows[i + 75].Cells[1].Value = "А-Церумен";
                dataGridView2.Rows[i + 76].Cells[1].Value = "Гинокомфорт";
                dataGridView2.Rows[i + 77].Cells[1].Value = "Св12";
                dataGridView2.Rows[i + 78].Cells[1].Value = "Анауретте";

                dataGridView2.Rows[i + 79].Cells[0].Value = "Расходные медицинские материалы";
                dataGridView2.Rows[i + 79].Cells[1].Value = "Маска медицинская";
                dataGridView2.Rows[i + 80].Cells[1].Value = "Спрей антибактериальный";
                dataGridView2.Rows[i + 81].Cells[1].Value = "Антисептик";
                dataGridView2.Rows[i + 82].Cells[1].Value = "Перчатки медицинские";
                dataGridView2.Rows[i + 83].Cells[1].Value = "Бахилы";
                dataGridView2.Rows[i + 84].Cells[1].Value = "Пипетка";
                dataGridView2.Rows[i + 85].Cells[1].Value = "Таблетницы";
                dataGridView2.Rows[i + 86].Cells[1].Value = "Беруши";
                dataGridView2.Rows[i + 87].Cells[1].Value = "Дезинфицирующее средство";
                dataGridView2.Rows[i + 88].Cells[1].Value = "Растворы для контактных линз";
                dataGridView2.Rows[i + 89].Cells[1].Value = "Пластыри";

                dataGridView2.Rows[i + 90].Cells[0].Value = "Витамины и минералы";
                dataGridView2.Rows[i + 90].Cells[1].Value = "Бэби Формула";
                dataGridView2.Rows[i + 91].Cells[1].Value = "Комбилипен";
                dataGridView2.Rows[i + 92].Cells[1].Value = "Мильгамма";
                dataGridView2.Rows[i + 93].Cells[1].Value = "Магнерот";
                dataGridView2.Rows[i + 94].Cells[1].Value = "Витамин C";
                dataGridView2.Rows[i + 95].Cells[1].Value = "Супрадин";
                dataGridView2.Rows[i + 96].Cells[1].Value = "Ларигама";
                dataGridView2.Rows[i + 97].Cells[1].Value = "Магне В6";
                dataGridView2.Rows[i + 98].Cells[1].Value = "Детримакс";
                dataGridView2.Rows[i + 99].Cells[1].Value = "Кальций-Д3";
                dataGridView2.Rows[i + 100].Cells[1].Value = "Селмевит";
                dataGridView2.Rows[i + 101].Cells[1].Value = "Витрум";
                dataGridView2.Rows[i + 102].Cells[1].Value = "Берокка Плюс";
                dataGridView2.Rows[i + 103].Cells[1].Value = "Перфектил";
                dataGridView2.Rows[i + 104].Cells[1].Value = "Элевит";

                dataGridView2.Rows[i + 105].Cells[0].Value = "Успокоительные";
                dataGridView2.Rows[i + 105].Cells[1].Value = "Тенотен";
                dataGridView2.Rows[i + 106].Cells[1].Value = "Пустырник";
                dataGridView2.Rows[i + 107].Cells[1].Value = "Персен";
                dataGridView2.Rows[i + 108].Cells[1].Value = "Ново-Пассит";
                dataGridView2.Rows[i + 109].Cells[1].Value = "Дормиплант";
                dataGridView2.Rows[i + 110].Cells[1].Value = "Лотосоник";
                dataGridView2.Rows[i + 111].Cells[1].Value = "Валемидин";
                dataGridView2.Rows[i + 112].Cells[1].Value = "Симпатил";
                dataGridView2.Rows[i + 113].Cells[1].Value = "Страттера ";
                dataGridView2.Rows[i + 114].Cells[1].Value = "Релаксозан";
                dataGridView2.Rows[i + 115].Cells[1].Value = "Корвалол";
                dataGridView2.Rows[i + 116].Cells[1].Value = "Стрессовит";
                dataGridView2.Rows[i + 117].Cells[1].Value = "Валериана";
                dataGridView2.Rows[i + 118].Cells[1].Value = "Триосон";
                dataGridView2.Rows[i + 119].Cells[1].Value = "Релаксен";
            }
            else
            {
                int i = dataGridView2.RowCount;
                dataGridView2.RowCount = i + 130;
                dataGridView2.Rows[i + 0].Cells[0].Value = "От кашля";
                dataGridView2.Rows[i + 0].Cells[1].Value = "Флуифор";
                dataGridView2.Rows[i + 1].Cells[1].Value = "Гербион Плюща";
                dataGridView2.Rows[i + 2].Cells[1].Value = "Гербион Первоцвета";
                dataGridView2.Rows[i + 3].Cells[1].Value = "Эльмуцин";
                dataGridView2.Rows[i + 4].Cells[1].Value = "Флюдитек";
                dataGridView2.Rows[i + 5].Cells[1].Value = "Аскорил";
                dataGridView2.Rows[i + 6].Cells[1].Value = "Доктор Мом";
                dataGridView2.Rows[i + 7].Cells[1].Value = "Бронхипрет";
                dataGridView2.Rows[i + 8].Cells[1].Value = "Флуимуцил";
                dataGridView2.Rows[i + 9].Cells[1].Value = "Лазолван";
                dataGridView2.Rows[i + 10].Cells[1].Value = "Синекод";
                dataGridView2.Rows[i + 11].Cells[1].Value = "Омнитус";

                dataGridView2.Rows[i + 12].Cells[0].Value = "От боли в горле";
                dataGridView2.Rows[i + 12].Cells[1].Value = "Стрепсилс";
                dataGridView2.Rows[i + 13].Cells[1].Value = "Тонзилгон";
                dataGridView2.Rows[i + 14].Cells[1].Value = "Доритрицин";
                dataGridView2.Rows[i + 15].Cells[1].Value = "Оки";
                dataGridView2.Rows[i + 16].Cells[1].Value = "Септолете Тотал";
                dataGridView2.Rows[i + 17].Cells[1].Value = "Анти-Ангин";
                dataGridView2.Rows[i + 18].Cells[1].Value = "Ангидак";
                dataGridView2.Rows[i + 19].Cells[1].Value = "Гексаспрей";
                dataGridView2.Rows[i + 20].Cells[1].Value = "Гексорал";

                dataGridView2.Rows[i + 21].Cells[0].Value = "От насморка";
                dataGridView2.Rows[i + 21].Cells[1].Value = "Аквалор Актив Форте";
                dataGridView2.Rows[i + 22].Cells[1].Value = "Лазолван Рино";
                dataGridView2.Rows[i + 23].Cells[1].Value = "Пиносол";
                dataGridView2.Rows[i + 24].Cells[1].Value = "Линаква Форте";
                dataGridView2.Rows[i + 25].Cells[1].Value = "Ринофлуимуцил";
                dataGridView2.Rows[i + 26].Cells[1].Value = "Изофра";
                dataGridView2.Rows[i + 27].Cells[1].Value = "Долфин";
                dataGridView2.Rows[i + 28].Cells[1].Value = "Отривин";
                dataGridView2.Rows[i + 29].Cells[1].Value = "Трамицент";
                dataGridView2.Rows[i + 30].Cells[1].Value = "Риностоп";

                dataGridView2.Rows[i + 31].Cells[0].Value = "От симптомов простуды";
                dataGridView2.Rows[i + 31].Cells[1].Value = "Антигриппин";
                dataGridView2.Rows[i + 32].Cells[1].Value = "Ринзасип";
                dataGridView2.Rows[i + 33].Cells[1].Value = "Фервекс";
                dataGridView2.Rows[i + 34].Cells[1].Value = "Анвимакс";
                dataGridView2.Rows[i + 35].Cells[1].Value = "ТераФлю";
                dataGridView2.Rows[i + 36].Cells[1].Value = "Стопгрипан";
                dataGridView2.Rows[i + 37].Cells[1].Value = "Колдакт Флю";
                dataGridView2.Rows[i + 38].Cells[1].Value = "Гриппофлю";
                dataGridView2.Rows[i + 39].Cells[1].Value = "Звездочка Флю";
                dataGridView2.Rows[i + 40].Cells[1].Value = "Колдрекс";

                dataGridView2.Rows[i + 41].Cells[0].Value = "От гриппа";
                dataGridView2.Rows[i + 41].Cells[1].Value = "Ингавирин";
                dataGridView2.Rows[i + 42].Cells[1].Value = "Арбидол";
                dataGridView2.Rows[i + 43].Cells[1].Value = "Триазавирин";
                dataGridView2.Rows[i + 44].Cells[1].Value = "Генферон";
                dataGridView2.Rows[i + 45].Cells[1].Value = "Бак-Сет Колд/Флю";
                dataGridView2.Rows[i + 46].Cells[1].Value = "Тамифлю";
                dataGridView2.Rows[i + 47].Cells[1].Value = "Осельтамивир-Акрихин";
                dataGridView2.Rows[i + 48].Cells[1].Value = "Орвирем";

                dataGridView2.Rows[i + 49].Cells[0].Value = "Для детей и мам";
                dataGridView2.Rows[i + 49].Cells[1].Value = "Лидент Бэби";
                dataGridView2.Rows[i + 50].Cells[1].Value = "Медела Пурелан";
                dataGridView2.Rows[i + 51].Cells[1].Value = "Бепантен";
                dataGridView2.Rows[i + 52].Cells[1].Value = "Судокрем";
                dataGridView2.Rows[i + 53].Cells[1].Value = "Камистад Бэби";
                dataGridView2.Rows[i + 54].Cells[1].Value = "Дантинорм Бэби";
                dataGridView2.Rows[i + 55].Cells[1].Value = "Ла-Кри гель";
                dataGridView2.Rows[i + 56].Cells[1].Value = "Неотанин";
                dataGridView2.Rows[i + 57].Cells[1].Value = "Отривин Бэби";
                dataGridView2.Rows[i + 58].Cells[1].Value = "Аквалор Беби";
                dataGridView2.Rows[i + 59].Cells[1].Value = "Фэст Бандаж";
                dataGridView2.Rows[i + 60].Cells[1].Value = "Бутылочки для кормления";
                dataGridView2.Rows[i + 61].Cells[1].Value = "Слюнявчики";

                dataGridView2.Rows[i + 62].Cells[0].Value = "Детские смеси";
                dataGridView2.Rows[i + 62].Cells[1].Value = "Педиашур Малоежка";
                dataGridView2.Rows[i + 63].Cells[1].Value = "Неокейт Джуниор";
                dataGridView2.Rows[i + 64].Cells[1].Value = "Нестле Нан Оптипро";
                dataGridView2.Rows[i + 65].Cells[1].Value = "Дп Неокейт Lcp";
                dataGridView2.Rows[i + 66].Cells[1].Value = "Симилак Голд";
                dataGridView2.Rows[i + 67].Cells[1].Value = "Пептамен Юниор";
                dataGridView2.Rows[i + 68].Cells[1].Value = "Клинутрен Юниор";
                dataGridView2.Rows[i + 69].Cells[1].Value = "Нэнни";

                dataGridView2.Rows[i + 70].Cells[0].Value = "Средства личной гигиены";
                dataGridView2.Rows[i + 70].Cells[1].Value = "Лакалют Фикс";
                dataGridView2.Rows[i + 71].Cells[1].Value = "Корега Экстра";
                dataGridView2.Rows[i + 72].Cells[1].Value = "Лактацид";
                dataGridView2.Rows[i + 73].Cells[1].Value = "Асепта Актив";
                dataGridView2.Rows[i + 74].Cells[1].Value = "Протефикс";
                dataGridView2.Rows[i + 75].Cells[1].Value = "А-Церумен";
                dataGridView2.Rows[i + 76].Cells[1].Value = "Гинокомфорт";
                dataGridView2.Rows[i + 77].Cells[1].Value = "Св12";
                dataGridView2.Rows[i + 78].Cells[1].Value = "Анауретте";

                dataGridView2.Rows[i + 79].Cells[0].Value = "Расходные медицинские материалы";
                dataGridView2.Rows[i + 79].Cells[1].Value = "Маска медицинская";
                dataGridView2.Rows[i + 80].Cells[1].Value = "Спрей антибактериальный";
                dataGridView2.Rows[i + 81].Cells[1].Value = "Антисептик";
                dataGridView2.Rows[i + 82].Cells[1].Value = "Перчатки медицинские";
                dataGridView2.Rows[i + 83].Cells[1].Value = "Бахилы";
                dataGridView2.Rows[i + 84].Cells[1].Value = "Пипетка";
                dataGridView2.Rows[i + 85].Cells[1].Value = "Таблетницы";
                dataGridView2.Rows[i + 86].Cells[1].Value = "Беруши";
                dataGridView2.Rows[i + 87].Cells[1].Value = "Дезинфицирующее средство";
                dataGridView2.Rows[i + 88].Cells[1].Value = "Растворы для контактных линз";
                dataGridView2.Rows[i + 89].Cells[1].Value = "Пластыри";

                dataGridView2.Rows[i + 90].Cells[0].Value = "Витамины и минералы";
                dataGridView2.Rows[i + 90].Cells[1].Value = "Бэби Формула";
                dataGridView2.Rows[i + 91].Cells[1].Value = "Комбилипен";
                dataGridView2.Rows[i + 92].Cells[1].Value = "Мильгамма";
                dataGridView2.Rows[i + 93].Cells[1].Value = "Магнерот";
                dataGridView2.Rows[i + 94].Cells[1].Value = "Витамин C";
                dataGridView2.Rows[i + 95].Cells[1].Value = "Супрадин";
                dataGridView2.Rows[i + 96].Cells[1].Value = "Ларигама";
                dataGridView2.Rows[i + 97].Cells[1].Value = "Магне В6";
                dataGridView2.Rows[i + 98].Cells[1].Value = "Детримакс";
                dataGridView2.Rows[i + 99].Cells[1].Value = "Кальций-Д3";
                dataGridView2.Rows[i + 100].Cells[1].Value = "Селмевит";
                dataGridView2.Rows[i + 101].Cells[1].Value = "Витрум";
                dataGridView2.Rows[i + 102].Cells[1].Value = "Берокка Плюс";
                dataGridView2.Rows[i + 103].Cells[1].Value = "Перфектил";
                dataGridView2.Rows[i + 104].Cells[1].Value = "Элевит";

                dataGridView2.Rows[i + 105].Cells[0].Value = "Успокоительные";
                dataGridView2.Rows[i + 105].Cells[1].Value = "Тенотен";
                dataGridView2.Rows[i + 106].Cells[1].Value = "Пустырник";
                dataGridView2.Rows[i + 107].Cells[1].Value = "Персен";
                dataGridView2.Rows[i + 108].Cells[1].Value = "Ново-Пассит";
                dataGridView2.Rows[i + 109].Cells[1].Value = "Дормиплант";
                dataGridView2.Rows[i + 110].Cells[1].Value = "Лотосоник";
                dataGridView2.Rows[i + 111].Cells[1].Value = "Валемидин";
                dataGridView2.Rows[i + 112].Cells[1].Value = "Симпатил";
                dataGridView2.Rows[i + 113].Cells[1].Value = "Страттера ";
                dataGridView2.Rows[i + 114].Cells[1].Value = "Релаксозан";
                dataGridView2.Rows[i + 115].Cells[1].Value = "Корвалол";
                dataGridView2.Rows[i + 116].Cells[1].Value = "Стрессовит";
                dataGridView2.Rows[i + 117].Cells[1].Value = "Валериана";
                dataGridView2.Rows[i + 118].Cells[1].Value = "Триосон";
                dataGridView2.Rows[i + 119].Cells[1].Value = "Релаксен";

                dataGridView2.Rows[i+120].Cells[0].Value = "Антидепрессанты";
                dataGridView2.Rows[i+120].Cells[1].Value = "Пароксетин";
                dataGridView2.Rows[i+121].Cells[1].Value = "Селектра";
                dataGridView2.Rows[i+122].Cells[1].Value = "Велаксин";
                dataGridView2.Rows[i+123].Cells[1].Value = "Циталопрам";
                dataGridView2.Rows[i+124].Cells[1].Value = "Симбалта";
                dataGridView2.Rows[i+125].Cells[1].Value = "Серлифт";
                dataGridView2.Rows[i+126].Cells[1].Value = "Депратал";
                dataGridView2.Rows[i+127].Cells[1].Value = "Золофт";
                dataGridView2.Rows[i+128].Cells[1].Value = "Флуоксетин";
                dataGridView2.Rows[i+129].Cells[1].Value = "Амитриптилин";
            }            
        }
	private void trc()
        {
            if (radioButton1.Checked == true)
            {
                int i = dataGridView2.RowCount;
                dataGridView2.RowCount = i + 104;
                dataGridView2.Rows[i + 0].Cells[0].Value = "От кашля";
                dataGridView2.Rows[i + 0].Cells[1].Value = "Гербион Плюща";
                dataGridView2.Rows[i + 1].Cells[1].Value = "Эльмуцин";
                dataGridView2.Rows[i + 2].Cells[1].Value = "Доктор Мом";
                dataGridView2.Rows[i + 3].Cells[1].Value = "Бронхипрет";
                dataGridView2.Rows[i + 4].Cells[1].Value = "Флуимуцил";
                dataGridView2.Rows[i + 5].Cells[1].Value = "Лазолван";
                dataGridView2.Rows[i + 6].Cells[1].Value = "Синекод";
                dataGridView2.Rows[i + 7].Cells[1].Value = "Омнитус";

                dataGridView2.Rows[i + 8].Cells[0].Value = "От боли в горле";
                dataGridView2.Rows[i + 8].Cells[1].Value = "Стрепсилс";
                dataGridView2.Rows[i + 9].Cells[1].Value = "Тонзилгон";
                dataGridView2.Rows[i + 10].Cells[1].Value = "Септолете Тотал";
                dataGridView2.Rows[i + 11].Cells[1].Value = "Анти-Ангин";
                dataGridView2.Rows[i + 12].Cells[1].Value = "Гексаспрей";
                dataGridView2.Rows[i + 13].Cells[1].Value = "Гексорал";

                dataGridView2.Rows[i + 14].Cells[0].Value = "От насморка";
                dataGridView2.Rows[i + 14].Cells[1].Value = "Аквалор Актив Форте";
                dataGridView2.Rows[i + 15].Cells[1].Value = "Лазолван Рино";
                dataGridView2.Rows[i + 16].Cells[1].Value = "Ринофлуимуцил";
                dataGridView2.Rows[i + 17].Cells[1].Value = "Долфин";
                dataGridView2.Rows[i + 18].Cells[1].Value = "Отривин";
                dataGridView2.Rows[i + 19].Cells[1].Value = "Трамицент";
                dataGridView2.Rows[i + 20].Cells[1].Value = "Риностоп";

                dataGridView2.Rows[i + 21].Cells[0].Value = "От симптомов простуды";
                dataGridView2.Rows[i + 21].Cells[1].Value = "Антигриппин";
                dataGridView2.Rows[i + 22].Cells[1].Value = "Ринзасип";
                dataGridView2.Rows[i + 23].Cells[1].Value = "Фервекс";
                dataGridView2.Rows[i + 24].Cells[1].Value = "Анвимакс";
                dataGridView2.Rows[i + 25].Cells[1].Value = "ТераФлю";
                dataGridView2.Rows[i + 26].Cells[1].Value = "Стопгрипан";
                dataGridView2.Rows[i + 27].Cells[1].Value = "Колдакт Флю";
                dataGridView2.Rows[i + 28].Cells[1].Value = "Гриппофлю";
                dataGridView2.Rows[i + 29].Cells[1].Value = "Звездочка Флю";
                dataGridView2.Rows[i + 30].Cells[1].Value = "Колдрекс";

                dataGridView2.Rows[i + 31].Cells[0].Value = "От гриппа";
                dataGridView2.Rows[i + 31].Cells[1].Value = "Ингавирин";
                dataGridView2.Rows[i + 32].Cells[1].Value = "Арбидол";
                dataGridView2.Rows[i + 33].Cells[1].Value = "Триазавирин";
                dataGridView2.Rows[i + 34].Cells[1].Value = "Генферон";
                dataGridView2.Rows[i + 35].Cells[1].Value = "Тамифлю";
                dataGridView2.Rows[i + 36].Cells[1].Value = "Орвирем";

                dataGridView2.Rows[i + 37].Cells[0].Value = "Для детей и мам";
                dataGridView2.Rows[i + 37].Cells[1].Value = "Лидент Бэби";
                dataGridView2.Rows[i + 38].Cells[1].Value = "Медела Пурелан";
                dataGridView2.Rows[i + 39].Cells[1].Value = "Бепантен";
                dataGridView2.Rows[i + 40].Cells[1].Value = "Судокрем";
                dataGridView2.Rows[i + 41].Cells[1].Value = "Камистад Бэби";
                dataGridView2.Rows[i + 42].Cells[1].Value = "Дантинорм Бэби";
                dataGridView2.Rows[i + 43].Cells[1].Value = "Неотанин";
                dataGridView2.Rows[i + 44].Cells[1].Value = "Бутылочки для кормления";
                dataGridView2.Rows[i + 45].Cells[1].Value = "Слюнявчики";

                dataGridView2.Rows[i + 46].Cells[0].Value = "Детские смеси";
                dataGridView2.Rows[i + 46].Cells[1].Value = "Педиашур Малоежка";
                dataGridView2.Rows[i + 47].Cells[1].Value = "Неокейт Джуниор";
                dataGridView2.Rows[i + 48].Cells[1].Value = "Нестле Нан Оптипро";
                dataGridView2.Rows[i + 49].Cells[1].Value = "Дп Неокейт Lcp";
                dataGridView2.Rows[i + 50].Cells[1].Value = "Симилак Голд";
                dataGridView2.Rows[i + 51].Cells[1].Value = "Пептамен Юниор";
                dataGridView2.Rows[i + 52].Cells[1].Value = "Клинутрен Юниор";
                dataGridView2.Rows[i + 53].Cells[1].Value = "Нэнни";

                dataGridView2.Rows[i + 54].Cells[0].Value = "Средства личной гигиены";
                dataGridView2.Rows[i + 55].Cells[1].Value = "Лакалют Фикс";
                dataGridView2.Rows[i + 55].Cells[1].Value = "Корега Экстра";
                dataGridView2.Rows[i + 56].Cells[1].Value = "Лактацид";
                dataGridView2.Rows[i + 57].Cells[1].Value = "Асепта Актив";
                dataGridView2.Rows[i + 58].Cells[1].Value = "Протефикс";
                dataGridView2.Rows[i + 59].Cells[1].Value = "А-Церумен";
                dataGridView2.Rows[i + 60].Cells[1].Value = "Гинокомфорт";
                dataGridView2.Rows[i + 61].Cells[1].Value = "Анауретте";

                dataGridView2.Rows[i + 62].Cells[0].Value = "Расходные медицинские материалы";
                dataGridView2.Rows[i + 62].Cells[1].Value = "Маска медицинская";
                dataGridView2.Rows[i + 63].Cells[1].Value = "Спрей антибактериальный";
                dataGridView2.Rows[i + 64].Cells[1].Value = "Антисептик";
                dataGridView2.Rows[i + 65].Cells[1].Value = "Перчатки медицинские";
                dataGridView2.Rows[i + 66].Cells[1].Value = "Бахилы";
                dataGridView2.Rows[i + 67].Cells[1].Value = "Пипетка";
                dataGridView2.Rows[i + 68].Cells[1].Value = "Таблетницы";
                dataGridView2.Rows[i + 69].Cells[1].Value = "Беруши";
                dataGridView2.Rows[i + 70].Cells[1].Value = "Дезинфицирующее средство";
                dataGridView2.Rows[i + 71].Cells[1].Value = "Растворы для контактных линз";
                dataGridView2.Rows[i + 72].Cells[1].Value = "Пластыри";

                dataGridView2.Rows[i + 73].Cells[0].Value = "Витамины и минералы";
                dataGridView2.Rows[i + 73].Cells[1].Value = "Бэби Формула";
                dataGridView2.Rows[i + 74].Cells[1].Value = "Комбилипен";
                dataGridView2.Rows[i + 75].Cells[1].Value = "Мильгамма";
                dataGridView2.Rows[i + 76].Cells[1].Value = "Магнерот";
                dataGridView2.Rows[i + 77].Cells[1].Value = "Витамин C";
                dataGridView2.Rows[i + 78].Cells[1].Value = "Магнелис В6";
                dataGridView2.Rows[i + 79].Cells[1].Value = "Супрадин";
                dataGridView2.Rows[i + 80].Cells[1].Value = "Ларигама";
                dataGridView2.Rows[i + 81].Cells[1].Value = "Магне В6";
                dataGridView2.Rows[i + 82].Cells[1].Value = "Детримакс";
                dataGridView2.Rows[i + 83].Cells[1].Value = "Кальций-Д3";
                dataGridView2.Rows[i + 84].Cells[1].Value = "Селмевит";
                dataGridView2.Rows[i + 85].Cells[1].Value = "Витрум";
                dataGridView2.Rows[i + 86].Cells[1].Value = "Берокка Плюс";
                dataGridView2.Rows[i + 87].Cells[1].Value = "Перфектил";
                dataGridView2.Rows[i + 88].Cells[1].Value = "Элевит";

                dataGridView2.Rows[i + 89].Cells[0].Value = "Успокоительные";
                dataGridView2.Rows[i + 89].Cells[1].Value = "Тенотен";
                dataGridView2.Rows[i + 90].Cells[1].Value = "Пустырник";
                dataGridView2.Rows[i + 91].Cells[1].Value = "Персен";
                dataGridView2.Rows[i + 92].Cells[1].Value = "Ново-Пассит";
                dataGridView2.Rows[i + 93].Cells[1].Value = "Дормиплант";
                dataGridView2.Rows[i + 94].Cells[1].Value = "Лотосоник";
                dataGridView2.Rows[i + 95].Cells[1].Value = "Валемидин";
                dataGridView2.Rows[i + 96].Cells[1].Value = "Симпатил";
                dataGridView2.Rows[i + 97].Cells[1].Value = "Страттера ";
                dataGridView2.Rows[i + 98].Cells[1].Value = "Релаксозан";
                dataGridView2.Rows[i + 99].Cells[1].Value = "Корвалол";
                dataGridView2.Rows[i + 100].Cells[1].Value = "Стрессовит";
                dataGridView2.Rows[i + 101].Cells[1].Value = "Валериана";
                dataGridView2.Rows[i + 102].Cells[1].Value = "Триосон";
                dataGridView2.Rows[i + 103].Cells[1].Value = "Релаксен";
            }
            else
            {
                int i = dataGridView2.RowCount;
                dataGridView2.RowCount = i + 114;
                dataGridView2.Rows[i + 0].Cells[0].Value = "От кашля";
                dataGridView2.Rows[i + 0].Cells[1].Value = "Гербион Плюща";
                dataGridView2.Rows[i + 1].Cells[1].Value = "Эльмуцин";
                dataGridView2.Rows[i + 2].Cells[1].Value = "Доктор Мом";
                dataGridView2.Rows[i + 3].Cells[1].Value = "Бронхипрет";
                dataGridView2.Rows[i + 4].Cells[1].Value = "Флуимуцил";
                dataGridView2.Rows[i + 5].Cells[1].Value = "Лазолван";
                dataGridView2.Rows[i + 6].Cells[1].Value = "Синекод";
                dataGridView2.Rows[i + 7].Cells[1].Value = "Омнитус";

                dataGridView2.Rows[i + 8].Cells[0].Value = "От боли в горле";
                dataGridView2.Rows[i + 8].Cells[1].Value = "Стрепсилс";
                dataGridView2.Rows[i + 9].Cells[1].Value = "Тонзилгон";
                dataGridView2.Rows[i + 10].Cells[1].Value = "Септолете Тотал";
                dataGridView2.Rows[i + 11].Cells[1].Value = "Анти-Ангин";
                dataGridView2.Rows[i + 12].Cells[1].Value = "Гексаспрей";
                dataGridView2.Rows[i + 13].Cells[1].Value = "Гексорал";

                dataGridView2.Rows[i + 14].Cells[0].Value = "От насморка";
                dataGridView2.Rows[i + 14].Cells[1].Value = "Аквалор Актив Форте";
                dataGridView2.Rows[i + 15].Cells[1].Value = "Лазолван Рино";
                dataGridView2.Rows[i + 16].Cells[1].Value = "Ринофлуимуцил";
                dataGridView2.Rows[i + 17].Cells[1].Value = "Долфин";
                dataGridView2.Rows[i + 18].Cells[1].Value = "Отривин";
                dataGridView2.Rows[i + 19].Cells[1].Value = "Трамицент";
                dataGridView2.Rows[i + 20].Cells[1].Value = "Риностоп";

                dataGridView2.Rows[i + 21].Cells[0].Value = "От симптомов простуды";
                dataGridView2.Rows[i + 21].Cells[1].Value = "Антигриппин";
                dataGridView2.Rows[i + 22].Cells[1].Value = "Ринзасип";
                dataGridView2.Rows[i + 23].Cells[1].Value = "Фервекс";
                dataGridView2.Rows[i + 24].Cells[1].Value = "Анвимакс";
                dataGridView2.Rows[i + 25].Cells[1].Value = "ТераФлю";
                dataGridView2.Rows[i + 26].Cells[1].Value = "Стопгрипан";
                dataGridView2.Rows[i + 27].Cells[1].Value = "Колдакт Флю";
                dataGridView2.Rows[i + 28].Cells[1].Value = "Гриппофлю";
                dataGridView2.Rows[i + 29].Cells[1].Value = "Звездочка Флю";
                dataGridView2.Rows[i + 30].Cells[1].Value = "Колдрекс";

                dataGridView2.Rows[i + 31].Cells[0].Value = "От гриппа";
                dataGridView2.Rows[i + 31].Cells[1].Value = "Ингавирин";
                dataGridView2.Rows[i + 32].Cells[1].Value = "Арбидол";
                dataGridView2.Rows[i + 33].Cells[1].Value = "Триазавирин";
                dataGridView2.Rows[i + 34].Cells[1].Value = "Генферон";
                dataGridView2.Rows[i + 35].Cells[1].Value = "Тамифлю";
                dataGridView2.Rows[i + 36].Cells[1].Value = "Орвирем";

                dataGridView2.Rows[i + 37].Cells[0].Value = "Для детей и мам";
                dataGridView2.Rows[i + 37].Cells[1].Value = "Лидент Бэби";
                dataGridView2.Rows[i + 38].Cells[1].Value = "Медела Пурелан";
                dataGridView2.Rows[i + 39].Cells[1].Value = "Бепантен";
                dataGridView2.Rows[i + 40].Cells[1].Value = "Судокрем";
                dataGridView2.Rows[i + 41].Cells[1].Value = "Камистад Бэби";
                dataGridView2.Rows[i + 42].Cells[1].Value = "Дантинорм Бэби";
                dataGridView2.Rows[i + 43].Cells[1].Value = "Неотанин";
                dataGridView2.Rows[i + 44].Cells[1].Value = "Бутылочки для кормления";
                dataGridView2.Rows[i + 45].Cells[1].Value = "Слюнявчики";

                dataGridView2.Rows[i + 46].Cells[0].Value = "Детские смеси";
                dataGridView2.Rows[i + 46].Cells[1].Value = "Педиашур Малоежка";
                dataGridView2.Rows[i + 47].Cells[1].Value = "Неокейт Джуниор";
                dataGridView2.Rows[i + 48].Cells[1].Value = "Нестле Нан Оптипро";
                dataGridView2.Rows[i + 49].Cells[1].Value = "Дп Неокейт Lcp";
                dataGridView2.Rows[i + 50].Cells[1].Value = "Симилак Голд";
                dataGridView2.Rows[i + 51].Cells[1].Value = "Пептамен Юниор";
                dataGridView2.Rows[i + 52].Cells[1].Value = "Клинутрен Юниор";
                dataGridView2.Rows[i + 53].Cells[1].Value = "Нэнни";

                dataGridView2.Rows[i + 54].Cells[0].Value = "Средства личной гигиены";
                dataGridView2.Rows[i + 55].Cells[1].Value = "Лакалют Фикс";
                dataGridView2.Rows[i + 55].Cells[1].Value = "Корега Экстра";
                dataGridView2.Rows[i + 56].Cells[1].Value = "Лактацид";
                dataGridView2.Rows[i + 57].Cells[1].Value = "Асепта Актив";
                dataGridView2.Rows[i + 58].Cells[1].Value = "Протефикс";
                dataGridView2.Rows[i + 59].Cells[1].Value = "А-Церумен";
                dataGridView2.Rows[i + 60].Cells[1].Value = "Гинокомфорт";
                dataGridView2.Rows[i + 61].Cells[1].Value = "Анауретте";

                dataGridView2.Rows[i + 62].Cells[0].Value = "Расходные медицинские материалы";
                dataGridView2.Rows[i + 62].Cells[1].Value = "Маска медицинская";
                dataGridView2.Rows[i + 63].Cells[1].Value = "Спрей антибактериальный";
                dataGridView2.Rows[i + 64].Cells[1].Value = "Антисептик";
                dataGridView2.Rows[i + 65].Cells[1].Value = "Перчатки медицинские";
                dataGridView2.Rows[i + 66].Cells[1].Value = "Бахилы";
                dataGridView2.Rows[i + 67].Cells[1].Value = "Пипетка";
                dataGridView2.Rows[i + 68].Cells[1].Value = "Таблетницы";
                dataGridView2.Rows[i + 69].Cells[1].Value = "Беруши";
                dataGridView2.Rows[i + 70].Cells[1].Value = "Дезинфицирующее средство";
                dataGridView2.Rows[i + 71].Cells[1].Value = "Растворы для контактных линз";
                dataGridView2.Rows[i + 72].Cells[1].Value = "Пластыри";

                dataGridView2.Rows[i + 73].Cells[0].Value = "Витамины и минералы";
                dataGridView2.Rows[i + 73].Cells[1].Value = "Бэби Формула";
                dataGridView2.Rows[i + 74].Cells[1].Value = "Комбилипен";
                dataGridView2.Rows[i + 75].Cells[1].Value = "Мильгамма";
                dataGridView2.Rows[i + 76].Cells[1].Value = "Магнерот";
                dataGridView2.Rows[i + 77].Cells[1].Value = "Витамин C";
                dataGridView2.Rows[i + 78].Cells[1].Value = "Магнелис В6";
                dataGridView2.Rows[i + 79].Cells[1].Value = "Супрадин";
                dataGridView2.Rows[i + 80].Cells[1].Value = "Ларигама";
                dataGridView2.Rows[i + 81].Cells[1].Value = "Магне В6";
                dataGridView2.Rows[i + 82].Cells[1].Value = "Детримакс";
                dataGridView2.Rows[i + 83].Cells[1].Value = "Кальций-Д3";
                dataGridView2.Rows[i + 84].Cells[1].Value = "Селмевит";
                dataGridView2.Rows[i + 85].Cells[1].Value = "Витрум";
                dataGridView2.Rows[i + 86].Cells[1].Value = "Берокка Плюс";
                dataGridView2.Rows[i + 87].Cells[1].Value = "Перфектил";
                dataGridView2.Rows[i + 88].Cells[1].Value = "Элевит";

                dataGridView2.Rows[i + 89].Cells[0].Value = "Успокоительные";
                dataGridView2.Rows[i + 89].Cells[1].Value = "Тенотен";
                dataGridView2.Rows[i + 90].Cells[1].Value = "Пустырник";
                dataGridView2.Rows[i + 91].Cells[1].Value = "Персен";
                dataGridView2.Rows[i + 92].Cells[1].Value = "Ново-Пассит";
                dataGridView2.Rows[i + 93].Cells[1].Value = "Дормиплант";
                dataGridView2.Rows[i + 94].Cells[1].Value = "Лотосоник";
                dataGridView2.Rows[i + 95].Cells[1].Value = "Валемидин";
                dataGridView2.Rows[i + 96].Cells[1].Value = "Симпатил";
                dataGridView2.Rows[i + 97].Cells[1].Value = "Страттера ";
                dataGridView2.Rows[i + 98].Cells[1].Value = "Релаксозан";
                dataGridView2.Rows[i + 99].Cells[1].Value = "Корвалол";
                dataGridView2.Rows[i + 100].Cells[1].Value = "Стрессовит";
                dataGridView2.Rows[i + 101].Cells[1].Value = "Валериана";
                dataGridView2.Rows[i + 102].Cells[1].Value = "Триосон";
                dataGridView2.Rows[i + 103].Cells[1].Value = "Релаксен";

                dataGridView2.Rows[i + 104].Cells[0].Value = "Антидепрессанты";
                dataGridView2.Rows[i + 104].Cells[1].Value = "Пароксетин";
                dataGridView2.Rows[i + 105].Cells[1].Value = "Селектра";
                dataGridView2.Rows[i + 106].Cells[1].Value = "Велаксин";
                dataGridView2.Rows[i + 107].Cells[1].Value = "Циталопрам";
                dataGridView2.Rows[i + 108].Cells[1].Value = "Симбалта";
                dataGridView2.Rows[i + 109].Cells[1].Value = "Серлифт";
                dataGridView2.Rows[i + 110].Cells[1].Value = "Депратал";
                dataGridView2.Rows[i + 111].Cells[1].Value = "Золофт";
                dataGridView2.Rows[i + 112].Cells[1].Value = "Флуоксетин";
                dataGridView2.Rows[i + 113].Cells[1].Value = "Амитриптилин";
            }

    }
    }
} 
