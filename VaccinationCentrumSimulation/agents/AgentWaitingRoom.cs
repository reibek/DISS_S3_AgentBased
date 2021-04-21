using OSPABA;
using simulation;
using managers;
using continualAssistants;
//using instantAssistants;
namespace agents
{
	//meta! id="7"
	public class AgentWaitingRoom : Agent
	{
		public AgentWaitingRoom(int id, Simulation mySim, Agent parent) :
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
			new ManagerWaitingRoom(SimId.ManagerWaitingRoom, MySim, this);
			new ProcessWaitingRoom(SimId.ProcessWaitingRoom, MySim, this);
			AddOwnMessage(Mc.RequestWaitingRoom);
		}
		//meta! tag="end"
	}
}