using OSPABA;
using simulation;
using agents;
using continualAssistants;
//using instantAssistants;
namespace managers
{
	//meta! id="4"
	public class ManagerRegistration : Manager
	{
		public ManagerRegistration(int id, Simulation mySim, Agent myAgent) :
			base(id, mySim, myAgent)
		{
			Init();
		}

		override public void PrepareReplication()
		{
			base.PrepareReplication();
			// Setup component for the next replication

			if (PetriNet != null)
			{
				PetriNet.Clear();
			}
		}

		//meta! sender="ProcessRegistration", id="20", type="Finish"
		public void ProcessFinish(MessageForm message)
		{
		}

		//meta! sender="AgentCentrum", id="32", type="Request"
		public void ProcessRequestRegistration(MessageForm message)
		{
		}

		//meta! userInfo="Process messages defined in code", id="0"
		public void ProcessDefault(MessageForm message)
		{
			switch (message.Code)
			{
			}
		}

		//meta! userInfo="Generated code: do not modify", tag="begin"
		public void Init()
		{
		}

		override public void ProcessMessage(MessageForm message)
		{
			switch (message.Code)
			{
			case Mc.RequestRegistration:
				ProcessRequestRegistration(message);
			break;

			case Mc.Finish:
				ProcessFinish(message);
			break;

			default:
				ProcessDefault(message);
			break;
			}
		}
		//meta! tag="end"
		public new AgentRegistration MyAgent
		{
			get
			{
				return (AgentRegistration)base.MyAgent;
			}
		}
	}
}