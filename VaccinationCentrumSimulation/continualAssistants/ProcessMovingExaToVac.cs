using OSPABA;
using simulation;
using agents;
namespace continualAssistants
{
	//meta! id="73"
	public class ProcessMovingExaToVac : Process
	{
		public ProcessMovingExaToVac(int id, Simulation mySim, CommonAgent myAgent) :
			base(id, mySim, myAgent)
		{
		}

		override public void PrepareReplication()
		{
			base.PrepareReplication();
			// Setup component for the next replication
		}

		//meta! sender="AgentCentrum", id="74", type="Start"
		public void ProcessStart(MessageForm message)
		{
            message.Code = Mc.ProcessMovingExaToVacEnded;
            Hold(MyAgent.RandMovingExaToVacTime.Sample(), message);
		}

		//meta! userInfo="Process messages defined in code", id="0"
		public void ProcessDefault(MessageForm message)
		{
			switch (message.Code)
			{
                case Mc.ProcessMovingExaToVacEnded:
                    AssistantFinished(message);
                    break;
			}
		}

		//meta! userInfo="Generated code: do not modify", tag="begin"
		override public void ProcessMessage(MessageForm message)
		{
			switch (message.Code)
			{
			case Mc.Start:
				ProcessStart(message);
			break;

			default:
				ProcessDefault(message);
			break;
			}
		}
		//meta! tag="end"
		public new AgentCentrum MyAgent
		{
			get
			{
				return (AgentCentrum)base.MyAgent;
			}
		}
	}
}