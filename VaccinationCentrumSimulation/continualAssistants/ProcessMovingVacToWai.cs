using OSPABA;
using simulation;
using agents;
namespace continualAssistants
{
	//meta! id="75"
	public class ProcessMovingVacToWai : Process
	{
		public ProcessMovingVacToWai(int id, Simulation mySim, CommonAgent myAgent) :
			base(id, mySim, myAgent)
		{
		}

		public override void PrepareReplication()
		{
			base.PrepareReplication();
			// Setup component for the next replication
		}

		//meta! sender="AgentCentrum", id="76", type="Start"
		public void ProcessStart(MessageForm message)
		{
            message.Code = Mc.NoticeProcessMovingVacToWaiEnded;

            if (((MySimulation)MySim).EnableLightModel)
                Hold(0, message);
            else
				Hold(MyAgent.RandMovingVacToWaiTime.Sample(), message);
		}

		//meta! userInfo="Process messages defined in code", id="0"
		public void ProcessDefault(MessageForm message)
		{
			switch (message.Code)
			{
            }
		}

		//meta! sender="AgentCentrum", id="139", type="Notice"
		public void ProcessNoticeProcessMovingVacToWaiEnded(MessageForm message)
		{
            AssistantFinished(message);
		}

		//meta! userInfo="Generated code: do not modify", tag="begin"
		override public void ProcessMessage(MessageForm message)
		{
			switch (message.Code)
			{
			case Mc.NoticeProcessMovingVacToWaiEnded:
				ProcessNoticeProcessMovingVacToWaiEnded(message);
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