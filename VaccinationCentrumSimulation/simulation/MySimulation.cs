using System;
using OSPABA;
using agents;
using entities;
using OSPStat;
using Priority_Queue;

namespace simulation
{
	public class MySimulation : Simulation
    {
        public Random RandSeedGenerator { get; private set; }
        public int OrderedPatientsNum { get; set; }
        public int ResAdminWorkersCount { get; set; }
        public int ResDoctorsCount { get; set; }
        public int ResNursesCount { get; set; }
        public bool EnableEarlyArrivals { get; set; }
        public bool EnableLightModel { get; set; }
        public SimplePriorityQueue<EntityPatient> PreGeneratedPatients { get; set; }

        #region STATISTICS

        public Stat RegistrationQuSize { get; set; }
        public Stat RegistrationQuTime { get; set; }
        public Stat AdminWorkersUtilization { get; set; }
        public Stat ExaminationQuSize { get; set; }
        public Stat ExaminationQuTime { get; set; }
        public Stat DoctorsUtilization { get; set; }
        public Stat VaccinationQuSize { get; set; }
        public Stat VaccinationQuTime { get; set; }
        public Stat NursesUtilization { get; set; }
        public Stat WaitingRoomQuSize { get; set; }
        public double CurrentReplicationDuration { get; set; }

        #endregion

        public MySimulation()
		{
            RandSeedGenerator = new Random(); RandSeedGenerator = new Random();
            OrderedPatientsNum = 540;
            ResAdminWorkersCount = 5;
            ResDoctorsCount = 6;
            ResNursesCount = 3;
            EnableEarlyArrivals = false;
            EnableLightModel = false;
            PreGeneratedPatients = new SimplePriorityQueue<EntityPatient>();
			
            Init();

            RegistrationQuSize = new Stat();
            RegistrationQuTime = new Stat();
            AdminWorkersUtilization = new Stat();
            ExaminationQuSize = new Stat();
            ExaminationQuTime = new Stat();
            DoctorsUtilization = new Stat();
            VaccinationQuSize = new Stat();
            VaccinationQuTime = new Stat();
            NursesUtilization = new Stat();
            WaitingRoomQuSize = new Stat();

            CurrentReplicationDuration = 0;
        }

        protected override void PrepareSimulation()
        {
            base.PrepareSimulation();

            PreGeneratedPatients.Clear();

            RegistrationQuSize.Clear();
            RegistrationQuTime.Clear();
            AdminWorkersUtilization.Clear();
            ExaminationQuSize.Clear();
            ExaminationQuTime.Clear();
            DoctorsUtilization.Clear();
            VaccinationQuSize.Clear();
            VaccinationQuTime.Clear();
            NursesUtilization.Clear();
            WaitingRoomQuSize.Clear();

            CurrentReplicationDuration = 0;
        }

        protected override void PrepareReplication()
        {
            base.PrepareReplication();
        }

        protected override void ReplicationFinished()
        {
            RegistrationQuSize.AddSample(AgentRegistration.StatQuRegistrationSize.Mean());
            RegistrationQuTime.AddSample(AgentRegistration.StatQuRegistrationTime.Mean());
            AdminWorkersUtilization.AddSample(AgentRegistration.PoolAdminWorkers.AverageWorkingTime() /
                                              CurrentReplicationDuration);
            
            ExaminationQuSize.AddSample(AgentExamination.StatQuExaminationSize.Mean());
            ExaminationQuTime.AddSample(AgentExamination.StatQuExaminationTime.Mean());
            DoctorsUtilization.AddSample(AgentExamination.PoolDoctors.AverageWorkingTime() /
                                         CurrentReplicationDuration);
            
            VaccinationQuSize.AddSample(AgentVaccination.StatQuVaccinationSize.Mean());
            VaccinationQuTime.AddSample(AgentVaccination.StatQuVaccinationTime.Mean());
            NursesUtilization.AddSample(AgentVaccination.PoolNurses.AverageWorkingTime() / 
                                        CurrentReplicationDuration);

            WaitingRoomQuSize.AddSample(AgentWaitingRoom.StatWaitingPatientsCount.Mean());

            base.ReplicationFinished();
		}

        protected override void SimulationFinished()
        {
            base.SimulationFinished();
        }

		//meta! userInfo="Generated code: do not modify", tag="begin"
		private void Init()
		{
			AgentModel = new AgentModel(SimId.AgentModel, this, null);
			AgentSurrounding = new AgentSurrounding(SimId.AgentSurrounding, this, AgentModel);
			AgentCentrum = new AgentCentrum(SimId.AgentCentrum, this, AgentModel);
			AgentRegistration = new AgentRegistration(SimId.AgentRegistration, this, AgentCentrum);
			AgentExamination = new AgentExamination(SimId.AgentExamination, this, AgentCentrum);
			AgentVaccination = new AgentVaccination(SimId.AgentVaccination, this, AgentCentrum);
			AgentWaitingRoom = new AgentWaitingRoom(SimId.AgentWaitingRoom, this, AgentCentrum);
			AgentColdStorage = new AgentColdStorage(SimId.AgentColdStorage, this, AgentVaccination);
			AgentCanteen = new AgentCanteen(SimId.AgentCanteen, this, AgentCentrum);
		}
		public AgentModel AgentModel
		{ get; set; }
		public AgentSurrounding AgentSurrounding
		{ get; set; }
		public AgentCentrum AgentCentrum
		{ get; set; }
		public AgentRegistration AgentRegistration
		{ get; set; }
		public AgentExamination AgentExamination
		{ get; set; }
		public AgentVaccination AgentVaccination
		{ get; set; }
		public AgentWaitingRoom AgentWaitingRoom
		{ get; set; }
		public AgentColdStorage AgentColdStorage
		{ get; set; }
		public AgentCanteen AgentCanteen
		{ get; set; }

        //meta! tag="end"
	}
}