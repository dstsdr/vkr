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
using System.Text.RegularExpressions;

namespace vkr
{
    public partial class prodazhi : Form
    {
        public prodazhi()
        {
            InitializeComponent();
        }

        private void prodazhi_Load(object sender, EventArgs e)
        {
            dataset();
        }
        SqlConnection Connection = new SqlConnection(@"Data Source=LAPTOP-862V88EF\SQLEXPRESS;Initial Catalog=vkr;Integrated Security=True");

        private void dataset()
        {
            Connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT [Безрецептурные продажи].Дата, SUM([Безрецептурные продажи].Сумма) as 'Сумма', STRING_AGG(CONCAT('*', [Форма выпуска].Форма, ' ', " +
                "Лекарства.Наименование, ' ',[Характеристики лекарств].Дозировка, ' ', LEFT([Единицы измерения].Обозначение, CHARINDEX(' ',[Единицы измерения].Обозначение) - 1)), ',') AS Товары, " +
                "[Серийный номер].[Код безрецептурной продажи] from[Серийный номер] inner join[Безрецептурные продажи] ON[Серийный номер].[Код безрецептурной продажи] =[Безрецептурные продажи].[№] " +
                "inner join([Характеристики лекарств] inner join Лекарства ON Лекарства.[Код лекарства]=[Характеристики лекарств].[Код лекарства] inner join ([Форма выпуска] inner join " +
                "[Единицы измерения] ON[Форма выпуска].[Код ед.изм]=[Единицы измерения].[Код ед.изм])ON [Характеристики лекарств].[Код формы] =[Форма выпуска].[Код формы]) ON" +
                "[Характеристики лекарств].[Код характеристики] =[Серийный номер].[Код характеристики] WHERE[Серийный номер].[Код безрецептурной продажи] IS NOT NULL " +
                "GROUP BY[Безрецептурные продажи].Дата, [Серийный номер].[Код безрецептурной продажи]", Connection);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "info");
            int k = ds.Tables[0].Rows.Count;

            SqlDataAdapter adapter2 = new SqlDataAdapter("SELECT [Рецептурные продажи].Дата, SUM([Рецептурные продажи].Цена) as Сумма, STRING_AGG(CONCAT([Рецептурные продажи].Количество, " +
                "'*', [Форма выпуска].Форма, ' ', Лекарства.Наименование, ' ',[Характеристики лекарств].Дозировка, ' ', LEFT([Единицы измерения].Обозначение, " +
                "CHARINDEX(' ',[Единицы измерения].Обозначение) - 1)), ', ') AS Товары, Рецепты.[Номер рецепта] AS Серия, Рецепты.[Номер рецепта] AS Номер FROM[Рецептурные продажи]" +
                " inner join Рецепты ON[Рецептурные продажи].Рецепт = Рецепты.[№] inner join([Характеристики лекарств] inner join Лекарства ON Лекарства.[Код лекарства]=" +
                "[Характеристики лекарств].[Код лекарства]) inner join([Форма выпуска] inner join [Единицы измерения] ON[Форма выпуска].[Код ед.изм]=[Единицы измерения].[Код ед.изм])ON" +
                "[Характеристики лекарств].[Код формы] = [Форма выпуска].[Код формы] ON[Рецептурные продажи].[Код характеристики] =[Характеристики лекарств].[Код характеристики]" +
                "GROUP BY[Рецептурные продажи].Дата, Рецепты.[Номер рецепта], Рецепты.[Номер рецепта]", Connection);
            DataSet ds2 = new DataSet();
            adapter2.Fill(ds2, "info");
            DataSet ds3 = new DataSet();
            ds3 = ds;
            ds3.Merge(ds2);
            dataGridView1.DataSource = ds3.Tables[0];
            Connection.Close();
            int rows = dataGridView1.Rows.Count - 1;
            label1.Text = "Количество продаж:" + rows.ToString();
            kolichestvo(k);
            dataGridView1.Columns[3].Visible=false;
        }
        private void kolichestvo (int n) // рассчитывает количество для безрецептурных продаж
        {
            for (int i = 0; i != n; i++)
            {
                if (dataGridView1[3, i].Value.ToString().IndexOf(',') != -1)
                {
                    string[] words = dataGridView1[3, i].Value.ToString().Split(',');
                    var duplicates = words.GroupBy(x => x)
                        .Where(g => g.Count() > 1)
                        .ToDictionary(y => y.Count(), x => x.Key);
                    string l = String.Join(" ", duplicates);
                    dataGridView1[3, i].Value = l.Replace(",", "").Replace("[", "").Replace("]", "");
                    var singles = words
                        .GroupBy(x => x)
                        .Where(g => !g.Skip(1).Any())
                        .SelectMany(g => g);
                    foreach (var g in singles)
                    {
                        dataGridView1[3, i].Value += ", 1" + g;
                    }
                }
                else dataGridView1[3, i].Value = "1" + dataGridView1[3, i].Value.ToString();
            }
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
