/*
 * Quinn Berichon
 * FormStockLoader Class
 * 4/18/2023
 */

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace COP_2513_002
{
    public partial class FormStockLoader : Form
    {
        private DateTime startDate;
        private DateTime endDate;
        private string period;
        private string path;
        private const String folder = "Stock Data";
        private FormChart candleChart;
        private double maxY;
        private double minY;
        private double padding;
       

        /// <summary>
        /// Default constructor for the FormStockLoader class
        /// </summary>
        public FormStockLoader()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Updates the text of the form to "Stock Loader" when the form is loaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            Text = "Stock Loader";
        }


        /// <summary>
        /// Loads a new instance of the FormChart class with the current state of the input controls and invokes the StockDataReader class
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadDisplay(object sender, EventArgs e)
        {
            //Update attributes
            startDate = dateTimePickerStartDate.Value.Date;
            endDate = dateTimePickerEndDate.Value.Date;
            path = folder + @"\" + comboBoxTicker.Text + ".csv";

            //Read stock data
            StockDataReader reader = new StockDataReader();
            List<Candlestick> candlesticks = reader.ReadStockData(path, startDate, endDate);

            //If data was found, display in a new candleChart, else display the "Error" dialog
            if (candlesticks.Count == 0)
            {
                FormError error = new FormError();
                error.ShowDialog();
            }
            else
            {
                //Instantiate a new FormChart
                candleChart = new FormChart(candlesticks, comboBoxTicker.Text);

                //Create a data table for the candleChart DataSource
                DataTable stockHistory = CreateDataTable(candlesticks);

                double maxY = candlesticks.Max(cs => cs.High);
                double minY = candlesticks.Min(cs => cs.Low);
                double padding = 0.10 * (maxY - minY);
                ConfigureChart(stockHistory, maxY, minY, padding);

                candleChart.Show();
            }
        }


        /// <summary>
        /// Creates a stock data table with columns (Date, Open, High, Low, Close, Volume) from a list of Candlestick objects
        /// </summary>
        /// <param name="candles"></param>
        private DataTable CreateDataTable(List<Candlestick> candles)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Date");
            dt.Columns.Add("Open");
            dt.Columns.Add("High");
            dt.Columns.Add("Low");
            dt.Columns.Add("Close");
            dt.Columns.Add("Volume");

            foreach (Candlestick c in candles)
            {
                String[] line = new string[6];
                line[0] = c.Date.ToString();
                line[1] = c.Open.ToString();
                line[2] = c.High.ToString();
                line[3] = c.Low.ToString();
                line[4] = c.Close.ToString();
                line[5] = c.Volume.ToString();

                dt.Rows.Add(line);
            }

            return dt;
        }


        /// <summary>
        /// Closes the form and terminates program execution when clicked by the user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        /// <summary>
        /// Event handler for all of the radio buttons, updates the period attribute as necessary
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aRadioButtonCheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            if (rb.Checked == true)
            {
                period = rb.AccessibleName;
                string[] fullFileNames = Directory.GetFiles("Stock Data", "*-" + period + ".csv");

                comboBoxTicker.Items.Clear();
                foreach (string fullName in fullFileNames)
                {
                    comboBoxTicker.Items.Add(Path.GetFileNameWithoutExtension(fullName));
                }
            }
        }


        /// <summary>
        /// Updates the value of the startDate attribute whenever the value of dateTimePickerStartDate changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dateTimePickerStartDate_ValueChanged(object sender, EventArgs e)
        {
            startDate = dateTimePickerStartDate.Value.Date;
        }


        /// <summary>
        /// Updates the value of the endDate attribute whenever the value of dateTimePickerEndDate changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dateTimePickerEndDate_ValueChanged(object sender, EventArgs e)
        {
               endDate = dateTimePickerEndDate.Value.Date;
        }


        /// <summary>
        /// Sets up the chart control of the FormChart instance and binds it to the datatable
        /// </summary>
        /// <param name="dt"></param>
        private void ConfigureChart(DataTable dt, double maxY, double minY, double padding)
        {
            candleChart.chartStockHistory.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineWidth = 0;
            candleChart.chartStockHistory.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineWidth = 0;
            candleChart.chartStockHistory.ChartAreas["ChartArea1"].AxisX.Title = "Date";
            candleChart.chartStockHistory.ChartAreas["ChartArea1"].AxisY.Title = "Price ($)";
            candleChart.chartStockHistory.ChartAreas["ChartArea1"].AxisY.Maximum = maxY + padding;
            candleChart.chartStockHistory.ChartAreas["ChartArea1"].AxisY.Minimum = Math.Max(minY - padding, 0); //Doesn't allow a negative y minimum
            candleChart.chartStockHistory.Series["Candles"].XValueMember = "Date";
            candleChart.chartStockHistory.Series["Candles"].YValueMembers = "High,Low,Open,Close";
            candleChart.chartStockHistory.Series["Candles"].XValueType = ChartValueType.Date;
            candleChart.chartStockHistory.Series["Candles"].CustomProperties = "PriceDownColor=Red,PriceUpColor=Green";
            candleChart.chartStockHistory.Series["Candles"]["ShowOpenClose"] = "Both";
            candleChart.chartStockHistory.DataManipulator.IsStartFromFirst = true;
            candleChart.chartStockHistory.DataSource = dt;
            candleChart.chartStockHistory.DataBind();
            candleChart.chartStockHistory.Series[0].Name = comboBoxTicker.Text;
            candleChart.chartStockHistory.ChartAreas["ChartArea1"].AxisX.Interval = 0;
            candleChart.chartStockHistory.ChartAreas["ChartArea1"].AxisX.IntervalType = DateTimeIntervalType.Days;
            candleChart.chartStockHistory.ChartAreas["ChartArea1"].AxisX.LabelStyle.Format = "dd/MM/yyyy";
        }
    }
}