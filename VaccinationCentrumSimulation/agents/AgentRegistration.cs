using OSPABA;
using simulation;
using managers;
using continualAssistants;
using DataStructures;

//using instantAssistants;
namespace agents
{
	//meta! id="4"
	public class AgentRegistration : Agent
	{
        public Queue<MyMessage> QuRegistration { get; set; }

		public AgentRegistration(int id, Simulation mySim, Agent parent) :
			base(id, mySim, parent)
		{
			Init();

			QuRegistration = new Queue<MyMessage>();
		}

		public override void PrepareReplication()
		{
			base.PrepareReplication();
			
			QuRegistration.Clear();
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