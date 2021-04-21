using OSPABA;
using simulation;
using managers;
using continualAssistants;
//using instantAssistants;
namespace agents
{
	//meta! id="6"
	public class AgentVaccination : Agent
	{
		public AgentVaccination(int id, Simulation mySim, Agent parent) :
			base(id, mySim, parent)
		{
			Init();
		}

		public override void PrepareReplication()
		{
			base.PrepareReplication();
			// Setup component for the next replication
		}

		//meta! userInfo="Generated code: do not modify", tag="begin"
		private void Init()
		{
			new ManagerVaccination(SimId.ManagerVaccination, MySim, this);
			new ProcessVaccination(SimId.ProcessVaccination, MySim, this);
			AddOwnMessage(Mc.RequestVaccination);
		}
		//meta! tag="end"
	}
}