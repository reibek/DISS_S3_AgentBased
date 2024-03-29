using OSPABA;
using simulation;
using agents;
namespace continualAssistants
{
	//meta! id="68"
	public class ProcessEating : Process
	{
		public ProcessEating(int id, Simulation mySim, CommonAgent myAgent) :
			base(id, mySim, myAgent)
		{
		}

		override public void PrepareReplication()
		{
			base.PrepareReplication();
			// Setup component for the next replication
		}

		//meta! sender="AgentCanteen", id="69", type="Start"
		public void ProcessStart(MessageForm message)
		{
            message.Code = Mc.NoticeProcessEatingEnded;

            if (((MySimulation)MySim).EnableLightModel)
                Hold(0, message);
			else
                Hold(MyAgent.RandEatingTime.Sample(), message);

        }

		//meta! userInfo="Process messages defined in code", id="0"
		public void ProcessDefault(MessageForm message)
		{
			switch (message.Code)
			{
            }
		}

		//meta! sender="AgentCanteen", id="155", type="Notice"
		public void ProcessNoticeProcessEatingEnded(MessageForm message)
		{
            AssistantFinished(message);
		}

		//meta! userInfo="Generated code: do not modify", tag="begin"
		override public void ProcessMessage(MessageForm message)
		{
			switch (message.Code)
			{
			case Mc.NoticeProcessEatingEnded:
				ProcessNoticeProcessEatingEnded(message);
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
		public new AgentCanteen MyAgent
		{
			get
			{
				return (AgentCanteen)base.MyAgent;
			}
		}
	}
}