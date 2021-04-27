using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GUI.Annotations;
using GUI.Pages;
using LiveCharts;
using LiveCharts.Wpf;

namespace GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        #region INotifyPropertyChanged_Implementation

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        private bool _windowIsMaximized;
        private readonly Page _pageSim;
        private readonly Page _pageRep;
        private readonly Page _pageExp;
        private readonly Page _pageSet;
        private TimeSpan _simulationTimeSpan;
        private int _replicationsNum;
        private int _simQuRegistrationCount;
        private int _simAdminWorkersBusyCount;
        private int _simQuExaminationCount;
        private int _simDoctorsBusyCount;
        private int _simQuVaccinationCount;
        private int _simNursesBusyCount;
        private int _simQuWaitingRoomCount;
        private int _simAdminWorkersCount;
        private int _simDoctorsCount;
        private int _simNursesCount;
        private int _simOrderedPatientsCount;
        private int _simCanceledPatientsCount;
        private int _simVaccinatedPatientsCount;
        private double _simStatQuExamination;
        private double _simStatQuVaccination;
        private double _simStatQuRegistration;
        private double _simStatQuWaitingRoom;
        private double _simStatUtilAdminWorkers;
        private double _simStatUtilDoctors;
        private double _simStatUtilNurses;
        //private ObservableCollection<AdminWorker> _adminWorkers;
        //private ObservableCollection<Doctor> _doctors;
        //private ObservableCollection<Nurse> _nurses;
        private double _simQuRegistrationTime;
        private double _simQuExaminationTime;
        private double _simQuVaccinationTime;
        public int[] SpeedValues;
        private int _setAdminWorkersCount;
        private int _setDoctorsCount;
        private int _setNursesCount;
        private int _setOrderedPatients;
        private bool _setCheckContSim;
        private int _repCount;
        private double _repQuRegistrationSize;
        private double _repQuExaminationSize;
        private double _repQuVaccinationSize;
        private double _repQuRegistrationTime;
        private double _repQuExaminationTime;
        private double _repQuVaccinationTime;
        private double _repQuWaitingRoomSize;
        private int _setChartVisibleValuesNum;
        private double _repQuWaitingRoomSizeCiLower;
        private double _repQuWaitingRoomSizeCiUpper;
        private SeriesCollection _avgWaitingRoomCountSeries;
        private int _setNthObservation;
        private double _repAdminWorkersUtil;
        private double _repDoctorsUtil;
        private double _repNursesUtil;
        private int _setExpReplicationsNum;
        private int _setMinDoctors;
        private int _setMaxDoctors;
        private SeriesCollection _avgExpQuLengthSeries;
        private SeriesCollection _avgExpQuWaitTimeSeries;

        #region Simulation Properties

        public TimeSpan SimulationTimeSpan
        {
            get => _simulationTimeSpan;
            set
            {
                _simulationTimeSpan = value;
                OnPropertyChanged(nameof(SimulationTimeSpan));
            }
        }

        public int SimOrderedPatientsCount
        {
            get => _simOrderedPatientsCount;
            set
            {
                _simOrderedPatientsCount = value;
                OnPropertyChanged(nameof(SimOrderedPatientsCount));
            }
        }

        public int SimCanceledPatientsCount
        {
            get => _simCanceledPatientsCount;
            set
            {
                _simCanceledPatientsCount = value;
                OnPropertyChanged(nameof(SimCanceledPatientsCount));
            }
        }

        public int SimVaccinatedPatientsCount
        {
            get => _simVaccinatedPatientsCount;
            set
            {
                _simVaccinatedPatientsCount = value;
                OnPropertyChanged(nameof(SimVaccinatedPatientsCount));
            }
        }

        //public ObservableCollection<AdminWorker> AdminWorkers
        //{
        //    get => _adminWorkers;
        //    set
        //    {
        //        _adminWorkers = value;
        //        OnPropertyChanged(nameof(AdminWorkers));
        //    }
        //}

        //public ObservableCollection<Doctor> Doctors
        //{
        //    get => _doctors;
        //    set
        //    {
        //        _doctors = value;
        //        OnPropertyChanged(nameof(Doctors));
        //    }
        //}

        //public ObservableCollection<Nurse> Nurses
        //{
        //    get => _nurses;
        //    set
        //    {
        //        _nurses = value;
        //        OnPropertyChanged(nameof(Nurses));
        //    }
        //}

        public double SimStatQuRegistration
        {
            get => _simStatQuRegistration;
            set
            {
                _simStatQuRegistration = value;
                OnPropertyChanged(nameof(SimStatQuRegistration));
            }
        }

        public double SimStatQuExamination
        {
            get => _simStatQuExamination;
            set
            {
                _simStatQuExamination = value;
                OnPropertyChanged(nameof(SimStatQuExamination));
            }
        }

        public double SimStatQuVaccination
        {
            get => _simStatQuVaccination;
            set
            {
                _simStatQuVaccination = value;
                OnPropertyChanged(nameof(SimStatQuVaccination));
            }
        }

        public double SimStatQuWaitingRoom
        {
            get => _simStatQuWaitingRoom;
            set
            {
                _simStatQuWaitingRoom = value;
                OnPropertyChanged(nameof(SimStatQuWaitingRoom));
            }
        }

        public double SimStatUtilAdminWorkers
        {
            get => _simStatUtilAdminWorkers;
            set
            {
                _simStatUtilAdminWorkers = value;
                OnPropertyChanged(nameof(SimStatUtilAdminWorkers));
            }
        }

        public double SimStatUtilDoctors
        {
            get => _simStatUtilDoctors;
            set
            {
                _simStatUtilDoctors = value; 
                OnPropertyChanged(nameof(SimStatUtilDoctors));
            }
        }

        public double SimStatUtilNurses
        {
            get => _simStatUtilNurses;
            set
            {
                _simStatUtilNurses = value;
                OnPropertyChanged(nameof(SimStatUtilNurses));
            }
        }

        public int SimQuRegistrationCount
        {
            get => _simQuRegistrationCount;
            set
            {
                _simQuRegistrationCount = value;
                OnPropertyChanged(nameof(SimQuRegistrationCount));
            }
        }

        public int SimAdminWorkersBusyCount
        {
            get => _simAdminWorkersBusyCount;
            set
            {
                _simAdminWorkersBusyCount = value;
                OnPropertyChanged(nameof(SimAdminWorkersBusyCount));
            }
        }

        public int SimAdminWorkersCount
        {
            get => _simAdminWorkersCount;
            set
            {
                _simAdminWorkersCount = value;
                OnPropertyChanged(nameof(SimAdminWorkersCount));
            }
        }

        public int SimQuExaminationCount
        {
            get => _simQuExaminationCount;
            set
            {
                _simQuExaminationCount = value;
                OnPropertyChanged(nameof(SimQuExaminationCount));
            }
        }

        public int SimDoctorsBusyCount
        {
            get => _simDoctorsBusyCount;
            set
            {
                _simDoctorsBusyCount = value;
                OnPropertyChanged(nameof(SimDoctorsBusyCount));
            }
        }

        public int SimDoctorsCount
        {
            get => _simDoctorsCount;
            set
            {
                _simDoctorsCount = value;
                OnPropertyChanged(nameof(SimDoctorsCount));
            }
        }

        public int SimQuVaccinationCount
        {
            get => _simQuVaccinationCount;
            set
            {
                _simQuVaccinationCount = value;
                OnPropertyChanged(nameof(SimQuVaccinationCount));
            }
        }

        public int SimNursesBusyCount
        {
            get => _simNursesBusyCount;
            set
            {
                _simNursesBusyCount = value;
                OnPropertyChanged(nameof(SimNursesBusyCount));
            }
        }

        public int SimNursesCount
        {
            get => _simNursesCount;
            set
            {
                _simNursesCount = value;
                OnPropertyChanged(nameof(SimNursesCount));
            }
        }

        public int SimQuWaitingRoomCount
        {
            get => _simQuWaitingRoomCount;
            set
            {
                _simQuWaitingRoomCount = value;
                OnPropertyChanged(nameof(SimQuWaitingRoomCount));
            }
        }

        public double SimQuRegistrationTime
        {
            get => _simQuRegistrationTime;
            set
            {
                _simQuRegistrationTime = value;
                OnPropertyChanged(nameof(SimQuRegistrationTime));
            }
        }

        public double SimQuExaminationTime
        {
            get => _simQuExaminationTime;
            set
            {
                _simQuExaminationTime = value;
                OnPropertyChanged(nameof(SimQuExaminationTime));
            }
        }

        public double SimQuVaccinationTime
        {
            get => _simQuVaccinationTime;
            set
            {
                _simQuVaccinationTime = value;
                OnPropertyChanged(nameof(SimQuVaccinationTime));
            }
        }

        #endregion

        #region Replications Properties

        public Func<double, string> XFormatter { get; set; }

        public int ReplicationsNum
        {
            get => _replicationsNum;
            set
            {
                _replicationsNum = value;
                OnPropertyChanged(nameof(ReplicationsNum));
            }
        }

        public int RepCount
        {
            get => _repCount;
            set
            {
                _repCount = value;
                OnPropertyChanged(nameof(RepCount));
            }
        }

        public double RepQuRegistrationSize
        {
            get => _repQuRegistrationSize;
            set
            {
                _repQuRegistrationSize = value;
                OnPropertyChanged(nameof(RepQuRegistrationSize));
            }
        }

        public double RepQuRegistrationTime
        {
            get => _repQuRegistrationTime;
            set
            {
                _repQuRegistrationTime = value;
                OnPropertyChanged(nameof(RepQuRegistrationTime));
            }
        }

        public double RepAdminWorkersUtil
        {
            get => _repAdminWorkersUtil;
            set
            {
                _repAdminWorkersUtil = value;
                OnPropertyChanged(nameof(RepAdminWorkersUtil));
            }
        }

        public double RepQuExaminationSize
        {
            get => _repQuExaminationSize;
            set
            {
                _repQuExaminationSize = value;
                OnPropertyChanged(nameof(RepQuExaminationSize));
            }
        }

        public double RepQuExaminationTime
        {
            get => _repQuExaminationTime;
            set
            {
                _repQuExaminationTime = value;
                OnPropertyChanged(nameof(RepQuExaminationTime));
            }
        }

        public double RepDoctorsUtil
        {
            get => _repDoctorsUtil;
            set
            {
                _repDoctorsUtil = value;
                OnPropertyChanged(nameof(RepDoctorsUtil));
            }
        }

        public double RepQuVaccinationSize
        {
            get => _repQuVaccinationSize;
            set
            {
                _repQuVaccinationSize = value;
                OnPropertyChanged(nameof(RepQuVaccinationSize));
            }
        }

        public double RepQuVaccinationTime
        {
            get => _repQuVaccinationTime;
            set
            {
                _repQuVaccinationTime = value;
                OnPropertyChanged(nameof(RepQuVaccinationTime));
            }
        }

        public double RepNursesUtil
        {
            get => _repNursesUtil;
            set
            {
                _repNursesUtil = value;
                OnPropertyChanged(nameof(RepNursesUtil));
            }
        }

        public double RepQuWaitingRoomSize
        {
            get => _repQuWaitingRoomSize;
            set
            {
                _repQuWaitingRoomSize = value;
                OnPropertyChanged(nameof(RepQuWaitingRoomSize));
            }
        }

        public double RepQuWaitingRoomSizeCiLower
        {
            get => _repQuWaitingRoomSizeCiLower;
            set
            {
                _repQuWaitingRoomSizeCiLower = value;
                OnPropertyChanged(nameof(RepQuWaitingRoomSizeCiLower));
            }
        }

        public double RepQuWaitingRoomSizeCiUpper
        {
            get => _repQuWaitingRoomSizeCiUpper;
            set
            {
                _repQuWaitingRoomSizeCiUpper = value;
                OnPropertyChanged(nameof(RepQuWaitingRoomSizeCiUpper));
            }
        }

        public SeriesCollection AvgWaitingRoomCountSeries
        {
            get => _avgWaitingRoomCountSeries;
            set
            {
                _avgWaitingRoomCountSeries = value;
                OnPropertyChanged(nameof(AvgWaitingRoomCountSeries));
            }
        }

        #endregion

        #region Experiment Properties

        public Func<double, string> XFormatterExp { get; set; }

        public SeriesCollection AvgExpQuLengthSeries
        {
            get => _avgExpQuLengthSeries;
            set
            {
                _avgExpQuLengthSeries = value;
                OnPropertyChanged(nameof(AvgExpQuLengthSeries));
            }
        }

        public SeriesCollection AvgExpQuWaitTimeSeries
        {
            get => _avgExpQuWaitTimeSeries;
            set
            {
                _avgExpQuWaitTimeSeries = value;
                OnPropertyChanged(nameof(AvgExpQuWaitTimeSeries));
            }
        }

        #endregion

        #region Settings Properties

        public int SetAdminWorkersCount
        {
            get => _setAdminWorkersCount;
            set
            {
                _setAdminWorkersCount = value;
                OnPropertyChanged(nameof(SetAdminWorkersCount));
            }
        }

        public int SetDoctorsCount
        {
            get => _setDoctorsCount;
            set
            {
                _setDoctorsCount = value;
                OnPropertyChanged(nameof(SetDoctorsCount));
            }
        }

        public int SetNursesCount
        {
            get => _setNursesCount;
            set
            {
                _setNursesCount = value;
                OnPropertyChanged(nameof(SetNursesCount));
            }
        }

        public int SetOrderedPatients
        {
            get => _setOrderedPatients;
            set
            {
                _setOrderedPatients = value;
                OnPropertyChanged(nameof(SetOrderedPatients));
            }
        }

        public bool SetCheckContSim
        {
            get => _setCheckContSim;
            set
            {
                _setCheckContSim = value;
                OnPropertyChanged(nameof(SetCheckContSim));
            }
        }

        public int SetChartVisibleValuesNum
        {
            get => _setChartVisibleValuesNum;
            set
            {
                _setChartVisibleValuesNum = value;
                OnPropertyChanged(nameof(SetChartVisibleValuesNum));
            }
        }

        public int SetNthObservation
        {
            get => _setNthObservation;
            set
            {
                _setNthObservation = value;
                OnPropertyChanged(nameof(SetNthObservation));
            }
        }

        public int SetExpReplicationsNum
        {
            get => _setExpReplicationsNum;
            set
            {
                _setExpReplicationsNum = value;
                OnPropertyChanged(nameof(SetExpReplicationsNum));
            }
        }

        public int SetMinDoctors
        {
            get => _setMinDoctors;
            set
            {
                _setMinDoctors = value;
                OnPropertyChanged(nameof(SetMinDoctors));
            }
        }

        public int SetMaxDoctors
        {
            get => _setMaxDoctors;
            set
            {
                _setMaxDoctors = value;
                OnPropertyChanged(nameof(SetMaxDoctors));
            }
        }

        #endregion

        public MainWindow()
        {
            InitializeComponent();

            _windowIsMaximized = false;

            SpeedValues = new []{ 1, 2, 5, 10, 50, 100, 500, 1000 };

            SimulationTimeSpan = TimeSpan.FromSeconds(0);

            SimStatQuRegistration = 0;
            SimStatQuExamination = 0;
            SimStatQuVaccination = 0;
            SimStatQuWaitingRoom = 0;

            SimStatUtilAdminWorkers = 0;
            SimStatUtilDoctors = 0;
            SimStatUtilNurses = 0;
            
            SimQuRegistrationCount = 0;
            SimAdminWorkersBusyCount = 0;
            SimAdminWorkersCount = 0;
            
            SimQuExaminationCount = 0;
            SimDoctorsBusyCount = 0;
            SimDoctorsCount = 0;
            
            SimQuVaccinationCount = 0;
            SimNursesBusyCount = 0;
            SimNursesCount = 0;
            
            SimQuWaitingRoomCount = 0;

            SimQuRegistrationTime = 0;
            SimQuExaminationTime = 0;
            SimQuVaccinationTime = 0;

            SetAdminWorkersCount = 6;
            SetDoctorsCount = 8;
            SetNursesCount = 4;
            SetOrderedPatients = 1000;
            SetCheckContSim = false;
            SetChartVisibleValuesNum = 100;
            SetNthObservation = 20;
            SetExpReplicationsNum = 5000;
            SetMinDoctors = 15;
            SetMaxDoctors = 25;

            ReplicationsNum = 1_000_000;
            RepCount = 0;
            
            RepQuRegistrationSize = 0;
            RepQuVaccinationTime = 0;
            RepAdminWorkersUtil = 0;

            RepQuExaminationSize = 0;
            RepQuExaminationTime = 0;
            RepDoctorsUtil = 0;

            RepQuVaccinationSize = 0;
            RepQuVaccinationTime = 0;
            RepNursesUtil = 0;
            
            RepQuWaitingRoomSize = 0;
            RepQuWaitingRoomSizeCiLower = 0;
            RepQuWaitingRoomSizeCiUpper = 0;

            _pageSim = new PageSimulation();
            _pageRep = new PageReplications();
            //_pageExp = new PageExperiment(this, _vaccineCentrum);
            //_pageSet = new PageSettings(this, _vaccineCentrum);

            AvgWaitingRoomCountSeries = new SeriesCollection
            {
                new LineSeries
                {
                    Values = new ChartValues<double>()
                }
            };

            AvgExpQuLengthSeries = new SeriesCollection
            {
                new LineSeries
                {
                    Values = new ChartValues<double>()
                }
            };

            AvgExpQuWaitTimeSeries = new SeriesCollection
            {
                new LineSeries
                {
                    Values = new ChartValues<double>()
                }
            };

            XFormatter = value =>
            {
                if (AvgWaitingRoomCountSeries[0].Values.Count >= SetChartVisibleValuesNum)
                    return (RepCount + SetNthObservation * (value - SetChartVisibleValuesNum)).ToString(CultureInfo.CurrentCulture);
                return (value * SetNthObservation).ToString(CultureInfo.CurrentCulture);
            };

            XFormatterExp = value => (SetMinDoctors + value).ToString(CultureInfo.CurrentCulture);

            DataContext = this;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            SystemCommands.CloseWindow(this);
        }

        private void ButtonMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (_windowIsMaximized)
            {
                SystemCommands.RestoreWindow(this);
                _windowIsMaximized = false;
            }
            else
            {
                SystemCommands.MaximizeWindow(this);
                _windowIsMaximized = true;
            }
        }

        private void ButtonMinimize_Click(object sender, RoutedEventArgs e)
        {
            SystemCommands.MinimizeWindow(this);
        }

        private void ButtonSim_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Content = _pageSim;
        }

        private void ButtonRep_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Content = _pageRep;
        }

        private void ButtonExp_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Content = _pageExp;
        }

        private void ButtonSet_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Content = _pageSet;
        }

        //public void UpdateInSameInterval(ISubject subject)
        //{
        //    if (subject is VaccineCentrum.VaccineCentrum simulation)
        //    {
        //        Application.Current.Dispatcher.Invoke(() => { });
        //        SimulationTimeSpan = TimeSpan.FromSeconds(simulation.SimulationTime) + TimeSpan.FromHours(8);
        //    }
        //}

        //public void UpdateBeforeSimulation(ISubject subject)
        //{
        //    if (subject is VaccineCentrum.VaccineCentrum simulation)
        //    {
        //        Application.Current.Dispatcher.Invoke(() => { });
        //        SimOrderedPatientsCount = simulation.OrderedPatientsNum;
        //        SimCanceledPatientsCount = simulation.CanceledPatientsNum;
        //        SimAdminWorkersCount = simulation.ResAdminWorkersCount;
        //        SimDoctorsCount = simulation.ResDoctorsCount;
        //        SimNursesCount = simulation.ResNursesCount;
        //    }
        //}

        //public void UpdateSimulation(ISubject subject)
        //{
        //    if (subject is VaccineCentrum.VaccineCentrum simulation)
        //    {
        //        Application.Current.Dispatcher.Invoke(() => { });
        //        SimulationTimeSpan = TimeSpan.FromSeconds(simulation.SimulationTime) + TimeSpan.FromHours(8);
        //        AdminWorkers = new ObservableCollection<AdminWorker>(simulation.ResAdminWorkers.Entities);
        //        Doctors = new ObservableCollection<Doctor>(simulation.ResDoctors.Entities);
        //        Nurses = new ObservableCollection<Nurse>(simulation.ResNurses.Entities);

        //        SimVaccinatedPatientsCount = simulation.VaccinatedPatientsCount;

        //        SimQuRegistrationCount = simulation.QuRegistration.Size;
        //        SimStatQuRegistration = simulation.StatQuRegistrationSize.Average;
        //        SimAdminWorkersBusyCount = simulation.ResAdminWorkers.BusyCount;
        //        SimAdminWorkersCount = simulation.ResAdminWorkersCount;

        //        SimQuExaminationCount = simulation.QuExamination.Size;
        //        SimStatQuExamination = simulation.StatQuExaminationSize.Average;
        //        SimDoctorsBusyCount = simulation.ResDoctors.BusyCount;
        //        SimDoctorsCount = simulation.ResDoctorsCount;
                
        //        SimQuVaccinationCount = simulation.QuVaccination.Size;
        //        SimStatQuVaccination = simulation.StatQuVaccinationSize.Average;
        //        SimNursesBusyCount = simulation.ResNurses.BusyCount;
        //        SimNursesCount = simulation.ResNursesCount;
                
        //        SimQuWaitingRoomCount = simulation.QuWaitingRoom.Size;
        //        SimStatQuWaitingRoom = simulation.StatQuWaitingRoomSize.Average;

        //        SimStatUtilAdminWorkers = simulation.ResAdminWorkers.AverageUtilization();
        //        SimStatUtilDoctors = simulation.ResDoctors.AverageUtilization();
        //        SimStatUtilNurses = simulation.ResNurses.AverageUtilization();

        //        SimQuRegistrationTime = simulation.StatQuRegistrationTime.Average;
        //        SimQuExaminationTime = simulation.StatQuExaminationTime.Average;
        //        SimQuVaccinationTime = simulation.StatQuVaccinationTime.Average;
        //    }
        //}

        //public void UpdateReplication(ISubject subject)
        //{
        //    if (subject is VaccineCentrum.VaccineCentrum simulation)
        //    {
        //        RepCount = simulation.ReplicationCount;

        //        RepQuRegistrationSize = simulation.StatRepQuRegistrationSize.Average;
        //        RepQuRegistrationTime = simulation.StatRepQuRegistrationTime.Average;
        //        RepAdminWorkersUtil = simulation.StatRepAdminWorkersUtil.Average;

        //        RepQuExaminationSize = simulation.StatRepQuExaminationSize.Average;
        //        RepQuExaminationTime = simulation.StatRepQuExaminationTime.Average;
        //        RepDoctorsUtil = simulation.StatRepDoctorsUtil.Average;

        //        RepQuVaccinationSize = simulation.StatRepQuVaccinationSize.Average;
        //        RepQuVaccinationTime = simulation.StatRepQuVaccinationTime.Average;
        //        RepNursesUtil = simulation.StatRepNursesUtil.Average;

        //        RepQuWaitingRoomSize = simulation.StatRepQuWaitingRoomSize.Average;
        //        RepQuWaitingRoomSizeCiLower = RepQuWaitingRoomSize - simulation.StatRepQuWaitingRoomSize.ConfidenceInterval95;
        //        RepQuWaitingRoomSizeCiUpper = RepQuWaitingRoomSize + simulation.StatRepQuWaitingRoomSize.ConfidenceInterval95;

        //        Application.Current.Dispatcher.Invoke(() =>
        //        {
        //            if (simulation.ReplicationCount % SetNthObservation == 0)
        //            {
        //                AvgWaitingRoomCountSeries[0].Values.Add(RepQuWaitingRoomSize);
        //                if (AvgWaitingRoomCountSeries[0].Values.Count > SetChartVisibleValuesNum) 
        //                    AvgWaitingRoomCountSeries[0].Values.RemoveAt(0);
        //            }
        //        });
        //    }
        //}

        //public void UpdateExperiment(ISubject subject)
        //{
        //    if (subject is VaccineCentrum.VaccineCentrum simulation)
        //    {
        //        Application.Current.Dispatcher.Invoke(() =>
        //        {
        //            if (simulation.ReplicationCount % SetNthObservation == 0)
        //            {
        //                AvgExpQuLengthSeries[0].Values.Add(RepQuExaminationSize);
        //                AvgExpQuWaitTimeSeries[0].Values.Add(RepQuExaminationTime);
        //            }
        //        });
        //    }
        //}  
    }
}
