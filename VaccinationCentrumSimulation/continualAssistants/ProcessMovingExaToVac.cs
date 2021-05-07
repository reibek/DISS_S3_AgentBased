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
            message.Code = Mc.NoticeProcessMovingExaToVacEnded;

            if (((MySimulation)MySim).EnableLightModel)
                Hold(0, message);
            else
                Hold(MyAgent.RandMovingExaToVacTime.Sample(), message);
		}

		//meta! userInfo="Process messages defined in code", id="0"
		public void ProcessDefault(MessageForm message)
		{
			switch (message.Code)
			{
            }
		}

		//meta! sender="AgentCentrum", id="138", type="Notice"
		public void ProcessNoticeProcessMovingExaToVacEnded(MessageForm message)
		{
            AssistantFinished(message);
		}

		//meta! userInfo="Generated code: do not modify", tag="begin"
		override public void ProcessMessage(MessageForm message)
		{
			switch (message.Code)
			{
			case Mc.Start:
				ProcessStart(message);
			break;

			case Mc.NoticeProcessMovingExaToVacEnded:
				ProcessNoticeProcessMovingExaToVacEnded(message);
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