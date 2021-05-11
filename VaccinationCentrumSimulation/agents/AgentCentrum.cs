using OSPABA;
using simulation;
using managers;
using continualAssistants;
using OSPRNG;

//using instantAssistants;
namespace agents
{
	//meta! id="3"
	public class AgentCentrum : Agent
    {
        public UniformContinuousRNG RandMovingRegToExaTime { get; set; }
        public UniformContinuousRNG RandMovingExaToVacTime { get; set; }
        public UniformContinuousRNG RandMovingVacToWaiTime { get; set; }
        public UniformContinuousRNG RandMovingToFromCan { get; set; }
        public int ArrivedPatientsCount { get; set; }
		public int VaccinatedPatientsCount { get; set; }
        public int MovingPatientsRegToExa { get; set; }
        public int MovingPatientsExaToVac { get; set; }
        public int MovingPatientsVacToWai { get; set; }
        public int MovingEmployeesToCan { get; set; }
        public int MovingEmployeesFromCan { get; set; }

		public AgentCentrum(int id, Simulation mySim, Agent parent) :
			base(id, mySim, parent)
		{
			Init();

            RandMovingRegToExaTime = new UniformContinuousRNG(40, 90, ((MySimulation)MySim).RandSeedGenerator);
			RandMovingExaToVacTime = new UniformContinuousRNG(20, 45, ((MySimulation)MySim).RandSeedGenerator);
			RandMovingVacToWaiTime = new UniformContinuousRNG(45, 110, ((MySimulation)MySim).RandSeedGenerator);
			RandMovingToFromCan = new UniformContinuousRNG(70, 200, ((MySimulation)MySim).RandSeedGenerator);

        }

		override public void PrepareReplication()
		{
			base.PrepareReplication();

            ArrivedPatientsCount = 0;
			VaccinatedPatientsCount = 0;
            MovingPatientsRegToExa = 0;
            MovingPatientsExaToVac = 0;
            MovingPatientsVacToWai = 0;
            MovingEmployeesToCan = 0;
            MovingEmployeesFromCan = 0;
        }

		//meta! userInfo="Generated code: do not modify", tag="begin"
		private void Init()
		{
			new ManagerCentrum(SimId.ManagerCentrum, MySim, this);
			new ProcessMovingExaToVac(SimId.ProcessMovingExaToVac, MySim, this);
			new ProcessMovingVacToWai(SimId.ProcessMovingVacToWai, MySim, this);
			new ProcessMovingRegToExa(SimId.ProcessMovingRegToExa, MySim, this);
			new ProcessMovingToFromCan(SimId.ProcessMovingToFromCan, MySim, this);
			AddOwnMessage(Mc.Initialization);
			AddOwnMessage(Mc.NoticeProcessMovingVacToWaiEnded);
			AddOwnMessage(Mc.NoticeProcessMovingExaToVacEnded);
			AddOwnMessage(Mc.RequestNurseBreak);
			AddOwnMessage(Mc.NoticeProcessMovingToFromCanEnded);
			AddOwnMessage(Mc.NoticeNewPatient);
			AddOwnMessage(Mc.RequestEmployeeLunch);
			AddOwnMessage(Mc.RequestRegistration);
			AddOwnMessage(Mc.RequestExamination);
			AddOwnMessage(Mc.RequestWaitingRoom);
			AddOwnMessage(Mc.RequestAdminWorkerBreak);
			AddOwnMessage(Mc.RequestVaccination);
			AddOwnMessage(Mc.RequestDoctorBreak);
			AddOwnMessage(Mc.NoticeProcessMovingRegToExaEnded);
		}
		//meta! tag="end"
	}
}