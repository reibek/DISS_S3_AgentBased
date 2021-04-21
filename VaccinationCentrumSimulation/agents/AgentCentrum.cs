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
		public AgentCentrum(int id, Simulation mySim, Agent parent) :
			base(id, mySim, parent)
		{
			Init();
		}

		override public void PrepareReplication()
		{
			base.PrepareReplication();
			// Setup component for the next replication
		}

		//meta! userInfo="Generated code: do not modify", tag="begin"
		private void Init()
		{
			new ManagerCentrum(SimId.ManagerCentrum, MySim, this);
			AddOwnMessage(Mc.RequestExamination);
			AddOwnMessage(Mc.RequestWaitingRoom);
			AddOwnMessage(Mc.NoticeNewPatient);
			AddOwnMessage(Mc.RequestVaccination);
			AddOwnMessage(Mc.RequestRegistration);
		}
		//meta! tag="end"
	}
}