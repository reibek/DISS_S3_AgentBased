using OSPABA;
using simulation;
using agents;
namespace continualAssistants
{
	//meta! id="62"
	public class SchedulerDoctorBreak : Scheduler
	{
		public SchedulerDoctorBreak(int id, Simulation mySim, CommonAgent myAgent) :
			base(id, mySim, myAgent)
		{
		}

		override public void PrepareReplication()
		{
			base.PrepareReplication();
			// Setup component for the next replication
		}

		//meta! sender="AgentExamination", id="63", type="Start"
		public void ProcessStart(MessageForm message)
		{
            message.Code = Mc.NoticeTimeForBreak;
            Hold(13500, message); // 3:45 hours (11:45)
        }

		//meta! userInfo="Process messages defined in code", id="0"
		public void ProcessDefault(MessageForm message)
		{
			switch (message.Code)
			{
            }
		}

		//meta! sender="AgentExamination", id="146", type="Notice"
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
		public new AgentExamination MyAgent
		{
			get
			{
				return (AgentExamination)base.MyAgent;
			}
		}
	}
}