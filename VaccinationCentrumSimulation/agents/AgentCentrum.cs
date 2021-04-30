using OSPABA;
using simulation;
using managers;
using continualAssistants;
//using instantAssistants;
namespace agents
{
	//meta! id="3"
	public class AgentCentrum : Agent
    {
        public int ArrivedPatientsCount { get; set; }
		public int VaccinatedPatientsCount { get; set; }

		public AgentCentrum(int id, Simulation mySim, Agent parent) :
			base(id, mySim, parent)
		{
			Init();
        }

		override public void PrepareReplication()
		{
			base.PrepareReplication();

            ArrivedPatientsCount = 0;
			VaccinatedPatientsCount = 0;
        }

		//meta! userInfo="Generated code: do not modify", tag="begin"
		private void Init()
		{
			new ManagerCentrum(SimId.ManagerCentrum, MySim, this);
			new ProcessMovingRegToExa(SimId.ProcessMovingRegToExa, MySim, this);
			new ProcessMovingToFromCan(SimId.ProcessMovingToFromCan, MySim, this);
			new ProcessMovingVacToWai(SimId.ProcessMovingVacToWai, MySim, this);
			new ProcessMovingExaToVac(SimId.ProcessMovingExaToVac, MySim, this);
			AddOwnMessage(Mc.RequestNurseBreak);
			AddOwnMessage(Mc.RequestExamination);
			AddOwnMessage(Mc.RequestWaitingRoom);
			AddOwnMessage(Mc.RequestAdminWorkerBreak);
			AddOwnMessage(Mc.NoticeNewPatient);
			AddOwnMessage(Mc.RequestEmployeeLunch);
			AddOwnMessage(Mc.RequestVaccination);
			AddOwnMessage(Mc.RequestDoctorBreak);
			AddOwnMessage(Mc.RequestRegistration);
		}
		//meta! tag="end"
	}
}