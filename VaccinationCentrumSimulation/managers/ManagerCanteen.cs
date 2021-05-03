using OSPABA;
using simulation;
using agents;
using continualAssistants;
//using instantAssistants;

namespace managers
{
	//meta! id="47"
	public class ManagerCanteen : Manager
	{
		public ManagerCanteen(int id, Simulation mySim, Agent myAgent) :
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

		//meta! sender="ProcessEating", id="69", type="Finish"
		public void ProcessFinish(MessageForm message)
		{
		}

		//meta! sender="AgentCentrum", id="57", type="Request"
		public void ProcessRequestEmployeeLunch(MessageForm message)
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
			case Mc.Finish:
				ProcessFinish(message);
			break;

			case Mc.RequestEmployeeLunch:
				ProcessRequestEmployeeLunch(message);
			break;

			default:
				ProcessDefault(message);
			break;
			}
		}
		//meta! tag="end"
		public new AgentCanteen MyAgent
		{
			get
			{
				return (AgentCanteen)base.MyAgent;
			}
		}
	}
}