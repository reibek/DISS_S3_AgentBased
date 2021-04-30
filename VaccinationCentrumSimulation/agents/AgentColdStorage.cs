using OSPABA;
using simulation;
using managers;
using continualAssistants;
//using instantAssistants;

namespace agents
{
	//meta! id="44"
	public class AgentColdStorage : Agent
	{
		public AgentColdStorage(int id, Simulation mySim, Agent parent) :
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
			new ManagerColdStorage(SimId.ManagerColdStorage, MySim, this);
			new ProcessFillingSyringes(SimId.ProcessFillingSyringes, MySim, this);
			AddOwnMessage(Mc.RequestFillSyringes);
		}
		//meta! tag="end"
	}
}