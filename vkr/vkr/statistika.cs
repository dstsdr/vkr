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
using System.Windows.Forms.DataVisualization.Charting;

namespace vkr
{
    public partial class statistika : Form
    {
        public statistika()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0) doxod();
            if (comboBox1.SelectedIndex == 1) rashod();
            if (comboBox1.SelectedIndex == 3) prodazh();
            if (comboBox1.SelectedIndex == 2) postav();
        }
        SqlConnection Connection = new SqlConnection(@"Data Source=LAPTOP-862V88EF\SQLEXPRESS;Initial Catalog=vkr;Integrated Security=True");
        private void doxod()
        {
            chart1.Series.Clear();
            if (comboBox2.SelectedIndex == 0)
            {
                var months = DateTime.Today;
                Connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(" SELECT R.Месяц, SUM(R.Сумма) as Сумма " +
                    "FROM(SELECT MONTH([Рецептурные продажи].Дата) as Месяц, SUM([Рецептурные продажи].Цена) AS Сумма " +
                    "FROM[Рецептурные продажи] " +
                    "                    WHERE[Рецептурные продажи].Дата > '16.06.2021' " +
                    "                    GROUP BY MONTH([Рецептурные продажи].Дата)  " +
                    "                    UNION " +
                    "                    SELECT MONTH([Безрецептурные продажи].Дата)as Месяц, SUM([Безрецептурные продажи].Сумма) AS Сумма " +
                    "                    from[Безрецептурные продажи] WHERE[Безрецептурные продажи].Дата > '" + months + "' " +
                    "                    GROUP BY MONTH([Безрецептурные продажи].Дата)) AS R " +
                    "                    GROUP BY R.Месяц", Connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "info");
                dataGridView1.DataSource = ds.Tables[0];
                Connection.Close();
            }
            if (comboBox2.SelectedIndex == 1)
            {
                var months = DateTime.Today;
                months = months.AddYears(-1);
                Connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(" SELECT R.Месяц, SUM(R.Сумма) as Сумма " +
                    "FROM(SELECT MONTH([Рецептурные продажи].Дата) as Месяц, SUM([Рецептурные продажи].Цена) AS Сумма " +
                    "FROM[Рецептурные продажи] " +
                    "                    WHERE[Рецептурные продажи].Дата > '16.06.2021' " +
                    "                    GROUP BY MONTH([Рецептурные продажи].Дата)  " +
                    "                    UNION " +
                    "                    SELECT MONTH([Безрецептурные продажи].Дата)as Месяц, SUM([Безрецептурные продажи].Сумма) AS Сумма " +
                    "                    from[Безрецептурные продажи] WHERE[Безрецептурные продажи].Дата > '" + months + "' " +
                    "                    GROUP BY MONTH([Безрецептурные продажи].Дата)) AS R " +
                    "                    GROUP BY R.Месяц", Connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "info");
                dataGridView1.DataSource = ds.Tables[0];
                Connection.Close();
            }
            if (comboBox2.SelectedIndex == 2)
            {
                var months = DateTime.MinValue;
                Connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(" SELECT R.Месяц, SUM(R.Сумма) as Сумма " +
                    "FROM(SELECT MONTH([Рецептурные продажи].Дата) as Месяц, SUM([Рецептурные продажи].Цена) AS Сумма " +
                    "FROM[Рецептурные продажи] " +
                    "                    WHERE[Рецептурные продажи].Дата > '16.06.2021' " +
                    "                    GROUP BY MONTH([Рецептурные продажи].Дата)  " +
                    "                    UNION " +
                    "                    SELECT MONTH([Безрецептурные продажи].Дата)as Месяц, SUM([Безрецептурные продажи].Сумма) AS Сумма " +
                    "                    from[Безрецептурные продажи] WHERE[Безрецептурные продажи].Дата > '" + months + "' " +
                    "                    GROUP BY MONTH([Безрецептурные продажи].Дата)) AS R " +
                    "                    GROUP BY R.Месяц", Connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "info");
                dataGridView1.DataSource = ds.Tables[0];
                Connection.Close();
            }
            chart1.ChartAreas[0].AxisX.Maximum = Double.NaN;
            chart1.ChartAreas[0].AxisY.Maximum = Double.NaN;
            chart1.Series.Add("Сумма");
            switch (comboBox3.SelectedIndex)
            {
                case 0: chart1.Series["Сумма"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie; break;
                case 1: chart1.Series["Сумма"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column; break;
                case 3: chart1.Series["Сумма"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline; break;
            }
            int rows = dataGridView1.Rows.Count - 1;
            for (int i = 0; i != rows; i++)
            {
                chart1.Series["Сумма"].Points.AddXY(dataGridView1.Rows[i].Cells[0].Value.ToString(), dataGridView1.Rows[i].Cells[1].Value);
                chart1.Series["Сумма"].Points[i].Label = dataGridView1.Rows[i].Cells[1].Value.ToString();
                chart1.ChartAreas[0].AxisX.Maximum = Double.NaN;
                dataGridView1.Rows[i].Cells[1].Value = Math.Round(Convert.ToDouble(dataGridView1.Rows[i].Cells[1].Value), 2);

            }
            Axis ax = new Axis();
            ax.Title = "Месяц";
            chart1.ChartAreas[0].AxisX = ax;
            Axis ay = new Axis();
            ay.Title = "Сумма";
            chart1.ChartAreas[0].AxisY = ay;
        }
        private void rashod()
        {
            chart1.Series.Clear();
            if (comboBox2.SelectedIndex == 0)
            {
                var months = DateTime.Today;
                Connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(" SELECT MONTH(Договор.[Дата заключения]) as Месяц, SUM(Договор.Сумма) as Сумма " +
                    "from Договор " +
                    "where [Дата заключения] > '"+months+"' " +
                    "GROUP" +
                    " BY MONTH(Договор.[Дата заключения]) ", Connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "info");
                dataGridView1.DataSource = ds.Tables[0];
                Connection.Close();
            }
            if (comboBox2.SelectedIndex == 1)
            {
                var months = DateTime.Today;
                months = months.AddYears(-1);
                Connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(" SELECT MONTH(Договор.[Дата заключения]) as Месяц, SUM(Договор.Сумма) as Сумма " +
                    "from Договор " +
                    "where [Дата заключения] > '" + months + "' " +
                    "GROUP" +
                    " BY MONTH(Договор.[Дата заключения]) ", Connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "info");
                dataGridView1.DataSource = ds.Tables[0];
                Connection.Close();
            }
            if (comboBox2.SelectedIndex == 2)
            {
                var months = DateTime.MinValue;
                Connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(" SELECT MONTH(Договор.[Дата заключения]) AS Месяц, SUM(Договор.Сумма) as Сумма " +
                    "from Договор " +
                    "where [Дата заключения] > '" + months + "' " +
                    "GROUP" +
                    " BY MONTH(Договор.[Дата заключения]) ", Connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "info");
                dataGridView1.DataSource = ds.Tables[0];
                Connection.Close();
            }
            chart1.ChartAreas[0].AxisX.Maximum = Double.NaN;
            chart1.ChartAreas[0].AxisY.Maximum = Double.NaN;
            chart1.Series.Add("Сумма");
            switch (comboBox3.SelectedIndex)
            {
                case 0: chart1.Series["Сумма"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie; break;
                case 1: chart1.Series["Сумма"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column; break;
                case 3: chart1.Series["Сумма"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline; break;
            }
            int rows = dataGridView1.Rows.Count - 1;
            for (int i = 0; i != rows; i++)
            {
                chart1.Series["Сумма"].Points.AddXY(dataGridView1.Rows[i].Cells[0].Value.ToString(), dataGridView1.Rows[i].Cells[1].Value);
                chart1.Series["Сумма"].Points[i].Label = dataGridView1.Rows[i].Cells[1].Value.ToString();
                chart1.ChartAreas[0].AxisX.Maximum = Double.NaN;
                dataGridView1.Rows[i].Cells[1].Value = Math.Round(Convert.ToDouble(dataGridView1.Rows[i].Cells[1].Value), 2);
            }
            Axis ax = new Axis();
            ax.Title = "Месяц";
            chart1.ChartAreas[0].AxisX = ax;
            Axis ay = new Axis();
            ay.Title = "Сумма";
            chart1.ChartAreas[0].AxisY = ay;
        }
        private void postav()
        {
            chart1.Series.Clear();
            if (comboBox2.SelectedIndex == 0)
            {
                var months = DateTime.Today;
                Connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT r.Дата as Месяц, SUM(r.[Кол-во]) as [Кол-во]" + 
                    "FROM(SELECT MONTH(DATEADD(d, Договор.[Срок первой поставки], Договор.[Дата заключения]))" + " as Дата, SUM(Договор.Количество * ((Договор.[Процент первой поставки]) / 100)) as [Кол-во] " +
                    "FROM Договор " +
                    "where DATEADD(d, Договор.[Срок первой поставки], " + "Договор.[Дата заключения]) > '" + months + "' " +
                    "GROUP BY MONTH(DATEADD(d, Договор.[Срок первой поставки], Договор.[Дата заключения])) " +
                    "UNION " +
                    "SELECT " +
                    "MONTH(DATEADD(d, Договор.[Срок второй поставки], Договор.[Дата заключения])) as Дата, SUM(Договор.Количество * ((Договор.[Процент второй поставки]) / 100)) " +
                    "as [Кол-во] " +
                    "FROM Договор where DATEADD(d, Договор.[Срок второй поставки], Договор.[Дата заключения]) > '" + months + "' " +
                    "GROUP BY MONTH(DATEADD(d, Договор.[Срок второй поставки], " +  "Договор.[Дата заключения]))) as R " +
                    "GROUP BY r.Дата", Connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "info");
                dataGridView1.DataSource = ds.Tables[0];
                Connection.Close();
            }
            if (comboBox2.SelectedIndex == 1)
            {
                var months = DateTime.Today;
                months = months.AddYears(-1);
                Connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT r.Дата as Месяц, SUM(r.[Кол-во]) as [Кол-во] " +
                    "FROM(SELECT MONTH(DATEADD(d, Договор.[Срок первой поставки], Договор.[Дата заключения]))" + " as Дата, SUM(Договор.Количество * ((Договор.[Процент первой поставки]) / 100)) as [Кол-во] " +
                    "FROM Договор " +
                    "where DATEADD(d, Договор.[Срок первой поставки], " + "Договор.[Дата заключения]) > '" + months + "' " +
                    "GROUP BY MONTH(DATEADD(d, Договор.[Срок первой поставки], Договор.[Дата заключения])) " +
                    "UNION " +
                    "SELECT " +
                    "MONTH(DATEADD(d, Договор.[Срок второй поставки], Договор.[Дата заключения])) as Дата, SUM(Договор.Количество * ((Договор.[Процент второй поставки]) / 100)) " +
                    "as [Кол-во] " +
                    "FROM Договор where DATEADD(d, Договор.[Срок второй поставки], Договор.[Дата заключения]) > '" + months + "' " +
                    "GROUP BY MONTH(DATEADD(d, Договор.[Срок второй поставки], " + "Договор.[Дата заключения]))) as R " +
                    "GROUP BY r.Дата", Connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "info");
                dataGridView1.DataSource = ds.Tables[0];
                Connection.Close();
            }
            if (comboBox2.SelectedIndex == 2)
            {
                var months = DateTime.MinValue;
                Connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT r.Дата as Месяц, SUM(r.[Кол-во]) as [Кол-во] " +
                    "FROM(SELECT MONTH(DATEADD(d, Договор.[Срок первой поставки], Договор.[Дата заключения]))" + " as Дата, SUM(Договор.Количество * ((Договор.[Процент первой поставки]) / 100)) as [Кол-во] " +
                    "FROM Договор " +
                    "where DATEADD(d, Договор.[Срок первой поставки], " + "Договор.[Дата заключения]) > '" + months + "' " +
                    "GROUP BY MONTH(DATEADD(d, Договор.[Срок первой поставки], Договор.[Дата заключения])) " +
                    "UNION " +
                    "SELECT " +
                    "MONTH(DATEADD(d, Договор.[Срок второй поставки], Договор.[Дата заключения])) as Дата, SUM(Договор.Количество * ((Договор.[Процент второй поставки]) / 100)) " +
                    "as [Кол-во] " +
                    "FROM Договор where DATEADD(d, Договор.[Срок второй поставки], Договор.[Дата заключения]) > '" + months + "' " +
                    "GROUP BY MONTH(DATEADD(d, Договор.[Срок второй поставки], " + "Договор.[Дата заключения]))) as R " +
                    "GROUP BY r.Дата", Connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "info");
                dataGridView1.DataSource = ds.Tables[0];
                Connection.Close();
            }
            chart1.ChartAreas[0].AxisX.Maximum = Double.NaN;
            chart1.ChartAreas[0].AxisY.Maximum = Double.NaN;
            chart1.Series.Add("Кол-во");
            switch (comboBox3.SelectedIndex)
            {
                case 0: chart1.Series["Кол-во"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie; break;
                case 1: chart1.Series["Кол-во"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column; break;
                case 3: chart1.Series["Кол-во"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline; break;
            }
            int rows = dataGridView1.Rows.Count - 1;
            for (int i = 0; i != rows; i++)
            {
                chart1.Series["Кол-во"].Points.AddXY(dataGridView1.Rows[i].Cells[0].Value.ToString(), dataGridView1.Rows[i].Cells[1].Value);
                chart1.Series["Кол-во"].Points[i].Label = dataGridView1.Rows[i].Cells[1].Value.ToString();
                chart1.ChartAreas[0].AxisX.Maximum = Double.NaN;
                dataGridView1.Rows[i].Cells[1].Value = Math.Round(Convert.ToDouble(dataGridView1.Rows[i].Cells[1].Value), 2);
            }
            Axis ax = new Axis();
            ax.Title = "Месяц";
            chart1.ChartAreas[0].AxisX = ax;
            Axis ay = new Axis();
            ay.Title = "Количество";
            chart1.ChartAreas[0].AxisY = ay;
        }
        private void prodazh()
        {
            chart1.Series.Clear();
            if (comboBox2.SelectedIndex == 0)
            {
                var months = DateTime.Today;
                Connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("Select MONTH([Безрецептурные продажи].Дата) AS Месяц, SUM([Безрецептурные продажи].Количество) AS Количество " +
                    "FROM[Безрецептурные продажи] " +
                    "inner join([Характеристики лекарств] " +
                    "inner join Лекарства ON Лекарства.[Код лекарства] =[Характеристики лекарств].[Код лекарства])" +
                    "ON[Безрецептурные продажи].[Код характеристики] =[Характеристики лекарств].[Код характеристики] " +
                    "WHERE[Безрецептурные продажи].Дата > '"+months+"' " +
                    "Group by " +   "MONTH([Безрецептурные продажи].Дата), Лекарства.Наименование " +
                    "UNION " +
                    "Select MONTH([Рецептурные продажи].Дата) AS Месяц, SUM([Рецептурные продажи].Количество) " + "AS Количество " +
                    "FROM[Рецептурные продажи] " +
                    "inner join([Характеристики лекарств] " +
                    "inner join Лекарства ON Лекарства.[Код лекарства]=" +  "[Характеристики лекарств].[Код лекарства])" +
                    "ON[Рецептурные продажи].[Код характеристики] =[Характеристики лекарств].[Код характеристики] " +
                    "WHERE" + "[Рецептурные продажи].Дата > '"+months+"' " +
                    "Group by MONTH([Рецептурные продажи].Дата), Лекарства.Наименование", Connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "info");
                dataGridView1.DataSource = ds.Tables[0];
                Connection.Close();
            }
            if (comboBox2.SelectedIndex == 1)
            {
                var months = DateTime.Today;
                months = months.AddYears(-1);
                Connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("Select MONTH([Безрецептурные продажи].Дата) AS Месяц, SUM([Безрецептурные продажи].Количество) AS Количество " +
                    "FROM[Безрецептурные продажи] " +
                    "inner join([Характеристики лекарств] " +
                    "inner join Лекарства ON Лекарства.[Код лекарства] =[Характеристики лекарств].[Код лекарства])" +
                    "ON[Безрецептурные продажи].[Код характеристики] =[Характеристики лекарств].[Код характеристики] " +
                    "WHERE[Безрецептурные продажи].Дата > '" + months + "' " +
                    "Group by " + "MONTH([Безрецептурные продажи].Дата), Лекарства.Наименование " +
                    "UNION " +
                    "Select MONTH([Рецептурные продажи].Дата) AS Месяц, SUM([Рецептурные продажи].Количество) " + "AS Количество " +
                    "FROM[Рецептурные продажи] " +
                    "inner join([Характеристики лекарств] " +
                    "inner join Лекарства ON Лекарства.[Код лекарства]=" + "[Характеристики лекарств].[Код лекарства])" +
                    "ON[Рецептурные продажи].[Код характеристики] =[Характеристики лекарств].[Код характеристики] " +
                    "WHERE" + "[Рецептурные продажи].Дата > '" + months + "' " +
                    "Group by MONTH([Рецептурные продажи].Дата), Лекарства.Наименование", Connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "info");
                dataGridView1.DataSource = ds.Tables[0];
                Connection.Close();
            }
            if (comboBox2.SelectedIndex == 2)
            {
                var months = DateTime.MinValue;
                Connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("Select MONTH([Безрецептурные продажи].Дата) AS Месяц, SUM([Безрецептурные продажи].Количество) AS Количество " +
                    "FROM[Безрецептурные продажи] " +
                    "inner join([Характеристики лекарств] " +
                    "inner join Лекарства ON Лекарства.[Код лекарства] =[Характеристики лекарств].[Код лекарства])" +
                    "ON[Безрецептурные продажи].[Код характеристики] =[Характеристики лекарств].[Код характеристики] " +
                    "WHERE[Безрецептурные продажи].Дата > '" + months + "' " +
                    "Group by " + "MONTH([Безрецептурные продажи].Дата), Лекарства.Наименование " +
                    "UNION " +
                    "Select MONTH([Рецептурные продажи].Дата) AS Месяц, SUM([Рецептурные продажи].Количество) " + "AS Количество " +
                    "FROM[Рецептурные продажи] " +
                    "inner join([Характеристики лекарств] " +
                    "inner join Лекарства ON Лекарства.[Код лекарства]=" + "[Характеристики лекарств].[Код лекарства])" +
                    "ON[Рецептурные продажи].[Код характеристики] =[Характеристики лекарств].[Код характеристики] " +
                    "WHERE" + "[Рецептурные продажи].Дата > '" + months + "' " +
                    "Group by MONTH([Рецептурные продажи].Дата), Лекарства.Наименование", Connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "info");
                dataGridView1.DataSource = ds.Tables[0];
                Connection.Close();
            }
            chart1.ChartAreas[0].AxisX.Maximum = Double.NaN;
            chart1.ChartAreas[0].AxisY.Maximum = Double.NaN;
            chart1.Series.Add("Кол-во");
            switch (comboBox3.SelectedIndex)
            {
                case 0: chart1.Series["Кол-во"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie; break;
                case 1: chart1.Series["Кол-во"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column; break;
                case 3: chart1.Series["Кол-во"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline; break;
            }
            int rows = dataGridView1.Rows.Count - 1;
            for (int i = 0; i != rows; i++)
            {
                chart1.Series["Кол-во"].Points.AddXY(dataGridView1.Rows[i].Cells[0].Value.ToString(), dataGridView1.Rows[i].Cells[1].Value);
                chart1.Series["Кол-во"].Points[i].Label = dataGridView1.Rows[i].Cells[1].Value.ToString();
                chart1.ChartAreas[0].AxisX.Maximum = Double.NaN;
                dataGridView1.Rows[i].Cells[1].Value = Math.Round(Convert.ToDouble(dataGridView1.Rows[i].Cells[1].Value), 2);
            }
            Axis ax = new Axis();
            ax.Title = "Месяц";
            chart1.ChartAreas[0].AxisX = ax;
            Axis ay = new Axis();
            ay.Title = "Количество";
            chart1.ChartAreas[0].AxisY = ay;
        }

        private void statistika_Load(object sender, EventArgs e)
        {

        }
    }
}
