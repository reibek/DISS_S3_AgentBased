using OSPABA;
using simulation;
using agents;
namespace continualAssistants
{
	//meta! id="28"
	public class ProcessWaitingRoom : Process
	{
		public ProcessWaitingRoom(int id, Simulation mySim, CommonAgent myAgent) :
			base(id, mySim, myAgent)
		{
		}

		public override void PrepareReplication()
		{
			base.PrepareReplication();
			// Setup component for the next replication
		}

		//meta! sender="AgentWaitingRoom", id="29", type="Start"
		public void ProcessStart(MessageForm message)
        {
            message.Code = Mc.NoticeProcessWaitingRoomEnded;
            Hold(((MessagePatient) message).Patient.WaitingRoomTime, message);
        }

		//meta! userInfo="Process messages defined in code", id="0"
		public void ProcessDefault(MessageForm message)
		{
			switch (message.Code)
			{
            }
		}

		//meta! sender="AgentWaitingRoom", id="152", type="Notice"
		public void ProcessNoticeProcessWaitingRoomEnded(MessageForm message)
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

			case Mc.NoticeProcessWaitingRoomEnded:
				ProcessNoticeProcessWaitingRoomEnded(message);
			break;

			default:
				ProcessDefault(message);
			break;
			}
		}
		//meta! tag="end"
		public new AgentWaitingRoom MyAgent
		{
			get
			{
				return (AgentWaitingRoom)base.MyAgent;
			}
		}
	}
}