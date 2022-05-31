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
            SqlDataAdapter adapter = new SqlDataAdapter("Select MONTH([Безрецептурные продажи].Дата) AS Месяц, SUM([Безрецептурные продажи].Количество) AS Количество " +
                "FROM[Безрецептурные продажи] inner join([Характеристики лекарств] inner join Лекарства ON Лекарства.[Код лекарства] = " +
                "[Характеристики лекарств].[Код лекарства])ON[Безрецептурные продажи].[Код характеристики] =[Характеристики лекарств].[Код характеристики] " +
                "WHERE Лекарства.Наименование = '" + comboBox1.Text+ "' Group by MONTH([Безрецептурные продажи].Дата), Лекарства.Наименование UNION Select" +
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
            }



        }

private void progznoz_Load(object sender, EventArgs e)
{
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
}

private void button3_Click(object sender, EventArgs e)
{
method();
}
}
}
