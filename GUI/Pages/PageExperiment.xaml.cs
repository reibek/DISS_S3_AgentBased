using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using GUI.Annotations;
using LiveCharts;
using LiveCharts.Wpf;
using simulation;

namespace GUI.Pages
{
    /// <summary>
    /// Interaction logic for PageExperiment.xaml
    /// </summary>
    public partial class PageExperiment : Page, INotifyPropertyChanged
    {
        private readonly MySimulation _simRef;
        private Thread _simulationThread;
        private readonly MainWindow _mw;
        private SeriesCollection _avgExpQuLengthSeries;
        private SeriesCollection _avgSumQuWaitTimeSeries;
        private SeriesCollection _avgSumUtilSeries;
        private bool _simPaused;
        private int _replicationsCount;

        #region PROPERTIES

        public Func<double, string> XFormatterExp { get; set; }

        public int ReplicationsCount
        {
            get => _replicationsCount;
            set
            {
                _replicationsCount = value;
                OnPropertyChanged(nameof(ReplicationsCount));
            }
        }

        public SeriesCollection AvgExpQuLengthSeries
        {
            get => _avgExpQuLengthSeries;
            set
            {
                _avgExpQuLengthSeries = value;
                OnPropertyChanged(nameof(AvgExpQuLengthSeries));
            }
        }

        public SeriesCollection AvgSumQuWaitTimeSeries
        {
            get => _avgSumQuWaitTimeSeries;
            set
            {
                _avgSumQuWaitTimeSeries = value;
                OnPropertyChanged(nameof(AvgSumQuWaitTimeSeries));
            }
        }

        public SeriesCollection AvgSumUtilSeries
        {
            get => _avgSumUtilSeries;
            set
            {
                _avgSumUtilSeries = value;
                OnPropertyChanged(nameof(AvgSumUtilSeries));
            }
        }

        #endregion

        public PageExperiment(MainWindow mw)
        {
            InitializeComponent();
            _mw = mw;
            _simRef = new MySimulation();
            DataContext = this;

            InitGui();
        }

        private void InitGui()
        {
            ReplicationsCount = 0;

            AvgExpQuLengthSeries = new SeriesCollection
            {
                new LineSeries
                {
                    Values = new ChartValues<double>()
                }
            };

            AvgSumQuWaitTimeSeries = new SeriesCollection
            {
                new LineSeries
                {
                    Values = new ChartValues<double>()
                }
            };

            AvgSumUtilSeries = new SeriesCollection
            {
                new LineSeries
                {
                    Values = new ChartValues<double>()
                }
            };
        }

        private void RunSimulation()
        {
            _simRef.OrderedPatientsNum = _mw.SetOrderedPatients;
            _simRef.ResAdminWorkersCount = _mw.SetAdminWorkersCount;
            _simRef.ResNursesCount = _mw.SetNursesCount;
            _simRef.EnableEarlyArrivals = _mw.SetCheckEarlyArrivals;
            _simRef.EnableLightModel = _mw.SetCheckLightModel;

            _simRef.OnReplicationDidFinish(s =>
            {
                var sim = (MySimulation)s;

                Application.Current.Dispatcher.Invoke(() =>
                {
                    ReplicationsCount = sim.CurrentReplication + 1;
                });
            });

            _simRef.OnSimulationDidFinish(s =>
            {
                var sim = (MySimulation) s;

                Application.Current.Dispatcher.Invoke(() =>
                {
                    AvgExpQuLengthSeries[0].Values.Add(sim.ExaminationQuSize.Mean());
                    AvgSumQuWaitTimeSeries[0].Values.Add(sim.RegistrationQuTime.Mean() 
                                                         + sim.ExaminationQuTime.Mean() 
                                                         + sim.VaccinationQuTime.Mean());
                    AvgSumUtilSeries[0].Values.Add((sim.AdminWorkersUtilization.Mean() 
                                                         + sim.DoctorsUtilization.Mean() 
                                                         + sim.NursesUtilization.Mean()) / 3);
                });
            });

            for (int i = _mw.SetMinDoctors; i <= _mw.SetMaxDoctors; i++)
            {
                _simRef.ResDoctorsCount = i;
                _simRef.Simulate(_mw.SetExpReplicationsNum, double.MaxValue);
            }
        }

        private void ButtonExpStart_Click(object sender, RoutedEventArgs e)
        {
            AvgExpQuLengthSeries[0].Values.Clear();
            AvgSumQuWaitTimeSeries[0].Values.Clear();
            AvgSumUtilSeries[0].Values.Clear();

            _simRef.StopSimulation();
            _simulationThread?.Abort();
            _simulationThread = new Thread(RunSimulation);
            _simulationThread.Start();
            _simPaused = false;
        }

        private void ButtonExpPause_Click(object sender, RoutedEventArgs e)
        {
            if (_simPaused)
            {
                _simRef.ResumeSimulation();
                _simPaused = false;
            }
            else
            {
                _simRef.PauseSimulation();
                _simPaused = true;
            }
        }

        private void ButtonExpStop_Click(object sender, RoutedEventArgs e)
        {
            _simRef.StopSimulation();
            _simulationThread?.Abort();
            _simPaused = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
