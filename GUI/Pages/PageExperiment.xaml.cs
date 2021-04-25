using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace GUI.Pages
{
    /// <summary>
    /// Interaction logic for PageExperiment.xaml
    /// </summary>
    public partial class PageExperiment : Page
    {
        private readonly MainWindow _dataContext;
        private bool _stopExperiment;

        public PageExperiment(MainWindow dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            _dataContext = dataContext;
        }

        private void RunSimulation()
        {
            
        }

        private void ButtonExpStart_Click(object sender, RoutedEventArgs e)
        {
            _dataContext.AvgExpQuLengthSeries[0].Values.Clear();
            _dataContext.AvgExpQuWaitTimeSeries[0].Values.Clear();

            Thread simulationThread = new Thread(RunSimulation);
            simulationThread.Start();
        }

        private void ButtonExpPause_Click(object sender, RoutedEventArgs e)
        {
            //
        }

        private void ButtonExpStop_Click(object sender, RoutedEventArgs e)
        {
            //
        }
    }
}
