using OSPABA;
using simulation;
using agents;
namespace continualAssistants
{
	//meta! id="77"
	public class ProcessMovingToFromCan : Process
	{
		public ProcessMovingToFromCan(int id, Simulation mySim, CommonAgent myAgent) :
			base(id, mySim, myAgent)
		{
		}

		override public void PrepareReplication()
		{
			base.PrepareReplication();
			// Setup component for the next replication
		}

		//meta! sender="AgentCentrum", id="78", type="Start"
		public void ProcessStart(MessageForm message)
        {
            message.Code = Mc.ProcessMovingToFromCanEnded;

            if (((MySimulation)MySim).EnableLightModel)
                Hold(0, message);
            else
				Hold(MyAgent.RandMovingToFromCan.Sample(), message);
        }

		//meta! userInfo="Process messages defined in code", id="0"
		public void ProcessDefault(MessageForm message)
		{
			switch (message.Code)
			{
				case Mc.ProcessMovingToFromCanEnded:
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