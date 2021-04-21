using OSPABA;
using simulation;
using managers;
using continualAssistants;
//using instantAssistants;
namespace agents
{
	//meta! id="4"
	public class AgentRegistration : Agent
	{
		public AgentRegistration(int id, Simulation mySim, Agent parent) :
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
			new ManagerRegistration(SimId.ManagerRegistration, MySim, this);
			new ProcessRegistration(SimId.ProcessRegistration, MySim, this);
			AddOwnMessage(Mc.RequestRegistration);
		}
		//meta! tag="end"
	}
}