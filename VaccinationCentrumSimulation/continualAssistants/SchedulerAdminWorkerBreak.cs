using OSPABA;
using simulation;
using agents;
namespace continualAssistants
{
	//meta! id="59"
	public class SchedulerAdminWorkerBreak : Scheduler
	{
		public SchedulerAdminWorkerBreak(int id, Simulation mySim, CommonAgent myAgent) :
			base(id, mySim, myAgent)
		{
		}

		override public void PrepareReplication()
		{
			base.PrepareReplication();
			// Setup component for the next replication
		}

		//meta! sender="AgentRegistration", id="60", type="Start"
		public void ProcessStart(MessageForm message)
        {
            message.Code = Mc.NoticeTimeForBreak;
            Hold(10800, message); // 3 hours (11:00)
		}

		//meta! userInfo="Process messages defined in code", id="0"
		public void ProcessDefault(MessageForm message)
		{
			switch (message.Code)
			{
            }
		}

		//meta! sender="AgentRegistration", id="143", type="Notice"
		public void ProcessNoticeTimeForBreak(MessageForm message)
		{
            AssistantFinished(message);
		}

		//meta! userInfo="Generated code: do not modify", tag="begin"
		override public void ProcessMessage(MessageForm message)
		{
			switch (message.Code)
			{
			case Mc.NoticeTimeForBreak:
				ProcessNoticeTimeForBreak(message);
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
		public new AgentRegistration MyAgent
		{
			get
			{
				return (AgentRegistration)base.MyAgent;
			}
		}
	}
}