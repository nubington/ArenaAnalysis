using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Graph = System.Windows.Forms.DataVisualization.Charting;

namespace arena_analysis
{
    public partial class Form1 : Form
    {
        Graph.Chart profitChart, dustChart, goldChart;
        const string DEFAULT_PACK_VALUE_STRING = "100", DEFAULT_DUST_VALUE_STRING = "1";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            packValueTextBox.Text = DEFAULT_PACK_VALUE_STRING;
            dustValueTextBox.Text = DEFAULT_DUST_VALUE_STRING;

            Analyzer.GetRecordsFromFile("C:/Users/Alex/Desktop/new arena record.txt");

            doCalculations();

            createGoldGraph();
            createDustGraph();
            createProfitGraph();

            this.WindowState = FormWindowState.Maximized;
        }

        void createGoldGraph()
        {
            // Create new Graph
            goldChart = new Graph.Chart();
            goldChart.Location = new System.Drawing.Point(mainTextBox.Location.X + mainTextBox.Size.Width, 10);
            goldChart.Size = new System.Drawing.Size(Math.Max(0, this.Size.Width - mainTextBox.Size.Width - 10), Math.Max(0, this.Size.Height / 3 - 15));

            // Add a chartarea called "draw", add axes to it and color the area black
            goldChart.ChartAreas.Add("draw");

            goldChart.ChartAreas["draw"].AxisX.Minimum = Analyzer.LowestWins;
            goldChart.ChartAreas["draw"].AxisX.Maximum = Analyzer.HighestWins;
            goldChart.ChartAreas["draw"].AxisX.Interval = 1;
            goldChart.ChartAreas["draw"].AxisX.MajorGrid.LineColor = Color.Black;
            goldChart.ChartAreas["draw"].AxisX.MajorGrid.LineDashStyle = Graph.ChartDashStyle.Solid;
            goldChart.ChartAreas["draw"].AxisX.Title = "Wins";

            goldChart.ChartAreas["draw"].AxisY.Minimum = Analyzer.LowestGold;
            goldChart.ChartAreas["draw"].AxisY.Maximum = Analyzer.HighestGold;
            //goldChart.ChartAreas["draw"].AxisY.Interval = (Analyzer.HighestProfit - Analyzer.LowestProfit) / 20f;
            goldChart.ChartAreas["draw"].AxisY.Interval = 50;
            goldChart.ChartAreas["draw"].AxisY.MajorGrid.LineColor = Color.Black;
            goldChart.ChartAreas["draw"].AxisY.MajorGrid.LineDashStyle = Graph.ChartDashStyle.Solid;
            goldChart.ChartAreas["draw"].AxisY.Title = "Gold";

            goldChart.ChartAreas["draw"].BackColor = Color.Gray;

            // Create a new function series
            goldChart.Series.Add("MyFunc");

            // Set the type to line      
            goldChart.Series["MyFunc"].ChartType = Graph.SeriesChartType.Line;

            // Color the line of the graph light green and give it a thickness of 3
            goldChart.Series["MyFunc"].Color = Color.Gold;
            goldChart.Series["MyFunc"].BorderWidth = 3;

            //float[] dust = Analyzer.DustByWins;
            ArenaRecord[] averageRecords = Analyzer.AverageRecords;

            //This function cannot include zero, and we walk through it in steps of 0.1 to add coordinates to our series
            for (int i = 0; i < 13; i++)
            {
                //if (dust[i] != -999)
                if (averageRecords[i] != null)
                    goldChart.Series["MyFunc"].Points.AddXY(i, averageRecords[i].Gold);
            }

            goldChart.Series["MyFunc"].LegendText = "Gold";

            // Create a new legend called "MyLegend".
            goldChart.Legends.Add("MyLegend");
            goldChart.Legends["MyLegend"].BorderColor = Color.Tomato; // I like tomato juice!

            Controls.Add(this.goldChart);
        }

        void createDustGraph()
        {
            // Create new Graph
            dustChart = new Graph.Chart();
            dustChart.Location = new System.Drawing.Point(mainTextBox.Location.X + mainTextBox.Size.Width, goldChart.Location.Y + goldChart.Size.Height);
            dustChart.Size = new System.Drawing.Size(Math.Max(0, this.Size.Width - mainTextBox.Size.Width - 10), Math.Max(0, this.Size.Height / 3 - 15));

            // Add a chartarea called "draw", add axes to it and color the area black
            dustChart.ChartAreas.Add("draw");

            dustChart.ChartAreas["draw"].AxisX.Minimum = Analyzer.LowestWins;
            dustChart.ChartAreas["draw"].AxisX.Maximum = Analyzer.HighestWins;
            dustChart.ChartAreas["draw"].AxisX.Interval = 1;
            dustChart.ChartAreas["draw"].AxisX.MajorGrid.LineColor = Color.Black;
            dustChart.ChartAreas["draw"].AxisX.MajorGrid.LineDashStyle = Graph.ChartDashStyle.Solid;
            dustChart.ChartAreas["draw"].AxisX.Title = "Wins";

            dustChart.ChartAreas["draw"].AxisY.Minimum = Analyzer.LowestDust;
            dustChart.ChartAreas["draw"].AxisY.Maximum = Analyzer.HighestDust;
            //dustChart.ChartAreas["draw"].AxisY.Interval = (Analyzer.HighestProfit - Analyzer.LowestProfit) / 20f;
            dustChart.ChartAreas["draw"].AxisY.Interval = 25;
            dustChart.ChartAreas["draw"].AxisY.MajorGrid.LineColor = Color.Black;
            dustChart.ChartAreas["draw"].AxisY.MajorGrid.LineDashStyle = Graph.ChartDashStyle.Solid;
            dustChart.ChartAreas["draw"].AxisY.Title = "Dust";

            dustChart.ChartAreas["draw"].BackColor = Color.Gray;

            // Create a new function series
            dustChart.Series.Add("MyFunc");

            // Set the type to line      
            dustChart.Series["MyFunc"].ChartType = Graph.SeriesChartType.Line;

            // Color the line of the graph light green and give it a thickness of 3
            dustChart.Series["MyFunc"].Color = Color.SkyBlue;
            dustChart.Series["MyFunc"].BorderWidth = 3;

            //float[] dust = Analyzer.DustByWins;
            ArenaRecord[] averageRecords = Analyzer.AverageRecords;

            //This function cannot include zero, and we walk through it in steps of 0.1 to add coordinates to our series
            for (int i = 0; i < 13; i++)
            {
                //if (dust[i] != -999)
                if (averageRecords[i] != null)
                    dustChart.Series["MyFunc"].Points.AddXY(i, averageRecords[i].Dust);
            }

            dustChart.Series["MyFunc"].LegendText = "Dust";

            // Create a new legend called "MyLegend".
            dustChart.Legends.Add("MyLegend");
            dustChart.Legends["MyLegend"].BorderColor = Color.Tomato; // I like tomato juice!

            Controls.Add(this.dustChart);
        }

        void createProfitGraph()
        {
            // Create new Graph
            profitChart = new Graph.Chart();
            profitChart.Location = new System.Drawing.Point(mainTextBox.Location.X + mainTextBox.Size.Width, dustChart.Location.Y + dustChart.Size.Height);
            profitChart.Size = new System.Drawing.Size(Math.Max(0, this.Size.Width - mainTextBox.Size.Width - 10), Math.Max(0, this.Size.Height / 3 - 15));

            // Add a chartarea called "draw", add axes to it and color the area black
            profitChart.ChartAreas.Add("draw");

            profitChart.ChartAreas["draw"].AxisX.Minimum = Analyzer.LowestWins;
            profitChart.ChartAreas["draw"].AxisX.Maximum = Analyzer.HighestWins;
            profitChart.ChartAreas["draw"].AxisX.Interval = 1;
            profitChart.ChartAreas["draw"].AxisX.MajorGrid.LineColor = Color.Black;
            profitChart.ChartAreas["draw"].AxisX.MajorGrid.LineDashStyle = Graph.ChartDashStyle.Solid;
            profitChart.ChartAreas["draw"].AxisX.Title = "Wins";

            profitChart.ChartAreas["draw"].AxisY.Minimum = Analyzer.LowestValue;
            profitChart.ChartAreas["draw"].AxisY.Maximum = Analyzer.HighestValue;
            //profitChart.ChartAreas["draw"].AxisY.Interval = (Analyzer.HighestProfit - Analyzer.LowestProfit) / 20f;
            profitChart.ChartAreas["draw"].AxisY.Interval = 50;
            profitChart.ChartAreas["draw"].AxisY.MajorGrid.LineColor = Color.Black;
            profitChart.ChartAreas["draw"].AxisY.MajorGrid.LineDashStyle = Graph.ChartDashStyle.Solid;
            profitChart.ChartAreas["draw"].AxisY.Title = "Value";

            profitChart.ChartAreas["draw"].BackColor = Color.Gray;

            // Create a new function series
            profitChart.Series.Add("MyFunc");

            // Set the type to line      
            profitChart.Series["MyFunc"].ChartType = Graph.SeriesChartType.Line;

            // Color the line of the graph light green and give it a thickness of 3
            profitChart.Series["MyFunc"].Color = Color.Black;
            profitChart.Series["MyFunc"].BorderWidth = 3;

            //float[] profits = Analyzer.ProfitsByWins;
            ArenaRecord[] averageRecords = Analyzer.AverageRecords;

            //This function cannot include zero, and we walk through it in steps of 0.1 to add coordinates to our series
            for (int i = 0; i < 13; i++)
            {
                //if (profits[i] != -999)
                if (averageRecords[i] != null)
                    profitChart.Series["MyFunc"].Points.AddXY(i, averageRecords[i].Value);
            }

            profitChart.Series["MyFunc"].LegendText = "Value";

            // Create a new legend called "MyLegend".
            profitChart.Legends.Add("MyLegend");
            profitChart.Legends["MyLegend"].BorderColor = Color.Tomato; // I like tomato juice!

            Controls.Add(this.profitChart);
        }

        void refreshCharts()
        {
            Controls.Remove(goldChart);
            Controls.Remove(dustChart);
            Controls.Remove(profitChart);
            createGoldGraph();
            createDustGraph();
            createProfitGraph();
        }

        void doCalculations()
        {
            Analyzer.DoAllCalculations();

            Analyzer.PrintResults(mainTextBox);
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            refreshCharts();
        }

        private void packValueTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                float value;

                if (float.TryParse(packValueTextBox.Text, out value))
                {
                    ArenaRecord.PackValue = value;
                    doCalculations();
                    refreshCharts();
                }
                else
                {
                    packValueTextBox.Text = ArenaRecord.PackValue.ToString();
                }
            }
        }

        private void dustValueTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                float value;

                if (float.TryParse(dustValueTextBox.Text, out value))
                {
                    ArenaRecord.DustValue = value;
                    doCalculations();
                    refreshCharts();
                }
                else
                {
                    dustValueTextBox.Text = ArenaRecord.DustValue.ToString();
                }
            }
        }
    }
}
