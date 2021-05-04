using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using entities;
using simulation;
using OSPABA;
using GUI.Annotations;

namespace GUI.Pages
{
    /// <summary>
    /// Interaction logic for PageSimulation.xaml
    /// </summary>
    public partial class PageSimulation : Page, INotifyPropertyChanged, ISimDelegate
    {
        private readonly MySimulation _simRef;
        private Thread _simulationThread;
        private MainWindow _mw;
        private bool _simPaused;
        private readonly double[] _speedValues;
        private int _orderedPatientsNum;
        private int _arrivedPatientsCount;
        private TimeSpan _simulationTimeSpan;
        private int _vaccinatedPatientsCount;
        private int _canceledPatientsNum;
        private int _quRegistrationSize;
        private double _quRegistrationAverageSize;
        private double _quRegistrationAverageTime;
        private int _adminWorkersCount;
        private int _adminWorkersBusyCount;
        private double _adminWorkersUtilization;
        private int _quExaminationSize;
        private double _quExaminationAverageSize;
        private double _quExaminationAverageTime;
        private int _doctorsCount;
        private int _doctorsBusyCount;
        private double _doctorsUtilization;
        private int _quVaccinationSize;
        private double _quVaccinationAverageSize;
        private double _quVaccinationAverageTime;
        private int _nursesCount;
        private int _nursesBusyCount;
        private double _nursesUtilization;
        private int _quWaitingRoomSize;
        private double _quWaitingRoomAverageSize;
        private ObservableCollection<EntityAdminWorker> _adminWorkers;
        private ObservableCollection<EntityDoctor> _doctors;
        private ObservableCollection<EntityNurse> _nurses;
        private int _nursesFillingCount;
        private int _quNursesSize;
        private double _quNursesAverageSize;
        private int _currentCentrumPatients;
        private int _patientsRegToExaCount;
        private int _patientsExaToVacCount;
        private int _patientsVacToWaiCount;

        #region PROPERTIES

        public TimeSpan SimulationTimeSpan
        {
            get => _simulationTimeSpan;
            set
            {
                _simulationTimeSpan = value;
                OnPropertyChanged(nameof(SimulationTimeSpan));
            }
        }

        public int OrderedPatientsNum
        {
            get => _orderedPatientsNum;
            set
            {
                _orderedPatientsNum = value;
                OnPropertyChanged(nameof(OrderedPatientsNum));
            }
        }

        public int CanceledPatientsNum
        {
            get => _canceledPatientsNum;
            set
            {
                _canceledPatientsNum = value;
                OnPropertyChanged(nameof(CanceledPatientsNum));
            }
        }

        public int ArrivedPatientsCount
        {
            get => _arrivedPatientsCount;
            set
            {
                _arrivedPatientsCount = value;
                OnPropertyChanged(nameof(ArrivedPatientsCount));
            }
        }

        public int VaccinatedPatientsCount
        {
            get => _vaccinatedPatientsCount;
            set
            {
                _vaccinatedPatientsCount = value;
                OnPropertyChanged(nameof(VaccinatedPatientsCount));
            }
        }

        public int QuRegistrationSize
        {
            get => _quRegistrationSize;
            set
            {
                _quRegistrationSize = value;
                OnPropertyChanged(nameof(QuRegistrationSize));
            }
        }

        public double QuRegistrationAverageSize
        {
            get => _quRegistrationAverageSize;
            set
            {
                _quRegistrationAverageSize = value;
                OnPropertyChanged(nameof(QuRegistrationAverageSize));
            }
        }

        public double QuRegistrationAverageTime
        {
            get => _quRegistrationAverageTime;
            set
            {
                _quRegistrationAverageTime = value;
                OnPropertyChanged(nameof(QuRegistrationAverageTime));
            }
        }

        public int AdminWorkersCount
        {
            get => _adminWorkersCount;
            set
            {
                _adminWorkersCount = value;
                OnPropertyChanged(nameof(AdminWorkersCount));
            }
        }

        public int AdminWorkersBusyCount
        {
            get => _adminWorkersBusyCount;
            set
            {
                _adminWorkersBusyCount = value;
                OnPropertyChanged(nameof(AdminWorkersBusyCount));
            }
        }

        public double AdminWorkersUtilization
        {
            get => _adminWorkersUtilization;
            set
            {
                _adminWorkersUtilization = value;
                OnPropertyChanged(nameof(AdminWorkersUtilization));
            }
        }

        public int QuExaminationSize
        {
            get => _quExaminationSize;
            set
            {
                _quExaminationSize = value;
                OnPropertyChanged(nameof(QuExaminationSize));
            }
        }

        public double QuExaminationAverageSize
        {
            get => _quExaminationAverageSize;
            set
            {
                _quExaminationAverageSize = value;
                OnPropertyChanged(nameof(QuExaminationAverageSize));
            }
        }

        public double QuExaminationAverageTime
        {
            get => _quExaminationAverageTime;
            set
            {
                _quExaminationAverageTime = value;
                OnPropertyChanged(nameof(QuExaminationAverageTime));
            }
        }

        public int DoctorsCount
        {
            get => _doctorsCount;
            set
            {
                _doctorsCount = value;
                OnPropertyChanged(nameof(DoctorsCount));
            }
        }

        public int DoctorsBusyCount
        {
            get => _doctorsBusyCount;
            set
            {
                _doctorsBusyCount = value;
                OnPropertyChanged(nameof(DoctorsBusyCount));
            }
        }

        public double DoctorsUtilization
        {
            get => _doctorsUtilization;
            set
            {
                _doctorsUtilization = value;
                OnPropertyChanged(nameof(DoctorsUtilization));
            }
        }

        public int QuVaccinationSize
        {
            get => _quVaccinationSize;
            set
            {
                _quVaccinationSize = value;
                OnPropertyChanged(nameof(QuVaccinationSize));
            }
        }

        public double QuVaccinationAverageSize
        {
            get => _quVaccinationAverageSize;
            set
            {
                _quVaccinationAverageSize = value;
                OnPropertyChanged(nameof(QuVaccinationAverageSize));
            }
        }

        public double QuVaccinationAverageTime
        {
            get => _quVaccinationAverageTime;
            set
            {
                _quVaccinationAverageTime = value;
                OnPropertyChanged(nameof(QuVaccinationAverageTime));
            }
        }

        public int NursesCount
        {
            get => _nursesCount;
            set
            {
                _nursesCount = value;
                OnPropertyChanged(nameof(NursesCount));
            }
        }

        public int NursesBusyCount
        {
            get => _nursesBusyCount;
            set
            {
                _nursesBusyCount = value;
                OnPropertyChanged(nameof(NursesBusyCount));
            }
        }

        public double NursesUtilization
        {
            get => _nursesUtilization;
            set
            {
                _nursesUtilization = value;
                OnPropertyChanged(nameof(NursesUtilization));
            }
        }

        public int NursesFillingCount
        {
            get => _nursesFillingCount;
            set
            {
                _nursesFillingCount = value;
                OnPropertyChanged(nameof(NursesFillingCount));
            }
        }

        public int QuNursesSize
        {
            get => _quNursesSize;
            set
            {
                _quNursesSize = value;
                OnPropertyChanged(nameof(QuNursesSize));
            }
        }

        public double QuNursesAverageSize
        {
            get => _quNursesAverageSize;
            set
            {
                _quNursesAverageSize = value;
                OnPropertyChanged(nameof(QuNursesAverageSize));
            }
        }

        public int QuWaitingRoomSize
        {
            get => _quWaitingRoomSize;
            set
            {
                _quWaitingRoomSize = value;
                OnPropertyChanged(nameof(QuWaitingRoomSize));
            }
        }

        public double QuWaitingRoomAverageSize
        {
            get => _quWaitingRoomAverageSize;
            set
            {
                _quWaitingRoomAverageSize = value;
                OnPropertyChanged(nameof(QuWaitingRoomAverageSize));
            }
        }

        public ObservableCollection<EntityAdminWorker> AdminWorkers
        {
            get => _adminWorkers;
            set
            {
                _adminWorkers = value;
                OnPropertyChanged(nameof(AdminWorkers));
            }
        }

        public ObservableCollection<EntityDoctor> Doctors
        {
            get => _doctors;
            set
            {
                _doctors = value;
                OnPropertyChanged(nameof(Doctors));
            }
        }

        public ObservableCollection<EntityNurse> Nurses
        {
            get => _nurses;
            set
            {
                _nurses = value;
                OnPropertyChanged(nameof(Nurses));
            }
        }

        public int CurrentCentrumPatients
        {
            get => _currentCentrumPatients;
            set
            {
                _currentCentrumPatients = value;
                OnPropertyChanged(nameof(CurrentCentrumPatients));
            }
        }

        public int PatientsRegToExaCount
        {
            get => _patientsRegToExaCount;
            set
            {
                _patientsRegToExaCount = value;
                OnPropertyChanged(nameof(PatientsRegToExaCount));
            }
        }

        public int PatientsExaToVacCount
        {
            get => _patientsExaToVacCount;
            set
            {
                _patientsExaToVacCount = value;
                OnPropertyChanged(nameof(PatientsExaToVacCount));
            }
        }

        public int PatientsVacToWaiCount
        {
            get => _patientsVacToWaiCount;
            set
            {
                _patientsVacToWaiCount = value;
                OnPropertyChanged(nameof(PatientsVacToWaiCount));
            }
        }

        #endregion

        public PageSimulation(MainWindow mw)
        {
            InitializeComponent();
            _mw = mw;
            _simRef = new MySimulation();
            _simRef.RegisterDelegate(this);
            DataContext = this;

            _speedValues = new[] {1, 2.5, 5, 10, 25, 50, 100, 250, 500, 1000, 2500, 5000, 10000};

            InitGui();
        }

        private void InitGui()
        {
            SimulationTimeSpan = TimeSpan.FromSeconds(0);
            OrderedPatientsNum = 0;
            CanceledPatientsNum = 0;
            ArrivedPatientsCount = 0;
            VaccinatedPatientsCount = 0;

            QuRegistrationSize = 0;
            QuRegistrationAverageSize = 0;
            QuRegistrationAverageTime = 0;
            AdminWorkersCount = 0;
            AdminWorkersBusyCount = 0;
            AdminWorkersUtilization = 0;

            QuExaminationSize = 0;
            QuExaminationAverageSize = 0;
            QuExaminationAverageTime = 0;
            DoctorsCount = 0;
            DoctorsBusyCount = 0;
            DoctorsUtilization = 0;

            QuVaccinationSize = 0;
            QuVaccinationAverageSize = 0;
            QuVaccinationAverageTime = 0;
            NursesCount = 0;
            NursesBusyCount = 0;
            NursesUtilization = 0;
            
            NursesFillingCount = 0;
            QuNursesSize = 0;
            QuNursesAverageSize = 0;

            QuWaitingRoomSize = 0;
            QuWaitingRoomAverageSize = 0;

            CurrentCentrumPatients = 0;
            PatientsRegToExaCount = 0;
            PatientsExaToVacCount = 0;
            PatientsVacToWaiCount = 0;
        }

        private void RunSimulation()
        {
            _simRef.SetSimSpeed(1, 1);
            _simRef.OrderedPatientsNum = _mw.SetOrderedPatients;
            _simRef.ResAdminWorkersCount = _mw.SetAdminWorkersCount;
            _simRef.ResDoctorsCount = _mw.SetDoctorsCount;
            _simRef.ResNursesCount = _mw.SetNursesCount;
            _simRef.EnableEarlyArrivals = true;
            _simRef.Simulate(1, double.MaxValue);
        }

        private void ButtonSimStart_Click(object sender, RoutedEventArgs e)
        {
            _simRef.StopSimulation();
            _simulationThread?.Abort();
            _simulationThread = new Thread(RunSimulation);
            _simulationThread.Start();
            _simPaused = false;
        }

        private void ButtonSimPause_Click(object sender, RoutedEventArgs e)
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

        private void ButtonSimStop_Click(object sender, RoutedEventArgs e)
        {
            _simRef.StopSimulation();
            _simulationThread?.Abort();
            _simPaused = false;
        }

        private void ComboSpeed_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboSpeed.SelectedIndex < 4)
                _simRef?.SetSimSpeed(1, 1.0 / _speedValues[ComboSpeed.SelectedIndex]);
            else
                _simRef?.SetSimSpeed(_speedValues[ComboSpeed.SelectedIndex] / 10, 0.1);
        }

        public void SimStateChanged(Simulation sim, SimState state)
        {
            
        }

        public void Refresh(Simulation sim)
        {
            if (sim is MySimulation simulation)
            {
                Application.Current.Dispatcher.Invoke(() => { });

                SimulationTimeSpan = TimeSpan.FromSeconds(simulation.CurrentTime) + TimeSpan.FromHours(8);
                OrderedPatientsNum = simulation.OrderedPatientsNum;
                CanceledPatientsNum = simulation.AgentSurrounding.CanceledPatientsNum;
                ArrivedPatientsCount = simulation.AgentCentrum.ArrivedPatientsCount;
                VaccinatedPatientsCount = simulation.AgentCentrum.VaccinatedPatientsCount;

                QuRegistrationSize = simulation.AgentRegistration.QuRegistration.Size;
                QuRegistrationAverageSize = simulation.AgentRegistration.StatQuRegistrationSize.Mean();
                QuRegistrationAverageTime = simulation.AgentRegistration.StatQuRegistrationTime.Mean();
                AdminWorkersCount = simulation.AgentRegistration.PoolAdminWorkers.Count;
                AdminWorkersBusyCount = simulation.AgentRegistration.PoolAdminWorkers.BusyCount;
                AdminWorkersUtilization = simulation.AgentRegistration.PoolAdminWorkers.AverageUtilization();

                QuExaminationSize = simulation.AgentExamination.QuExamination.Size;
                QuExaminationAverageSize = simulation.AgentExamination.StatQuExaminationSize.Mean();
                QuExaminationAverageTime = simulation.AgentExamination.StatQuExaminationTime.Mean();
                DoctorsCount = simulation.AgentExamination.PoolDoctors.Count;
                DoctorsBusyCount = simulation.AgentExamination.PoolDoctors.BusyCount;
                DoctorsUtilization = simulation.AgentExamination.PoolDoctors.AverageUtilization();

                QuVaccinationSize = simulation.AgentVaccination.QuVaccination.Size;
                QuVaccinationAverageSize = simulation.AgentVaccination.StatQuVaccinationSize.Mean();
                QuVaccinationAverageTime = simulation.AgentVaccination.StatQuVaccinationTime.Mean();
                NursesCount = simulation.AgentVaccination.PoolNurses.Count;
                NursesBusyCount = simulation.AgentVaccination.PoolNurses.BusyCount;
                NursesUtilization = simulation.AgentVaccination.PoolNurses.AverageUtilization();

                NursesFillingCount = simulation.AgentColdStorage.PreparingNursesCount;
                QuNursesSize = simulation.AgentColdStorage.QuNurses.Size;
                QuNursesAverageSize = simulation.AgentColdStorage.StatQuNursesSize.Mean();

                QuWaitingRoomSize = simulation.AgentWaitingRoom.WaitingPatientsCount;
                QuWaitingRoomAverageSize = simulation.AgentWaitingRoom.StatWaitingPatientsCount.Mean();

                CurrentCentrumPatients = ArrivedPatientsCount - VaccinatedPatientsCount;
                PatientsRegToExaCount = simulation.AgentCentrum.MovingPatientsRegToExa;
                PatientsExaToVacCount = simulation.AgentCentrum.MovingPatientsExaToVac;
                PatientsVacToWaiCount = simulation.AgentCentrum.MovingPatientsVacToWai;

                AdminWorkers = new ObservableCollection<EntityAdminWorker>(simulation.AgentRegistration.PoolAdminWorkers.Entities);
                Doctors = new ObservableCollection<EntityDoctor>(simulation.AgentExamination.PoolDoctors.Entities);
                Nurses = new ObservableCollection<EntityNurse>(simulation.AgentVaccination.PoolNurses.Entities);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
