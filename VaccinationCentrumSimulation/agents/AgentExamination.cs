using OSPABA;
using simulation;
using managers;
using continualAssistants;
//using instantAssistants;
namespace agents
{
	//meta! id="5"
	public class AgentExamination : Agent
	{
		public AgentExamination(int id, Simulation mySim, Agent parent) :
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
			new ManagerExamination(SimId.ManagerExamination, MySim, this);
			new ProcessExamination(SimId.ProcessExamination, MySim, this);
			AddOwnMessage(Mc.RequestExamination);
		}
		//meta! tag="end"
	}
}