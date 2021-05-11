using OSPABA;
using simulation;
using agents;
namespace continualAssistants
{
	//meta! id="65"
	public class SchedulerNurseBreak : Scheduler
	{
		public SchedulerNurseBreak(int id, Simulation mySim, CommonAgent myAgent) :
			base(id, mySim, myAgent)
		{
		}

		override public void PrepareReplication()
		{
			base.PrepareReplication();
			// Setup component for the next replication
		}

		//meta! sender="AgentVaccination", id="66", type="Start"
		public void ProcessStart(MessageForm message)
		{
            message.Code = Mc.NoticeTimeForBreak;
            Hold(19800, message); // 5:30 hours (13:30)
		}

		//meta! userInfo="Process messages defined in code", id="0"
		public void ProcessDefault(MessageForm message)
		{
			switch (message.Code)
			{
            }
		}

		//meta! sender="AgentVaccination", id="148", type="Notice"
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
		public new AgentVaccination MyAgent
		{
			get
			{
				return (AgentVaccination)base.MyAgent;
			}
		}
	}
}