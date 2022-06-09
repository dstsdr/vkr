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
    public partial class prodazhi__ : Form
    {
        public prodazhi__()
        {
            InitializeComponent();
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            groupBox4MK.Visible = false;
            groupBox1MZ.Visible = true;
            checkBox4.Visible=false;
            checkBox4.Checked=false;
            checkBox1.Visible = true;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            groupBox4MK.Visible = true;
            groupBox1MZ.Visible = false;
            checkBox4.Visible = true;
            checkBox1.Visible = false;
            checkBox1.Checked = false;

        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            groupBox4MK.Visible = false;
            groupBox3.Visible = false;
            groupBox2.Visible = false;
            groupBox1MZ.Visible = false;
            checkBox4.Visible = false;
            checkBox1.Visible = false;
            checkBox1.Checked = false;
            checkBox4.Checked = false;
            checkBox2.Visible = true;
            checkBox5.Checked = false;
            checkBox5.Visible = false;
            this.Height = 330;

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true) recepttovar();
            groupBox4MK.Visible = true;
            groupBox1MZ.Visible = false;
            checkBox4.Visible = true;
            checkBox1.Visible = false;
            checkBox1.Checked = false;
            checkBox4.Checked = false;
            checkBox2.Visible = false;
            checkBox5.Visible = true;
            checkBox2.Checked = false;
            groupBox3.Visible = true;
            groupBox2.Visible = true;
            this.Height= 637;
        }
        SqlConnection Connection = new SqlConnection(@"Data Source=LAPTOP-862V88EF\SQLEXPRESS;Initial Catalog=vkr;Integrated Security=True");

        private void recepttovar()
        {
            Connection.Open();
            SqlCommand cmd4 = Connection.CreateCommand();
            cmd4.CommandType = CommandType.Text;
            cmd4.CommandText = "SELECT CONCAT ([Характеристики лекарств].[Код характеристики] , ' ',Лекарства.Наименование, " +
                "' ', [Характеристики лекарств].Дозировка, SUBSTRING([Единицы измерения].Обозначение, 1, CHARINDEX(' ', [Единицы измерения].Обозначение)-1),' ', [Форма выпуска].Форма, ' ', " +
                "Производитель.Наименование) as ДА from[Характеристики лекарств] inner join(Лекарства inner join[Условие отпуска] ON " +
                "[Условие отпуска].[Код условия]= Лекарства.[Код условия]) ON Лекарства.[Код лекарства] =" +
                "[Характеристики лекарств].[Код лекарства] inner join([Форма выпуска] inner join [Единицы измерения] ON" +
                "[Единицы измерения].[Код ед.изм]=[Форма выпуска].[Код ед.изм]) ON[Форма выпуска].[Код формы] =" +
                "[Характеристики лекарств].[Код формы] inner join Производитель ON Производитель.[Код производителя] =" +
                "[Характеристики лекарств].[Код производителя] inner join[Серийный номер] ON[Характеристики лекарств].[Код характеристики] " +
                "=[Серийный номер].[Код характеристики] WHERE[Серийный номер].[Код рецептурной продажи] is null and " +
                "[Серийный номер].[Код безрецептурной продажи] is null and [Условие отпуска].[Код условия] = 1 GROUP BY" +
                "[Характеристики лекарств].Дозировка, Производитель.Наименование , [Характеристики лекарств].Наценка, " +
                "Лекарства.Наименование, [Форма выпуска].Форма, [Характеристики лекарств].[Номер договора], [Единицы измерения].Обозначение, " +
                "[Характеристики лекарств].[Код характеристики] ORDER BY ДА ASC";
            cmd4.ExecuteNonQuery();
            DataTable dt4 = new DataTable();
            SqlDataAdapter da4 = new SqlDataAdapter(cmd4);
            da4.Fill(dt4);
            foreach (DataRow dr4 in dt4.Rows)
            {
                checkedListBox3.Items.Add(dr4["ДА"].ToString());
            }
            Connection.Close();
        }
        private void skidki(string id)
        {
            Connection.Open();
            SqlCommand cmd3 = Connection.CreateCommand();
            cmd3.CommandType = CommandType.Text;
            cmd3.CommandText = "SELECT CONCAT (Лекарства.Наименование, ' ' ,[Форма выпуска].Форма, ' ',Скидки.Размер, '% от ', Скидки.[Минимальное количество], ' шт.') AS СКИДКИ " +
                "from Скидки inner join ([Характеристики лекарств]  inner join Лекарства ON Лекарства.[Код лекарства]=[Характеристики лекарств].[Код лекарства] " +
                "inner join [Форма выпуска] ON [Форма выпуска].[Код формы]=[Характеристики лекарств].[Код формы])ON[Характеристики лекарств].[Код характеристики] = " +
                "Скидки.[Код характеристики] WHERE [Характеристики лекарств].[Код характеристики] =" + id;
            cmd3.ExecuteNonQuery();
            DataTable dt3 = new DataTable();
            SqlDataAdapter da3 = new SqlDataAdapter(cmd3);
            da3.Fill(dt3);
            foreach (DataRow dr3 in dt3.Rows)
            {
                checkedListBox2.Items.Add(dr3["СКИДКИ"].ToString());
            }
            Connection.Close();
        }
        private void seriya(string id)
        {
            Connection.Open();
            SqlCommand cmd4 = Connection.CreateCommand();
            cmd4.CommandType = CommandType.Text;
            cmd4.CommandText = "SELECT [Серийный номер].[Серийный номер] AS ДА from[Серийный номер] inner join[Характеристики лекарств] " +
                "ON[Характеристики лекарств].[Код характеристики] =[Серийный номер].[Код характеристики] WHERE " +
                "[Серийный номер].[Код рецептурной продажи] is null and [Серийный номер].[Код безрецептурной продажи] " +
                "is null and [Характеристики лекарств].[Код характеристики] =" + id;
            cmd4.ExecuteNonQuery();
            DataTable dt4 = new DataTable();
            SqlDataAdapter da4 = new SqlDataAdapter(cmd4);
            da4.Fill(dt4);
            foreach (DataRow dr4 in dt4.Rows)
            {
                checkedListBox1.Items.Add(dr4["ДА"].ToString());
            }
            Connection.Close();
        }

        private void checkedListBox3_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Checked)
            {
                string name = checkedListBox3.SelectedItem.ToString();
                string[] words = name.Split(' ');
                string id = words[0];
                seriya(id);
                skidki(id);
            }
            else
            {
                checkedListBox1.Items.Clear();
                checkedListBox1.Items.Add("Добавить серийный номер");
                checkedListBox2.Items.Clear();
                checkedListBox2.Items.Add("Добавить скидку");

                for (int i = 0; i < checkedListBox3.Items.Count; i++)
                {
                    if (i != Convert.ToInt32(checkedListBox3.SelectedIndex))
                    {
                        if (checkedListBox3.GetItemChecked(i)) // работает только с кнопкой
                        {
                            string id = (checkedListBox3.Items[i]).ToString().Remove(checkedListBox3.Items[i].ToString().IndexOf(' '));
                            seriya(id);
                            skidki(id);
                        }
                    }
                }
            }

        }
        private void checkedListBox2_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            dataGridView1.Rows.Clear();
            for (int i = 0; i < checkedListBox3.Items.Count; i++)
            {
                if (checkedListBox3.GetItemChecked(i))
                {
                    dataGridView1.Rows.Add((checkedListBox3.Items[i]).ToString().Substring(0, (checkedListBox3.Items[i]).ToString().IndexOf(' ')));
                }
            }
            Connection.Open();
             for (int i = 0; i < checkedListBox1.Items.Count; i++)
             {
                 if (checkedListBox1.GetItemChecked(i))
                 {
                     SqlCommand cmd3 = Connection.CreateCommand();
                     cmd3.CommandType = CommandType.Text;
                     cmd3.CommandText = "select [Характеристики лекарств].[Код характеристики] AS НОМЕР from[Характеристики лекарств] inner join[Серийный номер] " +
                         "ON[Характеристики лекарств].[Код характеристики] =[Серийный номер].[Код характеристики] where[Серийный номер].[Серийный номер] = " + (checkedListBox1.Items[i]).ToString();
                     cmd3.ExecuteNonQuery();
                     DataTable dt3 = new DataTable();
                     SqlDataAdapter da3 = new SqlDataAdapter(cmd3);
                     da3.Fill(dt3);
                     int N = 0;
                     foreach (DataRow dr3 in dt3.Rows)
                     {
                         N =Convert.ToInt32(dr3["НОМЕР"]);
                     }
                    int k = 1;
                    for (int j=0; j<dataGridView1.Rows.Count-1; j++)
                     {
                         if (Convert.ToInt32(dataGridView1[0, j].Value) == N)
                        {
                            dataGridView1[1, j].Value = Convert.ToInt32(dataGridView1[1, j].Value)+k;
                        }
                    }
                 }
             }            
            Connection.Close();
           // rashet();
        }
        private void rashet ()
        {
            for (int i = 0; i<checkedListBox2.Items.Count; i++)
            {
                if (checkedListBox2.GetItemChecked(i))
                {
                    string sel = (checkedListBox2.Items[i]).ToString();
                    string[] words = sel.Split(' ');
                    for (int l = 0; l<checkedListBox3.Items.Count; l++)
                    {
                        if (checkedListBox3.GetItemChecked(l))
                        {
                            string tovar = (checkedListBox3.Items[l]).ToString();
                            string[] t = tovar.Split(' ');
                            if (t[1]==words[0] && t[3]==words[1])
                            {
                                for (int j = 0; j<dataGridView1.Rows.Count - 1; j++)
                                {
                                    if (dataGridView1[0, j].Value.ToString() == t[0] && Convert.ToInt32(dataGridView1[1, j].Value) >=Convert.ToInt32(words[4]))
                                    {
                                        dataGridView1[2, j].Value = words[2];
                                    }
                                    else dataGridView1[2, j].Value = "0%";
                                }
                            }
                        }
                    }                    
                }
            }
            Connection.Open();
            for (int i= 0; i<dataGridView1.RowCount-1; i++)
            {
                SqlCommand cmd4 = Connection.CreateCommand();
                cmd4.CommandType = CommandType.Text;
                cmd4.CommandText = "SELECT [Характеристики лекарств].Цена AS ЦЕНА from [Характеристики лекарств] where [Характеристики лекарств].[Код характеристики] =" + dataGridView1[0, i].Value.ToString();
                cmd4.ExecuteNonQuery();
                DataTable dt4 = new DataTable();
                SqlDataAdapter da4 = new SqlDataAdapter(cmd4);
                da4.Fill(dt4);
                foreach (DataRow dr4 in dt4.Rows)
                {
                    dataGridView1[3, i].Value = dr4["ЦЕНА"].ToString();
                    if (dataGridView1[2, i].Value.ToString() != "0")
                    {
                        dataGridView1[3, i].Value = Convert.ToDecimal(dataGridView1[3, i].Value) - (Convert.ToDecimal(dataGridView1[3, i].Value) * (Convert.ToDecimal(dataGridView1[2, i].Value.ToString().Substring(0, dataGridView1[2, i].Value.ToString().Length - 1)) / 100));
                        dataGridView1[3, i].Value = Convert.ToDecimal(dataGridView1[3, i].Value) * Convert.ToDecimal(dataGridView1[1, i].Value);
                    }
                }
            }
            Connection.Close();            
        }

        private void checkedListBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            rashet();          
        }

        private void prodazhi___Load(object sender, EventArgs e)
        {

        }
    }
}
