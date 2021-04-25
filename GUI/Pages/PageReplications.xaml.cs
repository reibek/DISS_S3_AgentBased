using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace GUI.Pages
{
    /// <summary>
    /// Interaction logic for PageReplications.xaml
    /// </summary>
    public partial class PageReplications : Page
    {
        private readonly MainWindow _dataContext;

        public PageReplications(MainWindow dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            _dataContext = dataContext;
        }

        private void RunSimulation()
        {
        }

        private void ButtonRepStart_Click(object sender, RoutedEventArgs e)
        {
            _dataContext.AvgWaitingRoomCountSeries[0].Values.Clear();

            Thread simulationThread = new Thread(RunSimulation);
            simulationThread.Start();
        }

        private void ButtonRepPause_Click(object sender, RoutedEventArgs e)
        {
            //
        }

        private void ButtonRepStop_Click(object sender, RoutedEventArgs e)
        {
            //
        }
    }
}
