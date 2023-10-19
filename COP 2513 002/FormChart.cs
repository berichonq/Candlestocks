/*
 * Quinn Berichon
 * FormChart Class
 * 4/18/2023
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using COP_2513_002;

namespace COP_2513_002
{
    public partial class FormChart : Form
    {
        private String pattern;
        private List<Candlestick> candlesticks;
        private List<int> indices;
        private readonly string dataName;
        private List<Recognizer> recognizers = new List<Recognizer>(15);


        /// <summary>
        /// Constructor for the FormChart Class, requires a List<Candlestick> argument to be passed
        /// </summary>
        /// <param name="candlesticks"></param>
        public FormChart(List<Candlestick> candlesticks, String dn)
        {
            dataName = dn;
            InitializeComponent();
            this.candlesticks = candlesticks;
            populateComboBoxPattern();
        }


        /// <summary>
        /// Populates the combobox with the pattern names of all the derived recognizer classes called
        /// </summary>
        private void populateComboBoxPattern()
        {
            Recognizer r = null;
            recognizers.Add(r);

            r = new Recognizer_Doji();
            recognizers.Add(r);

            r = new Recognizer_LongLeggedDoji();
            recognizers.Add(r);

            r = new Recognizer_DragonflyDoji();
            recognizers.Add(r);

            r = new Recognizer_GravestoneDoji();
            recognizers.Add(r);

            r = new Recognizer_WhiteMarubozu();
            recognizers.Add(r);

            r = new Recognizer_BlackMarubozu();
            recognizers.Add(r);

            r = new Recognizer_BullishHammer();
            recognizers.Add(r);

            r = new Recognizer_BearishHammer();
            recognizers.Add(r);

            r = new Recognizer_BullishInvertedHammer();
            recognizers.Add(r);

            r = new Recognizer_BearishInvertedHammer();
            recognizers.Add(r);

            r = new Recognizer_BullishEngulfing();
            recognizers.Add(r);

            r = new Recognizer_BearishEngulfing();
            recognizers.Add(r);

            comboBoxPatterns.Items.Add("None");
            for (int i = 1; i < recognizers.Count; i++)
            {
                comboBoxPatterns.Items.Add(recognizers[i].patternName);
            }
            comboBoxPatterns.SelectedIndex = 0;
        }


        /// <summary>
        /// Clears any prior Legend or Annotation items and annotates the chart control object according to the pattern selected by the user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadPatternDetection(object sender, EventArgs e)
        {
            //FormChartUpdateText();
            chartStockHistory.Annotations.Clear();
            chartStockHistory.Legends[0].CustomItems.Clear();
            if (comboBoxPatterns.SelectedIndex == 0) { }
            else
            {
                int i = comboBoxPatterns.SelectedIndex;

                chartStockHistory.Legends[0].CustomItems.Add(Color.LightPink, recognizers[i].patternName);
                indices = recognizers[i].Recognize(candlesticks);
                AnnotateChart(chartStockHistory, indices, Color.LightPink);
            }
        }


        /// <summary>
        /// Adds RectangleAnnotations for any candlestick indexes in the indices paramter to the chart control object passed to it
        /// </summary>
        /// <param name="chart"></param>
        /// <param name="indices"></param>
        /// <param name="subpattern"></param>
        private void AnnotateChart(Chart chart, List<int> indices, Color color)
        {
            string txt = "|\n|\n|";

            foreach (int index in indices)
            {
                RectangleAnnotation rect = new RectangleAnnotation();
                rect.Text = txt;
                rect.ForeColor = color;
                rect.LineColor = color;
                rect.BackColor = Color.FromArgb(75, color);
                rect.Font = new Font("Arial", 7, FontStyle.Italic);
                rect.AxisX = chart.ChartAreas["ChartArea1"].AxisX;
                rect.AxisY = chart.ChartAreas["ChartArea1"].AxisY;
                rect.AnchorDataPoint = chart.Series[0].Points[index];
                rect.AnchorOffsetY = 2.5;
                chart.Annotations.Add(rect);
            }
        }


        /// <summary>
        /// Updates the text of the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void FormChartUpdateText()
        {
            if (pattern == "None")
            {
                Text = dataName.Substring(0, dataName.IndexOf('-'));
            }
            else
            {
                Text = dataName.Substring(0, dataName.IndexOf('-')) + " | Pattern: " + pattern;
            }
        }


        /// <summary>
        /// Updates the pattern property with the text of the ComboBox control the event was raised from and then calls the LoadPatternDetection() method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            pattern = ((ComboBox)sender).Text;
            FormChartUpdateText();
            LoadPatternDetection(sender, e);
        }
    }
}