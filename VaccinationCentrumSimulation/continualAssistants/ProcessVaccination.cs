using OSPABA;
using simulation;
using agents;
namespace continualAssistants
{
	//meta! id="25"
	public class ProcessVaccination : Process
	{
		public ProcessVaccination(int id, Simulation mySim, CommonAgent myAgent) :
			base(id, mySim, myAgent)
		{
		}

		public override void PrepareReplication()
		{
			base.PrepareReplication();
			// Setup component for the next replication
		}

		//meta! sender="AgentVaccination", id="26", type="Start"
		public void ProcessStart(MessageForm message)
        {
            message.Code = Mc.NoticeProcessVaccinationEnded;
			Hold(((MessagePatient) message).Nurse.RandVaccinationTime.Sample(), message);
        }

		//meta! userInfo="Process messages defined in code", id="0"
		public void ProcessDefault(MessageForm message)
		{
			switch (message.Code)
			{
            }
		}

		//meta! sender="AgentVaccination", id="149", type="Notice"
		public void ProcessNoticeProcessVaccinationEnded(MessageForm message)
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

			case Mc.NoticeProcessVaccinationEnded:
				ProcessNoticeProcessVaccinationEnded(message);
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