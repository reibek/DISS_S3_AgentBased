using OSPABA;
using simulation;
using agents;
using continualAssistants;
//using instantAssistants;
namespace managers
{
	//meta! id="3"
	public class ManagerCentrum : Manager
	{
		public ManagerCentrum(int id, Simulation mySim, Agent myAgent) :
			base(id, mySim, myAgent)
		{
			Init();
		}

		public override void PrepareReplication()
		{
			base.PrepareReplication();
			// Setup component for the next replication

			if (PetriNet != null)
			{
				PetriNet.Clear();
			}
		}

		//meta! sender="AgentExamination", id="33", type="Response"
		public void ProcessRequestExamination(MessageForm message)
		{
            message.Addressee = MySim.FindAgent(SimId.AgentVaccination);
            message.Code = Mc.RequestVaccination;
            Request(message);
		}

		//meta! sender="AgentWaitingRoom", id="35", type="Response"
		public void ProcessRequestWaitingRoom(MessageForm message)
        {
            MyAgent.VaccinatedPatientsCount++;
			message.Addressee = MySim.FindAgent(SimId.AgentModel);
            message.Code = Mc.NoticePatientLeave;
			Notice(message);

            if (MyAgent.VaccinatedPatientsCount == MyAgent.ArrivedPatientsCount)
            {
                MySim.StopReplication();
            }
        }

		//meta! sender="AgentModel", id="31", type="Notice"
		public void ProcessNoticeNewPatient(MessageForm message)
		{
            MyAgent.ArrivedPatientsCount++;
			message.Addressee = MySim.FindAgent(SimId.AgentRegistration);
            message.Code = Mc.RequestRegistration;
            Request(message);
		}

		//meta! sender="AgentVaccination", id="34", type="Response"
		public void ProcessRequestVaccination(MessageForm message)
        {
            message.Addressee = MySim.FindAgent(SimId.AgentWaitingRoom);
            message.Code = Mc.RequestWaitingRoom;
			Request(message);
        }

		//meta! sender="AgentRegistration", id="32", type="Response"
		public void ProcessRequestRegistration(MessageForm message)
        {
            message.Addressee = MySim.FindAgent(SimId.AgentExamination);
            message.Code = Mc.RequestExamination;
			Request(message);
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

		public override void ProcessMessage(MessageForm message)
		{
			switch (message.Code)
			{
			case Mc.RequestVaccination:
				ProcessRequestVaccination(message);
			break;

			case Mc.NoticeNewPatient:
				ProcessNoticeNewPatient(message);
			break;

			case Mc.RequestRegistration:
				ProcessRequestRegistration(message);
			break;

			case Mc.RequestWaitingRoom:
				ProcessRequestWaitingRoom(message);
			break;

			case Mc.RequestExamination:
				ProcessRequestExamination(message);
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