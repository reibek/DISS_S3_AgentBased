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

        public double RegistrationQuTime
        {
            get => _registrationQuTime;
            set
            {
                _registrationQuTime = value;
                OnPropertyChanged(nameof(RegistrationQuTime));
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

        public double ExaminationQuSize
        {
            get => _examinationQuSize;
            set
            {
                _examinationQuSize = value;
                OnPropertyChanged(nameof(ExaminationQuSize));
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

        public double DoctorsUtilization
        {
            get => _doctorsUtilization;
            set
            {
                _doctorsUtilization = value;
                OnPropertyChanged(nameof(DoctorsUtilization));
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

        public double VaccinationQuTime
        {
            get => _vaccinationQuTime;
            set
            {
                _vaccinationQuTime = value;
                OnPropertyChanged(nameof(VaccinationQuTime));
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
            RegistrationQuTime = 0;
            AdminWorkersUtilization = 0;
            ExaminationQuSize = 0;
            ExaminationQuTime = 0;
            DoctorsUtilization = 0;
            VaccinationQuSize = 0;
            VaccinationQuTime = 0;
            NursesUtilization = 0;
        }

        private void RunSimulation()
        {
            _simRef.OrderedPatientsNum = _mw.SetOrderedPatients;
            _simRef.ResAdminWorkersCount = _mw.SetAdminWorkersCount;
            _simRef.ResDoctorsCount = _mw.SetDoctorsCount;
            _simRef.ResNursesCount = _mw.SetNursesCount;

            _simRef.OnReplicationDidFinish(s =>
            {
                var sim = (MySimulation) s;

                Application.Current.Dispatcher.Invoke(() =>
                {
                    ReplicationsCount = sim.CurrentReplication + 1;

                    RegistrationQuSize = sim.RegistrationQuSize.Mean();
                    RegistrationQuTime = sim.RegistrationQuTime.Mean();
                    AdminWorkersUtilization = sim.AdminWorkersUtilization.Mean();
                    ExaminationQuSize = sim.ExaminationQuSize.Mean();
                    ExaminationQuTime = sim.ExaminationQuTime.Mean();
                    DoctorsUtilization = sim.DoctorsUtilization.Mean();
                    VaccinationQuSize = sim.VaccinationQuSize.Mean();
                    VaccinationQuTime = sim.VaccinationQuTime.Mean();
                    NursesUtilization = sim.NursesUtilization.Mean();
                });
            });

            _simRef.Simulate(10000, double.MaxValue);
        }

        private void ButtonRepStart_Click(object sender, RoutedEventArgs e)
        {
            Thread simulationThread = new Thread(RunSimulation);
            simulationThread.Start();
        }

        private void ButtonRepPause_Click(object sender, RoutedEventArgs e)
        {
            _simRef.PauseSimulation();
        }

        private void ButtonRepStop_Click(object sender, RoutedEventArgs e)
        {
            _simRef.StopSimulation();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
