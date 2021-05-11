using OSPABA;
using simulation;
using agents;
namespace continualAssistants
{
	//meta! id="71"
	public class ProcessMovingRegToExa : Process
	{
		public ProcessMovingRegToExa(int id, Simulation mySim, CommonAgent myAgent) :
			base(id, mySim, myAgent)
		{
		}

		override public void PrepareReplication()
		{
			base.PrepareReplication();
			// Setup component for the next replication
		}

		//meta! sender="AgentCentrum", id="72", type="Start"
		public void ProcessStart(MessageForm message)
        {
            message.Code = Mc.NoticeProcessMovingRegToExaEnded;

            if (((MySimulation)MySim).EnableLightModel)
                Hold(0, message);
            else
				Hold(MyAgent.RandMovingRegToExaTime.Sample(), message);
        }

		//meta! userInfo="Process messages defined in code", id="0"
		public void ProcessDefault(MessageForm message)
		{
			switch (message.Code)
			{
            }
		}

		//meta! sender="AgentCentrum", id="137", type="Notice"
		public void ProcessNoticeProcessMovingRegToExaEnded(MessageForm message)
		{
            AssistantFinished(message);
		}

		//meta! userInfo="Generated code: do not modify", tag="begin"
		override public void ProcessMessage(MessageForm message)
		{
			switch (message.Code)
			{
			case Mc.NoticeProcessMovingRegToExaEnded:
				ProcessNoticeProcessMovingRegToExaEnded(message);
			break;

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