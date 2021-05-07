using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using GUI.Annotations;
using OSPABA;
using simulation;

namespace GUI.Pages
{
    /// <summary>
    /// Interaction logic for PageReplications.xaml
    /// </summary>
    public partial class PageReplications : Page, INotifyPropertyChanged
    {
        private readonly MySimulation _simRef;
        private Thread _simulationThread;
        private readonly MainWindow _mw;
        private int _replicationsCount;
        private double _registrationQuSize;
        private double _registrationQuTime;
        private double _adminWorkersUtilization;
        private double _examinationQuSize;
        private double _examinationQuTime;
        private double _doctorsUtilization;
        private double _vaccinationQuSize;
        private double _vaccinationQuTime;
        private double _nursesUtilization;
        private double _waitingPatientsCount;
        private double _waitingPatientsCount95Up;
        private double _waitingPatientsCount95Lo;
        private double _registrationQuSize95Up;
        private double _registrationQuSize95Lo;
        private double _registrationQuTime95Up;
        private double _registrationQuTime95Lo;
        private double _adminWorkersUtilization95Up;
        private double _adminWorkersUtilization95Lo;
        private double _examinationQuSize95Up;
        private double _examinationQuSize95Lo;
        private double _examinationQuTime95Up;
        private double _examinationQuTime95Lo;
        private double _doctorsUtilization95Up;
        private double _doctorsUtilization95Lo;
        private double _vaccinationQuSize95Up;
        private double _vaccinationQuSize95Lo;
        private double _vaccinationQuTime95Up;
        private double _vaccinationQuTime95Lo;
        private double _nursesUtilization95Up;
        private double _nursesUtilization95Lo;
        private double _coldStorageQuSize;
        private double _coldStorageQuSize95Lo;
        private double _coldStorageQuSize95Up;
        private TimeSpan _centreOvertime;
        private TimeSpan _centreOvertime95Lo;
        private TimeSpan _centreOvertime95Up;
        private bool _simPaused;
        private double _employeeUtilization;
        private double _employeeUtilization95Lo;
        private double _employeeUtilization95Up;
        private TimeSpan _sumWaitingTime;
        private TimeSpan _sumWaitingTime95Lo;
        private TimeSpan _sumWaitingTime95Up;

        #region PROPERTIES

        public int ReplicationsCount
        {
            get => _replicationsCount;
            set
            {
                _replicationsCount = value;
                OnPropertyChanged(nameof(ReplicationsCount));
            }
        }

        public double RegistrationQuSize
        {
            get => _registrationQuSize;
            set
            {
                _registrationQuSize = value;
                OnPropertyChanged(nameof(RegistrationQuSize));
            }
        }

        public double RegistrationQuSize95Up
        {
            get => _registrationQuSize95Up;
            set
            {
                _registrationQuSize95Up = value;
                OnPropertyChanged(nameof(RegistrationQuSize95Up));
            }
        }

        public double RegistrationQuSize95Lo
        {
            get => _registrationQuSize95Lo;
            set
            {
                _registrationQuSize95Lo = value;
                OnPropertyChanged(nameof(RegistrationQuSize95Lo));
            }
        }

        public double RegistrationQuTime
        {
            get => _registrationQuTime;
            set
            {
                _registrationQuTime = value;
                OnPropertyChanged(nameof(RegistrationQuTime));
            }
        }

        public double RegistrationQuTime95Up
        {
            get => _registrationQuTime95Up;
            set
            {
                _registrationQuTime95Up = value;
                OnPropertyChanged(nameof(RegistrationQuTime95Up));
            }
        }

        public double RegistrationQuTime95Lo
        {
            get => _registrationQuTime95Lo;
            set
            {
                _registrationQuTime95Lo = value;
                OnPropertyChanged(nameof(RegistrationQuTime95Lo));
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

        public double AdminWorkersUtilization95Up
        {
            get => _adminWorkersUtilization95Up;
            set
            {
                _adminWorkersUtilization95Up = value;
                OnPropertyChanged(nameof(AdminWorkersUtilization95Up));
            }
        }

        public double AdminWorkersUtilization95Lo
        {
            get => _adminWorkersUtilization95Lo;
            set
            {
                _adminWorkersUtilization95Lo = value;
                OnPropertyChanged(nameof(AdminWorkersUtilization95Lo));
            }
        }

        public double ExaminationQuSize
        {
            get => _examinationQuSize;
            set
            {
                _examinationQuSize = value;
                OnPropertyChanged(nameof(ExaminationQuSize));
            }
        }

        public double ExaminationQuSize95Up
        {
            get => _examinationQuSize95Up;
            set
            {
                _examinationQuSize95Up = value;
                OnPropertyChanged(nameof(ExaminationQuSize95Up));
            }
        }

        public double ExaminationQuSize95Lo
        {
            get => _examinationQuSize95Lo;
            set
            {
                _examinationQuSize95Lo = value;
                OnPropertyChanged(nameof(ExaminationQuSize95Lo));
            }
        }

        public double ExaminationQuTime
        {
            get => _examinationQuTime;
            set
            {
                _examinationQuTime = value;
                OnPropertyChanged(nameof(ExaminationQuTime));
            }
        }

        public double ExaminationQuTime95Up
        {
            get => _examinationQuTime95Up;
            set
            {
                _examinationQuTime95Up = value;
                OnPropertyChanged(nameof(ExaminationQuTime95Up));
            }
        }

        public double ExaminationQuTime95Lo
        {
            get => _examinationQuTime95Lo;
            set
            {
                _examinationQuTime95Lo = value;
                OnPropertyChanged(nameof(ExaminationQuTime95Lo));
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

        public double DoctorsUtilization95Up
        {
            get => _doctorsUtilization95Up;
            set
            {
                _doctorsUtilization95Up = value;
                OnPropertyChanged(nameof(DoctorsUtilization95Up));
            }
        }

        public double DoctorsUtilization95Lo
        {
            get => _doctorsUtilization95Lo;
            set
            {
                _doctorsUtilization95Lo = value;
                OnPropertyChanged(nameof(DoctorsUtilization95Lo));
            }
        }

        public double VaccinationQuSize
        {
            get => _vaccinationQuSize;
            set
            {
                _vaccinationQuSize = value;
                OnPropertyChanged(nameof(VaccinationQuSize));
            }
        }

        public double VaccinationQuSize95Up
        {
            get => _vaccinationQuSize95Up;
            set
            {
                _vaccinationQuSize95Up = value;
                OnPropertyChanged(nameof(VaccinationQuSize95Up));
            }
        }

        public double VaccinationQuSize95Lo
        {
            get => _vaccinationQuSize95Lo;
            set
            {
                _vaccinationQuSize95Lo = value;
                OnPropertyChanged(nameof(VaccinationQuSize95Lo));
            }
        }

        public double VaccinationQuTime
        {
            get => _vaccinationQuTime;
            set
            {
                _vaccinationQuTime = value;
                OnPropertyChanged(nameof(VaccinationQuTime));
            }
        }

        public double VaccinationQuTime95Up
        {
            get => _vaccinationQuTime95Up;
            set
            {
                _vaccinationQuTime95Up = value;
                OnPropertyChanged(nameof(VaccinationQuTime95Up));
            }
        }

        public double VaccinationQuTime95Lo
        {
            get => _vaccinationQuTime95Lo;
            set
            {
                _vaccinationQuTime95Lo = value;
                OnPropertyChanged(nameof(VaccinationQuTime95Lo));
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

        public double NursesUtilization95Up
        {
            get => _nursesUtilization95Up;
            set
            {
                _nursesUtilization95Up = value;
                OnPropertyChanged(nameof(NursesUtilization95Up));
            }
        }

        public double NursesUtilization95Lo
        {
            get => _nursesUtilization95Lo;
            set
            {
                _nursesUtilization95Lo = value;
                OnPropertyChanged(nameof(NursesUtilization95Lo));
            }
        }

        public double WaitingPatientsCount
        {
            get => _waitingPatientsCount;
            set
            {
                _waitingPatientsCount = value;
                OnPropertyChanged(nameof(WaitingPatientsCount));
            }
        }

        public double WaitingPatientsCount95Lo
        {
            get => _waitingPatientsCount95Lo;
            set
            {
                _waitingPatientsCount95Lo = value;
                OnPropertyChanged(nameof(WaitingPatientsCount95Lo));
            }
        }

        public double WaitingPatientsCount95Up
        {
            get => _waitingPatientsCount95Up;
            set
            {
                _waitingPatientsCount95Up = value;
                OnPropertyChanged(nameof(WaitingPatientsCount95Up));
            }
        }

        public double ColdStorageQuSize
        {
            get => _coldStorageQuSize;
            set
            {
                _coldStorageQuSize = value;
                OnPropertyChanged(nameof(ColdStorageQuSize));
            }
        }

        public double ColdStorageQuSize95Lo
        {
            get => _coldStorageQuSize95Lo;
            set
            {
                _coldStorageQuSize95Lo = value;
                OnPropertyChanged(nameof(ColdStorageQuSize95Lo));
            }
        }

        public double ColdStorageQuSize95Up
        {
            get => _coldStorageQuSize95Up;
            set
            {
                _coldStorageQuSize95Up = value;
                OnPropertyChanged(nameof(ColdStorageQuSize95Up));
            }
        }

        public TimeSpan CentreOvertime
        {
            get => _centreOvertime;
            set
            {
                _centreOvertime = value;
                OnPropertyChanged(nameof(CentreOvertime));
            }
        }

        public TimeSpan CentreOvertime95Lo
        {
            get => _centreOvertime95Lo;
            set
            {
                _centreOvertime95Lo = value;
                OnPropertyChanged(nameof(CentreOvertime95Lo));
            }
        }

        public TimeSpan CentreOvertime95Up
        {
            get => _centreOvertime95Up;
            set
            {
                _centreOvertime95Up = value;
                OnPropertyChanged(nameof(CentreOvertime95Up));
            }
        }

        public double EmployeeUtilization
        {
            get => _employeeUtilization;
            set
            {
                _employeeUtilization = value;
                OnPropertyChanged(nameof(EmployeeUtilization));
            }
        }

        public double EmployeeUtilization95Lo
        {
            get => _employeeUtilization95Lo;
            set
            {
                _employeeUtilization95Lo = value;
                OnPropertyChanged(nameof(EmployeeUtilization95Lo));
            }
        }

        public double EmployeeUtilization95Up
        {
            get => _employeeUtilization95Up;
            set
            {
                _employeeUtilization95Up = value;
                OnPropertyChanged(nameof(EmployeeUtilization95Up));
            }
        }

        public TimeSpan SumWaitingTime
        {
            get => _sumWaitingTime;
            set
            {
                _sumWaitingTime = value;
                OnPropertyChanged(nameof(SumWaitingTime));
            }
        }

        public TimeSpan SumWaitingTime95Lo
        {
            get => _sumWaitingTime95Lo;
            set
            {
                _sumWaitingTime95Lo = value;
                OnPropertyChanged(nameof(SumWaitingTime95Lo));
            }
        }

        public TimeSpan SumWaitingTime95Up
        {
            get => _sumWaitingTime95Up;
            set
            {
                _sumWaitingTime95Up = value;
                OnPropertyChanged(nameof(SumWaitingTime95Up));
            }
        }

        #endregion

        public PageReplications(MainWindow mw)
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

            RegistrationQuSize = 0;
            RegistrationQuSize95Lo = 0;
            RegistrationQuSize95Up = 0;
            
            RegistrationQuTime = 0;
            RegistrationQuTime95Lo = 0;
            RegistrationQuTime95Up = 0;
            
            AdminWorkersUtilization = 0;
            AdminWorkersUtilization95Lo = 0;
            AdminWorkersUtilization95Up = 0;
            
            ExaminationQuSize = 0;
            ExaminationQuSize95Lo = 0;
            ExaminationQuSize95Up = 0;

            ExaminationQuTime = 0;
            ExaminationQuTime95Lo = 0;
            ExaminationQuTime95Up = 0;

            DoctorsUtilization = 0;
            DoctorsUtilization95Lo = 0;
            DoctorsUtilization95Up = 0;

            VaccinationQuSize = 0;
            VaccinationQuSize95Lo = 0;
            VaccinationQuSize95Up = 0;

            VaccinationQuTime = 0;
            VaccinationQuTime95Lo = 0;
            VaccinationQuTime95Up = 0;

            NursesUtilization = 0;
            NursesUtilization95Lo = 0;
            NursesUtilization95Up = 0;

            WaitingPatientsCount = 0;
            WaitingPatientsCount95Lo = 0;
            WaitingPatientsCount95Up = 0;

            ColdStorageQuSize = 0;
            ColdStorageQuSize95Lo = 0;
            ColdStorageQuSize95Up = 0;

            CentreOvertime = TimeSpan.Zero;
            CentreOvertime95Lo = TimeSpan.Zero;
            CentreOvertime95Up = TimeSpan.Zero;

            EmployeeUtilization = 0;
            EmployeeUtilization95Lo = 0;
            EmployeeUtilization95Up = 0;

            SumWaitingTime = TimeSpan.Zero;
            SumWaitingTime95Lo = TimeSpan.Zero;
            SumWaitingTime95Up = TimeSpan.Zero;
        }

        private void RunSimulation()
        {
            _simRef.OrderedPatientsNum = _mw.SetOrderedPatients;
            _simRef.ResAdminWorkersCount = _mw.SetAdminWorkersCount;
            _simRef.ResDoctorsCount = _mw.SetDoctorsCount;
            _simRef.ResNursesCount = _mw.SetNursesCount;
            _simRef.EnableEarlyArrivals = _mw.SetCheckEarlyArrivals;
            _simRef.EnableLightModel = _mw.SetCheckLightModel;

            _simRef.OnReplicationDidFinish(s =>
            {
                var sim = (MySimulation) s;

                Application.Current.Dispatcher.Invoke(() =>
                {
                    ReplicationsCount = sim.CurrentReplication + 1;

                    RegistrationQuSize = sim.RegistrationQuSize.Mean();
                    RegistrationQuSize95Lo = sim.RegistrationQuSize.SampleSize > 2 ? sim.RegistrationQuSize.ConfidenceInterval95[0] : 0;
                    RegistrationQuSize95Up = sim.RegistrationQuSize.SampleSize > 2 ? sim.RegistrationQuSize.ConfidenceInterval95[1] : 0;

                    RegistrationQuTime = sim.RegistrationQuTime.Mean();
                    RegistrationQuTime95Lo = sim.RegistrationQuTime.SampleSize > 2 ? sim.RegistrationQuTime.ConfidenceInterval95[0] : 0;
                    RegistrationQuTime95Up = sim.RegistrationQuTime.SampleSize > 2 ? sim.RegistrationQuTime.ConfidenceInterval95[1] : 0;

                    AdminWorkersUtilization = sim.AdminWorkersUtilization.Mean();
                    AdminWorkersUtilization95Lo = sim.AdminWorkersUtilization.SampleSize > 2 ? sim.AdminWorkersUtilization.ConfidenceInterval95[0] : 0;
                    AdminWorkersUtilization95Up = sim.AdminWorkersUtilization.SampleSize > 2 ? sim.AdminWorkersUtilization.ConfidenceInterval95[1] : 0;

                    ExaminationQuSize = sim.ExaminationQuSize.Mean();
                    ExaminationQuSize95Lo = sim.ExaminationQuSize.SampleSize > 2 ? sim.ExaminationQuSize.ConfidenceInterval95[0] : 0;
                    ExaminationQuSize95Up = sim.ExaminationQuSize.SampleSize > 2 ? sim.ExaminationQuSize.ConfidenceInterval95[1] : 0;

                    ExaminationQuTime = sim.ExaminationQuTime.Mean();
                    ExaminationQuTime95Lo = sim.ExaminationQuTime.SampleSize > 2 ? sim.ExaminationQuTime.ConfidenceInterval95[0] : 0;
                    ExaminationQuTime95Up = sim.ExaminationQuTime.SampleSize > 2 ? sim.ExaminationQuTime.ConfidenceInterval95[1] : 0;

                    DoctorsUtilization = sim.DoctorsUtilization.Mean();
                    DoctorsUtilization95Lo = sim.DoctorsUtilization.SampleSize > 2 ? sim.DoctorsUtilization.ConfidenceInterval95[0] : 0;
                    DoctorsUtilization95Up = sim.DoctorsUtilization.SampleSize > 2 ? sim.DoctorsUtilization.ConfidenceInterval95[1] : 0;

                    VaccinationQuSize = sim.VaccinationQuSize.Mean();
                    VaccinationQuSize95Lo = sim.VaccinationQuSize.SampleSize > 2 ? sim.VaccinationQuSize.ConfidenceInterval95[0] : 0;
                    VaccinationQuSize95Up = sim.VaccinationQuSize.SampleSize > 2 ? sim.VaccinationQuSize.ConfidenceInterval95[1] : 0;

                    VaccinationQuTime = sim.VaccinationQuTime.Mean();
                    VaccinationQuTime95Lo = sim.VaccinationQuTime.SampleSize > 2 ? sim.VaccinationQuTime.ConfidenceInterval95[0] : 0;
                    VaccinationQuTime95Up = sim.VaccinationQuTime.SampleSize > 2 ? sim.VaccinationQuTime.ConfidenceInterval95[1] : 0;

                    NursesUtilization = sim.NursesUtilization.Mean();
                    NursesUtilization95Lo = sim.NursesUtilization.SampleSize > 2 ? sim.NursesUtilization.ConfidenceInterval95[0] : 0;
                    NursesUtilization95Up = sim.NursesUtilization.SampleSize > 2 ? sim.NursesUtilization.ConfidenceInterval95[1] : 0;

                    WaitingPatientsCount = sim.WaitingRoomQuSize.Mean();
                    WaitingPatientsCount95Lo = sim.WaitingRoomQuSize.SampleSize > 2 ? sim.WaitingRoomQuSize.ConfidenceInterval95[0] : 0;
                    WaitingPatientsCount95Up = sim.WaitingRoomQuSize.SampleSize > 2 ? sim.WaitingRoomQuSize.ConfidenceInterval95[1] : 0;

                    ColdStorageQuSize = sim.ColdStorageQuSize.Mean();
                    ColdStorageQuSize95Lo = sim.ColdStorageQuSize.SampleSize > 2 ? sim.ColdStorageQuSize.ConfidenceInterval95[0] : 0;
                    ColdStorageQuSize95Up = sim.ColdStorageQuSize.SampleSize > 2 ? sim.ColdStorageQuSize.ConfidenceInterval95[1] : 0;

                    CentreOvertime = TimeSpan.FromSeconds(sim.CentreOvertime.Mean());
                    CentreOvertime95Lo = TimeSpan.FromSeconds(sim.CentreOvertime.SampleSize > 2
                        ? sim.CentreOvertime.ConfidenceInterval95[0]
                        : 0);
                    CentreOvertime95Up = TimeSpan.FromSeconds(sim.CentreOvertime.SampleSize > 2
                        ? sim.CentreOvertime.ConfidenceInterval95[1]
                        : 0);

                    EmployeeUtilization = sim.EmployeeUtilization.Mean();
                    EmployeeUtilization95Lo = sim.EmployeeUtilization.SampleSize > 2 ? sim.EmployeeUtilization.ConfidenceInterval95[0] : 0;
                    EmployeeUtilization95Up = sim.EmployeeUtilization.SampleSize > 2 ? sim.EmployeeUtilization.ConfidenceInterval95[1] : 0;

                    SumWaitingTime = TimeSpan.FromSeconds(sim.SumWaitingTime.Mean());
                    SumWaitingTime95Lo = TimeSpan.FromSeconds(sim.SumWaitingTime.SampleSize > 2
                        ? sim.SumWaitingTime.ConfidenceInterval95[0]
                        : 0);
                    SumWaitingTime95Up = TimeSpan.FromSeconds(sim.SumWaitingTime.SampleSize > 2
                        ? sim.SumWaitingTime.ConfidenceInterval95[1]
                        : 0);
                });
            });

            _simRef.Simulate(10000, double.MaxValue);
        }

        private void ButtonRepStart_Click(object sender, RoutedEventArgs e)
        {
            _simRef.StopSimulation();
            _simulationThread?.Abort();
            _simulationThread = new Thread(RunSimulation);
            _simulationThread.Start();
            _simPaused = false;
        }

        private void ButtonRepPause_Click(object sender, RoutedEventArgs e)
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

        private void ButtonRepStop_Click(object sender, RoutedEventArgs e)
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
