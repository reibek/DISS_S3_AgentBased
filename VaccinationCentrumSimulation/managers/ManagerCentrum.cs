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
            message.Addressee = MyAgent.FindAssistant(SimId.ProcessMovingExaToVac);
            StartContinualAssistant(message);

            MyAgent.MovingPatientsExaToVac++;
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
                ((MySimulation) MySim).CurrentReplicationDuration = MySim.CurrentTime;
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
            message.Addressee = MyAgent.FindAssistant(SimId.ProcessMovingVacToWai);
            StartContinualAssistant(message);

            MyAgent.MovingPatientsVacToWai++;
        }

		//meta! sender="AgentRegistration", id="32", type="Response"
		public void ProcessRequestRegistration(MessageForm message)
        {
            message.Addressee = MyAgent.FindAssistant(SimId.ProcessMovingRegToExa);
            StartContinualAssistant(message);

            MyAgent.MovingPatientsRegToExa++;
        }

		//meta! userInfo="Process messages defined in code", id="0"
		public void ProcessDefault(MessageForm message)
		{
			switch (message.Code)
			{
			}
		}

		//meta! sender="AgentVaccination", id="54", type="Request"
		public void ProcessRequestNurseBreak(MessageForm message)
		{
		}

		//meta! sender="AgentRegistration", id="52", type="Request"
		public void ProcessRequestAdminWorkerBreak(MessageForm message)
		{
		}

		//meta! sender="ProcessMovingRegToExa", id="72", type="Finish"
		public void ProcessFinishProcessMovingRegToExa(MessageForm message)
		{
            message.Addressee = MySim.FindAgent(SimId.AgentExamination);
            message.Code = Mc.RequestExamination;
            Request(message);

            MyAgent.MovingPatientsRegToExa--;
		}

		//meta! sender="ProcessMovingVacToWai", id="76", type="Finish"
		public void ProcessFinishProcessMovingVacToWai(MessageForm message)
		{
            message.Addressee = MySim.FindAgent(SimId.AgentWaitingRoom);
            message.Code = Mc.RequestWaitingRoom;
            Request(message);

            MyAgent.MovingPatientsVacToWai--;
		}

		//meta! sender="ProcessMovingExaToVac", id="74", type="Finish"
		public void ProcessFinishProcessMovingExaToVac(MessageForm message)
		{
            message.Addressee = MySim.FindAgent(SimId.AgentVaccination);
            message.Code = Mc.RequestVaccination;
            Request(message);

            MyAgent.MovingPatientsExaToVac--;
		}

		//meta! sender="ProcessMovingToFromCan", id="78", type="Finish"
		public void ProcessFinishProcessMovingToFromCan(MessageForm message)
		{
		}

		//meta! sender="AgentCanteen", id="57", type="Response"
		public void ProcessRequestEmployeeLunch(MessageForm message)
		{
		}

		//meta! sender="AgentExamination", id="53", type="Request"
		public void ProcessRequestDoctorBreak(MessageForm message)
		{
		}

		//meta! userInfo="Generated code: do not modify", tag="begin"
		public void Init()
		{
		}

		override public void ProcessMessage(MessageForm message)
		{
			switch (message.Code)
			{
			case Mc.RequestEmployeeLunch:
				ProcessRequestEmployeeLunch(message);
			break;

			case Mc.RequestNurseBreak:
				ProcessRequestNurseBreak(message);
			break;

			case Mc.Finish:
				switch (message.Sender.Id)
				{
				case SimId.ProcessMovingRegToExa:
					ProcessFinishProcessMovingRegToExa(message);
				break;

				case SimId.ProcessMovingVacToWai:
					ProcessFinishProcessMovingVacToWai(message);
				break;

				case SimId.ProcessMovingExaToVac:
					ProcessFinishProcessMovingExaToVac(message);
				break;

				case SimId.ProcessMovingToFromCan:
					ProcessFinishProcessMovingToFromCan(message);
				break;
				}
			break;

			case Mc.RequestRegistration:
				ProcessRequestRegistration(message);
			break;

			case Mc.RequestExamination:
				ProcessRequestExamination(message);
			break;

			case Mc.RequestAdminWorkerBreak:
				ProcessRequestAdminWorkerBreak(message);
			break;

			case Mc.RequestVaccination:
				ProcessRequestVaccination(message);
			break;

			case Mc.NoticeNewPatient:
				ProcessNoticeNewPatient(message);
			break;

			case Mc.RequestWaitingRoom:
				ProcessRequestWaitingRoom(message);
			break;

			case Mc.RequestDoctorBreak:
				ProcessRequestDoctorBreak(message);
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