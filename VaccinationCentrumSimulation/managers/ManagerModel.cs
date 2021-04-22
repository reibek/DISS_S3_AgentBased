using OSPABA;
using simulation;
using agents;
using continualAssistants;
//using instantAssistants;
namespace managers
{
	//meta! id="1"
	public class ManagerModel : Manager
	{
		public ManagerModel(int id, Simulation mySim, Agent myAgent) :
			base(id, mySim, myAgent)
		{
			Init();
		}

        public override void PrepareReplication()
		{
			base.PrepareReplication();
        }

		//meta! sender="AgentSurrounding", id="30", type="Notice"
		public void ProcessNoticePatientArrival(MessageForm message)
		{
			message.Addressee = MySim.FindAgent(SimId.AgentCentrum);
            message.Code = Mc.NoticeNewPatient;
			Notice(message);
        }

		//meta! sender="AgentCentrum", id="37", type="Notice"
		public void ProcessNoticePatientLeave(MessageForm message)
		{
		}

		//meta! userInfo="Process messages defined in code", id="0"
		public void ProcessDefault(MessageForm message)
		{
			switch (message.Code)
			{
			}
		}

		//meta! userInfo="Generated code: do not modify", tag="begin"
		public void Init()
		{
		}

		override public void ProcessMessage(MessageForm message)
		{
			switch (message.Code)
			{
			case Mc.NoticePatientArrival:
				ProcessNoticePatientArrival(message);
			break;

			case Mc.NoticePatientLeave:
				ProcessNoticePatientLeave(message);
			break;

			default:
				ProcessDefault(message);
			break;
			}
		}
		//meta! tag="end"
		public new AgentModel MyAgent
		{
			get
			{
				return (AgentModel)base.MyAgent;
			}
		}
	}
}