using OSPABA;
using simulation;
using agents;
namespace continualAssistants
{
	//meta! id="22"
	public class ProcessExamination : Process
	{
		public ProcessExamination(int id, Simulation mySim, CommonAgent myAgent) :
			base(id, mySim, myAgent)
		{
		}

		public override void PrepareReplication()
		{
			base.PrepareReplication();
			// Setup component for the next replication
		}

		//meta! sender="AgentExamination", id="23", type="Start"
		public void ProcessStart(MessageForm message)
        {
            ((MessagePatient) message).Patient.WaitingRoomTime =
                ((MessagePatient) message).Doctor.RandWaitingRoomTimeDecision.Sample() < 0.95 ? 900 : 1800;
            message.Code = Mc.ProcessExaminationEnded;
            Hold(((MessagePatient)message).Doctor.RandExaminationTime.Sample(), message);
		}

		//meta! userInfo="Process messages defined in code", id="0"
		public void ProcessDefault(MessageForm message)
		{
			switch (message.Code)
			{
                case Mc.ProcessExaminationEnded:
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
		public new AgentExamination MyAgent
		{
			get
			{
				return (AgentExamination)base.MyAgent;
			}
		}
	}
}