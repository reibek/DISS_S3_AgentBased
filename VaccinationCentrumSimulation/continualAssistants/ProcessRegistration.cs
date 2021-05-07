using OSPABA;
using simulation;
using agents;
namespace continualAssistants
{
	//meta! id="19"
	public class ProcessRegistration : Process
	{
		public ProcessRegistration(int id, Simulation mySim, CommonAgent myAgent) :
			base(id, mySim, myAgent)
		{
		}

		override public void PrepareReplication()
		{
			base.PrepareReplication();
			// Setup component for the next replication
		}

		//meta! sender="AgentRegistration", id="20", type="Start"
		public void ProcessStart(MessageForm message)
        {
            message.Code = Mc.NoticeProcessRegistrationEnded;
			Hold(((MessagePatient)message).AdminWorker.RandRegistrationTime.Sample(), message);
		}

		//meta! userInfo="Process messages defined in code", id="0"
		public void ProcessDefault(MessageForm message)
		{
			switch (message.Code)
			{
            }
		}

		//meta! sender="AgentRegistration", id="144", type="Notice"
		public void ProcessNoticeProcessRegistrationEnded(MessageForm message)
		{
            AssistantFinished(message);
		}

		//meta! userInfo="Generated code: do not modify", tag="begin"
		override public void ProcessMessage(MessageForm message)
		{
			switch (message.Code)
			{
			case Mc.NoticeProcessRegistrationEnded:
				ProcessNoticeProcessRegistrationEnded(message);
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