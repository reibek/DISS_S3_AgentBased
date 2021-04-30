using OSPABA;
using simulation;
using managers;
using continualAssistants;
//using instantAssistants;

namespace agents
{
	//meta! id="47"
	public class AgentCanteen : Agent
	{
		public AgentCanteen(int id, Simulation mySim, Agent parent) :
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
			new ManagerCanteen(SimId.ManagerCanteen, MySim, this);
			new ProcessEating(SimId.ProcessEating, MySim, this);
			AddOwnMessage(Mc.RequestEmployeeLunch);
		}
		//meta! tag="end"
	}
}