using System;
using Simulation;
using LiveCharts;

using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.Linq;
using LiveCharts.Configurations;
using LiveCharts.Defaults;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for GraderView.xaml
    /// </summary>
    public partial class GraderView : UserControl
    {
        public ChartValues<int> GraderOutputCount { get; set; }
        public string[] GraderLabels { get; set; }
        public Grader Grader { get; set; }



        public GraderView()
        {
            InitializeComponent();
            GraderOutputCount = new ChartValues<int>() { 0,0,0,0,0,0,0,0,0,0 };            
            Simulator simulator = new Simulator();
            simulator.Simulate();
            Grader = simulator.Grader;
            Grader.UpdateChartView += OnUpdateChartView;
            GraderLabels = Grader.SortedOutputConveyors.Select(x => x.Item1).ToArray();
            DataContext = this;
            
        }

        //Event handler
        void OnUpdateChartView(object sender, EventArgs e)
        {
            //GraderOutputCount = new ChartValues<int>(Grader.SortedOutputConveyors.Select(x => x.Item2.Count)); FOUT
            Trace.WriteLine("OnUpdateChartView got called");
            GraderOutputCount.Clear();
            foreach(var num in Grader.SortedOutputConveyors.Select(x => x.Item2.Count))
            {
                GraderOutputCount.Add(num);
            }            
            
            for (int i = 0; i < GraderLabels.Length; i++)
            {
                Trace.WriteLine($"label:{GraderLabels[i]} amount:{GraderOutputCount[i]}");
            }            
        }
    }
}
